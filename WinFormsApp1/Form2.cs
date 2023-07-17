using System.Text.Json;

namespace DeikstraAlgorithm
{
    public partial class Form2 : Form
    {
        // TODO: Имеются вершины А, Б, В, Г. При удалении серединной вершины (в данном случае, Б или В) сбивается алфавитный порядок букв и следующая созданная вершина будет иметь букву предыдущей созданной вершины. Необходимо перенести все имена вершин после удалённой вершины на 1 букву назад.
        int nodeID = -1;
        public Dictionary<int, string> names = new Dictionary<int, string>();
        public Form2(Dictionary<int, string> names)
        {
            InitializeComponent();
            this.names = names;
            GenerateName();
        }
        public Form2(Dictionary<int, string> names, int node)
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
            int last_node_id = names.Last().Key;
            if (NameTB.Text == "")
            {
                name = GenerateName();
            }
            else
            {
                name = NameTB.Text;
            }
            names[last_node_id] = name;
        }
        private string GenerateName()
        {
            int id = names.Last().Key;
            string name = "";
            char[] alphabet = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й',
                                       'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф',
                                       'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ', 'Э', 'Ю', 'Я' };
            if (id >= 33)
            {
                // Генерация имени
                name += alphabet[((id + 1) / 33) - 1];
                if (((id + 1) % 33) != 0) name += alphabet[((id + 1) % 33) - 1];
                else name += alphabet[32];
            }
            else name += alphabet[id];
            NameTB.Text = name;
            return name;
        }
        private void ChangeName()
        {
            
            foreach(int id in names.Keys)
            {
                if (id == nodeID)
                {
                    if (NameTB.Text == "")
                    {
                        names[nodeID] = GenerateName();
                    }
                    else names[nodeID] = NameTB.Text;
                }
            }
        }
        private void ShowName()
        {
            string name = File.ReadAllLines("Graph Data\\names.txt")[nodeID];
            NameTB.Text = name;
        }
    }
}
