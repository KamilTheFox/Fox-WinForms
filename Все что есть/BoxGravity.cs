using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Все_что_есть
{
    class BoxGravity : Item
    {
        public Phisiks phisiks;

        public BoxGravity(Point Location, Point Rect, Color color, Image image = null, bool isVisibleCollision = true) : base(Location, Rect, TupeObject.Rectangle, color, image, isVisibleCollision)
        {
            SetCollision = "Terrain,Item";
            phisiks = new Phisiks(this);
            phisiks.Higth = 8;
        }
        bool isTake;
        public override void Interaction()
        {
           
        }
        public override void Update()
        {
            Vector2 MousePos = Raycast.CursorVec;
            if (!isTake)
                phisiks.Update();
            if (this.CheckPointInObject(MousePos.PointinVector2()))
            {
                if (Input.GetKeyUp(System.Windows.Forms.MouseButtons.Middle))
                {
                    phisiks.Clear();
                    isTake = true;
                }

                if (Input.GetKeyUp(System.Windows.Forms.MouseButtons.Right))
                    phisiks.AddForce(Location - MousePos);
            }
            bool flag = false;
            if (Input.GetKeyUp(System.Windows.Forms.MouseButtons.Left))
            { 
                foreach (ObjectGame obj in ObjectGames)
                    if (obj != this && !obj.CheckPointInObject(Raycast.CursorVec.PointinVector2()))
                    { flag = true; break; }
            if (flag)
            { isTake = false; }
            }
                if (isTake)
                {
                ObjectGame open = new ObjectGame();
                open.points = points;
                open.Locate = MousePos.PointinVector2();
                open.Size = Size;
                open.SetCollision = SetCollision;
                open.thisObject = this;
                if (!open.Collision())
                    Location = open.Locate.Vector2inPoint();
                }
            
        }
    }
}
