using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Fungsi Input Image
        private void chooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    // Display image in PictureBox
                    InputImage.Image = Image.FromFile(filePath);
                }
            }
        }
        
        // BM Mode Click
        private void BM_Click(object sender, EventArgs e)
        {

        }

        // KMP Mode Click
        private void KMP_Click(object sender, EventArgs e)
        {

        }

        // Search Button
        private void searchButton_Click(object sender, EventArgs e)
        {

        }
    }
}
