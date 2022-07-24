using System.Numerics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Все_что_есть
{
    public static class Extensions
    {
      
        public static bool CheckPointInObject(this ObjectGame game, Point point)
        {
            bool result = false;
            int j = game.Vector2.Count - 1;
            for (int i = 0; i < game.Vector2.Count; i++)
            {
                if (game.Vector2[i].Y < point.Y && game.Vector2[j].Y >= point.Y || game.Vector2[j].Y < point.Y && game.Vector2[i].Y >= point.Y)
                {
                    if (game.Vector2[i].X + (point.Y - game.Vector2[i].Y) / (game.Vector2[j].Y - game.Vector2[i].Y) * (game.Vector2[j].X - game.Vector2[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        public static Vector2 Vector2inPoint(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
        public static Point PointinVector2(this Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
        public static bool Range(this Vector2 point, Vector2 vector1, Vector2 vector2)
        {
            return Vector2.Distance(vector1, vector2) < Vector2.Distance(point, vector1);
        }
     
        public static void Swap<Tupe>(ref Tupe item,ref Tupe item2)
        {
            Tupe C = item;
            item = item2;
            item2 = C;
        }
       public static Vector2 Maximum(this Vector2 e) { return new Vector2(float.MaxValue, float.MaxValue); }
      
    }
}
