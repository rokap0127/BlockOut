// このファイルで必要なライブラリのnamespaceを指定
using Blockout.Actor;
using Blockout.Def;
using Blockout.Device;
using Blockout.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace Blockout
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        //private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト
        private GameDevice gameDevice;
        private Renderer renderer;
        private SceneManager sceneManager;

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";

            //画面サイズ設定
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            //ゲームデバイスの実体生成を取得
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);

            //シーンマネジャーの生成
            sceneManager = new SceneManager();
            //各シーンの追加
            sceneManager.Add(Scene.Scene.Load, new LoadScene());
            //sceneManager.Add(Scene.Scene.Title, new Title());
            //sceneManager.Add(Scene.Scene.GamePlay, new GamePlay());
            //sceneManager.Add(Scene.Scene.Ending, new Ending());
            //sceneManager.Add(Scene.Scene.GoodEnding, new GoodEnding());
            //sceneManager.Add(Scene.Scene.Load, new SceneFader(new LoadScene()));
            sceneManager.Add(Scene.Scene.Title, new SceneFader(new Title()));　 //Secen追加
            IScene addScene = new GamePlay();
            sceneManager.Add(Scene.Scene.GamePlay, addScene);
            sceneManager.Add(Scene.Scene.Ending, new Ending());
            sceneManager.Add(Scene.Scene.GoodEnding, new GoodEnding());
            //一番最初のシーンを設定
            sceneManager.Change(Scene.Scene.Load);

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // この下にロジックを記述
            renderer = gameDevice.GetRenderer();

            renderer.LoadContent("load", "./");
            renderer.LoadContent("number", "./");

            //１ピクセルの黒色の画像を生成しレンダラーに登録
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1 * 1];
            colors[0] = new Color(0, 0, 0);
            fade.SetData(colors);
            renderer.LoadContent("fade", fade);

            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述
            renderer.Unload();

            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            //この一回のみ更新が必要なもの
            gameDevice.Update(gameTime); //他のところでこれをやると入力処理がおかしくなる

            // この下に更新ロジックを記述
            sceneManager.Update(gameTime);
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述
            renderer.Begin();
            sceneManager.Draw(renderer);
            renderer.End();

            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
