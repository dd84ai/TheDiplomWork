using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Time
    {
        public class localtime
        {
            double Time = 0;
            public double Time_Speed = 1.0;
            public float Sun_Time_Decreaser = 100000;
            public double GetTotalRadianTime()
            {
                return Time / Sun_Time_Decreaser;
            }
            
            public void TimeIncrease(double inp)
            {
                Time += inp;
            }
            public double TimeViaRadianAngle()
            {
                return ((GetTotalRadianTime() + Math.PI) % (Math.PI * 2)) / Math.PI;
            }
            public string GetDayTime()
            {
                double value = TimeViaRadianAngle();
                int hours = (int)(value * 12);
                
                return (hours).ToString("D2") + ":" + ((int)(((12 * value) - (double)hours)*60)).ToString("D2");
            }
        }
        public static localtime time = new localtime();
    }
}
