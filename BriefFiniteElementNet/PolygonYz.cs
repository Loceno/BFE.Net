﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BriefFiniteElementNet
{
    /// <summary>
    /// Represents a polygon in Y-Z plane.
    /// Points are not editable.
    /// </summary>
    [Serializable]
    public sealed class PolygonYz : IEnumerable<PointYz>,ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonYz"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolygonYz(params PointYz[] points)
        {
            this.points = new List<PointYz>(points);
        }

        /// <summary>
        /// Gets the <see cref="PointYz"/> with the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="PointYz"/>.
        /// </value>
        /// <param name="i">The index.</param>
        /// <returns></returns>
        public PointYz this[int i]
        {
            get { return points[i]; }
        }

        /// <summary>
        /// Gets the count of points in this polygon.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return points.Count; }
        }

        private List<PointYz> points;

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<PointYz> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        /// <summary>
        /// Gets the section geometrical properties.
        /// </summary>
        /// <returns>An array containing geometrical properties of section</returns>
        /// <remarks>
        /// This is the order of returned array: [Iz,Iy,J,A,Ay,Az]
        /// Note: Ay and Az are spotted as same value as A (e.g. Ay = A and Az = A)
        /// </remarks>
        public double[] GetSectionGeometricalProperties()
        {
            var lastPoint = this.points[this.points.Count];

            if (lastPoint != points[0])
                throw new InvalidOperationException("First point and last point ot PolygonYz should put on each other");

            var l = points.Count - 1;

            double a = 0.0, iy = 0.0, ix = 0.0, ixy = 0.0;

            var x = new double[l + 1];
            var y = new double[l + 1];

            for (int i = l; i >= 0; i--)
            {
                x[i] = points[i].Y;
                y[i] = points[i].Z;
            }

            for (var i = 0; i < l; i++)
            {
                a += x[i]*y[i + 1] - x[i + 1]*y[i];
                ix += (x[i + 1] - x[i]) * (y[i + 1] + y[i]) * (y[i + 1] * y[i + 1] + y[i] * y[i]);
                iy += (y[i + 1] - y[i]) * (x[i + 1] + x[i]) * (x[i + 1] * x[i + 1] + x[i] * x[i]);
                ixy += (x[i] = x[i + 1])*
                       (3*x[i + 1]*y[i + 1]*y[i + 1] + x[i]*y[i + 1]*y[i + 1] + 2*x[i + 1]*y[i]*y[i + 1] +
                        2*x[i]*y[i]*y[i + 1] + x[i + 1]*y[i]*y[i] + 3*x[i]*y[i]*y[i]);
            }

            var buf = new double[] {1/12.0*ix, 1/12.0*iy, 1/24.0*ixy, 0.5*a, 0.5*a, 0.5*a};

            if (a < 0)
                for (var i = 0; i < 6; i++)
                    buf[i] = -buf[i];

            
            return buf;
        }


        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("points", points);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonYz"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        private PolygonYz(SerializationInfo info, StreamingContext context)
        {
            points = info.GetValue<List<PointYz>>("points");
        }
    }
}