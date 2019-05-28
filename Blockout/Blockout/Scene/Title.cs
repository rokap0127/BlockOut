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
    class Title : IScene
    {
        private Sound sound;//サウンド
        private bool isEndFlag;//終了フラグ

        /// <summary>
        /// タイトル
        /// </summary>
        public Title()
        {
            isEndFlag = false;
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("background", Vector2.Zero);
            renderer.DrawTexture("title2", Vector2.Zero);
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
        /// 終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }
        //次のシーン
        public Scene Next()
        {
            return Scene.GamePlay;
        }
        //終了処理
        public void Shutdown()
        {
            sound.StopBGM();
        }
        //更新
        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                //sound.PlaySE("");
            }
        }
    }
}
