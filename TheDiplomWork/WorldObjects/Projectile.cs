using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
using System.Windows.Forms;
namespace TheDiplomWork
{
    class Projectile : GeneralProgrammingStuff
    {
        public static Localized_projectile jp = new Localized_projectile();
        public class Localized_projectile
        {
            public bool Launched = false;

            public Localized_projectile()
            {
                
            }
            public class Starting_Data
            {
                public vec3 default_velocity = new vec3(0, 20, 0);
                public vec3 starting_velocity = new vec3(0, 0, 0);
            }
            public Starting_Data sd = new Starting_Data();

            public bool RotatingStartingVelocity = false;

            float StartingVelocity = 20f;

            Point3D Player_rotational_view_OLD = new Point3D(0, 0, 0);
            Point3D Player_rotational_view_Result = new Point3D(0, 0, 0);
            public void SetStartingPlayerView()
            {
                if (!Projectile.jp.RotatingStartingVelocity)
                {
                    Projectile.jp.RotatingStartingVelocity = true;
                    Player_rotational_view_OLD.x = Scene.SS.env.player.coords.Player_rotational_view.y;
                    Player_rotational_view_OLD.y = -Scene.SS.env.player.coords.Player_rotational_view.x;
                    Player_rotational_view_OLD.z = Scene.SS.env.player.coords.Player_rotational_view.z;
                }
            }
            float MultiPlayerForView = 2.0f;
            public void SetEndingPlayerView()
            {
                if (Projectile.jp.RotatingStartingVelocity)
                {
                    Projectile.jp.RotatingStartingVelocity = false;
                    Player_rotational_view_Result.x += MultiPlayerForView * (Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.x);
                    Player_rotational_view_Result.y += MultiPlayerForView * ((-Scene.SS.env.player.coords.Player_rotational_view.x) - Player_rotational_view_OLD.y);
                    Player_rotational_view_Result.z += MultiPlayerForView * (Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z);
                }
            }
            vec3 angles = new vec3(0, 0, 0);
            public vec3 PlayerAngles()
            {
                if (RotatingStartingVelocity)
                {
                    angles.x = Player_rotational_view_Result.x + MultiPlayerForView * (Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.x) - AngleBetweenStartingAndCurrentVelocity();
                    angles.y = Player_rotational_view_Result.y + MultiPlayerForView * ((-Scene.SS.env.player.coords.Player_rotational_view.x) - Player_rotational_view_OLD.y);
                    angles.z = Player_rotational_view_Result.z + MultiPlayerForView * (Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z);
                }
                else
                {
                    angles.x = Player_rotational_view_Result.x - AngleBetweenStartingAndCurrentVelocity();
                    angles.y = Player_rotational_view_Result.y;
                    angles.z = Player_rotational_view_Result.z;
                }
                return angles;
            }
            public void Player_rotational_view_Result_NULLIFICATE()
            {
                Player_rotational_view_Result.x = 0;
                Player_rotational_view_Result.y = 0;
                Player_rotational_view_Result.z = 0;
            }
            vec3 Angles(bool deactivated)
            {
                vec3 nv = glm.normalize(Velocity());

                angles.x = (float)Math.Atan(nv.y / nv.x);
                angles.y = (float)Math.Atan(Math.Sqrt(nv.x * nv.x + nv.y + nv.y) / nv.z);
                angles.z = 0;

                return angles;
            }
            CubicalMemory.Cube hpos1 = null;
            CubicalMemory.Cube hpos2 = null;

            public vec3 hposition1 = new vec3(0, 0, 0);
            public vec3 hposition2 = new vec3(0, 0, 0);

            public bool Loaded = false;
            public void LoadFromFile()
            {
                vec3things.TryLoad(ref Projectile.jp.hposition1, "hposition1");
                vec3things.TryLoad(ref Projectile.jp.hposition2, "hposition2");
                
                GatherCubes();
            }

