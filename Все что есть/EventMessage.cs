using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Все_что_есть
{
     class EventMessage : Item
    {
        public EventMessage(Point Location, Point Rect, Color color, Image image = null, bool isVisibleCollision = true) : base(Location, Rect, TupeObject.Rectangle, color, image, isVisibleCollision)
        {

        }
        public override void Interaction(Player player)
        {
            Input.Clear();
            int rnd = new Random().Next(-20, 21);
            string Win = $"Вы ничего не выиграли";
            if (rnd > 0)
                Win = $"Вы выиграли {rnd}$";
            else if (rnd < 0)
                Win = $"Вы проиграли {rnd}$";
            MessageBox.Show(Win, "Внимание!!!", MessageBoxButtons.OK, MessageBoxIcon.Question);
            player.AddCoin(rnd);
            player.SizeChange(rnd);
            DeleteObject();
        }

        public override void Update()
        {
            
        }
    }
}
