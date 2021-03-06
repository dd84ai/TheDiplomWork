﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
using System.Windows.Forms;
using System.IO;
namespace TheDiplomWork
{
    class Projectile : GeneralProgrammingStuff
    {
        public class Settings
        {
            public bool AdvancedPhysics = true;

            //0.05,0.001,0.25,1,0,0
            public double mass { set; get; } = 0.05; //в kg масса объекта для расчета силы воздушного сопротивления(СВС)
            public double area { set; get; } = 0.001; //в м^2 фронтальная площадь объекта влияет на СВС
            public double cd { set; get; } = 0.25; // Коэффициент воздушного сопротивления, раскрывается в более крутую величину.
            public double density { set; get; } = 1; //kg/m^3 Плотноть окружающей среды. 

            public double windVx { set; get; } = 0; //Ветер по Х
            public double windVy { set; get; } = 0; //Ветер по Y

            public double timespeed { set; get; } = 1.0;


            public double Me { set; get; } = 5e-2; //mass of the fragmenting casing//
            public double Mc { set; get; } = 5e-4; //mass of the explosive charge
            public double K { set; get; } = (double)1 / 2; //Geometrical Constant for cube
            public double dE { set; get; } = 2.157e+6f; // J/kg Heat of TNT Explosion

            public double Explosion_radius { set; get; } = 10.0f;

            public Settings()
            {
                //mass = 0.05; //в kg масса объекта для расчета силы воздушного сопротивления(СВС)
                //area = 0.001; //в м^2 фронтальная площадь объекта влияет на СВС
                //cd = 0.25; // Коэффициент воздушного сопротивления, раскрывается в более крутую величину.
                //density = 1; //kg/m^3 Плотноть окружающей среды. 

                //windVx = 0; //Ветер по Х
                //windVy = 0; //Ветер по Y
            }
        }
        public static Settings settings = new Settings();
        public static Localized_projectile jp = new Localized_projectile();
        public class Localized_projectile
        {
            public bool Launched = false;

            public Localized_projectile()
            {
                
            }
            public class Starting_Data
            {
                public float StartingVelocity = 20f;

                public vec3 Get_Default_Velosity() { return new vec3(0, StartingVelocity, 0); }

                vec3 starting_position = new vec3(0, 0, 0);

                vec3 center = new vec3(0, 0, 0);
                public vec3 Get_Center() { return center; }
                public void Set_Center(vec3 inp) { center = inp; }

                vec3 starting_velocity = new vec3(0, 0, 0);
                public vec3 Get_Starting_velocity() { return starting_velocity; }
                public void Set_Starting_velocity(vec3 inp) { starting_velocity = inp; }


                public void ChangeStartingVelocity(float value)
                {
                    if (StartingVelocity + value > 0) StartingVelocity += value;
                }
            }
            public Starting_Data sd = new Starting_Data();

            public bool RotatingStartingVelocity = false;

            

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

            public float TimePauseUntilExplosion = 0.6f;
            public vec3 AbsoluteEstimatedLocation(bool GiveMeNewVers = false)
            {
                if (!Projectile.settings.AdvancedPhysics && !GiveMeNewVers) return sd.Get_Center() + Projectile.jp.CoordinatesAtTime(TimeOfFlight() + TimePauseUntilExplosion);
                else return sd.Get_Center() + WP.get_vec3_Predicted_Position(TimePauseUntilExplosion);
            }
            public vec3 AbsoluteEstimatedLocation_with_CoordinatesAtTimeAtHighPart()
            {
                return sd.Get_Center() + Projectile.jp.CoordinatesAtTimeAtHighPart(TimePauseUntilExplosion);
            }
            public vec3 CoordinatesAtTimeAtHighPart(float time, bool GiveMeNewVers = false)
            {
                if (!Projectile.settings.AdvancedPhysics && !GiveMeNewVers) return CoordinatesAtTime(TimeOfFlight() + time) + half_height * glm.normalize(Velocity());
                else return WP.get_vec3_Predicted_Position(time) +half_height * glm.normalize(Velocity(true)) - glm.normalize(Velocity(true));
            }
            public vec3 AbsoluteLocationAtTime(float _time, bool GiveMeNewVers = false)
            {
                if (!Projectile.settings.AdvancedPhysics && !GiveMeNewVers)
                    return sd.Get_Center() + Projectile.jp.CoordinatesAtTime(_time);
                else
                {
                    return sd.Get_Center() + Projectile.jp.WP.get_vec3_Predicted_Position_NotDepenedToTime(_time);
                }
            }

            
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

                sd.Set_Center((hposition1 + hposition2) / 2);

