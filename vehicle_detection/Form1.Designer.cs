namespace vehicle_detection
{
    partial class Menu
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
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imagebutton = new System.Windows.Forms.Button();
            this.backgroundbutton = new System.Windows.Forms.Button();
            this.processbutton = new System.Windows.Forms.Button();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogBg = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 24);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(311, 270);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(373, 24);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(317, 270);
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 354);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(429, 108);
            this.textBox1.TabIndex = 3;
            // 
            // imagebutton
            // 
            this.imagebutton.Location = new System.Drawing.Point(126, 310);
            this.imagebutton.Name = "imagebutton";
            this.imagebutton.Size = new System.Drawing.Size(75, 23);
            this.imagebutton.TabIndex = 4;
            this.imagebutton.Text = "Image";
            this.imagebutton.UseVisualStyleBackColor = true;
            this.imagebutton.Click += new System.EventHandler(this.imagebutton_Click_1);
            // 
            // backgroundbutton
            // 
            this.backgroundbutton.Location = new System.Drawing.Point(494, 310);
            this.backgroundbutton.Name = "backgroundbutton";
            this.backgroundbutton.Size = new System.Drawing.Size(75, 23);
            this.backgroundbutton.TabIndex = 5;
            this.backgroundbutton.Text = "Background";
            this.backgroundbutton.UseVisualStyleBackColor = true;
            this.backgroundbutton.Click += new System.EventHandler(this.backgroundbutton_Click_1);
            // 
            // processbutton
            // 
            this.processbutton.Location = new System.Drawing.Point(494, 370);
            this.processbutton.Name = "processbutton";
            this.processbutton.Size = new System.Drawing.Size(88, 62);
            this.processbutton.TabIndex = 6;
            this.processbutton.Text = "Process";
            this.processbutton.UseVisualStyleBackColor = true;
            this.processbutton.Click += new System.EventHandler(this.processbutton_Click);
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.FileName = "openFileDialogImage";
            this.openFileDialogImage.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openFileDialogBg
            // 
            this.openFileDialogBg.FileName = "openFileDialogBg";
            this.openFileDialogBg.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogBg_FileOk);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 499);
            this.Controls.Add(this.processbutton);
            this.Controls.Add(this.backgroundbutton);
            this.Controls.Add(this.imagebutton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Name = "Menu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button imagebutton;
        private System.Windows.Forms.Button backgroundbutton;
        private System.Windows.Forms.Button processbutton;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.OpenFileDialog openFileDialogBg;
    }
}

