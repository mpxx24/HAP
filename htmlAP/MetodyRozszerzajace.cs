using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlAP
{
    static class MetodyRozszerzajace
    {
        public static void CzyZawiera(this IEnumerable<Rekord> kontener, string przyklad)
        {
            foreach (var rekord in kontener)
            {
                if (rekord.nazwa.ToLower().Contains(przyklad.ToLower()))
                {
                    Console.WriteLine(rekord);
                }
            }
        }
    }
}
