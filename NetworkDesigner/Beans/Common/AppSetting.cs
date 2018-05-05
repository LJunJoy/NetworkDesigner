using NetworkDesigner.Utils.Common;
using NetworkDesigner.Utils.FileUtil;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner.Beans.Common
{
    /// <summary>
    /// 程序配置以及常量字符串
    /// </summary>
    public class AppSetting
    {
        #region 需要在退出程序时保存的配置参数
        
        /// <summary>
        /// 最近打开的项目路径
        /// </summary>
        public static List<string> LatestRecentProjects;//读取xml后存RecentProjects，写入xml时用LatestRecentProjects
        public static int RecentMaxFiles = 5;

        #endregion

        #region 不需要保存到文件的配置参数

        //这里可集中修改用到的常量字符串
        private static string _settingFile = @"Config\AppSetting.xml";
        private static string _dockPanelFile = @"Config\DockPanel.config";
        private static string _modelFilePath = @"Config\Model";
        private static string xpath_RecentProjects = "/UserSetting/RecentProjectPath/ProjectPath";
        private static string xpath_RecentProjects_Parent = "/UserSetting/RecentProjectPath";
        private static string xpath_SyntaxModes = "/UserSetting/EditorSyntaxModes/SyntaxMode";
        private static string xpath_SymbolPalettes_Dir = "/UserSetting/SymbolPalettes/Directory";

        public static List<string> RecentProjects;//读取xml后存RecentProjects，写入xml时用LatestRecentProjects
        public static List<string> EditorSyntaxModes;
        public static int TreeGridIndentSize = 16;

        private static string _BinPath = "";
        /// <summary>
        /// 获取应用程序根路径，注意：开发阶段是用的bin文件夹的同层目录，后续可能需要修改此函数
        /// </summary>
        public static string BinPath
        {
            get
            {
                if ("".Equals(_BinPath))
                {
                    string path = System.Windows.Forms.Application.StartupPath;
                    //string path=AppDomain.CurrentDomain.BaseDirectory; //另一种获取方式
                    string folderName = String.Empty;
                    while (folderName.ToLower() != "bin")
                    {
                        path = path.Substring(0, path.LastIndexOf("\\"));
                        folderName = path.Substring(path.LastIndexOf("\\") + 1);
                    }
                    _BinPath = path.Substring(0, path.LastIndexOf("\\") + 1);
                }
                return _BinPath;
            }
        }

        /// <summary>
        /// 程序配置文件AppSetting.Xml的完整路径
        /// </summary>
        public static string AppSettingXmlFile
        {
            get {
                return Path.Combine(BinPath, _settingFile);
            }
        }

        /// <summary>
        /// 程序配置文件DockPanel.config的完整路径
        /// </summary>
        public static string DockPanelFile
        {
            get {
                return Path.Combine(BinPath,AppSetting._dockPanelFile);
            }
        }

        public static string DefaultModelDirPath
        {
            get { 
                return Path.Combine(BinPath, _modelFilePath); 
            }
        }
        /// <summary>
        /// “我的文档”目录路径
        /// </summary>
        public static string MyDocumentPath = System.Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);
        /// <summary>
        /// 返回SymbolPattels的文件路径
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSymbolPalettes()
        {
            List<string> palettes = new List<string>();
            var dirs = XmlHelper.ReadValueList(AppSettingXmlFile, xpath_SymbolPalettes_Dir, "");
            foreach (string dir in dirs)
            {
                string path = dir;
                if(!dir.Contains(":"))//不包含:的相对路径
                    path = Path.Combine(BinPath, dir);
                if (Directory.Exists(path))
                {
                    string[] models = Directory.GetDirectories(path);//有可能包含目录，要判断
                    foreach (string mod in models)
                    {
                        string[] files = Directory.GetFiles(mod);
                        string extension = "";
                        foreach (string file in files)
                        {
                            extension = Path.GetExtension(file);
                            if (extension.Equals(".pat") || extension.Equals(".xml"))
                            {
                                palettes.Add(file);
                                break;
                            }
                        }
                    }
                }
            }
            return palettes;
        }
        /// <summary>
        /// 返回模型映射的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetModelMapsInfos()
        {
            var dirs = XmlHelper.ReadValueList(AppSettingXmlFile, xpath_SymbolPalettes_Dir, "");
            foreach (string dir in dirs)
            {
                string path = dir;
                if (!dir.Contains(":"))//不包含:的相对路径
                    path = Path.Combine(BinPath, dir);
                if (Directory.Exists(path))
                {
                    string[] models = Directory.GetDirectories(path);//有可能包含目录，要判断
                    foreach (string mod in models)
                    {
                        if (!mod.Contains("模型映射"))
                            continue;
                        return mod;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 返回模板转换的文件目录
        /// </summary>
        /// <returns></returns>
        public static string GetModelTransInfos()
        {
            var dirs = XmlHelper.ReadValueList(AppSettingXmlFile, xpath_SymbolPalettes_Dir, "");
            foreach (string dir in dirs)
            {
                string path = dir;
                if (!dir.Contains(":"))//不包含:的相对路径
                    path = Path.Combine(BinPath, dir);
                if (Directory.Exists(path))
                {
                    string[] models = Directory.GetDirectories(path);
                    foreach (string mod in models)
                    {
                        if (!mod.Contains("模板转换"))
                            continue;
                        return mod;
                    }
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 保存程序配置到UserSetting.xml
        /// </summary>
        public static void SaveUserSetting()
        {
            //保存最近打开项目的路径
            XmlHelper.DeleteElement(AppSettingXmlFile, xpath_RecentProjects, true);
            List<XmlHelper.ElementData> eds = new List<XmlHelper.ElementData>(LatestRecentProjects.Count);
            foreach (string path in LatestRecentProjects)
            {
                var data = new XmlHelper.ElementData("ProjectPath",path,null);
                eds.Insert(0, data);
            }
            XmlHelper.InsertChildElementList(AppSettingXmlFile, xpath_RecentProjects_Parent, eds);
        }
        /// <summary>
        /// 加载UserSetting.xml配置文件,返回true表示正常加载
        /// </summary>
        /// <returns></returns>
        public static bool LoadUserSetting()
        {
            if (FileHelper.IsExistFile(AppSettingXmlFile) == false)
            {
                MessageBox.Show("错误! 找不到配置文件AppSetting.xml\n" + AppSettingXmlFile);
                return false;
            }
            try
            {
                RecentProjects = XmlHelper.ReadValueList(AppSettingXmlFile, xpath_RecentProjects, "");
                EditorSyntaxModes = XmlHelper.ReadValueList(AppSettingXmlFile, xpath_SyntaxModes, "");
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
                return false;
            }

            return true;
        }
    }
}
