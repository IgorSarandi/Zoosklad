using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ZooSklad
{
    public partial class Form1 : Form
    {

        Label loglab;
        Label passlab;
        TextBox logtext;
        TextBox passtext;
        Button butload;
        static string path = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString() + "\\ZooSklad.mdf";
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=" + path + ";Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            MainForm();
        }
        void MainForm()
        {
            this.Text = "Вход";
            this.Size = new Size(640, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            Icon icon = new Icon(Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString() + "\\ZooSklad.ico");
            this.Icon = icon;
            loglab = new Label();
            passlab = new Label();
            logtext = new TextBox();
            passtext = new TextBox();
            butload = new Button();
            int x = (this.Width - loglab.Width) / 3;
            int y = (this.Height - loglab.Height) / 5;
            loglab.Text = "Введите логин";
            loglab.AutoSize = true;
            loglab.Location = new Point(x, y);
            y += logtext.Height;
            logtext.AutoSize = true;
            logtext.Location = new Point(x, y);
            y += 2 * passlab.Height;
            passlab.Text = "Введите пароль";
            passlab.AutoSize = true;
            passlab.Location = new Point(x, y);
            y += passtext.Height;
            passtext.AutoSize = true;
            passtext.Location = new Point(x, y);
            passtext.PasswordChar = '*';
            y += 2 * passtext.Height;
            butload.Text = "Вход";
            butload.AutoSize = true;
            butload.Location = new Point(x, y);
            butload.Click += butload_Click;
            this.Controls.Add(loglab);
            this.Controls.Add(logtext);
            this.Controls.Add(passlab);
            this.Controls.Add(passtext);
            this.Controls.Add(butload);
        }
        public void butload_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            Person p = new Person();
            command = conn.CreateCommand();
            command.CommandText = CommandType.Text.ToString();
            MessageBox.Show("подключено", "Состояние");
            command.CommandText = "SELECT Логин, Пароль, ИД FROM УчетныеЗаписи";
            command.Connection = conn;
            SqlDataReader reader1 = command.ExecuteReader();
            bool b = false;
            bool a = false;
            if (reader1.HasRows) // если есть данные
            {
                reader1.Close();
                SqlCommand command1 = new SqlCommand("SELECT Count(*) As Логин FROM УчетныеЗаписи", conn);
                uint Z = Convert.ToUInt32(command1.ExecuteScalar());
                command.CommandType = CommandType.Text;
                SqlDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    p.login = reader2["Логин"].ToString();
                    p.password = reader2["Пароль"].ToString();
                    p.id = reader2["ИД"].ToString();
                    if (logtext.Text == p.login && passtext.Text == p.password)
                    {
                        b = true;
                        if (p.id == "True")//замененить на нужный индекс int
                        {
                            a = true;// тоже самое
                        }
                    }
                    else
                        Z -= 1;
                }
                reader2.Close();
                if (Z == 0)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (b)
                {
                    if (a)
                    {
                        Form3 fr3 = new Form3();
                        fr3.Show();
                        this.Hide();
                        conn.Close();
                    }
                    else
                    {
                        Form2 fr2 = new Form2();
                        fr2.Show();
                        this.Hide();
                        conn.Close();
                    }
                }
            }
        }
    }
    class Person
    {
        public string login;
        public string password;
        public string id;
    }
}
