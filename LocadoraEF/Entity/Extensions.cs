using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Extensions
{
    //Classe utilizada como container para guardar nossos métodos
    //de extensão (métodos que aparecem nos objetos da Microsoft)
    public static class Extensions
    {
        public static int ToInt(this string val)
        {
            int.TryParse(val, out int temp);
            return temp;
        }
        public static double ToDouble(this string val)
        {
            double.TryParse(val, out double temp);
            return temp;
        }

    }
}
