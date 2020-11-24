using System;
using System.Numerics;
using System.Collections.Generic;
using Лаба;


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

        public override string ToString()
        { return "Step = " + Step + ", " + "Node = " + Node; }

    }

    abstract class V2Data
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

        }
    }
