using graps.Model;
using MetroFramework.Forms;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace graps
{
    public partial class MainForm : MetroForm
    {
        private string SAVE_PATH = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private async void toolStripButton_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,

                Filter = "图片文件|*.png;*.jpg"
            };

            DialogResult dr = ofd.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                this.richTextBox_ZplStr.Text = "正在转换...";

                // 获取上传的图片
                string file = ofd.FileName;

                //压缩图片
                Image thumPicture = GetThumbnail(Image.FromFile(file), 96, 96);

                Image image;

                // 如果选择的图片是PNG则先转换成JPG

                if (ofd.SafeFileName.Split('.')[1].ToUpper().Equals("PNG"))
                {
                    //图片转JPG
                    image = PngConvertJpg(thumPicture, file.Split('.')[0] + ".jpg");
                }
                else
                {
                    image = thumPicture;
                }

                //转换成灰度图片
                Bitmap bitmap = RgbToGrayScale(new Bitmap(image));

                // 显示灰度图片
                this.sPictureBox_ShowPicture.Image = bitmap;

                //程序根目录
                string sysPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd");

                // 文件名称
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssDD");

                //文件保存路径
                SAVE_PATH = sysPath + "\\" + filename + ".PNG";

                //检测文件夹路径是否存在
                bool exists = Directory.Exists(sysPath);

                //不存在则创建
                if (!exists)
                {
                    Directory.CreateDirectory(sysPath);
                }

                this.sPictureBox_ShowPicture.Image.Save(SAVE_PATH);

                string res = await zplClientDoAsync(SAVE_PATH);

                ZplClientClass zplClientClass = JsonConvert.DeserializeObject<ZplClientClass>(res);

                StringBuilder sb = new StringBuilder();
                sb.Append("^FO50,30^GFA,");
                sb.Append(zplClientClass.totalBytes + ",");
                sb.Append(zplClientClass.totalBytes + ",");
                sb.Append(zplClientClass.rowBytes + ",");
                sb.Append(zplClientClass.data);
                sb.Append("\n");
                sb.Append("^FS");

                this.richTextBox_ZplStr.Text = sb.ToString();
            }
        }

        /// <summary>
        /// 上传图片到ZPL官方得到图片的ZPL指令
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string> zplClientDoAsync(string file)
        {
            RestClientOptions options = new RestClientOptions("https://api.labelary.com")
            {
                MaxTimeout = -1,
            };

            RestClient client = new RestClient(options);

            RestRequest request = new RestRequest("/v1/graphics", Method.Post)
            {
                AlwaysMultipartFormData = true
            };

            request.AddFile("file", file);

            RestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        /// <summary>
        /// 将源图像灰度化，并转化为8位灰度图像。
        /// </summary>
        /// <param name="original">源图像</param>
        /// <returns> 8位灰度图像</returns>
        public static Bitmap RgbToGrayScale(Bitmap original)
        {
            if (original != null)
            {
                // 将源图像内存区域锁定
                Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
                BitmapData bmpData = original.LockBits(rect, ImageLockMode.ReadOnly,
                        PixelFormat.Format24bppRgb);

                // 获取图像参数
                int width = bmpData.Width;
                int height = bmpData.Height;
                int stride = bmpData.Stride;  // 扫描线的宽度,比实际图片要大
                int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙
                IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置的指针
                int scanBytesLength = stride * height;  // 用stride宽度，表示这是内存区域的大小

                // 分别设置两个位置指针，指向源数组和目标数组
                int posScan = 0, posDst = 0;
                byte[] rgbValues = new byte[scanBytesLength];  // 为目标数组分配内存
                Marshal.Copy(ptr, rgbValues, 0, scanBytesLength);  // 将图像数据拷贝到rgbValues中
                // 分配灰度数组
                byte[] grayValues = new byte[width * height]; // 不含未用空间。
                // 计算灰度数组

                byte blue, green, red, YUI;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        blue = rgbValues[posScan];
                        green = rgbValues[posScan + 1];
                        red = rgbValues[posScan + 2];
                        YUI = (byte)(0.229 * red + 0.587 * green + 0.144 * blue);
                        //grayValues[posDst] = (byte)((blue + green + red) / 3);
                        grayValues[posDst] = YUI;
                        posScan += 3;
                        posDst++;
                    }
                    // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                    posScan += offset;
                }

                // 内存解锁
                Marshal.Copy(rgbValues, 0, ptr, scanBytesLength);

                original.UnlockBits(bmpData);  // 解锁内存区域

                // 构建8位灰度位图
                Bitmap retBitmap = BuiltGrayBitmap(grayValues, width, height);

                return retBitmap;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用灰度数组新建一个8位灰度图像。
        /// </summary>
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
        /// <param name="width"> 图像宽度。 </param>
        /// <param name="height"> 图像高度。 </param>
        /// <returns> 新建的8位灰度位图。 </returns>
        private static Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

            // 为图像数据赋值
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                posScan += offset;
            }

            // 内存解锁
            Marshal.Copy(grayValues, 0, ptr, scanBytes);
            bitmap.UnlockBits(bmpData);  // 解锁内存区域

            // 修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表
            bitmap.Palette = palette;

            return bitmap;
        }

        /// <summary>
        /// 生成高清缩略图片方法
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetThumbnail(Image image, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            //从Bitmap创建一个System.Drawing.Graphics
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            //设置 
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //下面这个也设成高质量
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //下面这个设成High
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //把原始图像绘制成上面所设置宽高的缩小图
            System.Drawing.Rectangle rectDestination = new Rectangle(0, 0, width, height);

            gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            return bmp;
        }

        private Image PngConvertJpg(Image img, string jpgPath)
        {
            //Image img = Image.FromFile(pngPath);

            using (Bitmap b = new Bitmap(img.Width, img.Height))
            {
                b.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(img, 0, 0);
                }

                Bitmap bit = new Bitmap(b);

                b.Dispose();

                //bit.Save(jpgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                return bit;

            }
        }
    }
}
