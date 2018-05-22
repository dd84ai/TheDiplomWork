using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using SharpGL;
namespace TheDiplomWork
{
    class StaticCompass
    {
        public class Compas
        {
            public Compas()
            {
                //Scene.SS.env.player.NormalizedToXYWorld;
            }

            public double Xangle = 0, Zangle = 0;
            vec3 Xvec = new vec3(1, 0, 0);
            vec3 Zvec = new vec3(0, 0, 1);

            public double r = 50.0;
            void RefreshCompas()
            {
                Xangle = Math.Acos(CalculateAngle(Xvec));
                Zangle = Math.Acos(CalculateAngle(Zvec));

                if (Zangle < Math.PI / 2) Xangle = Math.PI + (Math.PI - Xangle);

                Xangle += Math.PI;

                Zangle = Xangle + Math.PI;
            }
            float CalculateAngle(vec3 inp)
            {
                vec3 Look =
                    new vec3(
                        Scene.SS.env.player.coords.NormalizedLook.x,//Scene.SS.env.player.coords.Player_chunk_position_OLD.x - Scene.SS.env.player.coords.Player_precise_stepback.x,
                        0,
                        Scene.SS.env.player.coords.NormalizedLook.z//Scene.SS.env.player.coords.Player_chunk_position_OLD.z - Scene.SS.env.player.coords.Player_precise_stepback.z
                        );

                
                float temp = glm.dot(inp, Look) /(vec3things.LengthForSure(inp) * vec3things.LengthForSure(Look));

                if (temp > 1.0f) return 1.0f;
                if (temp < -1.0f) return -1.0f;
                return temp;
            }

            public void Draw_Compas(OpenGL gl, int cx, int cy)
            {
                RefreshCompas();

                drawFilledCircle(gl, cx, cy, r + 15, System.Drawing.Color.White);

                Draw_Arrow(gl, System.Drawing.Color.Red, cx, cy, Xangle, false);
                Draw_Arrow(gl, System.Drawing.Color.Blue, cx, cy, Zangle, true);

                int radius = (int)(r * 4 / 5);
                int fontsize = 20;
                StaticAccess.FMOS.CF.Ultimate_DrawText(cx - fontsize * 2 / 2, cy - fontsize * 2 / 4 + radius, System.Drawing.Color.Black, fontsize, "+Z", 1.0f);
                StaticAccess.FMOS.CF.Ultimate_DrawText(cx - fontsize * 2 / 2, cy - fontsize * 2 / 4 - radius, System.Drawing.Color.Black, fontsize, "-Z", 1.0f);
                StaticAccess.FMOS.CF.Ultimate_DrawText(cx - fontsize * 2 / 2 - radius, cy - fontsize * 2 / 4, System.Drawing.Color.Black, fontsize, "+X", 1.0f);
                StaticAccess.FMOS.CF.Ultimate_DrawText(cx - fontsize * 2 / 2 + radius, cy - fontsize * 2 / 4, System.Drawing.Color.Black, fontsize, "-X", 1.0f);
            }
            void Draw_Arrow(OpenGL gl, System.Drawing.Color _colour, int cx, int cy, double angle, bool triangle)
            {
                float LineWidth = 2.0f;
                float Line_Height = 0.5f;
                //  Clear the color and depth buffer.
                //  Load the identity matrix.
                gl.LoadIdentity();
                gl.Color((float)_colour.R / 255, (float)_colour.G / 255, (float)_colour.B / 255, 1.0f); //Must have, weirdness!
                gl.LineWidth(LineWidth);
                gl.Begin(OpenGL.GL_LINES);

                //if (angle > 0)
                //    sina = -Math.Sqrt(1 - angle * angle);
                //else sina = +Math.Sqrt(1 - angle * angle);

                double cosa = Math.Cos(angle);
                double sina = Math.Sin(angle);

                int x1 = cx + (int)(r * cosa);
                int y1 = cy + (int)(r * sina);

                gl.Vertex(x1, y1, Line_Height);

                int x2 = cx + (int)(r * (-cosa));
                int y2 = cy + (int)(r * (-sina));

                gl.Vertex(cx, cy, Line_Height);

                gl.End();

                if (triangle)
                {
                    gl.Begin(OpenGL.GL_TRIANGLES);

                    gl.Vertex(x1, y1, Line_Height);

                    int xvec = (x2 - x1) / 10;
                    int yvec = (y2 - y1) / 10;

                    int x3 = x1 + xvec;
                    int y3 = y1 + yvec;

                    int x4 = -yvec;
                    int y4 = xvec;

                    gl.Vertex(x3 + x4, y3 + y4, Line_Height);
                    gl.Vertex(x3 - x4, y3 - y4, Line_Height);

                    gl.End();
                }
            }

            void drawFilledCircle(OpenGL gl, int x, int y, double radius, System.Drawing.Color color)
            {
                gl.Color((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, 1.0f); //Must have, weirdness!
                int i;
                int triangleAmount = 20; //# of triangles used to draw circle

                //GLfloat radius = 0.8f; //radius
                double twicePi = 2.0f * Math.PI;

                float Line_Height = 0.4f;
                gl.Begin(OpenGL.GL_TRIANGLE_FAN);
                gl.Vertex(x, y, Line_Height); // center of circle
                for (i = 0; i <= triangleAmount; i++)
                {
                    gl.Vertex(
                            x + (radius * Math.Cos(i * twicePi / triangleAmount)),
                        y + (radius * Math.Sin(i * twicePi / triangleAmount)),
                        Line_Height
                    );
                }
                gl.End();
            }
        }
        public static Compas C = new Compas();
    }
}
