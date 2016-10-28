using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emoji过滤Demo
{
    public static class EmojiFilterHelper
    {
        /// <summary>
        /// 过滤Emoji
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string FilterEmoji(string content)
        {
            if (!IsContainEmoji(content))
                return content;

            StringBuilder builder = null;

            int len = content.Length;

            for (int i = 0; i < len; i++)
            {
                char ch = content[i];

                if (IsEmojiCharacter(ch))
                {
                    if (builder == null)
                    {
                        builder = new StringBuilder(content.Length);
                    }

                    builder.Append(ch);
                }
                else
                {
                }
            }

            if (builder == null)
                return content;

            if (builder.Length == len)
            {
                //这里的意义在于尽可能少的toString，因为会重新生成字符串
                builder = null;
                return content;
            }
            else
            {
                return builder.ToString();
            }
        }

        /// <summary>
        /// 是否包含Emoji
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool IsContainEmoji(string content)
        {
            if (string.IsNullOrEmpty(content))
                return false;

            int length = content.Length;

            for (int i = 0; i < length; i++)
            {
                char ch = content[i];

                if (IsEmojiCharacter(ch))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 是否是Emoji字符
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsEmojiCharacter(char ch)
        {
            return (ch == 0x0) ||
                    (ch == 0x9) ||
                    (ch == 0xA) ||
                    (ch == 0xD) ||
                    ((ch >= 0x20) && (ch <= 0xD7FF)) ||
                    ((ch >= 0xE000) && (ch <= 0xFFFD)) ||
                    ((ch >= 0x10000) && (ch <= 0x10FFFF));
        }
    }
}
