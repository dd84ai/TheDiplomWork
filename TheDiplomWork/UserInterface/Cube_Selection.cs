using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Cube_Selection
    {
        public static CubicalMemory.Cube Decide_Position_To_Place_Cube(bool FallingCube)
        {
            int y = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
            bool found = false;
            if (FallingCube)
            {
                for (; y >= 0; y--)
                    if (!Scene.SS.env.cub_mem.world.World_as_Whole
                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                    [y]
                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled
                    || Scene.SS.env.cub_mem.world.World_as_Whole
                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                    [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                    [y]
                    [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsTakenForExplosion)
                    { found = true; }
                    else break;

                if (found)
                    y++;
                else y = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
            }
            return Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z];
        }
    }
}
