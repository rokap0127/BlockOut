using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blockout.Device;
using Blockout.Def;

namespace Blockout.Actor
{
    class Item : Charactor
    {
        //横32px,縦32px
        //フィールド
        //落下スピード
        private Vector2 speed;
        private int ans;
        private Sound sound;

        public Item(Vector2 position)
            : base("item")
        {
            isDeadFlag = false;
            this.position = position;
            //0に設定
            speed = Vector2.Zero;
            Random rnd = new Random();
            ans = rnd.Next(1, 5);


        }

        public override void Draw(Renderer renderer)
        {
            switch (ans)
            {
                case 1: renderer.DrawTexture("item1", position);
                    break;
                case 2:renderer.DrawTexture("item2" , position);
                    break;
                case 3: renderer.DrawTexture("item3", position);
                    break;
                case 4: renderer.DrawTexture("item4", position);
                    break;
            }
        }

        public override void Initialize()
        {
            speed.Y = 4;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

        }

        public override void Update(GameTime gameTime)
        {
            Fall();
            ScreenOut();
        }

        public override void Hit(Charactor other)
        {
            isDeadFlag = true;
        }

        private void Fall()
        {
            position += speed;
        }
        private void ScreenOut()
        {
            if (position.Y > 768)
            {
                //終了
                isDeadFlag = true;
            }
        }

        public override void HitX(Charactor other)
        {
            throw new NotImplementedException();
        }
    }
}
