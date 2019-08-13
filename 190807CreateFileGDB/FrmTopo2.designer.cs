namespace RHW.Topo
{
    partial class FrmTopo2
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(199, 382);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(107, 36);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "上一步";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(314, 382);
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
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(149, 64);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(446, 30);
            this.textEdit1.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(40, 69);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 22);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "拓扑名称：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(40, 138);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(126, 22);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "参与拓扑要素：";
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.CheckOnClick = true;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(40, 170);
            this.checkedListBoxControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(446, 149);
            this.checkedListBoxControl1.TabIndex = 11;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(494, 193);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(107, 36);
            this.simpleButton1.TabIndex = 12;
            this.simpleButton1.Text = "全选";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(494, 255);
            this.simpleButton5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(107, 36);
            this.simpleButton5.TabIndex = 13;
            this.simpleButton5.Text = "全部取消";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // FrmTopo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 453);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmTopo2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "拓扑检查";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmTopo2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
    }
}