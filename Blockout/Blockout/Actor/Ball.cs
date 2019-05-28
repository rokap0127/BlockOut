using Blockout.Def;
using Blockout.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockout.Actor
{
    class Ball : Charactor
    {
        private Vector2 size;
        private Vector2 halfsize;
        private Vector2 speed;
        private Sound sound;
        private int coler;

        private Random random;


        public Ball(Vector2 position)
            :base("blue")
        {
            this.position = position;
            size = new Vector2(32, 32);
            halfsize = new Vector2(16, 16);
            speed = Vector2.Zero;
            random = new Random();
            isDeadFlag = false;
        }

        public override void Initialize()
        {
           
            speed = new Vector2(3, -8);

            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            coler = random.Next(1, 5);
            isDeadFlag = false;
        }

        public override void Draw(Renderer renderer)
        {
            switch (coler) {
                case 1:renderer.DrawTexture("ball1", position);
                    break;
                case 2: renderer.DrawTexture("ball2", position);
                    break;
                case 3: renderer.DrawTexture("ball3", position);
                    break;
                case 4: renderer.DrawTexture("ball4", position);
                    break;

            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += speed;
            InScreen();
            ScreenOut();
        }

        /// <summary>
        /// 壁のバウンド
        /// </summary>
        private void InScreen()
        {
            if(position.X < 0)
            {
                speed.X *= -1;
            }

            if(position.Y < 0)
            {
                speed.Y *= -1;
            }
            if (position.X + size.X > Screen.Width)
            {
                speed.X *= -1;
            }
        }

        //下に落ちたら
        private void ScreenOut()
        {
            if(position.Y > 768)
            {
                //終了
                isDeadFlag= true;
            }
        }

        public bool IsEnd()
        {
            return isDeadFlag;
        }

        public override void Hit(Charactor other)
        {
            if(other is CenterPaddle)
            {
                CenterHit();
            }
            else if(other is RightPaddle)
            {
                RigthHit();
            }
            else if(other is LeftPaddle)
            {
                LeftHit();
            }
            else
            {
                //上下に跳ね返り
                speed.Y *= -1;
            }
           
            
        }

        public override void HitX(Charactor other)
        {
            //左右に跳ね返り
            speed.X *= -1;
            //if(other is CenterPaddle)
            //{
            //    //sound.PlaySE("");
            //}
        }

        void CenterHit()
        {
            speed.Y *= -1;
        }

        void LeftHit()
        {
            speed.Y *= -1;
            if(speed.X >= 0)
            {
                speed.X = 0;
            }
            speed.X -= 1;
            if(speed.X <= -4)
            {
                speed.X = -4;
            }
        }

        void RigthHit()
        {
            speed.Y *= -1;
            if(speed.X < 0)
            {
                speed.X = 0;
            }
            speed.X += 1;
            if(speed.X >= 4)
            {
                speed.X = 4;
            }
        }
    }
}
