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
        private int _Row_Weight = 960, _Col_Hight = 960;
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
            panel1.Size = new Size(_Row_Weight, _Col_Hight);

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            waferDiskPad.Tabledisplay.Rows[4][waferDiskPad._Col + 3] = Convert.ToInt32(DateTime.Now.Millisecond) ;
            waferDiskPad.dataGridView[3,4].Style.BackColor = Color.FromArgb(128, 255, 255- Convert.ToInt32(DateTime.Now.Millisecond/4) , Convert.ToInt32(DateTime.Now.Millisecond/4));
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                return;
            }
            // for (int i = 10; i < 100; i++)
            {
                if (panel1.Controls.Contains(waferDiskPad) )       waferDiskPad.Dispose();

                {

                    waferDiskPad = new WaferDiskPad.WaferDiskPad();
                    waferDiskPad.Size = panel1.Size;
                    //waferDiskPad.BackColor = Color.FromArgb(64, Color.CadetBlue);
                    waferDiskPad.Dock = DockStyle.Fill;

                    panel1.Controls.Clear();
                    GC.Collect();
                    //waferDiskPad.Location=new Point(84, 100);
                    //waferDiskPad._Row = i; 
                    //waferDiskPad._Col = i;
                    waferDiskPad._Row = Convert.ToInt32(textBox2.Text.ToString().Trim());
                    waferDiskPad._Col = Convert.ToInt32(textBox1.Text.ToString().Trim());
                    panel1.Controls.Add(waferDiskPad);
                    timer1.Enabled = true;
                    //System.Threading.Thread.Sleep(800);
                    //waferDiskPad.Dispose();
                }
            }

        }
    }
}
