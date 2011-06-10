/*
 * @Author:  yzhou
 * @Date:    2006.07.27
 * @Version: 1.0
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace KODY.WindowsUI
{
    public class TreeViewAdv : System.Windows.Forms.TreeView
    {
        public TreeViewAdv()
            : base()
        {
            this.InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            if (!this.DesignMode)
            {
                this.InitalizeDrapDrop();
                this.InitalizeNodeOP();
                this.InitalizeCheckBoxsRelation();
            }
        }

        #region Extend properties

        private bool allowReorder = false;
        public bool AllowReorder
        {
            get { return this.allowReorder; }
            set { this.allowReorder = value; }
        }

        private bool showPopupMenu = false;
        public bool ShowPopupMenu
        {
            get { return this.showPopupMenu; }
            set { this.showPopupMenu = value; }
        }

        private bool relationCheckBoxs = false;
        public bool RelationCheckBoxs
        {
            get { return this.relationCheckBoxs; }
            set { this.relationCheckBoxs = value; }
        }

        public ArrayList GetLeafNodes(bool isChecked)
        {
            ArrayList result = new ArrayList();
            foreach (TreeNode node in this.Nodes)
            {
                this.FindLeafNodes(node, isChecked, result);
            }
            return result;
        }

        private void FindLeafNodes(TreeNode pNode, bool isChecked, ArrayList result)
        {
            if (pNode.Nodes.Count == 0)
            {
                if ((isChecked))
                {
                    if (pNode.Checked)
                    {
                        result.Add(pNode);
                    }
                }
                else
                {
                    result.Add(pNode);
                }
                return;
            }

            foreach (TreeNode node in pNode.Nodes)
            {
                FindLeafNodes(node, isChecked, result);
            }
        }
        #endregion

        #region Extend events

        public delegate void AfterNodeDragDropHandle(object sender, TreeViewDragDropEventArgs e);
        public event AfterNodeDragDropHandle AfterNodeDragDrop = null;

        public delegate void AfterNodeAddedHandle(object sender, TreeViewEventArgs e);
        public event AfterNodeAddedHandle AfterNodeAdded = null;

        public delegate void BeforeNodeAddedHandle(object sender, TreeViewEventArgs e);
        public event BeforeNodeAddedHandle BeforeNodeAdded = null;

        public delegate void AfterNodeDeletedHandle(object sender, TreeViewEventArgs e);
        public event AfterNodeDeletedHandle AfterNodeDeleted = null;

        public delegate void BeforeNodeDeletedHandle(object sender, TreeViewEventArgs e);
        public event BeforeNodeDeletedHandle BeforeNodeDeleted = null;

        public delegate void BeforeSubNodesDeletedHandle(object sender, TreeViewChildrenDelEventArgs e);
        public event BeforeSubNodesDeletedHandle BeforeSubNodesDeleted = null;
        #endregion

        #region Drag and drop support

        private TreeNode dragNode = null; // Node being dragged
        private TreeNode tempDropNode = null; // Temporary drop node for selection
        private Timer timer = null; // Timer for scrolling
        private ImageList imageListDrag = null;

        private void InitalizeDrapDrop()
        {
            this.timer = new Timer();
            this.timer.Interval = 200;

            this.imageListDrag = new ImageList();
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListDrag.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;

            this.ListenDrapDropEvents();
        }

        private void ListenDrapDropEvents()
        {
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_DragOver);
            this.DragLeave += new System.EventHandler(this.TreeView_DragLeave);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.TreeView_GiveFeedback);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.timer.Tick += new EventHandler(Timer_Tick);
        }

        private void TreeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            // Get drag node and select it
            this.dragNode = (TreeNode)e.Item;
            this.SelectedNode = this.dragNode;

            // Reset image list used for drag image
            this.imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.Indent, this.dragNode.Bounds.Height);

            // Create new bitmap
            // This bitmap will contain the tree node image to be dragged
            Bitmap bmp = new Bitmap(this.dragNode.Bounds.Width + this.Indent, this.dragNode.Bounds.Height);

            // Get graphics from bitmap
            Graphics gfx = Graphics.FromImage(bmp);

            // Draw node icon into the bitmap
            if (this.ImageList != null)
            {
                int imgIndex = (this.dragNode.ImageIndex > -1) ? this.dragNode.ImageIndex : this.ImageIndex;
                if ((imgIndex > -1) && (imgIndex < this.ImageList.Images.Count))
                {
                    gfx.DrawImage(this.ImageList.Images[imgIndex], 0, 0);
                }
            }

            // Draw node label into bitmap
            gfx.DrawString(this.dragNode.Text,
                this.Font,
                new SolidBrush(this.ForeColor),
                (float)this.Indent, 1.0f);

            // Add bitmap to imagelist
            this.imageListDrag.Images.Add(bmp);

            // Get mouse position in client coordinates
            Point p = this.PointToClient(Control.MousePosition);

            // Compute delta between mouse position and node bounds
            int dx = p.X + this.Indent - this.dragNode.Bounds.Left - this.Left;
            int dy = p.Y - this.dragNode.Bounds.Top - this.Top;

            // Begin dragging image
            if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
            {
                // Begin dragging
                this.DoDragDrop(bmp, DragDropEffects.Move);
                // End dragging image
                DragHelper.ImageList_EndDrag();
            }

        }

        private void TreeView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Compute drag position and move image
            Point formP = this.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(formP.X - this.Left, formP.Y - this.Top);

            // Get actual drop node
            TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(e.X, e.Y)));
            if (dropNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;

            // if mouse is on a new node select it
            if (this.tempDropNode != dropNode)
            {
                DragHelper.ImageList_DragShowNolock(false);
                this.SelectedNode = dropNode;
                DragHelper.ImageList_DragShowNolock(true);
                tempDropNode = dropNode;
            }

            // Avoid that drop node is child of drag node 
            TreeNode tmpNode = dropNode;
            while (tmpNode.Parent != null)
            {
                if (tmpNode.Parent == this.dragNode) e.Effect = DragDropEffects.None;
                tmpNode = tmpNode.Parent;
            }
        }

        private void TreeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Unlock updates
            DragHelper.ImageList_DragLeave(this.Handle);

            // Get drop node
            TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(e.X, e.Y)));

            // If drop node isn't equal to drag node, add drag node as child of drop node
            if (this.dragNode != dropNode)
            {
                TreeViewDragDropEventArgs eArgs = new TreeViewDragDropEventArgs();
                eArgs.Node = this.dragNode;
                eArgs.PreviousParent = this.dragNode.Parent;

                // Remove drag node from parent
                if (this.dragNode.Parent == null)
                {
                    this.Nodes.Remove(this.dragNode);
                }
                else
                {
                    this.dragNode.Parent.Nodes.Remove(this.dragNode);
                }

                // Add drag node to drop node
                dropNode.Nodes.Add(this.dragNode);
                dropNode.Expand();
                this.SelectedNode = this.dragNode;

                // Set drag node to null
                this.dragNode = null;

                // Disable scroll timer
                this.timer.Enabled = false;

                // Raise node drag and drop event
                if (this.AfterNodeDragDrop != null)
                {
                    this.AfterNodeDragDrop(this, eArgs);
                }
            }
        }

        private void TreeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragHelper.ImageList_DragEnter(this.Handle, e.X - this.Left,
                e.Y - this.Top);

            // Enable timer for scrolling dragged item
            this.timer.Enabled = true;
        }

        private void TreeView_DragLeave(object sender, System.EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.Handle);

            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
        }

        private void TreeView_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                e.UseDefaultCursors = false;
                this.Cursor = Cursors.Default;
            }
            else e.UseDefaultCursors = true;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // get node at mouse position
            Point pt = PointToClient(Control.MousePosition);
            TreeNode node = this.GetNodeAt(pt);

            if (node == null) return;

            // if mouse is near to the top, scroll up
            if (pt.Y < 30)
            {
                // set actual node to the upper one
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    // hide drag image
                    DragHelper.ImageList_DragShowNolock(false);
                    // scroll and refresh
                    node.EnsureVisible();
                    this.Refresh();
                    // show drag image
                    DragHelper.ImageList_DragShowNolock(true);

                }
            }
            // if mouse is near to the bottom, scroll down
            else if (pt.Y > this.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }
        #endregion

        #region Node operation

        public ContextMenuStrip opContextMenu = null;  //Operation context menu
        protected TreeNode opNode = null; // node for operate

        private void InitalizeNodeOP()
        {
            // Create popup menu
            this.opContextMenu = new ContextMenuStrip();

            // Add popup menu item
            this.opContextMenu.Items.Add("展开", null, new EventHandler(this.MenuItem_NodeExpandAll));
            this.opContextMenu.Items.Add("折叠", null, new EventHandler(this.MenuItem_NodeCollapseAll));
            this.opContextMenu.Items.Add(new ToolStripSeparator());
            this.opContextMenu.Items.Add("添加", null, new EventHandler(this.MenuItem_NodeAddSibling));
            this.opContextMenu.Items.Add("删除", null, new EventHandler(this.MenuItem_NodeDelete));
            this.opContextMenu.Items.Add("添加子节点", null, new EventHandler(this.MenuItem_NodeAddChildren));
            this.opContextMenu.Items.Add("删除子节点", null, new EventHandler(this.MenuItem_NodeDeleteChildren));

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyDown);
        }

        private void MenuItem_NodeExpandAll(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");
            this.opNode.ExpandAll();
        }

        private void MenuItem_NodeCollapseAll(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");
            this.opNode.Collapse(false);
        }

        private void MenuItem_NodeAddChildren(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            TreeNode node = new TreeNode("节点 " + this.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }

            this.opNode.Nodes.Insert(0, node);
            this.opNode.Expand();
            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void MenuItem_NodeDeleteChildren(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            if (this.BeforeSubNodesDeleted != null)
            {
                this.BeforeSubNodesDeleted(this, new TreeViewChildrenDelEventArgs(this.opNode, this.opNode.Nodes));
            }

            this.opNode.Nodes.Clear();
        }

        private void MenuItem_NodeAddSibling(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            TreeNode node = new TreeNode("节点 " + this.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }

            TreeNodeCollection nodes = (opNode.Parent != null) ? opNode.Parent.Nodes : this.Nodes;
            nodes.Insert(this.opNode.Index + 1, node);

            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void MenuItem_NodeDelete(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert((this.opNode != null), "Operation node is null!");

            if (this.BeforeNodeDeleted != null)
            {
                this.BeforeNodeDeleted(this, new TreeViewEventArgs(this.opNode));
            }

            TreeNodeCollection nodeCol = (this.opNode.Parent != null) ? this.opNode.Parent.Nodes : this.Nodes;
            nodeCol.Remove(this.opNode);

            if (this.AfterNodeDeleted != null)
            {
                this.AfterNodeDeleted(this, new TreeViewEventArgs(this.opNode));
            }

            this.opNode = null;
        }

        private void MenuItem_AddRootNode(object sender, System.EventArgs e)
        {

            TreeNode node = new TreeNode("节点 " + this.GetNodeCount(true).ToString());
            if (this.BeforeNodeAdded != null)
            {
                this.BeforeNodeAdded(this, new TreeViewEventArgs(node));
            }

            this.Nodes.Add(node);
            if (this.AfterNodeAdded != null)
            {
                this.AfterNodeAdded(this, new TreeViewEventArgs(node));
            }
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if ((this.showPopupMenu)
                && (e.Button == MouseButtons.Right))
            {
                if (this.GetNodeCount(true) > 0)
                {
                    this.opNode = this.GetNodeAt(new Point(e.X, e.Y));
                    if (this.opNode != null)
                    {
                        this.SelectedNode = this.opNode;
                        this.opContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                }
                else
                {
                    ContextMenuStrip tempMenu = new ContextMenuStrip();
                    tempMenu.Items.Add("添加节点", null, new EventHandler(this.MenuItem_AddRootNode));
                    tempMenu.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.allowReorder) return;

            if ((!e.Control) || (this.SelectedNode == null))
            {
                return;
            }

            TreeNode node = this.SelectedNode;
            TreeNodeCollection nodeCol = (this.SelectedNode.Parent == null) ? this.Nodes
                : this.SelectedNode.Parent.Nodes;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        int index = node.Index;
                        if (index > 0)
                        {
                            nodeCol.Remove(node);
                            nodeCol.Insert(index - 1, node);
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        int index = node.Index;
                        if (index < nodeCol.Count - 1)
                        {
                            nodeCol.Remove(node);
                            nodeCol.Insert(index + 1, node);
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (node.Parent != null)
                        {
                            TreeNode parent = node.Parent;
                            nodeCol.Remove(node);

                            TreeNodeCollection tempNodes = (parent.Parent == null) ? this.Nodes : parent.Parent.Nodes;
                            tempNodes.Insert(parent.Index + 1, node);

                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (node.PrevNode != null)
                        {
                            TreeNode previousNode = node.PrevNode;
                            nodeCol.Remove(node);
                            previousNode.Nodes.Add(node);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.SelectedNode = node;
        }

        #endregion

        #region Relate nodes check box

        private bool checkedEventSwitchOn = true;

        private void InitalizeCheckBoxsRelation()
        {
            this.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
        }

        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            System.Diagnostics.Debug.Assert(this.CheckBoxes, "Item checkBoxes is unallowed!");

            if ((this.RelationCheckBoxs) && (this.checkedEventSwitchOn))
            {
                try
                {
                    this.checkedEventSwitchOn = false;
                    this.RalateForefather(e.Node);
                    this.RelateChildren(e.Node);
                }
                finally
                {
                    this.checkedEventSwitchOn = true;
                }
            }
        }

        private void RalateForefather(TreeNode node)
        {
            bool stop = false;
            while (node.Parent != null)
            {
                if (!node.Checked)
                {
                    for (TreeNode tempNd = node.NextNode; tempNd != null; tempNd = tempNd.NextNode)
                    {
                        if (tempNd.Checked)
                        {
                            stop = true;
                            break;
                        }
                    }

                    if (!stop)
                    {
                        for (TreeNode tempNd = node.PrevNode; tempNd != null; tempNd = tempNd.PrevNode)
                        {
                            if (tempNd.Checked)
                            {
                                stop = true;
                                break;
                            }
                        }
                    }
                }

                if (stop)
                {
                    break;
                }
                else
                {
                    node.Parent.Checked = node.Checked;
                    node = node.Parent;
                }
            }
        }

        private void RelateChildren(TreeNode node)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = node.Checked;
                RelateChildren(node.Nodes[i]);
            }
        }
        #endregion
    }

    #region Extend events define

    public class TreeViewDragDropEventArgs : EventArgs
    {

        #region Member

        private TreeNode node = null;
        private TreeNode previousParent = null;
        #endregion

        public TreeViewDragDropEventArgs()
            : base()
        {
        }

        public TreeViewDragDropEventArgs(TreeNode node, TreeNode previousParent)
            : base()
        {
            this.node = node;
            this.previousParent = previousParent;
        }

        public TreeNode Node
        {
            get { return this.node; }
            set { this.node = value; }
        }

        public TreeNode PreviousParent
        {
            get { return this.previousParent; }
            set { this.previousParent = value; }
        }
    }

    public class TreeViewChildrenDelEventArgs : EventArgs
    {
        #region Member

        private TreeNode node = null;
        private TreeNodeCollection nodes = null;
        #endregion

        public TreeViewChildrenDelEventArgs()
            : base()
        { }

        public TreeViewChildrenDelEventArgs(TreeNode node, TreeNodeCollection nodes)
            : base()
        {
            this.node = node;
            this.nodes = nodes;
        }

        public TreeNode Node
        {
            get { return this.node; }
            set { this.node = value; }
        }

        public TreeNodeCollection Nodes
        {
            get { return this.nodes; }
            set { this.nodes = value; }
        }
    }
    #endregion

    public class DragHelper
    {
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int
            iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr hwndLock);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragShowNolock(bool fShow);

        static DragHelper()
        {
            InitCommonControls();
        }
    }
}
