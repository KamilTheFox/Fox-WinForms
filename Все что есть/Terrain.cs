using System.Numerics;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Все_что_есть
{
    public class Terrain : IGrafics
    {
        public void Update()
        {

        }
        public void Draw(Graphics graphics)
        {
            try
            {
                foreach (ObjectGame obj in ObjectGame.ObjectGames)
                {
                    if (obj.IsVisible)
                    {
                        List<Point> points = new List<Point>();
                        for (int i = 0; i < obj.Vector2.Count; i++)
                        {
                            points.Add(obj.Vector2[i].PointinVector2());
                        }
                        switch (obj.tupeObject)
                        {
                            case ObjectGame.TupeObject.Elleps:
                                graphics.DrawEllipse(obj.pen, obj.Locate.X, obj.Locate.Y, obj.points[0].X, obj.points[0].Y);
                                break;
                            case ObjectGame.TupeObject.Lines:
                                graphics.DrawLines(obj.pen, points.ToArray());
                                break;
                            case ObjectGame.TupeObject.FillRectangle:
                                graphics.FillPolygon(new SolidBrush(obj.ColorObj), points.ToArray());
                                break;
                            case ObjectGame.TupeObject.Rectangle:
                                graphics.DrawPolygon(obj.pen, points.ToArray());
                                break;

                        }
                    }

                }
            }
            catch
            { }
        }

    }
}
