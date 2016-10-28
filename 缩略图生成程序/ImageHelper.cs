using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Net;
using System.IO;
using System.Text;

namespace 缩略图生成程序
{
    public static class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="sFullPath">原图路径</param>
        /// <param name="intThumbWidth">缩略图宽度</param>
        /// <param name="intThumbHeight">缩略图高度</param>
        /// <param name="sThumbExtension">缩略图前缀 sub</param>
        /// <param name="sMode">缩略图生成方式</param>
        /// <returns></returns>
        public static string BuildSubImg(string sFilePath, int intThumbWidth, int intThumbHeight, string sThumbExtension, string sMode)
        {
            string sThumbFile = "";
            try
            {
                System.Uri httpUrl = new System.Uri(sFilePath);
                HttpWebRequest req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Timeout = 180000; //设置超时值10秒
                //req.UserAgent = "XXXXX";
                //req.Accept = "XXXXXX";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)(req.GetResponse());
                Image img = new Bitmap(res.GetResponseStream());//获取图片流            

                string fileName = sFilePath.Substring(sFilePath.LastIndexOf("/") + 1);
                string filePath = sFilePath.Replace("http://attach.cuuju.com/", "");

                sThumbFile = sThumbExtension + fileName;
                string thumbFilePath = filePath.Replace(fileName, sThumbFile);


                //缩略图保存的绝对路径    
                string smallImagePath = @"C:\" + filePath;


                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                img.Save(@"C:/" + filePath);//随

                //string temp = HttpContext.Current.Server.MapPath(sFilePath);
                //原图加载    
                System.Drawing.Image sourceImage = Image.FromFile(@"C:/" + filePath);

                //原图宽度和高度    
                int width = sourceImage.Width;
                int height = sourceImage.Height;
                int smallWidth = intThumbWidth;
                int smallHeight = intThumbHeight;

                switch (sMode)
                {
                    case "HW"://指定高宽缩放（可能变形）               
                        break;
                    case "W"://指定宽，高按比例
                        if (smallWidth < width)
                        {
                            smallHeight = height * intThumbWidth / width;
                        }
                        else
                        {
                            smallWidth = width;
                            smallHeight = height;
                        }
                        break;
                    case "H"://指定高，宽按比例
                        if (smallHeight < height)
                        {
                            smallWidth = width * intThumbHeight / height;
                        }
                        else
                        {
                            smallWidth = width;
                            smallHeight = height;
                        }
                        break;
                    case "Cut"://指定高宽裁减（不变形）
                        //获取第一张绘制图的大小,(比较 原图的宽/缩略图的宽 和 原图的高/缩略图的高)    
                        if (width <= intThumbWidth && height <= intThumbHeight)
                        {
                            //原图宽度比缩略图的宽度小，要保持不变
                            smallWidth = width;
                            smallHeight = height;
                        }
                        else
                        {
                            if (((decimal)width) / height <= ((decimal)intThumbWidth) / intThumbHeight)
                            {
                                smallWidth = intThumbWidth;
                                smallHeight = intThumbWidth * height / width;
                            }
                            else if (((decimal)width) / height > ((decimal)intThumbWidth) / intThumbHeight)
                            {
                                smallWidth = intThumbHeight * width / height;
                                smallHeight = intThumbHeight;
                            }
                        }
                        break;
                }



                //新建一个图板,以最小等比例压缩大小绘制原图    
                using (System.Drawing.Image bitmap = new System.Drawing.Bitmap(smallWidth, smallHeight))
                {
                    //绘制中间图    
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        //高清,平滑    
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.Clear(Color.Transparent);
                        g.DrawImage(
                        sourceImage,
                        new System.Drawing.Rectangle(0, 0, smallWidth, smallHeight),
                        new System.Drawing.Rectangle(0, 0, width, height),
                        System.Drawing.GraphicsUnit.Pixel
                        );
                    }
                    //新建一个图板,以缩略图大小绘制中间图    
                    using (System.Drawing.Image bitmap1 = new System.Drawing.Bitmap(intThumbWidth, intThumbHeight))
                    {
                        //绘制缩略图    
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap1))
                        {
                            //高清,平滑    
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.Clear(Color.Transparent);
                            int lwidth = (smallWidth - intThumbWidth) / 2;
                            int bheight = (smallHeight - intThumbHeight) / 2;
                            g.DrawImage(bitmap, new Rectangle(0, 0, intThumbWidth, intThumbHeight), lwidth, bheight, intThumbWidth, intThumbHeight, GraphicsUnit.Pixel);
                            g.Dispose();
                            bitmap.Save(smallImagePath);
                        }
                    }
                }
            }
            catch
            {
                ////出错则删除    
                //System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(sFilePath));
                return "图片格式不正确";
            }
            //返回缩略图名称    
            return sThumbFile;

        }
    }
}
