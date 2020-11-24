using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Лаба
{
    
    class V2DataCollection : V2Data, IEnumerable<DataItem>
    {
        public List<DataItem> Data { get; set; }

        public V2DataCollection(string a, double b) : base(a, b)
        {
            Data = new List<DataItem>();
        }

        
        public V2DataCollection(string filename) : base("abc", 5)
        {

            FileStream f = null;
            List<DataItem> Data_file = new List<DataItem>();

            string vec_1; string vec_2; string compl_1; string compl_2;
            string f_data; bool open_1 = false; bool open_2 = false; bool c_1 = false; bool c_2 = false; bool real_neg = false;       

            try
            {
               
                if (!File.Exists(filename))
                {
                    throw new Exception("No such a filename\n");
                }
              
                f = new FileStream(filename, FileMode.Open);
                StreamReader file = new StreamReader(f);
                f_data = file.ReadLine();
              
                while (f_data != null)
                {
                    vec_1 = ""; vec_2= ""; compl_1 = ""; compl_2 = "";
                    open_1 = false; open_2 = false; c_1 = false;c_2 = false;real_neg = false;
                    for (int k = 0; k < f_data.Length; k++)
                    {
                        char c = f_data[k];
                        if (c == '(') { open_1 = true; }
                        else if ((c == ' ') || (c == '\n') || (c == '\t')) { }
                        else if (c == ')') { open_2 = false; }
                        else if (c == ',') { open_2 = true; open_1 = false; }
                        else if (c == '=') { c_1 = true; }
                        else if (c == '-' & c_1 & real_neg) { c_1 = false; c_2 = true; compl_2 += c; }
                        else if (c == '-' & c_1 & !real_neg & compl_1 == "") { compl_1 += c; real_neg = true; }
                        else if (c == '-' & c_1 & !real_neg & compl_1 != "") { c_1 = false; c_2 = true; compl_2 += c; }
                        else if (c == '+' & c_1) { c_1 = false; c_2 = true; }
                        else if (c == 'i') { }
                        else if (Char.IsDigit(c) & open_1) { vec_1 += c; }
                        else if (Char.IsDigit(c) & open_2) { vec_2 += c; }
                        else if (Char.IsDigit(c) & c_1) { compl_1 += c; }
                        else if (Char.IsDigit(c) & c_2) { compl_2 += c; }
                        else if (Char.IsLetter(c) & (open_1 || open_2)) { throw new Exception("Wrong vector input: Can't read letter.\n"); }
                        else if (Char.IsLetter(c) & (c_1 || c_2)) { throw new Exception("Wrong complex value input: Can't read letter\n"); }
                        else { throw new Exception("Wrong input: Can't read such symbol\n"); }
                    }
                  
                    DataItem Data_Coll = new DataItem();
                    Data_Coll.Vect2 = new Vector2(float.Parse(vec_1), float.Parse(vec_2));
                    Data_Coll.Compl = new Complex(Convert.ToDouble(compl_1), Convert.ToDouble(compl_2));
                    Data_file.Add(Data_Coll);
                    f_data = file.ReadLine();            
                }

                Data = new List<DataItem>(); 
                Data = Data_file;

            }

            catch (Exception e)
            {
                System.Console.WriteLine("Parse error");
                System.Console.WriteLine(e.Message);
            }

            finally
            {
                if (f != null)
                {
                    f.Close();
                }
            }
        }


        public override IEnumerator<DataItem> GetEnumerator()
        {
            return new DataEnumerator(Data);
        }

        private class DataEnumerator : IEnumerator<DataItem>
        {         
            private List<DataItem> Data_new;
            public int position = -1;
            object IEnumerator.Current => Current;
            public DataItem Current
            {
                get
                {
                    DataItem D = new DataItem();
                    D = Data_new.ElementAt(position);
                    return D;
                }
            }
            public DataEnumerator(List<DataItem> Data)
            {
                Data_new = Data;
            }
            public bool MoveNext()
            {
                position += 1;
                return position < Data_new.Count;
            }
            public void Reset()
            {
                position = -1;
            }
            public void Dispose()
            {
                Data_new = null;
            }
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
            string res = "Type is V2DataCollection, " + "Base class values:" + '\n' +
                         "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                         "List size = " + Data.Count + '\n';
            return res;
        }

        public override string ToLongString()
        {
            string long_res = "Type is V2DataCollection, " + "Base class values:" + '\n' +
                         "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                         "List size = " + Data.Count;

            foreach (var item in Data)
            {
                long_res = long_res + '\n' + item.Vect2 + " = " + item.Compl;
            }
            
            return long_res;
        }

        public override string ToLongString(string format)
        {
            /*string long_res = "Type is V2DataCollection, " + "Base class values:" + '\n' +
                         "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                         "List size = " + Data.Count;
            */
            string long_res = "";
            foreach (var item in Data)
            {
                long_res = long_res + '\n' + item.Vect2 + " = " + item.Compl.ToString(format);
            }

            return "s";
        }

    }

}