            public vec3 TrueLocation()
            {
                return Projectile.jp.center + Projectile.jp.Coordinates();
            }
            public vec3 center = new vec3(0,0,0);
            public void SetHpos1()
            {
                hpos1 = Cube_Selection.Decide_Position_To_Place_Cube(false);
            }
            public void SetHpos2()
            {
                if (hpos1 == null)
                {
                    MessageBox.Show("Select Hpos1(K) before Hpos2(U)!");
                    return;
                }

                hpos2 = Cube_Selection.Decide_Position_To_Place_Cube(false);

                ShaderedScene.CalculateFromMaptoGraphical(hpos1, ref hposition1);
                ShaderedScene.CalculateFromMaptoGraphical(hpos2, ref hposition2);

                GatherCubes();
            }
            public List<CubicalMemory.Cube> ProjectileParts = new List<CubicalMemory.Cube>();
            public CubicalMemory.Cube CenterCube = null;
            double CenterCube_RangeMax = double.MaxValue;
            public void GatherCubes()
            {
                Loaded = true;

                if (ProjectileParts.Count() != 0)
                    foreach (var item in ProjectileParts)
                        item.IsTakenForExplosion = false;
                ProjectileParts.Clear();

                center = (hposition1 + hposition2) / 2;

                vec3 pos_min = new vec3(Math.Min(hposition1.x, hposition2.x),
                    Math.Min(hposition1.y, hposition2.y),
                    Math.Min(hposition1.z, hposition2.z));

                vec3 pos_max = new vec3(Math.Max(hposition1.x, hposition2.x),
                    Math.Max(hposition1.y, hposition2.y),
                    Math.Max(hposition1.z, hposition2.z));

                vec3 cubeposition = new vec3(0, 0, 0);
                foreach (var XWorld in Scene.SS.env.cub_mem.world.World_as_Whole)
                    foreach (var XYWorld in XWorld)
                        foreach (var Xcube in XYWorld.cubes)
                            foreach (var XYcube in Xcube)
                                foreach (var XYZcube in XYcube)
                                {
                                    ShaderedScene.CalculateFromMaptoGraphical(XYZcube, ref cubeposition);

                                    if (cubeposition.x >= pos_min.x &&
                                        cubeposition.y >= pos_min.y &&
                                        cubeposition.z >= pos_min.z &&
                                        cubeposition.x <= pos_max.x &&
                                        cubeposition.y <= pos_max.y &&
                                        cubeposition.z <= pos_max.z
                                        )
                                    {
                                        vec3 ranged = cubeposition - center;
                                        double range = Math.Sqrt(ranged.x * ranged.x + ranged.y * ranged.y + ranged.z * ranged.z);

                                        if (range < CenterCube_RangeMax)
                                        {
                                            CenterCube_RangeMax = range;
                                            CenterCube = XYZcube;
                                        }

                                        if (XYZcube.IsFilled)
                                        {
                                            XYZcube.IsTakenForExplosion = true;
                                            ProjectileParts.Add(XYZcube);
                                        }
                                    }
                                }

                Scene.SS.ProjectileList.Reloader();
                StaticSettings.S.RealoderCauseOfBuildingBlocks = true;
            }
            public void ProcessStartingData()
            {
                FromShaderWithLove.ShaderRotator SR = new FromShaderWithLove.ShaderRotator(center + sd.default_velocity);
                if (!Launched)
                {
                    sd.starting_velocity = SR.ReturnTheThing();//Rotate(sd.default_velocity, PlayerAngles());
                }
            }
            public void Launch()
            {
                StartingTimeToFly = Time.time.GetGameTotalSeconds();
                Launched = true;
            }
            public void DeLaunch()
            {
                Launched = false;
            }

            

            vec3 os_x = new vec3(1, 0, 0);
            vec3 os_y = new vec3(1, 0, 0);

            

            float step = 0.01f;
            vec3 Velocity()
            {
                return (CoordinatesAtTime(TimeOfFlight() + step) - CoordinatesAtTime(TimeOfFlight()))/step;
            }
            float vec3_length(vec3 inp)
            {
                return (float)Math.Sqrt(inp.x * inp.x + inp.y * inp.y + inp.z * inp.z);
            }

            public float AngleBetweenStartingAndCurrentVelocity()
            {
                vec3 v1 = sd.starting_velocity;
                vec3 v2 = Velocity();

                float temp = (float)Math.Acos((glm.dot(v1,v2)) / (vec3_length(v1) * vec3_length(v2)));

                if (double.IsNaN(temp)) return 0; else return temp;
            }
            double StartingTimeToFly = 0;
            float TimeOfFlight()
            {
                if (!Launched) return 0;
                else return (float)(Time.time.GetGameTotalSeconds() - StartingTimeToFly);
            }

            vec3 coordinates = new vec3(0, 0, 0);
            public vec3 Coordinates()
            {
                return CoordinatesAtTime(TimeOfFlight());
            }
            vec3 CoordinatesAtTime(float time)
            {
                coordinates.x = sd.starting_velocity.x* time;
                coordinates.y = sd.starting_velocity.y * time - (9.8f / 2f) * time * time;
                coordinates.z = sd.starting_velocity.z * time;

                return coordinates;
            }
            public string GetStringedVec3(vec3 inp)
            {
                return inp.x.ToString() + " ; " + inp.y.ToString() + " ; " + inp.z.ToString();
            }
            public mat3 GetProjectileMatrix()
            {
                //Первый вектор. Координаты
                //Второй вектор.... Угол поворота
                //Третий вектор. Центр Снаряда.

                return new mat3(Coordinates(),
                    PlayerAngles(),
                    center
                    );
            }
        }
    }
}
