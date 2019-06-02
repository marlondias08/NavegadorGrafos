using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico01
{
    class Caminho
    {
        public int cidade1, cidade2, distancia;

        public override string ToString()
        {
            return string.Format($"{cidade1} -> {cidade2}");
        }
    }
}
