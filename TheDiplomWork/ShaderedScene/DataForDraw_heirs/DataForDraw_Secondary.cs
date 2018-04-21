using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class DataForDraw_Secondary : DataForDraw_angled
    {
        public override void initialization()
        {
            START_initialization();

            //Sun
            Draw_Quad_Full_Sunsided_angled(Scene.SS.env.player.coords.Player_precise_position.x,
                Scene.SS.env.player.coords.Player_precise_position.y + localed_range * 100,
                Scene.SS.env.player.coords.Player_precise_position.z,
                localed_range * 10, System.Drawing.Color.Gold, 0.0f, 0.0f, 0.0f, true);

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
                catch (Exception e)
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

                    Draw_Quad_Full_Sunsided_angled(x, y, z, localed_range, result, 0.0f, 0.0f, 0.0f, true);

                }
            }
            END_initialization();
        }
    }
}
