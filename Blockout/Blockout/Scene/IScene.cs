using Blockout.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockout.Scene
{
    /// <summary>
    /// シーンインタフェイス
    /// </summary>
    interface IScene
    {
        void Initialize(); //初期化
        void Update(GameTime gameTime); //更新
        void Draw(Renderer renderer); //描画
        void Shutdown(); //終了

        //シーンの管理用
        bool IsEnd(); //終了チェック
        Scene Next(); //次のシーンへ
    }
}
