using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockout.Actor;
using Blockout.Def;
using Blockout.Device;
using Blockout.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blockout.Scene
{
    class GamePlay : IScene
    {
        private CharactorManager charactorManager;
        private Block block;
        private ItemBlock itemBlock;
        private Random rnd;
        private Timer timer;
        private Sound sound;

        private float count;
        private bool isEndFlag;

        public GamePlay()
        {
            isEndFlag = false;
            rnd = new Random();
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("background", Vector2.Zero);
            charactorManager.Draw(renderer);
        }

        public void Initialize()
        {
            isEndFlag = false;
            charactorManager = new CharactorManager();
            charactorManager.Add(new RightPaddle());
            charactorManager.Add(new CenterPaddle());
            charactorManager.Add(new LeftPaddle());
            
            for (int i = 0; i <= Screen.Height/*横の長さ*/; i += 35)
            {
                for (int j = 100; j <= 138/*縦の長さ*/; j += 19)
                {
                    block = new Block(new Rectangle(100 + i, j, 32, 16));
                    charactorManager.Add(block);
                }
            }
            for (int i = 0; i <= 770; i += 70)
            {
                itemBlock = new ItemBlock(new Rectangle(100 + i, 210, 32, 16));
                charactorManager.Add(itemBlock);
            }
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            timer = new CountDownTimer(1);
            charactorManager.Add(new Ball(new Vector2(512, 578)));
  
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            if(charactorManager.BallCount() == 0)
            {
                return Scene.Ending;
            }
            return Scene.GoodEnding;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            //ブロックに当たると
            if (count < charactorManager.GetCount())
            {
                charactorManager.Add(new Item(new Vector2(
              rnd.Next(Screen.Width - 32), -32)));
            count = charactorManager.GetCount();
            }

            if (charactorManager.BornBall())
            {
                charactorManager.Add(new Ball(new Vector2
                    (rnd.Next(0, Screen.Width - 32), 0)));
            }

            //終了条件
            //if (Input.GetKeyTrigger(Keys.Space) ) //スペースを押したら
            //{
            //    isEndFlag = true;
            //}
            if (timer.IsTime())
            {
                if (charactorManager.End())
                {
                    isEndFlag = true;
                }
            }
            charactorManager.Update(gameTime);
            timer.Update(gameTime);

            //sound.PlayBGM("");
        }

    }
}
