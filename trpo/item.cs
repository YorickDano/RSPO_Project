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
    public partial class item : Form
    {


       public string name = "", about ="", img ="", group = "";
        //Данный метод отвечает за создание окна
        public item()
        {
            InitializeComponent();
        }
        Point lp;
        //Данный метод отвечает за перемещение окна
        private void item_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
        //Данный метод отвечает за перемещение окна
        private void item_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }


        //Данный метод отвечает за закрузку интерфеса при запуске окна
        private void item_Load(object sender, EventArgs e)
        {
          
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = @name", bd.getConn());

            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
            DataTable table = new DataTable();
            commandOut.Parameters.AddWithValue("@name", this.name);
         
            label1.Text = this.name;
            pictureBox1.Image = Image.FromFile(this.img);
            label2.Text = about;
            label3.Text = group;
        }
        //Данный метод отвечает за переход на глав меню
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Show s = new Show();
            
            s.Show();
        }
    }
}
