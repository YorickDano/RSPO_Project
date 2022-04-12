using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trpo;
using MySql.Data.MySqlClient;
namespace trpo
{
    public partial class Home : Form
    { //Данный метод отвечает за создание окна
        public Home()
        {

            InitializeComponent();

        }


        //Данный метод отвечает за открытия окна добавления
        void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            Add add = new Add();
            add.Show();
          
        }

        //Данный метод отвечает за закрытие окна
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }


        //Данный метод отвечает за настройку отобраения выхода
        private void exit_MouseEnter(object sender, EventArgs e)
        {
            exit.ForeColor = Color.White;
            exit.BackColor = Color.FromArgb(153, 0, 0);
        }
        //Данный метод отвечает за настройкуу отображения выхода
        private void exit_MouseLeave(object sender, EventArgs e)
        {
            exit.ForeColor = Color.Black;
            exit.BackColor = Color.White;
        }
        //Данный метод отвечает за переход на окно отображения
        private void ShowBut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Show sh = new Show();
            sh.Show();

            
        }
        //Данный метод отвечает за переход на окно изменения
        private void button2_Click(object sender, EventArgs e)
        {
            Edit ed = new Edit();
            this.Hide();
            ed.Show();
        }
        //Данный метод отвечает за переход на окно трапов
        private void button3_Click(object sender, EventArgs e)
        {
            Traps t = new Traps();
            this.Hide();
            t.Show();
        }
        Point lp;
        //Данный метод отвечает за перемещения окна
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X-lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
        //Данный метод отвечает за перемещения окна
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за переход на окно удаления
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Delete del = new Delete();
            del.Show();

        }
        //Данный метод отвечает за переход на окно коносубы
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Konosuba k = new Konosuba();
            k.Show();
        }
        //Данный метод отвечает за переход на окно оверлорда
        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Overlord o = new Overlord();
            o.Show();

        }
        //Данный метод отвечает за переход на окно гулей
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ghoul g = new Ghoul();
            g.Show();
            
        }

       
    }
}
