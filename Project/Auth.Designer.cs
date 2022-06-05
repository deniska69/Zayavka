namespace Project
{
    partial class Auth
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auth));
            this.Atxb1 = new System.Windows.Forms.TextBox();
            this.Albl1 = new System.Windows.Forms.Label();
            this.Acmbx1 = new System.Windows.Forms.ComboBox();
            this.Albl2 = new System.Windows.Forms.Label();
            this.Atlt1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Atxb1
            // 
            this.Atxb1.Location = new System.Drawing.Point(62, 56);
            this.Atxb1.MaxLength = 8;
            this.Atxb1.Name = "Atxb1";
            this.Atxb1.PasswordChar = '●';
            this.Atxb1.Size = new System.Drawing.Size(121, 20);
            this.Atxb1.TabIndex = 1;
            this.Atxb1.TextChanged += new System.EventHandler(this.Atxb1_TextChanged);
            // 
            // Albl1
            // 
            this.Albl1.AutoSize = true;
            this.Albl1.Location = new System.Drawing.Point(94, 10);
            this.Albl1.Name = "Albl1";
            this.Albl1.Size = new System.Drawing.Size(68, 13);
            this.Albl1.TabIndex = 2;
            this.Albl1.Text = "Authorization";
            // 
            // Acmbx1
            // 
            this.Acmbx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Acmbx1.FormattingEnabled = true;
            this.Acmbx1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Acmbx1.Location = new System.Drawing.Point(62, 29);
            this.Acmbx1.Name = "Acmbx1";
            this.Acmbx1.Size = new System.Drawing.Size(121, 21);
            this.Acmbx1.TabIndex = 3;
            this.Acmbx1.SelectedIndexChanged += new System.EventHandler(this.Acmbx1_SelectedIndexChanged);
            // 
            // Albl2
            // 
            this.Albl2.AutoSize = true;
            this.Albl2.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold);
            this.Albl2.Location = new System.Drawing.Point(218, 57);
            this.Albl2.Name = "Albl2";
            this.Albl2.Size = new System.Drawing.Size(15, 17);
            this.Albl2.TabIndex = 4;
            this.Albl2.Text = "?";
            // 
            // Auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 88);
            this.Controls.Add(this.Albl2);
            this.Controls.Add(this.Acmbx1);
            this.Controls.Add(this.Albl1);
            this.Controls.Add(this.Atxb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Auth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заявка v.1.0";
            this.Load += new System.EventHandler(this.Auth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Atxb1;
        private System.Windows.Forms.Label Albl1;
        private System.Windows.Forms.ComboBox Acmbx1;
        private System.Windows.Forms.Label Albl2;
        private System.Windows.Forms.ToolTip Atlt1;
    }
}