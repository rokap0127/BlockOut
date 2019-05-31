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
        private Vector2 size; //サイズ
        private Vector2 halfsize; //半サイズ
        private Vector2 speed; //スピード
        private Sound sound; //サウンド
        private int coler; //色番号

        private Random random;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position"></param>
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

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
           
            speed = new Vector2(3, -8);

            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            coler = random.Next(1, 5);
            isDeadFlag = false;
        }

        //描画
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

        //終了か？
        public bool IsEnd()
        {
            return isDeadFlag;
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other"></param>
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

        /// <summary>
        /// 横のヒット通知
        /// </summary>
        /// <param name="other"></param>
        public override void HitX(Charactor other)
        {
            //左右に跳ね返り
            speed.X *= -1;
            //if(other is CenterPaddle)
            //{
            //    //sound.PlaySE("");
            //}
        }

        /// <summary>
        /// 中心パドルのヒット通知
        /// </summary>
        void CenterHit()
        {
            speed.Y *= -1;
        }

        /// <summary>
        /// 左パドルのヒット通知
        /// </summary>
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

        /// <summary>
        /// 右パドルのヒット通知
        /// </summary>
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
