using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockout.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blockout.Scene
{
    class GoodEnding : IScene
    {
        private Sound sound;
        private bool isEndFlag;


        public GoodEnding() { }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("gameClear", Vector2.Zero);
        }

        public void Initialize()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {
            //sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            //sound.PlayBGM("");
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                //sound.PlaySE("");
            }
        }
    }
}
