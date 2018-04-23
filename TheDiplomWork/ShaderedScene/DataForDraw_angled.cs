﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class DataForDraw_angled : DataForDraw
    {
        public List<float> angles = new List<float>();
        public int angles_count = 0;
        public float[] angles_arrayed;

        public List<float> center = new List<float>();
        public int center_count = 0;
        public float[] center_arrayed;
        public override void START_initialization()
        {
            START_initialization_wrapped();
            angles_count = 0;
            center_count = 0;
        }
        public override void END_initialization()
        {
            END_initialization_wrapped();

            Extra_Remover(ref angles, angles_count);
            angles_arrayed = angles.ToArray();

            Extra_Remover(ref center, center_count);
            center_arrayed = center.ToArray();
        }


        float angled_x = 0, angled_y = 0, angled_z = 0;
        GeneralProgrammingStuff.Point3D shift = new GeneralProgrammingStuff.Point3D(0, 0, 0);
        public void Draw_Quad_Full_Sunsided_angled(
            float _x, float _y, float _z, float _s_x, float _s_y, float _s_z, float localed_range, System.Drawing.Color _colour, float _angled_x, float _angled_y, float _angled_z, bool SunSided_DRAWFULL = false)
        {
            angled_x = _angled_x; angled_y = _angled_y; angled_z = _angled_z;
            shift.x = _s_x; shift.y = _s_y; shift.z = _s_z;

            Process_Point(_x, _y, _z, _colour, 1);
        }
        public override void Process_Point(float _x, float _y, float _z, System.Drawing.Color _colour, int number)
        {
            base.Process_Point(_x, _y, _z, _colour, number);

            Add_Value(ref center, center_count++, _x + localed_range / 2 + shift.x);
            Add_Value(ref center, center_count++, _y + localed_range / 2 + shift.y);
            Add_Value(ref center, center_count++, _z + localed_range / 2 + shift.z);

            Add_Value(ref angles, angles_count++, angled_x);
            Add_Value(ref angles, angles_count++, angled_y);
            Add_Value(ref angles, angles_count++, angled_z);
        }
    }
}
