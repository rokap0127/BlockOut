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
        private Sound sound; //サウンド
        private bool isEndFlag; //終了フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GoodEnding() { }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("gameClear", Vector2.Zero);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// 終了
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
            return Scene.Title;
        }
        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            //sound.StopBGM();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
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
