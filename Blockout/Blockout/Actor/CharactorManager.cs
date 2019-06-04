using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Blockout.Device;
using Blockout.Def;
using Blockout.Util;

namespace Blockout.Actor
{
    class CharactorManager
    {
        private List<Charactor> paddles; //パドルリスト
        private List<Charactor> balls; //ボールリスト
        private List<Charactor> items; //アイテムリスト
        private List<Charactor> blocks; //ブロックリスト
        private List<Charactor> bullets; //バレットリスト
        private List<Charactor> addNewCharacters; //追加するキャラクターリスト
        private bool isHit; //ヒット通知
        private bool bornBall; //ボール生成
        private float itemCount; //アイテムカウント

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CharactorManager()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //各リスト、生成とクリア
            if (paddles != null)
                paddles.Clear();
            else
                paddles = new List<Charactor>();

            if (balls != null)
                balls.Clear();
            else
                balls = new List<Charactor>();

            if (items != null)
                items.Clear();
            else
                items = new List<Charactor>();

            if (blocks != null)
                blocks.Clear();
            else
                blocks = new List<Charactor>();

            if (bullets != null)
                bullets.Clear();
            else
                bullets = new List<Charactor>();

            if (addNewCharacters != null)
                addNewCharacters.Clear();
            else
                addNewCharacters = new List<Charactor>();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="charactor"></param>
        public void Add(Charactor charactor)
        {
            //早期リターン：することがなければ何もしない
            if(charactor == null)
            {
                return;
            }
            //追加リストにキャラ追加
            addNewCharacters.Add(charactor);
        }

        //当たり判定
        public void HitToCharacters()
        {
            //ボールとパドルの当たり判定
            foreach (var ball in balls)
            {
                foreach (var paddle in paddles)
                {
                    if (ball.IsDead() || paddle.IsDead())
                    {
                        continue;
                    }
                    if(paddle.IsPaddleCollision(ball) == 0)
                    {
                        paddle.Hit(ball);
                        if (paddle is CenterPaddle)
                        {
                            ball.Hit(paddle);
                        }
                        if(paddle is RightPaddle)
                        {
                            ball.Hit(paddle);
                        }
                        if(paddle is LeftPaddle)
                        {
                            ball.Hit(paddle);
                        }
                        
                    }
                    if(paddle.IsPaddleCollision(ball) == 1)
                    {
                        paddle.Hit(ball);
                        ball.HitX(paddle);
                    }
                }
            }
            //アイテムとパドルの当たり判定
            foreach(var item in items)
            {
                foreach(var paddle in paddles)
                {
                    if(item.IsDead() || paddle.IsDead())
                    {
                        continue;
                    }
                    if (paddle.IsPaddleCollision(item) == 0)
                    {
                        paddle.Hit(item);
                        item.Hit(paddle);
                        bornBall = true;
                    }
                    if (paddle.IsPaddleCollision(item) == 1)
                    {
                        paddle.Hit(item);
                        item.Hit(paddle);
                        bornBall = true;
                    }
                }
            }

            //ボールとブロックの当たり判定
            foreach(var ball in balls)
            {
                foreach(var block in blocks)
                {
                    if(ball.IsDead() || block.IsDead())
                    {
                        continue;
                    }
                    if (block.IsBlockCollision(ball) == 0)              
                    {
                        block.Hit(ball);
                        ball.Hit(block);
                        if(block is ItemBlock)
                        {
                            isHit = true;
                            itemCount++;
                        }
                    }
                    if(block.IsBlockCollision(ball) == 1)
                    {
                        block.Hit(ball);
                        ball.HitX(block);
                        if(block is ItemBlock)
                        {
                            isHit = true;
                            itemCount++;
                        }                      
                    }
                }
            }
            //ブロックとバレットの当たり判定
            foreach(var block in blocks)
            {
                foreach(var bullet in bullets)
                {
                    if (bullet.IsDead() || block.IsDead())
                    {
                        continue;
                    }
                    if (block.IsBlockCollision(bullet) == 0)
                    {
                        block.Hit(bullet);
                        bullet.Hit(block);
                    }
                    if (block.IsBlockCollision(bullet) == 1)
                    {
                        block.Hit(bullet);
                        bullet.HitX(block);
                    }
                }
            }
        }

        public float GetItemCount()
        {
            return itemCount;
        }

        public bool IsHit()
        {
            return isHit;
        }
        public bool BornBall()
        {
            return bornBall;
        }

        /// <summary>
        /// 死亡キャラの削除
        /// </summary>
        private void RemoveDeasCharacters()
        {
            //死んでいたらリストから削除
            balls.RemoveAll(b => b.IsDead());
            items.RemoveAll(i => i.IsDead());
            blocks.RemoveAll(bl => bl.IsDead());
            bullets.RemoveAll(bu => bu.IsDead());
        }

        public void Update(GameTime gameTime)
        {
            //オフ
            isHit = false;
            bornBall = false;

            //キャラ更新
            foreach (var p in paddles)
                p.Update(gameTime);

            foreach (var b in balls)
                b.Update(gameTime);

            foreach (var i in items)
                i.Update(gameTime);

            foreach (var bl in blocks)
                bl.Update(gameTime);
            foreach (var bu in bullets)
                bu.Update(gameTime);

            //追加候補者をリストに追加
            foreach (var newChara in addNewCharacters)
            {
                if(newChara is CenterPaddle 
                    || newChara is RightPaddle
                    || newChara is LeftPaddle)
                {
                    newChara.Initialize();
                    paddles.Add(newChara);
                }
                //ここにRightPaddle
                if (newChara is Ball)
                {
                    newChara.Initialize();
                    balls.Add(newChara);
                }
                if (newChara is Item)
                {
                    newChara.Initialize();
                    items.Add(newChara);
                }
                if(newChara is Block
                    || newChara is ItemBlock)
                {
                    newChara.Initialize();
                    blocks.Add(newChara);
                }
                if(newChara is Bullet)
                {
                    newChara.Initialize();
                    bullets.Add(newChara);
                }
            }
                //追加処理後、追加リストはクリア
                addNewCharacters.Clear();

                HitToCharacters();

                RemoveDeasCharacters();
        }

        public void Draw(Renderer renderer)
        {
            //全キャラ描画
            foreach (var p in paddles)
                p.Draw(renderer);

            foreach (var b in balls)
                b.Draw(renderer);

            foreach (var bl in blocks)
                bl.Draw(renderer);

            foreach (var i in items)
                i.Draw(renderer);
            foreach (var bu in bullets)
                bu.Draw(renderer);
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <returns></returns>
        public bool End()
        {
            //ボールの数が0になったら
            if(balls.Count == 0)
            {
                return true;
            }
            //ブロックの数が0になったら
            if(blocks.Count == 0)
            {
                return true;
            }

            return false;
        }
        public int BallCount()
        {
            return balls.Count;
        }
    }
}
