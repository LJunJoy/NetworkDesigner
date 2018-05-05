using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using NetworkDesigner.Utils.Common;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace NetworkDesigner.Utils.FileUtil
{
    /// <summary>
    /// 警告：凡是xml文件名用的path、检索路径名用的node的函数都是拷贝后没有修改的，可能有问题！
    /// 注意：XmlDocument不用释放，生命周期过后，自动销毁。
    /// xml的操作速度会比数据库快，因为程序会把整个xml先载入内容，然后在内存中操作，所以小容量的
    /// 数据库可以使用xml代替，但是如果xml文件太大会占用大量内存，此时建议改用数据库来操作。
    /// </summary>
    public static class XmlHelper
    {
        #region Fields and Properties

        public enum XmlType
        {
            File,
            String
        }

        #endregion

        #region  Methods

        /// <summary>
        ///     创建XML文档
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="type">根节点的一个属性值</param>
        /// <returns></returns>
        public static XmlDocument CreateXmlDocument(string name, string type)
        {
            /**************************************************
            * .net中调用方法：写入文件中,则：
            *document = XmlOperate.CreateXmlDocument("sex", "sexy");
            *document.Save("c:/bookstore.xml");
            ************************************************/
            XmlDocument xmlDocument;
            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml("<" + name + "/>");
                var rootElement = xmlDocument.DocumentElement;
                rootElement.SetAttribute("type", type);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
                return null;
            }
            return xmlDocument;
        }

        /// <summary>
        ///     获得xml文件中指定节点的节点数据
        /// </summary>
        /// <returns></returns>
        public static string GetNodeInfoByNodeName(string path, string nodeName)
        {
            try
            {
                var xmlString = "";
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var documentElementRoot = xmlDocument.DocumentElement;
                var selectSingleNode = documentElementRoot.SelectSingleNode("//" + nodeName);
                if (selectSingleNode != null)
                    xmlString = selectSingleNode.InnerText;
                return xmlString;
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
                return null;
            }
        }


        /// <summary>
        ///     读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static string GetNodeValue(string source, XmlType xmlType, string nodeName)
        {
            var xmlDocument = new XmlDocument();
            if (xmlType == XmlType.File)
                xmlDocument.Load(source);
            else
                xmlDocument.LoadXml(source);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = documentElement.SelectSingleNode("//" + nodeName);
            return selectSingleNode.InnerText;
        }


        /// <summary>
        ///     读取XML资源中的指定节点属性的内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">属性节点名称</param>
        /// <param name="attributeString"></param>
        /// <returns>节点内容</returns>
        public static string GetNodeAttributeValue(string source, XmlType xmlType, string nodeName,
            string attributeString)
        {
            var xmlDocument = new XmlDocument();
            if (xmlType == XmlType.File)
                xmlDocument.Load(source);
            else
                xmlDocument.LoadXml(source);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = (XmlElement)documentElement.SelectSingleNode("//" + nodeName);
            //if (selectSingleNode != null)
            return selectSingleNode.GetAttribute(attributeString);
        }

        /// <summary>
        ///     读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static string GetNodeValue(string source, string nodeName)
        {
            if (source == null || nodeName == null || source == "" || nodeName == "" ||
                source.Length < nodeName.Length * 2)
                return null;
            var start = source.IndexOf("<" + nodeName + ">", StringComparison.Ordinal) + nodeName.Length + 2;
            var end = source.IndexOf("</" + nodeName + ">", StringComparison.Ordinal);
            if (start == -1 || end == -1)
                return null;
            return start >= end ? null : source.Substring(start, end - start);
        }

        public class ElementData
        {
            public string Name;
            public string Value;
            /// <summary>
            /// 属性字典可以为空，表示元素没有属性
            /// </summary>
            public Dictionary<string, string> Attr;
            /// <summary>
            /// 参数分别为：元素名、元素文本值、属性列表，其中属性列表可以为null
            /// </summary>
            /// <param name="name"></param>
            /// <param name="value"></param>
            /// <param name="attr"></param>
            public ElementData(string name, string value, Dictionary<string, string> attr)
            {
                this.Name = name;
                this.Value = value;
                this.Attr = attr;
            }
        }
        /// <summary>
        /// 在指定路径的子节点中插入元素
        /// </summary>
        public static void InsertChildElement(string filePath, string xpath, ElementData ed)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                var selectSingleNode = xmlDocument.SelectSingleNode(xpath);
                var e = xmlDocument.CreateElement(ed.Name);
                selectSingleNode.AppendChild(e);
                e.InnerText = ed.Value;
                if (ed.Attr != null)
                {
                    foreach (var kv in ed.Attr)
                    {
                        XmlAttribute ar = xmlDocument.CreateAttribute(kv.Key);
                        ar.Value = kv.Value;
                    }
                }

                xmlDocument.Save(filePath);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
        }
        /// <summary>
        /// 在已经构造好的xmlDocument流的指定路径的子节点中插入元素<br/>
        /// 属性字典可以为空
        /// </summary>
        public static void InsertChildElement(XmlDocument xmlDoc, string xpath, ElementData ed)
        {
            try
            {
                var selectSingleNode = xmlDoc.SelectSingleNode(xpath);
                var e = xmlDoc.CreateElement(ed.Name);
                selectSingleNode.AppendChild(e);
                e.InnerText = ed.Value;
                if (ed.Attr != null)
                {
                    foreach (var kv in ed.Attr)
                    {
                        XmlAttribute ar = xmlDoc.CreateAttribute(kv.Key);
                        ar.Value = kv.Value;
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
        }
        /// <summary>
        /// 在指定路径的子节点中插入批量元素
        /// </summary>
        public static void InsertChildElementList(string filePath, string xpath, List<ElementData> eds)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                InsertChildElementList(xmlDocument, xpath, eds);
                xmlDocument.Save(filePath);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }

        }
        /// <summary>
        /// 在已经构造好的xmlDocument流的指定路径的子节点中插入批量元素
        /// </summary>
        public static void InsertChildElementList(XmlDocument xmlDoc, string xpath, List<ElementData> eds)
        {
            foreach (var ed in eds)
                InsertChildElement(xmlDoc, xpath, ed);
        }
        /// <summary>
        /// 删除指定路径的元素节点
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isRemoveAll">删除符合路径的第一个节点还是全部节点</param>
        /// <returns></returns>
        public static void DeleteElement(string filePath, string xpath, bool isRemoveAll)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Delete(path, "/Node/NodeA", true) 删除全部NodeA
             ************************************************/
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                DeleteElement(xmlDocument, xpath, isRemoveAll);
                xmlDocument.Save(filePath);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
        }

        /// <summary>
        /// 从已经构造好的xmlDocument流中删除指定路径的元素节点
        /// </summary>
        public static void DeleteElement(XmlDocument xmlDoc, string xpath, bool isRemoveAll)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Delete(path, "/Node/NodeA", true) 删除全部NodeA
             ************************************************/
            try
            {
                var selectSingleNode = xmlDoc.SelectSingleNode(xpath);
                if (null != selectSingleNode)
                {
                    if (null != selectSingleNode.ParentNode)
                    {
                        if (!isRemoveAll)
                            selectSingleNode.ParentNode.RemoveChild(selectSingleNode);
                        else
                            selectSingleNode.ParentNode.RemoveAll();
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
        }

        /// <summary>
        /// 从已经构造好的xmlDocument流中读取单个节点的数据
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xpath"></param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回文本值</param>
        /// XmlHelper.Read(path, "/Node", "")
        /// XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        /// <returns></returns>
        public static string ReadValue(XmlDocument xmlDoc, string xpath, string attribute)
        {
            var value = "";
            try
            {
                var selectSingleNode = xmlDoc.SelectSingleNode(xpath);
                value = attribute.Equals("")
                    ? selectSingleNode.InnerText
                    : selectSingleNode.Attributes[attribute].Value;
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
            return value;
        }
        /// <summary>
        /// 读取单个节点的数据
        /// </summary>
        /// <param name="file">xml文件路径</param>
        /// <param name="xpath">节点索引</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回文本值</param>
        /// XmlHelper.Read(path, "/Node", "")
        /// XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        /// <returns>string</returns>
        public static string ReadValue(string file, string xpath, string attribute)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Read(path, "/Node", "")
             * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
             ************************************************/
            var value = "";
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                value = ReadValue(xmlDocument, xpath, attribute);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
            return value;
        }

        /// <summary>
        /// 从已经构造好的xmlDocument流中读取匹配xpath路径的文本值或属性值，返回集合
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xpath"></param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回文本值</param>
        /// XmlHelper.Read(file, "/Node", "")
        /// XmlHelper.Read(file, "/Node/Element[@Attribute='Name']", "Attribute")
        public static List<string> ReadValueList(XmlDocument xmlDoc, string xpath, string attribute)
        {
            var selectNodes = xmlDoc.SelectNodes(xpath);
            List<string> value = new List<string>(selectNodes.Count);
            foreach (XmlNode node in selectNodes)
            {
                if (attribute.Equals(""))
                    value.Add(node.InnerText);
                else
                    value.Add(node.Attributes[attribute].Value);
            }

            return value;
        }
        /// <summary>
        /// 读取匹配xpath路径的节点的文本值或属性值
        /// </summary>
        /// <param name="file">xml文件路径</param>
        /// <param name="xpath">节点索引</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回文本值</param>
        /// XmlHelper.Read(path, "/Node", "")
        /// XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        public static List<string> ReadValueList(string file, string xpath, string attribute)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Read(path, "/Node", "")
             * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
             ************************************************/
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                return ReadValueList(xmlDocument, xpath, attribute);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Read Xml Error in xpath: " + xpath, exception);
                return null;
            }
        }

        /// <summary>
        /// 从构建好的xmlDoc流中修改指定路径的节点数据,属性名非空时修改该节点属性值，否则修改节点值<br/>
        /// isUpdateAll为true时修改所有匹配的节点，否则只修改第一个
        /// </summary>
        /// <param name="file">xml文件路径</param>
        /// <param name="xpath">节点索引</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Update(XmlDocument xmlDoc, string xpath, string attribute, string value, bool isUpdateAll)
        {
            if (isUpdateAll == false)
            {
                var xmlElement = (XmlElement)xmlDoc.SelectSingleNode(xpath);
                if (attribute.Equals(""))
                    if (xmlElement != null)
                        xmlElement.InnerText = value;
                    else
                        xmlElement.SetAttribute(attribute, value);
            }
            else
            {
                var selectNodes = xmlDoc.SelectNodes(xpath);
                foreach (var node in selectNodes)
                {
                    var xmlElement = (XmlElement)node;
                    if (attribute.Equals(""))
                        if (xmlElement != null)
                            xmlElement.InnerText = value;
                        else
                            xmlElement.SetAttribute(attribute, value);
                }
            }
        }
        /// <summary>
        /// 修改指定路径的节点数据,属性名非空时修改该节点属性值，否则修改节点值<br/>
        /// isUpdateAll为true时修改所有匹配的节点，否则只修改第一个
        /// </summary>
        /// <param name="file">xml文件路径</param>
        /// <param name="xpath">节点索引</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Update(string file, string xpath, string attribute, string value, bool isUpdateAll)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                Update(xmlDocument, xpath, attribute, value, isUpdateAll);
                xmlDocument.Save(file);
            }
            catch (Exception exception)
            {
                LogHelper.LogError("Error! ", exception);
            }
        }
        #endregion
    }
}