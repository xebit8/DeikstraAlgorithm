using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<(int,int)> vertices = new List<(int, int)>();
        Dictionary<(int, int), (int, int)> ribs = new Dictionary<(int, int), (int, int)>();
        // Проверка, нажата ли кнопка
        bool AddButtonClicked = false;
        bool RemoveButtonClicked = false;
        bool ConnectButtonClicked = false;
        bool StartDAButtonClicked = false;
        bool MoveButtonClicked = true;
        bool NightModeOn = false;
        bool MouseIsDown = false;
        // Проверка, выбраны ли две вершины для соединения/расчета кратчайшего пути
        int MultiClickNode1 = -2;
        int MultiClickNode2 = -2;
        int NodeClickTimesLeft = 2;
        public Form1()
        {
            InitializeComponent();
            GraphFromFiles();
        }
        private void GraphFromFiles()
        {
            if (!Directory.Exists("Graph Data"))
            {
                Directory.CreateDirectory("Graph Data");
            }
            Bitmap b0 = new Bitmap(Canvas.Width, Canvas.Height);
            using (Graphics g = Graphics.FromImage(b0))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                vertices = new List<(int, int)>();
                ribs = new Dictionary<(int, int), (int, int)>();
                string[] names = new string[] { };
                try
                {
                    names = File.ReadAllLines("Graph Data\\names.txt");
                    int n = names.Length;
                    (int, int)[] coords = new (int, int)[n]; // Для рёбер
                    int cc = 0;
                    using (StreamReader sr = new StreamReader("Graph Data\\coordinates.txt"))
                    {
                        while (sr.EndOfStream != true)
                        {
                            string[] line = sr.ReadLine().Split(' ');
                            (int, int) p = (Convert.ToInt32(line[0])-25, Convert.ToInt32(line[1])-25);
                            coords[cc] = p;
                            vertices.Add(p);
                            cc++;
                        }
                    }
                    if (n >= 2)
                    {
                        try
                        {
                            DrawEdge(g, coords, n);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            ;
                        }
                    }
                    DrawNode(g, coords, names);
                }
                catch (FileNotFoundException)
                {
                    ;
                }
                Canvas.Image = b0;
                Canvas.Refresh();
            }
        }
        private void DrawNode(Graphics g, (int, int)[] coords, string[] names)
        {
            Bitmap b1 = new Bitmap(50, 50);
            using (var g1 = Graphics.FromImage(b1))
            {
                g1.SmoothingMode = SmoothingMode.AntiAlias;
                if (NightModeOn)
                {
                    g1.FillEllipse(Brushes.DarkSlateGray, 0 + 3, 0 + 3, 50 - 7, 50 - 7);
                }
                else
                {
                    g1.FillEllipse(Brushes.LightBlue, 0 + 3, 0 + 3, 50 - 7, 50 - 7);
                }
            }
            for (int i = 0; i < coords.Length; i++)
            {
                (int node_x, int node_y) = coords[i];
                g.DrawImage(b1, new Point(node_x, node_y));
                DrawString(g, names[i], node_x + 16, node_y + 16);
            }
        }
        private void DrawEdge(Graphics g, (int, int)[] coords, int n)
        {
            Color color;
            if (NightModeOn == true)
            {
                color = Color.DarkSlateGray;
            }
            else color = Color.LightBlue;
            string[] lines = File.ReadAllLines("Graph Data\\connections.txt");
            {
                for (int i = 0; i < n; i++)
                {
                    if (lines[i] == "-") continue;
                    int ribNode1_ID = i;
                    string[] valuesAndNodes = lines[i].Split(',');
                    for (int j = 0; j < valuesAndNodes.Length; j++)
                    {
                        int ribNode2_ID = Convert.ToInt32(valuesAndNodes[j].Split(':')[0]);
                        int ribValue = Convert.ToInt32(valuesAndNodes[j].Split(':')[1]);
                        (int x1, int y1) = coords[ribNode1_ID];
                        (int x2, int y2) = coords[ribNode2_ID];
                        x1 += 25;
                        x2 += 25;
                        y1 += 25;
                        y2 += 25;
                        g.DrawLine(new Pen(color, 3), new Point(x1, y1), new Point(x2, y2));
                        // координаты центра линии, но с отступом вверх для создания надписи
                        int label_x = ((x1 + x2) / 2) - 4;
                        int label_y = ((y1 + y2) / 2) - 9;
                        ribs.Add((ribNode1_ID, ribNode2_ID), (label_x, label_y));
                        DrawString(g, ribValue.ToString(), label_x, label_y);
                    }
                }
            }
        }
        private void DrawString(Graphics g, string text, int x, int y)
        {
            Color textcolor;
            if (NightModeOn == true)
            {
                textcolor = Color.White;
            }
            else textcolor = Color.Black;
            string drawString = text;
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(textcolor);
            // Центрирование текста
            for (int i = 1; i < text.Length; i++)
            {
                x -= 4;
            }
            g.DrawString(drawString, drawFont, drawBrush, new Point(x, y));
            drawFont.Dispose();
            drawBrush.Dispose();
        }
        private void Canvas_Click(object sender, MouseEventArgs e)
        {
            int pX = e.X;
            int pY = e.Y;
            if (AddButtonClicked == true)
            {
                AddNodeForm();
                if (File.Exists("Graph Data\\names.txt"))
                {
                    // Заносим координаты новой вершины в файл
                    using (StreamWriter sw = new StreamWriter("Graph Data\\coordinates.txt", true))
                    {
                        sw.WriteLine($"{pX} {pY}");
                    }
                    // Объявляем пустой контейнер для связей с вершиной
                    using (StreamWriter sw = new StreamWriter("Graph Data\\connections.txt", true))
                    {
                        sw.WriteLine('-');
                    }
                    GraphFromFiles();
                }
            }
            else if (RemoveButtonClicked == true)
            {
                int nodeIndex = FindClosestNode(pX, pY);
                (int nodeIndex1, int nodeIndex2) = FindClosestEdge(pX, pY);
                if (nodeIndex != -1 && nodeIndex1 == -1 && nodeIndex2 == -1)
                {
                    // TODO: При удалении вершины визуально скрывает одно/несколько соединений в графе, в файле соединения остаются
                    RemoveNodePosition(nodeIndex);
                    RemoveNodeName(nodeIndex);
                    RemoveNodeConnections(nodeIndex);
                }
                else if (nodeIndex == -1 && nodeIndex1 != -1 && nodeIndex2 != -1)
                {
                    RemoveEdge(nodeIndex1, nodeIndex2);
                }
                GraphFromFiles();
            }
            else if (ConnectButtonClicked == true)
            {
                if (NodeClickTimesLeft == 2)
                {
                    int index1 = FindClosestNode(pX, pY);
                    if (index1 != -1)
                    {
                        MultiClickNode1 = index1;
                        NodeClickTimesLeft--;
                    }
                }
                else if (NodeClickTimesLeft == 1)
                {
                    int index2 = FindClosestNode(pX, pY);
                    if (index2 != -1)
                    {
                        MultiClickNode2 = index2;
                        NodeClickTimesLeft--;
                    }
                }
                if (NodeClickTimesLeft == 0 && MultiClickNode1 != -1 && MultiClickNode2 != -1 && MultiClickNode1 != MultiClickNode2)
                {
                    ConnectNodesForm();
                    if (File.Exists("Graph Data\\temp.txt"))
                    {
                        foreach ((int, int) key in ribs.Keys)
                        {
                            if (key == (MultiClickNode1, MultiClickNode2) || key == (MultiClickNode2, MultiClickNode1))
                            {
                                RemoveEdge(MultiClickNode1, MultiClickNode2);
                            }
                        }
                        ConnectNodes(MultiClickNode1, MultiClickNode2);
                        GraphFromFiles();
                    }
                    MultiClickNode1 = -1;
                    MultiClickNode2 = -1;
                    NodeClickTimesLeft = 2;
                }
            }
            else if (StartDAButtonClicked == true)
            {
                if (NodeClickTimesLeft == 2)
                {
                    int index1 = FindClosestNode(pX, pY);
                    if (index1 != -1)
                    {
                        MultiClickNode1 = index1;
                    }
                }
                if (NodeClickTimesLeft == 1)
                {
                    int index2 = FindClosestNode(pX, pY);
                    if (index2 != -1)
                    {
                        MultiClickNode2 = index2;
                    }
                }
                NodeClickTimesLeft--;
                if (NodeClickTimesLeft == 0)
                {
                    Graph g = new Graph();
                    (double value, string path) = g.DeikstraAlgorithm(MultiClickNode1, MultiClickNode2);
                    DialogResult mes = MessageBox.Show($"Вес пути = {value}, полный путь: {path}", "Результат", MessageBoxButtons.OK);
                    MultiClickNode1 = -1;
                    MultiClickNode2 = -1;
                    NodeClickTimesLeft = 2;
                }
            }
        }
        private void AddNodeForm()
        {
            Form2 NodeNameForm = new Form2();
            NodeNameForm.ShowDialog();
        }
        private void ChangeNodeForm(int node)
        {
            Form2 NodeNameForm = new Form2(node);
            NodeNameForm.ShowDialog();
        }
        private void ConnectNodesForm()
        {
            Form3 EdgeValueForm = new Form3();
            EdgeValueForm.ShowDialog();
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (FindClosestNode(e.X, e.Y) != -1)
            {
                MouseIsDown = true;
            }
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown == true && MoveButtonClicked == true)
            {
                int index = FindClosestNode(e.X, e.Y);
                // Изменение координат
                string coords_path = "Graph Data\\coordinates.txt";
                string[] coords = File.ReadAllLines(coords_path);
                using (StreamWriter sw = new StreamWriter(coords_path, false))
                {
                    for (int i = 0; i < coords.Length; i++)
                    {
                        if (i == index)
                        {
                            coords[i] = $"{e.X-25} {e.Y-25}";
                        }
                        sw.WriteLine(coords[i]);
                    }
                }
                GraphFromFiles();
            }
        }
        private void Canvas_DoubleMouseClick(object sender, MouseEventArgs e)
        {
            if (MoveButtonClicked == true)
            {
                int nodeIndex = FindClosestNode(e.X, e.Y);
                (int nodeIndex1, int nodeIndex2) = FindClosestEdge(e.X, e.Y);
                if (nodeIndex != -1 && nodeIndex1 == -1 && nodeIndex2 == -1)
                {
                    ChangeNodeForm(nodeIndex);
                }
                else if (nodeIndex == -1 && nodeIndex1 != -1 && nodeIndex2 != -1)
                {
                    (int index1, int index2) = FindClosestEdge(e.X, e.Y);
                    ConnectNodesForm();
                    RemoveEdge(index1, index2);
                    ConnectNodes(index1, index2);
                }
                GraphFromFiles();
            }
        }
        private void CancelAllButtons()
        {
            AddButtonClicked = false;
            RemoveButtonClicked = false;
            ConnectButtonClicked = false;
            StartDAButtonClicked = false;
            MoveButtonClicked = false;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            AddButtonClicked = true;
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            RemoveButtonClicked = true;
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            ConnectButtonClicked = true;
        }
        private void StartDAButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            StartDAButtonClicked = true;
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            File.Delete("Graph Data\\connections.txt");
            File.Delete("Graph Data\\names.txt");
            File.Delete("Graph Data\\coordinates.txt");
            Directory.Delete("Graph Data");
            vertices = new List<(int, int)>();
            ribs = new Dictionary<(int, int), (int, int)>();
            GraphFromFiles();
        }
        private void MoveButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            MoveButtonClicked = true;
        }
        private void ChangeThemeButton_Click(object sender, EventArgs e)
        {
            CancelAllButtons();
            ChangeCanvasTheme();
            GraphFromFiles();
        }
        private void ChangeCanvasTheme()
        {
            if (NightModeOn == false)
            {
                Canvas.BackColor = Color.Black;
                NightModeOn = true;
            }
            else
            {
                Canvas.BackColor = Color.White;
                NightModeOn = false;
            }
        }
        private int FindClosestNode(int x, int y)
        {
            if (vertices.Count == 0 && AddButtonClicked == true)
            {
                return 0;
            }
            for (int i = 0; i < vertices.Count; i++)
            {
                (int lx, int ly) = vertices[i];
                int hx = lx + 50;
                int hy = ly + 50;
                if (lx <= x && x <= hx)
                {
                    if (ly <= y && y <= hy)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        private (int,int) FindClosestEdge(int x, int y)
        {
            foreach ((int, int) key in ribs.Keys)
            {
                (int rx, int ry) = ribs[key];
                rx += 4;
                ry += 9;
                int lx = rx - 5;
                int ly = ry - 5;
                int hx = rx + 5;
                int hy = ry + 5;
                if (lx <= x && x <= hx)
                {
                    if (ly <= y && y <= hy)
                    {
                        return key;
                    }
                }
            }
            return (-1, -1);
        }
        private void RemoveNodePosition(int index)
        {
            // Удаление координат
            if (File.Exists("Graph Data\\names.txt"))
            {
                string coords_path = @"Graph Data\coordinates.txt";
                string[] coords = File.ReadAllLines(coords_path);
                File.Delete(@"Graph Data\coordinates.txt");
                if (coords.Length != 1)
                {
                    using (StreamWriter sw = new StreamWriter(coords_path, false))
                    {
                        for (int i = 0; i < coords.Length; i++)
                        {
                            if (i == index)
                            {
                                continue;
                            }
                            sw.WriteLine(coords[i]);
                        }
                    }
                }
            }
            
        }
        private void RemoveNodeName(int index)
        {
            // Удаление имени
            string names_path = "Graph Data\\names.txt";
            string[] names = File.ReadAllLines(names_path);
            File.Delete("Graph Data\\names.txt");
            if (names.Length != 1)
            {
                using (StreamWriter sw = new StreamWriter(names_path, false))
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (i == index)
                        {
                            continue;
                        }
                        sw.WriteLine(names[i]);
                    }
                }
            }
        }
        private void RemoveNodeConnections(int index)
        {
            // Удаление связей с другими вершинами
            string conns_path = "Graph Data\\connections.txt";
            string[] lines = File.ReadAllLines(conns_path);
            File.Delete("Graph Data\\connections.txt");
            if (lines.Length != 1)
            {
                using (StreamWriter sw = new StreamWriter(conns_path, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i == index)
                        {
                            continue;
                        }
                        if (lines[i] == "-")
                        {
                            sw.WriteLine("-");
                            continue;
                        }
                        string[] valuesAndNodes = lines[i].Split(',');
                        string newVAN = "";
                        for (int j = 0; j < valuesAndNodes.Length; j++)
                        {
                            int ribNode2_ID = Convert.ToInt32(valuesAndNodes[j].Split(':')[0]);
                            if (ribNode2_ID == index)
                            {
                                continue;
                            }
                            if (newVAN == "") newVAN += valuesAndNodes[j];
                            else newVAN += ',' + valuesAndNodes[j];
                        }
                        if (newVAN == "") newVAN = "-";
                        sw.WriteLine(newVAN);
                    }
                }
            }
        }
        private void RemoveEdge(int node1, int node2)
        {
            // Удаление связей с другими вершинами
            string conns_path = "Graph Data\\connections.txt";
            string[] lines = File.ReadAllLines(conns_path);
            File.Delete("Graph Data\\connections.txt");
            if (node2 < node1)
            {
                int buf = node1;
                node1 = node2;
                node2 = buf;
            }
            if (lines.Length != 2)
            {
                using (StreamWriter sw = new StreamWriter(conns_path, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] == "-")
                        {
                            sw.WriteLine("-");
                            continue;
                        }
                        string newVAN = "";
                        string[] valuesAndNodes = lines[i].Split(',');
                        for (int j = 0; j < valuesAndNodes.Length; j++)
                        {
                            int ribNode2_ID = Convert.ToInt32(valuesAndNodes[j].Split(':')[0]);
                            if (ribNode2_ID == node2 && i == node1)
                            {
                                continue;
                            }
                            if (newVAN == "") newVAN += valuesAndNodes[j];
                            else newVAN += ',' + valuesAndNodes[j];
                        }
                        if (newVAN == "") newVAN = "-";
                        sw.WriteLine(newVAN);
                    }
                }
            }
            else File.WriteAllLines(conns_path, new string[] { "-", "-" });
        }
        private void ConnectNodes(int node1, int node2)
        {
            string temp_path = "Graph Data\\temp.txt";
            int value = Convert.ToInt32(File.ReadAllText(temp_path));
            File.Delete(temp_path);
            string graph_path = "Graph Data\\connections.txt";
            string[] lines = File.ReadAllLines(graph_path);
            File.Delete(graph_path);
            if (node1 > node2)
            {
                if (lines[node2] == "-") lines[node2] = $"{node1}:{value}";
                else lines[node2] += ',' + $"{node1}:{value}";
            }
            else if (node1 < node2)
            {
                if (lines[node1] == "-") lines[node1] = $"{node2}:{value}";
                else lines[node1] += ',' + $"{node2}:{value}";
            }
            File.WriteAllLines(graph_path, lines);
        }
    }
}