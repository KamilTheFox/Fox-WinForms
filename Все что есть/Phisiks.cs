using System;
using System.Numerics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Все_что_есть
{
    internal class Phisiks
    {
        
        ObjectGame transform;
        float Gravity;
        const float constBoost = 0.4F;
        float Boost = constBoost;
        float ForceX = 0;
        public int Higth = 4;
        public Phisiks(ObjectGame transform)
        {
          this.transform= transform;
        }
        public void Clear()
        {
            Boost = constBoost;
            Gravity = 0;
            ForceX = 0;
        }
        public void AddForce(Vector2 value)
        {
            Gravity = 0;
            ForceX = value.X;
            Boost = value.Y/5;
        }
        public void Update()
        {
            if(Boost < constBoost)
            {
                Boost += 0.25F;
            }
            ObjectGame obj = new ObjectGame();
            obj.thisObject = transform;
            obj.SetCollision = transform.SetCollision;
            obj.Locate = transform.Locate;
            obj.Locate.Y += (int)Gravity;
            obj.Locate.X += (int)ForceX;
            obj.points = transform.points;
            if (obj.Collision(out ObjectGame game))
            {
               // if (game is BoxGravity boxGravity)
               // {
               //     boxGravity.phisiks.AddForce((obj.Location - boxGravity.Location)/new Vector2(2,2));
               // }
                Gravity = -Gravity / Higth;
                ForceX = -ForceX / Higth;
            }
            else
            {
                transform.Locate = obj.Locate;
                if (Gravity < transform.Size.Y)
                    Gravity += Boost;
            }
            
            
        }
       
      
    }
}
