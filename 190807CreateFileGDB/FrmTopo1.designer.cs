namespace RHW.Topo
{
    partial class FrmTopo1
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tbxGDBPath = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cbxFeatureDS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tbxGDBPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFeatureDS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(41, 99);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "选择数据库：";
            // 
            // tbxGDBPath
            // 
            this.tbxGDBPath.Location = new System.Drawing.Point(153, 94);
            this.tbxGDBPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxGDBPath.Name = "tbxGDBPath";
            this.tbxGDBPath.Size = new System.Drawing.Size(370, 30);
            this.tbxGDBPath.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(531, 93);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(63, 36);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "浏览";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(41, 185);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "要素数据集：";
            // 
            // cbxFeatureDS
            // 
            this.cbxFeatureDS.Location = new System.Drawing.Point(153, 181);
            this.cbxFeatureDS.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxFeatureDS.Name = "cbxFeatureDS";
            this.cbxFeatureDS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxFeatureDS.Size = new System.Drawing.Size(441, 30);
            this.cbxFeatureDS.TabIndex = 4;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Enabled = false;
            this.simpleButton2.Location = new System.Drawing.Point(197, 382);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(107, 36);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "上一步";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(313, 382);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(107, 36);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "下一步";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(487, 382);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(107, 36);
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "取消";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // FrmTopo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 453);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.cbxFeatureDS);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.tbxGDBPath);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmTopo1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拓扑检查";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmTopo1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbxGDBPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFeatureDS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbxGDBPath;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbxFeatureDS;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
    }
}