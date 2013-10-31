using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KakaoTalkPC_CA_Replacer
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UInt32 nSize, ref UInt32 lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public AllocationProtect AllocationProtect;
            public uint RegionSize;
            public StateEnum State;
            public AllocationProtect Protect;
            public TypeEnum Type;
        }
        public enum AllocationProtect : uint
        {
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400
        }
        public enum StateEnum : uint
        {
            MEM_COMMIT = 0x1000,
            MEM_FREE = 0x10000,
            MEM_RESERVE = 0x2000
        }
        public enum TypeEnum : uint
        {
            MEM_IMAGE = 0x1000000,
            MEM_MAPPED = 0x40000,
            MEM_PRIVATE = 0x20000
        }


        IntPtr handle;
        IntPtr certAddr;
        Int32 certLength;
        Byte[] certContent;

        public Form1()
        {
            InitializeComponent();
            initialize();
        }

        private bool findKakaoProcess()
        {
            Process[] procList = Process.GetProcessesByName("KakaoTalk");
            if (procList.Length > 0)
            {
                handle = procList.First().Handle;
                readBtn.Enabled = true;
                return true;
            }
            return false;
        }

        private void initialize()
        {
            handle = IntPtr.Zero;
            certAddr = IntPtr.Zero;
            certLength = 0;
            readBtn.Enabled = false;
            writeBtn.Enabled = false;
            newTextBox.Enabled = false;
            origTextBox.Enabled = false;
            origTextBox.Text = "";

            if (!findKakaoProcess())
            {
                MessageBox.Show("Failed to find KakaoTalk process.\nPlease run KakaoTalk, then hit refresh button.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setStatusLabel("KakaoTalk PC process not found. Run KakaoTalk and refresh.", false);
                return;
            }

            readBtn.Enabled = true;
            origTextBox.Enabled = true;
            origTextBox.ReadOnly = true;
            setStatusLabel("Successfully found KakaoTalk PC process.", true);

            // Making CA Cert more readable
            origTextBox.Font = new Font(FontFamily.GenericMonospace, origTextBox.Font.Size);
            newTextBox.Font = new Font(FontFamily.GenericMonospace, newTextBox.Font.Size);
        }

        private bool findCACertificate()
        {
            Byte[] certHeader = System.Text.Encoding.ASCII.GetBytes("-----BEGIN CERTIFICATE-----");
            Byte[] certFooter = System.Text.Encoding.ASCII.GetBytes("-----END CERTIFICATE-----\n");
            MEMORY_BASIC_INFORMATION mbi;
            IntPtr ptr;

            // Silly Themida...
            findKakaoProcess();
            for (ptr = IntPtr.Zero; VirtualQueryEx(handle, ptr, out mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) == Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)); ptr = IntPtr.Add(ptr, (int)mbi.RegionSize))
            {
                Byte[] buffer = new Byte[(UInt32)mbi.RegionSize];
                UInt32 bytesRead = 0;
                if (ReadProcessMemory(handle, mbi.BaseAddress, buffer, (UInt32)buffer.Length, ref bytesRead))
                {
                    long offset = ExtensionHelpers.IndexOf(buffer, certHeader);
                    if (offset != -1)
                    {
                        certAddr = IntPtr.Add(mbi.BaseAddress, (int)offset);
                    }
                    offset = ExtensionHelpers.IndexOf(buffer, certFooter);
                    if (offset != -1)
                    {
                        certLength = IntPtr.Add(mbi.BaseAddress, (int)offset).ToInt32() - certAddr.ToInt32() + certFooter.Length;
                        break;
                    }
                }
            }

            return !(certAddr.Equals(IntPtr.Zero) && certLength > 0);
        }

        private Byte[] getCACertificate()
        {
            Byte[] buffer = new Byte[certLength];
            UInt32 bytesRead = 0;
            
            // Silly Themida..
            findKakaoProcess();

            if (ReadProcessMemory(handle, certAddr, buffer, (UInt32)buffer.Length, ref bytesRead))
            {
                return buffer;
            }
            return null;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            initialize();
        }

        private void readBtn_Click(object sender, EventArgs e)
        {
            origTextBox.Text = "";
            findCACertificate();

            if ((certContent = getCACertificate()) != null)
            {
                origTextBox.Text = System.Text.Encoding.ASCII.GetString(certContent).Replace("\n", "\r\n");
                setStatusLabel("Successfully read CA cert in memory.", true);

                newTextBox.Enabled = true;
                writeBtn.Enabled = true;
            }
            else
            {
                origTextBox.Text = "";
                setStatusLabel("Failed to find CA certificate in memory.", false);
            }
        }

        private void setStatusLabel(string label, bool success)
        {
            statusLabel.Text = label;
            statusLabel.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        private bool setCACertificate(Byte[] newCert)
        {
            if (newCert.Length > certLength)
            {
                MessageBox.Show("Your CA cert is longer than the original. Abort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            IntPtr bytesWrote;

            // Silly Themida..
            findKakaoProcess();

            return WriteProcessMemory(handle, certAddr, newCert, (UIntPtr)newCert.Length, out bytesWrote);
        }

        private void writeBtn_Click(object sender, EventArgs e)
        {
            if (newTextBox.Text == "")
            {
                MessageBox.Show("New CA cert cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newTextBox.Text.Last() != '\n')
                newTextBox.Text += "\r\n";
            Byte[] newCert = System.Text.Encoding.ASCII.GetBytes(newTextBox.Text.Replace("\r\n", "\n"));
            if (setCACertificate(newCert))
                setStatusLabel("Successfully overwrote new CA cert into memory. Enjoy your MITM :)", true);
            else
                setStatusLabel("Failed to overwrite new CA cert into memory.", false);
        }
    }
}
