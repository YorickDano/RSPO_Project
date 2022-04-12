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
    public partial class Konosuba : Form
    { //Данный метод отвечает за создание окна
        public Konosuba()
        {
            InitializeComponent();
        }
        //Данный метод отвечает за переход на главное меню
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Home h = new Home();
            h.Show();
        }
        //Данный метод отвечает за изменение лабелов
        protected Label changing_label(Label l)
        {
            l.BackColor = System.Drawing.SystemColors.Window;
            l.ForeColor = Color.Black;
            l.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            l.Size = new Size(100, 25);
            // l.AutoSize = true;
            return l;
        }
        //Данный метод отвечает за отображение фото и лавбелов при открытии окна
        private void Konosuba_Load(object sender, EventArgs e)
        {
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM konosuba", bd.getConn());

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

                if (xp > 800) { xp = 50; yp += 200; }
                if (xl > 800) { xl = 40; yl += 205; }
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
        Point lp = new Point();
        //Данный метод отвечает за перемещение окна
        private void Konosuba_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }
        //Данный метод отвечает за перемещение окна
        private void Konosuba_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
    }
}
