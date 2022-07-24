using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Все_что_есть
{
    public class RayHit
    {
        public ObjectGame objectHit { get; set; }
        public Vector2 vector;
        public ObjectGame.TupeCollision[] tupeCollision = new ObjectGame.TupeCollision[] { ObjectGame.TupeCollision.None};
        public void GetCollisions(string collision)
        {
            if (string.IsNullOrEmpty(collision)) collision = "None";
            List< ObjectGame.TupeCollision > tupeCollisions = new List< ObjectGame.TupeCollision >();
            string[] collisions = collision.Split(',');
            foreach (string coll in collisions)
                for (int i = 0; i< Enum.GetNames(typeof(ObjectGame.TupeCollision)).Length;i++)
                    if (((ObjectGame.TupeCollision)i).ToString() == coll)
                        tupeCollisions.Add((ObjectGame.TupeCollision)i);
            tupeCollision = tupeCollisions.ToArray();
        }
    }

    public class Ray
    {
        public Vector2 Vector1;
        public Vector2 Vector2;
        public double X_component { get { return Vector2.X - Vector1.X; } }
        public double Y_component { get { return Vector2.Y - Vector1.Y; } }
        public Ray(Vector2 vector1, Vector2 vector2)
        {
            Vector1 = vector1;
            Vector2 = vector2;
        }
        public static bool CheckIsPointOnLineSegment(Point point, Ray line, double epsilon = 0.1)
        {
            var minX = Math.Min(line.Vector1.X, line.Vector2.X);
            var maxX = Math.Max(line.Vector1.X, line.Vector2.X);
            var minY = Math.Min(line.Vector1.Y, line.Vector2.Y);
            var maxY = Math.Max(line.Vector1.Y, line.Vector2.Y);
            if (!(minX <= point.X) || !(point.X <= maxX) || !(minY <= point.Y) || !(point.Y <= maxY))
            {
                return false;
            }
            if (Math.Abs(line.Vector1.X - line.Vector2.X) < epsilon)
            {
                return Math.Abs(line.Vector1.X - point.X) < epsilon || Math.Abs(line.Vector2.X - point.X) < epsilon;
            }
            if (Math.Abs(line.Vector1.Y - line.Vector2.Y) < epsilon)
            {
                return Math.Abs(line.Vector1.Y - point.Y) < epsilon || Math.Abs(line.Vector2.Y - point.Y) < epsilon;
            }
            if (Math.Abs((point.X - line.Vector1.X) / (line.Vector2.X - line.Vector1.X) - (point.Y - line.Vector1.Y) / (line.Vector2.Y - line.Vector1.Y)) < epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Vector2 Vector2onLineByDistance(Ray ray, float Distance)
        {
            Vector2 Vect = Vector2.Zero;
            var Rab = Math.Sqrt((Math.Pow((ray.Vector2.X - ray.Vector1.X), 2) + Math.Pow((ray.Vector2.Y - ray.Vector1.Y), 2)));
            var k = Distance / Rab;
            Vect.X = (float)(ray.Vector1.X + (ray.Vector2.X - ray.Vector1.X) * k);
            Vect.Y = (float)(ray.Vector1.Y + (ray.Vector2.Y - ray.Vector1.Y) * k);
            return Vect;
        }
    }
    internal class Raycast
    {
        bool Visible { get; set; }

        public static Vector2 CursorVec { get { int X, Y; try { X = Game.MainForm.PointToClient(Cursor.Position).X;  Y = Game.MainForm.PointToClient(Cursor.Position).Y; } catch{ return Vector2.Zero; } return new Vector2(X, Y); } }
        public void Draw(Graphics graphics)
        {
            if (Visible)
            {

            }
        }
       
        public static bool RayHitTerrain(Ray Ray, ref RayHit rayHit, ObjectGame Ignor = null)
        {
            rayHit.objectHit = null;
            bool f = false;
            foreach (ObjectGame obj in ObjectGame.ObjectGames)
            {
                foreach(ObjectGame.TupeCollision collision in rayHit.tupeCollision)
                   if(collision == obj.tupeCollision && Ignor != obj)
                    for (int i = 0; i < obj.Vector2.Count - 1; i++)
                    {
                        Vector2 v = LinesIntersection(obj.Vector2[i].PointinVector2(), obj.Vector2[i + 1].PointinVector2(), Ray.Vector1.PointinVector2(), Ray.Vector2.PointinVector2(), out bool hitCrossSegment);
                        if (hitCrossSegment)
                        {
                            if (Vector2.Distance(Ray.Vector1, v) < Vector2.Distance(Ray.Vector1, rayHit.vector))
                            {
                                rayHit.objectHit = obj;
                                rayHit.vector = v;
                                    f = true;
                            }
                        }
                    }
            }
            return f;
        }
        public static Vector2 LinesIntersection(Point p1, Point p2, Point p3, Point p4, out bool hitCrossSegment)
        {
            if (p1.X > p2.X || p1.Y > p2.Y)
            {
                Extensions.Swap(ref p1, ref p2);
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                Extensions.Swap(ref p1, ref p2);
            }
            hitCrossSegment = false;

            double a1 = p2.Y - p1.Y;
            double b1 = p1.X - p2.X;
            double c1 = -p1.X * p2.Y + p1.Y * p2.X;
            double a2 = p4.Y - p3.Y;
            double b2 = p3.X - p4.X;
            double c2 = -p3.X * p4.Y + p3.Y * p4.X;
            float X = (float)((b1 * c2 - b2 * c1) / (a1 * b2 - a2 * b1));
            float Y = (float)((a2 * c1 - a1 * c2) / (a1 * b2 - a2 * b1));

            if (p1.Y == p2.Y)
            {
                if ((p1.X <= X) && (p2.X >= X))
                {
                    if (((p3.Y <= Y) && (p4.Y >= Y) || (p4.Y <= Y) && (p3.Y >= Y)))
                        if ((p3.X <= X) && (p4.X >= X) || (p4.X <= X) && (p3.X >= X))
                            hitCrossSegment = true;
                }
            }
            else if ((p1.Y <= Y) && (p2.Y >= Y))
            {
                if ((p3.X <= X) && (p4.X >= X) || (p4.X <= X) && (p3.X >= X))
                    if (((p3.Y <= Y) && (p4.Y >= Y) || (p4.Y <= Y) && (p3.Y >= Y)))
                    {
                        hitCrossSegment = true;
                    }
            }
            // if ((a1 * b2 - a2 * b1) == 0)
            //{
            //  ---  || ---
            // }
            return new Vector2(X, Y);
        }
    }
}
