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
        //DoubleBufferDataGridView dataGridView;
        WaferDiskPad.WaferDiskPad waferDiskPad;
        //panel尺寸大小
        private int _Row_panel = 896, _Col_panel = 896;
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
            for (int i = 10; i < 100; i++)
            {
                waferDiskPad = new WaferDiskPad.WaferDiskPad();
                waferDiskPad.Size = panel1.Size;
                panel1.Controls.Clear();
               GC.Collect();
                //waferDiskPad.Location=new Point(84, 100);
                waferDiskPad._Col = i;// Convert.ToInt32(textBox1.Text.ToString().Trim());
                waferDiskPad._Row = i; // Convert.ToInt32(textBox2.Text.ToString().Trim());
                panel1.Controls.Add(waferDiskPad);
                System.Threading.Thread.Sleep(200);
                waferDiskPad.Dispose();
            }
            //Form displayView = new Form
            //{
            //    Dock = DockStyle.Fill,
            //    FormBorderStyle = FormBorderStyle.None,
            //    MaximizeBox = false,
            //    TopLevel = false
            //};
            // private void dgv_Circle_Draw(object sender, DataGridViewRowPostPaintEventArgs e)
            //{
            //    DataGridView gridView = (DataGridView)sender;
            //    Graphics draw_circle = gridView.CreateGraphics();
            //    //draw_circle.
            //    Brush brush = new SolidBrush(Color.Red);
            //    draw_circle.FillEllipse(brush, 0, 0, _Row_panel, _Col_panel);

            //}
            /// <summary>
            /// 可以定义一个可以输出的新数组。
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>

        }
    }

}
