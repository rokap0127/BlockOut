using Blockout.Def;
using Blockout.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockout.Actor
{
    //横250px 縦10px

    class LeftPaddle : Charactor
    {

        /// <summary>
        /// コンストラク
        /// </summary>
        public LeftPaddle()
            : base("paddle_blue")
        { }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            //収納
            rect = new Rectangle(260, Screen.Height - 32, 100, 10);
        }

        public Rectangle GetPaddlePos()
        {
            return rect;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTiime"></param>
        public override void Update(GameTime gameTiime)
        {
            MoveLeft();
            MoveRight();
            InScreen();
        }

        /// <summary>
        /// 左へ移動
        /// </summary>
        private void MoveLeft()
        {
            if (Input.GetKeyState(Keys.Left))
            {
                rect.X -= 15;
            }
        }

        /// <summary>
        /// 右へ移動
        /// </summary>
        private void MoveRight()
        {
            if (Input.GetKeyState(Keys.Right))
            {
                rect.X += 15;
            }
        }

        private void InScreen()
        {
            if (rect.X <= -290)
            {
                rect.X = -290 ;
            }
            if (rect.X >= Screen.Width - 10)
            {
                rect.X = Screen.Width - 10;
            }
        }

        public override void Hit(Charactor other)
        { }

        public override void HitX(Charactor other)
        { }
    }
}
