/*
 * @Author:  yzhou
 * @Date:    2006.07.28
 * @Version: 1.0
 */

using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace KODY.WindowsUI
{
    public class DataTreeView : TreeViewAdv
    {
        #region Member

        internal bool hasChanges = false;
        protected bool hasErrors = false;
        protected DataTable dataSource = null;
        private DataTreeViewKeys primaryKeys;
        private DataTreeViewDataSourceType dataSourceType = DataTreeViewDataSourceType.Unknow;
        private DataColumn[] tablePK = null;
        private string nodeTextField = "Name";
        #endregion

        public DataTreeView()
            : base()
        {
            this.InitalizeDataTreeView();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                try
                {
                    this.dataSource.RejectChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void InitalizeDataTreeView()
        {
            this.primaryKeys = DataTreeViewKeys.Default;

            this.ListenNodeOpEvents();
            InitExtendMenu();
        }

        internal DataTable GetSrcDataTable()
        {
            return this.dataSource;
        }

        #region Extend events

        public delegate void DataSourceChangedHandle(object sender, DataTreeViewDataSourceChangeEventArgs e);
        public event DataSourceChangedHandle DataSourceChanged = null;
        #endregion

        #region Extend interface

        public DataTreeViewKeys PrimaryKeys
        {
            get { return this.primaryKeys; }
            set { this.primaryKeys = value; }
        }

        public string NodeTextField
        {
            set
            {
                this.nodeTextField = value;
                if ((this.nodeTextField == null) || (this.nodeTextField == string.Empty))
                {
                    this.nodeTextField = "Name";
                }
            }
            get
            {
                return this.nodeTextField;
            }
        }

        public bool HasChanges
        {
            get
            {
                //return this.hasChanges;

                if ((this.dataSource != null) && (this.dataSource.GetChanges() != null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasErrors
        {
            get { return this.hasErrors; }
        }

        public virtual void LoadFromXml(string flieName)
        {
            this.dataSourceType = DataTreeViewDataSourceType.XML;

            DataSet ds = new DataSet("TreeViewStructure");
            ds.ReadXml(flieName);
            this.DataSource = ds.Tables[0];
        }

        public virtual void SaveToXml(string fileName)
        {
            if (this.dataSource == null)
            {
                this.CreateDataSourceStructure();
            }

            this.AcceptDataSourceChanges();
            DataSet ds = this.dataSource.DataSet;
            if (ds == null)
            {
                ds = new DataSet("TreeViewStructure");
                ds.Tables.Add(this.dataSource);
            }
            ds.WriteXml(fileName, XmlWriteMode.WriteSchema);
        }

        public virtual DataTable DataSource
        {
            get
            {
                if (this.dataSource != null)
                {
                    this.AcceptDataSourceChanges();

                    this.ResetTablePK();
                }

                return this.dataSource;
            }

            set
            {
                this.dataSource = value;

                if (this.dataSource == null)
                {
                    if (this.DataSourceChanged != null)
                    {
                        this.DataSourceChanged(this, new DataTreeViewDataSourceChangeEventArgs(DataTreeViewDataSourceType.Unknow));
                    }
                    return;
                }

                if ((!this.dataSource.HasErrors) && (this.dataSourceType == DataTreeViewDataSourceType.XML))
                {
                    this.dataSource.AcceptChanges();
                }

                this.LoadData();
                this.CancelTablePK();

                if (this.DataSourceChanged != null)
                {
                    if (this.dataSourceType == DataTreeViewDataSourceType.Unknow)
                    {
                        this.dataSourceType = DataTreeViewDataSourceType.DataBase;
                    }
                    this.DataSourceChanged(this, new DataTreeViewDataSourceChangeEventArgs(dataSourceType));
                }
            }
        }

        public void AcceptDataSourceChanges()
        {
            if (this.dataSource != null)
            {
                this.AcceptChangeToDataSource(this.Nodes);

                //this.LoadData();
            }
        }

        public void RejectDataSourceChanges()
        {
            if (this.dataSource != null)
            {
                this.dataSource.RejectChanges();

                this.LoadData();
            }
        }

        public object GetExtendedProperty(TreeNode node, string propertyName)
        {
            try
            {
                if ((!this.Equals(node.TreeView) || (this.dataSource == null)))
                {
                    return null;
                }

                DataRow row = node.Tag as DataRow;
                if (row.Table.Columns[propertyName] != null)
                {
                    return row[propertyName].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }

        public void SetExtendedProperty(TreeNode node, string propertyName, object value)
        {
            if ((propertyName == this.primaryKeys.ID)
                || (propertyName == this.primaryKeys.ParentID)
                || (propertyName == this.primaryKeys.Sequence)
                || (!this.Equals(node.TreeView))
                || (this.dataSource == null))
            {
                return;
            }

            DataRow row = node.Tag as DataRow;

            try
            {
                if (row.Table.Columns[propertyName] != null)
                {
                    row[propertyName] = value;
                }

                PropertyInfo property = node.GetType().GetProperty(propertyName);
                if (property != null)
                {
                    property.SetValue(node, value, null);
                }
            }
            catch (Exception ex)
            {
                row.RejectChanges();
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }

            if (row.Table.Columns[this.nodeTextField] != null)
            {
                try
                {
                    node.GetType().GetProperty("Text").SetValue(node, row[this.nodeTextField], null);
                }
                catch (Exception ex)
                {
                    this.hasErrors = true;
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            this.hasChanges = true;
        }

        public bool CheckTableStructure(DataTable table)
        {
            if (table == null) return false;


            if ((table.Columns[this.primaryKeys.ID] == null)
                || (table.Columns[this.primaryKeys.ParentID] == null)
                || (table.Columns[this.primaryKeys.Sequence] == null))
            {
                return false;
            }

            if ((table.Columns[this.primaryKeys.ID].DataType != System.Type.GetType("System.String"))
                || (table.Columns[this.primaryKeys.ParentID].DataType != System.Type.GetType("System.String"))
                || ((table.Columns[this.primaryKeys.Sequence].DataType != System.Type.GetType("System.Int64"))
                    && (table.Columns[this.primaryKeys.Sequence].DataType != System.Type.GetType("System.Int32"))
                    && (table.Columns[this.primaryKeys.Sequence].DataType != System.Type.GetType("System.Int16"))))
            {
                return false;
            }

            return true;

        }

        public DataTable CreateDataSourceStructure()
        {
            this.dataSource = new DataTable("DataTreeViewConfig");

            this.dataSource.Columns.Add(this.primaryKeys.ID, System.Type.GetType("System.String"));
            this.dataSource.Columns.Add(this.primaryKeys.ParentID, System.Type.GetType("System.String"));
            this.dataSource.Columns.Add(this.primaryKeys.Sequence, System.Type.GetType("System.Int64"));
            this.dataSource.Columns.Add("Text", System.Type.GetType("System.String"));

            return this.dataSource;
        }

        public void WriteTreeRelation(string srcField, string destField, string separator)
        {
            if (this.dataSource == null)
            {
                throw new Exception("DataSource is null!");
            }

            if ((this.dataSource.Columns[srcField] == null)
                || (this.dataSource.Columns[destField] == null))
            {
                throw new Exception("invalid source field or destination field name!");
            }

            if ((this.dataSource.Columns[srcField].DataType != typeof(string))
                || (this.dataSource.Columns[destField].DataType != typeof(string)))
            {
                throw new Exception("invalid source field or destination field type!");
            }

            separator = (separator == null) ? string.Empty : separator;

            foreach (TreeNode node in this.Nodes)
            {
                this.WriteFullPathInfo(node, srcField, destField, separator);
            }
        }

        private void WriteFullPathInfo(TreeNode node, string srcField, string destField, string separator)
        {
            DataRow row = node.Tag as DataRow;
            if (node.Parent == null)
            {
                row[destField] = row[srcField];
            }
            else
            {
                DataRow parentRow = node.Parent.Tag as DataRow;
                row[destField] = (string)parentRow[destField] + separator + (string)row[srcField];
            }

            foreach (TreeNode tempNode in node.Nodes)
            {
                WriteFullPathInfo(tempNode, srcField, destField, separator);
            }
        }

        #endregion

        #region Build tree from data source

        private void LoadData()
        {
            //Clear tree
            this.Nodes.Clear();
            this.hasChanges = false;
            this.hasErrors = false;

            System.Diagnostics.Debug.Assert((this.dataSource != null), "DataSource is null!");

            if (!this.CheckTableStructure(this.dataSource))
            {
                throw new Exception("Invalid table structure!");
            }

            if (this.dataSource != null)
            {
                DataView root = this.GetRootRows(this.dataSource);
                foreach (DataRowView rowView in root)
                {
                    BullidSubNode(rowView.Row, this.Nodes);
                }
            }
        }

        private void BullidSubNode(DataRow row, TreeNodeCollection parent)
        {
            TreeNode node = this.CreateNode(row);
            parent.Add(node);

            DataView children = this.GetChildRows(row);

            foreach (DataRowView rowView in children)
            {
                BullidSubNode(rowView.Row, node.Nodes);
            }
        }

        private TreeNode CreateNode(DataRow row)
        {
            TreeNode node = new TreeNode();
            node.Tag = row;

            //Read data
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                string colName = row.Table.Columns[i].ColumnName;
                PropertyInfo property = node.GetType().GetProperty(colName);
                if (property != null)
                {
                    try
                    {
                        property.SetValue(node, row[i], null);
                    }
                    catch (Exception ex)
                    {
                        this.hasErrors = true;
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
            }

            if (row.Table.Columns[this.nodeTextField] != null)
            {
                try
                {
                    node.GetType().GetProperty("Text").SetValue(node, row[this.nodeTextField], null);
                }
                catch (Exception ex)
                {
                    this.hasErrors = true;
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            return node;
        }

        private void AcceptChangeToDataSource(TreeNodeCollection nodeCol)
        {
            for (int i = 0; i < nodeCol.Count; i++)
            {
                TreeNode node = nodeCol[i];

                System.Diagnostics.Debug.Assert((node.Tag != null), "Tag is null!");
                System.Diagnostics.Debug.Assert((node.Tag is DataRow), "Invalid Tag type!");

                DataRow row = node.Tag as DataRow;
                DataRow parentRow = (node.Parent == null) ? null : nodeCol[i].Parent.Tag as DataRow;

                //Build filiation
                object parentId = (parentRow != null) ? parentRow[this.primaryKeys.ID] : row[this.primaryKeys.ID];
                row[this.primaryKeys.ParentID] = parentId;
                row[this.primaryKeys.Sequence] = node.Index;

                //Write extend propery
                WriteRowProperty(node, row);
                AcceptChangeToDataSource(node.Nodes);
            }
        }

        private void WriteRowProperty(TreeNode node, DataRow row)
        {
            for (int j = 0; j < row.Table.Columns.Count; j++)
            {
                string colName = row.Table.Columns[j].ColumnName;
                PropertyInfo property = node.GetType().GetProperty(colName);
                if (property != null)
                {
                    try
                    {
                        row[colName] = property.GetValue(node, null);
                    }
                    catch (Exception ex)
                    {
                        this.hasErrors = true;
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
            }

            if (row.Table.Columns[this.nodeTextField] != null)
            {
                try
                {
                    row[this.nodeTextField] = node.GetType().GetProperty("Text").GetValue(node, null);
                }
                catch (Exception ex)
                {
                    this.hasErrors = true;
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }

        private DataView GetRootRows(DataTable table)
        {
            string whereClause = this.primaryKeys.ParentID + " = " + this.primaryKeys.ID;

            DataView root = new DataView(this.dataSource, whereClause,
                this.primaryKeys.Sequence, DataViewRowState.CurrentRows);

            return root;
        }

        private DataView GetChildRows(DataRow row)
        {
            string whereClause = this.primaryKeys.ParentID + " = '" + row[this.primaryKeys.ID]
                + "' and " + this.primaryKeys.ParentID + " <> " + this.primaryKeys.ID;

            DataView children = new DataView(this.dataSource, whereClause,
                this.primaryKeys.Sequence, DataViewRowState.CurrentRows);

            return children;
        }

        internal void ResetTablePK()
        {
            try
            {
                if (this.dataSource.PrimaryKey != this.tablePK)
                {
                    if (this.tablePK != null)
                    {
                        foreach (DataColumn col in this.tablePK)
                        {
                            col.AllowDBNull = false;
                        }
                    }

                    this.dataSource.PrimaryKey = this.tablePK;
                }
            }
            catch (Exception ex)
            {
                this.dataSource.PrimaryKey = new DataColumn[] { this.dataSource.Columns[this.primaryKeys.ID] };
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
            this.tablePK = null;
        }

        internal void CancelTablePK()
        {
            //Record and Cancel the table primary keys
            if (this.dataSource.PrimaryKey != this.tablePK)
            {
                this.tablePK = this.dataSource.PrimaryKey;
                this.dataSource.PrimaryKey = new DataColumn[] { this.dataSource.Columns[this.primaryKeys.ID] };

                if (this.tablePK != null)
                {
                    foreach (DataColumn col in this.tablePK)
                    {
                        col.AllowDBNull = true;
                    }
                }
            }
        }

        #endregion

        #region Node operate events


        private void ListenNodeOpEvents()
        {
            this.BeforeSubNodesDeleted += new TreeViewAdv.BeforeSubNodesDeletedHandle(this.DataTreeView_BeforeSubNodesDeleted);
            this.AfterNodeAdded += new TreeViewAdv.AfterNodeAddedHandle(this.DataTreeView_AfterNodeAdded);
            this.BeforeNodeAdded += new TreeViewAdv.BeforeNodeAddedHandle(this.DataTreeView_BeforeNodeAdded);
            this.AfterNodeDeleted += new TreeViewAdv.AfterNodeDeletedHandle(this.DataTreeView_AfterNodeDeleted);
        }

        private void DataTreeView_BeforeNodeAdded(object sender, TreeViewEventArgs e)
        {
            if ((this.GetNodeCount(true) == 0) && (this.dataSource == null))
            {
                this.CreateDataSourceStructure();
            }
            this.hasChanges = true;
        }

        private void DataTreeView_AfterNodeAdded(object sender, TreeViewEventArgs e)
        {
            if (this.dataSource != null)
            {
                this.CancelTablePK();

                DataRow row = this.dataSource.NewRow();
                row[this.primaryKeys.ID] = Guid.NewGuid().ToString();

                //DataRow pRow = (e.Node.Parent == null) ? e.Node.Tag as DataRow : e.Node.Parent.Tag as DataRow;
                //row[this.primaryKeys.ParentID] = pRow[this.primaryKeys.ID];
                //row[this.primaryKeys.Sequence] = e.Node.Index;

                WriteRowProperty(e.Node, row);
                try
                {
                    this.dataSource.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    TreeNodeCollection nodeCol = (e.Node.Parent == null) ? this.Nodes : e.Node.Parent.Nodes;
                    nodeCol.Remove(e.Node);

                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    MessageBox.Show("添加节点时发生错误！\n详细信息：" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                e.Node.Tag = row;
            }

            this.hasChanges = true;
        }

        private void DataTreeView_AfterNodeDeleted(object sender, TreeViewEventArgs e)
        {
            if (this.dataSource != null)
            {
                System.Diagnostics.Debug.Assert((e.Node.Tag is DataRow), "Invalid Tag type!");

                //Delete children
                this.DeleteChildrenData(e.Node.Nodes, false);

                //Delete self
                DataRow row = e.Node.Tag as DataRow;
                row.Delete();
            }

            this.hasChanges = true;
        }

        private void DataTreeView_BeforeSubNodesDeleted(object sender, TreeViewChildrenDelEventArgs e)
        {
            this.DeleteChildrenData(e.Nodes, false);

            this.hasChanges = true;
        }

        private void DeleteChildrenData(TreeNodeCollection delNodes, bool cut)
        {
            for (int i = 0; i < delNodes.Count; i++)
            {
                DataRow row = delNodes[i].Tag as DataRow;
                if (row != null)
                {
                    if (cut)
                    {
                        delNodes[i].Tag = this.CloneRow(row);
                    }
                    row.Delete();
                }
                DeleteChildrenData(delNodes[i].Nodes, cut);
            }
        }

        private void DeleteNodeData(TreeNode node, bool cut)
        {
            DataRow row = node.Tag as DataRow;
            if (row != null)
            {
                if (cut)
                {
                    node.Tag = this.CloneRow(row);
                }
                row.Delete();
            }
            foreach (TreeNode temp in node.Nodes)
            {
                DeleteNodeData(temp, cut);
            }
        }

        #endregion

        #region Extend property menu

        private List<TreeNode> nodeClipBoard = null;

        private int pasteMenuIndex = -1;
        private void InitExtendMenu()
        {
            //Create node clip board
            this.nodeClipBoard = new List<TreeNode>();

            //Add separator item
            this.opContextMenu.Items.Add(new ToolStripSeparator());

            //Add copy menu item
            ToolStripMenuItem menuItem = new ToolStripMenuItem("复制");
            menuItem.DropDownItems.Add("当前节点", null, new EventHandler(this.MenuItem_CopyNodes));
            menuItem.DropDownItems.Add("子节点", null, new EventHandler(this.MenuItem_CopyChildNodes));
            this.opContextMenu.Items.Add(menuItem);

            //Add cut menu item
            menuItem = new ToolStripMenuItem("剪切");
            menuItem.DropDownItems.Add("当前节点", null, new EventHandler(this.MenuItem_CutNodes));
            menuItem.DropDownItems.Add("子节点", null, new EventHandler(this.MenuItem_CutChildNodes));
            this.opContextMenu.Items.Add(menuItem);

            //Add paste menu item
            menuItem = new ToolStripMenuItem("粘贴");
            menuItem.DropDownItems.Add("当前节点", null, new EventHandler(this.MenuItem_PasteNodes));
            menuItem.DropDownItems.Add("子节点", null, new EventHandler(this.MenuItem_PasteChildNodes));
            this.pasteMenuIndex = this.opContextMenu.Items.Add(menuItem);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataTreeView_MouseDown);
        }

        private void MenuItem_CopyNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            //Copy to clip board
            this.nodeClipBoard.Clear();
            this.nodeClipBoard.Add(this.CloneDataNode(this.opNode));
        }

        private void MenuItem_CopyChildNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            //Copy to clip board
            this.nodeClipBoard.Clear();
            foreach (TreeNode node in this.opNode.Nodes)
            {
                this.nodeClipBoard.Add(this.CloneDataNode(node));
            }
        }

        private void MenuItem_CutNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            //Cut to clip board
            this.nodeClipBoard.Clear();
            this.nodeClipBoard.Add(this.opNode);

            //Remove cuted nodes
            DeleteNodeData(this.opNode, true);
            this.opNode.Nodes.Remove(this.opNode);
        }

        private void MenuItem_CutChildNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            //Cut to clip board
            this.nodeClipBoard.Clear();
            foreach (TreeNode node in this.opNode.Nodes)
            {
                this.nodeClipBoard.Add(node);
            }

            //Remove cuted nodes
            this.DeleteChildrenData(this.opNode.Nodes, true);
            this.opNode.Nodes.Clear();
        }

        private void MenuItem_PasteNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            if (this.nodeClipBoard.Count > 0)
            {
                //Paste nodes
                TreeNodeCollection instNodeCol = (this.opNode.Parent == null) ? this.Nodes : this.opNode.Parent.Nodes;
                int startIndex = this.opNode.Index;
                for (int i = 0; i < this.nodeClipBoard.Count; i++)
                {
                    this.CopyRowData(this.nodeClipBoard[i]);
                    instNodeCol.Insert(startIndex + i + 1, this.nodeClipBoard[i]);
                }
                this.nodeClipBoard.Clear();
            }
        }

        private void MenuItem_PasteChildNodes(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            if (this.nodeClipBoard.Count > 0)
            {
                //Paste nodes
                foreach (TreeNode node in this.nodeClipBoard)
                {
                    this.CopyRowData(node);
                    this.opNode.Nodes.Add(node);
                }
                this.nodeClipBoard.Clear();

                this.opNode.Expand();
            }
        }

        private void DataTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (this.opContextMenu.Visible == true))
            {
                if (this.nodeClipBoard.Count > 0)
                {
                    this.opContextMenu.Items[this.pasteMenuIndex].Enabled = true;
                }
                else
                {
                    this.opContextMenu.Items[this.pasteMenuIndex].Enabled = false;
                }
            }
        }


        //private TreeNode CopyNode(TreeNode srcNode)
        //{
        //    TreeNode copyNode = this.CloneDataNode(srcNode);
        //    foreach (TreeNode node in srcNode.Nodes)
        //    {
        //        TreeNode tempNode = this.CopyNode(node);
        //        copyNode.Nodes.Add(tempNode);
        //    }
        //    return copyNode;
        //}

        private TreeNode CloneDataNode(TreeNode srcNode)
        {
            TreeNode cloneNode = srcNode.Clone() as TreeNode;
            System.Diagnostics.Debug.Assert((cloneNode != null), "Method TreeNode.Clone() failed!");

            return cloneNode;
        }

        private void CopyRowData(TreeNode cloneNode)
        {
            DataRow copyRow = null;
            DataRow row = cloneNode.Tag as DataRow;
            if (row != null)
            {
                //Copy row value
                copyRow = this.CloneRow(row);

                // Change primary keys
                copyRow[this.primaryKeys.ID] = Guid.NewGuid().ToString();

                //Add copy row to data source
                this.dataSource.Rows.Add(copyRow);
            }

            // Relate to node
            cloneNode.Tag = copyRow;

            // Copy child node's row data
            foreach (TreeNode temp in cloneNode.Nodes)
            {
                CopyRowData(temp);
            }
        }

        private DataRow CloneRow(DataRow srcRow)
        {
            DataRow cloneRow = this.dataSource.NewRow();

            for (int i = 0; i < this.dataSource.Columns.Count; i++)
            {
                cloneRow[i] = srcRow[i];
            }

            return cloneRow;
        }
        #endregion
    }

    public enum DataTreeViewDataSourceType
    {
        Unknow = 0,
        DataBase,
        XML
    }

    public class DataTreeViewDataSourceChangeEventArgs : EventArgs
    {
        private DataTreeViewDataSourceType dataSourceType = DataTreeViewDataSourceType.Unknow;
        public DataTreeViewDataSourceChangeEventArgs(DataTreeViewDataSourceType dataSourceType)
            : base()
        {
            this.dataSourceType = dataSourceType;
        }

        public DataTreeViewDataSourceType DataSourceType
        {
            get { return this.dataSourceType; }
        }
    }

    public struct DataTreeViewKeys
    {
        public DataTreeViewKeys(string idName, string pIdName, string seqName)
        {
            this.ID = idName;
            this.ParentID = pIdName;
            this.Sequence = seqName;
        }

        public string ID;
        public string ParentID;
        public string Sequence;

        public static DataTreeViewKeys Default
        {
            get
            {
                DataTreeViewKeys keys = new DataTreeViewKeys();
                keys.ID = "ID";
                keys.ParentID = "ParentID";
                keys.Sequence = "Sequence";

                return keys;
            }
        }
    }
}
