using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_Main : DataForDraw_without_angles
    {
        public DataForDraw_Main(OpenGL gl) : base(gl)
        {
        }

        static vec4 CalculatingThing = new vec4(0, 0, 0, 0);

        public override void initialization()
        {
            START_initialization();

            int i = 0;
            int j = 0;

            int value = 0;
            if ((value = Scene.SS.env.player.coords.Player_chunk_position.x - StaticSettings.S.RangeOfView) > 0)
                i = value + 1;
            else i = 0;

            for (; i < Scene.SS.env.cub_mem.world.World_as_Whole.Count() && i < Scene.SS.env.player.coords.Player_chunk_position.x + StaticSettings.S.RangeOfView; i++)
            {
                if ((value = Scene.SS.env.player.coords.Player_chunk_position.z - StaticSettings.S.RangeOfView) > 0)
                    j = value + 1;
                else j = 0;

                for (; j < Scene.SS.env.cub_mem.world.World_as_Whole[i].Count() && j < Scene.SS.env.player.coords.Player_chunk_position.z + StaticSettings.S.RangeOfView; j++)
                {
                    var XYworld = Scene.SS.env.cub_mem.world.World_as_Whole[i][j];

                    if (Math.Abs(XYworld.xz.x - Scene.SS.env.player.coords.Player_chunk_position.x) < StaticSettings.S.RangeOfView
                        && Math.Abs(XYworld.xz.z - Scene.SS.env.player.coords.Player_chunk_position.z) < StaticSettings.S.RangeOfView)

                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                if (XYZcube.IsFilled && !XYZcube.IsTakenForExplosion)
                                {
                                    ShaderedScene.CalculateFromMaptoGraphical(XYZcube, ref x, ref y, ref z);

                                    //POINT OF VIEWER
                                    Scene.SS.env.player.NormalizedToXYWorld.x = Scene.SS.env.player.coords.Player_precise_stepback.x - x;
                                    Scene.SS.env.player.NormalizedToXYWorld.y = Scene.SS.env.player.coords.Player_precise_stepback.y - y;
                                    Scene.SS.env.player.NormalizedToXYWorld.z = Scene.SS.env.player.coords.Player_precise_stepback.z - z;
                                    float range = GeneralProgrammingStuff.vec3_range(Scene.SS.env.player.NormalizedToXYWorld);
                                    GeneralProgrammingStuff.vec3_normalize(ref Scene.SS.env.player.NormalizedToXYWorld, range);
                                    Scene.SS.env.player.coords.LookForCube();
                                    float scalar = GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.NormalizedToXYWorld, Scene.SS.env.player.coords.NormalizedLook);
                                    //POINT OF VIEWER

                                    if (!StaticSettings.S.RealoderCauseOfPointOfView || scalar > 0)
                                    {

                                        //if (!StaticSettings.S.RealoderCauseOfSunSided ||
                                        //    XYworld.xz.x == Scene.SS.env.player.coords.Player_chunk_position.x
                                        //    && XYworld.xz.z == Scene.SS.env.player.coords.Player_chunk_position.z)
                                        //    Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color, true); //Вроде обычный куб.
                                        //else
                                        if (!StaticSettings.S.PointOfView_Circled_Visible_Cubes || range < CubicalMemory.Cube.rangeOfTheEdge * CubicalMemory.Chunk.Width * (StaticSettings.S.RangeOfView))
                                            Draw_Quad_Full_Sunsided_not_angled(x, y, z, localed_range, XYZcube.color);
                                    }
                                }
                            }
                }
            }
            END_initialization();
        }
    }
}
