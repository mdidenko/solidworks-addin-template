namespace CAPP.Graphic
{
    partial class TaskpaneUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskpaneUI));
            this.DocButton = new System.Windows.Forms.Button();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // DocButton
            // 
            this.DocButton.Location = new System.Drawing.Point(116, 138);
            this.DocButton.Name = "DocButton";
            this.DocButton.Size = new System.Drawing.Size(268, 166);
            this.DocButton.TabIndex = 0;
            this.DocButton.Text = "DocButton";
            this.DocButton.UseVisualStyleBackColor = true;
            this.DocButton.Click += new System.EventHandler(this.DocButton_Click);
            // 
            // Logo
            // 
            this.Logo.ErrorImage = null;
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.InitialImage = null;
            this.Logo.Location = new System.Drawing.Point(196, 27);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(100, 50);
            this.Logo.TabIndex = 1;
            this.Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 2;
            // 
            // TaskpaneUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.DocButton);
            this.Name = "TaskpaneUI";
            this.Size = new System.Drawing.Size(500, 1000);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DocButton;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label label1;
    }
}
