using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Note_it
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
          (
          int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse
          );

        public Form1()
        {
            InitializeComponent();
           
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            panel1.Hide();

             if (File.Exists("C:\\demo\\notes.odt"))
              {
                  string text = File.ReadAllText("C:\\demo\\notes.odt");
                  textbox.Text = text;
              }

              else textbox.Text = "";
             

        }
        Point lastPoint;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (panel1.Visible==false)
            { 
                panel1.Show(); 
                stgbtn.Image = Note_it.Properties.Resources.Cancel;
                stgbtn.ImageSize = new Size(10,10);
            }
            else if(panel1.Visible==true)
            {
                panel1.Hide(); stgbtn.Image = Note_it.Properties.Resources.Settings;
                stgbtn.ImageSize = new Size(20, 20);
            }
        }

        private void stgbtn_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            string dot;
            dot = " • ";
                    
              textbox.Text += dot;
            
            

            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
           
            Font SelectedText_Font = textbox.SelectionFont;
            if (SelectedText_Font != null)
            {

                textbox.SelectionFont = new Font(SelectedText_Font,
                  SelectedText_Font.Style ^ FontStyle.Bold);
            }

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            
            Font SelectedText_Font = textbox.SelectionFont;
            if (SelectedText_Font != null)
                textbox.SelectionFont = new Font(SelectedText_Font,
                  SelectedText_Font.Style ^ FontStyle.Underline);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Font SelectedText_Font = textbox.SelectionFont;
            if (SelectedText_Font != null)
                textbox.SelectionFont = new Font(SelectedText_Font,
                  SelectedText_Font.Style ^ FontStyle.Italic);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        { 
             TextWriter txt = new StreamWriter("C:\\demo\\notes.odt");
            
            txt.WriteAsync(textbox.Text);
            txt.Close();
        }
    }
}
