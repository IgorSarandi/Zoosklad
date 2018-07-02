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
using System.Collections;

namespace ZooSklad
{
    public partial class Form3 : Form
    {
        Panel panel = new Panel();
        Panel subpanel = new Panel();
        Panel subpanel1 = new Panel();
        Panel subpanel2 = new Panel();
        MenuStrip menu = new MenuStrip();
        ToolStripMenuItem PRODUCT;
        ToolStripMenuItem STATISTIC;
        DataGridView dgv1;
        ComboBox cb1;
        ComboBox cb2;
        DateTimePicker dtp1;
        Button b1;
        Button b2;
        Button b3;
        Button b4;
        Label lb0;
        Label lb1;
        Label lb2;
        Label lb3;
        Label lb4;
        Label lb5;
        Label lb6;
        Label lb7;
        Label lb8;
        Label lb9;
        TextBox tb0;
        TextBox tb1;
        TextBox tb2;
        TextBox tb3;
        TextBox tb4;
        TextBox tb5;
        TextBox tb6;
        TextBox tb7;
        TextBox tb9;
        string type;
        DateTime currDate;
        DateTime nowDate;
        Button exit = new Button()
        {
            Text = "Выход",
            AutoSize = true,
        };
        public Form3()
        {
            InitializeComponent();
            this.Size = new Size(1000, 650);
            this.Text = "Store manager";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Right / 2 - 300, this.Bottom / 2 - 320);
            this.HScroll = false;
            this.VScroll = true;
            this.AutoSize = true;

            Icon icon = new Icon(Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString() + "\\ZooSklad.ico");
            this.Icon = icon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Load += new EventHandler(_Load);
            panel.AutoSize = true;
            panel.Location = new Point(0, 30);
            panel.Size = new Size(this.Width - 30, this.Height - 50);
            this.Controls.Add(panel);

