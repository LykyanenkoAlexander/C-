using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Лаба
{
    class V2DataCollection : V2Data
    {
        public List<DataItem> Data { get; set; }

        public V2DataCollection(string a, double b) : base(a, b)
        {
            Data = new List<DataItem>();
        }

        public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
        {
            var rnd = new Random();
            for (int i = 0; i < nItems; i++)
            {
                var X = rnd.NextDouble() * xmax;
                var Y = rnd.NextDouble() * ymax;

                var X_f = (float)X;
                var Y_f = (float)Y;

                var next_Re = rnd.NextDouble();
                var next_Im = rnd.NextDouble();

                DataItem D = new DataItem();

                D.Vect2 = new Vector2(X_f, Y_f);
                D.Compl = new Complex(minValue + (next_Re * (maxValue - minValue)),
                                      minValue + (next_Im * (maxValue - minValue)));

                Data.Add(D);
            }
        }

        public override Complex[] NearAverage(float eps)
        {
            Complex mid_value = 0;
            int ind = 0;

            foreach (var item in Data)
            {
                mid_value += item.Compl;
            }

            mid_value = mid_value.Real / Data.Count;

            foreach (var item in Data)
            {
                if (Math.Abs(mid_value.Real - item.Compl.Real) <= (double)eps)
                {
                    ind++;
                }
            }

            Complex[] NearAverage = new Complex[ind];
            ind = 0;

            foreach (var item in Data)
            {
                if (Math.Abs(mid_value.Real - item.Compl.Real) <= (double)eps)
                {
                    NearAverage[ind] = item.Compl;
                    Console.WriteLine(item.Compl);
                    ind++;
                }
            }
            return NearAverage;
        }

        public override string ToString()
        {
            string res = "Base class values:" + '\n' +
                         "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                         "List size = " + Data.Count + '\n';
            return res;
        }

        public override string ToLongString()
        {
            string long_res = "Base class values:" + '\n' +
                         "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                         "List size = " + Data.Count;

            foreach (var item in Data)
            {
                long_res = long_res + '\n' + item.Vect2 + " = " + item.Compl;
            }

            return long_res;
        }
    }

}
