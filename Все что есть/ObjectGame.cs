
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Все_что_есть
{
    public class ObjectGame : Transform
    {
        static List<ObjectGame> objectGames = new List<ObjectGame>();
        public static List<ObjectGame> ObjectGames { get { return objectGames; } }
        public Pen pen { get; set; }
        
        public bool IsVisible { get; set; }
        public List<Vector2> Vector2
        {
            get
            {
                List<Vector2> list = new List<Vector2>();
                foreach (Point point in points)
                {
                    list.Add(new Point(point.X + Locate.X, point.Y + Locate.Y).Vector2inPoint());
                }
                return list;
            }
        }
        public static Point[] GetPointCube(int X, int Y)
        {
            return new Point[] { Point.Empty, new Point(X, 0), new Point(X, Y), new Point(0, Y) };
        }
        public TupeObject tupeObject { get; }
        public TupeCollision tupeCollision { get; }
        public Color ColorObj { get; set; }
        public ObjectGame()
        { }
        public ObjectGame(Point Location, Point[] Rect, TupeObject tupe, Color color, TupeCollision _tupeCollision = TupeCollision.Terrain)
        {
            pen = new Pen(color);
            Locate = Location;
            points = Rect.ToList();
            if (TupeObject.Rectangle == tupe || TupeObject.FillRectangle == tupe)
                points.Add(points[0]);
            tupeObject = tupe;
            ColorObj = color;
            tupeCollision = _tupeCollision;
            IsVisible = true;
            objectGames.Add(this);
            thisObject = this;
        }
        public void DeleteObject()
        {
            objectGames.Remove(this);
        }
        public void AddObject()
        {
            objectGames.Add(this);
        }
        public override string ToString()
        {
            return $"Points count: {points.Count} TypeObject: {tupeObject}, CollisionType: {tupeCollision}";
        }
        public ObjectGame Clone()
        {
            ObjectGame clone = new ObjectGame();
            clone.points = points;
            clone.Size = Size;
            clone.Locate = Locate;
            clone.ColorObj = ColorObj;
            clone.SetCollision = SetCollision;
            clone.IsVisible = IsVisible;
            return clone;

        }
        public enum TupeObject
        {
            Elleps = 1,
            Lines,
            Rectangle,
            FillRectangle
        }
        public enum TupeCollision
        {
            None = 0,
            Terrain,
            Texture,
            Item,
            Player
        }
    }
}