            this.Controls.Add(menu);
            menu.Focus();

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
        private void _Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
        void _Intro()
        {
            panel.Controls.Clear();
            menu.Focus();
            exit.Location = new Point(this.Width - 197, this.Height - 200);
            exit.Font = new System.Drawing.Font(exit.Font.Name, 12f);
            exit.Click += new EventHandler(exit_Click);
            panel.Controls.Add(exit);

            dgv1 = new DataGridView();
            dgv1.Location = new Point(50, 25);
            dgv1.Size = new System.Drawing.Size(900, 350);
            dgv1.ReadOnly = true;
            dgv1.RowHeadersVisible = false;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            panel.Controls.Add(dgv1);
        }
        void Product()
        {
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
            for (int i = 0; i < dgv1.ColumnCount; i++)
                dgv1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            currDate = new DateTime();
            nowDate = new DateTime();
            for (int i = 1; i < dgv1.RowCount; i++)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM PRODUCT where @ID = Id_Product", cn);
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters["@ID"].Value = i;
                command.ExecuteScalar();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string s = DateTime.Today.ToString();
                try
                {
                    s = reader.GetDateTime(7).ToShortDateString();
                }
                catch (Exception ex)
                { }
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
            _Intro();
            this.Size = new Size(1000, 650);
            Product();

            b1 = new Button();
            b1.Text = "Добавить";
            b1.AutoSize = true;
            b1.Location = new Point(dgv1.Location.X + 50, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b1);
            b1.Click += new EventHandler(Товары_Добавить_Click);

            b2 = new Button();
            b2.Text = "Изменить";
            b2.AutoSize = true;
            b2.Location = new Point(dgv1.Location.X + 150, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b2);
            b2.Click += new EventHandler(Товары_Изменить_Click);

            b3 = new Button();
            b3.Text = "Удалить";
            b3.AutoSize = true;
            b3.Location = new Point(dgv1.Location.X + 250, dgv1.Location.Y + dgv1.Height + 8);
            panel.Controls.Add(b3);
            b3.Click += new EventHandler(Товары_Удалить_Click);
        }
        void Товары_Добавить_Click(object sender, EventArgs e)
        {
            SomeMethod();
        }
        void SomeMethod()
        {
            Product();

            subpanel.Controls.Clear();
            subpanel.Location = new Point(0, b1.Location.Y + b1.Height + 15);
            subpanel.Size = new Size(this.Width, 250);
            panel.Controls.Add(subpanel);

            lb0 = new Label();
            lb0.Text = "Выберите категорию товара";
            lb0.Location = new Point(50, 20);
            lb0.AutoSize = true;
            subpanel.Controls.Add(lb0);

            cb1 = new ComboBox();
            cb1.Location = new Point(lb0.Width + lb0.Location.X + 5, lb0.Location.Y - 2);
            cb1.Size = new System.Drawing.Size(200, 25);
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;
            cb1.Items.Add("Выбрать существующий");
            cb1.Items.Add("Добавить новый");
            cb1.SelectedIndex = 0;
            cb1.SelectedIndexChanged += Товары_SelectedIndexChanged;
            subpanel.Controls.Add(cb1);
            lb0.Focus();
            //this.Focus();
        }
        void Товары_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb1.SelectedIndex == 0)
            {
                subpanel.Controls.Clear();
                cb2 = new ComboBox();
                cb2.Enabled = true;
                cb2.Location = new Point(cb1.Location.X, cb1.Location.Y + cb1.Height + 15);
                cb2.Size = new System.Drawing.Size(200, 25);
                cb2.DropDownStyle = ComboBoxStyle.DropDownList;

                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "select max(Type_Product) as Type_Product from (select count(Type_Product) as Type_Product from PRODUCT group by PRODUCT.Type_Product) PRODUCT";
                command1.Connection = conn;
                int count = Convert.ToInt32(command1.ExecuteScalar());

                SqlCommand command2 = new SqlCommand();
                command2.CommandText = "select Type_Product from PRODUCT group by PRODUCT.Type_Product";
                command2.Connection = conn;
                SqlDataReader reader = command2.ExecuteReader();
                string[] mas = new string[count];
                string s;
                for (int t = 0; reader.Read() && t < count; t++)
                {
                    s = Convert.ToString(reader.GetSqlString(0));
                    mas[t] = s;
                }
                reader.Close();
                conn.Close();
                cb2.Items.AddRange(mas);
                cb2.SelectedIndex = 0;
                lb0.Focus();
                subpanel.Controls.Add(cb2);
                subpanel.Controls.Add(lb0);
                subpanel.Controls.Add(cb1);

                try
                {
                    tb0.Enabled = false;
                }
                catch (NullReferenceException)
                { }
            }
            else
            {
                subpanel.Controls.Clear();
                tb0 = new TextBox();
                tb0.Enabled = true;
                tb0.Location = new Point(cb1.Location.X, cb1.Location.Y + cb1.Height + 15);
                tb0.Size = new System.Drawing.Size(200, 20);
                subpanel.Controls.Add(lb0);
                subpanel.Controls.Add(cb1);
                subpanel.Controls.Add(tb0);

                try
                {
                    cb2.Enabled = false;
                }
                catch (NullReferenceException)
                { }
            }
            b4 = new Button();
            b4.Text = "ОК";
            b4.AutoSize = true;
            b4.Location = new Point(cb1.Location.X + 250, cb1.Location.Y + cb1.Height + 15);
            subpanel.Controls.Add(b4);
            b4.Click += new EventHandler(Выполнить_Добавление_Товары_Click);
            b4.Focus();
        }
        void Выполнить_Добавление_Товары_Click(object sender, EventArgs e)
        {
            bool b = false, c = false;
            try
            {
                b = tb0.Enabled;
            }
            catch (NullReferenceException)
            { }
            try
            {
                c = cb2.Enabled;
            }
            catch (NullReferenceException)
            { }
            if (b && !c)
            {
                try
                {
                    b = tb0.Text.Length == 0 ? true : false;
                }
                catch (NullReferenceException)
                { }
                try
                {
                    c = tb0.CanSelect == true ? true : false;
                }
                catch (NullReferenceException)
                { }
                if (b && c)
                {
                    MessageBox.Show("Введите категорию", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SomeMethod();
                }
                else
                {
                    type = tb0.Text;
                    AnotherMethod();
                }
            }
            else if (!b || c)
            {
                type = cb2.SelectedItem.ToString();
                AnotherMethod();
            }
        }
        void AnotherMethod()
        {
            subpanel1.Controls.Clear();
            subpanel1.Location = new Point(0, b4.Location.Y + b4.Height + 15);
            subpanel1.Size = new Size(this.Width, 250);
            subpanel.Controls.Add(subpanel1);

            lb1 = new Label();
            lb1.Text = "Название товара";
            lb1.Location = new Point(50, 10);
            lb1.AutoSize = true;
            subpanel1.Controls.Add(lb1);

            tb1 = new TextBox();
            tb1.Location = new Point(lb1.Location.X + lb1.Width + 20, lb1.Location.Y - 2);
            tb1.Size = new System.Drawing.Size(200, 20);
            subpanel1.Controls.Add(tb1);

            lb2 = new Label();
            lb2.Text = "Amt_on_Sklad";
            lb2.Location = new Point(lb1.Location.X, lb1.Location.Y + lb1.Height + 15);
            lb2.AutoSize = true;
            subpanel1.Controls.Add(lb2);

            tb2 = new TextBox();
            tb2.Location = new Point(lb2.Location.X + lb2.Width + 20, lb2.Location.Y - 2);
            tb2.Size = new System.Drawing.Size(40, 20);
            subpanel1.Controls.Add(tb2);

            lb3 = new Label();
            lb3.Text = "Prodano_za_Month";
            lb3.Location = new Point(lb2.Location.X, lb2.Location.Y + lb2.Height + 15);
            lb3.AutoSize = true;
            subpanel1.Controls.Add(lb3);

            tb3 = new TextBox();
            tb3.Location = new Point(lb3.Location.X + lb3.Width + 20, lb3.Location.Y - 2);
            tb3.Size = new System.Drawing.Size(40, 20);
            subpanel1.Controls.Add(tb3);

            lb4 = new Label();
            lb4.Text = "Amt_Unit";
            lb4.Location = new Point(tb2.Location.X + tb2.Width + 50, tb2.Location.Y);
            lb4.AutoSize = true;
            subpanel1.Controls.Add(lb4);

            tb4 = new TextBox();
            tb4.Location = new Point(lb4.Location.X + lb4.Width + 20, lb4.Location.Y - 2);
            tb4.Size = new System.Drawing.Size(40, 20);
            subpanel1.Controls.Add(tb4);

            lb5 = new Label();
            lb5.Text = "Unit";
            lb5.Location = new Point(lb4.Location.X, lb4.Location.Y + lb4.Height + 15);
            lb5.AutoSize = true;
            subpanel1.Controls.Add(lb5);

            tb5 = new TextBox();
            tb5.Location = new Point(lb5.Location.X + lb5.Width + 20, lb5.Location.Y - 2);
            tb5.Size = new System.Drawing.Size(40, 20);
            subpanel1.Controls.Add(tb5);

            lb6 = new Label();
            lb6.Text = "Expiration_Date";
            lb6.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 15);
            lb6.AutoSize = true;
            subpanel1.Controls.Add(lb6);

            dtp1 = new DateTimePicker();
            dtp1.Format = DateTimePickerFormat.Short;
            dtp1.Location = new Point(lb6.Width + lb6.Location.X + 10, lb6.Location.Y - 3);
            dtp1.Size = new System.Drawing.Size(100, 30);
            subpanel1.Controls.Add(dtp1);

            lb7 = new Label();
            lb7.Text = "Price";
            lb7.Location = new Point(dtp1.Location.X + dtp1.Width + 20, dtp1.Location.Y);
            lb7.AutoSize = true;
            subpanel1.Controls.Add(lb7);

            tb7 = new TextBox();
            tb7.Location = new Point(lb7.Location.X + lb7.Width + 20, lb7.Location.Y - 2);
            tb7.Size = new System.Drawing.Size(40, 20);
            subpanel1.Controls.Add(tb7);

            b4 = new Button();
            b4.Text = "Принять";
            b4.AutoSize = true;
            b4.Location = new Point(lb7.Location.X + 150, lb7.Location.Y - 2);
            subpanel1.Controls.Add(b4);
            b4.Click += new EventHandler(Товары_Выполнить_Добавление_Click);
            b4.Focus();
        }
        void Товары_Выполнить_Добавление_Click(object sender, EventArgs e)
        {
            if (tb1.Text.Length == 0 || tb2.Text.Length == 0 || tb3.Text.Length == 0 || tb4.Text.Length == 0 || tb5.Text.Length == 0 || tb7.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();

                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "SELECT Max(PRODUCT.Id_Product) FROM PRODUCT";
                command1.Connection = conn;
                int count = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                string sql = "insert into PRODUCT (Id_Product, Name_Product, Type_Product, Amt_on_Sklad, Prodano_za_Month, Amt_Unit, Unit, Expiration_Date, Price) values (@Id_Product, @Name_Product, @Type_Product, @Amt_on_Sklad, @Prodano_za_Month, @Amt_Unit, @Unit, @Expiration_Date, @Price)";
                SqlCommand cmdOrderID = new SqlCommand(sql, conn);
                int i = 0;
                cmdOrderID.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                cmdOrderID.Parameters["@Id_Product"].Value = count;
                cmdOrderID.Parameters.Add(new SqlParameter("@Name_Product", SqlDbType.VarChar));
                cmdOrderID.Parameters["@Name_Product"].Value = tb1.Text;
                cmdOrderID.Parameters.Add(new SqlParameter("@Type_Product", SqlDbType.VarChar));
                cmdOrderID.Parameters["@Type_Product"].Value = type;
                cmdOrderID.Parameters.Add(new SqlParameter("@Amt_on_Sklad", SqlDbType.Int));
                i = Convert.ToInt32(tb2.Text);
                cmdOrderID.Parameters["@Amt_on_Sklad"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Prodano_za_Month", SqlDbType.Int));
                i = Convert.ToInt32(tb3.Text);
                cmdOrderID.Parameters["@Prodano_za_Month"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Amt_Unit", SqlDbType.Int));
                i = Convert.ToInt32(tb4.Text);
                cmdOrderID.Parameters["@Amt_Unit"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Unit", SqlDbType.Int));
                i = Convert.ToInt32(tb5.Text);
                cmdOrderID.Parameters["@Unit"].Value = i;
                cmdOrderID.Parameters.Add(new SqlParameter("@Expiration_Date", SqlDbType.Date));
                cmdOrderID.Parameters["@Expiration_Date"].Value = dtp1.Value;
                cmdOrderID.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int));
                i = Convert.ToInt32(tb7.Text);
                cmdOrderID.Parameters["@Price"].Value = i;


                cmdOrderID.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                Product();
                subpanel1.Controls.Clear();
                panel.Controls.Remove(subpanel);
                this.Size = new Size(1000, 650);
                menu.Focus();
            }
        }

        void OtherMethod()
        {
            subpanel.Controls.Clear();
            subpanel.Location = new Point(0, b1.Location.Y + b1.Height + 15);
            subpanel.Size = new Size(this.Width, 250);
            panel.Controls.Add(subpanel);

            lb1 = new Label();
            lb1.Text = "Выберите категорию товара";
            lb1.Location = new Point(50, 20);
            lb1.AutoSize = true;
            subpanel.Controls.Add(lb1);

            cb1 = new ComboBox();
            cb1.Location = new Point(lb1.Width + lb1.Location.X + 5, lb1.Location.Y - 2);
            cb1.Size = new System.Drawing.Size(200, 25);
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;

            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();
            SqlCommand command1 = new SqlCommand();
            command1.CommandText = "select max(Type_Product) as Type_Product from (select count(Type_Product) as Type_Product from PRODUCT group by PRODUCT.Type_Product) PRODUCT";
            command1.Connection = conn;
            int count = Convert.ToInt32(command1.ExecuteScalar());

            SqlCommand command2 = new SqlCommand();
            command2.CommandText = "select Type_Product from PRODUCT group by PRODUCT.Type_Product";
            command2.Connection = conn;
            SqlDataReader reader = command2.ExecuteReader();
            string[] mas = new string[count];
            string s;
            for (int t = 0; reader.Read() && t < count; t++)
            {
                s = Convert.ToString(reader.GetSqlString(0));
                mas[t] = s;
            }
            reader.Close();
            conn.Close();
            cb1.Items.AddRange(mas);
            cb1.SelectedIndex = 0;

            dgv1.Focus();
            subpanel.Controls.Add(cb1);
        }
        void Товары_Изменить_Click(object sender, EventArgs e)
        {
            Product();
            OtherMethod();
            cb1.SelectedIndexChanged += Товары_Изменить_SelectedIndexChanged;
        }
        void AnotherSomeMethod()
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            dgv1.DataSource = null;

            string sql1 = "SELECT * FROM PRODUCT WHERE Type_Product = @Type_Product";
            SqlCommand cmdOrderID1 = new SqlCommand(sql1, conn);
            cmdOrderID1.Parameters.Add("@Type_Product", SqlDbType.VarChar);
            cmdOrderID1.Parameters["@Type_Product"].Value = cb1.SelectedItem.ToString();
            conn.Open();
            SqlDataReader rdr = cmdOrderID1.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(rdr);
            dgv1.DataSource = dataTable;
            rdr.Close();
            conn.Close();

            subpanel1.Controls.Clear();
            subpanel1.Location = new Point(0, lb1.Location.Y + lb1.Height + 15);
            subpanel1.Size = new Size(this.Width, 250);
            subpanel.Controls.Add(subpanel1);

            lb2 = new Label();
            lb2.Text = "Введите код товара";
            lb2.Location = new Point(50, 10);
            lb2.AutoSize = true;
            subpanel1.Controls.Add(lb2);
            lb2.Focus();

            tb2 = new TextBox();
            tb2.Location = new Point(lb2.Width + lb2.Location.X + 5, lb2.Location.Y - 2);
            tb2.Size = new System.Drawing.Size(40, 25);
            subpanel1.Controls.Add(tb2);

            b4 = new Button();
            b4.AutoSize = true;
            b4.Location = new Point(lb2.Location.X + lb2.Width + tb2.Width + 40, lb2.Location.Y - 2);
            subpanel1.Controls.Add(b4);

        }
        void Товары_Изменить_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnotherSomeMethod();
            b4.Text = "ОК";
            b4.Click += new EventHandler(Изменить_Товары_Click);
        }
        void Изменить_Товары_Click(object sender, EventArgs e)
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
                    subpanel2.Controls.Clear();
                    subpanel2.Location = new Point(0, lb2.Location.Y + lb2.Height + 15);
                    subpanel2.Size = new Size(this.Width, 250);
                    subpanel1.Controls.Add(subpanel2);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    lb3 = new Label();
                    lb3.Text = "Название товара";
                    lb3.AutoSize = true;
                    lb3.Location = new Point(50, 10);
                    subpanel2.Controls.Add(lb3);

                    tb3 = new TextBox();
                    tb3.Text = Convert.ToString(reader.GetSqlString(1));
                    tb3.Location = new Point(lb3.Width + lb3.Location.X + 5, lb3.Location.Y - 2);
                    tb3.Size = new System.Drawing.Size(500, 25);
                    subpanel2.Controls.Add(tb3);

                    lb4 = new Label();
                    lb4.Text = "Amt_on_Sklad";
                    lb4.AutoSize = true;
                    lb4.Location = new Point(lb3.Location.X, lb3.Location.Y + lb3.Height + 25);
                    subpanel2.Controls.Add(lb4);
                    lb4.Focus();

                    tb4 = new TextBox();
                    tb4.Text = Convert.ToString(reader.GetSqlInt32(3));
                    tb4.Location = new Point(lb4.Width + lb4.Location.X + 5, lb4.Location.Y - 2);
                    tb4.Size = new System.Drawing.Size(40, 25);
                    subpanel2.Controls.Add(tb4);

                    lb5 = new Label();
                    lb5.Text = "Prodano_za_Month";
                    lb5.AutoSize = true;
                    lb5.Location = new Point(lb4.Location.X, lb4.Location.Y + lb4.Height + 25);
                    subpanel2.Controls.Add(lb5);
                    lb5.Focus();

                    tb5 = new TextBox();
                    tb5.Text = Convert.ToString(reader.GetSqlInt32(4));
                    tb5.Location = new Point(lb5.Width + lb5.Location.X + 5, lb5.Location.Y - 2);
                    tb5.Size = new System.Drawing.Size(40, 25);
                    subpanel2.Controls.Add(tb5);

                    lb6 = new Label();
                    lb6.Text = "Amt_Unit";
                    lb6.AutoSize = true;
                    lb6.Location = new Point(tb4.Location.X + tb4.Width + 50, tb4.Location.Y);
                    subpanel2.Controls.Add(lb6);
                    lb6.Focus();

                    tb6 = new TextBox();
                    tb6.Text = Convert.ToString(reader.GetSqlInt32(5));
                    tb6.Location = new Point(lb6.Width + lb6.Location.X + 5, lb6.Location.Y - 2);
                    tb6.Size = new System.Drawing.Size(40, 25);
                    subpanel2.Controls.Add(tb6);

                    lb0 = new Label();
                    lb0.Text = "Новая категория";
                    lb0.AutoSize = true;
                    lb0.Location = new Point(tb6.Location.X + tb6.Width + 50, tb6.Location.Y);
                    subpanel2.Controls.Add(lb0);
                    lb0.Focus();

                    tb0 = new TextBox();
                    tb0.Text = Convert.ToString(reader.GetSqlString(2));
                    tb0.Location = new Point(lb0.Width + lb0.Location.X + 5, lb0.Location.Y - 2);
                    tb0.Size = new System.Drawing.Size(250, 25);
                    subpanel2.Controls.Add(tb0);

                    lb7 = new Label();
                    lb7.Text = "Unit";
                    lb7.AutoSize = true;
                    lb7.Location = new Point(tb5.Location.X + tb5.Width + 50, tb5.Location.Y);
                    subpanel2.Controls.Add(lb7);
                    lb7.Focus();

                    tb7 = new TextBox();
                    tb7.Text = Convert.ToString(reader.GetSqlString(6));
                    tb7.Location = new Point(lb7.Width + lb7.Location.X + 5, lb7.Location.Y - 2);
                    tb7.Size = new System.Drawing.Size(40, 25);
                    subpanel2.Controls.Add(tb7);

                    lb8 = new Label();
                    lb8.Text = "Expiration_Date";
                    lb8.AutoSize = true;
                    lb8.Location = new Point(lb5.Location.X, lb5.Location.Y + lb5.Height + 25);
                    subpanel2.Controls.Add(lb8);
                    lb8.Focus();

                    dtp1 = new DateTimePicker();
                    dtp1.Format = DateTimePickerFormat.Short;
                    dtp1.Text = Convert.ToString(reader.GetDateTime(7).ToShortDateString());
                    dtp1.Location = new Point(lb8.Width + lb8.Location.X, lb8.Location.Y - 2);
                    dtp1.Size = new System.Drawing.Size(100, 30);
                    subpanel2.Controls.Add(dtp1);

                    lb9 = new Label();
                    lb9.Text = "Price";
                    lb9.AutoSize = true;
                    lb9.Location = new Point(lb7.Location.X, lb7.Location.Y + lb7.Height + 25);
                    subpanel2.Controls.Add(lb9);
                    lb9.Focus();

                    tb9 = new TextBox();
                    tb9.Text = Convert.ToString(reader.GetSqlInt32(8));
                    tb9.Location = new Point(lb9.Width + lb9.Location.X + 5, lb9.Location.Y - 2);
                    tb9.Size = new System.Drawing.Size(40, 25);
                    subpanel2.Controls.Add(tb9);

                    b4.Dispose();
                    b4 = new Button();
                    b4.Text = "ОК";
                    b4.AutoSize = true;
                    b4.Location = new Point(lb9.Location.X + lb9.Width + tb9.Width + 40, lb9.Location.Y - 2);
                    subpanel2.Controls.Add(b4);
                    b4.Click += new EventHandler(Выполнить_Изменить_Товары_Click);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Выполнить_Изменить_Товары_Click(object sender, EventArgs e)
        {
            if (tb3.Text.Length == 0 || tb4.Text.Length == 0 || tb5.Text.Length == 0 || tb6.Text.Length == 0 || tb7.Text.Length == 0 || tb9.Text.Length == 0)
            {
                MessageBox.Show("Заполните поля", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Form1.connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand();
                string s1 = "update PRODUCT set Name_Product = @Name_Product, Type_Product = @Type_Product, Amt_on_Sklad = @Amt_on_Sklad, ";
                string s2 = "Prodano_za_Month = @Prodano_za_Month, Amt_Unit = @Amt_Unit, Unit = @Unit, Expiration_Date = @Expiration_Date,";
                string s3 = " Price = @Price where Id_Product = @Id_Product";
                command.CommandText = s1 + s2 + s3;
                command.Connection = conn;
                int i = 0;
                command.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                i = Convert.ToInt32(tb2.Text);
                command.Parameters["@Id_Product"].Value = i;
                command.Parameters.Add(new SqlParameter("@Name_Product", SqlDbType.VarChar));
                command.Parameters["@Name_Product"].Value = tb3.Text;
                command.Parameters.Add(new SqlParameter("@Type_Product", SqlDbType.VarChar));
                command.Parameters["@Type_Product"].Value = tb0.Text;
                command.Parameters.Add(new SqlParameter("@Amt_on_Sklad", SqlDbType.Int));
                i = Convert.ToInt32(tb4.Text);
                command.Parameters["@Amt_on_Sklad"].Value = i;
                command.Parameters.Add(new SqlParameter("@Prodano_za_Month", SqlDbType.Int));
                i = Convert.ToInt32(tb5.Text);
                command.Parameters["@Prodano_za_Month"].Value = i;
                command.Parameters.Add(new SqlParameter("@Amt_Unit", SqlDbType.Int));
                i = Convert.ToInt32(tb6.Text);
                command.Parameters["@Amt_Unit"].Value = i;
                command.Parameters.Add(new SqlParameter("@Unit", SqlDbType.VarChar));
                command.Parameters["@Unit"].Value = tb7.Text;
                command.Parameters.Add(new SqlParameter("@Expiration_Date", SqlDbType.Date));
                command.Parameters["@Expiration_Date"].Value = dtp1.Value;
                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int));
                i = Convert.ToInt32(tb9.Text);
                command.Parameters["@Price"].Value = i;

                command.ExecuteNonQuery();
                conn.Close();
                dgv1.DataSource = null;

                Product();
                panel.Controls.Remove(subpanel);
                subpanel1.Controls.Clear();
                this.Size = new Size(1000, 650);
                menu.Focus();
            }
        }


        void Товары_Удалить_Click(object sender, EventArgs e)
        {
            Product();
            OtherMethod();
            cb1.SelectedIndexChanged += Удалить_Товары_Click;
        }
        void Удалить_Товары_Click(object sender, EventArgs e)
        {
            AnotherSomeMethod();
            b4.Text = "Удалить";
            b4.Click += new EventHandler(Выполнить_Удалить_Товары_Click);
        }
        void Выполнить_Удалить_Товары_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * From PRODUCT where Type_Product = @Type_Product AND Id_Product = @Id_Product";
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
                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "delete from PRODUCT where Id_Product = @Id_Product";
                command1.Connection = conn;
                i = 0;

                command1.Parameters.Add(new SqlParameter("@Id_Product", SqlDbType.Int));
                i = Convert.ToInt32(tb2.Text);
                command1.Parameters["@Id_Product"].Value = i;
                i = command1.ExecuteNonQuery();
                conn.Close();

                dgv1.DataSource = null;
                Product();
                menu.Focus();
                panel.Controls.Remove(subpanel);
                this.Size = new Size(1000, 650);
            }
        }


        void STATISTIC_Click(object sender, EventArgs e)
        {
            _Intro();
            this.Size = new Size(1000, 650);
            dgv1.Size = new System.Drawing.Size(693, 350);

            b1 = new Button();
            b1.Text = "Товары в наличии";
            b1.AutoSize = true;
            b1.Location = new Point(dgv1.Location.X + 30, dgv1.Location.Y + dgv1.Height + 35);
            b1.Font = new System.Drawing.Font(b1.Font.Name, 12f);
            panel.Controls.Add(b1);
            b1.Click += new EventHandler(STATISTIC_ProductAmt_Click);

            b2 = new Button();
            b2.Text = "Испорченные";
            b2.AutoSize = true;
            b2.Location = new Point(b1.Location.X, b1.Location.Y + b1.Size.Height + 25);
            b2.Font = new System.Drawing.Font(b2.Font.Name, 12f);
            panel.Controls.Add(b2);
            b2.Click += new EventHandler(STATISTIC_BrokenProducts_Click);
        }
        void STATISTIC_ProductAmt_Click(object sender, EventArgs e)
        {
            dgv1.DataSource = null;
            dgv1.Size = new System.Drawing.Size(693, 350);

            string s1 = "select Name_Product as Наименование, ";
            string s2 = "Type_Product as Тип, ";
            string s3 = "Amt_on_Sklad - Prodano_za_Month as Наличие from PRODUCT";
            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("Substract");
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
        void STATISTIC_BrokenProducts_Click(object sender, EventArgs e)
        {
            dgv1.DataSource = null;
            dgv1.Size = new System.Drawing.Size(693, 350);

            string s1 = "select Name_Product as Наименование, ";
            string s2 = "Type_Product as Тип, ";
            string s3 = "Amt_on_Sklad - Prodano_za_Month as Наличие, ";
            string s4 = "Expiration_Date as Срок_годности from PRODUCT where Expiration_Date < GETDATE() ";
            string s5 = "ORDER BY Expiration_Date Desc";
            SqlConnection cn = new SqlConnection(Form1.connectionString);
            DataSet zapisi = new DataSet();
            DataTable zap = zapisi.Tables.Add("Substract");
            SqlCommand cm = new SqlCommand(s1 + s2 + s3 + s4 + s5, cn);
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
