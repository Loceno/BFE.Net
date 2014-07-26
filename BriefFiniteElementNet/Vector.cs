﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BriefFiniteElementNet
{
    /// <summary>
    /// Represents a vector in 3D space
    /// </summary>
    [Serializable]
    public struct Vector :ISerializable
    {
        #region Properties

        private double x;
        private double y;
        private double z;


        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x component.
        /// </value>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y component.
        /// </value>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        /// <value>
        /// The z component.
        /// </value>
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public double Length
        {
            get { return System.Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        #endregion

        public Vector(double x, double y, double z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

       


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var buf = string.Format("{0}, {1}, {2}",
                                    MathUtil.Equals(X, 0) && X != 0 ? "~0" : (object)X,
                                    MathUtil.Equals(Y, 0) && Y != 0 ? "~0" : (object)Y,
                                    MathUtil.Equals(Z, 0) && Z != 0 ? "~0" : (object)Z);
            return buf;
        }


        #region Statics

        /// <summary>
        /// Froms the xyz.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns></returns>
        public static Vector FromXYZ(double x, double y, double z)
        {
            return new Vector(x, y, z);
        }

        /// <summary>
        /// Froms the point.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns></returns>
        public static Vector FromPoint(Point pt)
        {
            return new Vector(pt.X, pt.Y, pt.Z);
        }

        
        #region Properties

        /**/
        /// <summary>
        /// Gets a zero Vector.
        /// </summary>
        public static Vector Zero
        {
            get { return new Vector(0, 0, 0); }
        }

        /// <summary>
        /// Gets a unit Vector in x direction.
        /// </summary>
        public static Vector I
        {
            get { return new Vector(1, 0, 0); }
        }

        /// <summary>
        /// Gets a unit Vector in y direction.
        /// </summary>
        public static Vector J
        {
            get { return new Vector(0, 1, 0); }
        }

        /// <summary>
        /// Gets a unit Vector in z direction.
        /// </summary>
        public static Vector K
        {
            get { return new Vector(0, 0, 1); }
        }

        /// <summary>
        /// Gets a unit Vector in x direction (negative).
        /// </summary>
        public static Vector NegativeI
        {
            get { return new Vector(-1, 0, 0); }
        }

        /// <summary>
        /// Gets a unit Vector in y direction (negative).
        /// </summary>
        public static Vector NegativeJ
        {
            get { return new Vector(0, -1, 0); }
        }

        /// <summary>
        /// Gets a unit Vector in z direction (negative).
        /// </summary>
        public static Vector NegativeK
        {
            get { return new Vector(0, 0, -1); }
        }

        /// <summary>
        /// Gets the unit scale (1,1,1).
        /// </summary>
        public static Vector UnitScale
        {
            get { return new Vector(1, 1, 1); }
        }

        /**/
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the dot product.
        /// </summary>
        /// <param name="p">The that.</param>
        /// <returns></returns>
        public double Dot(Vector p)
        {
            return this.X * p.X + this.Y * p.Y + this.Z * p.Z;
        }

        /// <summary>
        /// Gets the cross product (this x that)
        /// </summary>
        /// <param name="that">The that.</param>
        /// <returns></returns>
        public Vector Cross(Vector that)
        {
            return new Vector(this.Y * that.Z - this.Z * that.Y,
                                this.Z * that.X - this.X * that.Z,
                                this.X * that.Y - this.Y * that.X);
        }

        /// <summary>
        /// Gets a Vector unit in length and in this direction.
        /// </summary>
        /// <returns></returns>
        public Vector GetUnit()
        {
            var length = this.GetLength();

            if (length == 0)
            {
                throw new DivideByZeroException("Can not normalize a vector when it's magnitude is zero.");
            }

            return this / length;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        private double GetLength()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="v1">The v.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// v+v2
        /// </returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="v1">The v.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// v-v2
        /// </returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>
        /// -v
        /// </returns>
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="c">The c.</param>
        /// <returns>
        /// c*v
        /// </returns>
        public static Vector operator *(Vector v, double c)
        {
            return new Vector(v.X * c, v.Y * c, v.Z * c);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="v">The v.</param>
        /// <returns>
        /// c*v
        /// </returns>
        public static Vector operator *(double c, Vector v)
        {
            return new Vector(v.X*c, v.Y*c, v.Z*c);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="c">The c.</param>
        /// <returns>
        /// v*1/c
        /// </returns>
        public static Vector operator /(Vector v, double c)
        {
            return new Vector(v.X / c, v.Y / c, v.Z / c);
        }

        #endregion


        /// <summary>
        /// Calculates the cross product of two vectors
        /// </summary>
        /// <param name="v1">V1</param>
        /// <param name="v2">V2</param>
        /// <returns>V1 X V2</returns>
        public static Vector Cross(Vector v1, Vector v2)
        {
            return new Vector(v1.y*v2.z - v1.z*v2.y, v1.z*v2.x - v2.z*v1.x, v1.x*v2.y - v1.y*v2.x);
        }

        #region Serialization stuff

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("x", x);
            info.AddValue("y", y);
            info.AddValue("z", z);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector" /> struct.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Vector(SerializationInfo info, StreamingContext context)
        {
            x = info.GetDouble("x");
            y = info.GetDouble("y");
            z = info.GetDouble("z");
        }

        #endregion


        public static implicit operator Point(Vector v)
        {
            return new Point(v.x, v.y, v.z);
        }

        public static implicit operator Vector(Point v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }
    }
}
