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
        private Size _size ;

        public Size Size 
        {
            get { return _size; }
            set { _size = value; }
        }

        #endregion
        
        #region 私有变量
        DoubleBufferDataGridView dataGridView;
        //panel尺寸大小
        private int _Row_panel = 768, _Col_panel = 768;

        #endregion
        private void WaferDiskPad_Load(object sender, EventArgs e)
        {
            dataGridView = new DoubleBufferDataGridView
            {
                //Dock = DockStyle.Fill,
                Size = this.Size,
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
            this.Controls.Add(dataGridView);

        }

        public WaferDiskPad ()
        {
            InitializeComponent();
        }

    }
}
