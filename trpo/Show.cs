using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace trpo
{
    public partial class Show : Form
    {
        Home h = new Home();
        bool fix = true;
        //Данный метод отвечает за создание окна
        public Show()
        {
            InitializeComponent();
        }

        //Данный метод отвечает за переход на главное меню
        private void button3_Click(object sender, EventArgs e)
        {

            this.Close();

            h.Show();
        }
        //Данный метод отвечает за настройку отображения Label
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
        //Данный метод отвечает за чтение информации из базыданных
        private static string[] fromReadertoArrayString(MySqlDataReader reader, int pos, DataBase db)
        {
            string str = "";

            while (reader.Read())
            {
                str += reader.GetValue(pos) + "~";
            }
            db.closeConn();
            return str.Split('~');
        }
        //Данный метод отвечает за создание range для excel таблицы
        //private static Excel.Range CreateRange(int num1, int num11, int num2, int num22, Excel.Application App)
        //{
        //    Excel.Worksheet Worksheet = (Excel.Worksheet)App.Worksheets.get_Item(1);

        //    Excel.Range RangeFieldFrom = Worksheet.Cells[num1, num11];
        //    Excel.Range RangeFieldTo = Worksheet.Cells[num2, num22];

        //    return App.get_Range(RangeFieldFrom, RangeFieldTo);

        //}
        //Данный метод отвечает за создание ехcel таблицы и заполнению её данными
        //private static void ExcelCreate(DataBase db, MySqlCommand commandOut)
        //{
        //    db.openConn();
        //    string[] arrayName = fromReadertoArrayString(commandOut.ExecuteReader(), 1, db);
        //    db.openConn();
        //    string[] arrayImage = fromReadertoArrayString(commandOut.ExecuteReader(), 3, db);
        //    db.openConn();
        //    string[] arrayGroup = fromReadertoArrayString(commandOut.ExecuteReader(), 4, db);


        //    Excel.Application App = new Excel.Application();

        //    App.Workbooks.Add(Type.Missing);
        //    App.DisplayAlerts = false;
        //    Excel.Worksheet Worksheet = (Excel.Worksheet)App.Worksheets.get_Item(1);
        //    Worksheet.Name = "ShowInformtion";
        //    Excel.Range RangeField = CreateRange(1, 1, arrayGroup.Length, 3, App);
        //    Excel.Range RangeTop = CreateRange(1, 1, 1, 3, App);
        //    Excel.Range RangeAll = CreateRange(1, 1, arrayGroup.Length, 3, App);
        //    RangeField.Cells[1, 1].Value = "Name";
        //    RangeField.Cells[1, 2].Value = "Link";
        //    RangeField.Cells[1, 3].Value = "Category";
        //    RangeTop.Interior.Color = ColorTranslator.ToOle(Color.Black);
        //    RangeTop.Font.Color = ColorTranslator.ToOle(Color.White);

        //    for (int indexArray = 0, i = 2; indexArray < arrayGroup.Length; ++indexArray, ++i)
        //    {
        //        for (int j = 1; j < 4; ++j)
        //        {
        //            if (j == 1)
        //                RangeField.Cells[i, j].Value = arrayName[indexArray];
        //            else if (j == 2)
        //                RangeField.Cells[i, j].Value = arrayImage[indexArray];
        //            else
        //                RangeField.Cells[i, j].Value = arrayGroup[indexArray];
        //        }
        //    }

        //    RangeAll.Borders.Color = ColorTranslator.ToOle(Color.Gray);
        //    RangeTop.Borders.Color = ColorTranslator.ToOle(Color.Black);
        //    RangeField.Font.Size = 18;
        //    RangeField.EntireColumn.AutoFit();
        //    RangeField.EntireRow.AutoFit();
        //    App.Visible = true;
        //}
        //Данный метод отвечает за отображения изображений
        private void Show_Load(object sender, EventArgs e)
        {
            DataBase DataBase = new DataBase();

            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods", DataBase.getConn());
            MySqlDataAdapter da = new MySqlDataAdapter(commandOut);
            DataTable table = new DataTable();
            // ExcelCreate(DataBase, commandOut);
            DataBase.closeConn();
            da.Fill(table);
            PictureBox[] pictureBoxArray = new PictureBox[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; ++i) pictureBoxArray[i] = new PictureBox();

            DataBase.openConn();
            MySqlDataReader reader = commandOut.ExecuteReader();
            DataBase.closeConn();
            if (fix)
            {
                fix = false;
                DataBase.openConn();
                string[] arrayName = fromReadertoArrayString(commandOut.ExecuteReader(), 1, DataBase);
                DataBase.openConn();
                string[] arrayImage = fromReadertoArrayString(commandOut.ExecuteReader(), 3, DataBase);
               


                imageList.ImageSize = new Size(150, 150);

                for (int i = 0; i < arrayName.Length - 1; ++i)
                {
                    if (arrayImage.Length <= 1) break;
                    imageList.Images.Add(Image.FromFile($@"{arrayImage[i]}"));
                    ListViewItem listViewItem = new ListViewItem(arrayName[i]);
                    listViewItem.ImageIndex = i;
                    listView.Items.Add(listViewItem);

                }
                listView.LargeImageList = imageList;

                this.Controls.Add(listView);
                var collection = new AutoCompleteStringCollection();
                collection.AddRange(arrayName);
                comboBox1.AutoCompleteCustomSource = collection;
            }
            
        }

        //Данный метод отвечает за переход на окно экспоната при нажатии на объект
        private void PictureBoxes_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            string name = pic.Name, about = "", img_link = "", group = "";
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = @name", bd.getConn());
            commandOut.Parameters.AddWithValue("@name", name);
            bd.openConn();

            MySqlDataReader reader = commandOut.ExecuteReader();


            while (reader.Read())
            {
                img_link += Convert.ToString(reader.GetValue(3));
                about += Convert.ToString(reader.GetValue(2));
                group += Convert.ToString(reader.GetValue(4));
            }
            this.Close();
            item i = new item();
            i.name = name;
            i.about = about;
            i.img = img_link;
            i.group = group;
            i.Show();
        }

        //Данный метод отвечает за переход на главное меню
        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            h.Show();
        }
        Point lp;
        //Данный метод отвечает за возможность передвигать окно
        private void Show_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lp.X;
                this.Top += e.Y - lp.Y;
            }
        }
        //Данный метод отвечает за возможность передвигать окно
        private void Show_MouseDown(object sender, MouseEventArgs e)
        {
            lp = new Point(e.X, e.Y);
        }


        //Данный метод отвечает за переход на окно экспоната
        private void listView_Click(object sender, EventArgs e)
        {
            var selectedItem = listView.SelectedItems[0];

            string name = selectedItem.Text, about = "", img_link = "", group = "";
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = @name", bd.getConn());
            commandOut.Parameters.AddWithValue("@name", name);
            bd.openConn();

            MySqlDataReader reader = commandOut.ExecuteReader();


            while (reader.Read())
            {
                img_link += Convert.ToString(reader.GetValue(3));
                about += Convert.ToString(reader.GetValue(2));
                group += Convert.ToString(reader.GetValue(4));
            }
            this.Close();
            item i = new item();
            i.name = name;
            i.about = about;
            i.img = img_link;
            i.group = group;
            i.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = comboBox1.Text, about = "", img_link = "", group = "";
            DataBase bd = new DataBase();
            MySqlCommand commandOut = new MySqlCommand("SELECT * FROM goods WHERE name = @name", bd.getConn());
            commandOut.Parameters.AddWithValue("@name", name);
            bd.openConn();

            MySqlDataReader reader = commandOut.ExecuteReader();


            while (reader.Read())
            {
                img_link += Convert.ToString(reader.GetValue(3));
                about += Convert.ToString(reader.GetValue(2));
                group += Convert.ToString(reader.GetValue(4));
            }
            this.Close();
            item i = new item
            {
                name = name,
                about = about,
                img = img_link,
                group = group
            };
            i.Show();
        }
    }

}
