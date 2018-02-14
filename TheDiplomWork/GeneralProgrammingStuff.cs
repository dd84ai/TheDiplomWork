using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class GeneralProgrammingStuff
    {
        /// <summary>
        /// I'm too lazy to write down this function for every thing I need
        /// to initialize something in three dimensions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subject"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public static void TripleDimIniter<T>(ref T[][][] subject, int a, int b, int c)
        {
            subject = new T[a][][];
            for (int i = 0; i < a; i++)
            {
                subject[i] = new T[b][];
                for (int j = 0; j < b; j++)
                    subject[i][j] = new T[c];
            }
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
