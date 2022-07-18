﻿using System;
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
using System.Runtime.InteropServices;

namespace MyPdfViewer
{
    public partial class Explorer : Form
    {
        public Explorer()
        {
            InitializeComponent();
            UpdateExplorer(GetLatestPath());
        }

        private string GetLatestPath()
        {
            return "E:/문서";
        }

        private void UpdateExplorer(string path)
        {
            flowLayoutPanel1.Controls.Clear();

            string[] files = Directory.GetFiles(path);
            DirectoryInfo root = new DirectoryInfo(path);

            foreach (DirectoryInfo dir in root.GetDirectories())
            {
                PictureBox dirPicture = new PictureBox();
                Icon icon = DefaultIcons.GetStockIcon(DefaultIcons.SHSIID_FOLDER, DefaultIcons.SHGSI_SMALLICON);
                dirPicture.Image = icon.ToBitmap();
                dirPicture.Name = dir.FullName;
                dirPicture.Click += OpenDir;

                flowLayoutPanel1.Controls.Add(dirPicture);
            }

            foreach (FileInfo file in root.GetFiles())
            {
                Debug.Print(file.Extension);

                if (!file.Extension.ToLower().Equals(".pdf"))
                    continue;

                PictureBox filePicture = new PictureBox();
                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);
                filePicture.Image = icon.ToBitmap();
                filePicture.Name = file.FullName;
                filePicture.Click += OpenPdf;

                flowLayoutPanel1.Controls.Add(filePicture);
            }

        }

        private void OpenDir(object sender, EventArgs e)
        {
            PictureBox dir = sender as PictureBox;
            UpdateExplorer(dir.Name);
        }

        private void OpenPdf(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;
        }
    }
}

public static class DefaultIcons
{
    private static Icon folderIcon;

    public static Icon FolderLarge => folderIcon ?? (folderIcon = GetStockIcon(SHSIID_FOLDER, SHGSI_LARGEICON));

    public static Icon GetStockIcon(uint type, uint size)
    {
        var info = new SHSTOCKICONINFO();
        info.cbSize = (uint)Marshal.SizeOf(info);

        SHGetStockIconInfo(type, SHGSI_ICON | size, ref info);

        var icon = (Icon)Icon.FromHandle(info.hIcon).Clone(); // Get a copy that doesn't use the original handle
        DestroyIcon(info.hIcon); // Clean up native icon to prevent resource leak

        return icon;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SHSTOCKICONINFO
    {
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysIconIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

    [DllImport("shell32.dll")]
    public static extern int SHGetStockIconInfo(uint siid, uint uFlags, ref SHSTOCKICONINFO psii);

    [DllImport("user32.dll")]
    public static extern bool DestroyIcon(IntPtr handle);

    public const uint SHSIID_FOLDER = 0x3;
    public const uint SHGSI_ICON = 0x100;
    public const uint SHGSI_LARGEICON = 0x0;
    public const uint SHGSI_SMALLICON = 0x1;
}
