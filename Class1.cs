using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    class CW
    {
        public int dec { get; set; }
        public int weight { get; set; }
        public string bin { get; set; }
        public CW(int _dec, int _weight, string _bin)
        {
            dec = _dec;
            weight = _weight;
            bin = _bin;
        }
    }
}
