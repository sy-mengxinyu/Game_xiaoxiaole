using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Game_xiaoxiaole
{
    public partial class FrmMain : Form
    {
        //游戏对象说明
        private Game _game;
        private int gameScore;
        DoubleBufferDataGridView dataGridView;
        WaferDiskPad.WaferDiskPad waferDiskPad = new WaferDiskPad.WaferDiskPad();
        //panel尺寸大小
        private int _Row_panel = 768, _Col_panel = 768;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //初始化
            labelNum.Text = "0";
            labelNum.BackColor = Color.Transparent;
            _game = new Game();
            _game.gameChanged += _game_gameChanged;
            _game.Eliminate();
            this.GameForArea.Invalidate();

            panel1.Size = new Size(_Row_panel, _Col_panel);
            waferDiskPad.Size = new Size(_Row_panel, _Col_panel);
            waferDiskPad.Top = 768;
            this.Controls.Add(waferDiskPad);
            waferDiskPad.Left = 760;
        }
        #region 消消乐主要程序
        private void _game_gameChanged()
        {
            GameForArea.Invalidate();
            if (gameScore >= 1000)
            {
                //游戏结束了
                _game.Timer.Stop();
                MessageBox.Show("Game over！");
                //重置游戏
                _game = new Game();
                labelNum.Text = "0";
                _game.gameChanged += _game_gameChanged;
                _game.Eliminate();
                this.GameForArea.Invalidate();

            }
        }

        private void GameForArea_Paint(object sender, PaintEventArgs e)
        {
            //初始化绘图
            _game.Draw(e.Graphics, AreaForGame.Size);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //重新初始游戏
            _game = new Game();
            _game.gameChanged += _game_gameChanged;
            _game.Eliminate();
            this.GameForArea.Invalidate();
            //重置积分板
            gameScore = 0;
            gameScore = _game.ScoreNum;
            labelNum.Text = gameScore.ToString();
            this.labelNum.Refresh();
        }


        private void GameForArea_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标点击
            if (_game != null && e.Button == MouseButtons.Left)
            {
                //调用game的鼠标点击处理程序
                _game.MouseDown(e.Location, GameForArea.Size);
                //重绘游戏区域
                GameForArea.Invalidate();
            }
        }

        private void GameForArea_MouseMove(object sender, MouseEventArgs e)
        {
            //按下鼠标左键
            if (e.Button == MouseButtons.Left)
            {
                _game.MouseMove(e.Location, GameForArea.Size);
                GameForArea.Invalidate();
            }
        }

        private void GameForArea_MouseUp(object sender, MouseEventArgs e)
        {
            //按下鼠标左键
            if (e.Button == MouseButtons.Left)
            {
                gameScore = _game.MouseUp(e.Location, GameForArea.Size);
                GameForArea.Invalidate();
                gameScore = _game.ScoreNum;
                labelNum.Text = gameScore.ToString();
                this.labelNum.Refresh();
                if (gameScore >= 10000)
                {
                    labelNum.Text = "0";
                }
            }
        }

        private void btnAboutme_Click(object sender, EventArgs e)
        {
            FrmAbtMe frm = new FrmAbtMe();
            frm.ShowDialog();
        }

        #endregion

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                return;
            }
            panel1.Controls.Clear();
            //Form displayView = new Form
            //{
            //    Dock = DockStyle.Fill,
            //    FormBorderStyle = FormBorderStyle.None,
            //    MaximizeBox = false,
            //    TopLevel = false
            //};
            dataGridView = new DoubleBufferDataGridView
            {
                //Dock = DockStyle.Fill,
                Size = panel1.Size,
                ScrollBars = ScrollBars.None,
                //DataGridView 的边框线的样式是通过 DataGridView.BorderStyle 属性来设定的
                BorderStyle = BorderStyle.None,
                //单元格的边框线的样式是通过 DataGridView.CellBorderStyle 属性来设定的.
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                SelectionMode = DataGridViewSelectionMode.RowHeaderSelect,
                BackColor=Color.CadetBlue
            };

            //dataGridView.DefaultCellStyle.ForeColor = Color.Red;
            //设定背景颜色，fromArgb有多种形式，首位数字位透明项
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, Color.AliceBlue);
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.Red;
            //dataGridView.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            //displayView.Controls.Add(dataGridView);
            panel1.Controls.Add(dataGridView);
            //this.Controls.Add(dataGridView);
            dataGridView.DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleLeft;


            //displayView.Show();

            int Rowlist = int.Parse(textBox1.Text.Trim())+4;
            int Collist = int.Parse(textBox2.Text.Trim())+4;
            if (Rowlist <= 1 || Collist <= 1)
            {
                return;
            }
            int[,] test = iuhsj(Rowlist, Collist);
            //dataGridView.RowPostPaint += dgv_detail_RowPostPaint;

            #region 设置datagridview的显示特性。包括cell的颜色、大小

            //dataGridView.RowCount = Rowlist;
            //去掉DataGridView最左边的空白列
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            //不显示出dataGridView1的最后一行空白
            //dataGridView.AllowUserToAddRows = false;
            //userControlNoBlink1.Anchor = AnchorStyles.Left|AnchorStyles.Top|AnchorStyles.Right|AnchorStyles.Bottom;
            //userControlNoBlink1.Dock = DockStyle.Fill;

            dataGridView.RowHeadersWidth = calculation_Lenght_Mod(Collist, _Row_panel);
            dataGridView.ColumnHeadersHeight = calculation_Lenght_Mod(Rowlist, _Col_panel);
            dataGridView.RowHeadersWidth = 4;
            dataGridView.ColumnHeadersHeight = 4;
            dataGridView.RowTemplate.Height = (_Col_panel  / Rowlist);
            int ColumnWight =(_Row_panel / Collist);
            #endregion

            //dataGridView.double
            //userControlNoBlink1.Controls.Add(dataGridView);

            for (int i = 0; i < Collist+6; i++)
            {
                DataGridViewTextBoxColumn col01 = new DataGridViewTextBoxColumn();
                col01.MaxInputLength = 3;
                // col01.CellTemplate.Size;
                col01.HeaderText = Convert.ToString(i);
                //col01.Name = Convert.ToString(i);
                col01.Width = ColumnWight;
                dataGridView.Columns.Add(col01);
            }
            int index = dataGridView.Rows.Add(Rowlist +5);
            for (int j = 0; j < Rowlist - 4; j++)
            {
                dataGridView[Collist-1, j + 2].Value = Convert.ToString(j);
            }
            for (int j = 0; j < Collist - 4; j++)
            {
                dataGridView[j + 2,Rowlist-1].Value = Convert.ToString(j);
            }
            dataGridView[8, 3].Style.BackColor = Color.DarkSeaGreen;
            dataGridView[3, 7].Style.BackColor = Color.DarkSeaGreen;
            Rectangle rectangle = dataGridView.GetCellDisplayRectangle(3, 7, true);
            label1.Text = Convert.ToString(rectangle.X);
            label2.Text = Convert.ToString(rectangle.Y);
            int ootest = Convert.ToInt32(Math.Round(2.49));
            //dataGridView.AutoResizeRows();
            //panel1.BackColor = Color.NavajoWhite;
            dataGridView.BackTransparent = true;
            //dataGridView.BackgroundColor =  Color.FromArgb(16,Color.AliceBlue);
            #region 画一个晶圆在Form
            dataGridView.BackgroundImage = dataGridView.GetBackImage(this.panel1, ColumnWight*2, dataGridView.RowTemplate.Height*2,
                ColumnWight * (Collist-4), dataGridView.RowTemplate.Height*(Rowlist-4),Color.LightGreen);
            //panel1.BackgroundImage = dataGridView.GetBackImage(this.panel1, 0, 0, this._Row_panel,this._Col_panel);
            //draw_circle.FillEllipse(brush, 0, 0, _Row_panel, _Col_panel);

            #endregion
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Invalidate();
        }
        private void dgv_Circle_Draw(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            Graphics draw_circle = gridView.CreateGraphics();
            //draw_circle.
            Brush brush = new SolidBrush(Color.Red);
            draw_circle.FillEllipse(brush, 0, 0, _Row_panel, _Col_panel);

        }
        /// <summary>
        /// 可以定义一个可以输出的新数组。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int[,] iuhsj(int x, int y)
        {
            return new int[x, y];
        }
        /// <summary>
        /// DataGridView的左侧那一列显示行号,添加到DataGridView的委托RowPostPaint。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_detail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            using (SolidBrush b = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex,
                    System.Globalization.CultureInfo.CurrentUICulture),
                    e.InheritedRowStyle.Font, b,
                    e.RowBounds.Location.X,
                    e.RowBounds.Location.Y);
            }

        }

        /// <summary>
        /// 输入单元格尺寸大小求取芯片显示网格大小。
        /// 计算两个数据的模
        ///  </summary>
        public int calculation_Lenght_Mod(int shortData, int lengthData)
        {
            int a = shortData;
            int b = lengthData;
            int c = b % a;

            return c <= 2 ? c + a : c;
        }

    }

}
