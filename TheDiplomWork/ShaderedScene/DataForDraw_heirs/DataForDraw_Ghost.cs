﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_Ghost : DataForDraw_angled
    {
        public DataForDraw_Ghost(OpenGL gl) : base(gl)
        {
        }
        public override void initialization()
        {
            START_initialization();

            //Ghost Cube
            if (StaticSettings.S.GhostCube_Add_in_Data_For_Draw)
            {
                bool thing = false;
                bool found = false;
                try
                {
                    thing = Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled;
                    found = true;

                }
                catch (Exception)
                {
                    found = false;
                }

                if (found)
                {
                    System.Drawing.Color result = System.Drawing.Color.Gray;
                    if (thing)
                    {
                        System.Drawing.Color colour = Scene.SS.env.cub_mem.world.World_as_Whole
                                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].color;

                        result = System.Drawing.Color.FromArgb
                            (
                            (int)((float)(GraphicalOverlap.GO_color.R + colour.R) / 2),
                            (int)((float)(GraphicalOverlap.GO_color.G + colour.G) / 2),
                            (int)((float)(GraphicalOverlap.GO_color.B + colour.B) / 2)
                            );
                    }
                    else result = GraphicalOverlap.GO_color;

                    ShaderedScene.CalculateFromMaptoGraphical(Scene.SS.env.player.coords.Player_chunk_lookforcube,
                        Scene.SS.env.player.coords.Player_cubical_lookforcube, ref x, ref y, ref z);

                    Draw_Quad_Full_Sunsided_angled(x, y, z, 0.0f,0.0f,0.0f, localed_range, result, 0, true);

                    
                }
            }
            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
