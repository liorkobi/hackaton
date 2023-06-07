namespace hackaton
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.firstuc1 = new hackaton.firstUC();
            this.seconduc1 = new hackaton.secondUC();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "WELCOME!!!";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(291, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "START SHOPPING";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // firstuc1
            // 
            this.firstuc1.Location = new System.Drawing.Point(12, 31);
            this.firstuc1.Name = "firstuc1";
            this.firstuc1.Size = new System.Drawing.Size(718, 387);
            this.firstuc1.TabIndex = 2;
            this.firstuc1.Visible = false;
            this.firstuc1.Load += new System.EventHandler(this.firstuc1_Load);
            // 
            // seconduc1
            // 
            this.seconduc1.Location = new System.Drawing.Point(12, 12);
            this.seconduc1.Name = "seconduc1";
            this.seconduc1.Size = new System.Drawing.Size(1335, 766);
            this.seconduc1.TabIndex = 3;
            this.seconduc1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 808);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstuc1);
            this.Controls.Add(this.seconduc1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button button1;
        private firstUC firstuc1;
       private secondUC seconduc1;
    }
}