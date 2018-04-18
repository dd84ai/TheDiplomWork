using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class DataForDraw_Ghost : DataForDraw
    {
        public override void initialization()
        {
            START_initialization();

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

                Draw_Quad_Full_Sunsided(x, y, z, localed_range, GraphicalOverlap.GO_color, true);

            }

            END_initialization();
        }
    }
}
