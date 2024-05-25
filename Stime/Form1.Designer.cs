namespace Stime
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.InputImage = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ResultImage = new System.Windows.Forms.PictureBox();
            this.ListBiodatas = new System.Windows.Forms.Panel();
            this.TubesStima = new System.Windows.Forms.Label();
            this.ChooseImageButton = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.BMButton = new System.Windows.Forms.Button();
            this.KMPButton = new System.Windows.Forms.Button();
            this.SearchButtonFinal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // InputImage
            // 
            this.InputImage.BackColor = System.Drawing.Color.White;
            this.InputImage.Location = new System.Drawing.Point(28, 90);
            this.InputImage.Name = "InputImage";
            this.InputImage.Size = new System.Drawing.Size(300, 400);
            this.InputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.InputImage.TabIndex = 0;
            this.InputImage.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(547, 47);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(0, 0);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // ResultImage
            // 
            this.ResultImage.BackColor = System.Drawing.Color.White;
            this.ResultImage.Location = new System.Drawing.Point(351, 90);
            this.ResultImage.Name = "ResultImage";
            this.ResultImage.Size = new System.Drawing.Size(300, 400);
            this.ResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ResultImage.TabIndex = 2;
            this.ResultImage.TabStop = false;
            // 
            // ListBiodatas
            // 
            this.ListBiodatas.BackColor = System.Drawing.Color.White;
            this.ListBiodatas.Location = new System.Drawing.Point(674, 90);
            this.ListBiodatas.Name = "ListBiodatas";
            this.ListBiodatas.Size = new System.Drawing.Size(300, 400);
            this.ListBiodatas.TabIndex = 3;
            // 
            // TubesStima
            // 
            this.TubesStima.AutoSize = true;
            this.TubesStima.BackColor = System.Drawing.Color.Transparent;
            this.TubesStima.Font = new System.Drawing.Font("Book Antiqua", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TubesStima.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TubesStima.Location = new System.Drawing.Point(24, 35);
            this.TubesStima.Name = "TubesStima";
            this.TubesStima.Size = new System.Drawing.Size(351, 34);
            this.TubesStima.TabIndex = 4;
            this.TubesStima.Text = "Tubes 3 Strategi Algoritma";
            // 
            // ChooseImageButton
            // 
            this.ChooseImageButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ChooseImageButton.Location = new System.Drawing.Point(28, 522);
            this.ChooseImageButton.Name = "ChooseImageButton";
            this.ChooseImageButton.Size = new System.Drawing.Size(300, 50);
            this.ChooseImageButton.TabIndex = 5;
            this.ChooseImageButton.Text = "Choose an Image";
            this.ChooseImageButton.UseVisualStyleBackColor = false;
            this.ChooseImageButton.Click += new System.EventHandler(this.chooseImage_Click);
            // 
            // BMButton
            // 
            this.BMButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BMButton.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BMButton.Location = new System.Drawing.Point(351, 522);
            this.BMButton.Name = "BMButton";
            this.BMButton.Size = new System.Drawing.Size(144, 50);
            this.BMButton.TabIndex = 6;
            this.BMButton.Text = "Boyer-Moore";
            this.BMButton.UseVisualStyleBackColor = false;
            this.BMButton.Click += new System.EventHandler(this.BM_Click);
            // 
            // KMPButton
            // 
            this.KMPButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.KMPButton.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KMPButton.Location = new System.Drawing.Point(507, 522);
            this.KMPButton.Name = "KMPButton";
            this.KMPButton.Size = new System.Drawing.Size(144, 50);
            this.KMPButton.TabIndex = 7;
            this.KMPButton.Text = "Knuth-Morris-Pratt";
            this.KMPButton.UseVisualStyleBackColor = false;
            this.KMPButton.Click += new System.EventHandler(this.KMP_Click);
            // 
            // SearchButtonFinal
            // 
            this.SearchButtonFinal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SearchButtonFinal.Location = new System.Drawing.Point(674, 522);
            this.SearchButtonFinal.Name = "SearchButtonFinal";
            this.SearchButtonFinal.Size = new System.Drawing.Size(300, 50);
            this.SearchButtonFinal.TabIndex = 8;
            this.SearchButtonFinal.Text = "Search";
            this.SearchButtonFinal.UseVisualStyleBackColor = false;
            this.SearchButtonFinal.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.SearchButtonFinal);
            this.Controls.Add(this.KMPButton);
            this.Controls.Add(this.BMButton);
            this.Controls.Add(this.ChooseImageButton);
            this.Controls.Add(this.TubesStima);
            this.Controls.Add(this.ListBiodatas);
            this.Controls.Add(this.ResultImage);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.InputImage);
            this.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox InputImage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox ResultImage;
        private System.Windows.Forms.Panel ListBiodatas;
        private System.Windows.Forms.Label TubesStima;
        private System.Windows.Forms.Button ChooseImageButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button BMButton;
        private System.Windows.Forms.Button KMPButton;
        private System.Windows.Forms.Button SearchButtonFinal;
    }
}

