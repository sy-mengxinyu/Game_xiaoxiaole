using System;
using System.Drawing;

namespace Game_xiaoxiaole
{
    /// <summary>
    /// 块对象
    /// </summary>
    class Block
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 尺寸位置
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// 下落block位置
        /// </summary>
        private Rectangle realRectangle
        {
            get {
                if (!IsFalling)
                    return Rectangle;
                return new Rectangle(Rectangle.X,Rectangle.Y+Game.fallingHeight,Rectangle.Width,Rectangle.Height);
            }
        }

        /// <summary>
        /// 矩阵坐标
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// 下落状态
        /// </summary>
        public bool IsFalling { get; set; }

        /// <summary>
        /// status所代表的图像
        /// </summary>
        private Image Image
        {
            get
            {
                return (Image)Properties.Resources.ResourceManager.GetObject(Status.ToString());
            }
        }

        /// <summary>
        /// 随机数
        /// </summary>
        private static Random rnd { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="point">矩阵坐标</param>
        /// <param name="rectangle">位置</param>
        public Block(Point point, Rectangle rectangle)
        {
            Point = point;
            Rectangle = rectangle;
            if (rnd==null)
            {
                rnd = new Random(unchecked((int)DateTime.Now.Ticks));

            }
            ///先要产生随机数(相对比较随机的)
            Status = rnd.Next(1, 7);
            ///下落状态
            IsFalling = false;
        }

        /// <summary>
        /// 画出图像
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            Color Color2 = Color.FromArgb(27, 20 , 20);
            g.DrawRectangle(new Pen(Color2), realRectangle);
            if (Status>0)
            {
                g.DrawImage(Image, realRectangle);
            }
        }

        
    }
}
