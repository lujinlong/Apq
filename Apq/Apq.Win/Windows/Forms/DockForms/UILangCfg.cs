using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apq.Windows.Forms.DockForms
{
    /// <summary>
    /// 语言设置
    /// </summary>
    public partial class UILangCfg : DockForm, Apq.Editor.IFileLoader
    {
        private static string _UILangFolder = string.Empty;
        /// <summary>
        /// 获取语言文件夹
        /// </summary>
        private string UILangFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_UILangFolder))
                {
                    _UILangFolder = System.IO.Path.GetDirectoryName(Apq.GlobalObject.TheProcess.MainModule.FileName) + @"\UILang";
                }
                return _UILangFolder;
            }
        }

        /// <summary>
        /// 语言设置
        /// </summary>
        public UILangCfg()
        {
            InitializeComponent();

            DataGridViewHelper.SetDefaultStyle(dataGridView1);
            DataGridViewHelper.AddBehaivor(dataGridView1);
        }

        /// <summary>
        /// 设置界面语言值
        /// </summary>
        /// <param name="UILang"></param>
        public override void SetUILang(Apq.UILang.UILang UILang)
        {
            this.Text = Apq.GlobalObject.UILang["语言设置"];
            this.TabText = this.Text;

            #region 工具栏
            tsbSave.Text = Apq.GlobalObject.UILang["保存(&S)"];
            tsbCur.Text = Apq.GlobalObject.UILang["当前(&U)"];
            tsbApply.Text = Apq.GlobalObject.UILang["应用(&A)"];
            #endregion

            #region 列头
            col1.HeaderText = Apq.GlobalObject.UILang["原文"];
            col2.HeaderText = Apq.GlobalObject.UILang["中文"];
            #endregion
        }

        private void UILangCfg_Load(object sender, EventArgs e)
        {
            this.DataBindings.Add("FileName", tscbFile, "Text");

            // 读取文件列表
            if (System.IO.Directory.Exists(UILangFolder))
            {
                foreach (string strFileName in System.IO.Directory.GetFiles(UILangFolder, "*.xml"))
                {
                    tscbFile.Items.Add(System.IO.Path.GetFileNameWithoutExtension(strFileName));
                }
            }

            string strUILangFile = Apq.GlobalObject.XmlConfigChain[typeof(Apq.GlobalObject), "UILang"];
            if (!string.IsNullOrEmpty(strUILangFile))
            {
                tscbFile.Text = System.IO.Path.GetFileNameWithoutExtension(strUILangFile);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!tscbFile.Items.Contains(tscbFile.Text))
            {
                tscbFile.Items.Add(tscbFile.Text);
            }

            Save();
        }

        private void tsbCur_Click(object sender, EventArgs e)
        {
            //uiLang1._UILang.Clear();
            uiLang1._UILang.Merge(Apq.GlobalObject.UILang.lst._UILang);
        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            tsbSave_Click(sender, e);

            Apq.GlobalObject.UILang.FileName = UILangFolder + @"\" + FileName + ".xml";
            Apq.GlobalObject.UILang.Load();
            Apq.GlobalObject.XmlConfigChain[typeof(Apq.GlobalObject), "UILang"] = Apq.GlobalObject.UILang.FileName;
            Apq.GlobalObject.XmlConfigChain.Save();

            ImeForm dfParent = this.MdiParent as ImeForm;
            if (dfParent != null)
            {
                dfParent.SetUILang(Apq.GlobalObject.UILang);
            }
            foreach (DockForm dc in dfParent.MdiChildren)
            {
                dc.SetUILang(Apq.GlobalObject.UILang);
            }
        }

        #region IFileLoader 成员
        private string _FileName = string.Empty;
        /// <summary>
        /// 获取或设置语言文件名(不含文件夹和后缀)
        /// </summary>
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
                this.Text = Apq.GlobalObject.UILang["语言设置"] + " - " + _FileName;
                this.TabText = this.Text;
            }
        }

        /// <summary>
        /// 打开语言文件
        /// </summary>
        public void Open()
        {
            string strFullFileName = UILangFolder + @"\" + FileName + ".xml";
            if (System.IO.File.Exists(strFullFileName))
            {
                try
                {
                    Apq.UILang.UILang uiLang = new Apq.UILang.UILangFile();
                    uiLang.lst._UILang.ReadXml(strFullFileName);
                    uiLang1._UILang.Merge(uiLang.lst._UILang);
                }
                catch { }
            }
        }

        /// <summary>
        /// 保存语言文件
        /// </summary>
        public void Save()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                if (!System.IO.Directory.Exists(UILangFolder))
                {
                    System.IO.Directory.CreateDirectory(UILangFolder);
                }
                dataGridView1.EndEdit();
                uiLang1._UILang.WriteXml(UILangFolder + @"\" + FileName + ".xml", XmlWriteMode.IgnoreSchema);
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="FileName"></param>
        public void SaveAs(string FileName)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void tscbFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Open();
        }
    }
}
