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

        private const String PicDecodeMethodRLERGB565 = "GUI_DRAW_RLEM16M";
        private const String PicDecodeMethodRLEBGR565 = "GUI_DRAW_RLE16M";
        private const String PicDecodeMethodRLEARGB8565 = "GUI_DRAW_AlphaRLEM16M";
        private const String PicDecodeMethodRLEABGR8565 = "GUI_DRAW_AlphaRLE16M";
        private const String PicDecodeMethodBMPARGB8565 = "GUI_DRAW_BMPAM16M";
        private const String PicDecodeMethodBMPABGR8565 = "GUI_DRAW_BMPA16M";
        private const String PicDecodeMethodBMPRGB565 = "GUI_DRAW_BMPM16M";
        private const String PicDecodeMethodBMPBGR565 = "GUI_DRAW_BMP16M";

        private const Int32 RGB_SWAP_AND_REL_COMPRESS = 0;
        private const Int32 BGR_SWAP_AND_REL_COMPRESS = 1;
        private const Int32 RGB_SWAP_NO_REL_COMPRESS = 2;
        private const Int32 BGR_SWAP_NO_REL_COMPRESS = 3;

        private Int32 AreadyWriteTimes = 0;
        //压缩后的总字节数
        private Int32 RleByteCount = 0;
        private Int32 RGBSwapAndCompress = RGB_SWAP_AND_REL_COMPRESS;
        private String ToSavePath = "";
        private String ToFileNameWE = "";
        private String PicDecodeMethod = "";

        public static PictureCompress PicCompressInterface = null;

        public static PictureCompress GetInterface()
        {
            if(null == PicCompressInterface)
            {
                PicCompressInterface = new PictureCompress();
            }
            return PicCompressInterface;
        }
        //根据文件名判断是否为图片
        public bool IsPicture(string fileName)
        {
            string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
            char[] separtor = { '|' };
            string[] tempFileds = StringSplit(strFilter, separtor);
            foreach (string str in tempFileds)
            {
                if (str.ToUpper() == fileName.Substring(fileName.LastIndexOf("."), 
                    fileName.Length - fileName.LastIndexOf(".")).ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
        // 通过字符串，分隔符返回string[]数组 
        public string[] StringSplit(string s, char[] separtor)
        {
            string[] tempFileds = s.Trim().Split(separtor); return tempFileds;
        }
        public void PictureCompressRLE(Bitmap InputBitmap, String SavePath, String FileNameWE, int Index)
        {
            Int32 PicWidth, PicHeight;
            Int32 x, y;
            Color BitmapColor;
            PicWidth = InputBitmap.Width;
            PicHeight = InputBitmap.Height;
            Int32 PicSize = 0;
            //j = 0;
            //i = 0;
            RleByteCount = 0;
            PicSize = PicWidth * PicHeight;
            SaveFullName = SavePath;
            ToFileNameWE = FileNameWE;
            switch (Index)
            {
                case 0:
                    RGBSwapAndCompress = RGB_SWAP_AND_REL_COMPRESS;
                    break;
                case 1:
                    RGBSwapAndCompress = BGR_SWAP_AND_REL_COMPRESS;
                    break;
                case 2:
                    RGBSwapAndCompress = RGB_SWAP_NO_REL_COMPRESS;
                    break;
                case 3:
                    RGBSwapAndCompress = BGR_SWAP_NO_REL_COMPRESS;
                    break;
                default:
                    break;
            }
            Debug.WriteLine("RGBSwap:" + RGBSwapAndCompress);
            /*int[] PicRgbBuffer = new int[] {
                0x00aa00, 0x00aa00, 0x00aa00, 0x00bb00, 0x00bb00, 0x00bb00, 0x00bb00, 0x00cc00, 0x00ee00, 0x00dd00,
                0x00ff00, 0x009900, 0x009900, 0x009900, 0x008800, 0x008800, 0x007700, 0x006600, 0x005500, 0x005500,
                0x004400, 0x004400, 0x004400, 0x004400,
            };*/
            //PicSize = PicRgbBuffer.Length;
            int A, R, G, B;
            int[] PicRgbBuffer = new int[PicSize];
            for (y = 0; y < PicHeight; y++)
            {
                for (x = 0; x < PicWidth; x++)
                {
                    BitmapColor = InputBitmap.GetPixel(x, y);
                    //PicRgbBuffer[y * PicWidth + x] = (BitmapColor.A << 24) | (BitmapColor.R << 16) | (BitmapColor.G << 8) | BitmapColor.B;
                    A = BitmapColor.A;
                    R = BitmapColor.R & 0xf8;
                    G = BitmapColor.G & 0xfc;
                    B = BitmapColor.B & 0xf8;
                    PicRgbBuffer[y * PicWidth + x] = (A << 24) | (R << 16) | (G << 8) | B;
                    //Debug.Write("0x" + String.Format("{0:X8}", PicRgbBuffer[y * PicWidth + x]) + " ");
                }
               // Debug.Write("\n");
            }
            switch (InputBitmap.PixelFormat.ToString())
            {
                case "Format24bppRgb":
                    Debug.WriteLine("picture pixel format is RGB 24 bpp");
                    switch(RGBSwapAndCompress)
                    {
                        case RGB_SWAP_AND_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodRLERGB565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, true, false, true);
                            break;
                        case BGR_SWAP_AND_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodRLEBGR565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, false, false, true);
                            break;
                        case RGB_SWAP_NO_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodBMPRGB565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, true, false, false);
                            break;
                        case BGR_SWAP_NO_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodBMPBGR565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, false, false, false);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Format32bppArgb":
                    Debug.WriteLine("picture pixel format is ARGB 32 bpp");
                    switch (RGBSwapAndCompress)
                    {
                        case RGB_SWAP_AND_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodRLEARGB8565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, true, true, true);
                            break;
                        case BGR_SWAP_AND_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodRLEABGR8565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, false, true, true);
                            break;
                        case RGB_SWAP_NO_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodBMPARGB8565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, true, true, false);
                            break;
                        case BGR_SWAP_NO_REL_COMPRESS:
                            PicDecodeMethod = PicDecodeMethodBMPABGR8565;
                            StartCompressREL(PicRgbBuffer, PicWidth, PicHeight, false, true, false);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    Debug.WriteLine("picture pixel format unkown");
                    break;
            }
        }
        private void StartCompressREL(int[] PicRgbBuffer, int PicWidth, int PicHeight, bool RGBSwap, bool HasAlpha, bool RLECompress)
        {
            Int32 PixelPos = 0;
            String WriteString;
            Int32 ColorBpp = 0;
            Int32 HexColorFlag = 0;
            Int32 ByteBytesPerLine = 0;
            Int32 BitsPerPixel = 0;
            Int32 SameColorCount = 0;
            Int32 DiffrentColorCount = 0;
            Int32 PicSize = 0;

            //文件保存的全路径
            //SaveFullName = ToSavePath + "\\" + ToFileNameWE + ".c";
            Debug.WriteLine("SaveFullName:" + SaveFullName);
            DeleteOldFile(SaveFullName);
            StreamWriter mStreamWriter = new StreamWriter(SaveFullName, false, Encoding.UTF8);
            WriteString = FileFrontStaticText1 + ToFileNameWE + FileFrontStaticText2 + ToFileNameWE + FileFrontStaticText3;
            mStreamWriter.Write(WriteString);
            PicSize = PicWidth * PicHeight;
            Debug.WriteLine("PicSize:" + PicSize);
            if(RLECompress)
            {
                int SwitchLineCnt = 0;
                for (PixelPos = 0; PixelPos < PicSize;)
                {
                    //AAABBBBBCABCDDD
                    ColorBpp = PicRgbBuffer[PixelPos];
                    //查找相同的颜色
                    for (SameColorCount = 1; SameColorCount < PicSize - PixelPos; SameColorCount++)
                    {
                        if (ColorBpp != PicRgbBuffer[PixelPos + SameColorCount])
                        {
                            break;
                        }
                    }
                    //只有当3个连续相同的颜色才做处理
                    if (SameColorCount > 2)
                    {
                        //写压缩信息
                        mStreamWriter.Write("\n  /* RLE: " + string.Format("{0:D3}", SameColorCount) + " Pixels @ " + string.Format("{0:D3}", PixelPos % PicWidth) + "," +
                            string.Format("{0:D3}", PixelPos / PicWidth) + " */ ");
                        AreadyWriteTimes = 0;
                        HexColorFlag = 0;
                        SwitchLineCnt = 0;
                        //一次最大只能标记127个连续相同的颜色
                        while (SameColorCount > 127)
                        {
                            HexColorFlag = 0xff;
                            WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", " +
                                Color2RGB565String(ColorBpp, RGBSwap, HasAlpha);
                            if (HasAlpha)
                            {
                                RleByteCount += 4;
                            }
                            else
                            {
                                RleByteCount += 3;
                            }
                            WriteStringToFile(mStreamWriter, WriteString);
                            SameColorCount -= 127;
                            PixelPos += 127;
                            SwitchLineCnt++;
                            if (SwitchLineCnt >= 10)
                            {
                                SwitchLineCnt = 0;
                                WriteStringToFile(mStreamWriter, "\n            ");
                            }
                        }
                        HexColorFlag = 0;
                        if (SameColorCount > 0)
                        {
                            HexColorFlag |= 0x80;
                            HexColorFlag += SameColorCount;
                            WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", " +
                                Color2RGB565String(ColorBpp, RGBSwap, HasAlpha);
                            if (HasAlpha)
                            {
                                RleByteCount += 4;
                            }
                            else
                            {
                                RleByteCount += 3;
                            }
                            WriteStringToFile(mStreamWriter, WriteString);
                        }
                        //更新坐标
                        PixelPos += SameColorCount;
                    }
                    //查找有多少个不连续的像素
                    else
                    {

                        AreadyWriteTimes = 0;
                        SameColorCount = 0;
                        SwitchLineCnt = 0;
                        for (DiffrentColorCount = 1; DiffrentColorCount < PicSize - PixelPos; DiffrentColorCount++)
                        {
                            //查找下一个连续相同的色块
                            if (ColorBpp == PicRgbBuffer[PixelPos + DiffrentColorCount])
                            {
                                SameColorCount++;
                                if (SameColorCount >= 2)
                                {
                                    if (DiffrentColorCount >= 2)
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
                        //写不需压缩的信息
                        mStreamWriter.Write("\n  /* ABS: " + string.Format("{0:D3}", DiffrentColorCount) + " Pixels @ " + string.Format("{0:D3}", PixelPos % PicWidth) + "," +
                           string.Format("{0:D3}", PixelPos / PicWidth) + " */ ");
                        //一次最多只能标记127个连续不相同的像素
                        //不连续的色块大于127的话  每次写127个像素入文件中
                        HexColorFlag = 0;
                        while (DiffrentColorCount > 127)
                        {
                            HexColorFlag = 127;
                            WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", ";
                            WriteStringToFile(mStreamWriter, WriteString);
                            RleByteCount += 1;
                            WriteBitmapDataToFile(mStreamWriter, PicRgbBuffer, PixelPos, 127, RGBSwap, HasAlpha);
                            DiffrentColorCount -= 127;
                            PixelPos += 127;
                            if (SwitchLineCnt >= 10)
                            {
                                SwitchLineCnt = 0;
                                WriteStringToFile(mStreamWriter, "\n            ");
                            }
                        }
                        HexColorFlag = 0;
                        if (DiffrentColorCount > 0)
                        {   //不连续的色块小于127 则直接将不连续的写入文件中
                            HexColorFlag |= DiffrentColorCount;
                            WriteString = "0x" + String.Format("{0:X2}", HexColorFlag) + ", ";
                            WriteStringToFile(mStreamWriter, WriteString);
                            RleByteCount += 1;
                            WriteBitmapDataToFile(mStreamWriter, PicRgbBuffer, PixelPos, DiffrentColorCount, RGBSwap, HasAlpha);
                        }
                        //更新坐标
                        PixelPos += DiffrentColorCount;
                    }
                }
                //写结束信息
                WriteStringToFile(mStreamWriter, "\n};  //");
                if (HasAlpha)
                {
                    WriteString = "" + PicSize + " pixels";
                    ByteBytesPerLine = PicWidth * 3;
                    BitsPerPixel = 24;
                }
                else
                {
                    WriteString = "" + PicSize + " pixels";
                    ByteBytesPerLine = PicWidth * 2;
                    BitsPerPixel = 16;
                }
                WriteString = WriteString + " compress to " + RleByteCount;
                
            }
            else
            {
                WriteStringToFile(mStreamWriter, "\n        ");
                WriteBitmapDataToFile(mStreamWriter, PicRgbBuffer, 0, PicSize, RGBSwap, HasAlpha);
                WriteStringToFile(mStreamWriter, "\n};  //");
                WriteString = "Total ";
                if (HasAlpha)
                {
                    WriteString += PicSize * 3;
                    ByteBytesPerLine = PicWidth * 3;
                    BitsPerPixel = 24;
                }
                else
                {
                    WriteString += + PicSize * 2;
                    ByteBytesPerLine = PicWidth * 2;
                    BitsPerPixel = 16;
                }
            }
            WriteStringToFile(mStreamWriter, WriteString + " bytes\n\n\n");
            WriteString = FileLastStaticText1 + ToFileNameWE + " = {\n  " + PicWidth + ",  //xSize\n  " + PicHeight + ",  //ySize\n  " +
                           ByteBytesPerLine + ",  //ByteBytesPerLine\n  " + BitsPerPixel + ",  //BitsPerPixel\n  " + "(unsigned char *)_ac" + ToFileNameWE +
                           ",  //Pointer to picture data\n  " + "NULL,  //Pointer to palette\n  " + PicDecodeMethod + "\n};  \n\n";
            WriteStringToFile(mStreamWriter, WriteString);
            mStreamWriter.Close();
            mStreamWriter.Dispose();
            mStreamWriter = null;
            Debug.WriteLine("get picture buffer end RleByteCount:" + RleByteCount);
            MessageBox.Show("转换成功");
        }
        public void WriteBitmapDataToFile(StreamWriter sWrite, int[] ColorBuffer, int StartPos, int WriteLen, bool Flag, bool HasAlpha)
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
                if(HasAlpha)
                {
                    RleByteCount += 3;
                }
                else
                {
                    RleByteCount += 2;
                }
                AreadyWriteTimes++;
                WriteString = Color2RGB565String(ColorBuffer[StartPos + i], Flag, HasAlpha);
                sWrite.Write(WriteString);
            }
        }
        public void WriteStringToFile(StreamWriter sWrite, String WriteString)
        {
            if (13 == AreadyWriteTimes)
            {
                AreadyWriteTimes = 0;
                sWrite.Write("\n        ");
            }
            sWrite.Write(WriteString);
        }
        public String Color2RGB565String(int ColorIndex, bool IsSwap, bool HasAlpha)
        {
            Int32 Temp = 0;
            Int32 Red, Green, Blue;
            String HexHigh8Bit, HexLow8Bit;
            Int32 ColorBpp = 0;
            String ReturnString = "";
            Red = (ColorIndex >> 16) & 0xff;
            Green = (ColorIndex >> 8) & 0xff;
            Blue = (ColorIndex >> 0) & 0xff;
           if (IsSwap)
           {
                //ColorBpp = ((Red & 0xf8) << 8) | ((Green & 0xfc) << 3) | ((Blue & 0xf8) >> 3);
                ColorBpp = (Red << 8) | (Green << 3) | (Blue >> 3);
           }
           else
           {
                //ColorBpp = ((Blue & 0xf8) << 8) | ((Green & 0xfc) << 3) | ((Red & 0xf8) >> 3);
                 ColorBpp = (Blue << 8) | (Green << 3) | (Red >> 3);
           }

            Temp = (ColorBpp >> 8) & 0xff;
            HexHigh8Bit = "0x" + String.Format("{0:X2}", Temp);
            Temp = ColorBpp & 0xff;
            HexLow8Bit = "0x" + String.Format("{0:X2}", Temp);
            if(HasAlpha)
            {
                Int32 Alpha;
                Alpha = ((ColorIndex >> 24) & 0xff);
                ReturnString = "0x" + String.Format("{0:X2}", Alpha) + ", ";
            }
            ReturnString = ReturnString + HexHigh8Bit + ", " + HexLow8Bit + ", ";
            return ReturnString;
        }
        public void DeleteOldFile(string filesToDelete)
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
