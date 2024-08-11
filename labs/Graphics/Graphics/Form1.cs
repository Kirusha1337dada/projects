using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("y = sin(x)");
            listBox1.Items.Add("y = cos(x)");
            listBox1.Items.Add("y = x^2");
            listBox1.Items.Add("y = k*sin(x+b)");

            textBox1X.Text = "0";
            textBox1Y.Text = "0";

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            System.Windows.Forms.Button zoomInButton = new System.Windows.Forms.Button();
            zoomInButton.Text = "+";
            zoomInButton.Size = new Size(30, 30);
            zoomInButton.Location = new Point(300, 350); 
            zoomInButton.Click += ZoomInButton_Click; 
            
            System.Windows.Forms.Button zoomOutButton = new System.Windows.Forms.Button();
            zoomOutButton.Text = "-";
            zoomOutButton.Size = new Size(30, 30);
            zoomOutButton.Location = new Point(340, 350); 
            zoomOutButton.Click += ZoomOutButton_Click; 

            this.Controls.Add(zoomInButton);
            this.Controls.Add(zoomOutButton);
        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            double currentXMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double currentXMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double currentYMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double currentYMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

            double newXMin = Math.Round(currentXMin + (currentXMax - currentXMin) * 0.1, 2);
            double newXMax = Math.Round(currentXMax - (currentXMax - currentXMin) * 0.1, 2);
            double newYMin = Math.Round(currentYMin + (currentYMax - currentYMin) * 0.1, 2);
            double newYMax = Math.Round(currentYMax - (currentYMax - currentYMin) * 0.1, 2);

            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(newXMin, newXMax);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(newYMin, newYMax);
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            double currentXMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double currentXMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double currentYMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double currentYMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

            double newXMin = Math.Round(currentXMin - (currentXMax - currentXMin) * 0.1, 2);
            double newXMax = Math.Round(currentXMax + (currentXMax - currentXMin) * 0.1, 2);
            double newYMin = Math.Round(currentYMin - (currentYMax - currentYMin) * 0.1, 2);
            double newYMax = Math.Round(currentYMax + (currentYMax - currentYMin) * 0.1, 2);

            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(newXMin, newXMax);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(newYMin, newYMax);
        }
    

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex==-1)
            {
                return;
            }

            string line = listBox1.SelectedItem.ToString();
            double startX = double.Parse(textBox1X.Text); 
            double startY = double.Parse(textBox1Y.Text);

            double a =startX -10, b =startX + 10, h = 0.1, x, y, k = 5;

            switch(line)
            {
                case "y = sin(x)":
                    

                    this.chart1.Series[0].Points.Clear();
                    x = a;
                    while (x <= b)
                    {
                        y = Math.Sin(x);
                        this.chart1.Series[0].Points.AddXY(x, y);
                        x += h;
                    }
                    break;

                case "y = cos(x)":
                    this.chart1.Series[0].Points.Clear();
                    x = a;
                    while(x<=b)
                    {
                        y = Math.Cos(x);
                        this.chart1.Series[0].Points.AddXY(x, y);
                        x += h;
                    }
                    break;
                case "y = x^2":
                    this.chart1.Series[0].Points.Clear();
                    x = a;
                    while(x<=b)
                    {
                        y = x * x;
                        this.chart1.Series[0].Points.AddXY(x, y);
                        x += h;
                    }
                    break;
                case "y = k*sin(x+b)":
                    this.chart1.Series[0].Points.Clear();
                    x = a;
                    while(x<=b)
                    {
                        y = k * Math.Sin(x + b);
                        this.chart1.Series[0].Points.AddXY(x, y);
                        x += h;
                    }
                    break;

                default: MessageBox.Show("Ни один график не выбран!","ОШИБКА",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);break;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

        }

       
    }
}
