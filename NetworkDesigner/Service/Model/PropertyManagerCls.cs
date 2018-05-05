using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

using NetworkDesigner.Beans.Model;

namespace NetworkDesigner.Service.Model
{
    public class PropertyManagerCls : CollectionBase, ICustomTypeDescriptor
    {
        //public PropertyManagerCls(ModelData md)
        //{
        //    foreach (MyProperty prop in md.PropsModel.Values)
        //    {
        //        this.Add(prop);
        //    }
        //}

        public void Add(MyProperty value)
        {
            int flag = -1;
            if (value != null)
            {
                if (base.List.Count > 0)
                {
                    IList<MyProperty> mList = new List<MyProperty>(); //作temp使用
                    for (int i = 0; i < base.List.Count; i++)
                    {
                        MyProperty p = base.List[i] as MyProperty;
                        if (value.name == p.name)
                        {
                            flag = i;
                        }
                        mList.Add(p);
                    }
                    if (flag == -1) //之前没添加过
                    {
                        mList.Add(value);
                    }
                    base.List.Clear();
                    foreach (MyProperty p in mList)
                    {
                        base.List.Add(p); //再放回去
                    }
                }
                else //之前肯定没添加过
                {
                    base.List.Add(value);
                }
            }
        }
        public void Remove(MyProperty value)
        {
            if (value != null && base.List.Count > 0)
                base.List.Remove(value);
        }
        public MyProperty this[int index]
        {
            get
            {
                return (MyProperty)base.List[index];
            }
            set
            {
                base.List[index] = (MyProperty)value;
            }
        }
        #region ICustomTypeDescriptor 成员
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                MyProperty prop = (MyProperty)this[i];
                newProps[i] = new CustomPropertyDescriptor(prop, attributes);
            }
            return new PropertyDescriptorCollection(newProps);
        }
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        MyProperty m_Property;
        public CustomPropertyDescriptor(MyProperty myProperty, Attribute[] attrs)
            : base(myProperty.name, attrs)
        {
            m_Property = myProperty;
        }
        #region 默认方法
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override Type ComponentType //不需要设计时的操作
        {
            get
            {
                return null;
            }
        }
        public override bool IsReadOnly
        {
            get
            {
                //return m_Property.ReadOnly;
                return false;
            }
        }
        public override void ResetValue(object component)
        {
            //暂时不支持重置操作
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        public override Type PropertyType
        {
            get { return m_Property.property.GetType(); }
        }
        #endregion

        public override string Description
        {
            get
            {
                return m_Property.path;
            }
        }
        public override string Category
        {
            get
            {
                return m_Property.disCategory;
            }
        }
        public override string DisplayName
        {
            get
            {
                return m_Property.display != "" ? m_Property.display : m_Property.name;
            }
        }

        public override object GetValue(object component)
        {
            string str="";
            try
            {
                str = m_Property.GetPropertyByStr();
            }
            catch (Exception)
            {
                MessageBox.Show("获取属性字符串表示值异常" + m_Property.name);
            }
            return str;
        }
        public override void SetValue(object component, object value)
        {
            try
            {
                string str = value as string;
                m_Property.SetPropertyByStr(str);
            }
            catch (Exception)
            {
                MessageBox.Show("设置属性字符串表示值异常" + m_Property.name);
            }
        }
        public override TypeConverter Converter
        {
            get
            {
                return m_Property.GetConverter();
            }
        }
        public override object GetEditor(Type editorBaseType)
        {
            object editor = m_Property.GetEditor();
            return editor == null ? base.GetEditor(editorBaseType) : editor;
        }

    }

    //用于模型的下拉框类型转换器  
    public class ModelSimpleConverter : StringConverter
    {
        public ModelSimpleConverter()
        {
 
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        //允许输入下拉框中没有的值
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context.Instance is ModelInfo)
            {
                ModelInfo model = context.Instance as ModelInfo;
                string str = context.PropertyDescriptor.Name;
                if (str.Equals("类型"))
                {
                    return new StandardValuesCollection(ModelType.AllTypes[model.modType.Category]);
                }
                else if (str.Equals("类别"))
                {
                    return new StandardValuesCollection(ModelType.AllTypes.Keys);
                }
            }
            return new StandardValuesCollection(new object[]{});
        }
    }

    /// <summary>  
    /// 打开文件对话框
    /// </summary>  
    public class FileItemConverter : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, 
            System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                // 可以打开任何特定的对话框  
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.AddExtension = false;
                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.FileName;
                }
            }
            return value;
        }
    }

    public class LinesTextConverter : UITypeEditor
    {
        public string type = GDataType.ListDouble_En;
        public LinesTextConverter()
        {
 
        }
        public LinesTextConverter(string type)
        {
            this.type = type;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    if (value is string)
                    {
                        RichTextBox box = new RichTextBox();
                        StringBuilder sbd = new StringBuilder();
                        string input = value as string;
                        if (this.type == GDataType.ListInt_En)
                        {
                            List<int> data = ConvertHelper.ListIntConverter(input);
                            foreach (int i in data)
                                sbd.Append(i).Append("\n");
                            box.Text = sbd.ToString();
                        }
                        else if (this.type == GDataType.ListDouble_En)
                        {
                            List<double> data = ConvertHelper.ListDoubleConverter(input);
                            foreach (double i in data)
                                sbd.Append(i).Append("\n");
                            box.Text = sbd.ToString();
                        }
                        else if (this.type == GDataType.ListString_En)
                        {
                            List<string> data = ConvertHelper.ListStringConverter(input);
                            foreach (string i in data)
                                sbd.Append(i).Append("\n");
                            box.Text = sbd.ToString();
                        }

                        edSvc.DropDownControl(box);

                        sbd.Clear();
                        foreach (string line in box.Lines)
                        {
                            if (line.Length != 0)
                            {
                                if (this.type == GDataType.ListString_En)
                                    sbd.Append("{").Append(line).Append("}");
                                else
                                    sbd.Append(line).Append(",");
                            }
                        }
                        if (sbd.ToString().EndsWith(","))
                            return sbd.Remove(sbd.Length - 2, 1).ToString();
                        return sbd.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入格式有误" + ex.Message);
                return value;
            }
            return value;
        }
    }
}
