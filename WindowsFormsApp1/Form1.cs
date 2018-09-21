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
        public String LastSelectPath = "C:\\Users\\Administrator\\Desktop\\waterheater";
        //所选择的文件的名字带扩展名
        public String SelectFileName = "";
        //所选择的文件的全路径
        public String SelectFileFullName = "";
        //所选择的文件的名字不带扩展名
        public String SelectFileNameWE = "";
        public static Form1 FromInterface = null;

        public Form1()
        {
            InitializeComponent();
            ListboxCompress.SelectedIndex = 0;
        }
        public static Form1 GetInterface()
        {
            if(null == FromInterface)
            {
                FromInterface = new Form1();
            }
            return FromInterface;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = LastSelectPath;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                LastSelectPath = Path.GetDirectoryName(this.openFileDialog1.FileName);
                SelectFileFullName = openFileDialog1.FileName;
                SelectFileName = Path.GetFileName(openFileDialog1.FileName);
                SelectFileNameWE = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                
                if((0 == SelectFileFullName.Length) || (false == PictureCompress.GetInterface().IsPicture(SelectFileFullName)))
                {
                    MessageBox.Show("请选择正确的图片!");
                    return;
                }
                LabelPicShowNotice.Visible = false;
                EditFileSelect.Text = SelectFileFullName;//显示文件的名字
                EditSavePath.Text = LastSelectPath + "\\" + SelectFileNameWE + ".c";
                Bitmap bitmap = new Bitmap(SelectFileFullName);
                LabelPicSize.Text = bitmap.Width + "x" + bitmap.Height;
                switch (bitmap.PixelFormat.ToString())
                {
                    case "Format24bppRgb":
                        LablePixelFormat.Text = "24bpp";
                        break;
                    case "Format32bppArgb":
                        LablePixelFormat.Text = "32bpp";
                        break;
                    default:
                        LablePixelFormat.Text = "Unknow";
                        break;
                }
                Debug.WriteLine("Select directory:" + LastSelectPath);
                Debug.WriteLine("SelectFileFullName:" + SelectFileFullName);
                Debug.WriteLine("SelectFileName:" + SelectFileName);
                Debug.WriteLine("SelectFileNameWE:" + SelectFileNameWE);
                try
                {
                    this.PicPreview.Load(SelectFileFullName);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
        }

        private void BtnSwitch_Click(object sender, EventArgs e)
        {
            if(0 == SelectFileFullName.Length)
            {
                MessageBox.Show("请选择正确的图片!");
                return;
            }
            Bitmap bitmap = new Bitmap(SelectFileFullName);
            if (null == bitmap)
            {
                MessageBox.Show("文件格式错误!");
                return;
            }
            Color col = bitmap.GetPixel(0, 0);
            Debug.WriteLine("SelectFileNameWE:" + SelectFileNameWE);
            Debug.WriteLine("(" + 0 + "," + 0 + ") col:" + col);
            Debug.WriteLine("Bitmap size " + bitmap.Width + "x" + bitmap.Height);
            if(0 == EditSavePath.Text.Length)
            {
                MessageBox.Show("请选择正确的保存路径");
                return;
            }
            PictureCompress.GetInterface().PictureCompressRLE(bitmap, EditSavePath.Text, SelectFileNameWE, ListboxCompress.SelectedIndex);
        }
    }
}
