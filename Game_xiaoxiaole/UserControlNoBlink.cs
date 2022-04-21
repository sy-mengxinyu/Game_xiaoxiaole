using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_xiaoxiaole
{
    public partial class UserControlNoBlink : UserControl
    {
        public UserControlNoBlink()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);//用户自己绘制
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);                  
       }

        private void UserControlNoBlink_Load(object sender, EventArgs e)
        {

        }
    }
}
