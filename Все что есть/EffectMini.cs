using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Все_что_есть
{
    internal class EffectMini : Item
    {
        public EffectMini(Point Location, Point Rect, Color color, Image image = null, bool isVisibleCollision = false) : base(Location, Rect, TupeObject.Rectangle, color, image, isVisibleCollision)
        {

        }

        public override void Interaction(Player player)
        {
            DeleteObject();
            player.SizeChange(-20);
        }

        public override void Update()
        {
        }
    }
}
