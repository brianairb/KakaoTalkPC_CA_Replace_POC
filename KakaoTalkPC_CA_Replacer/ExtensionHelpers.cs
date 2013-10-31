using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KakaoTalkPC_CA_Replacer
{
    static class ExtensionHelpers
    {
        // Taken from http://stackoverflow.com/questions/283456/byte-array-pattern-search/5227131#5227131
        public static unsafe long IndexOf(this byte[] Haystack, byte[] Needle)
        {
            fixed (byte* H = Haystack) fixed (byte* N = Needle)
            {
                long i = 0;
                for (byte* hNext = H, hEnd = H + Haystack.LongLength; hNext < hEnd; i++, hNext++)
                {
                    bool Found = true;
                    for (byte* hInc = hNext, nInc = N, nEnd = N + Needle.LongLength; Found && nInc < nEnd; Found = *nInc == *hInc, nInc++, hInc++) ;
                    if (Found) return i;
                }
                return -1;
            }
        }
    }
}
