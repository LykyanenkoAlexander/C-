using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Лаба
{
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
                V2DataCollection New_Coll = new V2DataCollection("a", 5);
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
    }

}
