using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Все_что_есть
{
     class Apple : Item
    {
        public Apple(Point Location, Point Rect, Color color, Image image = null, bool isVisibleCollision = true) : base(Location, Rect, TupeObject.Rectangle, color, image, isVisibleCollision)
        {

        }

       
        public override void Interaction(Player player)
        {
            player.AddCoin();
            player.SizeChange(1);
            GetItemRndPosition();
        }
        public override void Update()
        {
        }
    }
}
