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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static String LastSelectPath = "C:\\Users\\Administrator\\Desktop\\waterheater";
        //所选择的文件的名字带扩展名
        public static String SelectFileName;
        //所选择的文件的全路径
        public static String SelectFileFullName;
        //所选择的文件的名字不带扩展名
        public static String SelectFileNameWE;
        public static Form1 FromInterface = null;

        public Form1()
        {
            InitializeComponent();
        }
        public static Form1 GetInterface()
        {
            if(null == FromInterface)
            {
                FromInterface = new Form1();
            }
            return FromInterface;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = LastSelectPath;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LastSelectPath = Path.GetDirectoryName(this.openFileDialog1.FileName);
                SelectFileFullName = openFileDialog1.FileName;
                SelectFileName = Path.GetFileName(openFileDialog1.FileName);
                SelectFileNameWE = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                FileNameLabel.Text = SelectFileName;//显示文件的名字
                Debug.WriteLine("Select directory:" + LastSelectPath);
                Debug.WriteLine("SelectFileFullName:" + SelectFileFullName);
                Debug.WriteLine("SelectFileName:" + SelectFileName);
                Debug.WriteLine("SelectFileNameWE:" + SelectFileNameWE);
                try
                {
                    this.pictureBox1.Load(SelectFileFullName);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Listbox select item " + listBox1.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(SelectFileFullName);
            Color col = bitmap.GetPixel(0, 0);
            Debug.WriteLine("SelectFileNameWE:" + SelectFileNameWE);
            Debug.WriteLine("col:" + col);
            Debug.WriteLine("Bitmap size " + bitmap.Width + "x" + bitmap.Height);
            PictureCompress.PictureCompressRLE(bitmap);
        }

        public Int32 GetListboxSelectItem()
        {
            Int32 SelectItem;
            SelectItem = listBox1.SelectedIndex;
            if(-1 == SelectItem)
            {
                return 0;
            }
            return SelectItem;

        }
    }
}
