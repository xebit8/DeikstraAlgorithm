using System.Windows.Forms;


namespace DeikstraAlgorithm
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    // Неориентированный, взвешенный граф без отрицательных рёбер
    public class Graph
    {
        public List<Node> nodes_list = new List<Node>();
        public List<Edge> edges_list = new List<Edge>();
        public Node AddNode(string name, int x, int y)
        {
            Node node = new Node();
            node.ID = nodes_list.Count;
            nodes_list.Add(node);
            node.name = name;
            node.x = x;
            node.y = y;
            return node;
        }
        public Edge AddEdge(int value, Node node1, Node node2)
        {
            Edge edge = new Edge();
            edge.node1 = node1;
            edge.node2 = node2;
            edge.value = value;
            edge.ID = edges_list.Count;
            edges_list.Add(edge);
            return edge;
        }
        private void GraphFiller()
        {
            string[] lines = File.ReadAllLines("Graph Data\\connections.txt");
            string[] names = File.ReadAllLines("Graph Data\\names.txt");
            int n = names.Length;
            // Заполнение массива координат вершин
            using (StreamReader sr = new StreamReader("Graph Data\\coordinates.txt"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    AddNode(names[nodes_list.Count], Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
                }
            }
            Dictionary<(Node, Node), int> ribs = new Dictionary<(Node, Node), int>();
            // Строки вида
            // номер строки в файле = ID_вершины1, в строке:
            // ID_вершины:Вес_Ребра,ID_Вершины:Вес_Ребра,...
            for (int i = 0; i < n; i++)
            {
                if (lines[i] == "-") continue;
                int ribNode1_ID = i;
                string[] valueAndNode = lines[i].Split(',');
                for (int j = 0; j < valueAndNode.Length; j++)
                {
                    int ribNode2_ID = Convert.ToInt32(valueAndNode[j].Split(':')[0]);
                    int ribValue = Convert.ToInt32(valueAndNode[j].Split(':')[1]);
                    ribs.Add((nodes_list[ribNode1_ID], nodes_list[ribNode2_ID]), ribValue);
                }
            }
            foreach ((Node, Node) ribNodes in ribs.Keys)
            {
                Node node1, node2;
                (node1, node2) = ribNodes;
                AddEdge(ribs[ribNodes], node1, node2);
            }
        }
        public (double, string) DeikstraAlgorithm(int n1, int n2)
        {
            GraphFiller();
            List<string> node_path = new List<string>();
            double distance = 0;
            Node active_node = nodes_list[0];
            Dictionary<Node, List<Node>> node_conns = new Dictionary<Node, List<Node>>();
            for (int i = n1; i < n2 + 1; i++)
            {
                node_conns.Add(nodes_list[i], new List<Node>());
                if (i == n1)
                {
                    nodes_list[i].min_distance = 0;
                    nodes_list[i].status = "Проверено";
                }
                else
                {
                    nodes_list[i].min_distance = double.PositiveInfinity;
                    nodes_list[i].status = "Не проверено";
                }
                nodes_list[i].previous = null;
            }
            for (int i = n1; i < n2 + 1; i++)
            {
                foreach (Edge edge in edges_list)
                {
                    if (active_node == edge.node1)
                    {
                        if (edge.node2.status == "Не проверено")
                        {
                            Console.WriteLine($"расчет {active_node.name}->{edge.node2.name}...");
                            node_conns[active_node].Add(edge.node2);
                            if (edge.node2.min_distance == double.PositiveInfinity)
                            {
                                edge.node2.min_distance = active_node.min_distance + edge.value;
                                edge.node2.previous = active_node;
                            }
                            else
                            {
                                if (active_node.min_distance + edge.value < edge.node2.min_distance)
                                {
                                    edge.node2.min_distance = active_node.min_distance + edge.value;
                                    edge.node2.previous = active_node;
                                }
                            }
                        }
                    }
                    else if (active_node == edge.node2)
                    {
                        if (edge.node1.status == "Не проверено")
                        {
                            Console.WriteLine($"расчет {active_node.name}->{edge.node1.name}...");
                            node_conns[active_node].Add(edge.node1);
                            if (edge.node1.min_distance == double.PositiveInfinity)
                            {
                                edge.node1.min_distance = active_node.min_distance + edge.value;
                                edge.node1.previous = active_node;
                            }
                            else
                            {
                                if (active_node.min_distance + edge.value < edge.node1.min_distance)
                                {
                                    edge.node1.min_distance = active_node.min_distance + edge.value;
                                    edge.node1.previous = active_node;
                                }
                            }
                        }
                    }
                }
                Node key = active_node;
                double min = node_conns[key][0].min_distance;
                Node min_dist_node = node_conns[key][0]; // вершина с минимальной дистанцией
                for (int j = 0; j < node_conns[key].Count; j++)
                {
                    if (node_conns[key][j].min_distance < min)
                    {
                        min = node_conns[key][j].min_distance;
                        min_dist_node = node_conns[key][j];
                    }
                    if (j == node_conns[key].Count - 1)
                    {
                        min_dist_node.status = "Проверено";
                        active_node = min_dist_node;
                    }
                }
                if (active_node.ID == n2) break;
            }
            Console.WriteLine($"nodes: {node_conns.Count}");
            foreach (Node key in node_conns.Keys)
            {
                Console.WriteLine($"parent: {key.name}:");
                foreach (Node value in node_conns[key])
                {
                    Console.WriteLine($"-> child: {value.name}");
                }
            }
            foreach (Node node in nodes_list)
            {
                Console.WriteLine($"{node.name} | {node.min_distance}");
            }
            Node last_node = active_node;
            while (last_node != null)
            {
                node_path.Add(last_node.name);
                last_node = last_node.previous;
            }
            node_path.Reverse();
            return (active_node.min_distance, String.Join("->", node_path.ToArray()));
        }
    }
    public class Node
    {
        public int ID;
        public double min_distance;
        public string name;
        public string status = "Не проверено"; // Проверено или не проверено
        public Node previous; // предыдшествующая вершина графа
        public int x;
        public int y;
        public void node_info()
        {
            Console.WriteLine();
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"name: {name}");
            Console.WriteLine($"min_distance: {min_distance}");
            Console.WriteLine($"status: {status}");
            Console.WriteLine($"Position: ({x},{y})");
        }
    }
    public class Edge
    {
        public int ID;
        public int value;
        public Node node1;
        public Node node2;
        public void edge_info()
        {
            Console.WriteLine();
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"value: {value}");
            Console.WriteLine($"node1.ID: {node1.ID}");
            Console.WriteLine($"node2.ID: {node2.ID}");
        }
    }
}