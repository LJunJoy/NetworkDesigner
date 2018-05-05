using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using NetworkDesigner.Utils.Common;
using NetworkDesigner.Service.Model;

namespace NetworkDesigner.Beans.Model
{
    /// <summary>
    /// 模型基类，主要是定义虚函数的索引器
    /// </summary>
    [Serializable]
    public class SimModel
    {
        #region 方便使用，不保存到文件的变量
        /// <summary>
        /// 模型类别.模型名称
        /// </summary>
        public string modelID;
        /// <summary>
        /// 节点编号
        /// </summary>
        public string ID;
        /// <summary>
        /// 模型名称，注意不是其实例化后的节点名称，后者用["名称"]访问
        /// </summary>
        public string modelName
        {
            get { return modelInfo["模型名称"]; }
        }
        public string modelCate
        {
            get { return modelInfo["模型类别"]; }
        }
        public string modelImage
        {
            get { return modelInfo["模型图片"]; }
        }
        public string intf2LinkName="";
        
        #endregion
        public Dictionary<string, string> modelInfo;
        /// <summary>
        /// 该模型可能包含的属性项
        /// </summary>
        public Dictionary<string,SimAttr> attrs;

        /// <summary>
        /// 该模型附属的临时属性项
        /// </summary>
        public Dictionary<string, object> attrsTemp;

