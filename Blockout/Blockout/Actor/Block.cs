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
    class Block : Charactor
    {
        private Random rnd = new Random();
        private Sound sound;

        //横32px 縦16px
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Block(Rectangle rect)
            :base("block_32")
        {
            this.rect = rect;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            isDeadFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("block_2", new Vector2(rect.X, rect.Y));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

        }
        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other"></param>
        public override void Hit(Charactor other)
        {
            isDeadFlag = true;
            //sound.PlaySE("");
        }

        /// <summary>
        /// 死亡フラグ
        /// </summary>
        /// <returns></returns>
        public override bool IsDead()
        {
            return isDeadFlag;
        }


        public bool End()
        {
            if(rect == null)
            return true;

            return false;
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public override void HitX(Charactor other)
        {

        }
    }
}
