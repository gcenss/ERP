using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;

namespace HouseMIS.Web.House.Templete
{
    public class FileHelper
    {
        #region 检测指定文件是否存在
        /// <summary> 
        /// 检测指定文件是否存在,如果存在则返回true。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion



        #region 创建一个文件
        /// <summary> 
        /// 创建一个文件。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void CreateFile(string filePath)
        {
            //如果文件不存在则创建该文件 
            if (!IsExistFile(filePath))
            {
                //创建一个FileInfo对象 
                FileInfo file = new FileInfo(filePath);
                //创建文件 
                FileStream fs = file.Create();
                //关闭文件流 
                fs.Close();
            }
        }

        /// <summary> 
        /// 创建一个文件,并将字节流写入文件。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="buffer">二进制流数据</param> 
        public static void CreateFile(string filePath, byte[] buffer)
        {
            //如果文件不存在则创建该文件 
            if (!IsExistFile(filePath))
            {
                //创建一个FileInfo对象 
                FileInfo file = new FileInfo(filePath);
                //创建文件 
                FileStream fs = file.Create();
                //写入二进制流 
                fs.Write(buffer, 0, buffer.Length);
                //关闭文件流 
                fs.Close();
            }
        }
        #endregion


        #region 向文本文件写入内容
        /// <summary> 
        /// 向文本文件中写入内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param>         
        public static void WriteText(string filePath, string content)
        {
            //向文件写入内容 
            File.WriteAllText(filePath, content);
        }
        #endregion

        #region 向文本文件的尾部追加内容
        /// <summary> 
        /// 向文本文件的尾部追加内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param> 
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }
        #endregion

        #region 将现有文件的内容复制到新文件中
        /// <summary> 
        /// 将源文件的内容复制到目标文件中 
        /// </summary> 
        /// <param name="sourceFilePath">源文件的绝对路径</param> 
        /// <param name="destFilePath">目标文件的绝对路径</param> 
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }
        #endregion

        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static string FileToString(string filePath)
        {

            return FileToString(filePath, Encoding.UTF8);
        }

        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="encoding">字符编码</param> 
        public static string FileToString(string filePath, Encoding encoding)
        {
            //创建流读取器 
            StreamReader reader = new StreamReader(filePath, encoding);
            //读取流 
            string str = reader.ReadToEnd();
            //关闭流读取器 
            reader.Close();
            return str;
        }          
    }
}