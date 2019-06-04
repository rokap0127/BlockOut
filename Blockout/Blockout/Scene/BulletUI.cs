using Blockout.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockout.Scene
{
    class BulletUI
    {
        private float bulletCount;

        public BulletUI(float bulletCount)
        {
            this.bulletCount = bulletCount;
        }


        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("remaining bullets", new Vector2(20, -10));
            renderer.DrawNumber("number", new Vector2(195, 10), (int)bulletCount);
        }
    }
}
