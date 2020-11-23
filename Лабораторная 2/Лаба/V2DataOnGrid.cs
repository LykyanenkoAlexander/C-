using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Лаба
{
    class V2DataOnGrid : V2Data
    {
        public Grid1D[] Elem { get; set; }
        public Complex[,] Node_Val { get; set; }

        public V2DataOnGrid(string a, double b, Grid1D x, Grid1D y) : base(a, b)
        {
            Elem = new Grid1D[2];
            Elem[0] = x;
            Elem[1] = y;
        }

        public void InitRandom(double minValue, double maxValue)
        {
            Random rnd = new Random();
            Node_Val = new Complex[Elem[0].Node, Elem[1].Node];

            for (int i = 0; i < Elem[0].Node; i++)
            {
                for (int j = 0; j < Elem[1].Node; j++)
                {

                    var next_Re = rnd.NextDouble();
                    var next_Im = rnd.NextDouble();

                    Node_Val[i, j] = new Complex(minValue + (next_Re * (maxValue - minValue)),
                                                 minValue + (next_Im * (maxValue - minValue)));
                }
            }
        }

        public static implicit operator V2DataCollection(V2DataOnGrid a)
        {
            V2DataCollection V2 = new V2DataCollection(a.Indef, a.Freq);

            for (int i = 0; i < a.Node_Val.GetLength(0); i++)
            {
                for (int j = 0; j < a.Node_Val.GetLength(1); j++)
                {
                    DataItem DI = new DataItem();
                    DI.Vect2 = new Vector2(a.Elem[0].Step * i,
                                           a.Elem[1].Step * j);

                    DI.Compl = new Complex(a.Node_Val[i, j].Real,
                                           a.Node_Val[i, j].Imaginary);

                    V2.Data.Add(DI);
                }
            }

            return V2;
        }

        public override Complex[] NearAverage(float eps)
        {
            Complex mid_value = 0;
            int ind = 0;

            for (int i = 0; i < Elem[0].Node; i++)
            {
                for (int j = 0; j < Elem[1].Node; j++)
                {
                    mid_value += Node_Val[i, j];
                }
            }

            mid_value = mid_value.Real / (Elem[0].Node * Elem[1].Node);

            for (int i = 0; i < Elem[0].Node; i++)
            {
                for (int j = 0; j < Elem[1].Node; j++)
                {
                    if (Math.Abs(mid_value.Real - Node_Val[i, j].Real) <= (double)eps)
                    {
                        ind++;
                    }
                }
            }

            Complex[] NearAverage = new Complex[ind];
            ind = 0;

            for (int i = 0; i < Elem[0].Node; i++)
            {
                for (int j = 0; j < Elem[1].Node; j++)
                {
                    if (Math.Abs(mid_value.Real - Node_Val[i, j].Real) <= (double)eps)
                    {
                        NearAverage[ind] = Node_Val[i, j];
                        Console.WriteLine(NearAverage[ind]);
                        ind++;
                    }
                }
            }
            return NearAverage;
        }

        public override string ToString()
        {
            string res = "Base class values:" + '\n' +
                    "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                    "Grid data:" + '\n' + "Ox: " + '\n' + "Number of nodes = " + Elem[0].Node +
                    ", Number of steps = " + Elem[0].Step + '\n' + '\n' +
                    "Oy: " + '\n' + "Number of nodes = " + Elem[1].Node + ", Number of steps = " +
                    Elem[1].Step + '\n';

            return res;
        }

        public override string ToLongString()
        {
            string long_res = "Base class values:" + '\n' +
                    "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                    "Grid data:" + '\n' + "Ox: " + "  " + "Number of nodes = " + Elem[0].Node +
                    ", Number of steps = " + Elem[0].Step + '\n' +
                    "Oy: " + "  " + "Number of nodes = " + Elem[1].Node + ", Number of steps = " +
                    Elem[1].Step + '\n';

            for (int i = 0; i < Elem[0].Node; i++)
            {
                for (int j = 0; j < Elem[1].Node; j++)
                {
                    long_res = long_res + '\n' + "[" + i * Elem[0].Step + ", " + j * Elem[1].Step +
                               "] " + " = " + Node_Val[i, j];
                }
            }

            long_res = long_res + '\n';
            return long_res;
        }


    }
}
