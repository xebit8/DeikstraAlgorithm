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
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        // TODO: Имеются вершины А, Б, В, Г. При удалении серединной вершины (в данном случае, Б или В) сбивается алфавитный порядок букв и следующая созданная вершина будет иметь букву предыдущей созданной вершины. Необходимо перенести все имена вершин после удалённой вершины на 1 букву назад.
        int nodeID = -1;
        public Form2()
        {
            InitializeComponent();
            GenerateName(GetNamesQuantity());
        }
        public Form2(int node)
        {
            InitializeComponent();
            nodeID = node;
            ShowName();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (nodeID == -1) CreateName();
            else ChangeName();
            Close();
        }
        private void CreateName()
        {
            string name;
            string path = "Graph Data\\names.txt";
            int N = GetNamesQuantity();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                if (NameTB.Text == "")
                {
                    name = GenerateName(N);
                }
                else
                {
                    name = NameTB.Text;
                }
                sw.WriteLine(name);
            }
        }
        private string GenerateName(int N)
        {
            string name = "";
            char[] alphabet = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й',
                                       'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф',
                                       'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ', 'Э', 'Ю', 'Я' };
            string path = "Graph Data\\names.txt";
            if (N >= 33)
            {
                // Генерация имени
                name += alphabet[((N + 1) / 33) - 1];
                if (((N + 1) % 33) != 0) name += alphabet[((N + 1) % 33) - 1];
                else name += alphabet[32];
            }
            else name += alphabet[N];
            NameTB.Text = name;
            return name;
        }
        private void ChangeName()
        {
            string path = "Graph Data\\names.txt";
            string[] names = File.ReadAllLines(path);
            File.Delete(path);
            for (int i = 0; i<names.Length; i++)
            {
                if (i == nodeID)
                {
                    if (NameTB.Text == "")
                    {
                        names[i] = GenerateName(i);
                    }
                    else names[i] = NameTB.Text;
                }
            }
            File.WriteAllLines(path, names);
        }
        private void ShowName()
        {
            string name = File.ReadAllLines("Graph Data\\names.txt")[nodeID];
            NameTB.Text = name;
        }
        private int GetNamesQuantity()
        {
            int N;
            try
            {
                N = File.ReadAllLines("Graph Data\\names.txt").Length;
            }
            catch (FileNotFoundException)
            {
                N = 0;
            }
            return N;
        }
    }
}
