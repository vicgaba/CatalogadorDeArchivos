using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatalogadorDeArchivos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
                
        }


        public static string GetFiles(string path)
        {
            string str = "";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileSystemInfo f in dir.GetFileSystemInfos())
            {
                if (f is FileInfo)
                {

                    FileInfo j = f as FileInfo;
                    str += "" + f.Name + " - " + j.Length + "\r\n";
                }
                else
                {
                    str += "\\" + f.Name + "\r\n";
                }
            }
            return str;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void textBoxExplorador_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxExplorador_Load(Object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string path = fbd.SelectedPath;
            if (path != "")
            {
                textBoxArchivos.Text = GetFiles(path);
            }
        }
    }
}
