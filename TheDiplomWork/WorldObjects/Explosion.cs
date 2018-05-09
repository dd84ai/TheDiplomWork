﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class Explosion : GeneralProgrammingStuff
    {
        public class Exploder
        {
            CubicalMemory.Chunk_and_Cube_link abc;
            public Exploder()
            {

            }

            public Point3D Bomb_precise_position = new Point3D(0, 0, 0);
            public float Explosion_radius = 20.0f;

            float x = 0, y = 0, z = 0;
            public void SetBombLocation(float x1, float y1, float z1)
            {
                Bomb_precise_position.x = x1;
                Bomb_precise_position.y = y1;
                Bomb_precise_position.z = z1;
            }

            public float StartingTime = 0;

            public bool StartingFirst = false;
            public bool StartingFirstStarted = false;
            public float StartingShiftForLoeading = 0;

            public CubicalMemory.Chunk_and_Cube_link ExplosionCenter;

            public void Exploding_Rewriter()
            {
                Point2Int Bomb_chunk_position = new Point2Int(0, 0);
                Point3Int Bomb_cubical_position = new Point3Int(0, 0, 0);
                Scene.SS.env.player.coords.Reverse_presice_to_map_coords(Bomb_precise_position, ref Bomb_chunk_position, ref Bomb_cubical_position);

                int Range_of_chunk_explosion = (int)Explosion_radius;
                //DataForDraw_ExplodingList.TemporalList.Clear();

                int i = 0;
                int j = 0;

                int value = 0;
                if ((value = Bomb_chunk_position.x - Range_of_chunk_explosion) > 0)
                    i = value;
                else i = 0;

                for (; i < Scene.SS.env.cub_mem.world.World_as_Whole.Count() && i < Bomb_chunk_position.x + Range_of_chunk_explosion; i++)
                {
                    if ((value = Bomb_chunk_position.z - Range_of_chunk_explosion) > 0)
                        j = value;
                    else j = 0;

                    for (; j < Scene.SS.env.cub_mem.world.World_as_Whole[i].Count() && j < Bomb_chunk_position.z + Range_of_chunk_explosion; j++)
                    {
                        var XYworld = Scene.SS.env.cub_mem.world.World_as_Whole[i][j];

                        if (Math.Abs(XYworld.xz.x - Bomb_chunk_position.x) < Range_of_chunk_explosion
                            && Math.Abs(XYworld.xz.z - Bomb_chunk_position.z) < Range_of_chunk_explosion)

                            foreach (var Xcube in XYworld.cubes)
                                foreach (var XYcube in Xcube)
                                    foreach (var XYZcube in XYcube)
                                    {
                                        if (XYZcube.IsFilled && !XYZcube.IsTakenForExplosion)
                                        {
                                            ShaderedScene.CalculateFromMaptoGraphical(XYworld.xz, XYZcube.xyz, ref x, ref y, ref z);

                                            //POINT OF VIEWER
                                            vec3 range_to_cube = new vec3(0, 0, 0);
                                            range_to_cube.x = Bomb_precise_position.x - x;
                                            range_to_cube.y = Bomb_precise_position.y - y;
                                            range_to_cube.z = Bomb_precise_position.z - z;
                                            float range = GeneralProgrammingStuff.vec3_range(range_to_cube);

                                            if (range < CubicalMemory.Cube.rangeOfTheEdge * Explosion_radius)
                                            {
                                                    XYZcube.IsTakenForExplosion = true;
                                                XYZcube.FallingStartingTime = Explosion.exp.StartingTime;
                                                DataForDraw_ExplodingList.TemporalList.Add(new CubicalMemory.Chunk_and_Cube_link(XYworld,XYZcube));
                                                //Draw_Quad_Full_Sunsided_not_angled(x, y, z, localed_range, XYZcube.color);
                                            }
                                        }
                                    }
                    }
                }

                
            }
            public void Exploding_Last_Cancel()
            {
                foreach (var XYZcube in DataForDraw_ExplodingList.TemporalList)
                {
                    XYZcube.cube.IsTakenForExplosion = false;
                    XYZcube.cube.FallingStartingTime = 0;
                }
            }
            public void Exploding_Restorer()
            {
                foreach (var XWorld in Scene.SS.env.cub_mem.world.World_as_Whole)
                    foreach (var XYWorld in XWorld)
                        foreach (var Xcube in XYWorld.cubes)
                            foreach (var XYcube in Xcube)
                                foreach (var XYZcube in XYcube)
                                    XYZcube.IsTakenForExplosion = false;
            }
        }
        public static Exploder exp = new Exploder();
    }
}
