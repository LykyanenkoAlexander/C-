using System;
using System.Numerics;
using System.Collections.Generic;

struct DataItem
{
        public Vector2 Vect2 { get; set; }
        public Complex Compl { get; set; }

        public DataItem(float x, float y) 
        {
            Vect2  = new Vector2(x,y);      
            Compl = new Complex(x,y);           
        }

        public override string ToString()
        { return  Vect2 + " , " + Compl ; }

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
        { return  "Step = " + Step + ", " + "Node = " + Node; }

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

class V2DataOnGrid : V2Data
{
    public Grid1D[] Elem{ get; set;}
    public Complex[,] Node_Val { get; set; }

    public V2DataOnGrid(string a, double b, Grid1D x, Grid1D y):base(a,b)
    {
        Elem = new Grid1D[2];
        Elem[0] = x;
        Elem[1] = y;
    }

    public void InitRandom(double minValue, double maxValue)
    {
        Random rnd = new Random();
        Node_Val = new Complex[Elem[0].Node,  Elem[1].Node ];

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
            for (int j = 0; j < a.Node_Val.GetLength(0); j++)
            {
                DataItem DI = new DataItem();

                DI.Vect2 = new Vector2((float)a.Node_Val[i, j].Real,
                                      (float)a.Node_Val[i, j].Imaginary);

                DI.Compl = new Complex((float)a.Node_Val[i, j].Real,
                                      (float)a.Node_Val[i, j].Imaginary);
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
                    //Console.WriteLine(NearAverage[ind]);
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
                Elem[1].Step;

        for (int i = 0; i < Elem[0].Node; i++)
        {
            for (int j = 0; j < Elem[1].Node; j++)
            {            
                long_res = long_res + '\n' + "[" + i * Elem[0].Step + ", " + j * Elem[1].Step +
                           "] " + " = " + Node_Val[i, j];
            }
        }
        
        long_res  =long_res + '\n';
        return long_res;
    }


}

class V2DataCollection : V2Data
{
    public List<DataItem> Data{ get; set; }

    public V2DataCollection(string a, double b):base(a,b)
    {
        Data = new List<DataItem>();
    }

    public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
    {
        var rnd = new Random();
        for (int i = 0; i < nItems; i++)
        {
            var X = rnd.NextDouble()*xmax; 
            var Y = rnd.NextDouble()*ymax;

            var X_f = (float)X; 
            var Y_f = (float)Y;

            var next_Re = rnd.NextDouble();
            var next_Im = rnd.NextDouble();

            DataItem D = new DataItem();

            D.Vect2 = new Vector2(X_f,Y_f);
            D.Compl = new Complex(minValue + (next_Re * (maxValue - minValue)),
                                  minValue + (next_Im * (maxValue - minValue)));

            Data.Add(D);
        }

    }


    public override Complex[] NearAverage(float eps)
    {
        Complex mid_value = 0;
        int ind = -1;

        foreach (var item in Data)
        {
            mid_value += item.Compl;
        }

        mid_value = mid_value.Real/Data.Count;
        

        foreach (var item in Data)
        {
            if (Math.Abs(mid_value.Real - item.Compl.Real) <= (double)eps)
            {
                ind++;
            }
        }

        Complex[] NearAverage = new Complex[ind+1];
        ind = 0;

        foreach (var item in Data)
        {
            if (Math.Abs(mid_value.Real - item.Compl.Real) <= (double)eps)
            {
                NearAverage[ind] = item.Compl;
                ind++;
            }
        }
        return NearAverage;    
    }

    public override string ToString()
    {
        string res = "Base class values:" + '\n' +
                     "Indeficator = " + Indef + '\n' + "Frequency = " + Freq + '\n' +
                     "List size = " + Data.Count;
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
        
           foreach (var item in Main_Data.ToArray())
           {
               if ((item.Indef == id) && (item.Freq == w))
               {
                   res = true;
                   Main_Data.Remove(item);
               }
           }
        
        return res;
    }

    public void AddDefaults()
    {
        Random rnd = new Random();
        //int n_new = rnd.Next(3,Int32.MaxValue);
        int n_new = rnd.Next(3, 5);

        for (int i = 0; i < n_new; i++)
        {
            Grid1D d1 = new Grid1D(3, 4);
            V2DataOnGrid New_Grid = new V2DataOnGrid("a", 4, d1, d1);
            V2DataCollection New_Coll = new V2DataCollection("a",5);
            New_Grid.InitRandom(12,20);
            New_Coll.InitRandom(5,1,10,12,20);
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




class Res
{
    static void Main()
    {
        Grid1D G_1 = new Grid1D(3, 3);
        Grid1D G_2 = new Grid1D(5, 3);

        V2DataOnGrid V2_1 = new V2DataOnGrid("new_1", 5, G_1, G_2);
        V2_1.InitRandom(5,10);
        Console.WriteLine(V2_1.ToLongString());

        V2DataCollection V2C = (V2DataOnGrid)V2_1;

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

        //V2DataCollection V2C_1 = new V2DataCollection("newC_1", 6);
        //V2DataCollection V2C_2 = new V2DataCollection("newC_2", 7);
        //V2C_1.InitRandom(10,10,10,-20,30);
        //Console.WriteLine(V2C_1.ToLongString());
        //V2C_1.NearAverage(0.9f);
        //Console.WriteLine(V2C_1.ToString());
        //Console.WriteLine(V2C_1.ToLongString());

        //VM_1.Add(V2_1);
        //VM_1.Add(V2C_1);
        //VM_1.Add(V2C_2);
        //Console.WriteLine(VM_1.Count);
        //VM_1.Remove("new_1", 5);
        //Console.WriteLine(VM_1.Count);
        //VM_1.AddDefaults();
        //Console.WriteLine(VM_1.Count);

    }
}