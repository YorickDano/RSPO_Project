using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
namespace trpo
{
    public partial class Changing : Form
    {
        Home h = new Home();
        OpenFileDialog que = new OpenFileDialog();
        //Данный метод отвечает за создание окна
        public Changing()
        {
          

            InitializeComponent();
            textBox3.Hide();
        }
       public string name, descriptiion, img_link, group;
        //Данный метод отвечает за загрузку графического интерфейса окна
        private void Changing_Load(object sender, EventArgs e)
        {
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = '"+this.name+"'", bd.getConn());
            MySqlCommand commgroup = new MySqlCommand("SELECT * FROM goods", bd.getConn());
            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
            DataTable table = new DataTable();



            string group ="";


            bd.openConn();
         
            MySqlDataReader rgr = commgroup.ExecuteReader();
            
            while(rgr.Read()) group += Convert.ToString(rgr.GetValue(4)) + "`";
            string[] words_group = group.Split('`');
            string[] words_group_fixed = words_group.Distinct().ToArray();
            
            textBox1.Text += this.name;
            textBox2.Text += descriptiion;
            pictureBox1.Image = Image.FromFile(img_link);
            textBox3.Text += img_link;
            comboBox1.Text = this.group;
            for(byte i = 0; i < words_group_fixed.Length; ++i)
            {
                if (words_group_fixed[i] == "") continue;
                comboBox1.Items.Add(words_group_fixed[i]);

            }


        }
        //Данный метод отвечает за внесения изменений в базуданных
        private void button1_Click(object sender, EventArgs e)
        {
            DataBase bd = new DataBase();
            MySqlCommand command = new MySqlCommand("UPDATE goods SET name = @name, counts = @counts, img = @img WHERE name = '"+this.name+"'", bd.getConn());


            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@counts", textBox2.Text);
            command.Parameters.AddWithValue("@img", textBox3.Text);
            bd.openConn();
            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("da");

            else
                MessageBox.Show("net");
            bd.closeConn();
            this.Close();
           
            h.Show();
        }
        Point lp;
        //Данный метод отвечает за перемещение окна
        private void Changing_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }

        //Данный метод отвечает за перемещение окна
        private void Changing_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещение окна
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Edit ed = new Edit();
            ed.Show();



        }
        //Данный метод отвечает за добавление изображения в спецальную форму
        private void add_img_Click(object sender, EventArgs e)
        {

            que.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (que.ShowDialog() == DialogResult.OK) { pictureBox1.Image = Image.FromFile(que.FileName); textBox3.Text = que.FileName; }
        }

    }
}
