using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Все_что_есть.Properties;

namespace Все_что_есть
{

    public partial class Game : Form
    {

        public Graphics graphics;
        Player Main;
        public Point startPosition = new Point(20, 20);
        public static Game MainForm;
        Item Box;
        Item Effect;
        Item WinRandomEvent;
        public List<IGrafics> IDraw = new List<IGrafics>();
        public Game()
        {
            string gametype = "GameFox";
            InitializeComponent();
            Initilize();
            if (gametype == "Phisics")
            {
                label1.Visible = label2.Visible = false;
                InitilizeTestPhisics();
            }
            else
                InitilizeGameObject();
        }

        void InitilizeTestPhisics()
        {
            new BoxGravity(new Point(200, 100), new Point(40, 40), Color.Red);
            new BoxGravity(new Point(300, 100), new Point(40, 40), Color.Orange);
            new BoxGravity(new Point(350, 180), new Point(40, 40), Color.DarkGreen);
            new ObjectGame(Point.Empty, new Point[] { new Point(0, 0), new Point(0, 400), new Point(700, 400), new Point(700, 50) }, ObjectGame.TupeObject.Lines, Color.Green);
            new ObjectGame(Point.Empty, new Point[] { new Point(100, 200), new Point(450, 250) }, ObjectGame.TupeObject.Lines, Color.Green);
        }
        void UpdateGUI()
        {
            IDraw.Clear();
            IDraw.Add(new Terrain());
            foreach (ObjectGame player in ObjectGame.ObjectGames)
                if (player is Player)
                    IDraw.Add((Player)player);
            foreach (Item arrayItem in Item.Items)
                IDraw.Add(arrayItem);
        }
        void MainPlayerRayHit(Ray ray)
        {
            float Distanse = 50 + (Main.Size.X + Main.Size.Y / 2);
            RayHit Hit = new RayHit();
            Hit.GetCollisions(Main.SetCollision);
            if (Raycast.RayHitTerrain(ray, ref Hit, Main.ObjectMoving))
            {
                if (Vector2.Distance(Main.Location, Hit.vector) > Distanse)
                {
                    Hit.vector = Ray.Vector2onLineByDistance(ray, Distanse);
                    graphics.FillEllipse(new SolidBrush(Color.Red), Hit.vector.X - 3, Hit.vector.Y - 3, 6, 6);
                    goto Goto;
                }
                graphics.FillEllipse(new SolidBrush(Color.Red), Hit.vector.X - 3, Hit.vector.Y - 3, 6, 6);
                if (Input.GetKeyDown(MouseButtons.Left))
                {
                    if (Hit.objectHit.tupeCollision == ObjectGame.TupeCollision.Terrain)
                    {
                        Input.Clear();
                        MessageBox.Show("Стена =D");
                        return;
                    }
                    if (Hit.objectHit is Item item)
                    {
                        item.Interaction(Main);
                        if (item is Apple && new Random().Next(10) == 2 && !ObjectGame.ObjectGames.Contains(Effect))
                        {
                            Effect.GetItemRndPosition();
                            Effect.AddObject();
                        }
                    }

                }
            }
            else
            {
                Hit.vector = Ray.Vector2onLineByDistance(ray, Distanse);
                if (Vector2.Distance(Main.Location, ray.Vector2) <= Vector2.Distance(Main.Location, Hit.vector))
                    Hit.vector = Ray.Vector2onLineByDistance(ray, Vector2.Distance(ray.Vector1, ray.Vector2));
                graphics.FillEllipse(new SolidBrush(Color.Red), Hit.vector.X - 3, Hit.vector.Y - 3, 6, 6);
            }
        Goto:
            if (Main.ObjectMoving != null)
            {
                Main.ObjectMoving.Location =Hit.vector;
                if (Input.GetKeyDown(MouseButtons.Right))
                {
                    if (Main.ObjectMoving is Item item)
                        item.Interaction(Main);
                    Main.ObjectMoving = null;
                }
            }
        }
        void Initilize()
        {
            MainForm = this;
            groupBox1.Visible = false;
            Input.Start(this);
            buttonExit.Click += (obj, e) => { Application.Exit(); };
            OnGUI();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
        void InitilizeGameObject()
        {
            ObjectGame[] obj = new ObjectGame[]
        {
            new ObjectGame(new Point(150, 150), new Point[] { new Point(100, 0), new Point(0, 0) }, ObjectGame.TupeObject.Lines,  Color.Black),
            new ObjectGame(new Point(200, 100), new Point[] { new Point(50, 100), new Point(50, 0) }, ObjectGame.TupeObject.Lines,  Color.Black),
            new ObjectGame(new Point(300, 200), new Point[] { new Point(100, 100) , new Point(0, 0)}, ObjectGame.TupeObject.Lines, Color.Black),
            new ObjectGame(new Point(300, 200), new Point[] { new Point(0, 100), new Point(100, 0) }, ObjectGame.TupeObject.Lines, Color.Black),
            new Texture(new Point(300, 30), new Point(150,15) ,  Color.Cyan,ObjectGame.TupeObject.Rectangle),
            new ObjectGame(new Point(250, 200), new Point[] { new Point(0, 0), new Point(50, 20), new Point(250, 50), new Point(50, 250) }, ObjectGame.TupeObject.Rectangle,  Color.Blue),
        };
            Spawner spawn = new Spawner(startPosition, new Point(30, 30), Color.Brown, Resources.russia);
            new Spawner(new Point(700, 300), new Point(30, 30), Color.Black, Resources.russia);
            Box = new Box(new Point(500, 300), new Point(20, 20), ObjectGame.TupeObject.Rectangle, Color.White, false);
            Effect = new EffectMini(Point.Empty, new Point(15, 15), Color.White, Resources.free_icon_potion_3330442);
            Effect.DeleteObject();
            if (new Random().Next(10) != 5)
                Box.DeleteObject();
            Main = new Player( "Fox", startPosition, new Point(30, 30), Color.Green, spawn);
            Main.updateEvent += PlayerCollision;
            Main.deadEvent += () =>
            {
                BoxGameOver.Visible = true;
                labelGameOverCount.Text = Main.GetCoin.ToString();
            };
            Main.rayEvent += MainPlayerRayHit;
            new Texture(new Point(label2.Location.X - 5, label2.Location.Y - 5), new Point(label1.Location.X + label1.Width + 5, label1.Height + 10), Color.Black);
            new Apple(Point.Empty, new Point(30, 30), Color.Red, Resources.apple, false);
            for (int i = 0; i < new Random().Next(12, 30); i++)
            {
                new Trap(Point.Empty, new Point(15, 15), Color.White);
            }
            WinRandomEvent = new EventMessage(Point.Empty, new Point(5, 5), Color.Black);
        }
        public static string TestLog = "";
        public void PlayerCollision()
        {
            label1.Text = Main.GetCoin.ToString();
            if (Main.Collision(out ObjectGame collision))
            {
                if (collision is Item item)
                {
                    item.Interaction(Main);
                    if (item is Apple)
                    {
                        if (new Random().Next(10) == 2 && !ObjectGame.ObjectGames.Contains(Effect))
                        {
                            Effect.GetItemRndPosition();
                            Effect.AddObject();
                        }
                    }
                }
            }
        }
        private async void OnGUI()
        {
            while (true)
            {
                await Task.Delay(1);
                Update();
                Refresh();
            }
        }
        int CountUpdateObject;
        new void Update()
        {
            if (Input.GetKeyDown(Keys.F1) && Input.GetKeyDown(Keys.ControlKey))
            {
                groupBox1.Visible = !groupBox1.Visible;
            }
            if (CountUpdateObject != ObjectGame.ObjectGames.Count)
            {
                CountUpdateObject = ObjectGame.ObjectGames.Count;
                UpdateGUI();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Class1.GetMD5Hash(textBox1.Text);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;

            graphics.DrawString(TestLog, this.Font, new SolidBrush(Color.Black), Raycast.CursorVec.X + 20, Raycast.CursorVec.Y);
            foreach (IGrafics grafics in IDraw)
            {
                grafics.Update();
                grafics.Draw(graphics);
            }
        }
        private void button_Reset_Click(object sender, EventArgs e)
        {
            if (BoxGameOver.Visible)
            {
                Main.Reset();
                foreach (Item arrayItem in Item.Items)
                {
                    arrayItem.Reset();
                }
                if (!ObjectGame.ObjectGames.Contains(Box) && new Random().Next(10) == 5)
                {
                    Box.AddObject();
                }
                if (!ObjectGame.ObjectGames.Contains(WinRandomEvent))
                    WinRandomEvent.AddObject();
                for (int i = 0; i < new Random().Next(12, 30); i++)
                {
                    new Trap(Point.Empty, new Point(15, 15), Color.White);
                }
                label1.Text = "0";
                BoxGameOver.Visible = false;
                UpdateGUI(); 
            }
        }
    }

}
