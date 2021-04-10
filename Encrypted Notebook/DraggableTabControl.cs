using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DraggableTabControl
{
    [ToolboxBitmap(typeof(DraggableTabControl))]
    /// 
    /// Summary description for DraggableTabPage.
    /// 
    public class DraggableTabControl : System.Windows.Forms.TabControl
    {
        ///  
        /// Required designer variable.
        /// 
        private System.ComponentModel.Container components = null;

        public DraggableTabControl()
        {
            AllowDrop = true;

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call

        }

        ///  
        /// Clean up any resources being used.
        /// 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        ///  
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// 
        private void InitializeComponent()
        {
        }
        #endregion

        protected override void OnDragOver(System.Windows.Forms.DragEventArgs e)
        {
            base.OnDragOver(e);

            Point pt = new Point(e.X, e.Y);
            //We need client coordinates.
            pt = PointToClient(pt);

            //Make sure there is a TabPage being dragged.
            if (e.Data.GetDataPresent(typeof(TabPage)))
            {
                //Get the tab we are dragging.
                TabPage drag_tab = (TabPage)e.Data.GetData(typeof(TabPage));

                //Get the tab we are hovering over.
                TabPage hover_tab = GetTabPageByTab(pt, drag_tab); // watch the drag_tab extra param
                                                                   //Make sure we are on a tab.
                if (hover_tab != null)
                {
                    e.Effect = DragDropEffects.Move;

                    int item_drag_index = FindIndex(drag_tab);
                    int drop_location_index = FindIndex(hover_tab);

                    //Don't do anything if we are hovering over ourself.
                    if (item_drag_index != drop_location_index)
                    {
                        this.SuspendLayout();

                        TabPages[drop_location_index] = drag_tab;
                        TabPages[item_drag_index] = hover_tab;

                        //Make sure the drag tab is selected.
                        SelectedTab = drag_tab;
                        this.ResumeLayout();
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point pt = new Point(e.X, e.Y);
            TabPage tp = GetTabPageByTab(pt);

            if (tp != null)
            {
                DoDragDrop(tp, DragDropEffects.All);
            }
        }

        /// 
        /// Finds the TabPage whose tab is contains the given point.
        /// 
        /// The point (given in client coordinates) to look for a TabPage.
        /// The TabPage whose tab is at the given point (null if there isn't one).
        private TabPage GetTabPageByTab(Point pt)
        {
            TabPage tp =

                null;
            for (int i =

                0; i < TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(pt))
                {
                    tp = TabPages[i];
                    break;
                }
            }

            return tp;
        }
        
        /// <summary>
         /// Finds the TabPage whose tab is containing the given point.
         /// When DragTab.Width is smaller than HoverTab.Width, a blind zone (HoverTab.Width - DragTab.Width) is taken into account.
         /// </summary>
         /// <param name="pt">The point (given in client coordinates) to look for a TabPage.</param>
         /// <param name="dragTab">The TabPage to drag.</param>
         /// <returns>The TabPage whose tab is at the given point (null if there isn't one).</returns>
        private TabPage GetTabPageByTab(Point pt, TabPage dragTab)
        {
            TabPage tp = null;

            // The bounding rectangle for the drag tab
            Rectangle dragTabRect = GetTabRect(FindIndex(dragTab));

            for (int i = 0; i < TabPages.Count; i++)
            {
                // The bounding rectangle for the hover tab
                Rectangle hoverTabRect = GetTabRect(i);

                // Avoid flickering when DragTab Width is smaller then HoverTab Width
                if (dragTabRect.Width < hoverTabRect.Width)
                {
                    if (dragTabRect.X < hoverTabRect.X)
                        // Hover directions is to the right
                        hoverTabRect.X += hoverTabRect.Width - dragTabRect.Width;
                    else
                        // Hover direction is to the left
                        hoverTabRect.Width -= hoverTabRect.Width - dragTabRect.Width;
                }

                // Test if point is within modified HoverTab
                if (hoverTabRect.Contains(pt))
                {
                    tp = TabPages[i];
                    break;
                }
            }

            return tp;
        }

        /// 
        /// Loops over all the TabPages to find the index of the given TabPage.
        /// 
        /// The TabPage we want the index for.
        /// The index of the given TabPage(-1 if it isn't found.)
        private int FindIndex(TabPage page)
        {
            for (int i = 0; i < TabPages.Count; i++)
            {
                if (TabPages[i] == page)
                    return i;
            }

            return -1;
        }
    }
}