using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using NetworkDesigner.Beans.Model;
using Syncfusion.Windows.Forms.Diagram;
using NetworkDesigner.Beans.Common;
using NetworkDesigner.Utils.DiagramUtil;
using NetworkDesigner.Utils.Common;

namespace NetworkDesigner.Service.Model
{
    public class SimIP
    {
        public int[] ip={192, 168, 1, 1};
        public SimIP()
        {

        }
        public SimIP(string ipstr)
        {
            string[] strs = ipstr.Split('.');
            int temp = 1;
            for (int i = 0; i < strs.Length && i < ip.Length; i++)
            {
                if (!int.TryParse(strs[i], out temp))
                {
                    LogHelper.LogInfo("无效IP：" + ipstr);
                    break;
                }
                ip[i] = temp;
            }
        }
        public SimIP(int i, int j, int k, int l)
        {
            ip[0] = i;
            ip[1] = j;
            ip[2] = k;
            ip[3] = l;
        }
        public bool SameNet8(SimIP o)
        {
            for (int i = 0; i < 3; i++)
            {
                if (ip[i] != o.ip[i])
                    return false;
            }
            return true;
        }
        public bool SameNet8(int i1, int i2, int i3)
        {
            return (ip[0] == i1 && ip[1] == i2 && ip[2] == i3);
        }
        public bool SameNet16(SimIP o)
        {
            for (int i = 0; i < 2; i++)
            {
                if (ip[i] != o.ip[i])
                    return false;
            }
            return true;
        }