        public SimModel()
        {
            modelInfo = new Dictionary<string, string>();
            modelInfo["模型名称"] = "未知";
            modelInfo["模型类别"] = "未知";
            modelInfo["模型图片"] = "default.png";

            attrs = new Dictionary<string,SimAttr>();
            attrsTemp = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <returns></returns>
        public SimAttr GetAttrIntfs()
        {
            foreach (SimAttr attr in this.attrs.Values)
            {
                if (attr.type == AttrType.接口列表)
                    return attr;
            }
            return null;
        }	

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="ix"></param>
        /// <returns></returns>
        public object this[string ix]
        {
            get
            {
                //if (ix.StartsWith("接口") || ix.StartsWith("intf"))
                //{
                //    SimAttr attr = this.GetAttrIntfs();
                //    if (attr != null)
                //    {
                //        return attr.data;
                //    }
                //    return null;
                //}
                if (attrs.ContainsKey(ix))
                    return attrs[ix].data;
                switch(ix)
                {
                    case "ID":
                        return this.ID;
                    case "名称": //名称，只准用这两种名字
                        if(attrs.ContainsKey("name"))
                            return attrs["name"].data;
                        break;
                    case "name":
                        if (attrs.ContainsKey("名称"))
                            return attrs["名称"].data;
                        break;
                    case "编号": //接口编号，只准用这两种名字
                        if (attrs.ContainsKey("IF"))
                            return attrs["IF"].data;
                        break;
                    case "IF":
                        if (attrs.ContainsKey("编号"))
                            return attrs["编号"].data;
                        break;
                    case "ip": //IP，只准用这两种名字
                        if (attrs.ContainsKey("IP"))
                            return attrs["IP"].data;
                        break;
                    case "IP":
                        if (attrs.ContainsKey("ip"))
                            return attrs["ip"].data;
                        break;
                    default:
                        break;
                }
                if(attrsTemp.ContainsKey(ix))
                    return attrsTemp[ix];
                return null;
            }
            set
            {
                //if (ModelInfoHelper.NotSetByIndexer.Contains(ix))
                //    return;
                SimAttr attr = null;
                if (attrs.ContainsKey(ix))
                    attr = attrs[ix];
                switch (ix)
                {
                    case "ID":
                        this.ID = value as string;
                        return;
                    case "名称": //名称，只准用这两种名字
                        if (attrs.ContainsKey("name"))
                            attr = attrs["name"];
                        break;
                    case "name":
                        if (attrs.ContainsKey("名称"))
                            attr = attrs["名称"];
                        break;
                    case "编号": //接口编号，只准用这两种名字
                        if (attrs.ContainsKey("IF"))
                            attr = attrs["IF"];
                        break;
                    case "IF":
                        if (attrs.ContainsKey("编号"))
                            attr = attrs["编号"];
                        break;
                    case "ip": //IP，只准用这两种名字
                        if (attrs.ContainsKey("IP"))
                            attr = attrs["IP"];
                        break;
                    case "IP":
                        if (attrs.ContainsKey("ip"))
                            attr = attrs["ip"];
                        break;
                    default:
                        break;
                }
                if (attr != null)
                {
                    switch (attr.type)
                    {
                        case AttrType.字符串:
                        case AttrType.选择器:
                            attr.data = value;
                            break;
                        default: //其他类型不支持用这种方式赋值
                            break;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("未找到属性项：" + ix + "，添加到临时属性项");
                    attrsTemp[ix] = value;
                }
            }
        }
        public override string ToString()
        {
            return this.modelID;
        }

        /// <summary>
        /// 从xml的model元素中解析得到Model
        /// </summary>
        public static SimModel ParseXml(XmlNode xn)
        {
            string[] strs;
            SimModel model = new SimModel();
            foreach (XmlAttribute attr in xn.Attributes)
            {
                model.modelInfo[attr.Name]=attr.Value;
            }
            XmlNodeList props = xn.SelectNodes("Attr");
            ParseModelAttrs(model, props);
            //XmlNodeList links = xn.SelectNodes("Link"); //只解析一级属性的model的link信息，子模型的内部Link不管
            //if (links.Count > 0)
            //    model.links = MyLink.ParseXml(links);
            //model.UpdateDisplayProps();
            return model;
        }

        /// <summary>
        /// 从xml节点中解析得到模型的单个SimAttr，如果type是model则只保存ref引用
        /// </summary>
        private static void ParseModelAttrs(SimModel model, XmlNodeList props)//, string modelPath = "")
        {
            SimAttr mp = null;
            XmlNodeList values = null;
            XmlNodeList valProps = null;
            foreach (XmlNode pn in props)
            {
                mp = new SimAttr();
                mp.ParseXml(pn);
                //if (modelPath.Length == 0)
                //    mp.path = mp.name;
                //else
                //    mp.path = modelPath + "." + mp.name;
                model.attrs[mp.name] = mp;
            }
        }

        public void SaveXml(XmlDocument xmlDoc)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 深拷贝值而不是结构，各个attr都要深拷贝；模型结构本身永远不需要拷贝，只需从xml中读取
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public SimModel DeepCopy(SimModel other)
        {
            SimModel model = new SimModel();
            model.modelID = other.modelID;
            foreach (string key in other.modelInfo.Keys)
                model.modelInfo[key] = other.modelInfo[key];
            foreach (string key in other.attrs.Keys)
                model.attrs[key] = other.attrs[key].DeepCopy();
            return model;
        }

        public SimModel DeepCopy()
        {
            return DeepCopy(this);
        }
        /// <summary>
        /// 根据模型结构创建可用于保存模型值的对象，主要是将引用模型用数据结构填充
        /// </summary>
        /// <returns></returns>
        public static SimModel CreateValModel(string modelID)
        {
            SimModel bm = ModelInfoHelper.allModels[modelID];
            SimModel model = new SimModel();
            model.modelID = bm.modelID;
            foreach (string key in bm.modelInfo.Keys)
                model.modelInfo[key] = bm.modelInfo[key];
            foreach (string key in bm.attrs.Keys)
            {
                SimAttr attr = bm.attrs[key];
                model.attrs[key] = attr.CreateValModel(bm);
            }
            return model;
        }
    }
    [Serializable]
    public class SimAttr
    {
        #region 标记，只用于特定场合
        public int IndentLevel;
        public int ExpandState = 0;
        #endregion

        public string name;
        public object data="";
        public object defData="";
        public string converter = ""; 
        public AttrType type = AttrType.字符串;
        public string refModel = "";
        public StringCollection options = null;
        /// <summary>
        /// 类型为模型且引用的模型已加载时为true，否则为false
        /// </summary>
        public bool isInitRefModel = true;
        /// <summary>
        /// 该属性对应的检索路径
        /// </summary>
        //public string path = "";
        public override string ToString()
        {
 	         return data.ToString();
        }
        public SimAttr()
        {

        }
        public SimAttr(AttrType type, object data)
        {
            this.type = type;
            this.data = data;
        }
        public void ParseXml(XmlNode xn)
        {
            XmlNodeList props = null;
            foreach (XmlAttribute attr in xn.Attributes)
            {
                switch (attr.Name)
                {
                    case "name":
                        this.name = attr.Value;
                        break;
                    case "type":
                        try 
	                    {
                            this.type = (AttrType)Enum.Parse(typeof(AttrType), attr.Value);
                            if (type == AttrType.模型 || type == AttrType.模型列表 || type== AttrType.接口列表)
                            {
                                this.isInitRefModel = false;
                            }
                            else if (type == AttrType.选择器)
                            {
                                options = new StringCollection();
                                props = xn.SelectNodes("option");
                                foreach (XmlNode opt in props)
                                {
                                    options.Add(opt.InnerText);
                                }
                            }
	                    }
	                    catch (Exception)
	                    {
                            LogHelper.LogInfo("无效的属性类型：" + attr.Name + " " + attr.Value);
                            this.type = AttrType.未知;
		                    throw;
	                    }
                        break;
                    case "def":
                        this.defData = attr.Value;
                        this.data = this.defData;
                        break;
                    case "ref":
                        this.refModel = attr.Value;
                        break;
                    case "converter":
                        this.converter = attr.Value;
                        break;
                    //case "validator":
                    //    this.validator = attr.Value;
                        //break;
                    default:
                        break;
                }
            }
        }

        public SimAttr DeepCopy()
        {
            SimAttr sa = new SimAttr();
            sa.name = this.name;
            sa.data = this.data;
            sa.defData = this.defData;
            sa.converter = this.converter;
            sa.type = this.type;
            sa.refModel = this.refModel;
            if (this.options != null)
            {
                sa.options = new StringCollection();
                foreach(var str in this.options)
                    sa.options.Add(str);
            }

            switch (this.type)
            {
                case AttrType.模型:
                    SimModel tm = this.data as SimModel;
                    if (tm != null)
                        sa.data = tm.DeepCopy(); //警告，这里可能引起死循环，如果模型循环引用的话
                    break;
                case AttrType.模型列表:
                case AttrType.接口列表:
                    List<SimModel> tlm = this.data as List<SimModel>;
                    if (tlm!=null)
                    {
                        List<SimModel> nlm = new List<SimModel>();
                        foreach (var m in tlm)
                            nlm.Add(m.DeepCopy());
                        sa.data = nlm;
                    }
                    break;
                default:
                    break;
            }
            return sa;
        }
        /// <summary>
        /// 根据属性结构创建可用于保存属性值的对象，主要是构造数据结构
        /// </summary>
        public SimAttr CreateValModel(SimModel owner)
        {
            SimAttr nsa = new SimAttr();
            nsa.name = this.name;
            nsa.data = this.data;
            nsa.defData = this.defData;
            nsa.converter = this.converter;
            nsa.type = this.type;
            nsa.refModel = this.refModel;
            if (this.options != null)
            {
                nsa.options = new StringCollection();
                foreach (var str in this.options)
                    nsa.options.Add(str);
            }
            switch (this.type)
            {
                case AttrType.模型:
                    if (this.refModel.Length == 0)
                    {
                        nsa.data = new SimModel();
                    }
                    else if (ModelInfoHelper.allModels.ContainsKey(this.refModel))
                    {
                        nsa.data = SimModel.CreateValModel(this.refModel);
                    }
                    else
                    {
                        LogHelper.LogInfo(owner.modelID + " 属性创建异常：" + this.name
                            + "，该属性找不到引用模型" + this.refModel);
                        nsa.type = AttrType.未知;
                    }
                    break;
                case AttrType.模型列表:
                case AttrType.接口列表:
                    if (ModelInfoHelper.allModels.ContainsKey(this.refModel))
                    {
                        nsa.data = new List<SimModel>();
                    }
                    else
                    {
                        LogHelper.LogInfo(owner.modelID + " 属性创建异常：" + this.name
                            + "，该属性找不到引用模型" + this.refModel);
                        nsa.type = AttrType.未知;
                    }
                    break;
                default:
                    break;
            }
            return nsa;
        }
    }
    //[Serializable]
    //public class SimPos
    //{
    //    int type = 0;
    //    double x;
    //    double y;
    //    double z;
    //    double t;
    //    public string strVal = "";
    //    public SimPos()
    //    {

    //    }
    //    public SimPos DeepCopy()
    //    {
    //        SimPos p = new SimPos();
    //        p.type = this.type;
    //        p.x = this.x;
    //        p.y = this.y;
    //        p.z = this.z;
    //        p.t = this.t;
    //        return p;
    //    }

    //}
    public enum AttrType
    {
        字符串, 选择器, 模型, 模型列表, 接口列表,
        未知
    }
}
