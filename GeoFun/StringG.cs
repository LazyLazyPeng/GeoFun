using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class StringG:object
    {
        private StringBuilder internalString = new StringBuilder("");

        /// <summary>
        /// 将负索引转换为正索引,例如对于字符串"abcd" -1 => 3
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int NegIndex2PosIndex(int index)
        {
            if(index < 0)
            {
                return internalString.Length + index;
            }
            else
            {
                return index;
            }
        }

        /// <summary>
        /// 获取某个序号的字符
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                if(index < 0)
                {
                    return internalString[internalString.Length + index];
                }
                else
                {
                    return internalString[index];
                }
            }
            set
            {
                if (index < 0)
                {
                    internalString[internalString.Length + index] = value;
                }
                else
                {
                    internalString[index] = value;
                }
            }
        }

        /// <summary>
        /// 对字符串进行切片
        /// </summary>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public StringG this[string indexes]
        {
            get
            {
                if(indexes == null)
                {
                    return this;
                }

                if(indexes.Contains(":"))
                {
                    string[] segs = indexes.ToString().Trim().Split(':');
                    if(segs.Length < 2)
                    {
                        throw new FormatException("Slice format \"" + indexes + "\" error.");
                    }

                    int start = int.Parse(segs[0]);
                    int end = int.Parse(segs[1]);

                    start = NegIndex2PosIndex(start);
                    end = NegIndex2PosIndex(end);

                    int length = end - start;

                    if (length <= 0) return new StringG("");
                    else
                    {
                        string str = internalString.ToString().Substring(start, length);
                        return new StringG(str);
                    }
                }

                else
                {
                    int index = int.Parse(indexes);
                    return new StringG(this[index]);
                }
            }
        }

        public StringG(string str)
        {
            internalString = new StringBuilder(str);
        }
        public StringG(char c):this(c.ToString())
        {

        }
        public StringG(char[] chars):this(new string(chars))
        {
        }

        override
        public string ToString()
        {
            return internalString.ToString();
        }
    }
}
