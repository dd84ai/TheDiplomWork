﻿using System;
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
                    DawnAndSunset = Math.Abs(TimeFrom0to2 - 0.5);
                }
                else if (TimeFrom0to2 > 1.25 && TimeFrom0to2 < 1.75)
                {

                }


                    gl.ClearColor((float)r, (float)g, (float)b, 1.0f);
            }
        }
        public static localtime time = new localtime();
    }
}
