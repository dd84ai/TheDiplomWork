using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class StaticShadow
    {
        public class Shadow
        {
            public bool ViewFromSun = false;

            public bool ShadowProtocolWasPressed = false;
            float rememberPlayerHeight = 0;
            public vec3 SunPosition = new vec3(0, 0, 0);
            public void ShadowProtocol()
            {
                vec3 DefaultSunPosition = new vec3(0, Sun.LocalSun.Sun_Height, 0);
                SunPosition = FromShaderWithLove.ShaderRotator.Rotate(DefaultSunPosition, new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0)) + new vec3(Sun.S.player_pos.x, 0, Sun.S.player_pos.z);

                if (ShadowProtocolWasPressed)
                {
                    ShadowProtocolWasPressed = false;
                    Scene.SS.env.player.coords.Player_precise_position.y = rememberPlayerHeight;
                }
                if (Keyboard.Ctrl_is_pressed || StaticShadow.Sh.ViewFromSun)
                {
                    //SHADOWPROTOCOL
                    StaticAccess.FMOS.scene.viewMatrix = glm.translate(new mat4(1.0f), new vec3(-SunPosition.x,
                        -(SunPosition.y),
                        -SunPosition.z));

                    // = Sun.S.player_pos.y;
                    Sun.S.player_pos.y = CubicalMemory.Chunk.Height * CubicalMemory.Cube.rangeOfTheEdge - 0.5f;
                    rememberPlayerHeight = Scene.SS.env.player.coords.Player_precise_position.y;
                    Scene.SS.env.player.coords.Player_precise_position.y = CubicalMemory.Chunk.Height * CubicalMemory.Cube.rangeOfTheEdge - 0.5f;
                    StaticSettings.S.PointOfViewCuterEnabled = 0.0f;
                    ShadowProtocolWasPressed = true;

                    StaticAccess.FMOS.scene.rotMatrix = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate((float)Math.PI / 2 - (float)Time.time.GetTotalRadianTime(), new vec3(1.0f, 0.0f, 0.0f)) * glm.rotate((float)Math.PI, new vec3(0.0f, 1.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f));

                    //StaticAccess.FMOS.scene.playerMatrix_veced[2] = (SunPosition - Sun.S.player_pos);
                }
                else
                {
                    StaticSettings.S.PointOfViewCuterEnabled = 1.0f;
                }
            }
        }

        public static Shadow Sh = new Shadow();
    }
}
