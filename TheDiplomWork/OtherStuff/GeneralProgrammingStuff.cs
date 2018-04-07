using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class GeneralProgrammingStuff //GPS
    {
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
            public double x, y, z;
            public Point3D(double _x, double _y, double _z)
            {
                x = _x; y = _y; z = _z;
            }
            public Point3D(Point3D input)
            {
                x = input.x; y = input.y; z = input.z;
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
        }
    }
    
}
