using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_xiaoxiaole
{

    public class DoubleBufferDataGridView : DataGridView
    {
        public DoubleBufferDataGridView()
        {
            SetStyle(ControlStyles.UserPaint, true);//用户自己绘制
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private Image backImage;

        #region Property：Backimage
        //[DescriptionAttribute("自定义背景图：当BackTransparent=True时，忽略此设置，直接使用父容器背景")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Category("Custom"), Description("backgroundImage")]
        public override Image BackgroundImage
        {
            get { return backImage; }
            set
            {
                backImage = value;
                base.Refresh(); // 重新加载
            }
        }
        //原文链接：https://blog.csdn.net/zcn596785154/article/details/120158807
        #endregion
        #region PaintBackground
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);
            if (backImage != null)
            {
                graphics.DrawImageUnscaledAndClipped(this.backImage, gridBounds);
            }
            else
            {
                //backImage = GetBackImage(this, this.Left, this.Top, this.Width, this.Height);
                //如果不添加背景图片,会导致背景污染，应该需要调用上转型解决
            }

        }

        //原文链接：https://blog.csdn.net/zcn596785154/article/details/120158807
        #endregion
        #region OnCellPainting
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;

                Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                    e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                    e.CellBounds.Height - 4);

                using (
                    Brush gridBrush = new SolidBrush(this.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor),
                    selectedColorBrush = new SolidBrush(e.CellStyle.SelectionBackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        if (this.Rows[e.RowIndex].Selected)
                        {
                            e.Graphics.FillRectangle(selectedColorBrush, e.CellBounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        }
                        if (e.Value != null)
                        {
                            e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                Brushes.White, e.CellBounds.X + 2,
                                e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                    }

                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(new Pen(gridBrush), border);
                    e.Handled = true;
                }

        }
        //原文链接：https://blog.csdn.net/zcn596785154/article/details/120158807
        #endregion


        [DescriptionAttribute("背景透明：当True时，直接使用父容器背景。否则使用BackImage填充背景")]
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public bool BackTransparent { get; set; } = true;

        #region 绘制一个基础map的bmp
        public Bitmap GetBackImage(Control parent, int x, int y, int w, int h,Color color)
        {
            //if (parent.BackgroundImage == null)
            {
                //Bitmap bt = new Bitmap(parent.Width , parent.Height );
                //PictureBox pb = new PictureBox();
                //pb.Size = parent.Size;
                //pb.BackgroundImage = parent.BackgroundImage;
                //pb.BackgroundImageLayout = parent.BackgroundImageLayout;
                //pb.DrawToBitmap(bt, pb.DisplayRectangle);
                //pb.Dispose();
                Bitmap destBitmap = new Bitmap(parent.Width, parent.Height);
                Graphics g = Graphics.FromImage(destBitmap);
                //g.DrawImage(bt, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
                g.FillEllipse (new SolidBrush(color), new Rectangle(x, y, w, h));
                g.DrawEllipse(new Pen(Color.BlueViolet, (float)(2)), new Rectangle(x, y, w, h));
                g.DrawEllipse(new Pen(Color.BlueViolet, (float)(2)), new Rectangle(x-10, y-10, w+20, h+20));

                //bt.Dispose();
                g.Dispose();
                return destBitmap;
            }
            //else
            //return null;
        }

        #endregion

    }

}