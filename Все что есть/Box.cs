using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Все_что_есть.Properties;

namespace Все_что_есть
{
    class Box : Item
    {
        public Box(Point Location, Point Rect, TupeObject tupe, Color color, bool isVisibleCollision = true) : base(Location, Rect, tupe, color, Resources.giftbox, isVisibleCollision)
        {
            IsRevealed= false;
            SetCollision = "Item";
        }
        public bool IsRevealed { get; private set; }
        public override void Interaction(Player player)
        {
            if (!IsRevealed)
            {
                if (player.ObjectMoving == null)
                    player.ObjectMoving = this;
                else if (Collision(out ObjectGame @object) && @object is Trap trap)
                {
                    trap.Slammed();
                    IsRevealed = true;
                    this.Image = Resources.giftbox_Revealed;
                    MessageBox.Show("Капкан всхлопнулся раскрыв Коробку\nВы нашли в коробке 10 монет");
                    player.AddCoin(10);
                }
            }
        }
        public override void Reset()
        {
            IsRevealed = false;
            Image = Resources.giftbox;
            DeleteObject();
        }
        public override void Update()
        {
        }
    }
}
