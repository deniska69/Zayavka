namespace Project
{
    partial class MSBXYN_NC
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
            this.msbY = new System.Windows.Forms.Button();
            this.msbN = new System.Windows.Forms.Button();
            this.msbL0 = new System.Windows.Forms.Label();
            this.msbL1 = new System.Windows.Forms.Label();
            this.msbL2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // msbY
            // 
            this.msbY.Location = new System.Drawing.Point(117, 203);
            this.msbY.Name = "msbY";
            this.msbY.Size = new System.Drawing.Size(75, 23);
            this.msbY.TabIndex = 0;
            this.msbY.Text = "Да";
            this.msbY.UseVisualStyleBackColor = true;
            this.msbY.Click += new System.EventHandler(this.msbY_Click);
            // 
            // msbN
            // 
            this.msbN.Location = new System.Drawing.Point(231, 203);
            this.msbN.Name = "msbN";
            this.msbN.Size = new System.Drawing.Size(75, 23);
            this.msbN.TabIndex = 1;
            this.msbN.Text = "Нет";
            this.msbN.UseVisualStyleBackColor = true;
            this.msbN.Click += new System.EventHandler(this.msbN_Click);
            // 
            // msbL0
            // 
            this.msbL0.AutoSize = true;
            this.msbL0.Location = new System.Drawing.Point(29, 18);
            this.msbL0.Name = "msbL0";
            this.msbL0.Size = new System.Drawing.Size(371, 13);
            this.msbL0.TabIndex = 2;
            this.msbL0.Text = "Вы уверены, что хотите сохранить карту со следующими параметрами:";
            // 
            // msbL1
            // 
            this.msbL1.AutoSize = true;
            this.msbL1.Location = new System.Drawing.Point(122, 47);
            this.msbL1.Name = "msbL1";
            this.msbL1.Size = new System.Drawing.Size(57, 13);
            this.msbL1.TabIndex = 3;
            this.msbL1.Text = "Названия";
            this.msbL1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // msbL2
            // 
            this.msbL2.AutoSize = true;
            this.msbL2.Location = new System.Drawing.Point(204, 47);
            this.msbL2.Name = "msbL2";
            this.msbL2.Size = new System.Drawing.Size(37, 13);
            this.msbL2.TabIndex = 4;
            this.msbL2.Text = "Текст";
            this.msbL2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MSBXYN_NC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 231);
            this.ControlBox = false;
            this.Controls.Add(this.msbL2);
            this.Controls.Add(this.msbL1);
            this.Controls.Add(this.msbL0);
            this.Controls.Add(this.msbN);
            this.Controls.Add(this.msbY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MSBXYN_NC";
            this.ShowInTaskbar = false;
            this.Text = "Подтверждение";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MSBXYN_NC_FormClosing);
            //this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MSBXYN_FormClosed);
            this.Load += new System.EventHandler(this.MSBXYN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button msbY;
        private System.Windows.Forms.Button msbN;
        private System.Windows.Forms.Label msbL0;
        private System.Windows.Forms.Label msbL1;
        private System.Windows.Forms.Label msbL2;
    }
}