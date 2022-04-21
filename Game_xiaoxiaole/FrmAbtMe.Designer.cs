namespace Game_xiaoxiaole
{
    partial class FrmAbtMe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbtMe));
            this.picMe = new System.Windows.Forms.PictureBox();
            this.labelMe = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picMe)).BeginInit();
            this.SuspendLayout();
            // 
            // picMe
            // 
            this.picMe.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMe.BackgroundImage")));
            this.picMe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMe.Location = new System.Drawing.Point(49, 57);
            this.picMe.Name = "picMe";
            this.picMe.Size = new System.Drawing.Size(119, 167);
            this.picMe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picMe.TabIndex = 0;
            this.picMe.TabStop = false;
            this.picMe.Click += new System.EventHandler(this.picMe_Click);
            // 
            // labelMe
            // 
            this.labelMe.AutoSize = true;
            this.labelMe.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelMe.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMe.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelMe.Location = new System.Drawing.Point(218, 68);
            this.labelMe.Name = "labelMe";
            this.labelMe.Size = new System.Drawing.Size(202, 24);
            this.labelMe.TabIndex = 1;
            this.labelMe.Text = "学号：3018209278";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelName.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelName.ForeColor = System.Drawing.Color.Black;
            this.labelName.Location = new System.Drawing.Point(218, 129);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(154, 24);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "姓名：冯婧妮";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Black;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(250, 261);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 41);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(218, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "班级：信息管理与信息系统1班";
            // 
            // FrmAbtMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game_xiaoxiaole.Properties.Resources.backg;
            this.ClientSize = new System.Drawing.Size(588, 335);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelMe);
            this.Controls.Add(this.picMe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAbtMe";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About me";
            ((System.ComponentModel.ISupportInitialize)(this.picMe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMe;
        private System.Windows.Forms.Label labelMe;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
    }
}