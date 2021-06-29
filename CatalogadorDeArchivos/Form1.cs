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
using System.Xml;

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
            //genero mi xml para ir cargando los archivos que encuentre
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Archivos");
            doc.AppendChild(raiz);
            XmlElement archivo = doc.CreateElement("archivo");
            raiz.AppendChild(archivo);

            foreach (FileSystemInfo f in dir.GetFileSystemInfos())
            {
                if (f is FileInfo)
                {
                   

                    FileInfo j = f as FileInfo;
                    str += "" + f.Name + " - " + j.Length + "\r\n";

                    XmlElement nombre = doc.CreateElement("nombre");
                    nombre.AppendChild(doc.CreateTextNode(f.Name));
                    archivo.AppendChild(nombre);

                    XmlElement extension = doc.CreateElement("extension");
                    extension.AppendChild(doc.CreateTextNode(f.Extension));
                    archivo.AppendChild(extension);

                    XmlElement descripcion = doc.CreateElement("descripcion");
                    descripcion.AppendChild(doc.CreateTextNode(""));
                    archivo.AppendChild(descripcion);
                }
                else
                {
                    str += "\\" + f.Name + "\r\n";
                }
            }

            doc.Save("c:\\xml\\archivos.xml");
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
           
        }

        private void explorarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string path = fbd.SelectedPath;
            if (path != "")
            {
                textBoxArchivos.Text = GetFiles(path);
            }
        }

        private void guardarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }
    }
}
