using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace PCClubApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }

    public class LoginForm : Form
    {
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private PictureBox pictureBox;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(500, 400);
            this.Text = "Киберспортивный Клуб - Авторизация";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "LoginForm";
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.FromArgb(240, 245, 255);

            // Картинка
            this.pictureBox = new PictureBox();
            this.pictureBox.Size = new Size(200, 150);
            this.pictureBox.Location = new Point(150, 20);
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox.BorderStyle = BorderStyle.FixedSingle;
            LoadLoginImage();

            // Заголовок
            this.lblTitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Courier New", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(100, 180);
            this.lblTitle.Size = new Size(300, 26);
            this.lblTitle.Text = "КИБЕРСПОРТИВНЫЙ КЛУБ";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.ForeColor = Color.FromArgb(0, 32, 96);

            // Метка логина
            this.lblUsername = new Label();
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(120, 230);
            this.lblUsername.Size = new Size(80, 13);
            this.lblUsername.Text = "ЛОГИН:";
            this.lblUsername.Font = new Font("Courier New", 10F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(0, 32, 96);

            // Метка пароля
            this.lblPassword = new Label();
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(120, 270);
            this.lblPassword.Size = new Size(80, 13);
            this.lblPassword.Text = "ПАРОЛЬ:";
            this.lblPassword.Font = new Font("Courier New", 10F, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.FromArgb(0, 32, 96);

            // Поле логина
            this.txtUsername = new TextBox();
            this.txtUsername.Location = new Point(220, 227);
            this.txtUsername.Size = new Size(150, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Font = new Font("Courier New", 10F);
            this.txtUsername.Text = "admin";
            this.txtUsername.BackColor = Color.FromArgb(248, 250, 255);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // Поле пароля
            this.txtPassword = new TextBox();
            this.txtPassword.Location = new Point(220, 267);
            this.txtPassword.Size = new Size(150, 25);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Font = new Font("Courier New", 10F);
            this.txtPassword.Text = "1234";
            this.txtPassword.BackColor = Color.FromArgb(248, 250, 255);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Кнопка входа
            this.btnLogin = new Button();
            this.btnLogin.Location = new Point(140, 320);
            this.btnLogin.Size = new Size(100, 35);
            this.btnLogin.Text = "ВОЙТИ";
            this.btnLogin.BackColor = Color.FromArgb(100, 150, 255);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Font = new Font("Courier New", 10F, FontStyle.Bold);
            this.btnLogin.FlatStyle = FlatStyle.Flat;

            // Кнопка выхода
            this.btnExit = new Button();
            this.btnExit.Location = new Point(260, 320);
            this.btnExit.Size = new Size(100, 35);
            this.btnExit.Text = "ВЫХОД";
            this.btnExit.BackColor = Color.FromArgb(150, 150, 180);
            this.btnExit.ForeColor = Color.White;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnExit.Name = "btnExit";
            this.btnExit.Font = new Font("Courier New", 10F, FontStyle.Bold);
            this.btnExit.FlatStyle = FlatStyle.Flat;

            // Добавление элементов на форму
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadLoginImage()
        {
            try
            {
                string imagePath = @"E:\WindowsFormsApplication1\scale_1200.jpg";
                if (File.Exists(imagePath))
                {
                    pictureBox.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Создаем заглушку если изображение не найдено
                    Bitmap placeholder = new Bitmap(200, 150);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        g.Clear(Color.FromArgb(0, 32, 96));
                        using (Font font = new Font("Arial", 12, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            g.DrawString("CYBER CLUB", font, brush, new PointF(30, 50));
                        }
                    }
                    pictureBox.Image = placeholder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthenticateUser(username, password))
            {
                MainForm mainForm = new MainForm(username);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            return (username == "admin" && password == "1234") ||
                   (username == "user" && password == "1235") ||
                   (username == "manager" && password == "1111");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }

    public class MainForm : Form
    {
        private string currentUser;
        private Panel panelMenu;
        private Panel panelContent;
        private Button btnComputers;
        private Button btnTariffs;
        private Button btnBooking;
        private Button btnServices;
        private Button btnStaff;
        private Button btnStats;
        private Button btnTeams;
        private Button btnLogout;
        private Label lblWelcome;
        private Label lblTitle;
        private TextBox txtContent;
        private PictureBox mainPicture;

        public MainForm(string username)
        {
            currentUser = username;
            InitializeComponent();
            lblWelcome.Text = "ДОБРО ПОЖАЛОВАТЬ, " + username.ToUpper() + "!";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(1000, 700);
            this.Text = "Киберспортивный Клуб - Система управления";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.MinimumSize = new Size(800, 600);
            this.BackColor = Color.FromArgb(240, 245, 255);

            this.panelMenu = new Panel();
            this.panelMenu.BackColor = Color.FromArgb(220, 230, 255);
            this.panelMenu.Dock = DockStyle.Left;
            this.panelMenu.Size = new Size(200, 700);
            this.panelMenu.Name = "panelMenu";

            this.panelContent = new Panel();
            this.panelContent.BackColor = Color.FromArgb(248, 250, 255);
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.Location = new Point(200, 0);
            this.panelContent.Size = new Size(800, 700);
            this.panelContent.Name = "panelContent";

            CreateMenuButtons();
            CreateContentElements();

            this.panelMenu.Controls.AddRange(new Control[] {
                btnComputers, btnTariffs, btnBooking, btnServices,
                btnStaff, btnStats, btnTeams, btnLogout
            });

            this.panelContent.Controls.AddRange(new Control[] {
                lblWelcome, lblTitle, txtContent, mainPicture
            });

            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);

            this.ResumeLayout(false);
        }

        private void CreateMenuButtons()
        {
            int buttonHeight = 45;
            int startY = 20;

            this.btnComputers = CreateMenuButton("💻 КОМПЬЮТЕРЫ", startY, btnComputers_Click);
            startY += buttonHeight + 5;
            this.btnTariffs = CreateMenuButton("💰 ТАРИФЫ", startY, btnTariffs_Click);
            startY += buttonHeight + 5;
            this.btnBooking = CreateMenuButton("📅 БРОНИРОВАНИЕ", startY, btnBooking_Click);
            startY += buttonHeight + 5;
            this.btnServices = CreateMenuButton("🛠️ УСЛУГИ", startY, btnServices_Click);
            startY += buttonHeight + 5;
            this.btnStaff = CreateMenuButton("👥 ПЕРСОНАЛ", startY, btnStaff_Click);
            startY += buttonHeight + 5;
            this.btnStats = CreateMenuButton("📊 СТАТИСТИКА", startY, btnStats_Click);
            startY += buttonHeight + 5;
            this.btnTeams = CreateMenuButton("🏆 КОМАНДЫ", startY, btnTeams_Click);
            startY += buttonHeight + 20;
            this.btnLogout = CreateMenuButton("🚪 ВЫХОД", startY, btnLogout_Click);
            this.btnLogout.BackColor = Color.FromArgb(150, 150, 180);
            this.btnLogout.ForeColor = Color.White;
        }

        private Button CreateMenuButton(string text, int top, EventHandler clickHandler)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(10, top);
            button.Size = new Size(180, 45);
            button.Click += clickHandler;
            button.BackColor = Color.FromArgb(240, 245, 255);
            button.Font = new Font("Courier New", 9F, FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.ForeColor = Color.FromArgb(0, 32, 96);
            return button;
        }

        private void CreateContentElements()
        {
            this.lblWelcome = new Label();
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Courier New", 14F, FontStyle.Bold);
            this.lblWelcome.Location = new Point(30, 30);
            this.lblWelcome.Size = new Size(400, 24);
            this.lblWelcome.Text = "ДОБРО ПОЖАЛОВАТЬ!";
            this.lblWelcome.ForeColor = Color.FromArgb(0, 32, 96);
            this.lblWelcome.Name = "lblWelcome";

            this.lblTitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Courier New", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(30, 80);
            this.lblTitle.Size = new Size(400, 26);
            this.lblTitle.Text = "ВЫБЕРИТЕ РАЗДЕЛ В МЕНЮ СЛЕВА";
            this.lblTitle.ForeColor = Color.FromArgb(0, 80, 120);
            this.lblTitle.Name = "lblTitle";

            this.txtContent = new TextBox();
            this.txtContent.Location = new Point(30, 120);
            this.txtContent.Multiline = true;
            this.txtContent.Size = new Size(500, 400);
            this.txtContent.ScrollBars = ScrollBars.Vertical;
            this.txtContent.ReadOnly = true;
            this.txtContent.Font = new Font("Courier New", 10F);
            this.txtContent.Text = "ДОБРО ПОЖАЛОВАТЬ В СИСТЕМУ УПРАВЛЕНИЯ КИБЕРСПОРТИВНЫМ КЛУБОМ!\n\n" +
                                  "ДЛЯ ПРОСМОТРА ИНФОРМАЦИИ ВЫБЕРИТЕ СООТВЕТСТВУЮЩИЙ РАЗДЕЛ В МЕНЮ СЛЕВА.";
            this.txtContent.Name = "txtContent";
            this.txtContent.BorderStyle = BorderStyle.FixedSingle;
            this.txtContent.BackColor = Color.White;
            this.txtContent.ForeColor = Color.FromArgb(0, 32, 96);

            // Основное изображение
            this.mainPicture = new PictureBox();
            this.mainPicture.Location = new Point(550, 120);
            this.mainPicture.Size = new Size(200, 200);
            this.mainPicture.SizeMode = PictureBoxSizeMode.Zoom;
            this.mainPicture.BorderStyle = BorderStyle.FixedSingle;
            LoadMainImage();
        }

        private void LoadMainImage()
        {
            try
            {
                string imagePath = @"E:\WindowsFormsApplication1\maxresdefault.jpg";
                if (File.Exists(imagePath))
                {
                    mainPicture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Заглушка если изображение не найдено
                    Bitmap placeholder = new Bitmap(200, 200);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        g.Clear(Color.FromArgb(0, 32, 96));
                        using (Font font = new Font("Arial", 14, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            g.DrawString("CYBER\nCLUB\nMAIN", font, brush, new PointF(30, 60));
                        }
                    }
                    mainPicture.Image = placeholder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }

        private void btnComputers_Click(object sender, EventArgs e)
        {
            string computersText =
                "             ИГРОВЫЕ КОМПЬЮТЕРЫ             \n\n" +
                "НАЗВАНИЕ          ВИДЕОКАРТА    ПРОЦЕССОР    СТАТУС   \n" +
                "------------------------------------------------------\n" +
                "GAMING PC-001     RTX 4080      i9-13900K    СВОБОДЕН \n" +
                "GAMING PC-002     RTX 4070      i7-13700K    ЗАНЯТ    \n" +
                "GAMING PC-003     RTX 4060      i5-13600K    СВОБОДЕН \n" +
                "STREAMING PC-001  ДВОЙНАЯ СИСТЕМА            СВОБОДЕН \n" +
                "VR STATION        HTC VIVE PRO 2             РЕМОНТ   \n" +
                "TOURNAMENT PC-01  RTX 4090      i9-14900K    СВОБОДЕН \n" +
                "TOURNAMENT PC-02  RTX 4090      i9-14900K    СВОБОДЕН \n\n" +
                "ВСЕГО КОМПЬЮТЕРОВ: 7\n" +
                "СВОБОДНЫХ: 5\n" +
                "ЗАНЯТЫХ: 1\n" +
                "В РЕМОНТЕ: 1";

            ShowContent("💻 КОМПЬЮТЕРЫ", computersText);
            LoadComputersImage();
        }

        private void btnTariffs_Click(object sender, EventArgs e)
        {
            string tariffsText =
                "                 ИГРОВЫЕ ТАРИФЫ                 \n\n" +
                "ТАРИФ          ЦЕНА/ЧАС    ЧАСЫ ДОСТУПА   БРОНИРОВАНИЕ\n" +
                "------------------------------------------------------\n" +
                "СТАНДАРТ       200 руб     08:00-23:00    ДА          \n" +
                "ПРЕМИУМ        300 руб     08:00-23:00    ДА          \n" +
                "НОЧНОЙ         150 руб     23:00-08:00    НЕТ         \n" +
                "ТУРНИРНЫЙ      400 руб     10:00-22:00    ДА          \n" +
                "СТРИМ          350 руб     КРУГЛОСУТОЧНО  ДА          \n\n" +
                "ПАКЕТ 10 ЧАСОВ  1,700 руб   ЛЮБОЕ ВРЕМЯ    ДА          \n" +
                "ПАКЕТ 50 ЧАСОВ  7,500 руб   ЛЮБОЕ ВРЕМЯ    ДА          \n" +
                "ПАКЕТ 100 ЧАСОВ 14,000 руб  ЛЮБОЕ ВРЕМЯ    ДА          ";

            ShowContent("💰 ТАРИФЫ", tariffsText);
            LoadTariffsImage();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            string bookingText =
                "               ПРАВИЛА БРОНИРОВАНИЯ               \n\n" +
                "ПАРАМЕТР           ЗНАЧЕНИЕ                      \n" +
                "--------------------------------------------------\n" +
                "МИНИМАЛЬНОЕ ВРЕМЯ  2 ЧАСА                       \n" +
                "МАКСИМАЛЬНОЕ ВРЕМЯ 8 ЧАСОВ                      \n" +
                "ТУРНИРНЫЕ ДНИ      БРОНЬ ЗА 3 ДНЯ              \n" +
                "ПРЕДОПЛАТА         30% ОТ СУММЫ                \n" +
                "ОТМЕНА ЗА 24 ЧАСА  ВОЗВРАТ 100%                \n" +
                "ОТМЕНА ЗА 12 ЧАСОВ ВОЗВРАТ 50%                 \n" +
                "ОТМЕНА МЕНЕЕ 12 Ч  ВОЗВРАТ 0%                  \n" +
                "БРОНЬ ВЫХОДНЫХ     +20% К СТОИМОСТИ            \n" +
                "БРОНЬ ПРАЗДНИКОВ   +50% К СТОИМОСТИ            \n\n" +
                "⚡ БРОНИРОВАНИЕ ДОСТУПНО ТОЛЬКО ДЛЯ КЛИЕНТОВ С РЕГИСТРАЦИЕЙ";

            ShowContent("📅 БРОНИРОВАНИЕ", bookingText);
            LoadBookingImage();
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            string servicesText =
                "             ДОПОЛНИТЕЛЬНЫЕ УСЛУГИ              \n\n" +
                "УСЛУГА             ЦЕНА         ВРЕМЯ        ДОСТУПНО\n" +
                "------------------------------------------------------\n" +
                "КОУЧИНГ DOTA 2     500 руб/час  2-6 часов    ДА       \n" +
                "КОУЧИНГ CS:GO      500 руб/час  2-6 часов    ДА       \n" +
                "КОУЧИНГ VALORANT   450 руб/час  2-6 часов    ДА       \n" +
                "ЗАПИСЬ ГЕЙМПЛЕЯ    200 руб      1-8 часов    ДА       \n" +
                "МОНТАЖ ВИДЕО       300 руб/час  1-24 часов   ДА       \n" +
                "ФОТОСЕССИЯ         1,000 руб    1 час         ДА       \n" +
                "КИБЕРСПОРТ ФОРМА   2,000 руб    ИНДИВИДУАЛЬНО ДА       \n" +
                "АНАЛИЗ ИГРЫ        400 руб      30 мин        ДА       \n" +
                "ТРЕНИРОВКА КОМАНДЫ 2,000 руб/ч  2-8 часов    ДА       ";

            ShowContent("🛠️ УСЛУГИ", servicesText);
            LoadServicesImage();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            string staffText =
                "                   НАША КОМАНДА                   \n\n" +
                "ИМЯ       ДОЛЖНОСТЬ           ТЕЛЕФОН       ГРАФИК   \n" +
                "------------------------------------------------------\n" +
                "АЛЕКСЕЙ   ГЛАВНЫЙ АДМИН      +7-XXX-XXX-01  С 10:00  \n" +
                "МАРИЯ     КООРДИНАТОР        +7-XXX-XXX-02  С 12:00  \n" +
                "ДМИТРИЙ   ТЕХНИК             +7-XXX-XXX-03  С 09:00  \n" +
                "ОЛЬГА     КОУЧ DOTA 2        +7-XXX-XXX-04  С 14:00  \n" +
                "ИВАН      КОУЧ CS:GO         +7-XXX-XXX-05  С 16:00  \n" +
                "АННА      АДМИНИСТРАТОР      +7-XXX-XXX-06  С 18:00  \n" +
                "СЕРГЕЙ    СИСТЕМНЫЙ АДМИН    +7-XXX-XXX-07  С 08:00  \n" +
                "ЕЛЕНА     МАРКЕТОЛОГ         +7-XXX-XXX-08  С 11:00  \n\n" +
                "📞 ТЕХНИЧЕСКАЯ ПОДДЕРЖКА: +7-800-XXX-XXXX\n" +
                "📧 EMAIL: SUPPORT@CYBERCLUB.RU";

            ShowContent("👥 ПЕРСОНАЛ", staffText);
            LoadStaffImage();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            string statsText =
                "                 СТАТИСТИКА КЛУБА                 \n\n" +
                "ПОКАЗАТЕЛЬ          ЗНАЧЕНИЕ     ИЗМЕНЕНИЕ    ТРЕНД\n" +
                "----------------------------------------------------\n" +
                "ПОСЕЩАЕМОСТЬ        85%          +5%          ▲     \n" +
                "СРЕДНИЙ ЧЕК         450 руб      +50 руб      ▲     \n" +
                "ТУРНИРОВ ПРОВЕДЕНО  12           +3           ▲     \n" +
                "УЧАСТНИКОВ          240          +40          ▲     \n" +
                "ПРИЗОВОЙ ФОНД      150,000 руб  +50,000      ▲     \n" +
                "КЛИЕНТОВ В МЕСЯЦ   320          +45          ▲     \n" +
                "ВЫРУЧКА В МЕСЯЦ    144,000 руб  +18,000      ▲     \n" +
                "ОКУПАЕМОСТЬ         78%          +8%          ▲     \n" +
                "РЕЙТИНГ КЛУБА      4.7/5.0      +0.2         ▲     \n\n" +
                "📈 ПОЛОЖИТЕЛЬНАЯ ДИНАМИКА ПО ВСЕМ ПОКАЗАТЕЛЯМ";

            ShowContent("📊 СТАТИСТИКА", statsText);
            LoadStatsImage();
        }

        private void btnTeams_Click(object sender, EventArgs e)
        {
            TeamsForm teamsForm = new TeamsForm();
            teamsForm.ShowDialog();
        }

        private void LoadComputersImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\j_3tYV6wAVnWdRxLQm-3cW.jpg");
        }

        private void LoadTariffsImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\qHjkC2z2BpwEGOShT0-cDd.jpg");
        }

        private void LoadBookingImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\scale_1200.jpg");
        }

        private void LoadServicesImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\maxresdefault.jpg");
        }

        private void LoadStaffImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\j_3tYV6wAVnWdRxLQm-3cW.jpg");
        }

        private void LoadStatsImage()
        {
            LoadSectionImage(@"E:\WindowsFormsApplication1\qHjkC2z2BpwEGOShT0-cDd.jpg");
        }

        private void LoadSectionImage(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    mainPicture.Image = Image.FromFile(imagePath);
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки загрузки изображений для разделов
            }
        }

        private void ShowContent(string title, string content)
        {
            lblTitle.Text = title;
            txtContent.Text = content;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }
    }

    // Класс для хранения данных команды
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string Captain { get; set; }
        public string Game { get; set; }
        public string Players { get; set; }
        public string WorldRanking { get; set; }
        public string Coach { get; set; }
        public string Achievements { get; set; }
        public string Sponsors { get; set; }
    }

    // Форма для управления киберспортивными командами
    public class TeamsForm : Form
    {
        private TextBox txtID;
        private TextBox txtTeamName;
        private TextBox txtCaptain;
        private TextBox txtGame;
        private TextBox txtPlayers;
        private TextBox txtRanking;
        private TextBox txtCoach;
        private TextBox txtAchievements;
        private TextBox txtSponsors;
        private Button btnAdd;
        private Button btnClear;
        private Button btnView;
        private Button btnSaveToAccess;
        private Button btnExportExcel;
        private Label lblTitle;
        private Panel panelButtons;
        private PictureBox teamPicture;
        private string dataFilePath;
        private string accessDbPath;

        public TeamsForm()
        {
            dataFilePath = Path.Combine(Application.StartupPath, "teams_data.txt");
            accessDbPath = Path.Combine(Application.StartupPath, "EsportsTeams.accdb");
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(900, 650);
            this.Text = "Управление киберспортивными командами";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 255);

            // Заголовок
            lblTitle = new Label();
            lblTitle.Text = "УПРАВЛЕНИЕ КИБЕРСПОРТИВНЫМИ КОМАНДАМИ";
            lblTitle.Font = new Font("Courier New", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 32, 96);
            lblTitle.Location = new Point(150, 20);
            lblTitle.Size = new Size(600, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Поля ввода
            CreateInputFields();

            // Панель для кнопок
            panelButtons = new Panel();
            panelButtons.Location = new Point(150, 450);
            panelButtons.Size = new Size(600, 150);
            panelButtons.BackColor = Color.FromArgb(220, 230, 255);

            // Картинка для команд
            teamPicture = new PictureBox();
            teamPicture.Location = new Point(550, 80);
            teamPicture.Size = new Size(300, 200);
            teamPicture.SizeMode = PictureBoxSizeMode.Zoom;
            teamPicture.BorderStyle = BorderStyle.FixedSingle;
            LoadTeamImage();

            // Создаем кнопки в красивом расположении
            CreateButtonsLayout();

            this.Controls.AddRange(new Control[] {
                lblTitle, txtID, txtTeamName, txtCaptain, txtGame, txtPlayers,
                txtRanking, txtCoach, txtAchievements, txtSponsors, panelButtons, teamPicture
            });

            this.ResumeLayout(false);
        }

        private void CreateInputFields()
        {
            int yPos = 80;
            int labelWidth = 150;
            int textBoxWidth = 250;
            int spacing = 35;

            // ID КОМАНДЫ
            CreateLabel("ID КОМАНДЫ:", 50, yPos);
            txtID = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // НАЗВАНИЕ КОМАНДЫ
            CreateLabel("НАЗВАНИЕ КОМАНДЫ:", 50, yPos);
            txtTeamName = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // КАПИТАН
            CreateLabel("КАПИТАН:", 50, yPos);
            txtCaptain = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // ДИСЦИПЛИНА
            CreateLabel("ДИСЦИПЛИНА:", 50, yPos);
            txtGame = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // СОСТАВ ИГРОКОВ
            CreateLabel("СОСТАВ ИГРОКОВ:", 50, yPos);
            txtPlayers = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // МИРОВОЙ РЕЙТИНГ
            CreateLabel("МИРОВОЙ РЕЙТИНГ:", 50, yPos);
            txtRanking = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // ТРЕНЕР
            CreateLabel("ТРЕНЕР:", 50, yPos);
            txtCoach = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // ДОСТИЖЕНИЯ
            CreateLabel("ДОСТИЖЕНИЯ:", 50, yPos);
            txtAchievements = CreateTextBox(220, yPos, textBoxWidth);
            yPos += spacing;

            // СПОНСОРЫ
            CreateLabel("СПОНСОРЫ:", 50, yPos);
            txtSponsors = CreateTextBox(220, yPos, textBoxWidth);
        }

        private void CreateLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(150, 25);
            label.Font = new Font("Courier New", 9, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(0, 32, 96);
            this.Controls.Add(label);
        }

        private TextBox CreateTextBox(int x, int y, int width)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 25);
            textBox.Font = new Font("Courier New", 9);
            textBox.BackColor = Color.FromArgb(248, 250, 255);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            return textBox;
        }

        private void LoadTeamImage()
        {
            try
            {
                string imagePath = @"E:\WindowsFormsApplication1\j_3tYV6wAVnWdRxLQm-3cW.jpg";
                if (File.Exists(imagePath))
                {
                    teamPicture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Заглушка если изображение не найдено
                    Bitmap placeholder = new Bitmap(300, 200);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        g.Clear(Color.FromArgb(0, 32, 96));
                        using (Font font = new Font("Arial", 16, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            g.DrawString("TEAMS\nMANAGEMENT", font, brush, new PointF(50, 70));
                        }
                    }
                    teamPicture.Image = placeholder;
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки загрузки изображения
            }
        }

        private void CreateButtonsLayout()
        {
            int buttonWidth = 120;
            int buttonHeight = 40;
            int spacing = 20;

            // Первый ряд кнопок
            btnAdd = CreateButton("ДОБАВИТЬ В ФАЙЛ", 50, 20, buttonWidth, buttonHeight);
            btnAdd.BackColor = Color.FromArgb(100, 200, 100);
            btnAdd.Click += BtnAdd_Click;

            btnView = CreateButton("ПРОСМОТР", 50 + buttonWidth + spacing, 20, buttonWidth, buttonHeight);
            btnView.BackColor = Color.FromArgb(100, 150, 255);
            btnView.Click += BtnView_Click;

            btnSaveToAccess = CreateButton("СОХРАНИТЬ В ACCESS", 50 + (buttonWidth + spacing) * 2, 20, buttonWidth, buttonHeight);
            btnSaveToAccess.BackColor = Color.FromArgb(150, 100, 255);
            btnSaveToAccess.Click += BtnSaveToAccess_Click;

            // Второй ряд кнопок
            btnExportExcel = CreateButton("ЭКСПОРТ В EXCEL", 50, 20 + buttonHeight + spacing, buttonWidth, buttonHeight);
            btnExportExcel.BackColor = Color.FromArgb(255, 150, 100);
            btnExportExcel.Click += BtnExportExcel_Click;

            btnClear = CreateButton("ОЧИСТИТЬ", 50 + buttonWidth + spacing, 20 + buttonHeight + spacing, buttonWidth, buttonHeight);
            btnClear.BackColor = Color.FromArgb(150, 150, 180);
            btnClear.Click += BtnClear_Click;

            // Добавляем кнопки на панель
            panelButtons.Controls.AddRange(new Control[] {
                btnAdd, btnView, btnSaveToAccess, btnExportExcel, btnClear
            });
        }

        private Button CreateButton(string text, int x, int y, int width, int height)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Font = new Font("Courier New", 8, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            return button;
        }

        // Сохранение команды в текстовый файл
        private void SaveTeamToFile(Team team)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath, true, Encoding.UTF8))
                {
                    string line = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                        team.TeamID, team.TeamName, team.Captain, team.Game,
                        team.Players, team.WorldRanking, team.Coach,
                        team.Achievements, team.Sponsors);
                    writer.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения данных: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Сохранение команды в базу данных Access
        private void SaveTeamToAccess(Team team)
        {
            try
            {
                if (!File.Exists(accessDbPath))
                {
                    if (!CreateAccessDatabase())
                    {
                        MessageBox.Show("Не удалось создать базу данных Access. Убедитесь, что установлен Microsoft Access Database Engine.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + accessDbPath;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Создаем таблицу если она не существует
                    string createTableQuery = @"CREATE TABLE Teams (
                            TeamID INTEGER PRIMARY KEY,
                            TeamName TEXT NOT NULL,
                            Captain TEXT,
                            Game TEXT,
                            Players TEXT,
                            WorldRanking TEXT,
                            Coach TEXT,
                            Achievements TEXT,
                            Sponsors TEXT
                        )";

                    try
                    {
                        using (OleDbCommand createCommand = new OleDbCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // Таблица уже существует - это нормально
                    }

                    // Вставляем данные
                    string insertQuery = @"INSERT INTO Teams (TeamID, TeamName, Captain, Game, Players, WorldRanking, Coach, Achievements, Sponsors)
                        VALUES (@TeamID, @TeamName, @Captain, @Game, @Players, @WorldRanking, @Coach, @Achievements, @Sponsors)";

                    using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@TeamID", team.TeamID);
                        insertCommand.Parameters.AddWithValue("@TeamName", team.TeamName);
                        insertCommand.Parameters.AddWithValue("@Captain", team.Captain ?? "");
                        insertCommand.Parameters.AddWithValue("@Game", team.Game ?? "");
                        insertCommand.Parameters.AddWithValue("@Players", team.Players ?? "");
                        insertCommand.Parameters.AddWithValue("@WorldRanking", team.WorldRanking ?? "");
                        insertCommand.Parameters.AddWithValue("@Coach", team.Coach ?? "");
                        insertCommand.Parameters.AddWithValue("@Achievements", team.Achievements ?? "");
                        insertCommand.Parameters.AddWithValue("@Sponsors", team.Sponsors ?? "");

                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения в Access: " + ex.Message +
                    "\n\nУбедитесь, что установлен Microsoft Access Database Engine 2016.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CreateAccessDatabase()
        {
            try
            {
                // Простой способ создания Access базы данных - через ADOX
                try
                {
                    Type catalogType = Type.GetTypeFromProgID("ADOX.Catalog");
                    if (catalogType != null)
                    {
                        dynamic catalog = Activator.CreateInstance(catalogType);
                        catalog.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + accessDbPath);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(catalog);
                        return true;
                    }
                }
                catch
                {
                    // Альтернативный способ - создаем пустой файл с правильным заголовком
                    try
                    {
                        byte[] accessHeader = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4A, 0x65, 0x74, 0x20, 0x44, 0x42 };
                        File.WriteAllBytes(accessDbPath, accessHeader);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private List<Team> ReadTeamsFromFile()
        {
            List<Team> teams = new List<Team>();

            if (!File.Exists(dataFilePath))
                return teams;

            try
            {
                using (StreamReader reader = new StreamReader(dataFilePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 9)
                        {
                            Team team = new Team
                            {
                                TeamID = int.Parse(parts[0]),
                                TeamName = parts[1],
                                Captain = parts[2],
                                Game = parts[3],
                                Players = parts[4],
                                WorldRanking = parts[5],
                                Coach = parts[6],
                                Achievements = parts[7],
                                Sponsors = parts[8]
                            };
                            teams.Add(team);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения данных: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return teams;
        }

        private List<Team> ReadTeamsFromAccess()
        {
            List<Team> teams = new List<Team>();

            if (!File.Exists(accessDbPath))
                return teams;

            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + accessDbPath;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Teams";
                    using (OleDbCommand selectCommand = new OleDbCommand(selectQuery, connection))
                    using (OleDbDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Team team = new Team
                            {
                                TeamID = reader.GetInt32(0),
                                TeamName = reader.GetString(1),
                                Captain = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Game = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Players = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                WorldRanking = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Coach = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Achievements = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Sponsors = reader.IsDBNull(8) ? "" : reader.GetString(8)
                            };
                            teams.Add(team);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения из Access: " + ex.Message +
                    "\n\nУбедитесь, что установлен Microsoft Access Database Engine 2016.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return teams;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTeamName.Text))
                {
                    MessageBox.Show("Заполните обязательные поля: ID команды и Название команды!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int teamID = Convert.ToInt32(txtID.Text);
                string teamName = txtTeamName.Text;
                string captain = txtCaptain.Text ?? "";
                string game = txtGame.Text ?? "";
                string players = txtPlayers.Text ?? "";
                string ranking = txtRanking.Text ?? "";
                string coach = txtCoach.Text ?? "";
                string achievements = txtAchievements.Text ?? "";
                string sponsors = txtSponsors.Text ?? "";

                List<Team> existingTeams = ReadTeamsFromFile();
                bool teamExists = existingTeams.Exists(t => t.TeamID == teamID);

                if (teamExists)
                {
                    MessageBox.Show("Команда с таким ID уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Team newTeam = new Team
                {
                    TeamID = teamID,
                    TeamName = teamName,
                    Captain = captain,
                    Game = game,
                    Players = players,
                    WorldRanking = ranking,
                    Coach = coach,
                    Achievements = achievements,
                    Sponsors = sponsors
                };

                SaveTeamToFile(newTeam);

                MessageBox.Show("Команда успешно добавлена в текстовый файл!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnClear_Click(null, null);
            }
            catch (FormatException)
            {
                MessageBox.Show("ID команды должен быть числом!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveToAccess_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTeamName.Text))
                {
                    MessageBox.Show("Заполните обязательные поля: ID команды и Название команды!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int teamID = Convert.ToInt32(txtID.Text);
                string teamName = txtTeamName.Text;
                string captain = txtCaptain.Text ?? "";
                string game = txtGame.Text ?? "";
                string players = txtPlayers.Text ?? "";
                string ranking = txtRanking.Text ?? "";
                string coach = txtCoach.Text ?? "";
                string achievements = txtAchievements.Text ?? "";
                string sponsors = txtSponsors.Text ?? "";

                List<Team> existingTeams = ReadTeamsFromAccess();
                bool teamExists = existingTeams.Exists(t => t.TeamID == teamID);

                if (teamExists)
                {
                    MessageBox.Show("Команда с таким ID уже существует в базе данных Access!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Team newTeam = new Team
                {
                    TeamID = teamID,
                    TeamName = teamName,
                    Captain = captain,
                    Game = game,
                    Players = players,
                    WorldRanking = ranking,
                    Coach = coach,
                    Achievements = achievements,
                    Sponsors = sponsors
                };

                SaveTeamToAccess(newTeam);

                MessageBox.Show("Команда успешно сохранена в базу данных Access!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnClear_Click(null, null);
            }
            catch (FormatException)
            {
                MessageBox.Show("ID команды должен быть числом!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения в Access: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files|*.csv";
                saveFileDialog.Title = "Экспорт команд в CSV";
                saveFileDialog.FileName = "EsportsTeams_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show("Экспортировать из текстового файла (Да) или из Access (Нет)?",
                        "Выбор источника", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    List<Team> teams = new List<Team>();

                    if (result == DialogResult.Yes)
                    {
                        teams = ReadTeamsFromFile();
                    }
                    else if (result == DialogResult.No)
                    {
                        teams = ReadTeamsFromAccess();
                    }
                    else
                    {
                        return;
                    }

                    if (teams.Count == 0)
                    {
                        MessageBox.Show("Нет данных для экспорта!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Создаем CSV файл
                    StringBuilder csvContent = new StringBuilder();

                    // Заголовки
                    csvContent.AppendLine("ID;Название;Капитан;Дисциплина;Состав;Рейтинг;Тренер;Достижения;Спонсоры");

                    // Данные
                    foreach (Team team in teams)
                    {
                        csvContent.AppendLine(team.TeamID + ";" +
                                            EscapeCsvValue(team.TeamName) + ";" +
                                            EscapeCsvValue(team.Captain) + ";" +
                                            EscapeCsvValue(team.Game) + ";" +
                                            EscapeCsvValue(team.Players) + ";" +
                                            EscapeCsvValue(team.WorldRanking) + ";" +
                                            EscapeCsvValue(team.Coach) + ";" +
                                            EscapeCsvValue(team.Achievements) + ";" +
                                            EscapeCsvValue(team.Sponsors));
                    }

                    File.WriteAllText(saveFileDialog.FileName, csvContent.ToString(), Encoding.UTF8);

                    MessageBox.Show("Экспортировано " + teams.Count + " команд в CSV файл!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте в CSV: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            if (value.Contains(";") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Показать команды из текстового файла (Да) или из базы данных Access (Нет)?",
                    "Выбор источника",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                List<Team> teams = new List<Team>();

                if (result == DialogResult.Yes)
                {
                    teams = ReadTeamsFromFile();
                }
                else if (result == DialogResult.No)
                {
                    teams = ReadTeamsFromAccess();
                }
                else
                {
                    return;
                }

                StringBuilder teamsInfo = new StringBuilder();
                teamsInfo.AppendLine("             КИБЕРСПОРТИВНЫЕ КОМАНДЫ             \n");
                teamsInfo.AppendLine("ID  КОМАНДА        КАПИТАН   ДИСЦИПЛИНА  РЕЙТИНГ");
                teamsInfo.AppendLine("--------------------------------------------------");

                foreach (Team team in teams)
                {
                    teamsInfo.AppendLine(string.Format("{0,-3} {1,-13} {2,-9} {3,-10} {4,-10}",
                        team.TeamID,
                        team.TeamName.Length > 13 ? team.TeamName.Substring(0, 10) + "..." : team.TeamName,
                        team.Captain.Length > 9 ? team.Captain.Substring(0, 6) + "..." : team.Captain,
                        team.Game.Length > 10 ? team.Game.Substring(0, 7) + "..." : team.Game,
                        team.WorldRanking));
                }

                teamsInfo.AppendLine("--------------------------------------------------");
                teamsInfo.AppendLine("ВСЕГО КОМАНД: " + teams.Count);

                MessageBox.Show(teamsInfo.ToString(), "Список киберспортивных команд",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении данных: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtTeamName.Text = "";
            txtCaptain.Text = "";
            txtGame.Text = "";
            txtPlayers.Text = "";
            txtRanking.Text = "";
            txtCoach.Text = "";
            txtAchievements.Text = "";
            txtSponsors.Text = "";
            txtTeamName.Focus();
        }
    }
}