using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace trpo
{
    public partial class Delete : Form
    {
        Home h = new Home();
        //Данный метод отвечает за создание окна
        public Delete()
        {
            InitializeComponent();
        }
        //Данный метод отвечает за удаленя выбранного объекта
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") MessageBox.Show("Choose something");
            else
            {
                string name = comboBox1.Text;
                DataBase bd = new DataBase();
                MySqlCommand comm = new MySqlCommand("DELETE from goods WHERE name = '" + name + "'", bd.getConn());
                bd.openConn();
                comm.ExecuteNonQuery();
                bd.closeConn();
                comboBox1.Text = "";
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                MessageBox.Show("Item was deleted");
            }
        }
        //Данный метод отвечает за отображения при запуске окна
        private void delete_Load(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            if (db.getConn() == (null))
            {
                MessageBox.Show("NO connection");
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
            db.closeConn();
           
            for (byte i = 0; i < name_words.Length-1; ++i)
                comboBox1.Items.Add(name_words[i]);

        }
        Point lp;


        //Данный метод отвечает за перемещение окна
        private void Delete_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещения окна
        private void Delete_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
        //Данный метод отвечает за переход на глав меню
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
           
            h.Show();
        }
    }
}
