using System.Windows;
using System.Windows.Controls;

namespace Game.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Загрузка начальных настроек
            LoadSettings();
        }

        // Метод для загрузки начальных настроек
        private void LoadSettings()
        {
            // Пример загрузки настроек из файла конфигурации
            Width = 800;
            Height = 600;
            Title = "Игра";
        }

        // Обработчики событий кнопок
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Игра началась!");
        }

        private void Button_Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Owner = this;
            optionsWindow.ShowDialog();
        }

        private void Button_Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    // Окно опций
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            // Загрузка текущих настроек
            LoadCurrentOptions();
        }

        // Метод для загрузки текущих настроек
        private void LoadCurrentOptions()
        {
            // Пример загрузки настроек из файла конфигурации
            CheckBox_Fullscreen.IsChecked = Properties.Settings.Default.Fullscreen;
            Slider_Volume.Value = Properties.Settings.Default.Volume;
        }

        // Сохранение настроек
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение настроек в файл конфигурации
            Properties.Settings.Default.Fullscreen = (bool)CheckBox_Fullscreen.IsChecked;
            Properties.Settings.Default.Volume = (double)Slider_Volume.Value;
            Properties.Settings.Default.Save();

            MessageBox.Show("Настройки сохранены!");
            DialogResult = true;
        }

        // Отмена изменений
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}