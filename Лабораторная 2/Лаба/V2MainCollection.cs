using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Лаба
{

    //class V2MainCollection : IEnumerable<V2Data>
    class V2MainCollection
    {
        private List<V2Data> Main_Data;


        public interface IEnumerable
        {
            IEnumerator<V2Data> GetEnumerator();
        }

        public IEnumerator<V2Data> GetEnumerator()
        {
            return Main_Data.GetEnumerator();
        }

        /*public IEnumerator<V2Data> IEnumerable<V2Data>.GetEnumerator()
        {
            return ((IEnumerable<V2Data>)Main_Data).GetEnumerator();
        }*/

        //////
        /* public override IEnumerator<DataItem> GetEnumerator()
        {
            return new DataEnumerator(Data);
        }*////


        public double Mid_Value
        {
            get
            {
                var all = Main_Data.SelectMany(x => x);
                double result = (from x in all select x.Compl.Magnitude).Average();
               
                return result;
            }                      
        }

        public IEnumerable<DataItem> Max_Far_Away
        {
            get
            {
                /*IEnumerable<V2DataCollection> f1 = 
                    from V2Data x in Main_Data where
                    x.Indef == "b" select (V2DataCollection)x;

                IEnumerable<V2DataOnGrid> f2 = 
                    from V2Data x in Main_Data where
                    x.Indef == "a" select (V2DataOnGrid)x;

                double m_v = Mid_Value;

                double max_f1 = f1.SelectMany(x => x).Max(v => v.Compl.Magnitude);
                double max_f2 = f2.SelectMany(x => x).Min(v => v.Compl.Magnitude);
                double min_f1 = f1.SelectMany(x => x).Max(v => v.Compl.Magnitude);
                double min_f2 = f2.SelectMany(x => x).Min(v => v.Compl.Magnitude);

                var total_f1 = (Math.Abs(m_v - max_f1) > Math.Abs(m_v - min_f1)) || ((Math.Abs(m_v - max_f1) < Math.Abs(m_v - min_f2))) ?
                            ((Math.Abs(m_v - max_f1) > Math.Abs(m_v - min_f1)) ? max_f1 : min_f1) : min_f1;

                var total_f2 = (Math.Abs(m_v - max_f2) > Math.Abs(m_v - min_f2)) || ((Math.Abs(m_v - max_f2) < Math.Abs(m_v - min_f2))) ?
                            ((Math.Abs(m_v - max_f2) > Math.Abs(m_v - min_f2)) ? max_f2 : min_f2) : min_f2;


               // var res_f1 = f1.TakeWhile(x => (Math.Abs(m_v - x.Compl.Magnitude) == Math.Abs(total_f1 - m_v)));

                //var res_f2 = f2.TakeWhile(x => (Math.Abs(m_v - x.Compl.Magnitude) == Math.Abs(total_f2 - m_v)));
                */
                var united = Main_Data.SelectMany(x => x); 
               
                double m_v = Mid_Value;

                double max = united.Max(v => v.Compl.Magnitude);
                double min = united.Min(v => v.Compl.Magnitude);
                
                var total =  (Math.Abs(m_v - max) > Math.Abs(m_v - min)) ||((Math.Abs(m_v - max) < Math.Abs(m_v - min))) ?
                            ((Math.Abs(m_v - max) > Math.Abs(m_v - min)) ? max : min ):min;
                                       
                var res = united.TakeWhile(x => (Math.Abs(m_v - x.Compl.Magnitude) == Math.Abs(total - m_v)));

                //var r = (from x in united where ((Math.Abs(m_v - x.Compl.Magnitude) == Math.Abs(total - m_v))) select x); 

                return res;
            }
        }

        public IEnumerable<Vector2> More_then_one
        {
            get
            {
                var all = Main_Data.SelectMany(x => x);             
                var result = (from x in all where (all.Where(x => x.Equals(x.Vect2)).Count() > 1) select x.Vect2);
                return result;
            }
        }


        public V2MainCollection()
        {
            Main_Data = new List<V2Data>();
        }

        public int Count
        {
            get
            {
                return (Main_Data.Count);
            }
        }

        public void Add(V2Data item)
        {
            Main_Data.Add(item);
        }

        public bool Remove(string id, double w)
        {
            bool res = false;

            Console.WriteLine('\n' + "Before Removing:" + '\n');
            foreach (var item in Main_Data.ToArray())
            {
                Console.WriteLine(item);
            }

            foreach (var item in Main_Data.ToArray())
            {
                if ((item.Indef == id) && (item.Freq == w))
                {
                    res = true;
                    Main_Data.Remove(item);
                }
            }

            Console.WriteLine('\n' + "After Removing:" + '\n');
            foreach (var item in Main_Data.ToArray())
            {
                Console.WriteLine(item);
            }

            return res;
        }

        public void AddDefaults()
        {
            Random rnd = new Random();
            int n_new = rnd.Next(3, 5);

            for (int i = 0; i < n_new; i++)
            {
                Grid1D d1 = new Grid1D(3, 4);
                V2DataOnGrid New_Grid = new V2DataOnGrid("a", 4, d1, d1);
                V2DataCollection New_Coll = new V2DataCollection("b", 5);
                New_Grid.InitRandom(12, 20);
                New_Coll.InitRandom(5, 1, 10, 12, 20);
                Main_Data.Add(New_Grid);
                Main_Data.Add(New_Coll);
            }
        }

        public override string ToString()
        {
            foreach (var item in Main_Data)
            {
                Console.WriteLine(item.ToString());
            }
            return null;
        }

        public string ToLongString(string format)
        {
            foreach (var item in Main_Data)
            {
                Console.WriteLine(item.ToLongString(format));
            }
            return null;
        }

    }

}
