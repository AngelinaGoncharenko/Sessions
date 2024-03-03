using KeeperProCommonDivision.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Security.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Security.Views
{
    /// <summary>
    /// Логика взаимодействия для ApproveControlWindow.xaml
    /// </summary>
    public partial class ApproveControlWindow : Window
    {
        private readonly MyDbContext _myDbContext;
        private readonly User _user;
        private readonly Gender _emptyGender;

        private Uri _defaultUri;
        private Uri _currentUri;

        private int _countEmptyClick;

        public ApproveControlWindow(User user)
        {
            InitializeComponent();
            _myDbContext = MyDbContext.GetContext();
            _user = user;

            UserNameBlock.Text = user.FullName;

            var genders = _myDbContext
                .Genders
                .ToList();

            _emptyGender = new Gender { Name = "" };
            genders.Add(_emptyGender);

            GenderBox.ItemsSource = genders;

            _defaultUri = new Uri("/Resources/User.png", UriKind.Relative);
            _currentUri = _defaultUri;

            SetImage(_defaultUri);

            var currentTime = DateTime.Now;
            var bannedUser = _myDbContext
                .BannedUsers
                .Where(b => b.User == user)
                .ToList()
                .FirstOrDefault(b => (b.Time + TimeSpan.FromMinutes(1) - currentTime).Ticks > 0);

            if (bannedUser != null)
            {
                BlockWindow();

                new Thread(() =>
                {
                    var currentTime = DateTime.Now;
                    while (bannedUser.Time + TimeSpan.FromMinutes(1) >= currentTime)
                    {
                        currentTime = DateTime.Now;
                    }

                    UnlockWindow();
                }).Start();

                MessageBox.Show("Вы заблокированы, дождитесь разблокировки и сможете редактировать поля");
            }
        }

        /// <summary>
        /// Сохранение пользователя в базу данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            if (TryBan())
            {
                return;
            }

            var secondName = SecondNameBox.Text;
            var firstName = FirstNameBox.Text;
            var lastName = LastNameBox.Text;
            var gender = (Gender)GenderBox.SelectedItem;
            var job = JobBox.Text;

            if (secondName == null || firstName == null || gender == null || job == null)
            {
                MessageBox.Show("Все поля кроме отчества и фотографии обязательны для заполнения");
                return;
            }

            if (secondName == "" || firstName == "" || gender == _emptyGender || job == "")
            {
                MessageBox.Show("Все поля кроме отчества и фотографии обязательны для заполнения");
                return;
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Job = job,
                SecondName = secondName
            };

            if (_currentUri !=  _defaultUri)
            {
                try
                {
                    var bytesPhoto = File.ReadAllBytes(_currentUri.AbsolutePath);
                    var base64Photo = Convert.ToBase64String(bytesPhoto);
                    user.Photo = base64Photo;
                }
                catch
                {
                    MessageBox.Show("Путь до изображения нельзя выбрать, необходим доступ к абсолютному пути");
                }
            }

            _myDbContext.Users.Add(user);
            _myDbContext.SaveChanges();

            Clear();
            _countEmptyClick = 0;
            MessageBox.Show("Данные сохранены");
        }

        /// <summary>
        /// Очистить все данные и отменить сохранение в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            var startCount = _countEmptyClick;
            var tryBan = TryBan();
            if (!tryBan && startCount == _countEmptyClick)
            {
                _countEmptyClick = 0;
            }
            else
            {
                return;
            }

            Clear();
            MessageBox.Show("Данные очищены");
        }

        /// <summary>
        /// Очистка всех полей
        /// </summary>
        private void Clear()
        {
            SecondNameBox.Text = "";
            FirstNameBox.Text = "";
            LastNameBox.Text = "";
            GenderBox.SelectedItem = _emptyGender;
            JobBox.Text = "";
            SetImage(_defaultUri);
        }

        /// <summary>
        /// Загрузить картинку пользователю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadImage(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image | *.jpg;*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                var file = openFileDialog.FileName;
                SetImage(new Uri(file));
            }
        }

        private void SetImage(Uri uri)
        {
            _currentUri = uri;
            MyImage.Source = new BitmapImage(uri);
        }

        /// <summary>
        /// Метод попытки блокировки пользователя
        /// </summary>
        /// <returns></returns>
        private bool TryBan()
        {
            if (IsDataEmpty() == false)
                return false;

            if (_countEmptyClick > 0)
            {
                BlockWindow();

                var bannedUser = new BannedUser
                {
                    Time = DateTime.Now,
                    User = _user
                };

                _myDbContext.BannedUsers.Add(bannedUser);
                _myDbContext.SaveChanges();

                new Thread(() =>
                {
                    var currentTime = DateTime.Now;
                    while (bannedUser.Time + TimeSpan.FromMinutes(1) >= currentTime)
                    {
                        currentTime = DateTime.Now;
                    }

                    UnlockWindow();
                }).Start();

                MessageBox.Show("Вы заблокированы на минуту - запрещено чистить или сохранять пустые поля");

                _countEmptyClick = 0;
                return true;
            }
            else
            {
                _countEmptyClick += 1;
                MessageBox.Show("Запрещено чистить или сохранять пустые поля");
                return false;
            }
        }

        /// <summary>
        /// Проверка данных формы на пустые значения
        /// </summary>
        /// <returns></returns>
        public bool IsDataEmpty()
        {
            return SecondNameBox.Text == "" &&
            FirstNameBox.Text == "" &&
            LastNameBox.Text == "" &&
            GenderBox.SelectedItem == _emptyGender &&
            JobBox.Text == "" &&
            _currentUri == _defaultUri;
        }

        /// <summary>
        /// Разблокировка формы для ввода данных
        /// </summary>
        private void UnlockWindow()
        {
            Dispatcher.BeginInvoke(() =>
            {
                SecondNameBox.IsReadOnly = false;
                FirstNameBox.IsReadOnly = false;
                LastNameBox.IsReadOnly = false;
                GenderBox.IsReadOnly = false;
                GenderBox.IsEnabled = true;
                SaveButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
                LoadButton.IsEnabled = true;
            });
        }

        /// <summary>
        /// Блокировка формы для ввода данных
        /// </summary>
        private void BlockWindow()
        {
            Dispatcher.BeginInvoke(() =>
            {
                SecondNameBox.IsReadOnly = true;
                FirstNameBox.IsReadOnly = true;
                LastNameBox.IsReadOnly = true;
                GenderBox.IsEnabled = false;
                JobBox.IsReadOnly = true;
                SaveButton.IsEnabled = false;
                CancelButton.IsEnabled = false;
                LoadButton.IsEnabled = false;
            });
        }
    }
}
