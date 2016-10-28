//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.IO;
//using System.Configuration;
//using System.Drawing;
//using UEditorDemo.UploadFile;

//public class UploadHelper
//{
//    /// <summary>
//    /// 上传文件
//    /// </summary>
//    /// <param name="data">文件byte流</param>
//    /// <param name="fileanme">文件名</param>
//    /// <param name="rootPath">文件在附件服务器的根目录名</param>
//    /// <returns>文件在附件服务器的保存路径</returns>
//    public static string UploadFile(byte[] data, string fileanme, string rootPath)
//    {
//        UploadFile uf = new UploadFile();
//        string filepath = uf.UpLoadFiles(data, fileanme, rootPath);
//        return filepath;
//    }

//    /// <summary>
//    /// 上传图片，并生成缩略图（们）
//    /// 缩略图（们）为 sub1原图文件名，sub2原图文件名....
//    /// </summary>
//    /// <param name="data">图片byte流</param>
//    /// <param name="fileanme">文件名</param>
//    /// <param name="rootPath">文件在附件服务器的根目录名</param>
//    /// <param name="sm">缩略图尺寸</param>
//    /// <param name="sMode">生成方式"HW":指定高宽缩放（可能变形）"W":指定宽，高按比例  "H":指定高，宽按比例 "Cut":指定高宽裁减（不变形）</param>
//    /// <returns>文件在附件服务器的保存路径</returns>
//    public static string UploadImg(byte[] data, string fileanme, string rootPath, SizeModle[] sm, string sMode = "Cut")
//    {
//        UploadFile uf = new UploadFile();
//        string filepath = uf.UpLoadAndBuildManyThum(data, fileanme, rootPath, sm, sMode);
//        return filepath;
//    }

//    /// <summary>
//    /// 下载文件
//    /// </summary>
//    /// <param name="filepath">文件在附件服务器的保存路径</param>
//    public static byte[] DownLoad(string filepath)
//    {
//        UploadFile uf = new UploadFile();
//        return uf.GetFileBytes(filepath);
//    }

//    /// <summary>
//    /// 删除附件
//    /// </summary>
//    /// <param name="filepath">文件在附件服务器的保存路径</param>
//    public static int Delete(string filepath)
//    {
//        UploadFile uf = new UploadFile();
//        return uf.Delete(filepath);
//    }

//    /// <summary>
//    /// 批量删除附件
//    /// </summary>
//    /// <param name="filepaths">文件们在附件服务器的保存路径</param>
//    public static int DeleteMore(string[] filepaths)
//    {
//        UploadFile uf = new UEditorDemo.UploadFile();
//        return uf.DeleteMore(filepaths);
//    }

//    /// <summary>
//    /// 截图
//    /// </summary>
//    /// <returns></returns>
//    public static string ShowImage(string path, int PointX, int PointY, int CutWidth, int CutHeight, int PicWidth, int PicHeight, string folder)
//    {
//        UploadFile uf = new UploadFile();
//        return uf.ShowImage(path, PointX, PointY, CutWidth, CutHeight, PicWidth, PicHeight, folder);
//    }

//    /// <summary>
//    /// 上传图片
//    /// </summary>
//    /// <returns></returns>
//    public static string UpLoadImage(byte[] data, string fileanme, string rootPath)
//    {
//        UploadFile uf = new UploadFile();
//        return uf.UpLoadImages(data, fileanme, rootPath);
//    }

//    /// <summary>
//    /// 下载网络图片到附件服务器上
//    /// </summary>
//    /// <param name="url"></param>
//    /// <param name="folder"></param>
//    /// <returns></returns>
//    public static string DownLoadFile(string url, string folder)
//    {
//        UploadFile uf = new UploadFile();
//        return uf.DownLoadFile(url, folder);
//    }
//}