using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syncfusion.Windows.Forms.Diagram;
using NetworkDesigner.Service.Topology;
using NetworkDesigner.Beans.Model;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.Beans.DataStruct
{
    public class TopoConstants
    {
        /// <summary>
        /// 圆形直径大小
        /// </summary>
        public static int ellipseD = 32;
        /// <summary>
        /// 圆形半径大小
        /// </summary>
        public static int ellipseR = ellipseD / 2;
    }
    public class CommandParser
    {
        /// <summary>
        /// 将字符串按sep分隔为数字，返回list，转换异常返回null
        /// </summary>
        public static List<float> parseNums(string text,char sep=',')
        {
            List<float> nums = new List<float>();
            string[] strs = text.Split(sep);
            try
            {
                foreach (string str in strs)
                {
                    if (!string.IsNullOrWhiteSpace(str))
                        nums.Add(float.Parse(str));
                }
            }
            catch (Exception)
            {
                return null;
            }

            return nums;
        }
    }
    /// <summary>
    /// 暂时不用
    /// </summary>
    public class TopoRange
    {
        public PointF local;
        /// <summary>
        /// w表示x方向的长度 h表示y方向的长度
        /// </summary>
        public SizeF size;
        public TopoRange()
        {
            local = new PointF(0, 0);
            size = new SizeF(300, 300);
        }

        public TopoRange(PointF local, SizeF size)
        {
            this.local = local;
            this.size = size;
        }

        public TopoRange(float x, float y, float w, float h)
        {
            local = new PointF(x, y);
            size = new SizeF(w, h);
        }

        public static TopoRange GetDefault()
        {
            return new TopoRange();
        }

        public static string CheckInput(string[] strs)
        {
            string result = "";
            if ("target".Equals(strs[0]) == false)
            {
                result += "\n请以target开头";
            }
            if (strs.Contains("-l") == false)
            {
                result += "\n请设置基准坐标-l";
            }
            if (strs.Contains("-r") == false)
            {
                result += "\n请设置范围大小-r";
            }
            if (result.Length != 0)
            {
                return "范围参数错误：" + result + "\n";
            }
            return result;
        }
        public static TopoRange Parse(string text)
        {
            float[] xywh={-1,-1,0,0};
            string[] strs = text.Trim().Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
            
            string check = CheckInput(strs);
            if (check.Length != 0)
                throw new Exception(check);
            
            bool[] isSet= {false,false};
            for (int i=0;i<strs.Length;)
            {
                if (i >= strs.Length)
                    break;
                if ("-l".Equals(strs[i]))
                {
                    try
                    {
                        var xy = CommandParser.parseNums(strs[i + 1]);
                        xywh[0] = xy[0];
                        xywh[1] = xy[1];
                        isSet[0] = true;
                    }
                    catch (Exception)
                    {
                        check += "\n参数格式：-l f1,f2";
                    }
                    i = i + 2;
                }
                else if ("-r".Equals(strs[i]))
                {
                    try
                    {
                        var xy = CommandParser.parseNums(strs[i + 1]);
                        xywh[2] = xy[0];
                        xywh[3] = xy[1];
                        isSet[1] = true;
                    }
                    catch (Exception)
                    {
                        check += "\n参数格式：-r f1,f2";
                    }
                    i = i + 2;
                }
                else
                    i++;
            }
            if(!isSet[0])
                check += "\n-l设置错误";
            if(!isSet[1])
                check += "\n-r设置错误";
            if (check.Length != 0)
                throw new Exception("范围参数有误：\n" + check + "\n");
            
            return new TopoRange(xywh[0], xywh[1], xywh[2], xywh[3]);
        }
    }
    public class TopoNode
    {
        public int id = 0;
        public float x = 0;//pos
        public float y = 0;
        public Dictionary<string, string> propSets;
        public Node diagramNode = null;
        public TopoNode()
        {
            propSets = new Dictionary<string, string>();
            propSets["model"] = "实体.主机"; //默认值
        }
        
    }
    public class TopoLink
    {
        public int id = 0;
        public int srcNode = 0;//src node id
        public int desNode = 0;
        public Dictionary<string, string> propSets;
        public TopoLink()
        {
            propSets = new Dictionary<string, string>();
            propSets["model"] = "实体.有线链路";
        }
    }
    public class TopoResult
    {
        public RectangleF range = RectangleF.Empty;
        public List<TopoNode> nodes = new List<TopoNode>();
        public List<TopoLink> links = new List<TopoLink>();
    }
    /// <summary>
    /// 命令行简单参数
    /// </summary>
    public class ArgsParam
    {
        /// <summary>
        /// 参数名称，例如 -p
        /// </summary>
        public string name;

        /// <summary>
        /// 说明文字
        /// </summary>
        public string note;
        /// <summary>
        /// 是否必选
        /// </summary>
        public bool isMust;

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType cType;
        /// <summary>
        /// 默认取值
        /// </summary>
        public string defVal;

        /// <summary>
        /// 取值
        /// </summary>
        public string val;

        public StringCollection selector = null;
    }
    /// <summary>
    /// 命令行补充参数，一行{}
    /// </summary>
    public class SetParam
    {
        /// <summary>
        /// 选择器，目前只支持二维选择，行号+列号
        /// </summary>
        public bool[,] select;
        /// <summary>
        /// 暂存选择命令行，即[]的内容
        /// </summary>
        public string selectStr;
        public string model;
        //public string errorParam = "[ ]";
        /// <summary>
        /// 命令行，名称-取值的形式
        /// </summary>
        public Dictionary<string,string> attrs = new Dictionary<string,string>();
        /// <summary>
        /// 最大总行数，最大总列数，输入cmd
        /// </summary>
        public void ParseSet(int rows, int cols, string input)
        {
            select = new bool[rows,cols];
            for(int i=0;i<rows;i++)
                for(int j=0;j<cols;j++)
                    select[i,j]=false;
            List<string> rowsCmd = new List<string>();
            int iL = 0;
            int iR = 0;
            while (true)
            {
                if (iR < 0)
                    break;
                iL = input.IndexOf('[', iR);
                if (iL < 0)
                    break;
                iR = input.IndexOf(']',iL);
                if (iL < 0 || iR < 0 || iR < iL)
                    break;
                rowsCmd.Add(input.Substring(iL + 1, iR - iL - 1));
            }
            string setting = input.Substring(iR + 1, input.Length - iR -1);
            string cmd;
            switch (rowsCmd.Count) //目前支持最多二维解析，即[][]时前面是行，后面是列，[]时只针对行
            {
                case 0: //没有输入有效值
                    selectStr = "";
                    //throw new Exception();
                    break;
                case 1: //针对行设置，也即列全选
                    cmd = rowsCmd[0].Trim();
                    if (cmd.Length > 0)
                    {
                        List<int> added = ConvertHelper.SliceIntConverter(rows, cmd);
                        foreach (int i in added)
                        {
                            for (int j = 0; j < cols; j++)
                                select[i, j] = true;
                        }
                    }
                    else //输入了值，但没有选择节点---视作针对全部节点设置
                    {
                        for (int i = 0; i < rows; i++)
                            for (int j = 0; j < cols; j++)
                                select[i, j] = true;
                    }
                    selectStr = "[" + cmd + "]";
                    break;
                case 2: //针对行和列指定设置
                    List<int> addedCol = new List<int>();
                    cmd = rowsCmd[1].Trim(); //先解析列
                    if (cmd.Length > 0)
                    {
                        addedCol = ConvertHelper.SliceIntConverter(cols, cmd);
                    }
                    else //输入了[]，但没有选择节点---视作针对全部节点设置
                    {
                        for (int j = 0; j < cols; j++)
                            addedCol.Add(j);
                    }

                    List<int> addedRow = new List<int>();
                    cmd = rowsCmd[0].Trim(); //再解析行
                    if (cmd.Length > 0)
                    {
                        addedRow = ConvertHelper.SliceIntConverter(rows, cmd);
                    }
                    else //输入了[]，但没有选择节点---视作针对全部节点设置
                    {
                        for (int j = 0; j < rows; j++)
                            addedRow.Add(j);
                    }

                    foreach (int i in addedRow)
                        foreach (int j in addedCol)
                            select[i, j] = true;
                    selectStr = "[" + rowsCmd[0] + "]" + "[" + rowsCmd[1] + "]";
                    break;
                default:
                    break;
            }

            attrs = ConvertHelper.SliceKVStrConverter(setting);
            if (attrs.ContainsKey("model"))
                model = attrs["model"];
        }

        public string FormatString()
        {
            StringBuilder sbd = new StringBuilder();

            sbd.Append(selectStr);
            if (attrs.Count != 0)
            {
                foreach (string key in attrs.Keys)
                    sbd.Append(" ").Append(key).Append("=").Append(attrs[key]);
            }
            return sbd.ToString();
        }
    }
    public abstract class TopoBase
    {
        public static string BlankTab = "    ";
        public string Name = "";
        public abstract RectangleF Range { get; set; } //抽象属性
        public List<ArgsParam> args;
        public List<SetParam> pSets = new List<SetParam>();
        //public string defModel = "交换机";

        public TopoBase(string name)
        {
            Name = name;
        }
        public virtual void InitParam()
        {

        }
        public abstract TopoResult DoLayout();
        public virtual int GetTotalRow()
        {
            return 0;
        }
        public virtual string GetCmdText()
        {
            return "";
        }
        public bool ValidateSetParam(string input)
        {
            List<string> rowsCmd = new List<string>();
            int iL = 0;
            int iR = 0;

            int iL2 = 0;
            while (true) //[]和[][]有效，[[]]、[[]、[]]、都无效
            {
                iL = input.IndexOf('[', iR);
                if (iL < 0)
                {
                    if (input.IndexOf(']', iR) > 0)//没有[却有]
                        return false;
                    break;
                }
                if (iL + 1 < input.Length)
                    iL2 = input.IndexOf('[', iL + 1);
                else
                    iL2 = -1;
                iR = input.IndexOf(']', iL);
                if (iR < 0) //有[却没有]
                    return false;
                if (iL2 != -1 && iL2 < iR) //[[]的情况
                    return false;
                rowsCmd.Add(input.Substring(iL + 1, iR - iL - 1));
                iR++;
                if (iR == input.Length)
                    break;
            }
            if (rowsCmd.Count == 0)
                return false;
            return true;
        }
    }

    public class TopoGrid : TopoBase
    {
        //静态参数
        public const string Info = "格型，将区域等分为空格依次放置节点";
        public static readonly Dictionary<string, string> ArgNotes;
        static TopoGrid()
        {
            ArgNotes = new Dictionary<string, string>();
            ArgNotes.Add("-n", "节点总个数，正整数，默认10");
            ArgNotes.Add("-c", "最多放置几列，正整数，可选");
            ArgNotes.Add("-r", "最多放置几行，正整数，可选");
            ArgNotes.Add("-p", "补充参数，{}分隔的字符串数组，可选");
        }

        public TopoGrid() : base("格型")
        {
            args = new List<ArgsParam>();
            ArgsParam param;

            param = new ArgsParam();
            param.name = "-range";
            param.note = "布局范围*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = string.Format("{0},{1},{2},{3}",
                Range.X, Range.Y, Range.Width, Range.Height);
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-n";
            param.note = "节点总个数*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "10";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-c";
            param.note = "最多放置几列";
            param.isMust = false;
            param.cType = ControlType.textbox;
            param.defVal = "";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-r";
            param.note = "最多放置几行";
            param.isMust = false;
            param.cType = ControlType.textbox;
            param.defVal = "";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-p";
            param.note = "补充参数";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "{[0:-1][] model=PIM.实体.节点.host}";
            param.val = param.defVal;
            args.Add(param);
        }
        private RectangleF range = new RectangleF(0, 0, 200, 200);
        private int m_nums = 10;
        private int m_cols = -1;
        private int m_rows = -1;
        public override int GetTotalRow()
        {
            return m_rows;
        }
        public override RectangleF Range 
        {
            get { return range; }
            set
            {
                range = value;
                foreach (var arg in args)
                {
                    if (arg.name.Equals("-range"))
                    {
                        arg.val = string.Format("{0},{1},{2},{3}",
                                    range.X, range.Y, range.Width, range.Height);
                        break;
                    }
                }
            }
        }
        public override string GetCmdText()
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("layout ").Append(Name).Append("\n");
            foreach (var arg in this.args)
            {
                if (arg.isMust || !arg.defVal.Equals(arg.val))
                {
                    sbd.Append(BlankTab).Append(arg.name).Append(" ").Append(arg.val).Append("\n");
                }
            }
            sbd.Append(BlankTab).Append(";");
            return sbd.ToString();
        }
        public override void InitParam()
        {
            StringBuilder error = new StringBuilder();
            string temp;
            string paramSet="";
            foreach (var arg in args)
            {
                if (arg.val.Length == 0)
                    continue;
                try
                {
                    switch (arg.name)
                    {
                        case "-range":
                            string[] strs = arg.val.Split(',');
                            if (strs.Length < 4)
                                throw new Exception();
                            range.X = float.Parse(strs[0]);
                            range.Y = float.Parse(strs[1]);
                            range.Width = float.Parse(strs[2]);
                            range.Height = float.Parse(strs[3]);
                            break;
                        case "-n":
                            m_nums = int.Parse(arg.val);
                            if (m_nums <= 0)
                                throw new Exception();
                            break;
                        case "-c":
                            m_cols = int.Parse(arg.val);
                            if (m_cols <= 0)
                                throw new Exception();
                            break;
                        case "-r":
                            m_rows = int.Parse(arg.val);
                            if (m_rows <= 0)
                                throw new Exception();
                            break;
                        case "-p":
                            paramSet = arg.val;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    temp = "未知格式";
                    if (ArgNotes.ContainsKey(arg.name))
                        temp = ArgNotes[arg.name];
                    if (error.Length > 0)
                        error.Append("\n");
                    error.Append(arg.name).Append(" 错误：").Append(temp);
                }

            }

            if (m_cols > 0)
            {
                m_rows = (int)Math.Ceiling(1.0 * m_nums / m_cols);
            }
            else if (m_rows > 0)
            {
                m_cols = (int)Math.Ceiling(1.0 * m_nums / m_rows);
            }
            else
            {
                m_cols = (int)Math.Ceiling(Math.Sqrt(m_nums));
                m_rows = (int)Math.Ceiling(1.0 * m_nums / m_cols);
            }
            try 
	        {
                var inputs = ConvertHelper.ListStringConverter(paramSet);
                pSets = ParseSetParams(inputs);
	        }
	        catch (Exception)
	        {
                temp = "未知格式";
                if (ArgNotes.ContainsKey("-p"))
                    temp = ArgNotes["-p"];
                if (error.Length > 0)
                    error.Append("\n");
                error.Append("-p").Append(" 错误：").Append(temp);
	        }
            if (error.Length != 0)
                throw new Exception(error.ToString());
        }

        public List<SetParam> ParseSetParams(List<string> input)
        {
            List<SetParam> sps = new List<SetParam>();
            SetParam param;
            string cmd;
            foreach (var str in input)
            {
                cmd = str.Trim();
                if (cmd.Length == 0)
                    continue;
                param = new SetParam();
                try
                {
                    param.ParseSet(m_rows, m_cols, cmd);
                    sps.Add(param);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                
            }
            return sps;
        }

        public override TopoResult DoLayout()
        {
            TopoResult result = new TopoResult();
            result.range = range;

            float xSpace = range.Width / m_cols;//同一行内相邻node的间距
            float ySpace = range.Height / m_rows;//同一列内相邻node的间距
            float xStart = range.X + xSpace / 2;//垂直基准线：第一列node的X
            float yStart = range.Y + ySpace / 2;//水平基准线：第一行node的Y
            int nc = 0;//第几列
            int nr = 0;//第几行
            TopoNode node = null;
            TopoNode[,] temp = new TopoNode[m_rows,m_cols]; //按行和列暂存，便于快速访问
            for (int i = 0; i < m_nums; i++)
            {
                node = new TopoNode(); //先生成全部节点，在第二次遍历时才生成参数，更安全
                result.nodes.Add(node);
                node.id = i;
                node.x = xStart + nc * xSpace;
                node.y = yStart + nr * ySpace;
                temp[nr, nc] = node;

                nc++;
                if (nc == m_cols) //下一行
                {
                    nc = 0;
                    nr++;
                }
            }
            //开始设置参数
            int num = 0;
            bool isDone;
            foreach (SetParam param in pSets)
            {
                num = 0;
                isDone = false;
                for (int i = 0; i < m_rows; i++)
                {
                    for (int j = 0; j < m_cols; j++)
                    {
                        num++; 
                        if (num > m_nums) //总个数够了
                        {
                            isDone = true;
                            break;
                        }
                        if (!param.select[i, j])
                           continue;
                        node = temp[i, j];
                        foreach (var kv in param.attrs)
                            node.propSets[kv.Key] = kv.Value;
                        //todo：解析tool，例如linktool生成链路
                        //if (node.propSets["model"].Equals("Tool"))
                        //{ }
                    }
                    if (isDone)
                        break;
                }
            }
            return result;
        }
    }
    public class TopoRandom : TopoGrid
    {
        public TopoRandom()
        {
            this.Name = "随机";
        }
    }
    public class TopoStar : TopoBase
    {
        //静态参数
        public const string Info = "星型，由中心节点向外连接，呈放射状";
        public static readonly Dictionary<string, string> ArgNotes;
        static TopoStar()
        {
            ArgNotes = new Dictionary<string, string>();
            ArgNotes.Add("-r", "几层环状节点，默认2");
            ArgNotes.Add("-n", "外层节点个数，默认5，中心节点个数始终为1");
            ArgNotes.Add("-h", "最外层节点各挂载几个节点，可选");
            ArgNotes.Add("-f", "标志位：L是否带链路，H是否外层挂载节点带链路");
            ArgNotes.Add("-p", "补充参数，{}分隔的字符串数组，可选");
        }

        public TopoStar()
            : base("星型")
        {
            args = new List<ArgsParam>();
            ArgsParam param;

            param = new ArgsParam();
            param.name = "-range";
            param.note = "布局范围*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = string.Format("{0},{1},{2},{3}",
                Range.X, Range.Y, Range.Width, Range.Height);
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-r";
            param.note = "节点有几层*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "2";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-n";
            param.note = "外层节点个数"; //中心只有一个
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "5";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-h";
            param.note = "挂载节点个数";
            param.isMust = false;
            param.cType = ControlType.textbox;
            param.defVal = "0";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-f";
            param.note = "可选标志位";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "HL";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-p";
            param.note = "补充参数";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "{[0] model=PIM.实体.节点.router}{[1:-1] model=PIM.实体.节点.switch}{[-1] model=PIM.实体.节点.host}";
            param.val = param.defVal;
            args.Add(param);
        }
        private RectangleF range = new RectangleF(0, 0, 200, 200);

        private int MaxRow 
        {
            get { return m_rows + (m_host>0 ? 1:0) ; }
        }
        private int MaxCol
        {
            get { return m_npr * (m_host > 0 ? m_host : 1); }
        }

        private int m_rows = 2;
        private int m_npr = 5;
        private int m_host = 0;
        private string m_flag = "L";

        public override int GetTotalRow()
        {
            return MaxRow;
        }
        public override RectangleF Range
        {
            get { return range; }
            set
            {
                range = value;
                foreach (var arg in args)
                {
                    if (arg.name.Equals("-range"))
                    {
                        arg.val = string.Format("{0},{1},{2},{3}", range.X, range.Y, range.Width, range.Height);
                        break;
                    }
                }
            }
        }
        public override string GetCmdText()
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("layout ").Append(Name).Append("\n");
            foreach (var arg in this.args)
            {
                if (arg.isMust || !arg.defVal.Equals(arg.val))
                {
                    sbd.Append(BlankTab).Append(arg.name).Append(" ").Append(arg.val).Append("\n");
                }
            }
            sbd.Append(BlankTab).Append(";");
            return sbd.ToString();
        }

        public override void InitParam()
        {
            StringBuilder error = new StringBuilder();
            string temp;
            string paramSet="";
            foreach (var arg in args)
            {
                if (arg.val.Length == 0)
                    continue;
                try
                {
                    switch (arg.name)
                    {
                        case "-range":
                            string[] strs = arg.val.Split(',');
                            if (strs.Length < 4)
                                throw new Exception();
                            range.X = float.Parse(strs[0]);
                            range.Y = float.Parse(strs[1]);
                            range.Width = float.Parse(strs[2]);
                            range.Height = float.Parse(strs[3]);
                            break;
                        case "-r":
                            m_rows = int.Parse(arg.val);
                            if (m_rows < 1)
                                throw new Exception();
                            break;
                        case "-n":
                            m_npr = int.Parse(arg.val);
                            if (m_rows < 0)
                                throw new Exception();
                            break;
                        case "-h":
                            m_host = int.Parse(arg.val);
                            if (m_host < 0)
                                m_host = 0;
                            break;
                        case "-f":
                            m_flag = arg.val;
                            break;
                        case "-p":
                            paramSet = arg.val;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    temp = "未知格式";
                    if (ArgNotes.ContainsKey(arg.name))
                        temp = ArgNotes[arg.name];
                    if (error.Length > 0)
                        error.Append("\n");
                    error.Append(arg.name).Append(" 错误：").Append(temp);
                }
            }
            try
            {
                var inputs = ConvertHelper.ListStringConverter(paramSet);
                pSets = ParseSetParams(inputs);
            }
            catch (Exception)
            {
                temp = "未知格式";
                if (ArgNotes.ContainsKey("-p"))
                    temp = ArgNotes["-p"];
                if (error.Length > 0)
                    error.Append("\n");
                error.Append("-p").Append(" 错误：").Append(temp);
            }
            if (error.Length != 0)
                throw new Exception(error.ToString());
        }
        public bool isAddLink()
        {
            return m_flag.Contains('L');
        }
        public bool isAddHostLink()
        {
            return m_flag.Contains('H');
        }
        public bool isAddHost()
        {
            return m_host > 0;
        }

        public List<SetParam> ParseSetParams(List<string> input)
        {
            List<SetParam> sps = new List<SetParam>();
            SetParam param;
            string cmd;
            foreach (var str in input)
            {
                cmd = str.Trim();
                if (cmd.Length == 0)
                    continue;
                param = new SetParam();
                try
                {
                    param.ParseSet(MaxRow, MaxCol, cmd);
                    sps.Add(param);
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
            return sps;
        }

        public override TopoResult DoLayout()
        {
            TopoResult result = new TopoResult();
            result.range = range;

            double space = (range.Width / 2 - TopoConstants.ellipseR) / (MaxRow - 1); //正中心放节点，占一个row
            double radius = space;
            PointF center = new PointF(range.X + range.Width / 2, range.Y + range.Height / 2);
            List<PointF> points;
            TopoLink link;
            TopoNode node;
            TopoNode[,] temp = new TopoNode[MaxRow, MaxCol]; //按最大行和最大列暂存，便于快速访问
            //中心节点必有
            node = new TopoNode();
            temp[0, 0] = node; //第0行的其他列为空
            node.id = result.nodes.Count; //从0计
            node.x = center.X;
            node.y = center.Y;
            result.nodes.Add(node);
            for (int i = 1; i < m_rows; i++) //不含最外层可能有的主机
            {
                radius = i * space;
                points = TopoHelper.GetPosCircle(m_npr, center, radius);
                for (int j = 0; j < m_npr; j++)
                {
                    node = new TopoNode();
                    temp[i, j] = node;
                    node.id = result.nodes.Count;
                    node.x = points[j].X;
                    node.y = points[j].Y;
                    result.nodes.Add(node);
                }
            }
            bool needLink = isAddLink();
            bool neeHost = isAddHost();
            bool neeHostLink = isAddHostLink();
            if (neeHost) //外层添加树枝节点
            {
                radius = range.Width / 2 - TopoConstants.ellipseR;
                //radius = TopoHelper.GetDistance(center.X,center.Y,tn.x,tn.y) + 0.8 * space;
                double deltaRot = 360.0 / (m_npr * m_host);
                double rotate = 0;
                bool isHostOdd = (m_host % 2 == 1);
                int itemp = m_host / 2;
                if (isHostOdd)
                    rotate = -1 * itemp * deltaRot; //逆时针方向旋转圆环，使对齐
                else
                    rotate = -1 * (itemp - 0.5) * deltaRot;
                //树枝节点仍以中心节点为圆心形成圆弧
                //警告：最外层节点要不旋转才能用下面的公式统一计算树枝节点坐标
                points = TopoHelper.GetPosCircle(m_npr * m_host, center, radius, rotate);
                int index = 0;
                int lastRow = MaxRow - 1;
                for (int i = 0; i < m_npr; i++)
                {
                    itemp = i * m_host;
                    for (int j = 0; j < m_host; j++)
                    {
                        index = itemp + j;

                        node = new TopoNode();
                        temp[lastRow, index] = node;
                        node.id = result.nodes.Count;
                        node.x = points[index].X;
                        node.y = points[index].Y;
                        result.nodes.Add(node);

                        if (neeHostLink)
                        {
                            link = new TopoLink();
                            link.id = result.links.Count;
                            link.srcNode = temp[lastRow-1, i].id;
                            link.desNode = node.id;
                            result.links.Add(link);
                            //todo:设置链路参数
                        }
                    }
                }
            }
            if (needLink && m_rows > 1 ) //除去-h层的外层节点连接到内层节点
            {
                for (int i = 0; i < m_npr; i++) //row1-row0也即连接到中心节点
                {
                    link = new TopoLink();
                    link.id = result.links.Count;
                    link.srcNode = temp[1, i].id;
                    link.desNode = temp[0, 0].id;
                    result.links.Add(link);
                }
                for (int i = 2; i < m_rows; i++)
                {
                    for (int j = 0; j < m_npr; j++) //rowi-rowj也即连接到中心节点
                    {
                        link = new TopoLink();
                        link.id = result.links.Count;
                        link.srcNode = temp[i, j].id;
                        link.desNode = temp[i-1, j].id;
                        result.links.Add(link);
                    }
                }
                //todo:设置链路参数
            }
            //开始设置参数
            int num = 0;
            int maxRow = MaxRow;
            int maxCol = MaxCol;
            int maxNum = 1;
            int endRow = maxRow - 1;
            foreach (SetParam param in pSets)
            {
                for (int i = 0; i < maxRow; i++)
                {
                    if (i == 0)
                        maxNum = 1;
                    else if (i != endRow)
                        maxNum = m_npr;
                    else
                        maxNum = maxCol;
                    num = 0;
                    for (int j = 0; j < maxCol; j++)
                    {
                        num++;
                        if (num > maxNum) //该层总个数够了
                            break;
                        if (!param.select[i, j])
                            continue;
                        node = temp[i, j];
                        foreach (var kv in param.attrs)
                            node.propSets[kv.Key] = kv.Value;
                        //todo：解析tool，例如linktool生成链路
                        //if (node.propSets["model"].Equals("Tool"))
                        //{ }
                    }
                }
            }
            return result;
        }
    }

    public class TopoCircle : TopoBase
    {
        //静态参数
        public const string Info = "环型，无中心节点，呈圆环状";
        public static readonly Dictionary<string, string> ArgNotes;

        static TopoCircle()
        {
            ArgNotes = new Dictionary<string, string>();
            ArgNotes.Add("-r", "几层环状节点，默认1");
            ArgNotes.Add("-n", "各层节点个数，单个数表示各层个数相同，多个逗号分隔的数表示各层节点个数，未指定的层继承临近内层的个数");
            ArgNotes.Add("-h", "最外层节点各挂载几个节点，可选");
            ArgNotes.Add("-f", "标志位：L是否带链路，H是否外层挂载节点带链路");
            ArgNotes.Add("-p", "补充参数，{}分隔的字符串数组，可选");
        }

        public TopoCircle()
            : base("环型")
        {
            m_npr = new List<int>();
            m_npr.Add(5);

            args = new List<ArgsParam>();
            ArgsParam param;

            param = new ArgsParam();
            param.name = "-range";
            param.note = "布局范围*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = string.Format("{0},{1},{2},{3}",
                Range.X, Range.Y, Range.Width, Range.Height);
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-r";
            param.note = "节点有几层*";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "1";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-n";
            param.note = "各层节点个数";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "5";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-h";
            param.note = "挂载节点个数";
            param.isMust = false;
            param.cType = ControlType.textbox;
            param.defVal = "0";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-f";
            param.note = "可选标志位";
            param.isMust = false;
            param.cType = ControlType.textbox;
            param.defVal = "";
            param.val = param.defVal;
            args.Add(param);

            param = new ArgsParam();
            param.name = "-p";
            param.note = "补充参数";
            param.isMust = true;
            param.cType = ControlType.textbox;
            param.defVal = "{[0:-2] model=PIM.实体.节点.switch}{[-1] model=PIM.实体.节点.host}";
            param.val = param.defVal;
            args.Add(param);
        }

        private RectangleF range = new RectangleF(0, 0, 200, 200);

        private int MaxRow
        {
            get { return m_rows + (m_host > 0 ? 1 : 0); }
        }
        private int MaxCol
        {
            get 
            {
                int max = 0;
                foreach(int npr in m_npr)
                {
                    if(npr > max)
                        max = npr;
                }
                return max; 
            }
        }

        private int m_rows = 1;
        private List<int> m_npr;
        private int m_host = 0;
        private string m_flag = "";

        public override RectangleF Range
        {
            get { return range; }
            set
            {
                range = value;
                foreach (var arg in args)
                {
                    if (arg.name.Equals("-range"))
                    {
                        arg.val = string.Format("{0},{1},{2},{3}", range.X, range.Y, range.Width, range.Height);
                        break;
                    }
                }
            }
        }
        public override string GetCmdText()
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("layout ").Append(Name).Append("\n");
            foreach (var arg in this.args)
            {
                if (arg.isMust || !arg.defVal.Equals(arg.val))
                {
                    sbd.Append(BlankTab).Append(arg.name).Append(" ").Append(arg.val).Append("\n");
                }
            }
            sbd.Append(BlankTab).Append(";");
            return sbd.ToString();
        }

        public override void InitParam()
        {
            StringBuilder error = new StringBuilder();
            string temp;
            string paramSet = "";
            foreach (var arg in args)
            {
                if (arg.val.Length == 0)
                    continue;
                try
                {
                    switch (arg.name)
                    {
                        case "-range":
                            string[] strs = arg.val.Split(',');
                            if (strs.Length < 4)
                                throw new Exception();
                            range.X = float.Parse(strs[0]);
                            range.Y = float.Parse(strs[1]);
                            range.Width = float.Parse(strs[2]);
                            range.Height = float.Parse(strs[3]);
                            break;
                        case "-r":
                            m_rows = int.Parse(arg.val);
                            if (m_rows < 1)
                                throw new Exception();
                            break;
                        case "-n":
                            m_npr = ConvertHelper.ListIntConverter(arg.val);
                            if (MaxCol < 1)
                                throw new Exception();
                            break;
                        case "-h":
                            m_host = int.Parse(arg.val);
                            if (m_host < 0)
                                m_host = 0;
                            break;
                        case "-f":
                            m_flag = arg.val;
                            break;
                        case "-p":
                            paramSet = arg.val;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    temp = "未知格式";
                    if (ArgNotes.ContainsKey(arg.name))
                        temp = ArgNotes[arg.name];
                    if (error.Length > 0)
                        error.Append("\n");
                    error.Append(arg.name).Append(" 错误：").Append(temp);
                }
            }
            UpdateNodePerRow(); //必须在ParseSetParams之前调用
            try
            {
                var inputs = ConvertHelper.ListStringConverter(paramSet);
                pSets = ParseSetParams(inputs);
            }
            catch (Exception)
            {
                temp = "未知格式";
                if (ArgNotes.ContainsKey("-p"))
                    temp = ArgNotes["-p"];
                if (error.Length > 0)
                    error.Append("\n");
                error.Append("-p").Append(" 错误：").Append(temp);
            }
            if (error.Length != 0)
                throw new Exception(error.ToString());
        }
        public bool isAddLink()
        {
            return m_flag.Contains('L');
        }
        public bool isAddHostLink()
        {
            return m_flag.Contains('H');
        }
        public bool isAddHost()
        {
            return m_host > 0;
        }
        /// <summary>
        /// 填充各行的节点数，以及更新最大行最大列个数，必须优先调用
        /// </summary>
        private void UpdateNodePerRow()
        {
            foreach (ArgsParam arg in args)
            {
                if(arg.name.Equals("-n"))
                {
                    List<int> npr = ConvertHelper.ListIntConverter(arg.val);
                    if(npr.Count == 0)
                    {
                        for(int i=0;i<m_rows;i++)
                            npr.Add(5);
                    }
                    else if (npr.Count < m_rows)
                    {
                        int temp = npr[npr.Count - 1];
                        while(npr.Count != m_rows)
                            npr.Add(temp);
                    }
                    if (isAddHost())
                    {
                        npr.Add(npr[npr.Count - 1] * m_host);
                    }
                    m_npr = npr;
                    break;
                }
            }
        }
        public List<SetParam> ParseSetParams(List<string> input)
        {
            List<SetParam> sps = new List<SetParam>();
            SetParam param;
            string cmd;
            foreach (var str in input)
            {
                cmd = str.Trim();
                if (cmd.Length == 0)
                    continue;
                param = new SetParam();
                try
                {
                    param.ParseSet(MaxRow, MaxCol, cmd);
                    sps.Add(param);
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
            return sps;
        }
        public override TopoResult DoLayout()
        {
            TopoResult result = new TopoResult();
            result.range = range;

            int mRow = MaxRow;
            int mCol = MaxCol;
            double space = (range.Width / 2 - TopoConstants.ellipseR) / mRow;//正中心不放节点，不占一个row
            double radius = space;
            PointF center = new PointF(range.X + range.Width / 2, range.Y + range.Height / 2);
            List<PointF> points;
            TopoLink link;
            TopoNode node;
            TopoNode[,] temp = new TopoNode[mRow, mCol]; //按最大行和最大列暂存，便于快速访问
            //正中心不放节点
            for (int i = 0; i < m_rows; i++) //外层环形节点
            {
                radius = (i + 1) * space;
                points = TopoHelper.GetPosCircle(m_npr[i], center, radius);
                for (int j = 0; j < points.Count; j++)
                {
                    node = new TopoNode();
                    temp[i, j] = node;
                    node.id = result.nodes.Count;
                    node.x = points[j].X;
                    node.y = points[j].Y;
                    result.nodes.Add(node);
                }
            }
            bool needLink = isAddLink();
            bool neeHost = isAddHost();
            bool neeHostLink = isAddHostLink();
            if (neeHost) //外层添加树枝节点
            {
                radius = range.Width / 2 - TopoConstants.ellipseR;
                double deltaRot = 360.0 / m_npr[m_npr.Count - 1];
                double rotate = 0;
                bool isHostOdd = (m_host % 2 == 1);
                int itemp = m_host / 2;
                if (isHostOdd)
                    rotate = -1 * itemp * deltaRot; //逆时针方向旋转圆环，使对齐
                else
                    rotate = -1 * (itemp - 0.5) * deltaRot;
                //树枝节点仍以中心节点为圆心形成圆弧
                //警告：最外层节点要不旋转才能用下面的公式统一计算树枝节点坐标
                points = TopoHelper.GetPosCircle(m_npr[m_npr.Count - 1], center, radius, rotate);
                int index = 0;
                int lastRow = MaxRow - 1;
                for (int i = 0; i < m_npr[m_npr.Count - 2]; i++)
                {
                    itemp = i * m_host;
                    for (int j = 0; j < m_host; j++)
                    {
                        index = itemp + j;

                        node = new TopoNode();
                        temp[lastRow, index] = node;
                        node.id = result.nodes.Count;
                        node.x = points[index].X;
                        node.y = points[index].Y;
                        result.nodes.Add(node);

                        if (neeHostLink)
                        {
                            link = new TopoLink();
                            link.id = result.links.Count;
                            link.srcNode = temp[lastRow - 1, i].id;
                            link.desNode = node.id;
                            result.links.Add(link);
                            //todo:设置链路参数
                        }
                    }
                }
            }
            if (needLink) //除去-h层的外层节点添加环内链路
            {
                for (int i = 0; i < m_rows; i++)
                {
                    int col=m_npr[i];
                    for (int j = 1; j < col; j++) //环内链路
                    {
                        link = new TopoLink();
                        link.id = result.links.Count;
                        link.srcNode = temp[i, j-1].id;
                        link.desNode = temp[i, j].id;
                        result.links.Add(link);
                    }
                    if (col > 2)
                    {
                        link = new TopoLink();
                        link.id = result.links.Count;
                        link.srcNode = temp[i, col - 1].id;
                        link.desNode = temp[i, 0].id;
                        result.links.Add(link);
                    }
                }
                //todo:设置链路参数
            }
            //开始设置参数
            int num = 0;
            int maxNum = 1;
            foreach (SetParam param in pSets)
            {
                for (int i = 0; i < mRow; i++)
                {
                    maxNum = m_npr[i];
                    num = 0;
                    for (int j = 0; j < mCol; j++)
                    {
                        num++;
                        if (num > maxNum) //该层总个数够了
                            break;
                        if (!param.select[i, j])
                            continue;
                        node = temp[i, j];
                        foreach (var kv in param.attrs)
                            node.propSets[kv.Key] = kv.Value;
                        //todo：解析tool，例如linktool生成链路
                        //if (node.propSets["model"].Equals("Tool"))
                        //{ }
                    }
                }
            }
            return result;
        }
    }
    public class TopoMix : TopoCircle
    {
        public TopoMix()
        {
            this.Name = "混合型";
        }
    }
    public class TopoNets
    {
        //静态参数
        public const string Info = "网状，同单层的环型，不过两两之间有链路";
        public static readonly Dictionary<string, string> Lines;

        static TopoNets()
        {
            Lines = new Dictionary<string, string>();
            Lines.Add("-n", "-n 1 单层节点放置几个*");
            Lines.Add("-f", "-f k 标志位：k不带链路，默认带");
            Lines.Add("-p", "-p node[n] pX=x,pY=(y1,y2) 参数");
        }
        
        private int m_npr = 1;
        private string m_flag = "";
        private string m_pSets = "";

        private void InitParam(Dictionary<string, string> iparam)
        {
            foreach (var item in iparam)
            {
                switch (item.Key)
                {
                    case "-n":
                        m_npr = int.Parse(item.Value);
                        if (m_npr <= 0)
                            throw new Exception("节点个数错误\n");
                        break;
                    case "-f":
                        m_flag = item.Value;
                        break;
                    case "-p":
                        m_pSets = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }
        private bool isAddLink()
        {
            return !m_flag.Contains('k');
        }
        public void DoLayout(TopoResult topoResult, Dictionary<string, string> iparam)
        {
            try
            {
                InitParam(iparam);
            }
            catch (Exception)
            {
                throw new Exception("参数错误\n");
            }

            TopoRange range = new TopoRange();//topoResult.range;
            double space = range.size.Width / 2 - TopoConstants.ellipseD;
            double radius = space;
            PointF center = new PointF(range.local.X + range.size.Width / 2, range.local.Y + range.size.Height / 2);
            TopoLink link;
            TopoNode node;
            List<PointF> points = TopoHelper.GetPosCircle(m_npr, center, radius);
            for (int i = 0; i < m_npr; i++)
            {
                node = new TopoNode();
                node.id = topoResult.nodes.Count;
                topoResult.nodes.Add(node);
                node.x = points[i].X;
                node.y = points[i].Y;
            }
            if (isAddLink())
            {
                bool[,] isAdd = new bool[m_npr, m_npr];//下标表示节点编号，值为true表示已连接
                for (int i = 0; i < m_npr; i++)
                {
                    for (int j = 0; j < m_npr; j++)
                    {
                        if (i != j)
                        {
                            if (isAdd[i, j] || isAdd[j, i])
                                continue;
                            link = new TopoLink();
                            link.id = topoResult.links.Count;
                            link.srcNode = topoResult.nodes[i].id;
                            link.desNode = topoResult.nodes[j].id;
                            topoResult.links.Add(link);
                            isAdd[i, j] = true;
                        }
                    }
                }
            }
        }
    }

    public class TopoBus
    {
        //静态参数
        public const string Info = "总线型，同单层的星型，只是中心节点表示广播域";
        public static readonly Dictionary<string, string> Lines;

        static TopoBus()
        {
            Lines = new Dictionary<string, string>();
            Lines.Add("-n", "-n 1 单层节点放置几个*");
            Lines.Add("-p", "-p node[n] pX=x,pY=(y1,y2) 参数");
        }

        private int m_npr = 1;
        //private string m_flag = "";
        private string m_pSets = "";

        private void InitParam(Dictionary<string, string> iparam)
        {
            foreach (var item in iparam)
            {
                switch (item.Key)
                {
                    case "-n":
                        m_npr = int.Parse(item.Value);
                        if (m_npr <= 0)
                            throw new Exception("节点个数错误\n");
                        break;
                    case "-p":
                        m_pSets = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public void DoLayout(TopoResult topoResult, Dictionary<string, string> iparam)
        {
            try
            {
                InitParam(iparam);
            }
            catch (Exception)
            {
                throw new Exception("参数错误\n");
            }

            TopoRange range = new TopoRange();//topoResult.range;
            double space = range.size.Width / 2 - TopoConstants.ellipseD;
            double radius = space;
            PointF center = new PointF(range.local.X + range.size.Width / 2, range.local.Y + range.size.Height / 2);
            TopoLink link;
            TopoNode node = new TopoNode();
            node.id = 0;
            node.x = center.X;//正中心的一个节点
            node.y = center.Y;
            topoResult.nodes.Add(node);
            List<PointF> points = TopoHelper.GetPosCircle(m_npr, center, radius);
            for (int i = 0; i < m_npr; i++)
            {
                node = new TopoNode();
                node.id = topoResult.nodes.Count;
                topoResult.nodes.Add(node);
                node.x = points[i].X;
                node.y = points[i].Y;

                link = new TopoLink();
                link.id = topoResult.links.Count;
                link.srcNode = topoResult.nodes[0].id;
                link.desNode = node.id;
                topoResult.links.Add(link);
            }
        }
    }

    public class TopoTree
    {
        //静态参数
        public const string Info = "树型结构";
        public static readonly Dictionary<string, string> Lines;

        static TopoTree()
        {
            Lines = new Dictionary<string, string>();
            Lines.Add("-r", "-r 2 树层数，默认2，不设置-n时起作用");
            Lines.Add("-m", "-m 3 m叉树，默认3，不设置-n时起作用");
            Lines.Add("-n", "-n 1,3,(2,1,1) 各层节点的个数及父子关系");
            Lines.Add("-p", "-p node[r][n] pX=x,pY=(y1,y2) 参数");
        }
        private int m_rows = 1;
        private int m_mTree = 3;
        private List<object> m_nDefTree = null;
        private string m_pSets = "";

        private void InitParam(Dictionary<string, string> iparam)
        {
            foreach (var item in iparam)
            {
                switch (item.Key)
                {
                    case "-r":
                        m_rows = int.Parse(item.Value);
                        if (m_rows <= 0)
                            throw new Exception("树层数\n");
                        break;
                    case "-m":
                        m_mTree = int.Parse(item.Value);
                        if (m_mTree <= 0)
                            throw new Exception("树叉数\n");
                        break;
                    case "-n":
                        PareseDefTree(item.Value);
                        break;
                    case "-p":
                        m_pSets = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }
        public void PareseDefTree(string defTree)
        {
            if (defTree.Length == 0)
                return;
            m_nDefTree = new List<object>();
            List<int> iList;
            bool isList = false;
            string str = "";
            StringBuilder sbd = new StringBuilder();
            char[] defs = defTree.ToCharArray();
            for (int i = 0; i < defs.Length; i++)
            {
                switch(defs[i])
                {
                    case ',':
                        if (!isList)
                        {
                            str = sbd.ToString().Trim();
                            sbd.Clear();
                            if (str.Length == 0)
                                m_nDefTree.Add(0); //允许,,表示填2个0，与0,0等效
                            else
                                m_nDefTree.Add(int.Parse(str));
                        }
                        else
                        {
                            sbd.Append(defs[i]);
                        }
                        break;
                    case '(':
                        isList = true;
                        sbd.Clear();
                        break;
                    case ')':
                        isList = false;
                        str = sbd.ToString().Trim();
                        sbd.Clear();
                        iList = new List<int>();
                        foreach (string s in str.Split(','))
                        {
                            str = s.Trim();
                            if (str.Length == 0)
                                iList.Add(0); //允许,,表示填2个0，与0,0等效
                            else
                                iList.Add(int.Parse(str));
                        }
                        m_nDefTree.Add(iList);
                        break;
                    default:
                        sbd.Append(defs[i]);
                        break;
                }
            }
            str = sbd.ToString().Trim();
            if (str.Length != 0) //结尾没有,但有数据的情况
            {
                if (isList)
                {
                    isList = false;
                    iList = new List<int>();
                    foreach (string s in str.Split(','))
                    {
                        str = s.Trim();
                        if (str.Length == 0)
                            iList.Add(0); //允许,,表示填2个0，与0,0等效
                        else
                            iList.Add(int.Parse(str));
                    }
                    m_nDefTree.Add(iList);
                }
                else
                {
                    m_nDefTree.Add(int.Parse(str));
                }
            }

            sbd.Clear();
        }

        /// <summary>
        /// 将当前行的节点平分到上一行节点作子节点，除不尽的补在最后一个父节点下，不够的补0
        /// </summary>
        public List<int> SplitTreeRow(int curRowCount, int preRowCount)
        {
            List<int> childs = new List<int>(preRowCount);
            if (preRowCount == 0)
                return childs;
            
            int c = curRowCount / preRowCount;
            int d = curRowCount % preRowCount;
            for (int i = 0; i < preRowCount; i++)
                childs.Add(c);
            if (d != 0)
                childs[preRowCount - 1] += d;
            return childs;
        }
        public void DoLayout(TopoResult topoResult, Dictionary<string, string> iparam)
        {
            try
            {
                InitParam(iparam);
            }
            catch (Exception)
            {
                throw new Exception("参数错误\n");
            }
            TopoRange topoRange = new TopoRange();//topoResult.range;
            float xSpace,ySpace;
            float xStart,yStart;
            int nr; //第 nr 行
            int maxRows = m_rows;
            int maxCols = 0;//GetMaxCols();
            List<TopoNode> preRow = null;
            List<TopoNode> curRow = null;
            TopoNode node = null;
            TopoLink link = null;
            if (m_nDefTree == null) // r层 m叉树
            {
                ySpace = topoRange.size.Height / maxRows;
                yStart = topoRange.local.Y + ySpace / 2;//水平基准线：第一行node的Y
                for (int i = 0; i < m_rows; i++)
                {
                    nr = i; //第 nr 层树节点
                    maxCols = (int)Math.Pow(m_mTree, i);
                    curRow = new List<TopoNode>(maxCols);
                    xSpace = topoRange.size.Width / maxCols;
                    xStart = topoRange.local.X + xSpace / 2;
                    if (nr == 0)  //这是第0层，放根节点
                    {
                        node = new TopoNode();
                        node.id = topoResult.nodes.Count;
                        node.x = xStart + 0 * xSpace;
                        node.y = yStart + 0 * ySpace;
                        topoResult.nodes.Add(node);
                        curRow.Add(node);
                        preRow = curRow;
                    }
                    else
                    {
                        for (int n = 0; n < preRow.Count; n++) //每个节点下面挂m个子节点
                        {
                            for (int m = 0; m < m_mTree; m++)
                            {
                                node = new TopoNode();
                                node.id = topoResult.nodes.Count;
                                node.x = xStart + (n * m_mTree + m) * xSpace;
                                node.y = yStart + nr * ySpace;
                                topoResult.nodes.Add(node);
                                curRow.Add(node);

                                link = new TopoLink();
                                link.id = topoResult.links.Count;
                                link.srcNode = preRow[n].id;
                                link.desNode = node.id;
                                topoResult.links.Add(link);
                            }
                        }
                        preRow = curRow;
                    }
                }
            }
            else 
            {
                bool isBridgeTree = false;
                maxRows = m_nDefTree.Count;
                if (maxRows == 0)
                    return;
                ySpace = topoRange.size.Height / maxRows;
                yStart = topoRange.local.Y + ySpace / 2;//水平基准线：第一行node的Y
                preRow = null;
                // 1,3,(2,1,2)表示第0层为1个根节点，第1层有3个节点，第2层有2+1+2个节点，且分别接在第1层的节点下面
                for (int i = 0; i < m_nDefTree.Count;i++ )
                {
                    maxCols = 0; //该层有多少节点，用于计算space
                    if (m_nDefTree[i].GetType() == typeof(List<int>))
                        foreach (int j in (List<int>)m_nDefTree[i])
                            maxCols += j;
                    else
                        maxCols = (int)m_nDefTree[i];
                    if (i == 0 && maxCols > 1)
                        isBridgeTree = true;//桥形树，第0层有多个节点，这里要把它们水平连接起来
                    xSpace = topoRange.size.Width / maxCols;
                    xStart = topoRange.local.X + xSpace / 2;
                    curRow = new List<TopoNode>(maxCols);

                    List<int> childs;
                    if (m_nDefTree[i].GetType() == typeof(int)) //如果只是给定了这一行的节点个数，平分到上一层节点
                        childs = SplitTreeRow(maxCols, preRow == null ? maxCols : preRow.Count);
                    else
                        childs = (List<int>)m_nDefTree[i];
                    TopoNode parent = null;
                    nr = -1;
                    for (int j = 0; j < childs.Count; j++)
                    {
                        if (preRow != null)
                        {
                            if (j < preRow.Count)
                                parent = preRow[j];
                            else
                                parent = preRow[preRow.Count - 1];
                        }
                        for (int k = 0; k < childs[j]; k++)
                        {
                            nr++;
                            node = new TopoNode();
                            node.id = topoResult.nodes.Count;
                            node.x = xStart + nr * xSpace; //nr列
                            node.y = yStart + i * ySpace; //i行
                            topoResult.nodes.Add(node);
                            curRow.Add(node);

                            if (parent != null)
                            {
                                link = new TopoLink();
                                link.id = topoResult.links.Count;
                                link.srcNode = parent.id;
                                link.desNode = node.id;
                                topoResult.links.Add(link);
                            }
                        }
                    }
                    preRow = curRow;
                    
                    if (isBridgeTree)
                    {
                        isBridgeTree = false;
                        for (int j = 1; j < curRow.Count;j++ )
                        {
                            link = new TopoLink();
                            link.id = topoResult.links.Count;
                            link.srcNode = curRow[j-1].id;
                            link.desNode = curRow[j].id;
                            topoResult.links.Add(link);
                        }
                    }
                }
            }
        }
    }

    public class TopoCLOS
    {
        //静态参数
        public const string Info = "CLOS架构，目前只支持数据中心常见的两层架构";
        public static readonly Dictionary<string, string> Lines;

        static TopoCLOS()
        {
            Lines = new Dictionary<string, string>();
            Lines.Add("-n", "-n 2 汇入层n个交换机");
            Lines.Add("-m", "-m 3 接入层m个交换机");
            Lines.Add("-h", "-h 2 接入层交换机带的主机数");
            Lines.Add("-p", "-p node[r][n] pX=x,pY=(y1,y2) 参数");
        }
        /// <summary>
        /// 汇入层
        /// </summary>
        private int m_n = 2;
        /// <summary>
        /// 接入层
        /// </summary>
        private int m_m = 3;
        /// <summary>
        /// 接入层交换机带的主机数
        /// </summary>
        private int m_h = 0;
        private string m_pSets = "";

        private void InitParam(Dictionary<string, string> iparam)
        {
            foreach (var item in iparam)
            {
                switch (item.Key)
                {
                    case "-n":
                        m_n = int.Parse(item.Value);
                        if (m_n <= 0)
                            throw new Exception("-n\n");
                        break;
                    case "-m":
                        m_m = int.Parse(item.Value);
                        if (m_m <= 0)
                            throw new Exception("-m\n");
                        break;
                    case "-h":
                        m_h = int.Parse(item.Value);
                        break;
                    case "-p":
                        m_pSets = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public void DoLayout(TopoResult topoResult, Dictionary<string, string> iparam)
        {
            try
            {
                InitParam(iparam);
            }
            catch (Exception)
            {
                throw new Exception("参数错误\n");
            }

            TopoRange topoRange = new TopoRange();//topoResult.range;
            int maxRows = 0;
            List<TopoNode> preRow = null;
            List<TopoNode> curRow = null;
            TopoNode node = null;
            TopoLink link = null;

            if (m_h > 0)
                maxRows = 3;
            else
                maxRows = 2;
            List<float> posX;
            List<float> posY = TopoHelper.GetPosition(maxRows, topoRange.size.Height);
            for (int i = 0; i < maxRows; i++) //目前只支持常见的两层架构
            {
                switch (i)
                {
                    case 0: //汇入层
                        curRow = new List<TopoNode>();
                        posX = TopoHelper.GetPosition(m_n, topoRange.size.Width);
                        for (int j = 0; j < m_n; j++)
                        {
                            node = new TopoNode();
                            node.id = topoResult.nodes.Count;
                            node.x = topoRange.local.X + posX[j];
                            node.y = topoRange.local.Y + posY[0];
                            topoResult.nodes.Add(node);
                            curRow.Add(node);
                        }
                        break;
                    case 1: //接入层
                        posX = TopoHelper.GetPosition(m_m, topoRange.size.Width);
                        preRow = new List<TopoNode>(curRow);
                        curRow.Clear();
                        for (int j = 0; j < m_m; j++)
                        {
                            node = new TopoNode();
                            node.id = topoResult.nodes.Count;
                            node.x = topoRange.local.X + posX[j];
                            node.y = topoRange.local.Y + posY[1];
                            topoResult.nodes.Add(node);
                            curRow.Add(node);

                            foreach (TopoNode parent in preRow)
                            {
                                link = new TopoLink();
                                link.id = topoResult.links.Count;
                                link.srcNode = node.id;
                                link.desNode = parent.id;
                                topoResult.links.Add(link);
                            }
                        }
                        break;
                    case 2: //主机
                        preRow = new List<TopoNode>(curRow);
                        curRow.Clear();
                        posX = TopoHelper.GetPosition(m_h * preRow.Count, topoRange.size.Width);
                        for (int j = 0; j < preRow.Count; j++)
                        {
                            for (int k = 0; k < m_h; k++)
                            {
                                node = new TopoNode();
                                node.id = topoResult.nodes.Count;
                                node.x = topoRange.local.X + posX[j * m_h + k];
                                node.y = topoRange.local.Y + posY[2];
                                topoResult.nodes.Add(node);
                                //curRow.Add(node);

                                link = new TopoLink();
                                link.id = topoResult.links.Count;
                                link.srcNode = node.id;
                                link.desNode = preRow[j].id;
                                topoResult.links.Add(link);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
