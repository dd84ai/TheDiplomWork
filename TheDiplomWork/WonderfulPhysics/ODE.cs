using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public abstract class ODE
    {
        // Declare fields used by the class
        private int numEqns; // number of equations to solve
        private double[] q; // array of dependent variables
        private double s; // independent variable
                          // Constructor
        public ODE(int numEqns)
        {
            this.numEqns = numEqns;
            q = new double[numEqns];
        }
        //        The ODE class declares a standard series of get/set methods to return or change the values
        //of the fields declared in the class.
        // These methods return the number of equations or
        // the value of the dependent or independent variables.
        public int getNumEqns()
        {
            return numEqns;
        }
        public double getS()
        {
            return s;
        }
        public double getQ(int index)
        {
            return q[index];
        }
        public double[] getAllQ()
        {
            return q;
        }
        // These methods change the value of the dependent
        // or independent variables.
        public void setS(double value)
        {
            s = value;
            return;
        }
        public void setQ(double value, int index)
        {
            q[index] = value;
            return;
        }
        // This method returns the right-hand side of the
        // ODEs. It is declared abstract to force subclasses
        // to implement their own version of the method.
        public abstract double[] getRightHandSide(double s,
        double[] q, double[] deltaQ, double ds, double qScale);
    }

    public class ODESolver
    {
        // Fourth-order Runge-Kutta ODE solver.
        public static void rungeKutta4(ODE ode, double ds)
        {
            // Define some convenience variables to make the
            // code more readable
            int j;
            int numEqns = ode.getNumEqns();
            double s;
            double[] q;
            double[] dq1 = new double[numEqns];
            double[] dq2 = new double[numEqns];
            double[] dq3 = new double[numEqns];
            double[] dq4 = new double[numEqns];
            // Retrieve the current values of the dependent
            // and independent variables.
            s = ode.getS(); // independent variable
            q = ode.getAllQ(); // dependent variables
            //The method computes the four estimates for Δq according to Equations(4.23a) through
            //(4.23d) by calling the getRightHandSide method on the ODE object.The arguments to the
            //getRightHandSide method define where the dependent and independent variables are evalu -
            //ated for each step (qn + 1 / 2Δq1, for example).
              // Compute the four Runge-Kutta steps. The return
              // value of getRightHandSide method is an array of
              // delta-q values for each of the four steps.
              dq1 = ode.getRightHandSide(s, q, q, ds, 0.0);
            dq2 = ode.getRightHandSide(s + 0.5 * ds, q, dq1, ds, 0.5);
            dq3 = ode.getRightHandSide(s + 0.5 * ds, q, dq2, ds, 0.5);
            dq4 = ode.getRightHandSide(s + ds, q, dq3, ds, 1.0);
            //Once the four estimates for Δq have been computed, the update to the dependent variables
            //can be calculated according to Equation(4.23e).The value of the independent variable, s, is
            //incremented to its new value.
            // Update the dependent and independent variable values
            // at the new dependent variable location and store the
            // values in the ODE object arrays.
            ode.setS(s + ds);
            for (j = 0; j < numEqns; ++j)
            {
                q[j] = q[j] + (dq1[j] + 2.0 * dq2[j] + 2.0 * dq3[j] + dq4[j]) / 6.0;
                ode.setQ(q[j], j);
            }
            return;
        }
    }
}
