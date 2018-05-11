using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    public class DataForDraw_angled : DataForDraw
    {
        public SceneInfo_Secondary scene_info;

        public DataForDraw_angled(OpenGL gl) : base()
        {
            scene_info = new SceneInfo_Secondary(gl);
        }

        public List<float> center = new List<float>();
        public int center_count = 0;
        public float[] center_arrayed;

        public List<float> size = new List<float>();
        public int size_count = 0;
        public float[] size_arrayed;
        public override void START_initialization()
        {
            START_initialization_wrapped();
            center_count = 0;
            size_count = 0;
        }
        public override void END_initialization()
        {
            END_initialization_wrapped();

            Extra_Remover(ref center, center_count);
            center_arrayed = center.ToArray();

            Extra_Remover(ref size, size_count);
            size_arrayed = size.ToArray();
        }


        float size_outer = 0;
        GeneralProgrammingStuff.Point3D shift = new GeneralProgrammingStuff.Point3D(0, 0, 0);
        public void Draw_Quad_Full_Sunsided_angled(
            float _x, float _y, float _z, float _s_x, float _s_y, float _s_z, float localed_range, System.Drawing.Color _colour, float _size, bool SunSided_DRAWFULL = false)
        {
            shift.x = _s_x; shift.y = _s_y; shift.z = _s_z;
            size_outer = _size;
            Process_Point(_x, _y, _z, _colour, 1);
        }
        public override void Process_Point(float _x, float _y, float _z, System.Drawing.Color _colour, int number)
        {
            base.Process_Point(_x, _y, _z, _colour, number);

            Add_Value(ref center, center_count++, shift.x);
            Add_Value(ref center, center_count++, shift.y);
            Add_Value(ref center, center_count++, shift.z);

            Add_Value(ref size, size_count++, size_outer);
            Add_Value(ref size, size_count++, size_outer);
            Add_Value(ref size, size_count++, size_outer);
        }

        
    }
}
