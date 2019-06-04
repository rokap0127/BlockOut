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
        private CenterPaddle centerPaddle;//センターパドル

        private Random rnd; //ランダム
        private Timer timer; //タイム
        private Sound sound; //サウンド

        private float itemCount; //アイテムカウント
        private float bulletCount;//バレットカウント

        private BulletUI bulletUI; //バレットUI

        private bool isEndFlag; //終了フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GamePlay()
        { }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //背景を描画
            renderer.DrawTexture("background", Vector2.Zero);
            //残弾数を描画
            bulletUI.Draw(renderer);
            //キャラクターマネジャを描画
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
            //パドルを追加
            charactorManager.Add(new RightPaddle());
            charactorManager.Add( centerPaddle = new CenterPaddle());
            charactorManager.Add(new LeftPaddle());
            //ブロックを追加
            for (int i = 0; i <= Screen.Height/*横の長さ*/; i += 35)
            {
                for (int j = 100; j <= 138/*縦の長さ*/; j += 19)
                {
                    block = new Block(new Rectangle(100 + i, j, 32, 16));
                    charactorManager.Add(block);
                }
            }
            //アイテムブロックを追加
            for (int i = 0; i <= 770; i += 70)
            {
                itemBlock = new ItemBlock(new Rectangle(100 + i, 210, 32, 16));
                charactorManager.Add(itemBlock);
            }
            //ボールを追加
            charactorManager.Add(new Ball(new Vector2(512, 578)));
            //サウンド関係
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            //カウントダウンタイマー1秒に設定
            timer = new CountDownTimer(1);
          

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
            //ブロックが0なら
            if(charactorManager.BallCount() == 0)
            {
                //ゲームオーバーへ
                return Scene.Ending;
            }
            //ゲームクリアへ
            return Scene.GoodEnding;
        }

        //終了処理
        public void Shutdown()
        {
            //BGMを止める
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //ブロックに当たると
            //（アイテムカウントが増えているとき）
            if (itemCount < charactorManager.GetItemCount())
            {
                //アイテム発生
                charactorManager.Add(new Item(new Vector2(        
                    rnd.Next(Screen.Width - 32), -32)));

                itemCount = charactorManager.GetItemCount();
   
            }

           
            if (charactorManager.BornBall())
            {
                //ボールを追加
                charactorManager.Add(new Ball(new Vector2
                    (rnd.Next(0, Screen.Width - 32), 0)));
                //バレットカウントを＋
                bulletCount += 1;
            }

            //バレットを撃つ
            if(bulletCount != 0)
            {
                //スペースを押すと
                if (Input.GetKeyTrigger(Keys.Space))
                {
                    //バレットを追加
                    Vector2 paddlePos = centerPaddle.GetPaddlePos() + new Vector2(50, 0);
                    charactorManager.Add(new Bullet(paddlePos));
                    //バレットカウントを-
                    bulletCount -= 1;
                }
            }

            //1秒たって
            if (timer.IsTime())
            {
                //ボールかブロックがなくなったら
                if (charactorManager.End())
                {
                    //ゲーム終了
                    isEndFlag = true;
                }
            }
            //ゲームマネージャを更新
            charactorManager.Update(gameTime);
            //タイマーを更新
            timer.Update(gameTime);

            //バレットUIの生成
            bulletUI = new BulletUI(bulletCount);


            //sound.PlayBGM("");

            //終了条件（デバック用）
            //if (Input.GetKeyTrigger(Keys.Space) ) //スペースを押したら
            //{
            //    isEndFlag = true;
            //}
        }

    }
}
