using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Blockout.Device;

namespace Blockout.Actor
{
    abstract class Charactor
    {
        protected Rectangle rect;
        protected Vector2 position;
        protected string name;
        protected float radius;
        protected bool isDeadFlag;
        protected int dirHit; //当たった方向

        public Charactor(string name)
        {
            this.name = name;
            rect = new Rectangle(0, 0, 0, 0);
            position = Vector2.Zero;
            radius = 16;
            isDeadFlag = false;
            dirHit = 0;
        }

        //抽象メソッド
        public abstract void Initialize(); //初期化
        public abstract void Update(GameTime gameTime); //更新
        public abstract void Hit(Charactor other); //ヒット通知
        public abstract void HitX(Charactor other); //X方向ヒット通知

        //public abstract void CenterHit(Charactor other);//真ん中のヒット通知
        //public abstract void RigthHit(Charactor other); //右側のヒット通知
        //public abstract void LeftHit(Charactor oyher);//左側のヒット通知

        public virtual bool IsDead()
        {
            return isDeadFlag;
        }

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, new Vector2(rect.X, rect.Y));
        }

        //当たり判定
        public int IsPaddleCollision(Charactor other)
        {
            //if(LineVsCircle(new Vector2(rect.Left, rect.Top),
            //    new Vector2(rect.Right, rect.Top),
            //    other.position + new Vector2(16, 16),
            //    16f))
            //{
            //    return true;
            //}
            //return false;

            int PaddlePosition = BlockVsCircle(rect, other.position + new Vector2(16, 16));
            if(PaddlePosition == 1 || PaddlePosition == 2)
            {
                return 0;
            }
            if(PaddlePosition == 3 || PaddlePosition == 4)
            {
                 return 1;
            }
            return 2;
        }

        public int IsBlockCollision(Charactor other)
        {

            int Blockposition = BlockVsCircle(rect, other.position + new Vector2(16, 16));
            if (Blockposition == 1 || Blockposition == 2)
            {
                return 0;
                
            }
            if (Blockposition == 3 || Blockposition == 4)
            {
                return 1;
                
            }
            return 2;
        }


        double DotProduct(Vector2 a, Vector2 b)
        {
            //内積計算
            return a.X * b.X + a.Y * b.Y;
        }

        //点と線の当たり判定
        public bool LineVsCircle(Vector2 p1, Vector2 p2, Vector2 center, float radius)
        {
            Vector2 lineDir = (p2 - p1); //パドルの方向ベクトル
            Vector2 n = new Vector2(lineDir.Y, -lineDir.X); //パドルの法線
            n.Normalize();

            Vector2 dir1 = center - p1;
            Vector2 dir2 = center - p2;

            double dist = Math.Abs(DotProduct(dir1, n));
            double a1 = DotProduct(dir1, lineDir);
            double a2 = DotProduct(dir2, lineDir);

            return (a1 * a2 < 0 && dist < radius) ? true : false;
        }

        //四角形の当たり判定
        public int BlockVsCircle(Rectangle block, Vector2 ball)
        {
            if (LineVsCircle(new Vector2(block.Left, block.Top),
                new Vector2(block.Right, block.Top), ball, radius))
                return 1;

            if (LineVsCircle(new Vector2(block.Left, block.Bottom),
                new Vector2(block.Right, block.Bottom), ball, radius))
                return 2;

            if (LineVsCircle(new Vector2(block.Right, block.Top),
                new Vector2(block.Right, block.Bottom), ball, radius))
                return 3;

            if (LineVsCircle(new Vector2(block.Left, block.Top),
                new Vector2(block.Left, block.Bottom), ball, radius))
                return 4;

            return -1;
        }
    }
}
