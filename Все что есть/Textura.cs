using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Все_что_есть
{
    class Texture : ObjectGame
    {
        public Texture(Point location, Point[] line,Color color, TupeObject tupeObject = TupeObject.Lines) : base(location, line, tupeObject,color,TupeCollision.Texture)
        {

        }
        public Texture(Point location, Point size, Color color, TupeObject tupeObject = TupeObject.FillRectangle) : base(location, GetPointCube(size.X, size.Y), tupeObject, color, TupeCollision.Texture)
        {

        }

    }
}