                GatherCubes();
            }
            public List<CubicalMemory.Cube> ProjectileParts = new List<CubicalMemory.Cube>();
            public CubicalMemory.Cube CenterCube = null;
            double CenterCube_RangeMax = double.MaxValue;

            public vec3 high_part = new vec3(0, 0, 0);
            public float half_height = 0;
            public void GatherCubes()
            {
                Loaded = true;

                if (ProjectileParts.Count() != 0)
                    foreach (var item in ProjectileParts)
                        item.IsTakenForExplosion = false;
                ProjectileParts.Clear();

                sd.Set_Center((hposition1 + hposition2) / 2);


                vec3 pos_min = new vec3(Math.Min(hposition1.x, hposition2.x),
                    Math.Min(hposition1.y, hposition2.y),
                    Math.Min(hposition1.z, hposition2.z));

                vec3 pos_max = new vec3(Math.Max(hposition1.x, hposition2.x),
                    Math.Max(hposition1.y, hposition2.y),
                    Math.Max(hposition1.z, hposition2.z));

                half_height = hposition2.y - hposition1.y;

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
                                        vec3 ranged = cubeposition - sd.Get_Center();
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

            public bool Exploded = false;
            public float TimeOfExplosion = 99999999999;
            //Масса в килограмах, область в квадратных метрах, коэф сопротив безразм, плотность килограм на кубометр.
            public WindProjectile WP = new WindProjectile(0, 0, 0, 0, 0, 0, 0,
                Projectile.settings.mass,
                Projectile.settings.area,
                Projectile.settings.density,
                Projectile.settings.cd,
                Projectile.settings.windVx,
                Projectile.settings.windVy);

            public vec3 LastPositionForMeasurements = new vec3(0, 0, 0);
            public float TotalRangeZXForMeasurements = 0;
            public float TotalFlyingDistanceInAnArcWay = 0;
            public void ProcessStartingData()
            {
                if (Loaded)
                {
                    

                    FromShaderWithLove.ShaderRotator SR = new FromShaderWithLove.ShaderRotator(sd.Get_Center() + sd.Get_Default_Velosity());
                    if (!Launched)
                    {
                        sd.Set_Starting_velocity(SR.ReturnTheThing());//Rotate(sd.default_velocity, PlayerAngles());
                        WP.Reiniting_StartingPositionAndVelocity(0, 0, 0, sd.Get_Starting_velocity().x, sd.Get_Starting_velocity().z, sd.Get_Starting_velocity().y, 0);

                        LastPositionForMeasurements.x = 0;
                        LastPositionForMeasurements.y = 0;
                        LastPositionForMeasurements.z = 0;

                        TotalFlyingDistanceInAnArcWay = 0;
                        TotalRangeZXForMeasurements = 0;
                    }
                    else
                    {
                        if (!(WP.getTime() > TimeOfExplosion) && !StaticAccess.FMOS.table_Menu_main.Visible)
                        {
                            WP.updateLocationAndVelocity(Time.time.Get_TimeLastIncreasement());

                            //float test1 = Coordinates().y;
                            //float test2 = sd.Get_Center().y;
                            if (Coordinates().y > 0)
                            {
                                TotalFlyingDistanceInAnArcWay += (float)Math.Sqrt(
                                      (Coordinates().x - LastPositionForMeasurements.x)
                                    * (Coordinates().x - LastPositionForMeasurements.x)
                                    + (Coordinates().y - LastPositionForMeasurements.y)
                                    * (Coordinates().y - LastPositionForMeasurements.y)
                                    + (Coordinates().z - LastPositionForMeasurements.z)
                                    * (Coordinates().z - LastPositionForMeasurements.z));

                                LastPositionForMeasurements.x = Coordinates().x;
                                LastPositionForMeasurements.y = Coordinates().y;
                                LastPositionForMeasurements.z = Coordinates().z;

                                TotalRangeZXForMeasurements = (float)Math.Sqrt(
                                    LastPositionForMeasurements.x * LastPositionForMeasurements.x
                                    //+ LastPositionForMeasurements.y * LastPositionForMeasurements.y
                                    + LastPositionForMeasurements.z * LastPositionForMeasurements.z);
                            }

                        }
                        else
                        {
                            if (WriteToFile)
                            {
                                SuperWriter();
                                WriteToFile = false;
                            }
                        }

                        //if (!Exploded && TimeOfFlight()>0.01f && Scene.SS.env.player.coords.Reverse_presice_to_map_coords(AbsoluteEstimatedLocation_with_CoordinatesAtTimeAtHighPart()))
                        if (!Exploded && TimeOfFlight() > 0.01f && Scene.SS.env.player.coords.Reverse_presice_to_map_coords(AbsoluteEstimatedLocation_with_CoordinatesAtTimeAtHighPart()))
                        //if (!Exploded && TimeOfFlight() > 0.01f && TimeOfFlight() > AnalyzedTimeOfExplosion)
                        {
                            TimeOfExplosion = TimeOfFlight() + TimePauseUntilExplosion;
                            Keyboard.Wrapped_SINGLE_KeyPressed_Reaction('b');
                            Exploded = true;

                            WriteToFile = true;
                        }

                        
                    }
                }

            }

            static string path = @"MyTest.txt";
            public class Deleter
            {
                
                public Deleter(string path)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception)
                    { }
                }
            }
            Deleter deleter = new Deleter(path);
            bool WriteToFile = false;
            public string delimeter = "\t";
            public void SuperWriter()
            {
                
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.Write("V0.x" + delimeter);
                        sw.Write("V0.y" + delimeter);
                        sw.Write("V0.z" + delimeter);
                        sw.Write("V0" + delimeter);
                        sw.Write("S.x" + delimeter);
                        sw.Write("S.y" + delimeter);
                        sw.Write("S.z" + delimeter);
                        sw.Write("S.XZ" + delimeter);
                        sw.Write("S.Arc" + delimeter);
                        sw.Write("Time" + delimeter);
                        sw.Write("WindVx" + delimeter);
                        sw.Write("WindVy" + delimeter);
                        sw.Write("WindVxy" + delimeter);
                        sw.Write("mass" + delimeter);
                        sw.Write("density" + delimeter);
                        sw.Write("area" + delimeter);
                        sw.Write("cd" + delimeter);
                        sw.Write("Mass of Exp" + delimeter);
                        sw.Write("Mass of Cas" + delimeter);
                        sw.Write("Geom Const" + delimeter);
                        sw.Write("Heat" + delimeter);
                        sw.Write("Exp Velocity" + delimeter);
                        sw.WriteLine("");
                    }
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.Write(sd.Get_Starting_velocity().x + delimeter);
                    sw.Write(sd.Get_Starting_velocity().y + delimeter);
                    sw.Write(sd.Get_Starting_velocity().z + delimeter);
                    sw.Write(sd.StartingVelocity + delimeter);

