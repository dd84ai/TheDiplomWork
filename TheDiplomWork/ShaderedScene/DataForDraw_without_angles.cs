using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    public class DataForDraw_without_angles : DataForDraw
    {
        public SceneInfo_Main scene_info;

        public DataForDraw_without_angles(OpenGL gl) : base()
        {
            scene_info = new SceneInfo_Main(gl);
        }

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
            //Process_Point(_x + localed_range, _y, _z, _colour, 1);
            //Process_Point(_x, _y + localed_range, _z, _colour, 1);
            //Process_Point(_x, _y, _z + localed_range, _colour, 1);
        }
        public override void Process_Point(float _x, float _y, float _z, System.Drawing.Color _colour, int number)
        {
            base.Process_Point(_x, _y, _z, _colour, number);
        }
        
    }
}
