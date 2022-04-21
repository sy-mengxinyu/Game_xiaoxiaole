using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Game_xiaoxiaole
{
    class Game
    {
        /// <summary>
        /// 二次绘图大小
        /// </summary>
        private const int Cols = 20;
        private const int Rows = 20;
        private int GameWidth = 1100;
        private int GameHeight = 1100;
        private const int unitGameWidth = 50;
        private const int unitGameHeight = 50;

        /// <summary>
        /// 消除次数及积分
        /// </summary>
        private int EliminateNum = 0;
        private int RealEliminateNum = 0;
        public int ScoreNum = 0;

        /// <summary>
        /// 鼠标点击block
        /// </summary>
        private Block MouseBlock { get; set; }
        private Block BlockOld { get; set; }

        /// <summary>
        /// 时钟
        /// </summary>
        public System.Timers.Timer Timer { get; set; }

        private DateTime MovieStopTime { get; set; }

        /// <summary>
        /// 所有类块的集合
        /// </summary>
        private List<Block> Blocks { get; set; }

        /// <summary>
        /// 下落速度
        /// </summary>
        public static int fallingHeight;

        /// <summary>
        /// 委托
        /// </summary>
        public delegate void GameEvent();
        public event GameEvent gameChanged;

        //背景音乐控制
        SoundPlayer player1 = new SoundPlayer(Properties.Resources.backmusic);

        /// <summary>
        /// 初始化，也相当于一个构造函数
        /// </summary>
        public Game()
        {
            player1.PlayLooping();
            Blocks = new List<Block>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    //初始化block
                    Block block = new Block(new Point(i, j),
                                  new Rectangle(unitGameHeight + j * unitGameWidth, unitGameWidth + i * unitGameHeight, unitGameWidth, unitGameHeight));
                    Blocks.Add(block);
                }
            }
            //时钟
            Timer = new System.Timers.Timer();
            Timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 时钟的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (MovieStopTime != null && MovieStopTime < System.DateTime.Now)
            {
                Timer.Stop();
                foreach (var block in Blocks)
                {
                    block.IsFalling = false;
                }
                //真实下落替换
                blockFall();
            }
            //实现下落过程
            fallingHeight += 10;
            gameChanged();
        }

        /// <summary>
        /// 下落
        /// </summary>
        private void SetIsFalling()
        {
            //改变要下落的block的下落状态
            foreach (var block in Blocks.Where(m => Blocks.Any(n => n.Point.Y == m.Point.Y && n.Point.X > m.Point.X && n.Status == 0)))
            {
                block.IsFalling = true;
            }
        }

        /// <summary>
        /// 绘制当前游戏的场景
        /// 这个类是尺寸自定，图片确定，这个draw相当于game的属性
        /// </summary>
        /// <param name="g">绘图句柄</param>
        /// <param name="rec">游戏区域尺寸</param>
        public void Draw(Graphics g, Size size)
        {
            //二次Draw
            Image img = new Bitmap(GameWidth, GameHeight);
            Graphics _g = Graphics.FromImage(img);

            ///画背景
            Rectangle rec = new Rectangle(new Point(0, 0), new Size(GameWidth, GameHeight));
            _g.DrawImage(Properties.Resources.backg, rec);
            Color Color1 = Color.FromArgb(27, 45, 73);
            Rectangle rec1 = new Rectangle(new Point((GameWidth / 11), (GameHeight / 11)), new Size(9 * (GameWidth / 11), 9 * (GameHeight / 11)));
            _g.FillRectangle(new SolidBrush(Color1), rec1);

            foreach (var block in Blocks)
            {
                block.Draw(_g);
            }

            if (MouseBlock != null)
            {
                MouseBlock.Draw(_g);
            }
            g.DrawImage(img, new Rectangle(new Point(0, 0), size));
        }

        /// <summary>
        ///初始化消除
        /// </summary>
        public void Eliminate()
        {
            RealEliminateNum = 0;
            for (int m = 0; m < 10; m++)
            {
                //消除
                Eraseable();

                //状态替换
                for (int j = 0; j < 9; j++)
                {
                    for (int i = 8; i > 0; i--)
                    {
                        int zeroNum = 0;
                        for (int k = 1; k < 9; k++)
                        {
                            //控制索引范围
                            if (i - k == -1)
                            {
                                break;
                            }

                            //判断以i，j为起点向上有多少个0
                            if (Blocks[9 * i + j].Status != 0)
                            {
                                break;
                            }
                            else
                            {
                                if (Blocks[9 * (i - k) + j].Status != 0)
                                {
                                    zeroNum = k;
                                }
                            }

                            //进行逐个替换，实现下落机制
                            for (int n = 0; n < zeroNum; n++)
                            {
                                if (i - n == -1 || i - zeroNum - n == -1)
                                {
                                    break;
                                }
                                Blocks[9 * (i - n) + j].Status = Blocks[9 * (i - zeroNum - n) + j].Status;//没有实现互换
                                Blocks[9 * (i - zeroNum - n) + j].Status = 0;
                            }
                        }
                    }
                }

                //按新状态重绘
                foreach (var block in Blocks)
                {
                    if (block.Status == 0)
                    {
                        block.Status = new Block(block.Point, block.Rectangle).Status;
                    }
                }
                RealEliminateNum = EliminateNum;

                //确定是否还可以继续消
                if (!Eraseable())
                {
                    break;
                }
            }
            ScoreNum = 0;
        }

        /// <summary>
        /// 鼠标点击取block
        /// </summary>
        /// <param name="point"></param>
        /// <param name="size"></param>
        public void MouseDown(Point point, Size size)
        {
            //找到被点击block
            Point newPoint = new Point(point.X * GameWidth / size.Width, point.Y * GameHeight / size.Height);
            var block = Blocks.FirstOrDefault(m => m.Rectangle.Contains(newPoint));
            if (block != null)
            {
                MouseBlock = new Block(new Point(-1, -1), block.Rectangle);
                MouseBlock.Status = block.Status;
                BlockOld = block;
                block.Status = 0;
            }
        }

        /// <summary>
        /// 实现点击block的移动
        /// </summary>
        /// <param name="point"></param>
        /// <param name="size"></param>
        public void MouseMove(Point point, Size size)
        {
            if (MouseBlock == null) return;
            Point newPoint = new Point(point.X * GameWidth / size.Width, point.Y * GameHeight / size.Height);
            MouseBlock.Rectangle = new Rectangle(newPoint.X - unitGameWidth / 2, newPoint.Y - unitGameHeight / 2, unitGameWidth, unitGameHeight);
        }

        /// <summary>
        /// 实现互换
        /// </summary>
        /// <returns></returns>
        public int MouseUp(Point point, Size size)
        {
            Point newPoint = new Point(point.X * GameWidth / size.Width, point.Y * GameHeight / size.Height);
            if (MouseBlock != null)
            {
                BlockOld.Status = MouseBlock.Status;
            }
            MouseBlock = null;
            var block = Blocks.FirstOrDefault(m => m.Rectangle.Contains(newPoint));
            if (block != null)
            {
                //判断是否相邻
                if ((BlockOld.Point.X == block.Point.X && (BlockOld.Point.Y == block.Point.Y - 1 || BlockOld.Point.Y == block.Point.Y + 1)) ||
                    (BlockOld.Point.Y == block.Point.Y && (BlockOld.Point.X == block.Point.X - 1 || BlockOld.Point.X == block.Point.X + 1)))
                {
                    //状态互换
                    int temp = block.Status;
                    block.Status = BlockOld.Status;
                    BlockOld.Status = temp;

                    //若不可消，换回来
                    if (!Eraseable())
                    {
                        int temp2 = block.Status;
                        block.Status = BlockOld.Status;
                        BlockOld.Status = temp2;
                    }
                    else
                    {
                        //消除声音播放
                        player1.Stop();
                        SoundPlayer player = new SoundPlayer(Properties.Resources.SOUND);
                        player.Play();
                        //实现下落
                        MovieStopTime = System.DateTime.Now.AddSeconds(1);
                        SetIsFalling();
                        fallingHeight = 0;
                        Timer.Start();
                    }
                }
            }
            return ScoreNum;
        }

        /// <summary>
        /// 判断是否可消
        /// </summary>
        /// <returns></returns>
        private bool Eraseable()
        {
            EliminateNum = 0;
            //直角T型等特殊消除
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //左上角向下向右的直角
                    ArrayList listLeftRight = new ArrayList();
                    ArrayList listUpDown = new ArrayList();
                    var Startstatus = Blocks[9 * i + j].Status;

                    for (int k = 1; k < 9; k++)
                    {
                        listLeftRight.Add(Blocks[9 * i + j]);
                        listUpDown.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (j + k == 9)
                        {
                            break;
                        }
                        if (i + k == 9)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftRight.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i + k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpDown.Add(Blocks[9 * (i + k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUpDown.Count >= 3 && listLeftRight.Count >= 3)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUpDown)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeftRight)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //左下角向上向右的直角
                    ArrayList listRight2 = new ArrayList();
                    ArrayList listUp2 = new ArrayList();

                    for (int k = 1; k < 9; k++)
                    {
                        listRight2.Add(Blocks[9 * i + j]);
                        listUp2.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (j + k == 9)
                        {
                            break;
                        }
                        if (i - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listRight2.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUp2.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUp2.Count >= 3 && listRight2.Count >= 3)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUp2)
                            {
                                Blocks.Status = 0;
                                foreach (Block Blocks1 in listRight2)
                                {
                                    Blocks1.Status = 0;
                                }
                            }
                        }
                    }

                    //右T字形
                    ArrayList listLeftT = new ArrayList();
                    ArrayList listUpDownT = new ArrayList();
                    int beforeNum = -1;
                    for (int k = 1; k < 9; k++)
                    {
                        listLeftT.Add(Blocks[9 * i + j]);
                        listUpDownT.Add(Blocks[9 * i + j]);

                        //控制索引范围 纵列相同放入集合
                        if (i - k == -1)
                        {
                            break;
                        }

                        if (Blocks[9 * i + j - k].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftT.Add(Blocks[9 * i + j - k]);
                        }
                        else
                        {
                            break;
                        }

                        //横行将相同的放入集合
                        beforeNum *= beforeNum;
                        int n = beforeNum * (k - 1);
                        if (i + n == 9 || i - n == -1 || i - n == 9 || i + n == -1)
                        {
                            break;
                        }

                        if (Blocks[9 * (i + n) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpDownT.Add(Blocks[9 * (i + n) + j]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i - n) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpDownT.Add(Blocks[9 * (i - n) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUpDownT.Count == 6 & listLeftT.Count == 4)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUpDownT)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeftT)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //左T字形
                    ArrayList listRightT = new ArrayList();
                    ArrayList listUpDownT1 = new ArrayList();
                    beforeNum = -1;
                    for (int k = 1; k < 9; k++)
                    {
                        listRightT.Add(Blocks[9 * i + j]);
                        listUpDownT1.Add(Blocks[9 * i + j]);

                        //控制索引范围 纵列相同放入集合
                        if (i + k == 9)
                        {
                            break;
                        }

                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listRightT.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        //横行将相同的放入集合
                        beforeNum *= beforeNum;
                        int n = beforeNum * (k - 1);
                        if (i + n == 9 || i - n == -1 || i - n == 9 || i + n == -1)
                        {
                            break;
                        }

                        if (Blocks[9 * (i + n) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpDownT1.Add(Blocks[9 * (i + n) + j]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i - n) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpDownT1.Add(Blocks[9 * (i - n) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUpDownT1.Count == 6 & listRightT.Count == 4)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUpDownT1)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listRightT)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }


                    //T字形
                    ArrayList listLeftRightT = new ArrayList();
                    ArrayList listDownT = new ArrayList();
                    beforeNum = -1;
                    for (int k = 1; k < 9; k++)
                    {
                        listLeftRightT.Add(Blocks[9 * i + j]);
                        listDownT.Add(Blocks[9 * i + j]);

                        //控制索引范围 纵列相同放入集合
                        if (i + k == 9)
                        {
                            break;
                        }

                        if (Blocks[9 * (i + k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listDownT.Add(Blocks[9 * (i + k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //横行将相同的放入集合
                        beforeNum *= beforeNum;
                        int n = beforeNum * (k - 1);
                        if (j + n == 9 || j + n == -1 || j - n == 9 || j - n == -1)
                        {
                            break;
                        }

                        if (Blocks[9 * i + j + n].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftRightT.Add(Blocks[9 * i + j + n]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * i + j - n].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftRightT.Add(Blocks[9 * i + j - n]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listDownT.Count == 4 & listLeftRightT.Count == 6)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listDownT)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeftRightT)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //倒T字形 
                    ArrayList listLeftRightT1 = new ArrayList();
                    ArrayList listUpT = new ArrayList();
                    beforeNum = -1;
                    for (int k = 1; k < 9; k++)
                    {
                        listLeftRightT1.Add(Blocks[9 * i + j]);
                        listUpT.Add(Blocks[9 * i + j]);

                        //控制索引范围 纵列相同放入集合
                        if (i - k == -1)
                        {
                            break;
                        }

                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUpT.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //横行将相同的放入集合
                        beforeNum = beforeNum * beforeNum;
                        int n = beforeNum * (k - 1);
                        if (j + n == 9 || j + n == -1 || j - n == 9 || j - n == -1)
                        {
                            break;
                        }
                        if (Blocks[9 * i + j + n].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftRightT1.Add(Blocks[9 * i + j + n]);
                        }
                        else
                        {
                            break;
                        }
                        if (Blocks[9 * i + j - n].Status == Blocks[9 * i + j].Status)
                        {
                            listLeftRightT1.Add(Blocks[9 * i + j - n]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUpT.Count == 4 && listLeftRightT1.Count == 6)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUpT)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeftRightT1)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //右上向左向下直角 
                    ArrayList listLeft = new ArrayList();
                    ArrayList listDown1 = new ArrayList();
                    for (int k = 1; k < 9; k++)
                    {
                        listLeft.Add(Blocks[9 * i + j]);
                        listDown1.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (i + k == 9 || j - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j - k].Status == Blocks[9 * i + j].Status)
                        {
                            listLeft.Add(Blocks[9 * i + j - k]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i + k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listDown1.Add(Blocks[9 * (i + k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listDown1.Count >= 3 && listLeft.Count >= 3)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listDown1)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeft)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }


                    //右下向左向上直角 
                    ArrayList listLeft1 = new ArrayList();
                    ArrayList listUp = new ArrayList();
                    for (int k = 1; k < 9; k++)
                    {
                        listLeft1.Add(Blocks[9 * i + j]);
                        listUp.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (i - k == -1 || j - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j - k].Status == Blocks[9 * i + j].Status)
                        {
                            listLeft1.Add(Blocks[9 * i + j - k]);
                        }
                        else
                        {
                            break;
                        }

                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUp.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUp.Count >= 3 && listLeft1.Count >= 3)
                        {
                            EliminateNum += 1;
                            foreach (Block Blocks in listUp)
                            {
                                Blocks.Status = 0;
                            }
                            foreach (Block Blocks in listLeft1)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }
                }
            }
            ScoreNum += 400 * EliminateNum;

            //从左下判断开始判断 横竖常规消子
            for (int i = 8; i >= 0; i--)
            {
                for (int j = 0; j < 9; j++)
                {
                    var Startstatus = Blocks[9 * i + j].Status;
                    ArrayList listRight = new ArrayList();
                    ArrayList listUp = new ArrayList();

                    //五子
                    for (int k = 1; k < 9; k++)
                    {
                        listRight.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (j + k == 9)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listRight.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        //分开有利于之后算积分
                        if (listRight.Count == 8)
                        {
                            EliminateNum += 1;
                            ScoreNum += 250;
                            foreach (Block Blocks in listRight)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //四子
                    listRight.Clear();
                    for (int k = 1; k < 9; k++)
                    {
                        listRight.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (j + k == 9)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listRight.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        //分开有利于之后算积分
                        if (listRight.Count == 6)
                        {
                            EliminateNum += 1;
                            ScoreNum += 200;
                            foreach (Block Blocks in listRight)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    ///三子
                    listRight.Clear();
                    for (int k = 1; k < 9; k++)
                    {
                        listRight.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (j + k == 9)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * i + j + k].Status == Blocks[9 * i + j].Status)
                        {
                            listRight.Add(Blocks[9 * i + j + k]);
                        }
                        else
                        {
                            break;
                        }

                        //分开有利于之后算积分
                        if (listRight.Count == 4)
                        {
                            EliminateNum += 1;
                            ScoreNum += 150;
                            foreach (Block Blocks in listRight)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //五子
                    for (int k = 1; k < 9; k++)
                    {
                        listUp.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (i - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUp.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }

                        //改变集合状态
                        if (listUp.Count == 8)
                        {
                            EliminateNum += 1;
                            ScoreNum += 250;
                            foreach (Block Blocks in listUp)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //四子
                    listUp.Clear();
                    for (int k = 1; k < 9; k++)
                    {
                        listUp.Add(Blocks[9 * i + j]);
                        //控制索引范围
                        if (i - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUp.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }


                        //改变集合状态
                        if (listUp.Count == 6)
                        {
                            EliminateNum += 1;
                            ScoreNum += 200;
                            foreach (Block Blocks in listUp)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }

                    //三子
                    listUp.Clear();
                    for (int k = 1; k < 9; k++)
                    {
                        listUp.Add(Blocks[9 * i + j]);

                        //控制索引范围
                        if (i - k == -1)
                        {
                            break;
                        }

                        //将相同的放入集合
                        if (Blocks[9 * (i - k) + j].Status == Blocks[9 * i + j].Status)
                        {
                            listUp.Add(Blocks[9 * (i - k) + j]);
                        }
                        else
                        {
                            break;
                        }


                        //改变集合状态
                        if (listUp.Count == 4)
                        {
                            EliminateNum += 1;
                            ScoreNum += 150;
                            foreach (Block Blocks in listUp)
                            {
                                Blocks.Status = 0;
                            }
                        }
                    }
                }
            }

            //判断消除次数
            if (EliminateNum == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 下落
        /// </summary>
        public void blockFall()
        {
            //状态替换
            for (int j = 0; j < 9; j++)
            {
                for (int i = 8; i > 0; i--)
                {
                    int zeroNum = 0;
                    for (int k = 1; k < 9; k++)
                    {
                        //控制索引范围
                        if (i - k == -1)
                        {
                            break;
                        }

                        //判断以i，j为起点向上有多少个0
                        if (Blocks[9 * i + j].Status != 0)
                        {
                            break;
                        }
                        else
                        {
                            if (Blocks[9 * (i - k) + j].Status != 0)
                            {
                                zeroNum = k;
                            }
                        }

                        //进行逐个替换，实现下落机制
                        for (int n = 0; n < zeroNum; n++)
                        {
                            if (i - n == -1 || i - zeroNum - n == -1)
                            {
                                break;
                            }
                            Blocks[9 * (i - n) + j].Status = Blocks[9 * (i - zeroNum - n) + j].Status;
                            Blocks[9 * (i - zeroNum - n) + j].Status = 0;
                        }
                    }
                }
            }

            //按新状态重绘
            foreach (var block in Blocks)
            {
                if (block.Status == 0)
                {
                    block.Status = new Block(block.Point, block.Rectangle).Status;
                }
            }

            //再次判断是否可消
            if (Eraseable())
            {
                //消除声音播放
                player1.Stop();
                SoundPlayer player = new SoundPlayer(Properties.Resources.SOUND);
                player.Play();
                //下落
                MovieStopTime = System.DateTime.Now.AddSeconds(1);
                SetIsFalling();
                fallingHeight = 0;
                Timer.Start();
            }
            else
            {
                player1.PlayLooping();
            }
        }
    }
}













