using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void ValueTV_TextChanged(object sender, EventArgs e)
        {
            if (ValueTB.Text == "" || ValueTB.Text == "0")
            {
                ValueTB.Text = "1";
            }
            if (CheckIfTextisDigits(ValueTB.Text) == false)
            {
                ValueTB.Text = RemoveChars();
            }
            else if (Convert.ToInt32(ValueTB.Text) < 0)
            {
                ValueTB.Text = RemoveChars();
            }

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            string value = ValueTB.Text;
            using(StreamWriter sw = new StreamWriter("Graph Data\\temp.txt"))
            {
                sw.Write(value);
            }
            Close();
        }
        private string RemoveChars()
        {
            string value = "";
            for (int i = 0; i < ValueTB.Text.Length; i++)
            {
                if (CheckIfTextisDigits($"{ValueTB.Text[i]}") == false)
                {
                    continue;
                }
                value += ValueTB.Text[i];
            }
            return value;
        }
        private bool CheckIfTextisDigits(string text)
        {
            try
            {
                Convert.ToInt32(text);
            }
            catch (FormatException)
            {
                Console.WriteLine("false");
                return false;
            }
            Console.WriteLine("true");
            return true;
        }
    }
}
