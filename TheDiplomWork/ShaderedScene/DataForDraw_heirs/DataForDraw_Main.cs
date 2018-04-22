using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class DataForDraw_Main : DataForDraw_without_angles
    {
        static vec4 CalculatingThing = new vec4(0, 0, 0, 0);

        public override void initialization()
        {
            START_initialization();

            //FOR OG WAR
            //Draw_Quad_Perpendecular_to_OSy
            //    (
            //    0, //Start_x
            //    0, //Start_z
            //    CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Width * CubicalMemory.Cube.rangeOfTheEdge, //End_x
            //    CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Length * CubicalMemory.Cube.rangeOfTheEdge, //End_z
            //    -1, //Height
            //    System.Drawing.Color.Gray
            //    );

            int i = 0;
            int j = 0;

            int value = 0;
            if ((value = Scene.SS.env.player.coords.Player_chunk_position.x - StaticSettings.S.RangeOfView) > 0)
                i = value;
            else i = 0;

            for (; i < Scene.SS.env.cub_mem.world.World_as_Whole.Count() && i < Scene.SS.env.player.coords.Player_chunk_position.x + StaticSettings.S.RangeOfView; i++)
            {
                if ((value = Scene.SS.env.player.coords.Player_chunk_position.z - StaticSettings.S.RangeOfView) > 0)
                    j = value;
                else j = 0;

                for (; j < Scene.SS.env.cub_mem.world.World_as_Whole[i].Count() && j < Scene.SS.env.player.coords.Player_chunk_position.z + StaticSettings.S.RangeOfView; j++)
                {
                    var XYworld = Scene.SS.env.cub_mem.world.World_as_Whole[i][j];

                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                if (XYZcube.IsFilled)
                                {
                                    ShaderedScene.CalculateFromMaptoGraphical(XYworld.xz, XYZcube.xyz, ref x, ref y, ref z);

                                    //POINT OF VIEWER
                                    Scene.SS.env.player.NormalizedToXYWorld.x = Scene.SS.env.player.coords.Player_precise_stepback.x - x;
                                    Scene.SS.env.player.NormalizedToXYWorld.y = Scene.SS.env.player.coords.Player_precise_stepback.y - y;
                                    Scene.SS.env.player.NormalizedToXYWorld.z = Scene.SS.env.player.coords.Player_precise_stepback.z - z;
                                    float range = GeneralProgrammingStuff.vec3_range(Scene.SS.env.player.NormalizedToXYWorld);
                                    GeneralProgrammingStuff.vec3_normalize(ref Scene.SS.env.player.NormalizedToXYWorld, range);
                                    Scene.SS.env.player.coords.LookForCube();
                                    float scalar = GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.NormalizedToXYWorld, Scene.SS.env.player.coords.NormalizedLook);
                                    //POINT OF VIEWER

                                    if (!StaticSettings.S.RealoderCauseOfPointOfView || scalar > 0 && range < CubicalMemory.Cube.rangeOfTheEdge * CubicalMemory.Chunk.Width * StaticSettings.S.RangeOfView)
                                    {
                                        //if (!StaticSettings.S.RealoderCauseOfSunSided ||
                                        //    XYworld.xz.x == Scene.SS.env.player.coords.Player_chunk_position.x
                                        //    && XYworld.xz.z == Scene.SS.env.player.coords.Player_chunk_position.z)
                                        //    Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color, true); //Вроде обычный куб.
                                        //else
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
