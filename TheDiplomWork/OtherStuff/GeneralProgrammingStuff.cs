using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public class GeneralProgrammingStuff //GPS
    {
        public static Random Rand_GPS = new Random();
        public static List<List<List<T>>> TripleListIniter<T>(int a, int b, int c)
        {
            List<List<List<T>>> subject;

            subject = new List<List<List<T>>>();
            for (int i = 0; i < a; i++)
            {
                subject.Add(new List<List<T>>());
                for (int j = 0; j < b; j++)
                {
                    subject[i].Add(new List<T>(new T[c]));
                }
            }
            return subject;
        }
        public static List<List<List<CubicalMemory.Cube>>> TripleCubeIniter(int a, int b, int c, GeneralProgrammingStuff.Point2Int xz)
        {
            List<List<List<CubicalMemory.Cube>>> subject;

            subject = new List<List<List<CubicalMemory.Cube>>>();
            for (int i = 0; i < a; i++)
            {
                subject.Add(new List<List<CubicalMemory.Cube>>());
                for (int j = 0; j < b; j++)
                {
                    subject[i].Add(new List<CubicalMemory.Cube>());

                    for (int k = 0; k < c; k++)
                    {
                        subject[i][j].Add(new CubicalMemory.Cube(i, j, k, xz));
                    }
                }
            }
            return subject;
        }
        public static List<List<T>> DoubleListIniter<T>(int a, int b)
        {
            List<List<T>> subject;

            subject = new List<List<T>>();
            for (int i = 0; i < a; i++)
            {
                subject.Add(new List<T>(new T[b]));
            }
            return subject;
        }
        public static List<List<CubicalMemory.Chunk>> DoubleChunkIniter(int a, int b)
        {
            List<List<CubicalMemory.Chunk>> subject;

            subject = new List<List<CubicalMemory.Chunk>>();
            for (int i = 0; i < a; i++)
            {
                subject.Add(new List<CubicalMemory.Chunk>());
                for (int j = 0; j < b; j++)
                {
                    subject[i].Add(new CubicalMemory.Chunk(i, j));
                }
            }
            return subject;
        }

        public static T[][][] TripleDimIniter<T>(int a, int b, int c)
        {
            T[][][] subject; //= new T subject,

            subject = new T[a][][];
            for (int i = 0; i < a; i++)
            {
                subject[i] = new T[b][];
                for (int j = 0; j < b; j++)
                    subject[i][j] = new T[c];
            }

            return subject;
        }
        public static T[][] DoubleDimIniter<T>(int a, int b)
        {
            T[][] subject;

            subject = new T[a][];
            for (int i = 0; i < a; i++)
            {
                subject[i] = new T[b];
            }
            return subject;
        }

        public class Point3D
        {
            public float x, y, z;
            public Point3D(float _x, float _y, float _z)
            {
                x = _x; y = _y; z = _z;
            }
            public Point3D(Point3D input)
            {
                x = input.x; y = input.y; z = input.z;
            }
            public override string ToString()
            {
                return "Point3D: " + x.ToString("G3") + " ; "
                    + y.ToString("G3") + " ; "
                + z.ToString("G3");
            }
            public static void CopyToFrom(ref Point3D Output, Point3D _Input)
            {
                Output.x = _Input.x;
                Output.y = _Input.y;
                Output.z = _Input.z;
            }
            string Path = Interface.ProjectPath + "\\" + "Save" + "\\";
            public void Save(string Name)
            {
                try
                {
                    using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Path + Name + ".dat"))
                    {
                        file.WriteLine(x.ToString("E15"));
                        file.WriteLine(y.ToString("E15"));
                        file.WriteLine(z.ToString("E15"));
                    }
                }
                catch (Exception ThrowItAway)
                {
                    Console.WriteLine($"{ThrowItAway.Message}");
                }
            }
            public static string ReplaceDecimalSeparator(string inp)
            {
                inp = inp.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                inp = inp.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                return inp;
            }
            public void TryLoad(string Name)
            {
                try
                {
                    using (System.IO.StreamReader file =
                new System.IO.StreamReader(Path + Name + ".dat"))
                    {
                        x = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                        y = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                        z = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                    }
                }
                catch (Exception ThrowItAway)
                {
                    Console.WriteLine($"{ThrowItAway.Message}");
                }
            }
        }
        public class Point2Int
        {
            public int x, z;
            public Point2Int(int _x, int _z)
            {
                x = _x; z = _z;
            }
            public Point2Int(Point2Int input)
            {
                x = input.x; z = input.z;
            }
            public override string ToString()
            {
                return "Point2Int: " + x.ToString("G2") + " ; "
                + z.ToString("G2");
            }
            public static bool operator ==(Point2Int c1, Point2Int c2)
            {
                if (c1.x == c2.x && c1.z == c2.z) return true;
                else return false;
            }
            public static bool operator !=(Point2Int c1, Point2Int c2)
            {
                if (c1.x != c2.x || c1.z != c2.z) return true;
                else return false;
            }
            public override bool Equals(Object obj)
            {
                // Check for null values and compare run-time types.
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Point2Int p = (Point2Int)obj;
                return (x == p.x) && (z == p.z);
            }
            public override int GetHashCode()
            {
                return x ^ z;
            }
        }
        public class Point3Int
        {
            public int x, y, z;
            public Point3Int(int _x, int _y, int _z)
            {
                x = _x; y = _y; z = _z;
            }
            public Point3Int(Point3Int input)
            {
                x = input.x; y = input.y; z = input.z;
            }
            public override string ToString()
            {
                return "Point2Int: " + x.ToString("G2") + " ; "
                    + y.ToString("G2") + " ; "
                + z.ToString("G2");
            }
            public static void CopyToFrom(ref Point3Int Output, Point3Int _Input)
            {
                Output.x = _Input.x;
                Output.y = _Input.y;
                Output.z = _Input.z;
            }
            public static bool operator ==(Point3Int c1, Point3Int c2)
            {
                if (c1.x == c2.x && c1.z == c2.z && c1.y == c2.y) return true;
                else return false;
            }
            public static bool operator !=(Point3Int c1, Point3Int c2)
            {
                if (c1.x != c2.x || c1.z != c2.z || c1.y != c2.y) return true;
                else return false;
            }
            public override bool Equals(Object obj)
            {
                // Check for null values and compare run-time types.
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Point3Int p = (Point3Int)obj;
                return (x == p.x) && (z == p.z) && (y == p.y);
            }
            public override int GetHashCode()
            {
                return x ^ z ^ y;
            }
        }

        public static int ColorSwitch_Quantity = 13;
        public static System.Drawing.Color ColorSwitch(int choice)
        {
            switch (choice)
            {
                case 00: return System.Drawing.Color.BurlyWood;
                case 01: return System.Drawing.Color.Gold;
                case 02: return System.Drawing.Color.Green;
                case 03: return System.Drawing.Color.GreenYellow;
                case 04: return System.Drawing.Color.Brown;
                case 05: return System.Drawing.Color.Indigo;
                case 06: return System.Drawing.Color.Purple;
                case 07: return System.Drawing.Color.Orange;
                case 08: return System.Drawing.Color.Red;
                case 09: return System.Drawing.Color.Aqua;
                case 10: return System.Drawing.Color.Blue;
                case 11: return System.Drawing.Color.White;
                case 12: return System.Drawing.Color.Black;
                default: return System.Drawing.Color.Gray;
            }
        }
        public static float vec3_range(vec3 NormalizedToXYWorld)
        {
            return (float)Math.Sqrt(
                                          NormalizedToXYWorld.x * NormalizedToXYWorld.x
                                        + NormalizedToXYWorld.y * NormalizedToXYWorld.y
                                        + NormalizedToXYWorld.z * NormalizedToXYWorld.z);
        }
        public static void vec3_normalize(ref vec3 NormalizedToXYWorld, float _range)
        {
            float range = 1 / _range;
            NormalizedToXYWorld.x *= range; NormalizedToXYWorld.y *= range; NormalizedToXYWorld.z *= range;
        }
        public static float vec3_scalar(vec3 a, vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
    }

    public class vec3things
    {
        static string Path = Interface.ProjectPath + "\\" + "Save" + "\\";
        public static void Save(vec3 vec, string Name)
        {
            try
            {
                using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(Path + Name + ".dat"))
                {
                    file.WriteLine(vec.x.ToString("E15"));
                    file.WriteLine(vec.y.ToString("E15"));
                    file.WriteLine(vec.z.ToString("E15"));
                }
            }
            catch (Exception ThrowItAway)
            {
                Console.WriteLine($"{ThrowItAway.Message}");
            }
        }
        static string ReplaceDecimalSeparator(string inp)
        {
            inp = inp.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            inp = inp.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            return inp;
        }
        public static string ToString(vec3 inp)
        {
            string standard = "G2";
            return inp.x.ToString(standard) + " ; " + inp.y.ToString(standard) + " ; " + inp.z.ToString(standard);
        }
        public static string Length(vec3 inp)
        {
            string standard = "G2";
            return Math.Sqrt(inp.x* inp.x + inp.y* inp.y + inp.z* inp.z).ToString(standard);
        }
        public static bool TryLoad(ref vec3 vec, string Name)
        {
            try
            {
                using (System.IO.StreamReader file =
            new System.IO.StreamReader(Path + Name + ".dat"))
                {
                    vec.x = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                    vec.y = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                    vec.z = Convert.ToSingle(ReplaceDecimalSeparator(file.ReadLine()));
                }
                return true;
            }
            catch (Exception ThrowItAway)
            {
                Console.WriteLine($"{ThrowItAway.Message}");
                return false;
            }
        }
    }
}
