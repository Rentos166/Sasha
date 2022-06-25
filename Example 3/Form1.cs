using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Example_3
{
    public partial class Form1 : Form
    {
        //String[] St;
        //Класс List<string> - коллекция строк -   лист из строк имеет методы 
        //накопления и преобразования неопределенного количества элементов 
        List<string> list_st = new List<string>();
        string[] St;
        Double[,] MatrD, MatrA, MatrB, MatrBT, MatrC, Matr1, Matr2, Matr3, Matr4;
        int i = 0, j = 0, n = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Матрица A
            dataGridView1.Columns.Clear();
            Sc(ref MatrA);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Матрица B
            dataGridView1.Columns.Clear();
            Sc(ref MatrB);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Матрица C
            dataGridView1.Columns.Clear();
            Sc(ref MatrC);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //Матрица D=(A+B)*C
            if ((dataGridView1.Columns.Count < 1) || MatrA == null || MatrB == null || MatrC == null) return;
            dataGridView1.Columns.Clear();
            //Добавление первой колонки 
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
            titleColumn.HeaderText = "Titlt" + 1.ToString();
            titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(titleColumn);
            for (int i = 0; i < n - 1; i++)
            {
                // Добавление колонки
                DataGridViewTextBoxColumn titleColumn1 = new DataGridViewTextBoxColumn();
                titleColumn1.HeaderText = "Titlt" + (i + 2).ToString();
                titleColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns.Add(titleColumn1);
            }
            MatrD = new Double[n, n];
            Matr1 = new Double[n, n];
            Matr2 = new Double[n, n];
            MatrBT = new Double[n, n];
            for (i = 0; i < n; i++)
            {
                dataGridView1.Rows.Add();
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                        Matr1[i, j] = MatrA[i, k] * MatrB[k, j];
                }
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    MatrBT[j, i] = MatrB[i, j];
                }
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                        Matr2[i, j] = MatrC[i, k] - MatrBT[k,j];
                }
            }
            
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    MatrD[i, j] = 0;
                        MatrD[i, j] = MatrD[i, j] + Matr1[i, j] + Matr2[i, j];
                    dataGridView1.Rows[i].Cells[j].Value = MatrD[i, j];
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Заполнение второй таблицы            
            if (dataGridView1.Columns.Count < 1) return;
            dataGridView2.Columns.Clear();
            //Добавление первой колонки и первой строки
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
            titleColumn.HeaderText = "Titlt" + 1.ToString();
            titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns.Add(titleColumn);
            dataGridView2.Rows.Add();
            for (int i = 0; i < n - 1; i++)
            {
                // Добавление колонки
                DataGridViewTextBoxColumn titleColumn1 = new DataGridViewTextBoxColumn();
                titleColumn1.HeaderText = "Titlt" + (i + 2).ToString();
                titleColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView2.Columns.Add(titleColumn1);
                // добавление строки
                dataGridView2.Rows.Add();
            }
            // Создание матрицы из второй таблицы
            Matr3 = new Double[n, n];
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = dataGridView1.Rows[i].Cells[j].Value;
                    Matr3[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //Вычисление определителя
            textBox1.Clear();
            double S = Opred(Matr3, n);
            textBox1.Text = "Определитель равен  " + Convert.ToString(S);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Процедура считывания строк из файла
        private void Sc(ref Double[,] Matr)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream FS = openFileDialog1.OpenFile() as FileStream;
                StreamReader SR = new StreamReader(FS);
                dataGridView1.Rows.Clear();
                //Добавление первой колонки 
                DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
                //Изменение заголовка колонки
                titleColumn.HeaderText = "Titlt" + 1.ToString();
                //Выравнивание ширины колонки в соответствии с заданным стилем - AllCells
                titleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns.Add(titleColumn);
                list_st.Clear();
                while (SR.Peek() > 0)
                {
                    // Считывание строки и добавление колонки (свойство AllowUserToAddRows - False)
                    list_st.Add(SR.ReadLine());
                    //dataGridView1
                    dataGridView1.Rows.Add();
                }
                St = list_st.ToArray();
                n = St.Length;
                Matr = new Double[n, n];
                for (i = 0; i < n - 1; i++)
                {
                    // Добавление колонки
                    DataGridViewTextBoxColumn titleColumn1 = new DataGridViewTextBoxColumn();
                    titleColumn1.HeaderText = "Titlt" + (i + 2).ToString();
                    //Выравнивание ширины колонки в соответствии с заданным стилем - AllCells
                    titleColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns.Add(titleColumn1);
                }
                for (i = 0; i < n; i++)
                {
                    St[i] = St[i].Replace(".", ","); ;
                    //Разбиение строки St  на подстроки
                    string[] aa = St[i].Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (j = 0; j < aa.Length; j++)
                    {
                        //Заполнение матрицы и таблицы
                        Matr[i, j] = Convert.ToDouble(aa[j]);
                        dataGridView1.Rows[i].Cells[j].Value = Matr[i, j];
                    }
                }
                SR.Close();
                FS.Close();
            }
        }
        //Функция вычисления определителя
        double Opred(double[,] M, int m)
        {
            if (M == null)
                return 0;
            Double[,] M2;
            M2 = new double[m - 1, m - 1];
            int i1 = 0, j1 = 0, k = 0, i = 0, j = 0;
            double Opr = 0;
            //Тривиальное решение
            if (m == 1)
            {
                return Opr = M[0, 0];
            }
            for (k = 0; k < m; k++)
            {
                //Разложение по элементам нулевой строки
                i1 = 0;
                for (i = 0; i < m; i++)
                {
                    j1 = 0;
                    if (i != 0)   //Вычеркивание нулевой строки
                    {
                        for (j = 0; j < m; j++)
                        {
                            if (j != k)    //Вычеркивание k-го столбца
                            {
                                M2[i1, j1] = M[i, j];
                                j1++;
                            }
                        }
                        i1++;
                    }
                }
                // Рекурсивное обращение
                Opr = Opr + Math.Pow(-1, k) * M[0, k] * Opred(M2, m - 1);
            }
            return Opr;
        }
    }
}
