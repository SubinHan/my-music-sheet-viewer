using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MyPdfViewer
{
    public partial class Explorer : Form
    {
        public Explorer()
        {
            InitializeComponent();

            const string path = "D:/문서/한수빈 스캔본";
            string[] files = Directory.GetFiles(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            
            foreach(FileInfo file in dir.GetFiles())
            {
                PictureBox filePicture = new PictureBox();
                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);
                filePicture.Image = icon.ToBitmap();

                flowLayoutPanel1.Controls.Add(filePicture);
            }

            string[] directories = Directory.GetDirectories(path);
            foreach(string file in files)
            {
                Debug.WriteLine(file);
            }
            foreach (string directory in directories)
            {
                Debug.WriteLine(directory);
            }
        }
    }
}
