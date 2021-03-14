using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Deikstra_09_963
{
    class Loader
    {
        public static double[,] LoadData(string filename)
        {
            StreamReader inp = new StreamReader(new FileStream(filename, FileMode.Open));
            double[,] data = null;
            var i = 0;
            while (!inp.EndOfStream)
            {
                var str_d = inp.ReadLine();
                var arr_d = str_d.Split(new char[] {';'});
                var vc = arr_d.Length;
                if (data == null) data = new double[vc, vc];
                for (int j = 0; j < vc; j++)
                {
                    double.TryParse(arr_d[j], out data[i, j]);
                }

                i++;
            }
            inp.Close();
            return data;
        }
    }
}
