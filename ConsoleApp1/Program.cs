using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    //abstract class Shape
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public Shape()
    //    {

    //    }

    //    public abstract Shape Clone();
    //}


    //class Rectangle : Shape
    //{
    //    public int Width { get; set; }
    //    public int Heigth { get; set; }

    //    public Rectangle(int x,int y, int width,int heigth)
    //    {
    //        X = x;
    //        Y = y;
    //        Width = width;
    //        Heigth = heigth;
    //    }

    //    public Rectangle(Rectangle re)
    //    {
    //        X = re.X;
    //        Y = re.Y;
    //        Width = re.Width;
    //        Heigth = re.Heigth;
    //    }
    //    public Rectangle()
    //    {

    //    }

    //    public override Shape Clone()
    //    {
    //        return new Rectangle(this);
    //    }
    //}


    //class Circle : Shape
    //{
    //    public int Radius { get; set; }
    //    public Circle(Circle c)
    //    {
    //        this.X = c.X;
    //        this.Y = c.Y;   
            
    //        this.Radius = c.Radius;
    //    }

    //    public Circle(int x, int y, int radius)
    //    {
    //        X = x;
    //        Y = y;
    //        Radius = radius;
    //    }
    //    public override Shape Clone()
    //    {
    //        return new Circle(this);
    //    }
    //}




    interface IAdapter
    {
        void Add();
        void Update();
        string Get(int id);
    }

    class SqlAdapter : IAdapter
    {
        private SqlDb _db;

        public SqlAdapter(SqlDb db)
        {
            _db = db;
        }

        public void Add()
        {
            _db.Add();
        }

        public string Get(int id)
        {
            return _db.Get(id);
        }

        public void Update()
        {
            _db.Update();
        }
    }


    class MangoAdapter : IAdapter
    {
        private MangoDb _db;

        public MangoAdapter(MangoDb db)
        {
            _db = db;
        }

        public void Add()
        {
            _db.Insert();
        }

        public string Get(int id)
        {
            return _db.getData(id);
        }

        public void Update()
        {
            _db.Modify();

        }
    }

    class SqlDb
    {
        public void Add()
        {
            Console.WriteLine("Add data to SQL");
        }

        public void Update()
        {
            Console.WriteLine("Update in Sql");
        }

        public string Get(int id)
        {
            return "Data from Sql Db";
        }

    }


    class MangoDb
    {
        public void Insert()
        {
            Console.WriteLine("Data Inserted to Mango Db");
        }

        public void Modify()
        {
            Console.WriteLine("Data Modified in mango Db");
        }

        public string getData(int id)
        {
            return "Data from Mango Db";
        }


    }


    class Converter
    {
        private readonly IAdapter _adapter;

        public Converter(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public void Add()
        {
            _adapter.Add();
        }

        public void Update()
        {
            _adapter.Update();
        }

        public string Get(int id)
        {
            return _adapter.Get(id);
        }

    }


    class Application
    {
        private readonly Converter _converter;
        public Application(IAdapter adapter)
        {
            _converter = new Converter(adapter);
        }
        public void Start()
        {
            _converter.Add();
            _converter.Update();
            Console.WriteLine(_converter.Get(10));
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {


            //Rectangle rect = new Rectangle(10, 20, 100, 300);
            //var rect_copy = rect.Clone();



            while (true)
            {
                Console.WriteLine("For Mango Db [1]");
                Console.WriteLine("For SQL Db [2]");
                Console.WriteLine();
                int select = int.Parse(Console.ReadLine());
                IAdapter adapter;
                if(select == 1)
                {
                    MangoDb mangoDb = new MangoDb();
                    adapter = new MangoAdapter(mangoDb);
                }
                else
                {
                    SqlDb sqldb = new SqlDb();
                    adapter = new SqlAdapter(sqldb);
                }
                Application application = new Application(adapter);
                application.Start();
            }



        }
    }
}
