using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaferDiskPad
{
    public partial class WaferDiskPad : UserControl
    {
        #region 公有变量
        public int _Row { get; set; }
        public int _Col { get; set; }
        //private Size _size = new Size(768, 768);
        //public Size displaySize
        //{
        //    get { return _size; }
        //    set { _size = value; }
        //}

        #endregion

        #region 私有变量
        DoubleBufferDataGridView dataGridView;
        //panel尺寸大小
        private int _Row_panel = 768, _Col_panel = 768;

        #endregion


        public WaferDiskPad()
        {
            InitializeComponent();
        }

        private void WaferDiskPad_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            dataGridView = new DoubleBufferDataGridView
            {
                //Dock = DockStyle.Fill,
                //Size = _size,
                ScrollBars = ScrollBars.None,
                //DataGridView 的边框线的样式是通过 DataGridView.BorderStyle 属性来设定的
                BorderStyle = BorderStyle.None,
                //单元格的边框线的样式是通过 DataGridView.CellBorderStyle 属性来设定的.
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                SelectionMode = DataGridViewSelectionMode.RowHeaderSelect,
                BackColor = Color.CadetBlue
            };
            //this.Controls.Add(dataGridView);


            dataGridView.Size = this.Parent.Size;

            //dataGridView.DefaultCellStyle.ForeColor = Color.Red;
            //设定背景颜色，fromArgb有多种形式，首位数字位透明项
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, Color.AliceBlue);
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.Red;
            //dataGridView.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            //displayView.Controls.Add(dataGridView);
            this.Controls.Add(dataGridView);
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            //displayView.Show();
            int Rowlist = _Row + 4;
            int Collist = _Col + 4;
            if (Rowlist <= 1 || Collist <= 1)
            {
                MessageBox.Show("Please Input Correct digital !");
            }
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
            dataGridView.RowTemplate.Height = (_Col_panel / Rowlist);
            int ColumnWight = (_Row_panel / Collist);
            #endregion

            //dataGridView.double
            //userControlNoBlink1.Controls.Add(dataGridView);

            for (int i = 0; i < Collist + 6; i++)
            {
                DataGridViewTextBoxColumn col01 = new DataGridViewTextBoxColumn();
                col01.MaxInputLength = 3;
                // col01.CellTemplate.Size;
                col01.HeaderText = Convert.ToString(i);
                //col01.Name = Convert.ToString(i);
                col01.Width = ColumnWight;
                dataGridView.Columns.Add(col01);
            }
            int index = dataGridView.Rows.Add(Rowlist + 5);
            for (int j = 0; j < Rowlist - 4; j++)
            {
                dataGridView[Collist - 1, j + 2].Value = Convert.ToString(j);
            }
            for (int j = 0; j < Collist - 4; j++)
            {
                dataGridView[j + 2, Rowlist - 1].Value = Convert.ToString(j);
            }
            dataGridView[8, 3].Style.BackColor = Color.DarkSeaGreen;
            dataGridView[3, 7].Style.BackColor = Color.DarkSeaGreen;
            Rectangle rectangle = dataGridView.GetCellDisplayRectangle(3, 7, true);

            int ootest = Convert.ToInt32(Math.Round(2.49));
            //dataGridView.AutoResizeRows();
            //panel1.BackColor = Color.NavajoWhite;
            dataGridView.BackTransparent = true;
            //dataGridView.BackgroundColor =  Color.FromArgb(16,Color.AliceBlue);
            #region 画一个晶圆在Form
            dataGridView.BackgroundImage = dataGridView.GetBackImage(this.Parent, ColumnWight * 2, dataGridView.RowTemplate.Height * 2,
                ColumnWight * (Collist - 4), dataGridView.RowTemplate.Height * (Rowlist - 4), Color.LightGreen);
            //panel1.BackgroundImage = dataGridView.GetBackImage(this.panel1, 0, 0, this._Row_panel,this._Col_panel);
            //draw_circle.FillEllipse(brush, 0, 0, _Row_panel, _Col_panel);

            #endregion
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            //dataGridView.Invalidate();
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

            return c <= 3 ? c + a : c;
        }

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

    }
}
