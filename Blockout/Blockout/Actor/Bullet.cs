using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockout.Device;
using Microsoft.Xna.Framework;

namespace Blockout.Actor
{
    class Bullet : Charactor
    {
        private Vector2 speed;


        public Bullet(Vector2 position)
            : base("ball1")
        {
            this.position = position;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("ball1", position);
        }

        public override void Initialize()
        {
            isDeadFlag = false;
            speed = new Vector2(0, -8);
        }

        public override void Hit(Charactor other)
        {
            isDeadFlag = true;
        }

        public override void HitX(Charactor other)
        {
            
        }

       

        public override void Update(GameTime gameTime)
        {
            position += speed;

            ScreenOut();
        }

        private void ScreenOut()
        {
            if(position.Y < -40)
            {
                isDeadFlag = true;

            }
        }
    }
}
