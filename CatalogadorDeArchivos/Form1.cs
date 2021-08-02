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
using System.Data;

namespace CatalogadorDeArchivos
{
    public partial class Form1 : Form
    {
        DataSet dataSet = new DataSet();
        public Form1()
        {
            InitializeComponent();

        }

        private void GetArchivosXML()
        {

            dataSet.ReadXml(@"C:\xml\archivos.xml");

            dataGridView1.DataSource = dataSet.Tables[0];
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
            

            foreach (FileSystemInfo f in dir.GetFileSystemInfos())
            {
                if (f is FileInfo)
                {
                    if ((f.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        FileInfo j = f as FileInfo;
                        str += "" + f.Name + " - " + j.Length + "\r\n";

                        //listViewArchivos.Items.Add("valor1");

                        XmlElement archivo = doc.CreateElement("archivo");
                        raiz.AppendChild(archivo);

                        XmlElement nombre = doc.CreateElement("nombre");
                        nombre.AppendChild(doc.CreateTextNode(f.FullName));
                        archivo.AppendChild(nombre);

                        XmlElement extension = doc.CreateElement("extension");
                        extension.AppendChild(doc.CreateTextNode(f.Extension));
                        archivo.AppendChild(extension);

                        XmlElement descripcion = doc.CreateElement("descripcion");
                        descripcion.AppendChild(doc.CreateTextNode(""));
                        archivo.AppendChild(descripcion);

                        XmlElement tipo = doc.CreateElement("Tipo");
                        tipo.AppendChild(doc.CreateTextNode(getFileType(f.Extension).ToString()));
                        archivo.AppendChild(tipo);


                    }

                }
                else
                {
                    //str += "\\" + f.Name + "\r\n";
                }
            }

            doc.Save("c:\\xml\\archivos.xml");
            return str;
        }

        private static string getFileType(string extension)
        {
            if (extension == ".pdf")
            {
                return "Portable document format";
            } 
            if (extension == ".xls")
            {
                return "Documento de planilla de cálculos";
            }
            if (extension == ".csv")
            {
                return "Comma separated values";
            }
            if (extension == ".jpg" || extension == ".png")
            {
                return "Archivo de imágen";
            }
                return "Desconocido";

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
            treeView1.Nodes.Clear();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string path = fbd.SelectedPath;
            if (path != "")
            {
                textBoxArchivos.Text = GetFiles(path);
                //treeView1.Nodes.Add(GetFiles(path));
            }

            AppendDirectoriesToTreeNode(treeView1.Nodes.Add("Explorar"), path);
            GetArchivosXML();
        }

        private void guardarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
                {
                }

        private void crearDirectoriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) { return; }

            //con la línea siguiente invoco al dialogo de explorar y habilito la creación de directorios y los agrego al treeview
            //this.treeView1.Nodes.Add(TraverseDirectory(dialog.SelectedPath));
            //aquí solo llamo al explorador y agrego directorios
            TraverseDirectory(dialog.SelectedPath);
        }
        private TreeNode TraverseDirectory(string path)
        {
            TreeNode result = new TreeNode(path);
            foreach (var subdirectory in Directory.GetDirectories(path))
            {
                result.Nodes.Add(TraverseDirectory(subdirectory));
            }

            return result;
        }

        protected void AppendDirectoriesToTreeNode(TreeNode node, string root)
        {
            DirectoryInfo rootDir = new DirectoryInfo(root);

            foreach (DirectoryInfo subDir in rootDir.GetDirectories())
            {
                TreeNode subdirNode = new TreeNode(subDir.Name);
                AppendDirectoriesToTreeNode(subdirNode, subDir.FullName);

                foreach (FileInfo fileInfo in subDir.GetFiles())
                {
                    subdirNode.Nodes.Add(fileInfo.Name);
                }

                node.Nodes.Add(subdirNode);
            }
        }

        public static void GetDisksInfo(ListView listView)
        {

            //DriveInfo para obtener info de discos

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            int id = 0;
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    ListViewItem lvi = new ListViewItem(d.Name);
                    lvi.SubItems.Add(d.VolumeLabel);
                    //lvi.SubItems.Add((d.AvailableFreeSpace/1024/1024).ToString());
                    lvi.SubItems.Add((d.TotalSize / 1024 / 1024).ToString());
                    lvi.SubItems.Add((d.AvailableFreeSpace / 1024/1024).ToString());
                    listView.Items.Add(lvi);

                }
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetDisksInfo(listView1);
            GetArchivosXML();
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string archivoName;
            archivoName = this.Text;
            Console.WriteLine(archivoName);
        }
    }
}
