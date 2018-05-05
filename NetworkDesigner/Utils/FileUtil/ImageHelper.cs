using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.Utils.FileUtil
{
    class ImageHelper
    {
        //获取img的后缀 http://www.jb51.net/article/46951.htm
        //var ext = System.IO.Path.GetExtension("C:\\soar.jpg");//已知文件后缀的情况下
        //不知道文件后缀的情况下要从 image对象中提取后缀
        private static Dictionary<String, ImageFormat> _imageFormats;
        /// <summary>
        /// 获取 所有支持的图片格式字典
        /// </summary>
        public static Dictionary<String, ImageFormat> ImageFormats
        {
            get
            {
                return _imageFormats ?? (_imageFormats = GetImageFormats());
            }
        }

        private static Dictionary<String, ImageFormat> GetImageFormats()
        {
            var dic = new Dictionary<String, ImageFormat>();
            var properties = typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var property in properties)
            {
                var format = property.GetValue(null, null) as ImageFormat;
                if (format == null) continue;
                dic.Add(("." + property.Name).ToLower(), format);
            }
            return dic;
        }

        /// <summary>
        /// 根据图像获取图像的扩展名
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static String GetExtension(Image image)
        {
            foreach (var pair in ImageFormats)
            {
                if (pair.Value.Guid == image.RawFormat.Guid)
                {
                    return pair.Key;
                }
            }
            throw new BadImageFormatException();
        }
        /// <summary>
        /// 提取ImageList的图标另保为文件
        /// </summary>
        /// <param name="imageList"></param>
        /// <param name="dirPath"></param>
        public static void SaveImageList(ImageList imageList, string dirPath,string extension=".png")
        {
            int i = 0;
            string ext="";
            foreach (Image img in imageList.Images)
            {
                if (extension.Equals(""))
                    ext = GetExtension(img);
                else
                    ext = extension;
                img.Save(Path.Combine(dirPath, ++i + ext));
            }
        }

        /// <summary>
        /// 按比例缩放图片，完成后会回收掉源图片
        /// </summary>
        /// <param name="SourceImage"></param>
        /// <param name="TargetWidth"></param>
        /// <param name="TargetHeight"></param>
        /// <returns></returns>
        public static Image ZoomImage(Image SourceImage, int TargetWidth, int TargetHeight)
        {
            int IntWidth; //新的图片宽
            int IntHeight; //新的图片高
            try
            {
                System.Drawing.Imaging.ImageFormat format = SourceImage.RawFormat;
                System.Drawing.Bitmap SaveImage = new System.Drawing.Bitmap(TargetWidth, TargetHeight);
                Graphics g = Graphics.FromImage(SaveImage);
                g.Clear(Color.Transparent);

                //计算缩放图片的大小
                if (SourceImage.Width > TargetWidth && SourceImage.Height <= TargetHeight)//宽度比目的图片宽度大，长度比目的图片长度小
                {
                    IntWidth = TargetWidth;
                    IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
                }
                else if (SourceImage.Width <= TargetWidth && SourceImage.Height > TargetHeight)//宽度比目的图片宽度小，长度比目的图片长度大
                {
                    IntHeight = TargetHeight;
                    IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
                }
                else if (SourceImage.Width <= TargetWidth && SourceImage.Height <= TargetHeight) //长宽比目的图片长宽都小
                {
                    IntHeight = SourceImage.Width;
                    IntWidth = SourceImage.Height;
                }
                else//长宽比目的图片的长宽都大
                {
                    IntWidth = TargetWidth;
                    IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
                    if (IntHeight > TargetHeight)//重新计算
                    {
                        IntHeight = TargetHeight;
                        IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
                    }
                }
                //设置画布的描绘质量           
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(SourceImage, (TargetWidth - IntWidth) / 2, (TargetHeight - IntHeight) / 2, IntWidth, IntHeight);
                //g.DrawImage(SourceImage, new System.Drawing.Rectangle((TargetWidth - IntWidth) / 2,
                    //(TargetHeight - IntHeight) / 2, IntWidth, IntHeight), 0, 0, SourceImage.Width, SourceImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                SourceImage.Dispose();

                return SaveImage;
            }
            catch (Exception ex)
            {
                NetworkDesigner.Utils.Common.LogHelper.LogInfo("图片转换缩放失败：" + ex.Message);
            }

            return null;
        }

        private static string[] extension = new string[] { ".bmp", ".jpg", ".jpeg", ".png", ".ico" };
        /// <summary>
        /// <para>根据文件扩展名判断是否为图片： *.bmp;*.jpg;*.jpeg;*.png;*.ico</para>
        /// 若文件不存在返回false，未知类型也返回false
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return false;
            string ext = Path.GetExtension(filePath);
            foreach (string s in extension)
            {
                if (s.ToLower().Equals(ext))
                    return true;
            }
            return false;
        }
    }
}
