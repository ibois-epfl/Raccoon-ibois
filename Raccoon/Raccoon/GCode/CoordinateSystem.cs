﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Raccoon.GCode
{
    public static class CoordinateSystem
    {
        public static Plane ABPlane(this Point3d pt, double A, double B)
        {
            Plane f1 = new Plane(pt, pt + new Vector3d(-1, 0, 0), pt + new Vector3d(0, -1, 0)); //horizontal plane of machine
            f1.Rotate((Math.PI / 180) * A, f1.ZAxis);
            f1.Rotate((Math.PI / 180) * (B * -1), f1.XAxis);
            return f1;
        }

        /// <summary>
        /// Convert Cartesian into Polar for NC, no Inversion on B
        /// A = Rotation around World-Z
        /// B = Rotation around World-X
        /// </summary>
        /// <param name="n0"></param>
        /// <returns></returns>
        public static Tuple<double, double, string> AB180(Vector3d n0, bool flipAXis = false)
        {
            double a = Math.Atan2(n0.X, n0.Y);
            double b = Math.Atan2(Math.Sqrt(n0.X * n0.X + n0.Y * n0.Y), n0.Z);

            double A = Math.Round(-1 * Rhino.RhinoMath.ToDegrees(a), 3);
            double B = Math.Round(-1 * Rhino.RhinoMath.ToDegrees(b), 3);

            if (flipAXis)
            {
                double sFlip = (Math.Abs(A - 180) < Math.Abs(A + 180)) ? -1 : 1;
                A += 180 * sFlip;
                B *= -1;
            }

            //Minimize A rotation
            if (Math.Abs(A - 180) < Math.Abs(A) || Math.Abs(A + 180) < Math.Abs(A))
            {
                double sFlip = (Math.Abs(A - 180) < Math.Abs(A + 180)) ? -1 : 1;
                A += 180 * sFlip;
                B *= -1;
            }

            if (B > 100.00 || B < -100.00)
                Rhino.RhinoApp.WriteLine("Not Reachable");

            string strAB = " " + Raccoon_Library.Axes.A + (A).ToString() + " " + Raccoon_Library.Axes.B + B.ToString();

            return new Tuple<double, double, string>(A, B, strAB);
        }

        public static Tuple<double, double, string> AB180(Plane plane, bool flipAXis = false)
        {
            Vector3d n0 = plane.ZAxis;
            Point3d pt = plane.Origin;

            double a = Math.Atan2(n0.X, n0.Y);
            double b = Math.Atan2(Math.Sqrt(n0.X * n0.X + n0.Y * n0.Y), n0.Z);

            double A = Math.Round(-1 * Rhino.RhinoMath.ToDegrees(a), 3);
            double B = Math.Round(-1 * Rhino.RhinoMath.ToDegrees(b), 3);

            Plane f1 = new Plane(pt, pt + new Vector3d(-1, 0, 0), pt + new Vector3d(0, -1, 0)); //horizontal plane of machine
            f1.Rotate((Math.PI / 180) * A, f1.ZAxis);
            f1.Rotate((Math.PI / 180) * (-B), f1.XAxis);

            //Measure the angle betweet initial plane xaxis and target one
            double angle = Vector3d.VectorAngle(plane.XAxis, f1.XAxis);
            //Rhino.RhinoApp.WriteLine(angle.ToString());

            if (angle > Math.PI * 0.5)
            {
                double sFlip = (Math.Abs(A - 180) < Math.Abs(A + 180)) ? -1 : 1;
                A += 180 * sFlip;
                B *= -1;

                //f1 = new Plane(pt, pt + new Vector3d(-1, 0, 0), pt + new Vector3d(0, -1, 0)); //horizontal plane of machine
                //f1.Rotate((Math.PI / 180) * A, f1.ZAxis);
                //f1.Rotate((Math.PI / 180) * (-B), f1.XAxis);
            }

            //Minimize A rotation
            if (Math.Abs(A - 180) < Math.Abs(A) || Math.Abs(A + 180) < Math.Abs(A))
            {
                double sFlip = (Math.Abs(A - 180) < Math.Abs(A + 180)) ? -1 : 1;
                A += 180 * sFlip;
                B *= -1;
            }

            if (B > 100.00 || B < -100.00)
                Rhino.RhinoApp.WriteLine("Not Reachable");

            string strAB = " " + Raccoon_Library.Axes.A + (A).ToString() + " " + Raccoon_Library.Axes.B + B.ToString();
            return new Tuple<double, double, string>(A, B, strAB);
        }

        /// <summary>
        /// Returns coordinates of a point in G-Code format (ISO6983)
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public static string Pt2nc(Point3d pt, int round = 3, string whitespace = " ")
        {
            return whitespace + "X" + Math.Round(pt.X, round).ToString() + " Y" + Math.Round(pt.Y, round).ToString() + " Z" + Math.Round(pt.Z, round).ToString();
        }

        public static string Pt2nc2D(Point3d pt, int round = 3, string whitespace = " ")
        {
            return whitespace + "X" + Math.Round(pt.X, round).ToString() + " Y" + Math.Round(pt.Y, round).ToString();
        }

        /// <summary>
        /// Returns coordinates of a point in G-Code format (ISO6983)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static string Pt2nc(double x, double y, double z)
        {
            return "X" + Math.Round(x, 3).ToString() + " Y" + Math.Round(y, 3).ToString() + " Z" + Math.Round(z, 3).ToString();
        }

        public static string Pt2nc(Point3d p)
        {
            return " X" + Math.Round(p.X, 3).ToString() + " Y" + Math.Round(p.Y, 3).ToString() + " Z" + Math.Round(p.Z, 3).ToString();
        }
    }
}