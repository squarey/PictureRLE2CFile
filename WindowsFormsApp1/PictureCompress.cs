using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class PictureCompress
    {
        //需要保存文件的全名字
        private static String SaveFullName;
        //文件中固定的内容
        private const String FileFrontStaticText1 = "\n\n\n#include <stdlib.h>\n#include \"GUI.h\"\n#ifndef GUI_CONST_STORAGE\n  #define GUI_CONST_STORAGE const\n#endif\n" +
                                                        "\n\nextern GUI_CONST_STORAGE GUI_BITMAP bm";
        private const String FileFrontStaticText2 = ";\n\nstatic GUI_CONST_STORAGE unsigned char _ac";
        private const String FileFrontStaticText3 = "[] = {";

        private const String FileLastStaticText1 = "GUI_CONST_STORAGE GUI_BITMAP bm";

        private static Int32 AreadyWriteTimes = 0;
        private static Int32 RleByteCount = 0;
        public static void PictureCompressRLE(Bitmap InputBitmap)
        {
            Int32 PicWidth, PicHeight;
            Int32 x, y, i, j;
            Int32 PixelPos = 0;
            Color BitmapColor;
            PicWidth = InputBitmap.Width;
            PicHeight = InputBitmap.Height;
            String WriteString;
            Int32 ColorBpp = 0;
            Int32 PicSize = 0;
            Int32 HexColorFlag = 0;
            j = 0;
            i = 0;
           
            Int32 ListBoxSelectItem = 0;
            Int32 Flag = 0;
            Int32 ByteBytesPerLine = 0;
            Int32 BitsPerPixel = 0;
            Int32 SameColorCount = 0;
            Int32 DiffrentColorCount = 0;
            String NumberByte, StartPosX, StartPosY;
            RleByteCount = 0;
            PicSize = PicWidth * PicHeight;
            /*int[] PicRgbBuffer = new int[] {
                0x00aa00, 0x00aa00, 0x00aa00, 0x00bb00, 0x00bb00, 0x00bb00, 0x00bb00, 0x00cc00, 0x00ee00, 0x00dd00,
                0x00ff00, 0x009900, 0x009900, 0x009900, 0x008800, 0x008800, 0x007700, 0x006600, 0x005500, 0x005500,
                0x004400, 0x004400, 0x004400, 0x004400,
            };*/
            //PicSize = PicRgbBuffer.Length;
            int[] PicRgbBuffer = new int[PicSize];
            switch (InputBitmap.PixelFormat.ToString())
            {
                case "Format24bppRgb":
                    Debug.WriteLine("picture pixel format is RGB 24 bpp");
                    
                    for (y = 0; y < PicHeight; y++)
                    {
                        for (x = 0; x < PicWidth; x++)
                        {
                            BitmapColor = InputBitmap.GetPixel(x, y);
                            PicRgbBuffer[y * PicWidth + x] = (BitmapColor.R << 16) | (BitmapColor.G << 8) | BitmapColor.B;
                        }
                    }
                    ListBoxSelectItem = Form1.GetInterface().GetListboxSelectItem();
                    Debug.WriteLine("ListBoxSelectBox:" + ListBoxSelectItem);
                    //文件保存的全路径
                    SaveFullName = Form1.LastSelectPath + "\\" + Form1.SelectFileNameWE + ".c";
                    Debug.WriteLine("SaveFullName:" + SaveFullName);
                    DeleteOldFile(SaveFullName);
                    StreamWriter mStreamWriter = new StreamWriter(SaveFullName, false, Encoding.UTF8);
                    WriteString = FileFrontStaticText1 + Form1.SelectFileNameWE + FileFrontStaticText2 + Form1.SelectFileNameWE + FileFrontStaticText3;
                    mStreamWriter.Write(WriteString);
                    BitsPerPixel = 16;
                    ByteBytesPerLine = PicWidth * BitsPerPixel / 8;
                    Debug.WriteLine("PicSize:" + PicSize);
                    for (PixelPos = 0; PixelPos < PicSize; )
                    {
                        //AAABBBBBCABCDDD
                        ColorBpp = PicRgbBuffer[PixelPos];
                        //查找相同的颜色
                        for(SameColorCount = 1; SameColorCount < PicSize - PixelPos; SameColorCount++)
                        {
                            if (ColorBpp != PicRgbBuffer[PixelPos + SameColorCount])
                            {
                                break;
                            }
                        }
                        
                        //只有当3个连续相同的颜色才做处理
                        if (SameColorCount > 2)
                        {
                            mStreamWriter.Write("\n  /* RLE: "+ string.Format("{0:D3}", SameColorCount) + " Pixels @ " + string.Format("{0:D3}", PixelPos%PicWidth) + "," + 
                                string.Format("{0:D3}", PixelPos / PicWidth) +" */ ");
                            AreadyWriteTimes = 0;
                            HexColorFlag = 0;
                            //一次最大只能标记127个连续相同的颜色
                            while (SameColorCount > 127)
                            {
                                HexColorFlag = 0xff;
                                WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", " + Color2RGB565String(ColorBpp, true);
                                RleByteCount += 2;
                                WriteStringToFile(mStreamWriter, WriteString);
                                SameColorCount -= 127;
                                PixelPos += 127;
                            }
                            HexColorFlag = 0;
                            if (SameColorCount > 0)
                            {
                                HexColorFlag |= 0x80;
                                HexColorFlag += SameColorCount;
                                WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", " + Color2RGB565String(ColorBpp, true);
                                RleByteCount += 2;
                                WriteStringToFile(mStreamWriter, WriteString);
                            }
                            //更新坐标
                            PixelPos += SameColorCount;
                        }
                        //查找有多少个不连续的像素
                        else
                        {
                            mStreamWriter.Write("\n  /* ABS: " + string.Format("{0:D3}", DiffrentColorCount) + " Pixels @ " + string.Format("{0:D3}", PixelPos % PicWidth) + "," +
                               string.Format("{0:D3}", PixelPos / PicWidth) + " */ ");
                            AreadyWriteTimes = 0;
                            SameColorCount = 0;
                            for (DiffrentColorCount = 1; DiffrentColorCount < PicSize - PixelPos; DiffrentColorCount++)
                            {
                                //查找下一个连续相同的色块
                                if (ColorBpp == PicRgbBuffer[PixelPos + DiffrentColorCount])
                                {
                                    SameColorCount++;
                                    if (SameColorCount >= 2)
                                    {
                                        if(DiffrentColorCount >= 2)
                                        {
                                            DiffrentColorCount -= 2;
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    ColorBpp = PicRgbBuffer[PixelPos + DiffrentColorCount];
                                    SameColorCount = 0;
                                }
                            }
                            //一次最多只能标记127个连续不相同的像素
                            //不连续的色块大于127的话  每次写127个像素入文件中
                            HexColorFlag = 0;
                            while (DiffrentColorCount > 127)
                            {
                                HexColorFlag = 127;
                                WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", ";
                                WriteStringToFile(mStreamWriter, WriteString);
                                RleByteCount += 2;
                                WriteBitmapDataToFile(mStreamWriter, PicRgbBuffer, PixelPos, 127, true);
                                DiffrentColorCount -= 127;
                                PixelPos += 127;
                            }
                            HexColorFlag = 0;
                            if (DiffrentColorCount > 0)
                            {   //不连续的色块小于127 则直接将不连续的写入文件中
                                HexColorFlag |= DiffrentColorCount;
                                WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", ";
                                WriteStringToFile(mStreamWriter, WriteString);
                                RleByteCount += 2;
                                WriteBitmapDataToFile(mStreamWriter, PicRgbBuffer, PixelPos, DiffrentColorCount, true);
                            }
                            //更新坐标
                            PixelPos += DiffrentColorCount;
                        }
                    }

                    WriteStringToFile(mStreamWriter, "};\n\n\n");
                    WriteString = FileLastStaticText1 + Form1.SelectFileNameWE + " = {\n  " + PicWidth + ",  //xSize\n  " + PicHeight + ",  //ySize\n  " +
                                   ByteBytesPerLine + ",  //ByteBytesPerLine\n  " + BitsPerPixel + ",  //BitsPerPixel\n  " + "(unsigned char *)_ac" + Form1.SelectFileNameWE +
                                   ",  //Pointer to picture data\n  " + "NULL,  //Pointer to palette\n  " + "GUI_DRAW_METHOD\n" + "};  \n\n";
                    WriteStringToFile(mStreamWriter, WriteString);
                    mStreamWriter.Close();
                    mStreamWriter.Dispose();
                    mStreamWriter = null;
                    Debug.WriteLine("get picture buffer end RleByteCount:" + RleByteCount);
                    break;
                case "Format32bppArgb":
                    Debug.WriteLine("picture pixel format is ARGB 32 bpp");
                    break;
                default:
                    Debug.WriteLine("picture pixel format unkown");
                    break;
            }
        }
        public static void WriteBitmapDataToFile(StreamWriter sWrite, int[] ColorBuffer, int StartPos, int WriteLen, bool Flag)
        {
            int i = 0;
            String WriteString;
            for(i = 0; i < WriteLen; i++)
            {
                if(13 == AreadyWriteTimes)
                {
                    AreadyWriteTimes = 0;
                    sWrite.Write("\n        ");
                }
                RleByteCount += 2;
                WriteString = Color2RGB565String(ColorBuffer[StartPos + i], Flag);
                sWrite.Write(WriteString);
                AreadyWriteTimes++;
            }
        }
        public static void WriteStringToFile(StreamWriter sWrite, String WriteString)
        {
            if (13 == AreadyWriteTimes)
            {
                AreadyWriteTimes = 0;
                sWrite.Write("\n        ");
            }
            sWrite.Write(WriteString);
            AreadyWriteTimes++;
        }
        public static String Color2RGB565String(int ColorIndex, bool IsSwap)
        {
            Int32 Temp = 0;
            Int32 Red, Green, Blue;
            String HexHigh8Bit, HexLow8Bit;
            Int32 ColorBpp = 0;
            Red = (ColorIndex >> 16) & 0xff;
            Green = (ColorIndex >> 8) & 0xff;
            Blue = (ColorIndex >> 0) & 0xff;
           if (IsSwap)
            {
                ColorBpp = ((Red & 0xf8) << 8) | ((Green & 0xfc) << 3) | ((Blue & 0xf8) >> 3);
            }
            else
            {
                ColorBpp = ((Blue & 0xf8) << 8) | ((Green & 0xfc) << 3) | ((Red & 0xf8) >> 3);
            }
            //ColorBpp = ColorIndex & 0xffff;
            Temp = (ColorIndex >> 8) & 0xff;
            HexHigh8Bit = "0x" + String.Format("{0:X2}", Temp);
            Temp = ColorIndex & 0xff;
            HexLow8Bit = "0x" + String.Format("{0:X2}", Temp);
            return (HexHigh8Bit + ", " + HexLow8Bit + ", ");
        }
        public static void DeleteOldFile(string filesToDelete)
        {
            if (null == filesToDelete)
            {
                return;
            }
            if (System.IO.File.Exists(Path.GetFullPath(filesToDelete)))
            {
                File.Delete(Path.GetFullPath(filesToDelete));
            }
        }
    }
}
