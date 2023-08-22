using Live_MCRC.RenderContext;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Live_MCRC
{
    public partial class Preview : Form
    {
        Dictionary<string, Action<Point>> tools = new Dictionary<string, Action<Point>>();

        public Preview()
        {
            InitializeComponent();
            Refresh();

            // add tools here

            AddTool("2D_RC_Rectangle", (pos) =>
            {
                Panel tmpCtrl = new Panel();
                tmpCtrl.BackColor = Color.Red;
                tmpCtrl.Size = new Size(25, 25);
                tmpCtrl.Location = panel1.PointToClient(pos);
                panel1.Controls.Add(tmpCtrl);
                tmpCtrl.BringToFront();
            });

            AddTool("2D_RC_TextDrawing", (pos) =>
            {
                TextDrawing tmpCtrl = new TextDrawing();
                tmpCtrl.Location = panel1.PointToClient(pos);
                tmpCtrl.Text = "2D_RC_TextDrawing";
                panel1.Controls.Add(tmpCtrl);
                tmpCtrl.BringToFront();
            });
        }

        void AddTool(string ID, Action<Point> action)
        {
            tools.Add(ID, action);

            Button tmpBtn = new Button();
            tmpBtn.Dock = DockStyle.Top;
            tmpBtn.Text = ID;
            tmpBtn.FlatStyle = FlatStyle.Popup;
            tmpBtn.MouseDown += Tool_MouseDown;

            groupBox1.Controls.Add(tmpBtn);
        }

        private void Tool_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.DoDragDrop(btn.Text, DragDropEffects.Copy);
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                // use this to do controls
                string controlType = e.Data.GetData(DataFormats.Text) as string;

                Action<Point> action;
                if (tools.TryGetValue(controlType, out action))
                    action(new Point(e.X, e.Y));
            }
        }
    }
}
