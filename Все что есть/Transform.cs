using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Все_что_есть
{
    public abstract class Transform
    {
        public Transform thisTransform { get { return this; }}
        public Point Locate;
        public Rectangle Rect { get { return new Rectangle(Locate.X, Locate.Y, Size.X, Size.Y); } }
        public Point Size { get; set; }
        public List<Point> points;
        public Vector2 Location { get { return new Vector2(Locate.X + Size.X / 2, Locate.Y + Size.Y / 2); } set { Locate = new Point((int)(value.X - Size.X / 2), (int)(value.Y - Size.Y / 2)); } }
        public ObjectGame thisObject;
        public string SetCollision { get; set; }
        public bool Collision(out ObjectGame tupe)
        {
            tupe = null;
            RayHit hit = new RayHit();
            hit.GetCollisions(SetCollision);
            bool flag = false;
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (Raycast.RayHitTerrain(new Ray(Locate.Vector2inPoint() + points[i].Vector2inPoint(), Locate.Vector2inPoint() +points[i + 1].Vector2inPoint()), ref hit, thisObject))
                {
                    tupe = hit.objectHit;
                    flag = true;
                    if (tupe != null)
                        break;
                }
            }
            return flag;

        }
        public bool Collision() => Collision(out _);
    }
}