        private static StringBuilder sbd = new StringBuilder();
        public override string ToString()
        {
            sbd.Clear();
            return sbd.Append(ip[0]).Append(".").Append(ip[1]).Append(".")
                .Append(ip[2]).Append(".").Append(ip[3]).ToString();
        }
    }
    class ModelInfoHelper
    {
        static ModelInfoHelper()
        {
            string[] nets = new string[] { "无线子网", "子网", "交换机", "集线器", "hub", "switch", "wiredsubnet", "wiredlesssubnet" };
            LanNetName.AddRange(nets);
        }
        public static List<string> LanNetName = new List<string>();
        public static List<SimIP> LanNetUsed = new List<SimIP>();
        public static Dictionary<string, List<int>> LanHostUsed = new Dictionary<string, List<int>>();
        /// <summary>
        /// 返回下一个未占用的局域网主机ip
        /// </summary>
        /// <returns></returns>
        public static string GetNextLanHost(string net)
        {
            SimIP sip = new SimIP(net);
            List<int> notused = null;
            if (LanHostUsed.ContainsKey(net))
                notused = LanHostUsed[net];
            else
            {
                notused = new List<int>();
                LanHostUsed[net] = notused;
            }
            for (int i = 1; i < 255; i++)
            {
                if (!notused.Contains(i))
                {
                    sip.ip[3] = i;
                    notused.Add(i);
                    return sip.ToString();
                }
            }
            return "192.168.100.1";
        }
        /// <summary>
        /// 返回下一个未占用的8位主机号的网段
        /// </summary>
        /// <returns></returns>
        public static string GetNextLanNet()
        {
            int ip1 = 190;
            int ip2 = 10;
            int ip3 = 1;
            int ip4 = 0;
            bool used = false;
            while (ip3 < 255)
            {
                foreach (var ip in LanNetUsed)
                {
                    if (ip.SameNet8(ip1, ip2, ip3))
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    SimIP ip = new SimIP(ip1, ip2, ip3, ip4);
                    LanNetUsed.Add(ip);
                    string ipstr = ip.ToString();
                    LanHostUsed[ipstr] = new List<int>();
                    return ipstr;
                }
                else
                {
                    used = false;
                    ip3++;
                }
            }
            ip2++;
            while (ip2 < 255)
            {
                foreach (var ip in LanNetUsed)
                {
                    if (ip.SameNet8(ip1, ip2, ip3))
                    {
                        ip2++;
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    SimIP ip = new SimIP(ip1, ip2, ip3, ip4);
                    LanNetUsed.Add(ip);
                    string ipstr = ip.ToString();
                    LanHostUsed[ipstr] = new List<int>();
                    return ipstr;
                }
            }
            return "190.10.10.0";
        }
        /// <summary>
        /// 禁止通过索引器设置的属性项
        /// </summary>
        public static List<string> NotSetByIndexer = new List<string>();
        /// <summary>
        /// 全局的模型信息库，面板名称-模型信息列表，注意直接修改可能引起的错误
        /// </summary>
        public static Dictionary<string, List<SimModel>> patModels = new Dictionary<string, List<SimModel>>();
        public static StringCollection paletteChoices = new StringCollection();
        /// <summary>
        /// 模型类别-所有模型名称
        /// </summary>
        public static Dictionary<string, StringCollection> modelCateChoices = new Dictionary<string, StringCollection>();
        public static StringCollection modelCates = new StringCollection();
        /// <summary>
        /// 全局的模型信息库，模型ID-模型，注意应该在UpdateChoices中动态更新
        /// </summary>
        public static Dictionary<string, SimModel> allModels = new Dictionary<string, SimModel>();
        /// <summary>
        /// 对模型库作修改后都要调用此方法更新缓存信息
        /// </summary>
        public static void UpdateChoices()
        {
            paletteChoices.Clear();
            paletteChoices.AddRange(patModels.Keys.ToArray());
            allModels.Clear();
            modelCateChoices.Clear();
            List<SimModel> models;
            StringCollection names;
            foreach (string key in patModels.Keys)
            {
                models = patModels[key];
                foreach (SimModel model in models)
                {
                    allModels[model.modelID] = model;

                    if (modelCateChoices.ContainsKey(model.modelCate))
                        names = modelCateChoices[model.modelCate];
                    else
                    {
                        names = new StringCollection();
                        modelCateChoices[model.modelCate] = names;
                    }
                    names.Add(model.modelName);
                }
            }
            modelCates.Clear();
            foreach (string key in modelCateChoices.Keys)
            {
                if(key.Contains(".实体.节点"))
                    modelCates.Add(key);
            }
   
        }
        /// <summary>
        /// 返回指定模型类别中的模型名称选项，找不到时返回含0个元素的Collection
        /// </summary>
        public static StringCollection GetModelCateChoice(string modelCate)
        {
            if (modelCateChoices.ContainsKey(modelCate))
                return modelCateChoices[modelCate];
            else
                return new StringCollection();
        }
        /// <summary>
        /// 根据模型库名称和模型名查询模型，为提高效率没有返回深拷贝，注意外部要修改其对象内容时要先深拷贝！
        /// </summary>
        public static SimModel FindModelInfo(string palette, string name)
        {
            if(patModels.ContainsKey(palette))
            {
                foreach (SimModel model in patModels[palette])
                {
                    if (model.modelName.Equals(name))
                        return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过深拷贝other来更新自己的参数
        /// </summary>
        /// <param name="paletteName"></param>
        /// <param name="modelInfo"></param>
        public static void UpdateModelInfo(string paletteName, SimModel other)
        {
            string[] strs = paletteName.Split('.');
            if (strs.Length != 2)
                return;
            string palette = strs[0];
            string name = strs[1];
            bool isUpdate = false;
            if (patModels.ContainsKey(palette))
            {
                List<SimModel> lm = patModels[palette];
                foreach (var model in lm)
                {
                    if (model.modelName.Equals(name))
                    {
                        isUpdate = true;
                        model.DeepCopy(other);
                        break;
                    }
                }
                if(isUpdate)
                    UpdateChoices();
            }
        }

        /// <summary>
        /// 读取模型库文件中Model信息
        /// </summary>
        public static List<SimModel> LoadModelFromFile(string fileName)
        {
            XmlNodeList xnodes = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlElement root = doc.DocumentElement;    //获取根节点
                xnodes = root.SelectNodes("Model");
            }
            catch (Exception e)
            {
                LogHelper.LogInfo("无效的模型文件："+fileName);
                throw e;
            }

            SimModel model = null;
            List<SimModel> models = new List<SimModel>();
            string modelName = "";
            int count=0;
            foreach (XmlNode xn in xnodes)
            {
                try
	            {
                    modelName = "No." + count;
                    modelName = xn.Attributes["模型名称"].Value;
                    model = SimModel.ParseXml(xn);
                    model.modelID = model.modelCate + "." + model.modelName;
                    models.Add(model);
                }
                catch (Exception e)
                {
                    LogHelper.LogInfo("无效的模型节点：" + fileName + "::" + modelName);
                }
            }
            return models;
        }

        /// <summary>
        /// 从文件中加载ModelInfo，如果文件不存在，返回的palette不包含元素
        /// </summary>
        public static SymbolPalette LoadPaletteFromFile(string fileName)
        {
            SymbolPalette palette = new SymbolPalette();
            List<SimModel> models = null;
            try
            {
                models = LoadModelFromFile(fileName);
            }
            catch (Exception e)
            {
                return palette;
            }
            ModelInfoHelper.patModels[Path.GetFileNameWithoutExtension(fileName)] = models;

            BitmapNode node;
            foreach (SimModel model in models)
            {
                node = new BitmapNode(GetPaletteNodeImage(fileName, model.modelImage));
                node.Name = model.modelName;
                node.Tag = model.modelID;
                PaletteHelper.SetNodeInPalette(node);
                node.LineStyle.LineColor = System.Drawing.Color.Transparent;
                palette.AppendChild(node);
            }
            UpdateChoices();
            return palette;
        }

        /// <summary>
        /// 将面板各模型的ModelInfo保存到文件，若有异常会抛给调用者
        /// </summary>
        public static void SavePaletteToFile(SymbolPalette palette, string paletteFile)
        {
            string paletteFileName = Path.GetFileNameWithoutExtension(paletteFile);
            List<SimModel> models = ModelInfoHelper.patModels[paletteFileName];
            DirectoryInfo paletteDir;
            FileInfo [] files;

            string paletteDirName = Path.GetDirectoryName(paletteFile);
            try //每一个面板保存为单独的文件夹，且里面有同名的.pat
	        {
                if (File.Exists(paletteFile)) //.pat存在了那么文件夹肯定也存在
                {
                    string bakFile = paletteFile + "_bak";
                    if (File.Exists(bakFile))
                        File.Delete(bakFile);
                    File.Move(paletteFile, paletteFile + "_bak"); //备份一下上次保存的文件
                    paletteDir = new DirectoryInfo(paletteDirName);
                }
                else if (!Directory.Exists(paletteDirName)) //.pat不存在则要检查文件夹是否存在
                {
                    paletteDir = Directory.CreateDirectory(paletteDirName);
                }
                else
                    paletteDir = new DirectoryInfo(paletteDirName);
	        }
	        catch (Exception e)
	        {
                throw e;
	        }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement root = xmlDoc.CreateElement("ModelInfo");
            xmlDoc.AppendChild(root);
            foreach (SimModel model in models)
            {
                model.SaveXml(xmlDoc);
                //保存需要的模型图标
                if(!model.modelImage.Equals("default.png"))
                {
                    bool isExist = false;
                    files = paletteDir.GetFiles();
                    foreach(var file in files)
                    {
                        if (file.FullName.Equals(model.modelImage)) //如果全路径都相同，说明已保存过图标
                        {
                            isExist = true;
                            break;
                        }
                        else if (file.Name.Equals(Path.GetFileName(model.modelImage)))
                        {
                            isExist = true;
                            break;
                        }
                        //只有当model.ModelImage设置为全路径且不存在，或者时才保存图标
                    }
                    if (!isExist)
                    {
                        if(File.Exists(model.modelImage))
                        {
                            try 
	                        {	        
		                        File.Copy(model.modelImage,Path.Combine(paletteDirName,
                                    Path.GetFileName(model.modelImage)),true);
	                        }
	                        catch (Exception)
	                        {
		                        NetworkDesigner.Utils.Common.LogHelper.LogInfo("图片保存失败："+model.modelImage);
	                        }
                        
                        }
                    }
                }
            }

            //if is group...由modelInfo表达，node本身不用包含这种信息
            xmlDoc.Save(paletteFile);
        }
        public static string GetPaletteNodeImage(string paletteFile, string image)
        {
            string imageDir = Path.GetDirectoryName(paletteFile);
            string imageFile = Path.Combine(imageDir, image);
            if (!File.Exists(imageFile))
            {
                imageFile = Path.Combine(imageDir, "default.png");
                if (!File.Exists(imageFile))
                    imageFile = Path.Combine(AppSetting.BinPath, @"Config\default_node.png");
                if (!File.Exists(imageFile))
                    throw new Exception("找不到节点图片" + image);
            }
            return imageFile;
        }

        public static SimModelType GetModelType(string modelID)
        {
            if (modelID.Contains("实体.节点"))
                return SimModelType.节点;
            if (modelID.Contains("实体.链路"))
                return SimModelType.链路;
            if (modelID.Contains(".业务"))
                return SimModelType.业务;
            if (modelID.Contains(".结果"))
                return SimModelType.结果;
            if (modelID.Contains(".拓扑"))
                return SimModelType.拓扑;
            return SimModelType.未知;
        }
        /// <summary>
        /// 该节点的接口是否在同一网段
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public static bool IsLanNet(string modelID)
        {
            string lower = modelID.ToLower();
            foreach (string net in LanNetName)
            {
                if (lower.Contains(net))
                    return true;    
            }
            return false;
        }
    }
    public enum SimModelType
    {
        未知,节点,链路,业务,结果,拓扑
    }
}
