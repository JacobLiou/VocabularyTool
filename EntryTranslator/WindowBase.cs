using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EntryTranslator
{
    public partial class WindowBase : UIForm
    {
        private List<Type> noTypeList = new List<Type>()
        {
            typeof(Panel),
            typeof(ComboBox),
            typeof(Timer),
            typeof(TextBox),
            typeof(MenuStrip),
            typeof(DateTimePicker)
        };

        public WindowBase()
        {
            InitializeComponent();
            ZoomScaleDisabled = true;
        }

        public virtual Dictionary<string, string> GetFormControlList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (Control c in this.Controls)
            {
                if (c.Name.Length == 0)
                {
                    continue;
                }
                if (!noTypeList.Contains(c.GetType()))

                {
                    result.Add(c.Name, c.Text);
                }
                if (c is MenuStrip)
                {
                    var menuStrip = c as MenuStrip;
                    GetMenuStripList(menuStrip, ref result);
                }
                else if (c is ContextMenuStrip)
                {
                    GetContextMenuList(c as ContextMenuStrip, ref result);
                }
                else if (c.Controls.Count > 0)
                {
                    GetControlControlList(c, ref result);
                }
            }
            return result;
        }

        public void GetControlControlList(Control subcontrol, ref Dictionary<string, string> result)
        {
            foreach (Control c in subcontrol.Controls)
            {
                if (c.Name.Length == 0)
                {
                    continue;
                }
                if (!noTypeList.Contains(c.GetType()))

                {
                    result.Add(c.Name, c.Text);
                }
                if (c.Controls.Count > 0)
                {
                    GetControlControlList(c, ref result);
                }
            }
        }

        public void GetMenuStripList(MenuStrip menuStrip, ref Dictionary<string, string> result)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                result.Add(item.Name, item.Text);
                //if(item.)
                //item.
                if (item.DropDownItems.Count > 0)
                {
                    foreach (ToolStripDropDownItem downitem in item.DropDownItems)
                    {
                        result.Add(downitem.Name, downitem.Text);
                        //if(downitem)
                    }
                }
            }
        }

        private void GetContextMenuList(ContextMenuStrip contextmenu, ref Dictionary<string, string> result)
        {
            foreach (ToolStripMenuItem item in contextmenu.Items)
            {
                result.Add(item.Name, item.Text);
                if (item.DropDownItems.Count > 0)
                {
                    foreach (ToolStripDropDownItem downitem in item.DropDownItems)
                    {
                        result.Add(downitem.Name, downitem.Text);
                        //if(downitem)
                    }
                }
            }
        }

        #region 设置文本内容超长显示省略号，鼠标放上去提示全部文本

        public void LTooltip(System.Windows.Forms.Control control, string value)
        {
            if (value.Length == 0) return;
            control.Text = Abbreviation(control, value);
            var tip = new ToolTip();
            tip.IsBalloon = false;
            tip.ShowAlways = true;
            tip.SetToolTip(control, value);
        }

        public string Abbreviation(Control control, string str)
        {
            if (str == null)
            {
                return null;
            }
            int strWidth = FontWidth(control.Font, control, str);

            //获取label最长可以显示多少字符
            int len = control.Width * str.Length / strWidth;
            if (len > 3 && len < str.Length)

            {
                return str.Substring(0, len - 3) + "...";
            }
            else
            {
                return str;
            }
        }

        private int FontWidth(Font font, Control control, string str)
        {
            using (Graphics g = control.CreateGraphics())
            {
                SizeF siF = g.MeasureString(str, font);
                return (int)siF.Width;
            }
        }

        #endregion 设置文本内容超长显示省略号，鼠标放上去提示全部文本
    }
}