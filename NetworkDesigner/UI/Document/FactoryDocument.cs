using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.UI.Document
{
    public static class FactoryDocument
    {
        public static List<FrmDocBase> allDocs = new List<FrmDocBase>();

        public static FrmDocBase FindDocument(string filePath)
        {
            foreach (var doc in allDocs)
            {
                if (filePath.Equals(doc.docInfo.filePath))
                {
                    return doc;
                }
            }
            return null;
        }
        /// <summary>
        /// <para>当文档窗口重新加载文件时，由于窗口并没有关闭，只更新其docInfo</para>
        /// 能找到原docInfo时更新docInfo并返回旧的docInfo，找不到时不作处理并返回null
        /// </summary>
        /// <param name="oldFilePath"></param>
        /// <param name="newFilePath"></param>
        /// <returns></returns>
        public static DocInfo UpdateDocumentInfo(string oldFilePath,string newFilePath)
        {
            DocInfo old = null;
            foreach (var doc in allDocs)
            {
                if (oldFilePath.Equals(doc.docInfo.filePath))
                {
                    old = doc.docInfo;
                    doc.docInfo = new DocInfo(newFilePath);
                    doc.Text = doc.docInfo.fileName;
                    doc.ToolTipText = newFilePath;
                    Debug.WriteLine("更新DocInfo：\r\n  " + oldFilePath + "\r\n=>" + newFilePath);
                    return old;
                }
            }
            if (old == null)
            {
                Debug.WriteLine("找不到旧路径的DocInfo：\r\n  " + oldFilePath);
            }
            return old;
        }
        /// <summary>
        /// 按文件路径查找文档并删除，成功删除则返回文档窗口，找不到则返回null
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FrmDocBase RemoveDocument(string filePath)
        {
            foreach (var doc in allDocs)
            {
                if (filePath.Equals(doc.docInfo.filePath))
                {
                    allDocs.Remove(doc);
                    System.Diagnostics.Debug.WriteLine("移除文档：" + doc.docInfo.fileName
                        + " 打开文档个数：" + allDocs.Count);
                    return doc;
                }
            }
            
            return null;
        }

        /// <summary>
        /// 按窗口查找文档并删除，成功删除则返回true，找不到则返回false
        /// </summary>
        /// <returns></returns>
        public static bool RemoveDocument(FrmDocBase form)
        {
            foreach (var doc in allDocs)
            {
                if (doc == form)
                {
                    allDocs.Remove(doc);
                    System.Diagnostics.Debug.WriteLine("移除文档：" + doc.docInfo.fileName
                        + " 打开文档个数：" + allDocs.Count);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 新建一个空的文本文档，可以指定目录
        /// </summary>
        /// <param name="mf"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FrmDocBase CreateBlankTextDocument(FrmMain mf, string dirPath="")
        {
            int index = 1;
            while (true)
            {
                if(null != FindDocumentByName("文档" + index))
                    index++;
                else
                    break;
            }

            string filePath = System.IO.Path.Combine(dirPath, "文档" + index);
            FrmDocBase frm = new FrmTextDoc(mf);
            DocInfo newdoc = new DocInfo(filePath);
            frm.Text = newdoc.fileName;
            frm.ToolTipText = "新建文档";
            frm.docInfo = newdoc;
            allDocs.Add(frm);
            System.Diagnostics.Debug.WriteLine("新建文档：" + newdoc.fileName
                        + " 打开文档个数：" + allDocs.Count);
            return frm;
        }

        public static FrmDocBase CreateBlankDiagramDocument(FrmMain mf, string dirPath = "")
        {
            int index = 1;
            while (true)
            {
                if (null != FindDocumentByName("网络" + index))
                    index++;
                else
                    break;
            }

            string filePath = System.IO.Path.Combine(dirPath, "网络" + index);
            FrmDocBase frm = new FrmDiagram(mf);
            DocInfo newdoc = new DocInfo(filePath);
            frm.Text = newdoc.fileName;
            frm.ToolTipText = "新建网络";
            frm.docInfo = newdoc;
            allDocs.Add(frm);
            System.Diagnostics.Debug.WriteLine("新建网络：" + newdoc.fileName
                        + " 打开文档个数：" + allDocs.Count);
            return frm;
        }

        public static FrmDocBase CreateBlankSymBolDocument(FrmMain mf, string dirPath = "")
        {
            int index = 1;
            while (true)
            {
                if (null != FindDocumentByName("模型" + index))
                    index++;
                else
                    break;
            }

            string filePath = System.IO.Path.Combine(dirPath, "模型" + index);
            FrmDocBase frm = new FrmSymbol(mf);
            DocInfo newdoc = new DocInfo(filePath);
            frm.Text = newdoc.fileName;
            frm.ToolTipText = "新建模型";
            frm.docInfo = newdoc;
            allDocs.Add(frm);
            System.Diagnostics.Debug.WriteLine("新建模型：" + newdoc.fileName
                        + " 打开文档个数：" + allDocs.Count);
            return frm;
        }

        private static FrmDocBase FindDocumentByName(string fileName)
        {
            foreach (var doc in allDocs)
            {
                if (fileName.Equals(doc.docInfo.fileName))
                {
                    return doc;
                }
            }
            return null;
        }
        /// <summary>
        /// 按文件路径新建对应类型的文档窗口，若文档已打开直接返回对应窗口，否则新建，若不支持该类型文档例如空文档，返回null。
        /// </summary>
        /// <param name="mf"></param>
        /// <param name="filePath"></param>
        /// <param name="alreadyOpen"></param>
        /// <returns></returns>
        public static FrmDocBase CreateDocument(FrmMain mf, string filePath, out bool alreadyOpen)
        {
            alreadyOpen = false;
            FrmDocBase frm = FindDocument(filePath);
            if (frm != null)
            {
                alreadyOpen = true;
                return frm;
            }
            
            DocInfo newdoc = new DocInfo(filePath);
            switch (newdoc.T)
            {
                case DocInfo.DType.TXT:
                case DocInfo.DType.XML:
                    frm = new FrmTextDoc(mf);
                    frm.Text = newdoc.fileName;
                    frm.ToolTipText = filePath;
                    frm.docInfo = newdoc;
                    break;
                case DocInfo.DType.DIA:
                    frm = new FrmDiagram(mf);
                    frm.Text = newdoc.fileName;
                    frm.ToolTipText = filePath;
                    frm.docInfo = newdoc;
                    break;
                case DocInfo.DType.PAT:
                    frm = new FrmSymbol(mf);
                    frm.Text = newdoc.fileName;
                    frm.ToolTipText = filePath;
                    frm.docInfo = newdoc;
                    break;
                default:
                    MessageBox.Show("暂不支持打开此类文档", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            if (frm != null)
            {
                allDocs.Add(frm);
                System.Diagnostics.Debug.WriteLine("打开文档：" + newdoc.fileName
                        + " 打开文档个数：" + allDocs.Count);
            }
            return frm;
        }

    }
}
