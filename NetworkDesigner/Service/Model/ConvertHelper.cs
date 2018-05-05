using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetworkDesigner.Utils.Common;
namespace NetworkDesigner.Service.Model
{
    class SimConverter
    {
        private string type = "整数";
        public SimConverter(string type)
        {
            this.type = type;
        }
    }
    class ConvertHelper
    {
        private static List<string> _Converts;
        public static Dictionary<string, SimConverter> Converts;
        static ConvertHelper()
        {
            _Converts = new List<string>();
            _Converts.AddRange(new string[]{
                "整数",
                "实数",
                "字符串",
                "整数列表",
                "实数列表",
                "字符串列表",
                "模型列表",
                "文件",
                "坐标",
                "时间",
                "带宽",
                "功率",
                "速率",
                "IPMask",
                "IPAddr",
            });
            Converts = new Dictionary<string, SimConverter>();
            foreach(string type in _Converts)
                Converts[type] = new SimConverter(type);
        }
        /// <summary>
        /// 把多余的空格去除保留一个
        /// </summary>
        public static string TrimString(string str)
        {
            string[] arr = str.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", arr);
        }

        /// <summary>
        /// 字符串转换器，始终会返回非null的list，内部不处理异常
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ListIntConverter(string input,char sep=',')
        {
            string[] strs = input.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries);
            List<int> result = new List<int>();
            string ss="";
            foreach (string str in strs)
            {
                ss = str.Trim();
                if(ss.Length > 0)
                    result.Add(int.Parse(ss));
            }
            return result;
        }
        public static string ListIntConverter(List<int> input, char sep = ',')
        {
            if (input.Count == 0)
                return "";
            string result = "";
            if (input.Count < 10)
            {
                result += input[0];
                for (int i = 0; i < input.Count; i++)
                {
                    if (i != 0)
                        result += sep + input[i];
                }
            }
            else 
            {
                StringBuilder sbd = new StringBuilder(result);
                foreach (int data in input)
                {
                    sbd.Append(data).Append(sep);
                }
                if (sbd.Length > 0)
                    sbd.Remove(sbd.Length - 1, 1);
                result = sbd.ToString();
            }
            return result;
        }
        /// <summary>
        /// 字符串转换器，始终会返回非null的list，内部不处理异常
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<double> ListDoubleConverter(string input, char sep = ',')
        {
            string[] strs = input.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries);
            List<double> result = new List<double>();
            string ss = "";
            foreach (string str in strs)
            {
                ss = str.Trim();
                if (ss.Length > 0)
                    result.Add(double.Parse(ss));
            }
            return result;
        }
        public static string ListDoubleConverter(List<double> input, char sep = ',')
        {
            if (input.Count == 0)
                return "";
            string result = "";
            if (input.Count < 10)
            {
                result += input[0];
                for(int i=0;i<input.Count;i++)
                {
                    if (i != 0)
                        result += sep + input[i];
                }
            }
            else
            {
                StringBuilder sbd = new StringBuilder(result);
                foreach (double data in input)
                {
                    sbd.Append(data).Append(sep);
                }
                if (sbd.Length > 0)
                    sbd.Remove(sbd.Length - 1, 1);
                result = sbd.ToString();
            }
            return result;
        }
        /// <summary>
        /// 以{}作分隔符的字符串转换器，始终会返回非null的list，内部不处理异常
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> ListStringConverter(string input)
        {
            string temp;
            
            string[] strs;
            List<string> result = new List<string>();
            
            bool isValid = true;
            if (!input.Contains('}'))
            {
                if (input.Contains('{'))
                    isValid = false;
                else if(input.Length > 0)
                    result.Add(input);
            }
            else
            {
                strs = input.Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries); //去掉最后的""
                for (int i=0;i < strs.Length;i++) 
                {
                    isValid = true;
                    temp = strs[i].Trim();
                    if (temp.StartsWith("{"))
                    {
                        temp = temp.Substring(1, temp.Length - 1);
                        if (temp.Contains("{"))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }

                    result.Add(temp);
                }
            }
            if (!isValid)
            {
                throw new Exception("字符串不是严格以{}分隔" + input);
            }
            return result;
        }
        /// <summary>
        /// 返回以{}包裹列表中各string的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ListStringConverter(List<string> input)
        {
            if (input.Count == 0)
                return "";
            string result = "";
            if (input.Count < 10)
            {
                foreach (string str in input)
                {
                    result += "{" + str + "}";
                }
            }
            else
            {
                StringBuilder sbd = new StringBuilder(result);
                foreach (string str in input)
                {
                    sbd.Append("{").Append(str).Append("}");
                }
                result = sbd.ToString();
            }
            return result;
        }

        /// <summary>
        /// key-value类属性字符串解析，例如a=b c=d
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<string, string> SliceKVStrConverter(string input, char sep = ' ')
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] ms = input.Split(new char[]{sep}, StringSplitOptions.RemoveEmptyEntries);
            string[] ss;
            string cmd;
            string temp;
            for (int i = 0; i < ms.Length; i++)
            {
                cmd = ms[i].Trim();
                if (cmd.Length != 0)
                {
                    ss = cmd.Split('=');
                    if (ss.Length == 2)
                    {
                        temp = ss[0].Trim();
                        if (temp.Length > 0)
                            result[temp] = ss[1].Trim();
                    }
                    else
                    {
                        LogHelper.LogInfo("无效的k-v属性字符串：" + cmd);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 分片选取下标，支持,-:等，例如输入1:3,-1将解析出1 2 3和count-1作为下标
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> SliceIntConverter(int count, string input)
        {
            bool[] added = new bool[count];
            for (int i = 0; i < count; i++)
                added[i] = false;
            string[] ms = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string cmd;
            foreach (string ins in ms)
            {
                cmd = ins.Trim();
                if (cmd.Length == 0)
                    continue;
                if (cmd.Contains(':'))
                {
                    string[] ss = cmd.Split(':');
                    if (ss.Length != 2)
                        throw new Exception();
                    int start = int.Parse(ss[0]); 
                    if (start < 0) //不支持倒数反方向切片
                        start = count + start; //负数从末尾开始选取
                    int end = int.Parse(ss[1]);
                    if (end < 0)
                        end = count + end; //负数从末尾开始选取
                    for (int i = start; i <= end; i++) //end要取
                        added[i] = true;
                }
                else
                {
                    int d = int.Parse(cmd);
                    if (d < 0)
                        added[count + d] = true;//越界异常直接抛给上层处理
                    else
                        added[d] = true;
                }
            }
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if (added[i])
                    result.Add(i);
            }
            return result;
        }
    }
}
