using System;
using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using System.Linq;

namespace SpaceDefence
{

    public class LinePieceCollider : Collider, IEquatable<LinePieceCollider>
    {

        public Vector2 Start;
        public Vector2 End;

        /// <summary>
        /// The length of the LinePiece, changing the length moves the end vector to adjust the length.
        /// </summary>
        public float Length 
        { 
            get { 
                return (End - Start).Length(); 
            } 
            set {
                End = Start + GetDirection() * value; 
            }
        }

        /// <summary>
        /// The A component from the standard line formula Ax + By - C = 0
        /// </summary>
        public float StandardA
        {
            get
            {
                return GetDirection().Y;
            }
        }

        /// <summary>
        /// The B component from the standard line formula Ax + By + C = 0
        /// </summary>
        public float StandardB
        {
            get
            {
                return GetDirection().X;
            }
        }

        /// <summary>
        /// The C component from the standard line formula Ax + By + C = 0
        /// </summary>
        public float StandardC
        {
            get
            {
                return Start.Y - (StandardA * Start.X);
            }
        }

        public LinePieceCollider(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
        
        public LinePieceCollider(Vector2 start, Vector2 direction, float length)
        {
            Start = start;
            End = start + direction * length;
        }

        /// <summary>
        /// Should return the angle between a given direction and the up vector.
        /// </summary>
        /// <param name="direction">The Vector2 pointing out from (0,0) to calculate the angle to.</param>
        /// <returns> The angle in radians between the the up vector and the direction to the cursor.</returns>
        public static float GetAngle(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X) + (float) Math.PI / 2.0f;
        }


        /// <summary>
        /// Calculates the normalized vector pointing from point1 to point2
        /// </summary>
        /// <returns> A Vector2 containing the direction from point1 to point2. </returns>
        public static Vector2 GetDirection(Vector2 point1, Vector2 point2)
        {
            var direction = point2 - point1;
            direction.Normalize();
            return direction;
        }


        /// <summary>
        /// Gets whether or not the Line intersects another Line
        /// </summary>
        /// <param name="other">The Line to check for intersection</param>
        /// <returns>true there is any overlap between the two lines.</returns>
        public override bool Intersects(LinePieceCollider other)
        {
            var intersect = GetIntersection(other);
            if (intersect == default) return false;
            return Contains(intersect) && other.Contains(intersect);
        }


        /// <summary>
        /// Gets whether or not the line intersects a Circle.
        /// </summary>
        /// <param name="other">The Circle to check for intersection.</param>
        /// <returns>true if line intersects circle, otherwise false.</returns>
        public override bool Intersects(CircleCollider other)
        {
            var pq = End - Start;
            var pc = other.Center - Start;
            var dist = Math.Clamp(Vector2.Dot(pq, pc) / Math.Pow(pq.Length(), 2), 0.0f, 1.0f);
            var near = Start + (pq * (float)dist);
            if (other.Contains(near)) return true;
            return false;
        }

        /// <summary>
        /// Gets whether or not the Line intersects the Rectangle.
        /// </summary>
        /// <param name="other">The Rectangle to check for intersection.</param>
        /// <returns>true there is any overlap between the Line and the Rectangle.</returns>
        public override bool Intersects(RectangleCollider other)
        {
            if(other.Contains(NearestPointOnLine(other.shape.Center.ToVector2()))) return true;

            return false;
        }

        /// <summary>
        /// Calculates the intersection point between 2 lines.
        /// </summary>
        /// <param name="Other">The line to intersect with</param>
        /// <returns>A Vector2 with the point of intersection.</returns>
        public Vector2 GetIntersection(LinePieceCollider other)
        {
            var divisor = StandardA * other.StandardB - StandardB * other.StandardA;
            if (divisor == 0) return default;
            var intersect = new Vector2(
                (StandardB * other.StandardC - StandardC * other.StandardB) / divisor,
                (StandardC * other.StandardA - StandardA * other.StandardC) / divisor 
            );
            return intersect;
        }

        /// <summary>
        /// Finds the nearest point on a line to a given vector, taking into account if the line is .
        /// </summary>
        /// <param name="other">The Vector you want to find the nearest point to.</param>
        /// <returns>The nearest point on the line.</returns>
        public Vector2 NearestPointOnLine(Vector2 other)
        {
            var direction = GetDirection();
            direction.Normalize();
            Vector2 lhs = other - Start;

            return Start + direction * Math.Clamp(Vector2.Dot(lhs, direction), 0.0f, Length);
        }

        /// <summary>
        /// Returns the enclosing Axis Aligned Bounding Box containing the control points for the line.
        /// As an unbound line has infinite length, the returned bounding box assumes the line to be bound.
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetBoundingBox()
        {
            Point topLeft = new Point((int)Math.Min(Start.X, End.X), (int)Math.Min(Start.Y, End.Y));
            Point size = new Point((int)Math.Max(Start.X, End.X), (int)Math.Max(Start.Y, End.X)) - topLeft;
            return new Rectangle(topLeft,size);
        }


        /// <summary>
        /// Gets whether or not the provided coordinates lie on the line.
        /// </summary>
        /// <param name="coordinates">The coordinates to check.</param>
        /// <returns>true if the coordinates are within the circle.</returns>
        public override bool Contains(Vector2 coordinates)
        {
            var thisDiff = coordinates - Start;

            var t = Vector2.Dot(GetDirection(), thisDiff);

            // Check if the projection is within the bounds of the line segment
            if (t < 0 || t > Length)
            {
                return false;
            }
            var perpendicularDistance = Math.Abs(Vector2.Dot(new Vector2(-GetDirection().Y, GetDirection().X), thisDiff));
            return perpendicularDistance < 0.001f;
        }

        public bool Equals(LinePieceCollider other)
        {
            return other.Start == this.Start && other.End == this.End;
        }

        /// <summary>
        /// Calculates the normalized vector pointing from point1 to point2
        /// </summary>
        /// <returns> A Vector2 containing the direction from point1 to point2. </returns>
        public static Vector2 GetDirection(Point point1, Point point2)
        {
            return GetDirection(point1.ToVector2(), point2.ToVector2());
        }


        /// <summary>
        /// Calculates the normalized vector pointing from point1 to point2
        /// </summary>
        /// <returns> A Vector2 containing the direction from point1 to point2. </returns>
        public Vector2 GetDirection()
        {
            return GetDirection(Start, End);
        }


        /// <summary>
        /// Should return the angle between a given direction and the up vector.
        /// </summary>
        /// <param name="direction">The Vector2 pointing out from (0,0) to calculate the angle to.</param>
        /// <returns> The angle in radians between the the up vector and the direction to the cursor.</returns>
        public float GetAngle()
        {
            return GetAngle(GetDirection());
        }
    }
}
