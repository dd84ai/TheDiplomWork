using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public class Player : GeneralProgrammingStuff
    {
        public class Coords
        {
            //По идеи данная стартовая позиция будет ровно посередине мира и немного выше.
            public Point3D Player_precise_position = new Point3D(0, 0, 0);
            public Point3D Player_precise_position_realToPreventEscapes = new Point3D(0, 0, 0);

            //public Point3D Player_Default_position = new Point3D(0, 0, 0);
            public Point3D Player_Default_position = new Point3D(
                (float)(((8 + 8 * CubicalMemory.Chunk.Width) * (double)CubicalMemory.Cube.rangeOfTheEdge)),
                11.5f * CubicalMemory.Cube.rangeOfTheEdge,
                (float)(((8 + 8 * CubicalMemory.Chunk.Length) * (double)CubicalMemory.Cube.rangeOfTheEdge)));
            //public Point3D Player_Default_position = new Point3D(0, 0, 0);
            public Point3D Player_rotational_view = new Point3D(3.14f / 2f, 0, 0);

            public Point2Int Player_chunk_position = new Point2Int(0,0);
            public Point2Int Player_chunk_position_OLD = new Point2Int(0, 0);

            public Point3Int Player_cubical_position = new Point3Int(0,0,0);
            public Point3Int Player_cubical_position_OLD = new Point3Int(0, 0, 0);
            public void Player_recalculate_extra_positions()
            {
                Reverse_presice_to_map_coords(Player_precise_position, ref Player_chunk_position, ref Player_cubical_position);
                LookForCube();

                if (!StaticSettings.S.FlyMod) TryToFallDown();
                //Keyboard.Wrapped_KeyPressed_Reaction('x');
            }

            public void TryToFallDown()
            {
                try
                {
                    if (!Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_position.x]
                                [Scene.SS.env.player.coords.Player_chunk_position.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_position.x]
                                [Scene.SS.env.player.coords.Player_cubical_position.y - 2]
                                [Scene.SS.env.player.coords.Player_cubical_position.z].IsFilled)
                    {
                        Player_precise_position.y -= 0.1f;
                    }
                    else
                    {
                        //Player_precise_position.y -= 0.1f;
                    }
                    {

                    }
                }
                catch (Exception Error)
                { }
            }
            /// <summary>
            /// Recalculate coordinates from player graphic position to chunk & cubical position.
            /// </summary>
            public void Reverse_presice_to_map_coords(Point3D _precise, ref Point2Int _chunk, ref Point3Int _cubical)
            {
                _chunk.x = (int)((_precise.x / (double)CubicalMemory.Cube.rangeOfTheEdge) / (double)CubicalMemory.Chunk.Width);
                _chunk.z = (int)((_precise.z / (double)CubicalMemory.Cube.rangeOfTheEdge) / (double)CubicalMemory.Chunk.Length);

                int x = (int)(_precise.x / (double)CubicalMemory.Cube.rangeOfTheEdge);
                int z = (int)(_precise.z / (double)CubicalMemory.Cube.rangeOfTheEdge);
                _cubical.y = (int)(_precise.y / (double)CubicalMemory.Cube.rangeOfTheEdge);

                x -= _chunk.x * CubicalMemory.Chunk.Width;
                z -= _chunk.z * CubicalMemory.Chunk.Length;

                _cubical.x = x;
                _cubical.z = z;
            }

            public Point3D Player_precise_lookforcube = new Point3D(0, 0, 0);
            public Point2Int Player_chunk_lookforcube = new Point2Int(0, 0);
            public Point3Int Player_cubical_lookforcube = new Point3Int(0, 0, 0);
            public Point3Int Player_cubical_lookforcube_temporal = new Point3Int(0, 0, 0);
            public Point3Int Player_cubical_lookforcube_OLD = new Point3Int(0, 0, 0);

            vec4 StepForARequiedCube = new vec4(0,0, 2.5f*CubicalMemory.Cube.rangeOfTheEdge,0);

            public vec3 NormalizedLook = new vec3(0,0,0);
            public vec3 LastPlayerLook = new vec3(0, 0, 0);

            public Point3Int Player_cubical_lastbeforecolission = new Point3Int(-1, -1, -1);
            public void LookForCube()
            {
                //StepForARequiedCube.z = 0.5f * CubicalMemory.Cube.rangeOfTheEdge;
                /*bool found = false;
                int visible_range = 4 * 3;
                for (int i = 0; i < visible_range; i++
                    )
                {
                    
                    StepForARequiedCube.z = 0.25f * CubicalMemory.Cube.rangeOfTheEdge * (float)i;
                    GeneralProgrammingStuff.Point3D.CopyToFrom(
                    ref Player_precise_lookforcube, Player_precise_position);
                    Keyboard.Wrapped_Do_Step(StepForARequiedCube, ref Player_precise_lookforcube, true);
                    Reverse_presice_to_map_coords(Player_precise_lookforcube, ref Player_chunk_lookforcube, ref Player_cubical_lookforcube);

                    if (Player_cubical_lookforcube != Player_cubical_position)
                    {
                        try
                        {
                            if (!Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled)
                            {
                                Player_cubical_lastbeforecolission.x = Player_cubical_lookforcube.x;
                                Player_cubical_lastbeforecolission.y = Player_cubical_lookforcube.y;
                                Player_cubical_lastbeforecolission.z = Player_cubical_lookforcube.z;
                            }
                            else break;
                        }
                        catch (Exception e) { }
                    }
                }*/

                GeneralProgrammingStuff.Point3D.CopyToFrom(
                ref Player_precise_lookforcube, Player_precise_position);
                Keyboard.Wrapped_Do_Step(StepForARequiedCube, ref Player_precise_lookforcube, true);
                Reverse_presice_to_map_coords(Player_precise_lookforcube, ref Player_chunk_lookforcube, ref Player_cubical_lookforcube);

                /*if (Player_cubical_lookforcube_temporal == Player_cubical_position)
                {
                    StepForARequiedCube.z = 1f * CubicalMemory.Cube.rangeOfTheEdge;
                    GeneralProgrammingStuff.Point3D.CopyToFrom(
                ref Player_precise_lookforcube, Player_precise_position);
                    Keyboard.Wrapped_Do_Step(StepForARequiedCube, ref Player_precise_lookforcube, true);
                    Reverse_presice_to_map_coords(Player_precise_lookforcube, ref Player_chunk_lookforcube, ref Player_cubical_lookforcube);
                }
                else
                GeneralProgrammingStuff.Point3Int.CopyToFrom(ref Player_cubical_lookforcube, Player_cubical_lookforcube_temporal);*/

                //Нормализованный взгляд
                NormalizedLook.x = Player_precise_lookforcube.x - Player_precise_position.x;
                NormalizedLook.y = Player_precise_lookforcube.y - Player_precise_position.y;
                NormalizedLook.z = Player_precise_lookforcube.z - Player_precise_position.z;

                //По идеи расстояние равно шагу на который сделали
                float range = -1/(float)Math.Sqrt(
                      NormalizedLook.x * NormalizedLook.x
                    + NormalizedLook.y * NormalizedLook.y
                    + NormalizedLook.z * NormalizedLook.z);
                NormalizedLook.x *= range; NormalizedLook.y *= range; NormalizedLook.z *= range;

                //Шаг назад
                GeneralProgrammingStuff.Point3D.CopyToFrom(
                    ref Player_precise_stepback, Player_precise_position);
                Keyboard.Wrapped_Do_Step(StepForARequiedstepback, ref Player_precise_stepback, true);
            }
            public Point3D Player_precise_stepback = new Point3D(0, 0, 0);
            vec4 StepForARequiedstepback = new vec4(0, 0, -0.25f * CubicalMemory.Cube.rangeOfTheEdge * 4 * 2, 0);


            /// <summary>
            /// Short link to Player_recalculate_extra_positions.
            /// </summary>
            void Player_rec() { Player_recalculate_extra_positions(); }
        }
        public Coords coords = new Coords();


    }
}
