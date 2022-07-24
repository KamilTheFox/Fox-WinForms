using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Все_что_есть.Properties;

namespace Все_что_есть
{
   class Trap : Item
    {
        public Trap(Point Location, Point Rect, Color color, bool isVisibleCollision = false) : base(Location, Rect, TupeObject.Rectangle, color, Resources.trap, isVisibleCollision)
        {

        }
        private bool isSlammed;
        public void Slammed()
        {
            isSlammed = true;
            Image = Resources.trap_Slammed;
        }
        public override void Interaction(Player player)
        {
            if (!isSlammed)
            {
                MessageBox.Show("Вы попали в Капкан!\nСочувствую =(");
                Slammed();
                Input.Clear();
                player.Dead();
               
            }
        }
        public override void Reset()=> DeleteObject();  
        public override void Update()
        {
        }
    }
}
