using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class DataForDraw_Ghost : DataForDraw
    {
        public override void initialization()
        {
            Memory_Init();
            vertices_count = 0;
            colours_count = 0;

            bool found = false;
            try
            {
                bool thing = Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled;
                if (!thing) found = true;
            }
            catch (Exception e)
            {
                found = false;
            }

            if (found)
            {
                ShaderedScene.CalculateFromMaptoGraphical(Scene.SS.env.player.coords.Player_chunk_lookforcube,
                    Scene.SS.env.player.coords.Player_cubical_lookforcube, ref x, ref y, ref z);

                Draw_Quad_Full_Sunsided(x, y, z, localed_range, System.Drawing.Color.White, true);

            }
            Extra_Remover(ref vertices, vertices_count);
            Extra_Remover(ref colours, colours_count);
            CopiedLastResult = false;
            vertices_arrayed = vertices.ToArray();
            colours_arrayed = colours.ToArray();
        }
    }
    class DataForDraw_Main : DataForDraw
    {
        static vec4 CalculatingThing = new vec4(0, 0, 0, 0);
        vec3 NormalizedToXYWorld = new vec3(0, 0, 0);

        public override void initialization()
        {
            Memory_Init();
            vertices_count = 0;
            colours_count = 0;

            //FOR OG WAR
            Draw_Quad_Perpendecular_to_OSy
                (
                0, //Start_x
                0, //Start_z
                CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Width * CubicalMemory.Cube.rangeOfTheEdge, //End_x
                CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Length * CubicalMemory.Cube.rangeOfTheEdge, //End_z
                0, //Height
                System.Drawing.Color.Gray
                );

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
                                    NormalizedToXYWorld.x = Scene.SS.env.player.coords.Player_precise_stepback.x - x;
                                    NormalizedToXYWorld.y = Scene.SS.env.player.coords.Player_precise_stepback.y - y;
                                    NormalizedToXYWorld.z = Scene.SS.env.player.coords.Player_precise_stepback.z - z;
                                    float range = GeneralProgrammingStuff.vec3_range(NormalizedToXYWorld);
                                    GeneralProgrammingStuff.vec3_normalize(ref NormalizedToXYWorld, range);
                                    Scene.SS.env.player.coords.LookForCube();
                                    float scalar = GeneralProgrammingStuff.vec3_scalar(NormalizedToXYWorld, Scene.SS.env.player.coords.NormalizedLook);
                                    //POINT OF VIEWER

                                    if (!StaticSettings.S.RealoderCauseOfPointOfView || scalar > 0 && range < CubicalMemory.Cube.rangeOfTheEdge * CubicalMemory.Chunk.Width * StaticSettings.S.RangeOfView)
                                    {
                                        if (!StaticSettings.S.RealoderCauseOfSunSided ||
                                            XYworld.xz.x == Scene.SS.env.player.coords.Player_chunk_position.x
                                            && XYworld.xz.z == Scene.SS.env.player.coords.Player_chunk_position.z)
                                            Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color, true);
                                        else
                                            Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color);
                                    }
                                }
                            }
                }
            }
            Extra_Remover(ref vertices, vertices_count);
            Extra_Remover(ref colours, colours_count);
            CopiedLastResult = false;
            vertices_arrayed = vertices.ToArray();
            colours_arrayed = colours.ToArray();
        }
    }
}
