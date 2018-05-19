﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class SimpleProjectile : ODE
    {
        // Gravitational acceleration.
        public static double G = -9.81;
        public SimpleProjectile(double x0, double y0, double z0,
        double vx0, double vy0, double vz0,
        double time) : base(6)
        {
            // Call the ODE class constructor.
            //base. super(6);
            // Load the initial position, velocity, and time
            // values into the s field and q array from the
            // ODE class.
            setS(time);
            setQ(vx0, 0);
            setQ(x0, 1);
            setQ(vy0, 2);
            setQ(y0, 3);
            setQ(vz0, 4);
            setQ(z0, 5);
        }
        //    The SimpleProjectile class declares a series of methods to return the current location,
        //velocity, and time values for the projectile.Since these quantities are stored in the s field and
        //q[] array of the ODE class, the get methods in the SimpleProjectile class simply call the getS or
        //getQ methods from the ODE class.
        // These methods return the location, velocity,
        // and time values.
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
        // This method updates the velocity and position
        // of the projectile according to the gravity-only model.
        public void updateLocationAndVelocity(double dt)
        {
            // Get current location, velocity, and time values
            // from the values stored in the ODE class.
            double time = getS();
            double vx0 = getQ(0);
            double x0 = getQ(1);
            double vy0 = getQ(2);
            double y0 = getQ(3);
            double vz0 = getQ(4);
            double z0 = getQ(5);
            // Update the xyz locations and the z-component
            // of velocity. The x- and y-velocities don't change.
            double x = x0 + vx0 * dt;
            double y = y0 + vy0 * dt;
            double vz = vz0 + G * dt;
            double z = z0 + vz0 * dt + 0.5 * G * dt * dt;
            // Update time;
            time = time + dt;
            // Load new values into ODE arrays and fields.
            setS(time);
            setQ(x, 1);
            setQ(y, 3);
            setQ(vz, 4);
            setQ(z, 5);
        }
        //    Because the SimpleProjectile class is a subclass of ODE, it has to provide an implemen-
        //tation of the getRightHandSide method.If you remember from the previous chapter, this
        //method is used to compute the right-hand side of the ODEs that will be solved.The
        //SimpleProjectile class doesn’t solve any ODEs, so the method is written to return a dummy
        //array.
        // Because SimpleProjectile extends the ODE class,
        // it must implement the getRightHandSide method.
        // In this case, the method returns a dummy array.
        public override double[] getRightHandSide(double s, double[] Q,
        double[] deltaQ, double ds, double qScale)
        {
            return new double[1];
        }
    }
}
