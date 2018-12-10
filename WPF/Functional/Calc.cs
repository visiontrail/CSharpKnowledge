using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class Calc
    {
        public string Add(string x, string y)
        {
            double temp_x;
            double temp_y;
            double temp_res = 0;
            string res = "";
            if(double.TryParse(x,out temp_x) && double.TryParse(y, out temp_y))
            {
                temp_res = temp_x + temp_y;
                res = temp_res.ToString();
            }
            else
            {
                res = "Error";
            }

            return res;
        }
    }
}
