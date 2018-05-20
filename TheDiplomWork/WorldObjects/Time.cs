using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class Time
    {
        public class localtime
        {
            public double AverageRebuildingTime = 0;

            public double Time = 0;
            
            public double Time_Speed = 1.0;
            public float Sun_Time_Decreaser = 200000;
            public double GetTotalRadianTime()
            {
                return Time / Sun_Time_Decreaser;
            }
            
            public void TimeIncrease(double inp)
            {
                if (!StaticAccess.FMOS.table_Menu_main.Visible)
                {
                    Time += inp;
                    TimeLastIncreasement = inp;
                }
            }
            double TimeLastIncreasement = 0.1;
            public double Get_TimeLastIncreasement()
            {
                return TimeLastIncreasement / 1000;
            }

            public double TimeViaRadianAngle()
            {
                return ((GetTotalRadianTime() + Math.PI) % (Math.PI * 2)) / Math.PI;
            }
            double TotalReadlSeconds = 0;
            public double TimeWaitForFallingCubes = 0;
            public double GetGameTotalSeconds()
            {
                return Time/1000;
                //return TotalReadlSeconds;
            }
            public double GetTotalSeconds()
            {
                return TotalReadlSeconds;
            }
            public void SetTotalSeconds(double inp)
            {
                TotalReadlSeconds = inp;
            }
            public string GetDayTime()
            {
                double value = TimeViaRadianAngle();
                int hours = (int)(value * 12);
                
                return (hours).ToString("D2") + ":" + ((int)(((12 * value) - (double)hours)*60)).ToString("D2");
            }
            public void GetSkyColor(OpenGL gl)
            {
                double TimeFrom0to2 = TimeViaRadianAngle();

                double r = 0;
                double g = 0;
                double b = 0;
                double TempTime = 0;

                if (TimeFrom0to2 > 0.5 && TimeFrom0to2 < 1.5)
                {
                    TempTime = Math.Abs(0.5f - Math.Abs(TimeFrom0to2 - 1.0f)) * 2.0f;
                    r += (TempTime) * ((double)126 / 255);
                    g += (TempTime) * ((double)192 / 255);
                    b += (TempTime) * ((double)238 / 255);
                }
                else
                {
                    if (TimeFrom0to2 >= 1.5) TempTime = (TimeFrom0to2 - 1.5) * 2;
                    else TempTime = (0.5 - TimeFrom0to2) * 2;

                    r += (TempTime) * ((double)0 / 255);
                    g += (TempTime) * ((double)24 / 255);
                    b += (TempTime) * ((double)72 / 255);
                }

                double DawnAndSunset = 0;
                if (TimeFrom0to2 > 0.25 && TimeFrom0to2 < 0.75)
                {
                    DawnAndSunset = (0.25 - Math.Abs(TimeFrom0to2 - 0.5)) * 2;

                    r += (DawnAndSunset) * ((double)231 / 255);
                    g += (DawnAndSunset) * ((double)216 / 255);
                    b += (DawnAndSunset) * ((double)205 / 255);
                }
                else if (TimeFrom0to2 > 1.26 && TimeFrom0to2 < 1.77)
                {
                    DawnAndSunset = (0.25 - Math.Abs(TimeFrom0to2 - 1.505)) * 2;

                    r += (DawnAndSunset) * ((double)0.99);
                    g += (DawnAndSunset) * ((double)0.37);
                    b += (DawnAndSunset) * ((double)0.33);
                }


                gl.ClearColor((float)r, (float)g, (float)b, 1.0f);
            }
        }
        public static localtime time = new localtime();
    }
}