                    sw.Write(LastPositionForMeasurements.x + delimeter);
                    sw.Write(LastPositionForMeasurements.y + delimeter);
                    sw.Write(LastPositionForMeasurements.z + delimeter);
                    sw.Write(TotalRangeZXForMeasurements + delimeter);
                    sw.Write(TotalFlyingDistanceInAnArcWay + delimeter);
                    sw.Write((TimeOfExplosion - TimePauseUntilExplosion) + delimeter);
                    sw.Write(Projectile.settings.windVx + delimeter);
                    sw.Write(Projectile.settings.windVy + delimeter);
                    sw.Write(Math.Sqrt(Projectile.settings.windVx * Projectile.settings.windVx + Projectile.settings.windVy * Projectile.settings.windVy) + delimeter);
                    sw.Write(Projectile.settings.mass + delimeter);
                    sw.Write(Projectile.settings.density + delimeter);
                    sw.Write(Projectile.settings.area + delimeter);
                    sw.Write(Projectile.settings.cd + delimeter);
                    sw.Write(Projectile.settings.Mc + delimeter);
                    sw.Write(Projectile.settings.Me + delimeter);
                    sw.Write(Projectile.settings.K + delimeter);
                    sw.Write(Projectile.settings.dE + delimeter);
                    sw.Write(DataForDraw_ExplodingList.ExplosionVelocity + delimeter);
                    sw.WriteLine("");
                }
            }

            double AnalyzedTimeOfExplosion = 0;
            public void Launch()
            {
                StartingTimeToFly = Time.time.GetGameTotalSeconds();

                //TimeOfExplosion = float.MaxValue;
                //AnalyzedTimeOfExplosion = double.MaxValue;
                //FindingExplosionTime();

                Launched = true;
            }
            void FindingExplosionTime()
            {
                Projectile.jp.WP.Save_Old_Data();
                Projectile.jp.WP.Reignite();
                vec3 temp;
                double Increment = 0.01;
                double time = 60 / Increment;
                for (int i = 0; i <= time; i++)
                {
                    Projectile.jp.WP.updateLocationAndVelocity(Increment);
                    temp = Projectile.jp.sd.Get_Center() + Projectile.jp.WP.get_vec3_Position();

                    if (i > 100 && Scene.SS.env.player.coords.Reverse_presice_to_map_coords(AbsoluteEstimatedLocation()))
                    {
                        AnalyzedTimeOfExplosion = i * Increment; break;
                    }
                }
                Projectile.jp.WP.Restore_Old_Data();
                Projectile.jp.WP.Reignite();
            }
            
            public void DeLaunch()
            {
                Launched = false;
            }

            vec3 os_x = new vec3(1, 0, 0);
            vec3 os_y = new vec3(1, 0, 0);

            float step = 0.01f;
            public vec3 Velocity(bool Givemenew = false)
            {
                if (!Projectile.settings.AdvancedPhysics && !Givemenew) return(CoordinatesAtTime(TimeOfFlight() + step) - CoordinatesAtTime(TimeOfFlight()))/step;
                else return WP.get_vec3_Velocity();

                //return new vec3(sd.starting_velocity.x, sd.starting_velocity.y - (9.8f) * TimeOfFlight(), sd.starting_velocity.z);

                //coordinates.x = sd.starting_velocity.x* time;
                //coordinates.y = sd.starting_velocity.y * time - (9.8f / 2f) * time * time;
                //coordinates.z = sd.starting_velocity.z * time;
            }
            float vec3_length(vec3 inp)
            {
                return (float)Math.Sqrt(inp.x * inp.x + inp.y * inp.y + inp.z * inp.z);
            }

            public float AngleBetweenStartingAndCurrentVelocity()
            {
                vec3 v1 = sd.Get_Starting_velocity();
                vec3 v2 = Velocity();

                double Inside = (glm.dot(v1, v2)) / (vec3_length(v1) * vec3_length(v2));
                if (Launched)
                {
                    if (Inside > 1.0f) Inside = 0.999999f;
                    if (Inside < -1.0f) Inside = -0.999999f;
                }
                float temp = (float)Math.Acos(Inside);

                if (double.IsNaN(temp))
                {
                    return 0; 
                }
                else return temp;
            }
            double StartingTimeToFly = 0;
            float TimeOfFlight()
            {
                float time = 0;
                if (!Launched) time = 0;
                else time = (float)(Time.time.GetGameTotalSeconds() - StartingTimeToFly);

                if (time > TimeOfExplosion)
                    time = TimeOfExplosion;

                return time;
            }

            vec3 coordinates = new vec3(0, 0, 0);
            public vec3 Coordinates(bool GiveMeNew = false)
            {
                if (!Projectile.settings.AdvancedPhysics && !GiveMeNew) return CoordinatesAtTime(TimeOfFlight());
                else return WP.get_vec3_Position();
            }
            vec3 CoordinatesAtTime(float time)
            {
                coordinates.x = sd.Get_Starting_velocity().x * time;
                coordinates.y = sd.Get_Starting_velocity().y * time - (9.8f / 2f) * time * time;
                coordinates.z = sd.Get_Starting_velocity().z * time;

                return coordinates;
            }
            public float TimeWhenSecondZero()
            {
                // - (9.8f / 2f) * time * time + sd.starting_velocity.y * time + 10 = 0;
                //double Discr = Math.Sqrt(sd.starting_velocity.y* sd.starting_velocity.y);
                //return (float)((-sd.starting_velocity.y - Discr) / (-2 * (9.8f / 2f)));

                double Discr = Math.Sqrt(sd.Get_Starting_velocity().y* sd.Get_Starting_velocity().y + 4 * 10 * (9.8f / 2f));
                return (float)((-sd.Get_Starting_velocity().y - Discr) / (-2 * (9.8f / 2f)));
            }
            public string GetStringedVec3(vec3 inp)
            {
                return inp.x.ToString() + " ; " + inp.y.ToString() + " ; " + inp.z.ToString();
            }
            public mat3 GetProjectileMatrix()
            {
                return new mat3(Coordinates(),
                    PlayerAngles(),
                    sd.Get_Center()
                    );
            }

            vec3 OldVelocity = new vec3(-9999123, -9999123, -9999123);
            public void NotEveryTimeRealoder()
            {
                if (OldVelocity.x != sd.Get_Starting_velocity().x
                    && OldVelocity.y != sd.Get_Starting_velocity().y
                    && OldVelocity.z != sd.Get_Starting_velocity().z)
                {
                    OldVelocity.x = sd.Get_Starting_velocity().x;
                    OldVelocity.y = sd.Get_Starting_velocity().y;
                    OldVelocity.z = sd.Get_Starting_velocity().z;
                    Scene.SS.TrajectoryPath.Reloader();
                }
            }
        }
    }
}
