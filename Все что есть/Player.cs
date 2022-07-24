using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using Все_что_есть.Properties;

namespace Все_что_есть
{

      class Player : ObjectGame, IGrafics
    {
        public List<Player> players 
        {
            get 
            {
                List<Player> New = new List<Player>();
                List<ObjectGame> list = ObjectGames.Where(e => e is Player).ToList();
                foreach (ObjectGame item in list)
                {
                    New.Add((Player)item);
                }
                return New;
            }
        }
        public delegate void PlayerDeadEvent();
        public delegate void UpdateEvent();
        public delegate void PlayerRayEvent(Ray ray);
        public int GetCoin { get { return Coin; } }
        private int Coin;
        public Spawner SpawnPoint;
        private Guid id;
        public Guid Id { get { return id; } private set { id = Guid.NewGuid() ; }  }
        public string Name { get; }
        public ObjectGame ObjectMoving { get; set; }
        public event PlayerDeadEvent deadEvent;
        public event PlayerRayEvent rayEvent;
        public event UpdateEvent updateEvent;
        public ObjectGame PlayerObject { get; }
        private Color color;
        private Color colorHit;
        public Image Skin { get; protected set; }
        public Player(string name, Point location, Point size, Color color,Spawner spawter = null) : base(location, GetPointCube(size.X, size.Y), TupeObject.FillRectangle, color, TupeCollision.Player)
        {
            Skin = "Fox" == name ? Resources._22218foxface_98828.ToBitmap() : Resources.poet;
            Name = name;
            Locate = location;
            if(spawter == null)
            SpawnPoint = new Spawner(location, size, color);
            else
            SpawnPoint = spawter;
            Size = size;
            colorHit = this.color = color;
            PlayerObject = this;
            SetCollision = "Terrain,Item";
        }
        public void SizeChange(int change, bool fill = false)
        {
            Point size = Size;
            if (!fill) { size.X += change; size.Y += change; }
            else size = new Point(change, change);
            if (size.X < 1 || size.Y < 1)
                Size = new Point(3, 3);
            else
                Size = size;
            points = GetPointCube(Size.X, Size.Y).ToList();
            points.Add(points[0]);
        }
        public void AddCoin(int addcoin =1)
        {
            Coin += addcoin;
        }
        public void Update()
        {
            updateEvent();
            if (IsMoving)
            {
                Moving();
            }
        }
        public void Draw(Graphics graphics)
        {
            Rectangle playerRect = new Rectangle(Locate.X, Locate.Y, Size.X, Size.Y);
            RotateFlipType rotate = RotateFlipType.RotateNoneFlipNone;
            Image skin = (Image)Skin.Clone();
            if (IsMoving)
            {
                if (Input.GetKeyUp(Keys.W)) rotate = RotateFlipType.RotateNoneFlipY;
                if (Input.GetKeyUp(Keys.S)) rotate = RotateFlipType.RotateNoneFlipX;
                if (Input.GetKeyUp(Keys.D)) rotate = RotateFlipType.Rotate270FlipNone;
                if (Input.GetKeyUp(Keys.A)) rotate = RotateFlipType.Rotate90FlipY;
                skin.RotateFlip(rotate);
            }
            graphics.DrawImage(skin, Rect);
            Ray ray = new Ray(Location, Raycast.CursorVec);
            rayEvent(ray);
        }
        public void Dead()
        {
            deadEvent();
            colorHit = Color.Red;
            Coin = 0;
            isMoving = false;
            ObjectMoving = null;
        }
        public void Reset(int intervalTime = 1)
        {
            Locate = SpawnPoint.Locate;
            SizeChange(SpawnPoint.Size.X, true);
            colorHit = color;
            isMoving = true;
        }
        public bool IsMoving { get { return isMoving; } }
        private bool isMoving = true;
        void Moving()
        {
            if (!isMoving)
            {
                return;
            }
            ObjectGame PlayerPhisics = new ObjectGame();
            PlayerPhisics.points = points;
            PlayerPhisics.Locate = Locate;
            PlayerPhisics.Size = Size;
            PlayerPhisics.SetCollision = SetCollision;
            int Speed = Input.GetKeyUp(Keys.ShiftKey) ? 4 : 2;
            if (Input.GetKeyUp(Keys.W) && Location.Y >= 0)
            {
                PlayerPhisics.Locate.Y -= Speed;
                if (!PlayerPhisics.Collision(out ObjectGame CollisionIgnor) || CollisionIgnor is Item)
                {
                    this.Locate = PlayerPhisics.Locate;
                }
            }
            PlayerPhisics.Locate = Locate;
            if (Input.GetKeyUp(Keys.S) && Location.Y <= Game.MainForm.Size.Height - 40)
            {
                PlayerPhisics.Locate.Y += Speed;
                if (!PlayerPhisics.Collision(out ObjectGame CollisionIgnor) || CollisionIgnor is Item)
                {
                    this.Locate = PlayerPhisics.Locate;
                }
            }
            PlayerPhisics.Locate = Locate;
            if (Input.GetKeyUp(Keys.D) && Location.X <= Game.MainForm.Size.Width - 25)
            {
                PlayerPhisics.Locate.X += Speed;
                if (!PlayerPhisics.Collision(out ObjectGame CollisionIgnor) || CollisionIgnor is Item)
                {
                    this.Locate = PlayerPhisics.Locate;
                }
            }
            PlayerPhisics.Locate = Locate;
            if (Input.GetKeyUp(Keys.A) && Location.X >= 0)
            {
                PlayerPhisics.Locate.X -= Speed;
                if (!PlayerPhisics.Collision(out ObjectGame CollisionIgnor) || CollisionIgnor is Item)
                {
                    this.Locate = PlayerPhisics.Locate;
                }
            }
            


        }


    }
}
