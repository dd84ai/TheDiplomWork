using System;
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
        public void Draw_Quad_Full_Sunsided_angled(
            float _x, float _y, float _z, float localed_range, System.Drawing.Color _colour, float _angled_x, float _angled_y, float _angled_z, bool SunSided_DRAWFULL = false)
        {
            angled_x = _angled_x; angled_y = _angled_y; angled_z = _angled_z;
            Draw_Quad_Full_Sunsided(_x, _y, _z, localed_range, _colour, SunSided_DRAWFULL);
        }
        public override void Process_Point(float _x, float _y, float _z, System.Drawing.Color _colour, int number)
        {
            Add_Value(ref vertices, vertices_count++, _x);
            Add_Value(ref vertices, vertices_count++, _y);
            Add_Value(ref vertices, vertices_count++, _z);

            Add_Value(ref center, center_count++, x + localed_range / 2);
            Add_Value(ref center, center_count++, y + localed_range / 2);
            Add_Value(ref center, center_count++, z + localed_range / 2);

            Add_Value(ref angles, angles_count++, angled_x);
            Add_Value(ref angles, angles_count++, angled_y);
            Add_Value(ref angles, angles_count++, angled_z);

            if (StaticSettings.S.GradientLightEffect && number == 0)
                _colour = System.Drawing.Color.FromArgb(
                    (_colour.R * 3 + 255) / 4,
                    (_colour.G * 3 + 255) / 4,
                    (_colour.B * 3 + 255) / 4);

            if (StaticSettings.S.GradientLightEffect && number == 2)
                _colour = System.Drawing.Color.FromArgb(
                    (_colour.R * 7 + 0) / 8,
                    (_colour.G * 7 + 0) / 8,
                    (_colour.B * 7 + 0) / 8);

            Add_Value(ref colours, colours_count++, (float)_colour.R / 255);
            Add_Value(ref colours, colours_count++, (float)_colour.G / 255);
            Add_Value(ref colours, colours_count++, (float)_colour.B / 255);
        }
    }
}
