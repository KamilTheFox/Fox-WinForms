using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Все_что_есть.Properties;

namespace Все_что_есть
{
    abstract class Item : ObjectGame, IGrafics
    {
        public static List<Item> Items { get {
                List<Item> New = new List<Item>();
                List<ObjectGame> list = ObjectGames.Where(e => e is Item).ToList();
                foreach (ObjectGame item in list)
                {
                    New.Add((Item)item);
                }
                return New;
            } }
        void Start(Point Location, bool isVisibleCollision = true)
        {
            if (Location == Point.Empty)
                GetItemRndPosition();
            IsVisible = isVisibleCollision;
        }
        private bool IsImage;
        public virtual void Interaction() { }
        public virtual void Interaction(Player player) { }
        public virtual void Draw(Graphics graphics)
        {
            if (IsImage)
                graphics.DrawImage(Image, Locate.X, Locate.Y, Size.X, Size.Y);
        }
        protected Image Image;
        public Item(Point Location, Point Rect, TupeObject tupe, Color color, Image image = null, bool isVisibleCollision = true) : base(Location, GetPointCube(Rect.X, Rect.Y), tupe, color, TupeCollision.Item)
        {
            Image = image;
            IsImage = image != null;
            if(!IsImage)
            {
                IsVisible = true;
            }
            Size = new Point(Rect.X, Rect.Y);
            Start(Location, isVisibleCollision);
        }
        static Random random = new Random();
        public void GetItemRndPosition()
        {
        now:
            Point rnd = new Point(random.Next(Game.MainForm.Size.Width - 50 - Size.X), random.Next(Game.MainForm.Size.Height - 50 - Size.Y));
            ObjectGame game = this.Clone();
            game.Locate = rnd;
            game.SetCollision = "Terrain,Item,Player,Texture";
            foreach (ObjectGame objectGame in ObjectGames)
            {
                if (game.Collision())
                {
                    goto now;
                }
                    if (objectGame.tupeObject == TupeObject.Rectangle || objectGame.tupeObject == TupeObject.FillRectangle)
                    {
                        if (objectGame.CheckPointInObject(game.Location.PointinVector2()))
                        goto now;
                    }
                

            }
            Locate = rnd;
        }
        public virtual void Reset()=> GetItemRndPosition(); 
        public abstract void Update();
    }
}
