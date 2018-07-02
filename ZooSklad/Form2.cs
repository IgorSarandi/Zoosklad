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
    public partial class Form2 : Form
    {
        Panel panel = new Panel();
        Panel subpanel = new Panel();
        Panel subpanel1 = new Panel();
        Panel subpanel2 = new Panel();
        Panel subpanel3 = new Panel();
        MenuStrip menu = new MenuStrip();
        ToolStripMenuItem CLIENT;
        ToolStripMenuItem ORDERS;
        ToolStripMenuItem PRODUCT;
        ToolStripMenuItem STATISTIC;
        DataGridView dgv1;
        DataGridView dgv2;
        ComboBox cb1;
        DateTimePicker dtp1;
        Button b1;
        Button b2;
        Button b3;
        Button b4;
        Button b5;
        Label lb0;
        Label podskazka;
        Label lb1;
        Label lb2;
        Label lb3;
        Label lb4;
        Label lb5;
        TextBox tb0;
        TextBox tb1;
        TextBox tb2;
        TextBox tb3;
        TextBox tb4;
        DateTime currDate;
        DateTime nowDate;
        MaskedTextBox mtb1;
        ToolTip tt1;
        Form form;
        Button exit = new Button()
        {
            Text = "Выход",
            AutoSize = true
        };
        string id_client;

        public Form2()
        {
            InitializeComponent();
            this.Size = new Size(800, 650);
            this.Text = "Shop manager";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Right / 2 - 300, this.Bottom / 2 - 320);
            this.HScroll = false;
            this.VScroll = true;
            this.AutoSize = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Load += new EventHandler(_Load);

            Icon icon = new Icon(Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString() + "\\ZooSklad.ico");
            this.Icon = icon;
            panel.AutoSize = true;
            panel.Location = new Point(0, 30);
            panel.Size = new Size(this.Width - 30, this.Height - 50);
            this.Controls.Add(panel);

            this.Controls.Add(menu);
            menu.Focus();

            CLIENT = new ToolStripMenuItem("Клиенты");
            CLIENT.Click += new EventHandler(CLIENT_Click);
            menu.Items.Add(CLIENT);

            ORDERS = new ToolStripMenuItem("Заказы");
            ORDERS.Click += new EventHandler(ORDERS_Click);
            menu.Items.Add(ORDERS);

            PRODUCT = new ToolStripMenuItem("Товары");
            PRODUCT.Click += new EventHandler(PRODUCT_Click);
            menu.Items.Add(PRODUCT);

            STATISTIC = new ToolStripMenuItem("Статистика");
            STATISTIC.Click += new EventHandler(STATISTIC_Click);
            menu.Items.Add(STATISTIC);

            exit.Location = new Point(this.Width - 180, this.Height - 200);
            exit.Click += new EventHandler(exit_Click);
            exit.Font = new System.Drawing.Font(exit.Font.Name, 12f);
            panel.Controls.Add(exit);
        }
        void _Intro()
        {
            panel.Controls.Clear();
            menu.Focus();
            exit.Location = new Point(this.Width - 197, this.Height - 200);
            exit.Click += new EventHandler(exit_Click);
            exit.Font = new System.Drawing.Font(exit.Font.Name, 12f);
            panel.Controls.Add(exit);

            this.Size = new Size(800, 650);

            dgv1 = new DataGridView();
            dgv1.Location = new Point(50, 25);
            dgv1.Size = new System.Drawing.Size(600, 375);
            dgv1.ReadOnly = true;
            dgv1.RowHeadersVisible = false;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            panel.Controls.Add(dgv1);
        }
        void Sugar()
        {
            panel.Controls.Remove(lb0);
            panel.Controls.Remove(tb0);
            panel.Controls.Remove(lb1);
            panel.Controls.Remove(tb1);
            panel.Controls.Remove(lb2);
            panel.Controls.Remove(tb2);
            panel.Controls.Remove(lb3);
            panel.Controls.Remove(mtb1);
            panel.Controls.Remove(lb4);
            panel.Controls.Remove(tb4);
            panel.Controls.Remove(b4);
            panel.Controls.Remove(b5);
            panel.Controls.Remove(dtp1);
            panel.Controls.Remove(podskazka);
            panel.Controls.Remove(subpanel1);

            try
            {
                form.Close();
            }
            catch (Exception ex)
            { }
        }
        private void _Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
        void CLIENT_Click(object sender, EventArgs e)
        {
            _Intro();
            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("CLIENT");
            SqlCommand cm = new SqlCommand("SELECT * FROM CLIENT", cn);
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(cm);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            zapisiAdapter.Fill(zap);
            dgv1.DataSource = zap.DefaultView;
            cn.Close();



            b1 = new Button();
            b1.Text = "Добавить";
            b1.AutoSize = true;
            b1.Location = new Point(dgv1.Location.X + 50, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b1);
            b1.Click += new EventHandler(Клиенты_Добавить_Click);

            b2 = new Button();
            b2.Text = "Изменить";
            b2.AutoSize = true;
            b2.Location = new Point(dgv1.Location.X + 150, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b2);
            b2.Click += new EventHandler(Клиенты_Изменить_Click);
        }
        void Клиенты_Добавить_Click(object sender, EventArgs e)
        {

            Sugar();
            this.Size = new Size(800, 650);

            lb1 = new Label();
            lb1.Text = "ФИО";
            lb1.Location = new Point(b1.Location.X, b1.Location.Y + b1.Height + 20);
            lb1.AutoSize = true;
            panel.Controls.Add(lb1);

            tb1 = new TextBox();
            tb1.Location = new Point(lb1.Location.X + lb1.Width + 20, lb1.Location.Y - 2);
            tb1.Size = new System.Drawing.Size(200, 20);
            panel.Controls.Add(tb1);

            lb2 = new Label();
            lb2.Text = "Адрес";
            lb2.Location = new Point(lb1.Location.X, lb1.Location.Y + lb1.Height + 15);
            lb2.AutoSize = true;
            panel.Controls.Add(lb2);

            tb2 = new TextBox();
            tb2.Location = new Point(lb2.Location.X + lb2.Width + 20, lb2.Location.Y - 2);
            tb2.Size = new System.Drawing.Size(200, 20);
            panel.Controls.Add(tb2);

            lb3 = new Label();
            lb3.Text = "Телефон";
            lb3.Location = new Point(lb2.Location.X, lb2.Location.Y + lb2.Height + 15);
            lb3.AutoSize = true;
            panel.Controls.Add(lb3);

            mtb1 = new MaskedTextBox();
            mtb1.Mask = "\\0000000000";
            mtb1.MaskInputRejected += new MaskInputRejectedEventHandler(mtb1_MaskInputRejected);
            mtb1.Location = new Point(lb3.Location.X + lb3.Width + 20, lb3.Location.Y - 2);
            mtb1.Size = new System.Drawing.Size(200, 20);
            panel.Controls.Add(mtb1);
            mtb1.Click += new EventHandler(mtb1_Click);

            lb4 = new Label();
            lb4.Text = "Скидка";
            lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 15);
            lb4.AutoSize = true;
            panel.Controls.Add(lb4);

            tb4 = new TextBox();
            tb4.Location = new Point(lb4.Location.X + lb4.Width + 20, lb4.Location.Y - 2);
            tb4.Size = new System.Drawing.Size(40, 20);
            panel.Controls.Add(tb4);

            b4 = new Button();
            b4.Text = "Принять";
            b4.AutoSize = true;
            b4.Location = new Point(lb4.Location.X + 200, lb4.Location.Y + lb4.Height + 35);
            panel.Controls.Add(b4);
            b4.Click += new EventHandler(Выполнить_Добавление_Клиенты_Click);
            b4.Focus();
        }
        void mtb1_Click(object sender, EventArgs e)
        {
            mtb1.Focus();
            mtb1.SelectionStart = mtb1.SelectionLength + 1;
        }
        void mtb1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            tt1 = new ToolTip();
            tt1.ToolTipTitle = "Ошибка ввода";
            tt1.Show("Извините, но для записи номера разрешается вводить только цифры (0-9).", mtb1, mtb1.Location, 1750);
        }
        void Выполнить_Добавление_Клиенты_Click(object sender, EventArgs e)
        {
            if (mtb1.Text.Length != 10)
            {
                MessageBox.Show("Введите корректный номер телефона", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tb1.Text.Length == 0 || tb2.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "SELECT Max(CLIENT.Id_Client) FROM CLIENT";
                command1.Connection = conn;
                int count = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                string sql = "insert into CLIENT (Id_Client, Name_Client, Address_Client, Number_Client, Sale) values (@Id_Client, @Name_Client, @Address_Client, @Number_Client, @Sale)";
                SqlCommand cmdOrderID = new SqlCommand(sql, conn);
                int i = 0;
                cmdOrderID.Parameters.Add(new SqlParameter("@Id_Client", SqlDbType.Int));
                cmdOrderID.Parameters["@Id_Client"].Value = count;
                cmdOrderID.Parameters.Add(new SqlParameter("@Name_Client", SqlDbType.VarChar));
                cmdOrderID.Parameters["@Name_Client"].Value = tb1.Text.ToString();
                cmdOrderID.Parameters.Add(new SqlParameter("@Address_Client", SqlDbType.VarChar));
                cmdOrderID.Parameters["@Address_Client"].Value = tb2.Text.ToString();
                cmdOrderID.Parameters.Add(new SqlParameter("@Number_Client", SqlDbType.Int));
                i = Convert.ToInt32(mtb1.Text);
                cmdOrderID.Parameters["@Number_Client"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Sale", SqlDbType.Int));
                try
                {
                    i = Convert.ToInt32(tb4.Text);
                }
                catch (System.FormatException)
                {
                    i = 0;
                }
                cmdOrderID.Parameters["@Sale"].Value = i;

                cmdOrderID.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                string sql1 = "select * from CLIENT";
                SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader rdr = cmdOrderID1.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rdr);
                this.dgv1.DataSource = dataTable;
                rdr.Close();
                conn.Close();
                Sugar();
                this.Size = new Size(800, 650);
                menu.Focus();
            }
        }

        void Клиенты_Изменить_Click(object sender, EventArgs e)
        {
            Sugar();
            this.Size = new Size(800, 650);

            lb0 = new Label();
            lb0.Text = "Код клиента";
            lb0.Location = new Point(b1.Location.X, b1.Location.Y + b1.Height + 20);
            lb0.AutoSize = true;
            panel.Controls.Add(lb0);

            tb0 = new TextBox();
            tb0.Location = new Point(lb0.Location.X + lb0.Width + 20, lb0.Location.Y - 2);
            tb0.Size = new System.Drawing.Size(40, 20);
            panel.Controls.Add(tb0);

            b4 = new Button();
            b4.Text = "ОК";
            b4.AutoSize = true;
            b4.Location = new Point(lb0.Location.X + lb0.Width + tb0.Width + 40, lb0.Location.Y - 2);
            panel.Controls.Add(b4);
            b4.Click += new EventHandler(Изменить_Клиенты_Click);
        }
        void Изменить_Клиенты_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * From CLIENT where Id_Client = @Id_Client";
            command.Connection = conn;
            int i = 0;
            command.Parameters.Add(new SqlParameter("@Id_Client", SqlDbType.Int));
            try
            {
                i = Convert.ToInt32(tb0.Text);
            }
            catch (System.FormatException)
            {
                i = 0;
            }
            command.Parameters["@Id_Client"].Value = i;
            i = Convert.ToInt32(command.ExecuteScalar());
            if (i == 0)
            {
                MessageBox.Show("Такого клиента нет", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                subpanel1.Controls.Clear();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                subpanel1.Location = new Point(0, lb0.Location.Y + lb0.Height);
                subpanel1.Size = new Size(this.Width, 300);
                panel.Controls.Add(subpanel1);

                lb1 = new Label();
                lb1.Text = "ФИО";
                lb1.Location = new Point(50, 15);
                lb1.AutoSize = true;
                subpanel1.Controls.Add(lb1);

                tb1 = new TextBox();
                tb1.Text = Convert.ToString(reader.GetSqlString(1));
                tb1.Location = new Point(lb1.Location.X + lb1.Width + 20, lb1.Location.Y - 2);
                tb1.Size = new System.Drawing.Size(200, 20);
                subpanel1.Controls.Add(tb1);

                lb2 = new Label();
                lb2.Text = "Адрес";
                lb2.Location = new Point(lb1.Location.X, lb1.Location.Y + lb1.Height + 15);
                lb2.AutoSize = true;
                subpanel1.Controls.Add(lb2);

                tb2 = new TextBox();
                tb2.Text = Convert.ToString(reader.GetSqlString(2));
                tb2.Location = new Point(lb2.Location.X + lb2.Width + 20, lb2.Location.Y - 2);
                tb2.Size = new System.Drawing.Size(200, 20);
                subpanel1.Controls.Add(tb2);

                lb3 = new Label();
                lb3.Text = "Телефон";
                lb3.Location = new Point(lb2.Location.X, lb2.Location.Y + lb2.Height + 15);
                lb3.AutoSize = true;
                subpanel1.Controls.Add(lb3);

                mtb1 = new MaskedTextBox();
                mtb1.Text = Convert.ToString(reader.GetSqlInt32(3));
                mtb1.MaskInputRejected += new MaskInputRejectedEventHandler(mtb1_MaskInputRejected);
                mtb1.Mask = "\\0000000000";
                mtb1.Location = new Point(lb3.Location.X + lb3.Width + 20, lb3.Location.Y - 2);
                mtb1.Size = new System.Drawing.Size(200, 20);
                subpanel1.Controls.Add(mtb1);
                mtb1.Click += new EventHandler(mtb1_Click);

                lb4 = new Label();
                lb4.Text = "Скидка";
                lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 15);
                lb4.AutoSize = true;
                subpanel1.Controls.Add(lb4);

                tb4 = new TextBox();
                tb4.Text = Convert.ToString(reader.GetSqlInt32(4));
                tb4.Location = new Point(lb4.Location.X + lb4.Width + 20, lb4.Location.Y - 2);
                tb4.Size = new System.Drawing.Size(40, 20);
                subpanel1.Controls.Add(tb4);

                b5 = new Button();
                b5.Text = "Принять";
                b5.AutoSize = true;
                b5.Location = new Point(lb4.Location.X + 200, lb4.Location.Y + lb4.Height + 15);
                subpanel1.Controls.Add(b5);
                b5.Click += new EventHandler(Выполнить_Изменить_Клиенты_Click);
                b5.Focus();

                reader.Close();
                conn.Close();
            }
        }
        void Выполнить_Изменить_Клиенты_Click(object sender, EventArgs e)
        {
            Sugar();
            this.Size = new Size(800, 650);
            if (mtb1.Text.Length != 10)
            {
                MessageBox.Show("Введите корректный номер телефона", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tb1.Text.Length == 0 || tb2.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "update CLIENT set Name_Client = @Name_Client, Address_Client = @Address_Client, Number_Client = @Number_Client, Sale = @Sale where Id_Client = @Id_Client";
                command.Connection = conn;
                int i = 0;
                command.Parameters.Add(new SqlParameter("@Id_Client", SqlDbType.Int));
                i = Convert.ToInt32(tb0.Text);
                command.Parameters["@Id_Client"].Value = i;
                command.Parameters.Add(new SqlParameter("@Name_Client", SqlDbType.VarChar));
                command.Parameters["@Name_Client"].Value = tb1.Text;
                command.Parameters.Add(new SqlParameter("@Address_Client", SqlDbType.VarChar));
                command.Parameters["@Address_Client"].Value = tb2.Text;
                command.Parameters.Add(new SqlParameter("@Number_Client", SqlDbType.Int));
                i = Convert.ToInt32(mtb1.Text);
                command.Parameters["@Number_Client"].Value = i;
                command.Parameters.Add(new SqlParameter("@Sale", SqlDbType.Int));
                i = Convert.ToInt32(tb4.Text);
                command.Parameters["@Sale"].Value = i;
                command.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                string sql1 = "select * from CLIENT";
                SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader rdr = cmdOrderID1.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rdr);
                this.dgv1.DataSource = dataTable;
                rdr.Close();
                conn.Close();
                menu.Focus();
            }
        }


        void ORDERS_Click(object sender, EventArgs e)
        {
            _Intro();

            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("ORDERS");
            SqlCommand cm = new SqlCommand("SELECT * FROM ORDERS", cn);
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(cm);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            zapisiAdapter.Fill(zap);
            dgv1.DataSource = zap.DefaultView;
            cn.Close();


            b1 = new Button();
            b1.Text = "Добавить";
            b1.AutoSize = true;
            b1.Location = new Point(dgv1.Location.X + 50, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b1);
            b1.Click += new EventHandler(Заказы_Добавить_Click);

            b2 = new Button();
            b2.Text = "Изменить";
            b2.AutoSize = true;
            b2.Location = new Point(dgv1.Location.X + 150, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b2);
            b2.Click += new EventHandler(Заказы_Изменить_Click);

            b3 = new Button();
            b3.Text = "Удалить";
            b3.AutoSize = true;
            b3.Location = new Point(dgv1.Location.X + 250, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b3);
            b3.Click += new EventHandler(Заказы_Удалить_Click);
        }
        void Заказы_Добавить_Click(object sender, EventArgs e)
        {
            Sugar();
            this.Size = new Size(800, 650);

            lb0 = new Label();
            lb0.Text = "Введите номер клиента";
            lb0.Location = new Point(b1.Location.X, b1.Location.Y + b1.Height + 20);
            lb0.AutoSize = true;
            panel.Controls.Add(lb0);

            mtb1 = new MaskedTextBox();
            mtb1.Mask = "\\0000000000";
            mtb1.Text = "";
            mtb1.MaskInputRejected += new MaskInputRejectedEventHandler(mtb1_MaskInputRejected);
            mtb1.Location = new Point(lb0.Location.X + lb0.Width + 20, lb0.Location.Y - 2);
            mtb1.Size = new System.Drawing.Size(80, 20);
            panel.Controls.Add(mtb1);
            mtb1.Click += new EventHandler(mtb1_Click);
            mtb1.TextChanged += new EventHandler(mtb1_TextChanged);

            podskazka = new Label();
            podskazka.Text = "Забыли телефон?";
            podskazka.Location = new Point(lb0.Location.X, lb0.Location.Y + 20);
            podskazka.AutoSize = true;
            podskazka.ForeColor = Color.DarkBlue;
            podskazka.Font = new Font(podskazka.Font.Name, 7, FontStyle.Underline);
            podskazka.Click += new EventHandler(podskazka_Click);
            podskazka.Cursor = Cursors.Hand;
            panel.Controls.Add(podskazka);

            b4 = new Button();
            b4.Text = "ОК";
            b4.AutoSize = true;
            b4.Location = new Point(lb0.Location.X + lb0.Width + mtb1.Width + 40, lb0.Location.Y - 2);
            panel.Controls.Add(b4);
            b4.Click += new EventHandler(Добавить_Заказы_Click);

            form = new Form();
            form.Text = "CLIENT";
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            form.BackColor = Color.White;
            form.AutoScroll = true;
            form.Size = new System.Drawing.Size(640, 480);
            form.ShowInTaskbar = false;
            form.Opacity = 0f;
            form.Enabled = false;

            dgv2 = new DataGridView();
            dgv2.Location = new Point(35, 25);
            dgv2.Size = new System.Drawing.Size(550, 375);
            dgv2.ReadOnly = true;//read-only
            dgv2.RowHeadersVisible = false;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        void podskazka_Click(object sender, EventArgs e)
        {
            lb0.Text = "Введите ФИО клиента";
            mtb1.Mask = "";
            mtb1.Text = "";
            podskazka.Text = "Отмена";
            podskazka.Click += new EventHandler(Заказы_Добавить_Click);
        }
        void mtb1_TextChanged(object sender, EventArgs e)
        {
            form.Show();
            form.ShowInTaskbar = true;
            form.Opacity = 100f;
            form.Enabled = true;
            form.Controls.Add(dgv2);

            mtb1.Focus();

            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("CLIENT");
            SqlCommand cm;
            string s = lb0.Text;
            if (s == "Введите номер клиента")
            {
                s = "SELECT * FROM CLIENT where Number_Client LIKE @Number_Client";
                cm = new SqlCommand(s, cn);
                string search = mtb1.Text;
                cm.Parameters.AddWithValue("@Number_Client", "%" + search + "%");
            }
            else
            {
                s = "SELECT * FROM CLIENT where Name_Client LIKE @Name_Client";
                cm = new SqlCommand(s, cn);
                string search = mtb1.Text;
                cm.Parameters.AddWithValue("@Name_Client", "%" + search + "%");
            }
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(cm);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            zapisiAdapter.Fill(zap);
            dgv2.DataSource = zap.DefaultView;

            SqlDataReader reader = cm.ExecuteReader();
            reader.Read();
            try
            {
                id_client = Convert.ToString(reader.GetInt32(0));
            }
            catch { }
            reader.Close();
            cn.Close();
        }
        void Добавить_Заказы_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand();
            int i = 0;
            string s = lb0.Text;
            if (s == "Введите номер клиента")
            {
                command.CommandText = "SELECT * From CLIENT where Number_Client = @Number_Client";
                command.Connection = conn;
                command.Parameters.Add(new SqlParameter("@Number_Client", SqlDbType.Int));
                try
                {
                    i = Convert.ToInt32(mtb1.Text);
                }
                catch (System.FormatException)
                {
                    i = 0;
                }
                command.Parameters["@Number_Client"].Value = i;
            }
            else
            {
                command.CommandText = "SELECT * From CLIENT where Name_Client = @Name_Client";
                command.Connection = conn;
                command.Parameters.Add(new SqlParameter("@Name_Client", SqlDbType.VarChar));
                command.Parameters["@Name_Client"].Value = mtb1.Text;
            }
            i = Convert.ToInt32(command.ExecuteScalar());
            if (i == 0)
            {
                MessageBox.Show("Такого клиента нет", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                subpanel1.Controls.Clear();
                subpanel1.Location = new Point(0, lb0.Location.Y + lb0.Height + 15);
                subpanel1.Size = new Size(this.Width, 300);
                panel.Controls.Add(subpanel1);

                lb1 = new Label();
                lb1.Text = "Выберите категорию товара";
                lb1.Location = new Point(50, 20);
                lb1.AutoSize = true;
                subpanel1.Controls.Add(lb1);

                cb1 = new ComboBox();
                cb1.Location = new Point(lb1.Width + lb1.Location.X + 5, lb1.Location.Y - 2);
                cb1.Size = new System.Drawing.Size(200, 25);
                cb1.DropDownStyle = ComboBoxStyle.DropDownList;

                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "select max(Type_Product) as Type_Product from (select count(Type_Product) as Type_Product from PRODUCT group by PRODUCT.Type_Product) PRODUCT";
                command1.Connection = conn;
                int count = Convert.ToInt32(command1.ExecuteScalar());

                SqlCommand command2 = new SqlCommand();
                command2.CommandText = "select Type_Product from PRODUCT group by PRODUCT.Type_Product";
                command2.Connection = conn;
                SqlDataReader reader = command2.ExecuteReader();
                string[] mas = new string[count];
                for (int t = 0; reader.Read() && t < count; t++)
                {
                    s = Convert.ToString(reader.GetSqlString(0));
                    mas[t] = s;
                }
                reader.Close();
                conn.Close();
                cb1.Items.AddRange(mas);
                cb1.SelectedIndex = 0;
                cb1.SelectedIndexChanged += Товары_SelectedIndexChanged;
                lb1.Focus();
                subpanel1.Controls.Add(cb1);
            }
        }
        void Товары_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            dgv2.DataSource = null;

            string sql1 = "SELECT * FROM PRODUCT WHERE Type_Product = @Type_Product";
            SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
            cmdOrderID1.Parameters.Add("@Type_Product", SqlDbType.VarChar);
            cmdOrderID1.Parameters["@Type_Product"].Value = cb1.SelectedItem.ToString();
            conn.Open();
            SqlDataReader rdr = cmdOrderID1.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(rdr);
            dgv2.DataSource = dataTable;
            rdr.Close();
            conn.Close();

            subpanel2.Controls.Clear();
            subpanel2.Location = new Point(0, lb1.Location.Y + lb1.Height + 5);
            subpanel2.Size = new Size(this.Width, 300);
            subpanel1.Controls.Add(subpanel2);

            lb2 = new Label();
            lb2.Text = "Введите код товара";
            lb2.Location = new Point(50, 15);
            lb2.AutoSize = true;
            subpanel2.Controls.Add(lb2);
            lb2.Focus();

            tb2 = new TextBox();
            tb2.Location = new Point(lb2.Width + lb2.Location.X + 5, lb2.Location.Y - 2);
            tb2.Size = new System.Drawing.Size(40, 25);
            subpanel2.Controls.Add(tb2);

            b4.Dispose();
            b4 = new Button();
            b4.Text = "ОК";
            b4.AutoSize = true;
            b4.Location = new Point(lb2.Location.X + lb2.Width + tb2.Width + 40, lb2.Location.Y - 2);
            subpanel2.Controls.Add(b4);
            b4.Click += new EventHandler(Добавить_Товары_Click);

        }
        void Добавить_Товары_Click(object sender, EventArgs e)
        {
            if (tb2.Text.Length != 0)
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * From PRODUCT WHERE Type_Product = @Type_Product AND Id_Product = @Id_Product";
                command.Connection = conn;
                int i = 0;
                command.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                try
                {
                    i = Convert.ToInt32(tb2.Text);
                }
                catch (System.FormatException)
                {
                    i = 0;
                }
                command.Parameters["@Id_Product"].Value = i;
                command.Parameters.Add(new SqlParameter("@Type_Product", SqlDbType.VarChar));
                command.Parameters["@Type_Product"].Value = cb1.SelectedItem.ToString();
                i = Convert.ToInt32(command.ExecuteScalar());
                if (i == 0)
                {
                    MessageBox.Show("Такого товара нет", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    subpanel3.Controls.Clear();
                    subpanel3.Location = new Point(0, lb2.Location.Y + lb2.Height + 15);
                    subpanel3.Size = new Size(this.Width, 300);
                    subpanel2.Controls.Add(subpanel3);

                    lb3 = new Label();
                    lb3.Text = "Укажите дату:";
                    lb3.AutoSize = true;
                    lb3.Location = new Point(50, 10);
                    subpanel3.Controls.Add(lb3);

                    dtp1 = new DateTimePicker();
                    dtp1.Format = DateTimePickerFormat.Short;
                    dtp1.Location = new Point(lb3.Width + lb3.Location.X, lb3.Location.Y - 3);
                    dtp1.Size = new System.Drawing.Size(100, 30);
                    subpanel3.Controls.Add(dtp1);

                    lb4 = new Label();
                    lb4.Text = "Количество товара";
                    lb4.AutoSize = true;
                    lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 25);
                    subpanel3.Controls.Add(lb4);
                    lb4.Focus();

                    tb4 = new TextBox();
                    tb4.Location = new Point(lb4.Width + lb4.Location.X + 5, lb4.Location.Y - 2);
                    tb4.Size = new System.Drawing.Size(40, 25);
                    subpanel3.Controls.Add(tb4);

                    b4.Dispose();
                    b4 = new Button();
                    b4.Text = "ОК";
                    b4.AutoSize = true;
                    b4.Location = new Point(lb4.Location.X + lb4.Width + tb4.Width + 40, lb4.Location.Y - 2);
                    subpanel3.Controls.Add(b4);
                    b4.Click += new EventHandler(Выполнить_Добавить_Заказы_Click);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// ///////IT'S DANGEROUS!!!!!!!!!!!!!!!!!!
        void Выполнить_Добавить_Заказы_Click(object sender, EventArgs e)
        {
            if (tb4.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();

                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "SELECT Max(ORDERS.Id_Order) FROM ORDERS";
                command1.Connection = conn;
                int count = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                string sql = "insert into ORDERS (Id_Order, Id_Product, Date_Order, Amt_Order, Id_Client, Status_Order) values (@Id_Order, @Id_Product, @Date_Order, @Amt_Order, @Id_Client, @Status_Order)";
                SqlCommand cmdOrderID = new SqlCommand(sql, conn);
                int i = 0;
                cmdOrderID.Parameters.Add(new SqlParameter("@Id_Order", SqlDbType.Int));
                cmdOrderID.Parameters["@Id_Order"].Value = count;
                cmdOrderID.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                i = Convert.ToInt32(tb2.Text);
                cmdOrderID.Parameters["@Id_Product"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Date_Order", SqlDbType.Date));
                cmdOrderID.Parameters["@Date_Order"].Value = dtp1.Value;
                cmdOrderID.Parameters.Add(new SqlParameter("@Amt_Order", SqlDbType.Int));
                i = Convert.ToInt32(tb4.Text);
                cmdOrderID.Parameters["@Amt_Order"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Id_Client", SqlDbType.Int));
                i = Convert.ToInt32(id_client);
                cmdOrderID.Parameters["@Id_Client"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Status_Order", SqlDbType.VarChar));
                string s = "в процессе";
                cmdOrderID.Parameters["@Status_Order"].Value = s;

                cmdOrderID.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                string sql1 = "select * from ORDERS";
                SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader rdr = cmdOrderID1.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rdr);
                this.dgv1.DataSource = dataTable;
                rdr.Close();
                conn.Close();
                form.Close();
                Sugar();
                this.Size = new Size(800, 650);
                menu.Focus();
            }
        }


        void Заказы_Изменить_Click(object sender, EventArgs e)
        {

            Sugar();
            this.Size = new Size(800, 650);

            lb0 = new Label();
            lb0.Text = "Код заказа";
            lb0.Location = new Point(b1.Location.X, b1.Location.Y + b1.Height + 20);
            lb0.AutoSize = true;
            panel.Controls.Add(lb0);

            tb0 = new TextBox();
            tb0.Location = new Point(lb0.Location.X + lb0.Width + 20, lb0.Location.Y - 2);
            tb0.Size = new System.Drawing.Size(40, 20);
            panel.Controls.Add(tb0);

            b4 = new Button();
            b4.Text = "ОК";
            b4.AutoSize = true;
            b4.Location = new Point(lb0.Location.X + lb0.Width + tb0.Width + 40, lb0.Location.Y - 2);
            panel.Controls.Add(b4);
            b4.Click += new EventHandler(Изменить_Заказы_Click);
        }
        void Изменить_Заказы_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * From ORDERS where Id_Order = @Id_Order";
            command.Connection = conn;
            int i = 0;
            command.Parameters.Add(new SqlParameter("@Id_Order", SqlDbType.Int));
            try
            {
                i = Convert.ToInt32(tb0.Text);
            }
            catch (System.FormatException)
            {
                i = 0;
            }
            command.Parameters["@Id_Order"].Value = i;
            i = Convert.ToInt32(command.ExecuteScalar());
            if (i == 0)
            {
                MessageBox.Show("Такого заказа нет", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                subpanel1.Controls.Clear();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                subpanel1.Location = new Point(0, lb0.Location.Y + lb0.Height);
                subpanel1.Size = new Size(this.Width, 300);
                panel.Controls.Add(subpanel1);

                lb1 = new Label();
                lb1.Text = "Код товара";
                lb1.Location = new Point(50, 15);
                lb1.AutoSize = true;
                subpanel1.Controls.Add(lb1);

                tb1 = new TextBox();
                tb1.Text = Convert.ToString(reader.GetSqlInt32(1));
                tb1.Location = new Point(lb1.Location.X + lb1.Width + 20, lb1.Location.Y - 2);
                tb1.Size = new System.Drawing.Size(40, 20);
                subpanel1.Controls.Add(tb1);

                lb2 = new Label();
                lb2.Text = "Дата";
                lb2.Location = new Point(lb1.Location.X, lb1.Location.Y + lb1.Height + 15);
                lb2.AutoSize = true;
                subpanel1.Controls.Add(lb2);

                dtp1 = new DateTimePicker();
                dtp1.Text = Convert.ToString(reader.GetDateTime(2).ToShortDateString());
                dtp1.Location = new Point(lb2.Location.X + lb2.Width + 20, lb2.Location.Y - 2);
                dtp1.Size = new System.Drawing.Size(120, 20);
                subpanel1.Controls.Add(dtp1);

                lb3 = new Label();
                lb3.Text = "Количество товара";
                lb3.Location = new Point(lb2.Location.X, lb2.Location.Y + lb2.Height + 15);
                lb3.AutoSize = true;
                subpanel1.Controls.Add(lb3);

                tb3 = new TextBox();
                tb3.Text = Convert.ToString(reader.GetSqlInt32(3));
                tb3.Location = new Point(lb3.Location.X + lb3.Width + 20, lb3.Location.Y - 2);
                tb3.Size = new System.Drawing.Size(40, 20);
                subpanel1.Controls.Add(tb3);

                lb4 = new Label();
                lb4.Text = "Код клиента";
                lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 15);
                lb4.AutoSize = true;
                subpanel1.Controls.Add(lb4);

                tb4 = new TextBox();
                tb4.Text = Convert.ToString(reader.GetSqlInt32(4));
                tb4.Location = new Point(lb4.Location.X + lb4.Width + 20, lb4.Location.Y - 2);
                tb4.Size = new System.Drawing.Size(40, 20);
                subpanel1.Controls.Add(tb4);

                lb5 = new Label();
                lb5.Text = "Статус";
                lb5.Location = new Point(lb4.Location.X, lb4.Location.Y + lb4.Height + 15);
                lb5.AutoSize = true;
                subpanel1.Controls.Add(lb5);

                cb1 = new ComboBox();
                cb1.Location = new Point(lb5.Width + lb5.Location.X + 5, lb5.Location.Y - 2);
                cb1.Size = new System.Drawing.Size(80, 25);
                cb1.DropDownStyle = ComboBoxStyle.DropDownList;
                cb1.Items.Add("в процессе");
                cb1.Items.Add("доставлен");
                cb1.SelectedItem = Convert.ToString(reader.GetSqlString(5));
                subpanel1.Controls.Add(cb1);

                b5 = new Button();
                b5.Text = "Принять";
                b5.AutoSize = true;
                b5.Location = new Point(lb5.Location.X + 200, lb5.Location.Y + lb5.Height + 15);
                subpanel1.Controls.Add(b5);
                b5.Click += new EventHandler(Выполнить_Изменить_Заказы_Click);
                b5.Focus();

                reader.Close();
                conn.Close();
            }
        }
        void Выполнить_Изменить_Заказы_Click(object sender, EventArgs e)
        {
            if (tb1.Text.Length == 0 || tb3.Text.Length == 0 || tb4.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "update ORDERS set Id_Product = @Id_Product, Date_Order = @Date_Order, Amt_Order = @Amt_Order, Id_Client = @Id_Client, Status_Order = @Status_Order where Id_Order = @Id_Order";
                command.Connection = conn;
                int i = 0;
                command.Parameters.Add(new SqlParameter("@Id_Order", SqlDbType.Int));
                i = Convert.ToInt32(tb0.Text);
                command.Parameters["@Id_Order"].Value = i;
                command.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                i = Convert.ToInt32(tb1.Text);
                command.Parameters["@Id_Product"].Value = i;
                command.Parameters.Add(new SqlParameter("@Date_Order", SqlDbType.Date));
                command.Parameters["@Date_Order"].Value = dtp1.Value;
                command.Parameters.Add(new SqlParameter("@Amt_Order", SqlDbType.Int));
                i = Convert.ToInt32(tb3.Text);
                command.Parameters["@Amt_Order"].Value = i;
                command.Parameters.Add(new SqlParameter("@Id_Client", SqlDbType.Int));
                i = Convert.ToInt32(tb4.Text);
                command.Parameters["@Id_Client"].Value = i;
                command.Parameters.Add(new SqlParameter("@Status_Order", SqlDbType.VarChar));
                command.Parameters["@Status_Order"].Value = cb1.SelectedItem.ToString();
                command.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                string sql1 = "select * from ORDERS";
                SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader rdr = cmdOrderID1.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rdr);
                this.dgv1.DataSource = dataTable;
                rdr.Close();
                conn.Close();
                Sugar();
                this.Size = new Size(800, 650);
                menu.Focus();
            }
        }


        void Заказы_Удалить_Click(object sender, EventArgs e)
        {
            Sugar();
            this.Size = new Size(800, 650);

            lb0 = new Label();
            lb0.Text = "Код заказа";
            lb0.Location = new Point(b1.Location.X, b1.Location.Y + b1.Height + 20);
            lb0.AutoSize = true;
            panel.Controls.Add(lb0);

            tb0 = new TextBox();
            tb0.Location = new Point(lb0.Location.X + lb0.Width + 20, lb0.Location.Y - 2);
            tb0.Size = new System.Drawing.Size(40, 20);
            panel.Controls.Add(tb0);

            b4 = new Button();
            b4.Text = "Принять";
            b4.AutoSize = true;
            b4.Location = new Point(tb0.Location.X + tb0.Width + 20, lb0.Location.Y - 2);
            panel.Controls.Add(b4);
            b4.Click += new EventHandler(Выполнить_Заказы_Удалить_Click);
            b4.Focus();
        }
        void Выполнить_Заказы_Удалить_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * From ORDERS where Id_Order = @Id_Order";
            command.Connection = conn;
            int i = 0;
            command.Parameters.Add(new SqlParameter("@Id_Order", SqlDbType.Int));
            try
            {
                i = Convert.ToInt32(tb0.Text);
            }
            catch (System.FormatException)
            {
                i = 0;
            }
            command.Parameters["@Id_Order"].Value = i;
            i = Convert.ToInt32(command.ExecuteScalar());
            if (i == 0)
            {
                MessageBox.Show("Такого заказа нет", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "delete from ORDERS where Id_Order = @Id_Order";
                command1.Connection = conn;
                i = 0;

                command1.Parameters.Add(new SqlParameter("@Id_Order", SqlDbType.Int));
                i = Convert.ToInt32(tb0.Text);
                command1.Parameters["@Id_Order"].Value = i;
                i = command1.ExecuteNonQuery();
                conn.Close();

                dgv1.DataSource = null;
                string sql1 = "select * from ORDERS";
                SqlCommand command2 = new SqlCommand(sql1, conn);
                conn.Open();
                SqlDataReader rdr = command2.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rdr);
                this.dgv1.DataSource = dataTable;
                rdr.Close();
                conn.Close();
                menu.Focus();
                Sugar();
                this.Size = new Size(800, 650);
            }
        }


        void _Color(string c, SqlConnection cn)
        {
            for (int i = 0; i < dgv1.ColumnCount; i++)
                dgv1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            currDate = new DateTime();
            nowDate = new DateTime();
            for (int i = 1; i < dgv1.RowCount; i++)
            {
                SqlCommand comm = new SqlCommand(c, cn);
                comm.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                comm.Parameters["@ID"].Value = i;
                comm.ExecuteScalar();
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                string s = reader.GetDateTime(7).ToShortDateString();

                currDate = Convert.ToDateTime(s);

                nowDate = DateTime.Today;

                double n = currDate.Subtract(nowDate).TotalDays;
                if (n < 0)//разница 0 либо меньше
                    dgv1.Rows[i - 1].DefaultCellStyle.BackColor = Color.Red;
                else if (n <= 10)//10 дней разница
                {
                    dgv1.Rows[i - 1].DefaultCellStyle.BackColor = Color.Pink;
                }
                else if (n <= 100)//100 дней разница
                {
                    dgv1.Rows[i - 1].DefaultCellStyle.BackColor = Color.Moccasin;
                }

                reader.Close();
            }
            cn.Close();
        }
        void PRODUCT_Click(object sender, EventArgs e)
        {
            Sugar();
            this.Size = new Size(800, 650);
            _Intro();
            dgv1.Size = new System.Drawing.Size(700, 375);
            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("PRODUCT");
            SqlCommand cm = new SqlCommand("SELECT * FROM PRODUCT", cn);
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(cm);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            zapisiAdapter.Fill(zap);
            dgv1.DataSource = zap.DefaultView;

            string s = "SELECT * FROM PRODUCT where @ID = Id_Product";
            _Color(s, cn);

            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();

            lb1 = new Label();
            lb1.Text = "Выберите категорию товара";
            lb1.Location = new Point(dgv1.Location.X + 30, dgv1.Location.Y + dgv1.Height + 20);
            lb1.AutoSize = true;
            panel.Controls.Add(lb1);

            cb1 = new ComboBox();
            cb1.Location = new Point(lb1.Width + lb1.Location.X + 35, lb1.Location.Y - 2);
            cb1.Size = new System.Drawing.Size(200, 25);
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;

            lb2 = new Label();
            lb2.Text = "Просроченные";
            lb2.Location = new Point(cb1.Location.X, cb1.Location.Y + cb1.Height + 40);
            lb2.AutoSize = true;
            lb2.BackColor = Color.Red;
            lb2.Font = new System.Drawing.Font(lb2.Font.Name, 12f, FontStyle.Bold);
            panel.Controls.Add(lb2);

            lb3 = new Label();
            lb3.Text = "Осталось меньше 10 дней";
            lb3.Location = new Point(lb2.Location.X, lb2.Location.Y + lb2.Height + 20);
            lb3.AutoSize = true;
            lb3.BackColor = Color.Pink;
            lb3.Font = new System.Drawing.Font(lb3.Font.Name, 12f, FontStyle.Bold);
            panel.Controls.Add(lb3);

            lb4 = new Label();
            lb4.Text = "Осталось меньше 100 дней";
            lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 20);
            lb4.AutoSize = true;
            lb4.BackColor = Color.Moccasin;
            lb4.Font = new System.Drawing.Font(lb4.Font.Name, 12f);
            panel.Controls.Add(lb4);

            b1 = new Button();
            b1.Text = "Cancel";
            b1.Location = new Point(lb1.Location.X, lb1.Location.Y + 25);
            b1.AutoSize = true;
            b1.Click += Товары_Отмена_Click;
            panel.Controls.Add(b1);

            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "select max(Type_Product) as Type_Product from (select count(Type_Product) as Type_Product from PRODUCT group by PRODUCT.Type_Product) PRODUCT";
            command1.Connection = conn;
            int count = Convert.ToInt32(command1.ExecuteScalar());

            SqlCommand command2 = new SqlCommand();
            command2.CommandText = "select Type_Product from PRODUCT group by PRODUCT.Type_Product";
            command2.Connection = conn;
            SqlDataReader rd = command2.ExecuteReader();
            string[] mas = new string[count];
            string ss;
            for (int t = 0; rd.Read() && t < count; t++)
            {
                ss = Convert.ToString(rd.GetSqlString(0));
                mas[t] = ss;
            }
            rd.Close();
            conn.Close();
            cb1.Items.AddRange(mas);
            cb1.SelectedIndex = 0;
            cb1.SelectedIndexChanged += Товары_Фильтр_SelectedIndexChanged;
            lb1.Focus();
            panel.Controls.Add(cb1);
        }
        void Товары_Фильтр_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv1.DataSource = null;

            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("PRODUCT");
            SqlCommand command = new SqlCommand("SELECT * From PRODUCT WHERE Type_Product = @Type_Product", cn);
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(command);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            command.Connection = cn;
            command.Parameters.Add(new SqlParameter("@Type_Product", SqlDbType.VarChar));
            command.Parameters["@Type_Product"].Value = cb1.SelectedItem.ToString();
            zapisiAdapter.Fill(zap);
            dgv1.DataSource = zap.DefaultView;
            cn.Close();
        }
        void Товары_Отмена_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(Form1.connectionString);

            dgv1.DataSource = null;

            string sql = "select * from PRODUCT";
            SqlCommand cmdOrderID1 = new SqlCommand(sql, cn);
            cn.Open();
            SqlDataReader rdr = cmdOrderID1.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(rdr);
            this.dgv1.DataSource = dataTable;
            rdr.Close();
            string s = "SELECT * FROM PRODUCT where @ID = Id_Product";
            _Color(s, cn);
            menu.Focus();
        }

        void STATISTIC_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            menu.Focus();
            this.Size = new Size(800, 650);
            exit.Location = new Point(this.Width - 180, this.Height - 100);
            exit.Click += new EventHandler(exit_Click);
            panel.Controls.Add(exit);

            b1 = new Button();
            b1.Text = "Количество заказов";
            b1.AutoSize = true;
            b1.Location = new Point(75, 75);
            b1.Font = new System.Drawing.Font(b1.Font.Name, 12f);
            panel.Controls.Add(b1);
            b1.Click += new EventHandler(STATISTIC_CountOrders_Click);

            b2 = new Button();
            b2.Text = "Топ клиентов";
            b2.AutoSize = true;
            b2.Location = new Point(b1.Location.X, b1.Location.Y + b1.Size.Height + 25);
            b2.Font = new System.Drawing.Font(b1.Font.Name, 12f);
            panel.Controls.Add(b2);
            b2.Click += new EventHandler(STATISTIC_TopOrders_Click);
        }
        void STATISTIC_CountOrders_Click(object sender, EventArgs e)
        {
            lb1 = new Label();
            panel.Controls.Remove(lb1);
            lb1.Location = new Point(b1.Location.X + b1.Size.Width + 30, b1.Location.Y);
            lb1.TextAlign = ContentAlignment.BottomCenter;
            lb1.Font = new System.Drawing.Font(lb1.Font.Name, 14f);
            lb1.AutoSize = true;
            panel.Controls.Add(lb1);

            SqlConnection cn = new SqlConnection(Form1.connectionString);
            SqlCommand cm = new SqlCommand("SELECT Count(*) AS Количество_сделок FROM ORDERS", cn);
            cn.Open();
            SqlDataReader reader = cm.ExecuteReader();
            reader.Read();
            string s;
            s = Convert.ToString(reader.GetSqlInt32(0));
            reader.Close();
            cn.Close();

            lb1.Text = "Общее число заказов равно " + s;
        }
        void STATISTIC_TopOrders_Click(object sender, EventArgs e)
        {
            panel.Controls.Remove(dgv1);
            dgv1 = new DataGridView();
            dgv1.Location = new Point(b2.Location.X + b2.Size.Width + 30, b2.Location.Y);
            dgv1.AutoSize = true;
            dgv1.ReadOnly = true;
            dgv1.Font = new System.Drawing.Font(dgv1.Font.Name, 10f);
            dgv1.RowHeadersVisible = false;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            panel.Controls.Add(dgv1);

            string s1 = "SELECT CLIENT.Name_Client as Клиент, Count(ORDERS.Id_Client) ";
            string s2 = "AS Заказы FROM ORDERS INNER JOIN CLIENT ON CLIENT.Id_Client = ORDERS.Id_Client ";
            string s3 = "GROUP BY CLIENT.Name_Client HAVING Count(ORDERS.Id_Client)>1 ORDER BY Заказы Desc, Клиент";
            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("TOP");
            SqlCommand cm = new SqlCommand(s1 + s2 + s3, cn);
            SqlDataAdapter zapisiAdapter = new SqlDataAdapter(cm);
            try
            {
                cn.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            zapisiAdapter.Fill(zap);
            dgv1.DataSource = zap.DefaultView;
            cn.Close();
        }
        void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
