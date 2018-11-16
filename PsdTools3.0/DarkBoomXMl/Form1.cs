using PSDUIImporter;
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

namespace DarkBoomXMl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public StringBuilder sb = new StringBuilder();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(xml文件)|*.xml;";

            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.textBox1.Text = fileDialog.FileName;
            sb = new StringBuilder();

            PSDUI psdUI = (PSDUI)PSDImportUtility.DeserializeXml(fileDialog.FileName, typeof(PSDUI));
            LogLayer(psdUI.layers);

            string path = System.IO.Directory.GetCurrentDirectory() + "/OutPut/" +
                fileDialog.SafeFileName.Substring(0, fileDialog.SafeFileName.Length - 4) + ".txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            StreamWriter sw = File.CreateText(path);
            sw.Write(sb.ToString());
            sw.Dispose();
            sw.Close();

            System.Diagnostics.Process.Start(path);
        }


        void LogLayer(Layer[] layers)
        {
            foreach (Layer item in layers)
            {
                if (item.image != null)
                    sb.AppendLine(item.image.name.PadRight(80, ' ') + "  " + item.image.position.ToString());
                if (item.layers != null)
                {
                    LogLayer(item.layers);
                }
            }
        }
    }
}
