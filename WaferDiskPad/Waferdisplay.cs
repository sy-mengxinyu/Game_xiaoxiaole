using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace WaferDiskPad
{
    public partial class WaferDiskPad : UserControl
    {
        #region 公有变量
        //行列数目
        public int _Row { get; set; }
        public int _Col { get; set; }
        //间隔宽度,最小宽度为DPI=2
        //TODO 注意这里是Bin间隙
         private int _in_Bin;

        private DataSet _dataSet;
        public DataSet dataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        BindingSource BindingSource { get; set; }

        private DataTable  dataTable1;

        public DataTable Tabledisplay
        {
            get { return dataTable1; }
            set { dataTable1 = value; }
        }

        private DataTable datatablsql;

        public DataTable Sqldatatable
        {
            get { return datatablsql; }
            set { datatablsql = value; }
        }

        public  int RowHight,ColumnWight;

        public int _interal_Bin
        {
            get { return _in_Bin; }
            set {
                if (_in_Bin <= 2 && _in_Bin != value)
                {
                    MessageBox.Show("间隙太小，请重新输入");
                    value = 2;
                } 
                _in_Bin= value;
                _out_Bin_Pad= value/2;    
            }
        }
       #endregion

        #region 私有变量
        //Cell中边界框线宽度的一半大小
        private int _out_Bin_Pad;

        public DoubleBufferDataGridView dataGridView;
        //panel尺寸大小,预留边界
        private int _Row_panel, _Col_panel, _in_margin = 4;
        private int unused_Row_weight, unused_Col_hight;
        private float X = 1024;
        private float Y = 788;
        Image BackguangImage;
        #endregion


        public WaferDiskPad()
        {

            InitializeComponent();
            dataGridView = new DoubleBufferDataGridView
            {
                //Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.None,
                //DataGridView 的边框线的样式是通过 DataGridView.BorderStyle 属性来设定的
                BorderStyle = BorderStyle.None,
                //单元格的边框线的样式是通过 DataGridView.CellBorderStyle 属性来设定的.
                CellBorderStyle = DataGridViewCellBorderStyle.Raised,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                //不显示出dataGridView1的最后一行空白
                AllowUserToAddRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                //BackColor = Color.CadetBlue
                //dataGridView.RowCount = Rowlist;
                //去掉DataGridView最左边的空白列
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                ReadOnly = true,
            AllowUserToResizeColumns = false

        };
            //BackguangImage = dataGridView.GetBackImage(this, 0, 0, this.Size.Width, this.Size.Height, Color.FromArgb(64, Color.CadetBlue), 0);
            //this.BackgroundImage = BackgroundImage;
            dataSet=new DataSet();
            BindingSource = new BindingSource();
            Tabledisplay = new DataTable("Dispaly_Bin");
            Sqldatatable = new DataTable("SqlDataTable");
            dataSet.Tables.Add(Tabledisplay);
            dataSet.Tables.Add(Sqldatatable);
            BindingSource.DataSource = dataSet.Tables["Dispaly_Bin"];
        }

        private Size dgv_Size(Control partent, int row, int col)
        {
            int _row_weigh, _col_hight,_row,_col;//行高和列宽
            int _row_left_weigh, _col_left_hight;
            _row = row;
            _col = col;
            _row_weigh = partent.Size.Width / _col;
            _col_hight = partent.Size.Height / _row;
            _row_left_weigh= partent.Size .Width- _row_weigh* _col;
            _col_left_hight = partent.Size .Height- _col_hight* _row;

            return new Size();
        }

        private void WaferDiskPad_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            if (_Row <= 2 || _Col <= 2)
            {
                MessageBox.Show("Please Input Correct digital !");
                return;
            }

            dataGridView.Size = this.Size;
            int Rowlist = _Row + 4;
            int Collist = _Col + 4;

            unused_Row_weight = (this.Size.Width - _in_margin) / Collist;
            unused_Col_hight = (this.Size.Height - _in_margin) / Rowlist;
            //dataGridView.Size = new Size(unused_Row_weight * Collist, unused_Col_hight * Rowlist);
            //dataGridView.Size.Height =  unused_Col_hight * Rowlist;
            //dataGridView.Size = this.Size;
            //dataGridView.Resize += new EventHandler(Main_Resize);

            //dataGridView.DefaultCellStyle.ForeColor = Color.Red;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //设定背景颜色，fromArgb有多种形式，首位数字位透明项
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, Color.CadetBlue);
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.Red;
            //dataGridView.DefaultCellStyle.ForeColor = Color.LightGoldenrodYellow;
            //dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            //dataGridView.RowsDefaultCellStyle.ForeColor = Color.MediumOrchid;
            //displayView.Controls.Add(dataGridView);


            //X = dataGridView.Width;
            //Y = dataGridView.Height;
            //float newx = (this.Width) / X;
            //float newy = this.Height / Y;

            //setTag(this);
            //setControls(newx, newy, this);
            //Main_Resize(new object(), new EventArgs());//x,y





            //displayView.Show();
            //dataGridView.RowPostPaint += dgv_detail_RowPostPaint;

            #region 设置datagridview的显示特性。包括cell的颜色、大小

            //userControlNoBlink1.Anchor = AnchorStyles.Left|AnchorStyles.Top|AnchorStyles.Right|AnchorStyles.Bottom;
            //userControlNoBlink1.Dock = DockStyle.Fill;
            this.Controls.Add(dataGridView);

            dataGridView.RowHeadersWidth = calculation_Lenght_Mod(Collist, this.Size.Width);
            dataGridView.ColumnHeadersHeight = calculation_Lenght_Mod(Rowlist, this.Size.Height);
            dataGridView.RowHeadersWidth = 4;
            dataGridView.ColumnHeadersHeight = 4;
            RowHight = unused_Col_hight;
            dataGridView.RowTemplate.Height = RowHight;
            ColumnWight = unused_Row_weight;
            
            #endregion

            //dataGridView.double
            //userControlNoBlink1.Controls.Add(dataGridView);
            //设定页面列宽数据，最后两列为Text文字cell

            for (int i = 0; i < Collist - 2; i++)
            {
                dataGridView.Columns.Add(cloummnSet(i));
                DataColumn dataColumn = new DataColumn(Convert.ToString(i), typeof(Image));
                dataColumn.ColumnName = rowSet(i).DataPropertyName;
                Tabledisplay.Columns.Add(dataColumn);
            }
            for (int i = Collist - 2; i < Collist ; i++)
            {
                dataGridView.Columns.Add(rowSet(i));
                DataColumn dataColumn = new DataColumn(Convert.ToString(i), typeof(String));
                dataColumn.ColumnName = rowSet(i).DataPropertyName;
                Tabledisplay.Columns.Add(dataColumn);

            }
            //设定页面行高数据，全部为imageCell，最后两行预备调整为Text文字cell，未实现
            int index = dataGridView.Rows.Add(Rowlist);
            for (int i = 0; i < Rowlist; i++)
            {
                DataRow row = Tabledisplay.NewRow();
                Tabledisplay.Rows.Add(row);

            }
            dataGridView.DataSource = BindingSource;
            
            Tabledisplay.Rows[2][Collist-1] = 8;
            //DataGridViewRow row = new DataGridViewRow();
            //row.Height= RowHight;            
            //DataGridViewTextBoxCell[] dataGridViewTextBoxCells = new DataGridViewTextBoxCell[Collist];
            #region 设定显示数据

            for (int j = 0; j < Rowlist - 3; j += 5)
            {
                dataGridView[Collist - 1, j + 2].Value = Convert.ToString(j + 1);
                dataGridView[Collist - 1, j + 2].Style.BackColor = Color.Orange;
            }
            for (int j = 0; j < Collist - 4; j += 5)
            {
                //dataGridView[j + 2, Rowlist -4].Value = Convert.ToString(j+1);
                dataGridView[j + 2, Rowlist - 1].Style.BackColor = Color.Orange;
            }
            #endregion

            Rectangle rectangle = dataGridView.GetCellDisplayRectangle(3, 7, true);

            //dataGridView.AutoResizeRows();
            //panel1.BackColor = Color.NavajoWhite;
            dataGridView.BackTransparent = true;
            //dataGridView.BackgroundColor =  Color.FromArgb(16,Color.AliceBlue);
            #region 画一个晶圆在Form
            int DrawX = dataGridView.GetCellDisplayRectangle(2, 2, true).X;
            int DrawY = dataGridView.GetCellDisplayRectangle(2, 2, true).Y;
            int DrawWidth = dataGridView.GetCellDisplayRectangle(Collist - 2, Rowlist - 2, true).X - DrawX;
            int DrowHeight = dataGridView.GetCellDisplayRectangle(Collist - 2, Rowlist - 2, true).Y - DrawY;

            BackgroundImage = dataGridView.GetBackImage(dataGridView, ColumnWight * 2, dataGridView.RowTemplate.Height * 2,
                DrawWidth, DrowHeight, Color.LightGray, 10);

            // BackgroundImage = dataGridView.GetBackImage(dataGridView, ColumnWight * 2, dataGridView.RowTemplate.Height * 2,
            //ColumnWight * (Collist - 4), dataGridView.RowTemplate.Height * (Rowlist - 4), Color.LightGray, 10);
            dataGridView.BackgroundImage = BackgroundImage;
            //panel1.BackgroundImage = dataGridView.GetBackImage(this.panel1, 0, 0, this._Row_panel,this._Col_panel);
            //draw_circle.FillEllipse(brush, 0, 0, _Row_panel, _Col_panel);
            #endregion

            dataGridView.Invalidate();
            #region 测试背景颜色变化

            dataGridView[(Collist - 0) / 2, (Rowlist - 0) / 2].Style.BackColor = Color.DarkRed;
            dataGridView[17, 12].Style.BackColor = Color.DarkRed;
            dataGridView[27, 23].Style.BackColor = Color.DarkRed;
            dataGridView[Collist / 2, 7].Style.BackColor = Color.DarkViolet;
            dataGridView[Collist / 2, 21].Style.BackColor = Color.DarkOrchid;
             #endregion

            dataGridView[31, 31].Style.BackColor = Color.DarkSlateBlue;

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

        private DataGridViewImageColumn cloummnSet(int i)
        {
            DataGridViewImageColumn col01 = new DataGridViewImageColumn();
            //col01.MaxInputLength = 3;
            // col01.CellTemplate.Size;
            //col01.HeaderText ="Col"+ Convert.ToString(i);
            col01.DataPropertyName = "Col" + Convert.ToString(i);
            //float a = Convert.ToSingle(ColumnWight) * newx;
            //col01.Width = (int)a;
            col01.Width = ColumnWight;
            return col01;
        }
        private DataGridViewTextBoxColumn rowSet(int i)
        {
            DataGridViewTextBoxColumn col01 = new DataGridViewTextBoxColumn();
            col01.DataPropertyName = "Col" + Convert.ToString(i);
            col01.Width = ColumnWight;
            return col01;
        }
        #region 缩放控件大小

        private void Main_Resize(object sender, EventArgs e)   //Form automatic size
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            //   this.Text = this.Width.ToString() + " " + this.Height.ToString();
            // this.Text = "Jasper Test Station";
        }
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        #endregion
    }
}
