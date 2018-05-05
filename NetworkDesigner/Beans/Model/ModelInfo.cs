using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using NetworkDesigner.Service.Model;
using NetworkDesigner.Utils.Common;
using Syncfusion.Windows.Forms.Diagram;

namespace NetworkDesigner.Beans.Model
{
    [Serializable]
    public class MyProperty
    {
        public string name = "";
        public string type = ""; //单值、序列、Model（由ModelInfo描述，其中的ModelType可看出内部组件）
        public string refModel = "";
        public object property = null; //默认值

        public string display = "";
        public string isDisplay = "y";
        public string disCategory = "";
        /// <summary>
        /// 该属性对应的检索路径，例如"某模型的IP子模型的参数1"表示为：IP.参数1，类似的有A.B.c
        /// </summary>
        public string path = "";
        /// <summary>
        /// 属性类别为model时可能会有的坐标
        /// </summary>
        public PointF pos = PointF.Empty;
        public MyProperty()
        {

        }
        public MyProperty(MyProperty other) //deepcopy时调用
        {
            this.name = other.name;
            this.type = other.type;
            this.refModel = other.refModel;
            this.property = other.property;
            this.display = other.display;
            this.isDisplay = other.isDisplay;
            this.disCategory = other.disCategory;
            this.pos = other.pos;
            this.path = other.path;
        }
        public MyProperty(string type, object property)
        {
            this.type = type;
            this.property = property;
        }
        /// <summary>
        /// 根据字符串和type类型设置属性值，暂不支持type为model时的设置
        /// </summary>
        /// <param name="value"></param>
        public void SetPropertyByStr(string value)
        {
            switch (type)
            {
                case GDataType.Double_En:
                    this.property = double.Parse(value);
                    break;
                case GDataType.Int_En:
                    this.property = int.Parse(value);
                    break;
                case GDataType.String_En:
                    this.property = value;
                    break;
                case GDataType.ListInt_En:
                    this.property = ConvertHelper.ListIntConverter(value);
                    break;
                case GDataType.ListDouble_En:
                    this.property = ConvertHelper.ListDoubleConverter(value);
                    break;
                case GDataType.ListString_En:
                    this.property = ConvertHelper.ListStringConverter(value);
                    break;
                //case GDataType.Model_En:
                //    this.property = ;
                //    break;
                default:
                    this.property = value;
                    break;
            }
        }
        /// <summary>
        /// 以字符串形式获取属性值，暂不支持type为model时的获取
        /// </summary>
        public string GetPropertyByStr()
        {
            string result = "";
            switch (type)
            {
                case GDataType.Double_En:
                case GDataType.Int_En:
                case GDataType.String_En:
                    result = property.ToString();
                    break;
                case GDataType.ListInt_En:
                    result = ConvertHelper.ListIntConverter((List<int>)property);
                    break;
                case GDataType.ListDouble_En:
                    result = ConvertHelper.ListDoubleConverter((List<double>)property);
                    break;
                case GDataType.ListString_En:
                    result = ConvertHelper.ListStringConverter((List<string>)property);
                    break;
                default:
                    throw new Exception("暂不支持获取model的str表示值");
            }
            return result;
        }
        public MyProperty DeepCopy()
        {
            MyProperty prop = new MyProperty(this);
            
            switch (type)
            {
                case GDataType.Double_En:
                    prop.property = (double)this.property;
                    break;
                case GDataType.Int_En:
                    prop.property = (int)this.property;
                    break;
                case GDataType.String_En:
                    prop.property = this.property;
                    break;
                case GDataType.ListInt_En:
                    prop.property = new List<int>((List<int>)this.property);
                    break;
                case GDataType.ListDouble_En:
                    prop.property = new List<double>((List<double>)this.property);
                    break;
                case GDataType.ListString_En:
                    prop.property = new List<string>((List<string>)this.property);
                    break;
                case GDataType.Model_En:
                    prop.property = ((ModelInfo)this.property).DeepCopy();
                    break;
                //case GDataType.ListModel_En:
                //    var models = new List<ModelInfo>();
                //    foreach (var mod in (List<ModelInfo>)this.property)
                //        models.Add(mod.DeepCopy());
                //    prop.property = models;
                //    break;
                //case "ModelCommon": //todo
                //    prop.property = new ModelCommon((ModelCommon)this.property);
                //    break;
                default:
                    prop.property = this.property;
                    break;
            }
            return prop;
        }

