using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Все_что_есть
{
    public interface IGrafics
    {
        void Draw(Graphics g);
        void Update();
    }
}
