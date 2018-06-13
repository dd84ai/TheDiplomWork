using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public class SimpleProjectile : ODE
    {

        public static double G = -9.81;
        public SimpleProjectile(double x0, double y0, double z0,
        double vx0, double vy0, double vz0,
        double time) : base(6)
        {
            setS(time);
            setQ(vx0, 0);
            setQ(x0, 1);
            setQ(vy0, 2);
            setQ(y0, 3);
            setQ(vz0, 4);
            setQ(z0, 5);
        }
        public void Reiniting_StartingPositionAndVelocity(double x0, double y0, double z0,
        double vx0, double vy0, double vz0,
        double time)
        {

            setS(time);
            setQ(vx0, 0);
            setQ(x0, 1);
            setQ(vy0, 2);
            setQ(y0, 3);
            setQ(vz0, 4);
            setQ(z0, 5);
        }

        public double getVx()
        {
            return getQ(0);
        }
        public double getVy()
        {
            return getQ(2);
        }
        public double getVz()
        {
            return getQ(4);
        }

        public double getX()
        {
            return getQ(1);
        }
        public double getY()
        {
            return getQ(3);
        }
        public double getZ()
        {
            return getQ(5);
        }

        public double getTime()
        {
            return getS();
        }


        public void updateLocationAndVelocity(double dt)
        {
            double time = getS();
            double vx0 = getQ(0);
            double x0 = getQ(1);
            double vy0 = getQ(2);
            double y0 = getQ(3);
            double vz0 = getQ(4);
            double z0 = getQ(5);


            double x = x0 + vx0 * dt;
            double y = y0 + vy0 * dt;
            double vz = vz0 + G * dt;
            double z = z0 + vz0 * dt + 0.5 * G * dt * dt;

            time = time + dt;

            setS(time);
            setQ(x, 1);
            setQ(y, 3);
            setQ(vz, 4);
            setQ(z, 5);
        }

        public override double[] getRightHandSide(double s, double[] Q,
        double[] deltaQ, double ds, double qScale)
        {
            return new double[1];
        }


        double[] q_old = new double[6];
        public double time_old = 0;
        public void Save_Old_Data()
        {
            for (int i = 0; i < 6; i++) q_old[i] = getQ(i);
            time_old = getTime();
        }
        public void Restore_Old_Data()
        {
            for (int i = 0; i < 6; i++) setQ(q_old[i], i);
            setS(time_old);
        }
        public vec3 get_vec3_Position()
        {
            return new vec3((float)getQ(1), (float)getQ(5), (float)getQ(3));
        }
        public vec3 get_vec3_Velocity()
        {
            return new vec3((float)getQ(0), (float)getQ(4), (float)getQ(2));
        }
        public vec3 get_vec3_Predicted_Position(double dt)
        {
            Save_Old_Data();
            updateLocationAndVelocity(dt);
            vec3 temp = new vec3((float)getQ(1), (float)getQ(5), (float)getQ(3));
            Restore_Old_Data();
            return temp;
        }
        public void Reignite()
        {
            Reiniting_StartingPositionAndVelocity(0, 0, 0, Projectile.jp.sd.Get_Starting_velocity().x, Projectile.jp.sd.Get_Starting_velocity().z, Projectile.jp.sd.Get_Starting_velocity().y, 0);
        }
        public vec3 get_vec3_Predicted_Position_NotDepenedToTime(double dt)
        {
            Save_Old_Data();

            updateLocationAndVelocity(dt);
            vec3 temp = new vec3((float)getQ(1), (float)getQ(5), (float)getQ(3));
            Restore_Old_Data();
            return temp;
        }
        public vec3 get_vec3_Predicted_Velocity(double dt)
        {
            Save_Old_Data();
            updateLocationAndVelocity(dt);
            vec3 temp = new vec3((float)getQ(0), (float)getQ(4), (float)getQ(2));
            Restore_Old_Data();
            return temp;
        }
    }
}
