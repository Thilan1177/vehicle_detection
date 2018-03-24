namespace ECV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recognizeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonImageRecognition = new System.Windows.Forms.Button();
            this.buttonImageProcessing = new System.Windows.Forms.Button();
            this.labelImageTrain = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.programToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(647, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processImageToolStripMenuItem,
            this.recognizeImageToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // processImageToolStripMenuItem
            // 
            this.processImageToolStripMenuItem.Name = "processImageToolStripMenuItem";
            this.processImageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.processImageToolStripMenuItem.Text = "Process Image";
            this.processImageToolStripMenuItem.Click += new System.EventHandler(this.processImageToolStripMenuItem_Click);
            // 
            // recognizeImageToolStripMenuItem
            // 
            this.recognizeImageToolStripMenuItem.Name = "recognizeImageToolStripMenuItem";
            this.recognizeImageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.recognizeImageToolStripMenuItem.Text = "Recognize Image";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // companyToolStripMenuItem
            // 
            this.companyToolStripMenuItem.Name = "companyToolStripMenuItem";
            this.companyToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.companyToolStripMenuItem.Text = "Company";
            // 
            // buttonImageRecognition
            // 
            this.buttonImageRecognition.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageRecognition.Image")));
            this.buttonImageRecognition.Location = new System.Drawing.Point(343, 39);
            this.buttonImageRecognition.Name = "buttonImageRecognition";
            this.buttonImageRecognition.Size = new System.Drawing.Size(247, 206);
            this.buttonImageRecognition.TabIndex = 2;
            this.buttonImageRecognition.UseVisualStyleBackColor = true;
            this.buttonImageRecognition.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonImageProcessing
            // 
            this.buttonImageProcessing.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageProcessing.Image")));
            this.buttonImageProcessing.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonImageProcessing.Location = new System.Drawing.Point(49, 39);
            this.buttonImageProcessing.Name = "buttonImageProcessing";
            this.buttonImageProcessing.Size = new System.Drawing.Size(248, 206);
            this.buttonImageProcessing.TabIndex = 1;
            this.buttonImageProcessing.UseVisualStyleBackColor = true;
            this.buttonImageProcessing.Click += new System.EventHandler(this.buttonImageProcessing_Click);
            // 
            // labelImageTrain
            // 
            this.labelImageTrain.AutoSize = true;
            this.labelImageTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImageTrain.Location = new System.Drawing.Point(63, 248);
            this.labelImageTrain.Name = "labelImageTrain";
            this.labelImageTrain.Size = new System.Drawing.Size(143, 18);
            this.labelImageTrain.TabIndex = 3;
            this.labelImageTrain.Text = "Image Processing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(386, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Image Recognition";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 278);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelImageTrain);
            this.Controls.Add(this.buttonImageRecognition);
            this.Controls.Add(this.buttonImageProcessing);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Menu";
            this.Text = "Menu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recognizeImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem companyToolStripMenuItem;
        private System.Windows.Forms.Button buttonImageProcessing;
        private System.Windows.Forms.Button buttonImageRecognition;
        private System.Windows.Forms.Label labelImageTrain;
        private System.Windows.Forms.Label label2;
    }
}