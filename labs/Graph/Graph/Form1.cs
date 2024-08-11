using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Соотношение числа мужчин и женщин.";
            textBox2.Text = "Средний доход.";
            textBox3.Text = "Средний рейтинг трат.";
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();

                var reader = new StreamReader(@"C:/Программы/Mall_Customers.csv");
                {
                    string[] headers = reader.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(',');
                        dataTable.Rows.Add(rows);
                    }
                }

                int maleCount = 0;
                int femaleCount = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    string gender = row["Genre"].ToString().ToLower();
                    if (gender == "male")
                        maleCount++;
                    else if (gender == "female")
                        femaleCount++;
                }

                chart1.Series.Clear();

                chart1.Series.Add("Genre");

                chart1.Series["Genre"].Points.AddXY("Мужчина", maleCount);
                chart1.Series["Genre"].Points.AddXY("Женщина", femaleCount);

                chart1.Series["Genre"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных и построении диаграммы: " + ex.Message);
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (var reader = new StreamReader(@"C:/Программы/Mall_Customers.csv"))
                {
                    string[] headers = reader.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(',');
                        dataTable.Rows.Add(rows);
                    }
                }

                
                Dictionary<string, double> averageIncomeByAgeGroup = new Dictionary<string, double>();

                
                int startAge = 15;
                int endAge = 20;
                while (endAge <= 100) 
                {
                    double totalIncome = 0;
                    int count = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        int age = Convert.ToInt32(row["Age"]);
                        double income = Convert.ToDouble(row["Annual Income (k$)"]);

                        if (age >= startAge && age <= endAge)
                        {
                            totalIncome += income;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        double averageIncome = totalIncome / count;
                        string ageGroup = startAge.ToString() + "-" + endAge.ToString();
                        averageIncomeByAgeGroup.Add(ageGroup, averageIncome);
                    }

                    startAge += 5;
                    endAge += 5;
                }

                chart2.Series.Clear();
                chart2.Series.Add("Средний доход");
                chart2.Series["Средний доход"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                foreach (var item in averageIncomeByAgeGroup)
                {
                    chart2.Series["Средний доход"].Points.AddXY(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при построении гистограммы: " + ex.Message);
            }
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (var reader = new StreamReader(@"C:/Программы/Mall_Customers.csv"))
                {
                    string[] headers = reader.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(',');
                        dataTable.Rows.Add(rows);
                    }
                }

                Dictionary<string, double> averageSpendingRatingByAgeGroup = new Dictionary<string, double>();

                int startAge = 15;
                int endAge = 20;
                while (endAge <= 100) 
                {
                    double totalSpendingRating = 0;
                    int count = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        int age = Convert.ToInt32(row["Age"]);
                        double spendingRating = Convert.ToDouble(row["Spending Score (1-100)"]);

                        if (age >= startAge && age <= endAge)
                        {
                            totalSpendingRating += spendingRating;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        double averageSpendingRating = totalSpendingRating / count;
                        string ageGroup = startAge.ToString() + "-" + endAge.ToString();
                        averageSpendingRatingByAgeGroup.Add(ageGroup, averageSpendingRating);
                    }

                    startAge += 5;
                    endAge += 5;
                }

                chart3.Series.Clear();
                chart3.Series.Add("Средний рейтинг трат");
                chart3.Series["Средний рейтинг трат"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                foreach (var item in averageSpendingRatingByAgeGroup)
                {
                    chart3.Series["Средний рейтинг трат"].Points.AddXY(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при построении графика: " + ex.Message);
            }
        }
    }
}
