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
     class Spawner : Item
    {
        
        public Spawner(Point Location, Point size, Color color, Image image = null, bool isVisibleCollision = true) : base(Location, size, TupeObject.Rectangle, color, image, isVisibleCollision)
        {
        }
        public override void Interaction(Player player)
        {
            if (Input.GetKeyDown(Keys.E))
            {
                player.SpawnPoint = this;
                MessageBox.Show("Новая точка спавна установлена");
            }
        }
        public override void Reset()
        {
        }
        public override void Update()
        {
        }
    }
}
