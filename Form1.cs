using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace rendercomic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Comic";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Declare the num variable
            int num = 0;

            // Check if textBox1 is empty or if it contains a number in the D4 format
            if (string.IsNullOrEmpty(textBox1.Text) || int.TryParse(textBox1.Text, out num))
            {
                // If textBox1 is empty, set it to "0001"
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1.Text = "0001";
                }
                else
                {
                    // If textBox1 contains a number in the D4 format, increase it by 1
                    num++;
                    textBox1.Text = num.ToString("D4");
                }
            }

            pictureBox1.Size = new Size(498, 416);
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Black, 4);
            graphics.DrawRectangle(pen, 8, 8, 482, 362);
            pictureBox1.Image = bitmap;
            //this.Controls.Add(pictureBox1);

            string json = File.ReadAllText("C:/Users/Admin/Documents/stupid cs thing/jframes/" + "frame-" + textBox1.Text + ".json");
            JObject data = JObject.Parse(json);

            // Load title
            label1.Text = (string)data["title"];

            // Load image
            string base64Image = (string)data["backgrounds"][0]["img"];
            base64Image = base64Image.Replace("data:image/bmp;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            MemoryStream ms = new MemoryStream(imageBytes);
            Image image = Image.FromStream(ms);

            // Resize image
            image = image.GetThumbnailImage(478, 358, null, IntPtr.Zero);

            graphics.DrawImage(image, 10, 10);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) pictureBox1.BackColor = Color.Gray;
            else if (checkBox1.Checked == false) pictureBox1.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new Bitmap with the size of the pictureBox1
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Draw the pictureBox1 and label1 onto the Bitmap
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            label1.DrawToBitmap(bmp, new Rectangle(label1.Location.X - pictureBox1.Location.X, label1.Location.Y - pictureBox1.Location.Y, label1.Width, label1.Height));
            label2.DrawToBitmap(bmp, new Rectangle(label2.Location.X - pictureBox1.Location.X, label2.Location.Y - pictureBox1.Location.Y, label2.Width, label2.Height));

            // Save the Bitmap to a PNG file
            bmp.Save("C:/Users/Admin/Documents/stupid cs thing/csframe/frame-" + textBox1.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6572; i++)
            {
                button1.PerformClick();
                button2.PerformClick();
            }
        }
    }
}