        public TypeConverter GetConverter()
        {
            //switch (this.type)
            //{
            //    case GDataType.ListDouble_En:
            //        break;
            //    case GDataType.ListInt_En:
            //        break;
            //    case GDataType.ListString_En:
            //        ;
            //        break;
            //    case GDataType.Model_En:
            //        return new ExpandableObjectConverter();
            //    default:
            //        break;
            //}
            return null;
        }
        public UITypeEditor GetEditor()
        {
            switch (this.type)
            {
                case GDataType.ListDouble_En:
                case GDataType.ListInt_En:
                case GDataType.ListString_En:
                    return new LinesTextConverter(this.type);
                default:
                    break;
            }
            return null;
        }

        public override string ToString()
        {
            return string.Format("Name:{0},DefType:{1},Value:{2}", name, type, property.ToString());
        }
    }

    [Serializable]
    public class MyLink
    {
        public string name="link";
        public string type = "duplex";
        public string src = "";
        public string dest = "";
        public Dictionary<string, string> attrs = new Dictionary<string, string>();

        public MyLink DeepCopy()
        {
            MyLink link = new MyLink();
            link.name = this.name;
            link.type = this.type;
            link.src = this.src;
            link.dest = this.dest;
            foreach (KeyValuePair<string, string> kv in attrs)
                link.attrs.Add(kv.Key, kv.Value);
            return link;
        }

        public void DeepCopy(MyLink other)
        {
            this.name = other.name;
            this.type = other.type;
            this.src = other.src;
            this.dest = other.dest;
            this.attrs.Clear();
            foreach (KeyValuePair<string, string> kv in other.attrs)
                this.attrs.Add(kv.Key, kv.Value);
        }

        /// <summary>
        /// 从xml元素的link参数解析得到Model内部model的链路信息
        /// </summary>
        public static List<MyLink> ParseXml(XmlNodeList linkNodes)
        {
            List<MyLink> links = new List<MyLink>();
            MyLink link;
            XmlNodeList atNodes;
            foreach (XmlNode node in linkNodes)
            {
                link = new MyLink();
                foreach (XmlAttribute attr in node.Attributes)
                {
                    switch (attr.Name)
                    {
                        case "name":
                            link.name = attr.Value;
                            break;
                        case "type":
                            link.type = attr.Value;
                            break;
                        case "src":
                            link.src = attr.Value;
                            break;
                        case "dest":
                            link.dest = attr.Value;
                            break;
                        default:
                            break;
                    }
                }
                atNodes = node.SelectNodes("Attr");
                foreach (XmlNode attr in atNodes)
                {
                    link.attrs[attr.Attributes["name"].Name] = attr.InnerText;
                }
                links.Add(link);
            }

            return links;
        }

        /// <summary>
        /// 将该link参数保为xml-modNode元素的子节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="modNode"></param>
        public void SaveXml(XmlDocument xmlDoc, XmlElement modNode)
        {
            XmlElement xe = xmlDoc.CreateElement("Link");
            modNode.AppendChild(xe);
            xe.SetAttribute("name", this.name);
            xe.SetAttribute("type", this.type);
            xe.SetAttribute("src", this.src);
            xe.SetAttribute("dest", this.dest);
            XmlElement sub;
            foreach (string key in this.attrs.Keys)
            {
                sub = xmlDoc.CreateElement("Attr");
                xe.AppendChild(sub);
                sub.SetAttribute("name", key);
                sub.InnerText = attrs[key];
            }
        }
    }

    [Serializable]
    public class ModelCommon
    {
        public string NodeName; //节点名称
        
        public ModelCommon(ModelCommon p)
        {
            NodeName = p.NodeName;
        }

    }

    public delegate void RefModelModifyDelegate();

    /// <summary>
    /// 模型信息
    /// </summary>
    [Serializable]
    public class ModelInfo
    {
        public override string ToString()
        {
            return "模型";
        }
        
        public ModelType modType;
        /// <summary>
        /// 模型名称，不是节点名称
        /// </summary>
        public string ModelName = "未知";
        /// <summary>
        /// 模型显示图标，不一定是节点图标
        /// </summary>
        public string ModelImage = "default.png";

