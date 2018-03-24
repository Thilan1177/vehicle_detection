namespace ECV
{
    partial class Recognition
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
            this.OriginalImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImageBox
            // 
            this.OriginalImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OriginalImageBox.Location = new System.Drawing.Point(22, 11);
            this.OriginalImageBox.Name = "OriginalImageBox";
            this.OriginalImageBox.Size = new System.Drawing.Size(448, 343);
            this.OriginalImageBox.TabIndex = 3;
            this.OriginalImageBox.TabStop = false;
            // 
            // Recognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 502);
            this.Controls.Add(this.OriginalImageBox);
            this.Name = "Recognition";
            this.Text = "Recognition";
            this.Load += new System.EventHandler(this.Recognition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox OriginalImageBox;
    }
}