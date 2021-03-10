using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using Tesseract;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using gma.System.Windows;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static Bitmap PrintScreen(Point fP, Point sP)
        {
            int c = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Hardware Profiles\\Current\\Software\\Fonts", "LogPixels", 96);
            Bitmap printscreen = new Bitmap((int)(Screen.PrimaryScreen.Bounds.Size.Width * (c / 96d)),
                (int)(Screen.PrimaryScreen.Bounds.Size.Height * (c / 96d)));
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            if (Math.Abs(fP.X - sP.X) < 5 || Math.Abs(fP.Y - sP.Y) < 5)
            {
                MessageBox.Show("Слишком маленькая область экрана. Повторите выбор области.");
                return null;
            }
            else
            {
            return printscreen.Clone(new Rectangle(new Point(Math.Min(fP.X, sP.X), Math.Min(fP.Y, sP.Y)), 
                new Size(Math.Abs(sP.X - fP.X), Math.Abs(sP.Y - fP.Y))), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
        }

        /// <summary>
        /// Эмуляция нажатия кнопки Prt Scr. Скриншот помещается по адресу "D:/1.jpeg", после чего область около курсора обрезается до прямоугольника. 
        /// </summary>
        /// <param name="mcords"> 
        /// Координаты курсора относительно экрана. 
        /// </param>
        public void PrintScreen(Point mcords)
        {
            Bitmap printscreen = new Bitmap(ScreenWidth, ScreenHeigth);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            Rectangle r = new Rectangle();
            //проверяю выход за границы экрана области отреза.
            if (mcords.X < 150 || mcords.Y < 50)
                switch (mcords.X < 150)
                {
                    case true:
                        {
                            switch (mcords.Y < 50)
                            {
                                case true:
                                    {
                                        r = new Rectangle(0, 0, 300, 100);
                                        break;
                                    }
                                case false:
                                    {
                                        r = new Rectangle(0, mcords.Y - 50, 300, 100);
                                        break;
                                    }
                            }
                            break;
                        }
                    case false:
                        {
                            switch (mcords.Y < 50)
                            {
                                case true:
                                    {
                                        r = new Rectangle(mcords.X - 150, 0, 300, 100);
                                        break;
                                    }
                                case false:
                                    {
                                        r = new Rectangle(mcords.X - 150, mcords.Y - 50, 300, 100);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            else if (mcords.X > ScreenWidth - 150 || mcords.Y > ScreenHeigth - 50)
                switch (mcords.X + 150 > ScreenWidth)
                {
                    case true:
                        {
                            switch (mcords.Y + 50 > ScreenHeigth)
                            {
                                case true:
                                    {
                                        r = new Rectangle(mcords.X - 300, mcords.Y - 100, 300, 100);
                                        break;
                                    }
                                case false:
                                    {
                                        r = new Rectangle(mcords.X - 300, mcords.Y - 50, 300, 100);
                                        break;
                                    }
                            }
                            break;
                        }
                    case false:
                        {
                            switch (mcords.Y + 50 > ScreenHeigth)
                            {
                                case true:
                                    {
                                        r = new Rectangle(mcords.X - 150, mcords.Y - 100, 300, 100);
                                        break;
                                    }
                                case false:
                                    {
                                        r = new Rectangle(mcords.X - 150, mcords.Y - 50, 300, 100);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            else
                r = new Rectangle(mcords.X - 150, mcords.Y - 50, 300, 100);
            using (Bitmap b = (Bitmap)printscreen.Clone())
                b.Save("Screen.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            using (Bitmap b = printscreen.Clone(r, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                b.Save("Area.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                Operations.CharacterRecognition(b);
            }
        }

        readonly double scallingCoefficent;

        /// <summary>
        /// Ширина экрана и высота зависят от масштабирования. Его цепляю по пути из регистра и ищу коэффицент масштабирования.
        /// </summary>
        readonly int ScreenWidth;

        /// <summary>
        /// Ширина экрана и высота зависят от масштабирования. Его цепляю по пути из регистра и ищу коэффицент масштабирования.
        /// </summary>
        readonly int ScreenHeigth;
        Point mousePos;
        Point firstCoords;
        Point secondCoords;
        UserActivityHook actHook = new UserActivityHook();
        bool fP = false;

        /// <summary>
        /// Таймер, проверяющий, поменялось ли расположение мыши на экране в течение секунды.
        /// </summary>
        /// <param name="sender"> Формальность.</param>
        /// <param name="e"> Формальность.....</param>
        void timer_Tick(object sender, EventArgs e)
        {
            bool doesCheatExists = false;
            foreach (Form elem in Application.OpenForms)
                if (elem.GetType() == typeof(Form3))
                    doesCheatExists = true;
            if (mousePos == Cursor.Position)
            {
                if (!doesCheatExists)
                {
                    PrintScreen(new Point((int)(Cursor.Position.X * scallingCoefficent), (int)(Cursor.Position.Y * scallingCoefficent)));
                }
            }
            else
                mousePos = Cursor.Position;
        }
        /// <summary>
        /// Стартуем! Класс с меню в трее.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //Инициализация констант
            //--------------------------------------------------
            scallingCoefficent = (double)(((int)Registry.GetValue(
            "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Hardware Profiles\\Current\\Software\\Fonts", "LogPixels", 96)) / 96d);
            ScreenHeigth = (int)(scallingCoefficent * Screen.PrimaryScreen.Bounds.Height);
            ScreenWidth = (int)(scallingCoefficent * Screen.PrimaryScreen.Bounds.Width);  
            //---------------------------------------------------
            DirectoryInfo defDir = new DirectoryInfo("./AppData/Definitions_Words");
            if (!defDir.Exists)
                defDir.Create();
            defDir = new DirectoryInfo("./AppData/Definitions_Images");
            if (!defDir.Exists)
                defDir.Create();
            contextMenuStrip1.Items.Add("О программе", null, (e, a) => {MessageBox.Show(
                "Программа\"Lazy Reader\" предназначена для упрощения процесса чтения книг.\n" +
                "Разработана Ильей Шмелевым(Саратов). \n" + "Во вкладке \"Как пользоваться программой? \" можно найти гайд по программке) \n Жду отзывов и предложений) "); });
            contextMenuStrip1.Items.Add("Как пользоваться программой?", null, (e, a) => { new Form5().Show(); });
            contextMenuStrip1.Items.Add("Редактирование определений", null, (e, a) => { new Form4().Show(); });
            contextMenuStrip1.Items.Add("Добавить слово", null, (e, a) => {MessageBox.Show("Выделите область экрана."); actHook.Start(); });
            contextMenuStrip1.Items.Add("Выход", null, (e, a) => { Environment.Exit(0); });
            //Глобальный хук для отслеживания нажатия на кнопки мыши.
            actHook.OnMouseActivity += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (fP)
                    {
                        secondCoords = Cursor.Position;
                        fP = false;
                        actHook.Stop();
                        if (PrintScreen(firstCoords, secondCoords) != null)
                            if (Operations.ImageDivision(PrintScreen(firstCoords, secondCoords)).Count > 0)
                            new Form2(PrintScreen(firstCoords, secondCoords), string.Empty).Show();
                            else
                            {
                                MessageBox.Show("Текст на данной области не найден. Выберите область заново.");
                                MessageBox.Show("Выделите область экрана.");
                                actHook.Start();
                            }
                    }
                    else
                    {
                        firstCoords = e.Location;
                        fP = true;
                    }
                }
            };
            actHook.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
