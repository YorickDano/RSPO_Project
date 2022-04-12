using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace trpo
{
    public partial class Add : Form
    {
        Home h = new Home();
        public DataTable table = new DataTable();
        //Данный метод отвечает за создание окна
        public Add()
        {
            InitializeComponent();
            textBox2.Hide();
        }
        //Данный метод отвечает за переход на главное меню
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
           
            h.Show();
        }

        OpenFileDialog que = new OpenFileDialog();

        //Данный метод отвечает за добавления анимэшки в базуданых
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || pictureBox1.Image == null) MessageBox.Show("Something is hollow");
            else
            {
                var bd = new DataBase();
                var command = new MySqlCommand($"INSERT INTO `goods`(`name`, `counts`, `img`, `group`) VALUES (@name, @counts, @img, @group)", bd.getConn());
                var commandTraps = new MySqlCommand("INSERT INTO `traps`(`name`, `img`) VALUES (@name, @img)", bd.getConn());
                var comKonosuba = new MySqlCommand("INSERT INTO `konosuba`(`name`, `img`, `num`) VALUES (@name, @img, @num)", bd.getConn());
                var comOverlord = new MySqlCommand("INSERT INTO `overlord`(`name`, `img`) VALUES (@name, @img)", bd.getConn());
                var com1000 = new MySqlCommand("INSERT INTO `1000-7=?`(`name`, `img`) VALUES (@name, @img)", bd.getConn());

                if (cheak()) { return; }
                switch (comboBox1.Text) 
                {
                    case "Traps":
                    {
                        commandTraps.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                        commandTraps.Parameters.Add("@img", MySqlDbType.VarChar).Value = textBox2.Text;
                        bd.openConn();
                        if (commandTraps.ExecuteNonQuery() == 1)
                            MessageBox.Show("Added");
                        else
                            MessageBox.Show("net");
                        bd.closeConn();
                                break;
                    }
                    case "Konosuba":
                    {
                        comKonosuba.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                        comKonosuba.Parameters.Add("@img", MySqlDbType.VarChar).Value = textBox2.Text;
                        comKonosuba.Parameters.Add("@num", MySqlDbType.VarChar).Value = textBox3.Text;
                        bd.openConn();
                        if (comKonosuba.ExecuteNonQuery() == 1)
                            MessageBox.Show("Added");

                        else
                            MessageBox.Show("net");
                        bd.closeConn();
                                break;
                    }
                    case "Overlord":
                    {
                        comOverlord.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                        comOverlord.Parameters.Add("@img", MySqlDbType.VarChar).Value = textBox2.Text;
                        bd.openConn();
                        if (comOverlord.ExecuteNonQuery() == 1)
                            MessageBox.Show("Added");

                        else
                            MessageBox.Show("net");
                        bd.closeConn();
                                break;
                    }
                    case "1000-7=?":
                    {
                        com1000.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                        com1000.Parameters.Add("@img", MySqlDbType.VarChar).Value = textBox2.Text;
                        bd.openConn();
                        if (com1000.ExecuteNonQuery() == 1)
                            MessageBox.Show("Added");

                        else
                            MessageBox.Show("net");
                        bd.closeConn();
                                break;
                    }
                    default:
                    {
                        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                        command.Parameters.Add("@counts", MySqlDbType.VarChar).Value = textBox3.Text;
                        command.Parameters.Add("@img", MySqlDbType.VarChar).Value = textBox2.Text;
                        command.Parameters.Add("@group", MySqlDbType.VarChar).Value = comboBox1.Text;
                        bd.openConn();
                        if (command.ExecuteNonQuery() == 1)
                            MessageBox.Show("Added");

                        else
                            MessageBox.Show("net");
                        bd.closeConn();
                                break;
                    }

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                pictureBox1.Image = null;

            }
        }
    }


        //Данный метод отвечает за проверку наличия объекта в базеданных
        public Boolean cheak()
        {
            DataBase bd = new DataBase();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `goods` WHERE name = @name", bd.getConn());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable t = new DataTable();
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(t);
            if (t.Rows.Count > 0) { MessageBox.Show("Enter another name"); return true; }
            else
                return false;
        }
        //Данный метод отвечает за загрузку изображения в окно
        private void button2_Click(object sender, EventArgs e)
        {
            que.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (que.ShowDialog() == DialogResult.OK) 
            { 
                pictureBox1.Image = Image.FromFile(que.FileName); 
                textBox2.Text = que.FileName; 
            }
        }
        //Данный метод отвечает за отображения графического интерфейса при загрзке окна
        private void Form1_Load(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            if (db.getConn() == null)
            {
                MessageBox.Show("NO connection");
                this.Close();
                h.Show();
                return;

            }
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods", db.getConn());

            string long_str = "";

            db.openConn();
            MySqlDataReader reader = commandOut.ExecuteReader();

            while (reader.Read()) { long_str += reader.GetValue(4) + "`"; }
            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
            string[] words_group = long_str.Split('`');
            string[] words_group_fixed = words_group.Distinct().ToArray();
            db.closeConn();
            da.Fill(table);
            db.openConn();
            comboBox1.Items.Clear();

            comboBox1.Items.Add("Traps");
            comboBox1.Items.Add("Konosuba");
            comboBox1.Items.Add("Overlord");
            comboBox1.Items.Add("1000-7=?");
            for (int i = 0; i < words_group_fixed.Length; ++i)
{
                if (words_group_fixed[i] == "") continue;
                comboBox1.Items.Add(words_group_fixed[i]);
                
            }
           

        }
        Point lp;
        //Данный метод отвечает за перемещения окна
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещения окна
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
        //Данный метод отвечает за логику при внесении изменений в поле текстового объекта техтбох3
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
