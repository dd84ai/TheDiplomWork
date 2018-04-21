using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class DataForDraw_without_angles : DataForDraw
    {
        public override void START_initialization()
        {
            START_initialization_wrapped();

        }
        public override void END_initialization()
        {
            END_initialization_wrapped();
        }


        public void Draw_Quad_Full_Sunsided_not_angled(
            float _x, float _y, float _z, float localed_range, System.Drawing.Color _colour, bool SunSided_DRAWFULL = false)
        {
            //Draw_Quad_Full_Sunsided(_x, _y, _z, localed_range, _colour, SunSided_DRAWFULL);
            Process_Point(_x, _y, _z, _colour, 1);
            Process_Point(_x + localed_range, _y, _z, _colour, 1);
            Process_Point(_x, _y + localed_range, _z, _colour, 1);
            Process_Point(_x, _y, _z + localed_range, _colour, 1);
        }
        public override void Process_Point(float _x, float _y, float _z, System.Drawing.Color _colour, int number)
        {
            Add_Value(ref vertices, vertices_count++, _x);
            Add_Value(ref vertices, vertices_count++, _y);
            Add_Value(ref vertices, vertices_count++, _z);

            Add_Value(ref colours, colours_count++, (float)_colour.R / 255);
            Add_Value(ref colours, colours_count++, (float)_colour.G / 255);
            Add_Value(ref colours, colours_count++, (float)_colour.B / 255);
        }
    }
}
