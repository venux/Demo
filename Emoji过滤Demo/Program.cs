using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.JScript;

namespace Emoji过滤Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = @"venux📕💚🔷🇧🇾🏂";
            //content = @"祥_v🐲⚽️8@【】";
            //content = @"\u7965\u005f\u0076\ud83d\udc32\u26bd\ufe0f\u0038\u0040\u3010\u3011";

            Console.WriteLine("原字符串：" + content);
            Console.WriteLine("encodeURI字符串：" + GlobalObject.encodeURI(content));
            Console.WriteLine("decodeURI字符串：" + GlobalObject.decodeURI(content));
            //content = ReplaceEmoji(content, "");

            string str1 = "✝️⚒🛰";
            string str2 = "bakhhz️🏼🏼🏽🏽🏿🕵🏿‍♂️🏽‍♂️🏾‍‍️⛹🏽‍♀️🏽🛣🏖🗝⛱️️✝️";
            string result1 = test(str1);
            string result2 = test(str2);

            //content = EmojiFilterHelper.FilterEmoji(content);

            Console.WriteLine("处理后字符串：" + content);





            //string content = @"abc123鲁文祥@‘；《<>》标题发送消息";

            //EncodeString(content);
            //EncodeString(content);
            //EncodeString(content);
            //DecodeString(content);
            //DecodeString(content);

            Console.ReadKey();
        }


        public static string RemoveUnknownChar(string content)
        {
            StringBuilder sb = new StringBuilder();
            int start = -1;
            for (int i = 0; i < content.Length; i++)
            {
                char c = content[i];
                if ((int)c >= 55356 && (int)c <= 65280)
                {
                    //记录起始位置
                    if (start == -1)
                    {
                        start = i;
                    }
                    //如果已经记录起始位置，并且到最后一个
                    else if (i == content.Length - 1)
                    {
                        sb.Append("");
                        start = -1;
                    }
                }
                else
                {
                    //如果已经记录了起始位置，并且当前不是不可识别字符
                    if (start != -1)
                    {
                        sb.Append(content.Substring(start, i - start));
                        start = -1;
                    }
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


        public static void EncodeString(string content)
        {
            Console.WriteLine("原字符串："+content);
            Console.WriteLine("编码后字符串：" + Microsoft.JScript.GlobalObject.encodeURI(content));
        }

        public static void DecodeString(string content)
        {
            Console.WriteLine("原字符串：" + content);
            Console.WriteLine("解码后字符串：" + Microsoft.JScript.GlobalObject.decodeURI(content));
        }

        public static string test(string str)
        {
            string result = Regex.Replace(str, @"\p{Cs}", "");
            return result;
        }

        /// <summary>  
        /// 清除文本中的Emoji表情符
        /// </summary>  
        /// <param name="patrn">要替换的标签正则表达式</param>  
        /// <param name="strRep">替换为的内容</param>  
        /// <param name="content">要替换的内容</param>  
        /// <returns></returns>  
        public static string ReplaceEmoji(string content, string strRep)
        {
            try
            {
                string patrn = @"\\ud83c[\\udc00-\\udfff]|\\ud83d[\\udc00-\\udfff]|[\\u2600-\\u27ff]";


                //patrn = @"^[\ud83c\udc00-\ud83c\udfff]+$";
                //patrn = @"[^\\u0000-\\uFFFF]";
                //patrn = @"\p{Cs}";
                if (string.IsNullOrEmpty(content))
                {
                    content = "";
                }

                string strTxt = Regex.Replace(content, patrn, "");
                //Regex rgEx = new Regex(patrn, RegexOptions.IgnoreCase);
                //string strTxt = rgEx.Replace(content, strRep);
                return strTxt;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "";
        }

    }
}
