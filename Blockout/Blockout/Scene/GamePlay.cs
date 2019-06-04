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
        private CharactorManager charactorManager; //キャラクターマネージャ
        private Block block; //ブロック
        private ItemBlock itemBlock; //アイテムブロック
        private CenterPaddle centerPaddle;
        private Random rnd; //ランダム
        private Timer timer; //タイム
        private Sound sound; //サウンド

        private float itemCount; //アイテムカウント
        private float bulletCount;

        private BulletUI bulletUI; //バレットUI

        private bool isEndFlag; //終了フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GamePlay()
        {
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("background", Vector2.Zero);
            bulletUI.Draw(renderer);
            charactorManager.Draw(renderer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //乱数を取得
            rnd = new Random();
            //終了フラグをオフ
            isEndFlag = false;
            //キャラクターマネージャを生成
            charactorManager = new CharactorManager();
            charactorManager.Add(new RightPaddle());
            charactorManager.Add( centerPaddle = new CenterPaddle());
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

            //アイテムカウントの初期化
            itemCount = 0;
            //バレットカウントの初期化
            bulletCount = 0;

            //バレットUIの生成、初期化
            bulletUI = new BulletUI(bulletCount);

        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            if(charactorManager.BallCount() == 0)
            {
                return Scene.Ending;
            }
            return Scene.GoodEnding;
        }

        //終了処理
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //ブロックに当たると
            if (itemCount < charactorManager.GetCount())
            {
                charactorManager.Add(new Item(new Vector2(
              rnd.Next(Screen.Width - 32), -32)));
            itemCount = charactorManager.GetCount();
   
            }

            //アイテム発生
            if (charactorManager.BornBall())
            {
                charactorManager.Add(new Ball(new Vector2
                    (rnd.Next(0, Screen.Width - 32), 0)));
                bulletCount += 1;
            }

            //バレットを撃つ
            if(bulletCount != 0)
            {
                if (Input.GetKeyTrigger(Keys.Space))
                {
                    Vector2 paddlePos = centerPaddle.GetPaddlePos() + new Vector2(50, 0);
                    charactorManager.Add(new Bullet(paddlePos));
                    bulletCount -= 1;
                }
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

            //バレットUIの生成
            bulletUI = new BulletUI(bulletCount);


            //sound.PlayBGM("");
        }

    }
}
