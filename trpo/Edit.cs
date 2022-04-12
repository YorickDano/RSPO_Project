using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using MySql.Data.MySqlClient;
namespace trpo
{
    public partial class Edit : Form
    {
    
        DataTable table = new DataTable();
        //Данный метод отвечает за создание окна
        public Edit()
        {
            InitializeComponent();
        }
        //Данный метод отвечает за переход на главное менб
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
          
            Home h = new Home();
     
            h.Show();
        }

        //Данный метод отвечает за переход на окно внесения изменений
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string name = comboBox1.Text, about = "", img_link = "",group ="";
                DataBase bd = new DataBase();
                MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = @name", bd.getConn());

                MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
                commandOut.Parameters.AddWithValue("@name", name);
                bd.openConn();

                MySqlDataReader reader = commandOut.ExecuteReader();


                while (reader.Read())
                {
                     
                    img_link += Convert.ToString(reader.GetValue(3));
                    about += Convert.ToString(reader.GetValue(2));
                    group += Convert.ToString(reader.GetValue(4));

                }
                Changing c = new Changing();
                c.name = name;
                c.descriptiion = about;
                c.img_link = img_link;
                c.group = group;
                this.Close();
                c.Show();
            }
            else
                MessageBox.Show("Choose something");
        }
        //Данный метод отвечает за загкзку отображения при загрузки окна
        private void Edit_Load(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            if (db.getConn() == (null))
            {
                MessageBox.Show("NO connection");

                Home h = new Home();
                this.Close();
                h.Show();
                return;

            }
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods", db.getConn());

            string long_str_names = "";

            db.openConn();
            MySqlDataReader reader = commandOut.ExecuteReader();

            while (reader.Read())
            {

                long_str_names += reader.GetValue(1) + "`";
   
            }

            string[] name_words = long_str_names.Split('`');
            
            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);

            db.closeConn();
            da.Fill(table);
            for (int i = 0; i < table.Rows.Count; ++i)
                comboBox1.Items.Add(name_words[i]);
            

        }
        Point lp;
        //Данный метод отвечает за перемещения окна
        private void Edit_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещения окна
        private void Edit_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
    }
}
