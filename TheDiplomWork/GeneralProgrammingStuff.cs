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
    }
    
}
