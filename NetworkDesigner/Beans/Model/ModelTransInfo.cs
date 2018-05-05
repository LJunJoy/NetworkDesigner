using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Antlr3.ST;
using NetworkDesigner.Beans.Common;
namespace NetworkDesigner.Beans.Model
{
    /// <summary>
    /// 模板转换相关信息
    /// </summary>
    class ModelTransInfo
    {
        private static StringTemplateGroup group = null;
        public static StringTemplate GetQualnetTmplate(string key)
        {
            System.Diagnostics.Debug.WriteLine("强制重新加载StringTemplateGroup模板，测试用");
            group = null;//强制重新加载模板，测试用

            string temDir = null;
            if (group == null)
            {
                temDir = AppSetting.GetModelTransInfos();
                if (temDir != "")
                {
                    group = new StringTemplateGroup("all-template", temDir);
                    group.FileCharEncoding = Encoding.UTF8;
                }
            }
            
            if(group!=null)
                return group.GetInstanceOf(key);
            return null;
        }

    }
}
