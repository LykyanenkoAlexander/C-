using System;
using System.Numerics;
using System.Collections.Generic;
using Лаба;
using System.Collections;




struct DataItem
    {
        public Vector2 Vect2 { get; set; }
        public Complex Compl { get; set; }

        public DataItem(float x, float y)
        {
            Vect2 = new Vector2(x, y);
            Compl = new Complex(x, y);
        }

        public override string ToString()
        { return Vect2 + " , " + Compl; }

        public string ToString(string format)
        {
            string res = Vect2.ToString();
            res = res + " : " + Compl.ToString(format) + Math.Sqrt(Compl.Real*Compl.Real + Compl.Imaginary*Compl.Imaginary).ToString(format);
            return res;
        }

    }

    struct Grid1D
    {
        public float Step { get; set; }
        public int Node { get; set; }

        public Grid1D(float a, int b)
        {
            Step = a;
            Node = b;
        }

        public float GetOXCoord(int ox_coord)
        {
            
            return ox_coord*Step;
        }

        public float GetOYCoord(int oy_coord)
        {

            return oy_coord*Step;
        }

    public override string ToString()
        { return "Step = " + Step + ", " + "Node = " + Node; }

        public string ToString(string format)
        {
            string res;
            res = "Step = " +  Step.ToString(format) + " ,Node = " + Node.ToString(format);
            return res;
        }

}

    abstract class V2Data : IEnumerable<DataItem>
{
        public string Indef { get; set; }
        public double Freq { get; set; }

        public V2Data(string a, double b)
        {
            Indef = a;
            Freq = b;
        }

        public abstract Complex[] NearAverage(float eps);

        public abstract string ToLongString();

        public override string ToString()
        { return "V2Data: " + Indef + "," + Freq; }

        public abstract string ToLongString(string format);

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

}

   

    class Res
    {
        static void Main()
        {
            Grid1D G_1 = new Grid1D(3, 3);
            Grid1D G_2 = new Grid1D(5, 3);

            V2DataOnGrid V2_1 = new V2DataOnGrid("new_1", 5, G_1, G_2);
            V2_1.InitRandom(5, 10);
            //V2_1.NearAverage(0.9f);

            Console.WriteLine('\n' + V2_1.ToLongString());

            V2DataCollection V2C = (V2DataOnGrid)V2_1;
            Console.WriteLine('\n' + V2C.ToLongString());
            //V2C.NearAverage(0.9f);
        
            V2MainCollection VM_1 = new V2MainCollection();
            VM_1.AddDefaults();
            VM_1.ToString();

            Vector2 vect = new Vector2((float)101010, (float)111);

            foreach (var item in VM_1)
            {
                item.NearAverage(0.5f);
            }
        

            //Console.WriteLine(V2C.ToLongString());
            //Console.WriteLine(V2_1.NearAverage(0.5f));
            //Console.WriteLine(V2_1.ToString());
            //Console.WriteLine(V2_1.ToLongString());

            V2DataCollection V2C_1 = new V2DataCollection("newC_1", 6);
            V2DataCollection V2C_2 = new V2DataCollection("newC_2", 7);
            //V2C_1.InitRandom(10,10,10,-20,30);
            //Console.WriteLine(V2C_1.ToLongString());
            //V2C_1.NearAverage(0.9f);
            //Console.WriteLine(V2C_1.ToString());
            //Console.WriteLine(V2C_1.ToLongString());

            VM_1.Add(V2_1);
            VM_1.Add(V2C_1);
            VM_1.Add(V2C_2);
            //Console.WriteLine(VM_1.Count);
            VM_1.Remove("new_1", 5);
        //Console.WriteLine(VM_1.Count);
        //VM_1.AddDefaults();
        //Console.WriteLine(VM_1.Count);

        
        V2DataCollection Lab_2_Data_Coll = new V2DataCollection("C:/Users/mrlyk/Desktop/Lab2/HomeTask/Лабораторная 2/Лаба/data.txt");
        Console.WriteLine(Lab_2_Data_Coll.ToLongString());

        V2MainCollection Lab_2_Main_Coll = new V2MainCollection();
        Lab_2_Main_Coll.AddDefaults();
        Console.WriteLine(Lab_2_Main_Coll.Mid_Value);
        Console.WriteLine(Lab_2_Main_Coll.Max_Far_Away);
        Console.WriteLine(Lab_2_Main_Coll.More_then_one);

    }
   }
