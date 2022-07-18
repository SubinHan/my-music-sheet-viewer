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
    public partial class ExplorerItem : UserControl
    {
        private PictureBox picture;
        private TextBox name;
        private string path;

        private Icon icon;
        private string itemName;

        public Icon Icon
        {
            get { return icon; }
            set 
            { 
                icon = value;
                picture.Image = icon.ToBitmap();
            }
        }
        public string ItemName 
        {
            get => itemName; 
            set
            {
                itemName = value;
                name.Text = itemName;
            }
        }
        public string Path
        {
            get => path;
            set => path = value;
        }

        public ExplorerItem()
        {
            InitializeComponent();
            WireAllControls(this);
        }
        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.Click += ControlClick;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }
        private void ControlClick(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
        }
    }
}