        //用于属性浏览器显示，属性实质上是方法，是不会被序列化的
        public string 名称
        {
            get { return ModelName; }
            set { ModelName = value; }
        }
        [EditorAttribute(typeof(FileItemConverter),
            typeof(System.Drawing.Design.UITypeEditor))]
        public string 图标
        {
            get 
            {
                return System.IO.Path.GetFileName(ModelImage); 
            }
            set 
            {
                ModelImage = value;
                if (DiagramNode != null && DiagramNode is BitmapNode)
                {
                    try
                    {
                        ((BitmapNode)DiagramNode).Image = new Bitmap(ModelImage);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        [TypeConverter(typeof(ModelSimpleConverter))]
        public string 类别
        {
            get { return modType.Category; }
            set { modType.Category = value; }
        }

        [TypeConverter(typeof(ModelSimpleConverter))]
        public string 类型
        {
            get { return modType.TypeName; }
            set { modType.TypeName = value; }
        }

        public SizeF 范围
        {
            get 
            {
                return range; 
            }
            set 
            {
                if(value.Width > 0)
                    range = value; 
            }
        }

        public double 比例
        {
            get 
            {
                return scale; 
            }
            set 
            {
                if(value > 0)
                    scale = value; 
            }
        }
        /// <summary>
        /// 模型坐标
        /// </summary>
        public PointF pinpoint = PointF.Empty;
        /// <summary>
        /// 模型范围
        /// </summary>
        public SizeF range = SizeF.Empty;
        /// <summary>
        /// 单位调节因子
        /// </summary>
        public double scale = 1;

        public Dictionary<string, MyProperty> properties;

        public List<MyLink> links;

        //不会保存到xml、但可以随着diagram-node.tag复制而复制的参数

        public string ModelID = "";

        //不会保存到xml、且不会随着diagram-node.tag复制而复制的参数

        [NonSerialized]
        private Node diagramNode;

        [Browsable(false)]
        public Node DiagramNode
        {
            get { return diagramNode; }
            set 
            {
                if (value == null)
                {
                    System.Diagnostics.Debug.WriteLine("******设置DiagramNode为空");
                }
                diagramNode = value;
            }
        }

        /// <summary>
        /// 引用模型的属性被修改时触发，可用于更新那些已经放置在模型编辑器上的模型
        /// </summary>
        [NonSerialized]
        public RefModelModifyDelegate modifyHandler = null;

        public ModelInfo()
        {
            modType = new ModelType();
            properties = new Dictionary<string, MyProperty>();
            links = new List<MyLink>();
            //必须有的属性：置默认值
            
        }
        /// <summary>
        /// 根据检索路径设置MyProperty属性值，注意路径是到property一级，不是到子模型一级</para>
        /// 也即暂时不能使用字符串设置子模型的值，而可以设置子模型的某项属性值</para>
        /// 返回0设置成功，返回1找不到对应属性，返回-1值设置异常
        /// </summary>
        /// <param name="path"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int SetMyProperty(string path, string val)
        {
            MyProperty prop = GetMyProperty(path);
            if (prop == null || prop.type == GDataType.Model_En) //必须是到property一级
                return 1;
            try
            {
                prop.SetPropertyByStr(val);
            }
            catch (Exception)
            {
                return -1;
            }
            
            return 0;
        }
        /// <summary>
        /// <para>根据检索路径返回MyProperty，注意路径可以是到property一级，也可以是到子模型一级</para>
        /// 例如"本模型的IP子模型的参数1"表示为：IP.参数1，类似的有A.B.c
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public MyProperty GetMyProperty(string path)
        {
            string[] xp = path.Split('.');
            if(xp.Length < 1) //暂不支持处理ModelInfo的直接属性---
                return null;
            Dictionary<string, MyProperty> props = this.properties;
            int i=0;
            while (i < xp.Length -1) //a.b.c只有最后的.c是真正的attr，前面都只是索引到这个attr所属的model
            {
                if(props.ContainsKey(xp[i]))
                {
                    if (props[xp[i]].type != GDataType.Model_En)
                        return null;
                    else
                    {
                        props = ((ModelInfo)(props[xp[i]].property)).properties;
                        i++;
                    }
                }
                else
                    return null;
            }
            foreach (MyProperty mp in props.Values)
            {
                if(mp.name.Equals(xp[xp.Length-1]))
                    return mp;
            }
            return null;
        }

        /// <summary>
        /// 如果参数是默认值，则从def模型中提取相应参数，除了properties
        /// </summary>
        /// <param name="def"></param>
        public void UpdateDefaultInfo(ModelInfo def)
        {
            if (modType.IsDefaultType())
                this.modType.UpdateModelType(def.modType);
            if (this.ModelName.Equals("未知"))
                this.ModelName = def.ModelName;
            if (this.ModelImage.Equals("default.png"))
                this.ModelImage = def.ModelImage;
            if (this.ModelID == "")
                this.ModelID = def.ModelID;
            if (this.pinpoint.IsEmpty)
                this.pinpoint = def.pinpoint;
            if (this.range.IsEmpty)
                this.range = def.range;
            if (GDataType.IsDataDefNum(scale, 1))
                this.scale = def.scale;
            MyProperty prop = null;
            //foreach (string key in this.properties.Keys)
            //{
            //    if (def.properties.ContainsKey(key) == false)
            //    {
            //        //如果基本模型不包含这些属性？ ===允许给子模型新增加属性
            //    }
            //}
            foreach (KeyValuePair<string, MyProperty> item in def.properties)
            {
                if (this.properties.ContainsKey(item.Key) == false)
                {
                    prop = item.Value.DeepCopy();
                    prop.isDisplay = "n";
                    this.properties[item.Key] = prop;
                }
            }
        }

        /// <summary>
        /// ？？其他属性呢 引用模型已被修改，更新对应属性项：修改名称，或者删除已不存在的属性，谨慎删除
        /// </summary>
        /// <param name="subm"></param>
        public void UpdateRefModelProperty(string modelID, Dictionary<string,string> changed)
        {
            ModelInfo subm = null;
            string newName = "";
            foreach (MyProperty m in properties.Values)
            {
                if (m.type.Equals(GDataType.Model_En) && m.refModel.Equals(modelID))
                {
                    subm = (ModelInfo)m.property;
                    foreach (string sk in subm.properties.Keys)
                    {
                        if (changed.ContainsKey(sk))
                        {
                            newName = changed[sk];
                            if (newName.Equals("!deleted!"))
                                subm.properties.Remove(sk);
                            else
                            {
                                subm.properties[sk].name = newName;
                                
                                string oldPath = subm.properties[sk].path;
                                if(!oldPath.Contains('.'))
                                    subm.properties[sk].path = newName;
                                else
                                {
                                    oldPath = oldPath.Substring(0,oldPath.LastIndexOf('.'));
                                    subm.properties[sk].path = subm.properties[sk].path + "." + newName;
                                }
                            }
                        }
                    }
                }
            }
        }

        public ModelInfo DeepCopy()
        {
            ModelInfo mod = new ModelInfo();
            mod.modType = new ModelType(this.modType);
            mod.ModelImage = this.ModelImage;
            mod.ModelID = this.ModelID;
            mod.ModelName = this.ModelName;
            mod.pinpoint = this.pinpoint;
            mod.range = this.range;
            mod.scale = this.scale;
            foreach (KeyValuePair<string, MyProperty> kv in properties)
                mod.properties.Add(kv.Key, kv.Value.DeepCopy());
            foreach (var link in this.links)
                mod.links.Add(link.DeepCopy());
            mod.UpdateDisplayProps();
            return mod;
        }

        public void DeepCopy(ModelInfo other)
        {
            this.modType.UpdateModelType(other.modType);
            this.ModelName = other.ModelName;
            this.ModelImage = other.ModelImage;
            this.ModelID = other.ModelID;
            this.pinpoint = other.pinpoint;
            this.range = other.range;
            this.scale = other.scale;
            this.properties.Clear();
            foreach (KeyValuePair<string, MyProperty> kv in other.properties)
                this.properties.Add(kv.Key, kv.Value.DeepCopy());
            this.links.Clear();
            foreach (var link in other.links)
                this.links.Add(link.DeepCopy());
            this.UpdateDisplayProps();
        }
        /// <summary>
        /// 将Model信息保存到xml-node节点
        /// </summary>
        public void SaveXml(XmlDocument xmlDoc)
        {
            XmlElement root = xmlDoc.DocumentElement;
            XmlElement modNode = xmlDoc.CreateElement("Model");
            root.AppendChild(modNode);

            //保存model行的xml-attribute
            modNode.SetAttribute("name", this.ModelName);
            modNode.SetAttribute("type", this.modType.FormatString());
            modNode.SetAttribute("image", this.图标);
            modNode.SetAttribute("name", this.ModelName);
            if (!this.pinpoint.IsEmpty)
                modNode.SetAttribute("pinpoint", this.pinpoint.X + "," + pinpoint.Y);
            if (!this.range.IsEmpty)
                modNode.SetAttribute("range", this.range.Width + "," + range.Height);
            modNode.SetAttribute("scale", this.scale.ToString());

            //保存model内部的property
            foreach (MyProperty prop in this.properties.Values)
            {
                SaveModelProperties(prop, xmlDoc, modNode);
            }

            //保存model内部可能有的links---只保存本身有的link
            foreach (MyLink link in this.links)
            {
                link.SaveXml(xmlDoc, modNode);
            }
        }
        /// <summary>
        /// 从xml的model元素中解析得到Model
        /// </summary>
        public static ModelInfo ParseXml(XmlNode xn)
        {
            string[] strs;
            ModelInfo model = new ModelInfo();
            foreach (XmlAttribute attr in xn.Attributes)
            {
                switch (attr.Name)
                {
                    case "name":
                        model.ModelName = attr.Value;
                        break;
                    case "type":
                        model.modType = new ModelType(attr.Value);
                        break;
                    case "image":
                        model.ModelImage = attr.Value;
                        break;
                    case "pinpoint":
                        strs = attr.Value.Split(',');
                        model.pinpoint = new PointF(float.Parse(strs[0]),float.Parse(strs[1]));
                        break;
                    case "range":
                        strs = attr.Value.Split(',');
                        model.range = new SizeF(float.Parse(strs[0]),float.Parse(strs[1]));
                        break;
                    case "scale":
                        model.scale = double.Parse(attr.Value);
                        break;
                    default:
                        LogHelper.LogInfo("未知属性" + attr.Name);
                        break;
                }
            }
            XmlNodeList props = xn.SelectNodes("Attr");
            ParseModelProperties(model, props);
            XmlNodeList links = xn.SelectNodes("Link"); //只解析一级属性的model的link信息，子模型的内部Link不管
            if (links.Count > 0)
                model.links = MyLink.ParseXml(links);
            model.UpdateDisplayProps();
            return model;
        }
        /// <summary>
        /// 将单个MyProperty属性保存为xml-modNode的子节点，如果type是model也会负责保存其内部属性
        /// </summary>
        private void SaveModelProperties(MyProperty prop, XmlDocument xmlDoc, XmlElement modNode)
        {
            XmlElement xe = xmlDoc.CreateElement("Attr");
            modNode.AppendChild(xe);
            
            //写入基本属性项---对应xml中的Attr行
            WritePropertyAttr(prop, xe);
            
            //写入innertext取值---对应xml中的Value行，或者当type为model时的嵌套Attr行等
            XmlElement elment;
            switch (prop.type)
            {
                case GDataType.Double_En:
                case GDataType.Int_En:
                case GDataType.String_En:
                    elment = xmlDoc.CreateElement("Value");
                    elment.InnerText = prop.property.ToString();
                    xe.AppendChild(elment);
                    break;
                case GDataType.Model_En:
                    ModelInfo subModel = (ModelInfo)prop.property; //子模型只保存其property-dictionary
                    //保存model内部的property
                    foreach (MyProperty subp in subModel.properties.Values)
                    {
                        SaveModelProperties(subp, xmlDoc, xe);
                    }
                    break;
                case GDataType.ListInt_En:
                    foreach (int data in (List<int>)prop.property)
                    {
                        elment = xmlDoc.CreateElement("Value");
                        elment.InnerText = data.ToString();
                        xe.AppendChild(elment);
                    }
                    break;
                case GDataType.ListDouble_En:
                    foreach (double data in (List<double>)prop.property)
                    {
                        elment = xmlDoc.CreateElement("Value");
                        elment.InnerText = data.ToString();
                        xe.AppendChild(elment);
                    }
                    break;
                case GDataType.ListString_En:
                    foreach (string data in (List<string>)prop.property)
                    {
                        elment = xmlDoc.CreateElement("Value");
                        elment.InnerText = data;
                        xe.AppendChild(elment);
                    }
                    break;
                default:
                    throw new Exception(string.Format("无法保存属性到XML，名称：{0} 类型：{1}" + prop.name, prop.type));
            }
        }

        private void WritePropertyAttr(MyProperty prop, XmlElement xe)
        {
            xe.SetAttribute("name",prop.name);
            xe.SetAttribute("type", prop.type);
            if (prop.refModel.Length != 0)
                xe.SetAttribute("ref", prop.refModel);
            if (prop.display.Length != 0)
                xe.SetAttribute("display", prop.display);
            if (prop.disCategory.Length != 0)
                xe.SetAttribute("disCategory", prop.disCategory);
            if(!prop.isDisplay.Equals("y"))
                xe.SetAttribute("isDisplay", prop.isDisplay);
            if (!prop.pos.IsEmpty)
                xe.SetAttribute("pos", prop.pos.X + "," + prop.pos.Y);
        }
        /// <summary>
        /// 从xml节点中解析得到模型的单个MyProperty，如果type是model也会负责加载其内部属性
        /// </summary>
        private static void ParseModelProperties(ModelInfo model, XmlNodeList props, string modelPath="")
        {
            MyProperty mp = null;
            List<string> strs = null;
            List<int> ints = null;
            List<double> doubs = null;
            XmlNodeList values = null;
            XmlNodeList valProps = null;
            foreach (XmlNode pn in props)
            {
                mp = new MyProperty();
                ReadPropertyAttr(pn.Attributes, mp); //读Attr这一行本身的属性
                if (mp.display.Length == 0)
                    mp.display = mp.name;
                if (modelPath.Length == 0)
                    mp.path = mp.name;
                else
                    mp.path = modelPath + "." + mp.name;
                switch (mp.type)
                {
                    case GDataType.Double_En:
                        values = pn.SelectNodes("Value");
                        if (values.Count > 0)
                            mp.property = double.Parse(values[0].InnerText);
                        break;
                    case GDataType.Int_En:
                        values = pn.SelectNodes("Value");
                        if (values.Count > 0)
                            mp.property = int.Parse(values[0].InnerText);
                        break;
                    case GDataType.String_En:
                        values = pn.SelectNodes("Value");
                        if (values.Count > 0)
                            mp.property = values[0].InnerText;
                        break;
                    case GDataType.Model_En:
                        ModelInfo subModel = new ModelInfo();
                        valProps = pn.SelectNodes("Attr");
                        ParseModelProperties(subModel, valProps, mp.path);//递归加载下面行的属性
                        mp.property = subModel;//无论些属性对应的模型多复杂，对外来说始终只是个property
                        break;
                    case GDataType.ListInt_En:
                        values = pn.SelectNodes("Value");
                        ints = new List<int>(values.Count);
                        foreach (XmlNode node in values)
                            doubs.Add(int.Parse(node.InnerText));
                        mp.property = ints;
                        break;
                    case GDataType.ListDouble_En:
                        values = pn.SelectNodes("Value");
                        doubs = new List<double>(values.Count);
                        foreach (XmlNode node in values)
                            doubs.Add(double.Parse(node.InnerText));
                        mp.property = doubs;
                        break;
                    case GDataType.ListString_En:
                        values = pn.SelectNodes("Value");
                        strs = new List<string>(values.Count);
                        foreach (XmlNode node in values)
                            strs.Add(node.InnerText);
                        mp.property = strs;
                        break;
                    //case GDataType.ListModel_En:
                        //var ms = new List<ModelInfo>();
                        //foreach (XmlNode node in values)
                        //ms.Add(ModelInfo.ParseXml(node));
                        //break;
                    default:
                        throw new Exception(string.Format("无法解析属性，名称：{0} 类型：{1}" + mp.name, mp.type));
                }
                model.properties[mp.name] = mp;
            }
        }
        /// <summary>
        /// 读MyProperty的基本属性，对应xml中的Attr行本身
        /// </summary>
        private static void ReadPropertyAttr(XmlAttributeCollection attrs, MyProperty mp)
        {
            string[] strs;
            foreach (XmlAttribute attr in attrs)
            {
                switch (attr.Name)
                {
                    case "name":
                        mp.name = attr.Value;
                        break;
                    case "type":
                        if (!GDataType.dt_En.Contains(attr.Value))
                            throw new Exception("Property属性类型无法识别：" + attr.Value);
                        mp.type = attr.Value;
                        break;
                    case "ref":
                        mp.refModel = attr.Value;
                        break;
                    case "pos":
                        strs = attr.Value.Split(',');
                        mp.pos = new PointF(float.Parse(strs[0]), float.Parse(strs[1]));
                        break;
                    case "display":
                        mp.display = attr.Value;
                        break;
                    case "isDisplay":
                        if (attr.Value.ToLower().Equals("y"))
                            mp.isDisplay = "y";
                        else
                            mp.isDisplay = "n";
                        break;
                    case "disCategory":
                        mp.disCategory = attr.Value;
                        break;
                    default:
                        LogHelper.LogInfo("未知属性" + attr.Name);
                        break;
                }
            }
        }

        /// <summary>
        /// 参数项修改后必须调用更新
        /// </summary>
        public void UpdateDisplayProps()
        {
            _displayProps = new List<MyProperty>();
            GetModelDisplayProp(this, _displayProps);
        }
        private void GetModelDisplayProp(ModelInfo model, List<MyProperty> result)
        {
            foreach(MyProperty prop in model.properties.Values)
            {
                if (prop.type != GDataType.Model_En && prop.isDisplay=="y")
                    result.Add(prop);
                else
                    GetModelDisplayProp((ModelInfo)prop.property, result);
            }
        }
        /// <summary>
        /// 该模型要显示的属性，都提升到一级，由其path唯一标识
        /// </summary>
        [NonSerialized]
        private List<MyProperty> _displayProps;
        public List<MyProperty> DisplayProps 
        {
            get { return _displayProps; }
        }
    }
    
    [Serializable]
    public class ModelType
    {
        public const string DefaultCategory = "实体";
        public const string DefaultTypeName = "主机";

        public string Category = DefaultCategory;
        public string TypeName = DefaultTypeName;
        //用于属性浏览器显示，属性实质上是方法，是不会被序列化的
        public string 类别
        {
            get { return Category; }
            set { Category = value; }
        }
        public string 类型
        {
            get { return TypeName; }
            set { TypeName = value; }
        }

        public override string ToString()
        {
            //return string.Format("{0}-{1}",Category,TypeName);
            return "";
        }

        public string FormatString()
        {
            return 类别 + "." + 类型;
        }

        //public static string[] 组件 = new string[] { "基本", "抽象"};
        //public static string[] 实体 = new string[] { "节点", "链路", "组合体" };
        //public static string[] 协议 = new string[] { "应用层", "表示层", "会话层","传输层","网络层","链路层","物理层"};
        //public static string[] 服务 = new string[] { "拓扑", "业务", "仿真结果","语义模型"};
        //暂时只实现两层划分

        public static Dictionary<string, List<string>> AllTypes;
        static ModelType()
        {
            AllTypes = new Dictionary<string, List<string>>();
            //这部分按道理应该从配置文件中加载，并且修改后能保存
            AllTypes.Add("组件", new List<string>() { "接口", "天线", "传感器", "CPU", "存储", "信道", "轨迹" });
            AllTypes.Add("实体", new List<string>() { "主机", "路由器", "交换机", "有线链路", "无线点对点链路", "无线广播链路", "卫星","基站" });
            AllTypes.Add("协议", new List<string>() { "应用层", "传输层", "网络层", "链路层", "物理层" });
            AllTypes.Add("服务", new List<string>() { "拓扑生成", "业务生成", "结果统计", "轨迹生成", "抽象功能"});
        }

        public ModelType()
        {
            
        }

        public ModelType(ModelType t)
        {
            this.UpdateModelType(t);
        }
        public void UpdateModelType(ModelType t)
        {
            this.Category = t.Category;
            this.TypeName = t.TypeName; //待改
        }

        public ModelType(string t)
        {
            string[] strs = t.Split('.');
            if (strs.Length > 1)
            {
                Category = strs[0].Trim();
                TypeName = strs[1].Trim();
            }
            else if(strs.Length > 0)
            {
                TypeName = strs[0].Trim();
            }
            
        }
        /// <summary>
        /// 是否是默认的类型，也即没有有效信息的类型
        /// </summary>
        /// <returns></returns>
        public bool IsDefaultType()
        {
            //if (Category.Equals(DefaultCategory) && TypeName.Equals(DefaultTypeName))
            if (Category.Equals("") && TypeName.Equals(""))
                return true;
            return false;
        }
    }
}
