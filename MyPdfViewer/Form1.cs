using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPdfViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SizeChanged += Pdf_SizeChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                PdfViewer.src = fd.FileName;
                PdfViewer.setLayoutMode("TwoColumnLeft");
                PdfViewer.setView("FitV");
            }
            else
            {
                ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PdfViewer.gotoNextPage();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {
        }

        private void Pdf_SizeChanged(object sender, EventArgs e)
        {
            Size size = new Size(this.Size.Width, this.Size.Height);
            PdfViewer.Size = size;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Explorer explorer = new Explorer();
            explorer.Show();
        }
    }
}
