using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockout.Device;
using Microsoft.Xna.Framework;

namespace Blockout.Actor
{
    class ItemBlock : Charactor
    {
        private Sound sound;

        public ItemBlock(Rectangle rect)
            :base("block_32")
        {
            this.rect = rect;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("block_4" ,new Vector2(rect.X, rect.Y));
        }


        public override void Hit(Charactor other)
        {
            isDeadFlag = true;
        }

        public override void HitX(Charactor other)
        { }

        public override void Initialize()
        {
            isDeadFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Update(GameTime gameTime)
        { }
    }
}
