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
                Player_rotational_view_OLD.x = Scene.SS.env.player.coords.Player_rotational_view.y;
                Player_rotational_view_OLD.y = -Scene.SS.env.player.coords.Player_rotational_view.x;
                Player_rotational_view_OLD.z = Scene.SS.env.player.coords.Player_rotational_view.z;
            }
            public void SetEndingPlayerView()
            {
                Player_rotational_view_Result.x += Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.x;
                Player_rotational_view_Result.y += (-Scene.SS.env.player.coords.Player_rotational_view.x) - Player_rotational_view_OLD.y;
                Player_rotational_view_Result.z += Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z;
            }
            vec3 angles = new vec3(0, 0, 0);
            vec3 PlayerAngles()
            {
                if (RotatingStartingVelocity)
                {
                    angles.x = Player_rotational_view_Result.x + Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.x;
                    angles.y = Player_rotational_view_Result.y + (-Scene.SS.env.player.coords.Player_rotational_view.x) - Player_rotational_view_OLD.y;
                    angles.z = Player_rotational_view_Result.z + Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z;
                }
                else
                {
                    angles.x = Player_rotational_view_Result.x;
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
                ShaderRotator SR = new ShaderRotator(center + sd.default_velocity);
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

            class ShaderRotator
            {
                vec3 in_Position;
                public ShaderRotator(vec3 inp)
                {
                    in_Position = inp;
                }
                vec3 Rotate(vec3 SomethingToRotate, vec3 ang)
                {
                    mat3 RotateX = new mat3(new vec3(1, 0, 0),
                    new vec3(0, (float)Math.Cos(ang.x), -(float)Math.Sin(ang.x)),
                    new vec3(0, (float)Math.Sin(ang.x), (float)Math.Cos(ang.x)));

                    mat3 RotateY = new mat3(new vec3((float)Math.Cos(ang.y), 0, (float)Math.Sin(ang.y)),
                    new vec3(0, 1, 0),
                    new vec3(-(float)Math.Sin(ang.y), 0, (float)Math.Cos(ang.y)));

                    mat3 RotateZ = new mat3(new vec3((float)Math.Cos(ang.z), -(float)Math.Sin(ang.z), 0),
                    new vec3((float)Math.Sin(ang.z), (float)Math.Cos(ang.z), 0),
                    new vec3(0, 0, 1));
                    mat3 Rotator = RotateX * RotateY * RotateZ;

                    vec3 temp = Rotator * SomethingToRotate;
                    return temp;
                }
                vec3 Shifted(vec3 input_vec)
                {
                    return new vec3(input_vec + (in_Position));
                }
                vec3 Deshifted(vec3 input_vec)
                {
                    return new vec3(input_vec - (in_Position));
                }
                vec3 ProcessingProjectile(vec3 inp)
                {
                    //Rotate
                    vec3 ShiftToRotate = Shifted(inp) - Projectile.jp.center;
                    ShiftToRotate = Rotate(ShiftToRotate, Projectile.jp.PlayerAngles());
                    //ShiftToRotate = Deshifted(ShiftToRotate) + Projectile.jp.center;

                    vec3 ShiftedToRelativePosition = Projectile.jp.Coordinates() + ShiftToRotate;

                    return ShiftedToRelativePosition;
                }
                public vec3 ReturnTheThing() { return ProcessingProjectile(new vec3(0, 0, 0)); }
            }

            vec3 os_x = new vec3(1, 0, 0);
            vec3 os_y = new vec3(1, 0, 0);

            

            float step = 0.01f;
            vec3 Velocity()
            {
                return (CoordinatesAtTime(TimeOfFlight() + step) - CoordinatesAtTime(TimeOfFlight()))/step;
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
