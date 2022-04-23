namespace Game_xiaoxiaole
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panelGame = new System.Windows.Forms.Panel();
            this.btnAboutme = new System.Windows.Forms.Button();
            this.labelNum = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GameForArea = new Game_xiaoxiaole.UserControlNoBlink();
            this.AreaForGame = new Game_xiaoxiaole.UserControlNoBlink();
            this.panelGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelGame.BackgroundImage")));
            this.panelGame.Controls.Add(this.btnAboutme);
            this.panelGame.Controls.Add(this.labelNum);
            this.panelGame.Controls.Add(this.btnStart);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGame.Location = new System.Drawing.Point(847, 20);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(188, 906);
            this.panelGame.TabIndex = 1;
            // 
            // btnAboutme
            // 
            this.btnAboutme.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAboutme.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAboutme.Image = ((System.Drawing.Image)(resources.GetObject("btnAboutme.Image")));
            this.btnAboutme.Location = new System.Drawing.Point(27, 189);
            this.btnAboutme.Name = "btnAboutme";
            this.btnAboutme.Size = new System.Drawing.Size(155, 40);
            this.btnAboutme.TabIndex = 3;
            this.btnAboutme.Text = "关于我";
            this.btnAboutme.UseVisualStyleBackColor = true;
            this.btnAboutme.Click += new System.EventHandler(this.btnAboutme_Click);
            // 
            // labelNum
            // 
            this.labelNum.Font = new System.Drawing.Font("楷体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNum.ForeColor = System.Drawing.Color.Maroon;
            this.labelNum.Image = ((System.Drawing.Image)(resources.GetObject("labelNum.Image")));
            this.labelNum.Location = new System.Drawing.Point(27, 326);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(155, 40);
            this.labelNum.TabIndex = 4;
            this.labelNum.Text = "SCORE\r\n";
            this.labelNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.Location = new System.Drawing.Point(27, 63);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(155, 42);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始游戏";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(671, -3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(41, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Tag = "输入行数";
            this.textBox1.Text = "35";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(790, -3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(41, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.Tag = "输入列数";
            this.textBox2.Text = "35";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(951, -1);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(100, 34);
            this.btn_Reset.TabIndex = 6;
            this.btn_Reset.Tag = "重置按钮";
            this.btn_Reset.Text = "重置按钮";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(2, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 322);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(669, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Row";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(790, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Column";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(619, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "Row";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameForArea
            // 
            this.GameForArea.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.GameForArea.Location = new System.Drawing.Point(73, 44);
            this.GameForArea.Name = "GameForArea";
            this.GameForArea.Size = new System.Drawing.Size(768, 640);
            this.GameForArea.TabIndex = 2;
            this.GameForArea.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForArea_Paint);
            this.GameForArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameForArea_MouseDown);
            this.GameForArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameForArea_MouseMove);
            this.GameForArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameForArea_MouseUp);
            // 
            // AreaForGame
            // 
            this.AreaForGame.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.AreaForGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AreaForGame.Location = new System.Drawing.Point(20, 20);
            this.AreaForGame.Name = "AreaForGame";
            this.AreaForGame.Size = new System.Drawing.Size(653, 548);
            this.AreaForGame.TabIndex = 2;
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(1055, 946);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.GameForArea);
            this.Controls.Add(this.panelGame);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消消乐";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelGame.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelRight;
        private UserControlNoBlink GameArea;
        private System.Windows.Forms.Panel panelGame;
        private UserControlNoBlink AreaForGame;
        private UserControlNoBlink GameForArea;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label labelNum;
        private System.Windows.Forms.Button btnAboutme;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}

