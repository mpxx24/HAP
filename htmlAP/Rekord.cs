using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlAP
{
    class Rekord
    {
        public string nazwa { get; set; }
        public string seedy { get; set; }
        public string peery { get; set; }
        public string kategoria { get; set; }

        public override string ToString()
        {
            return kategoria + " " + nazwa + " " + seedy + " " + peery;
        }
    }
}
