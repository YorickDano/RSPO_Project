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
    public partial class Overlord : Form
    { //Данный метод отвечает за создание окна
        public Overlord()
        {
            InitializeComponent();
        } //Данный метод отвечает за настройку лабелов
        protected Label changing_label(Label l)
        {
            l.BackColor = SystemColors.Window;
            l.ForeColor = Color.Black;
            l.Font = new Font("Comic Sans MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.BorderStyle = BorderStyle.FixedSingle;
            l.Size = new Size(100, 25);
          
            return l;
        }
        //Данный метод отвечает за отображения анимешек
        private void Overlord_Load(object sender, EventArgs e)
        {
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM overlord", bd.getConn());

            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
            DataTable table = new DataTable();

            string name = "", img_link = "";
            bd.openConn();

            MySqlDataReader reader = commandOut.ExecuteReader();


            while (reader.Read())
            {
                img_link += Convert.ToString(reader.GetValue(1)) + "`";
                name += Convert.ToString(reader.GetValue(0)) + "`";
            }

            string[] words_names = name.Split('`');
            string[] words_img = img_link.Split('`');


            bd.closeConn();
            da.Fill(table);


            for (int h = 0, xl = 65, yl = 170, xp = 50, yp = 10; h < table.Rows.Count; ++h, xp += 200)
            {

                Label ni = new Label();
                PictureBox pi = new PictureBox();
                pi.Size = new Size(150, 150);
                pi.SizeMode = PictureBoxSizeMode.StretchImage;

                if (xp > 1000) { xp = 50; yp += 200; }
                if (xl > 1000) { xl = 40; yl += 205; }
                pi.Location = new Point(xp, yp); pi.Show();
                xl = pi.Location.X + 25;
                yl = pi.Location.Y + 160;
                ni.Location = new Point(xl, yl); ni.Show();

                changing_label(ni);
                ni.Text = words_names[h];
                pi.Image = Image.FromFile(words_img[h]);
                pi.Name = words_names[h];




                this.Controls.Add(pi);
                this.Controls.Add(ni);



            }

        }
        //Данный метод отвечает за переход на глав меню
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Home h = new Home();
            h.Show();
        }
        Point lp = new Point();
        //Данный метод отвечает за перемещение окна
        private void Overlord_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещения окна
        private void Overlord_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
    }
}
