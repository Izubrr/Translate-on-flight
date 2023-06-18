using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using GTranslatorAPI;
using System.Net.Http;
using System.Text.Json;


namespace Translate_on_fly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : UiWindow
    {
        public ObservableCollection<string> ItemsSource { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            HotKey _hotKey = new HotKey(Key.Q, KeyModifier.Shift | KeyModifier.Win, OnHotKeyHandler);
            ItemsSource = LanguagesFull.GetLanguageNames();
            // Set the data context to the window itself (or your view model)
            DataContext = this;
            GTranslatorAPIClient client = new GTranslatorAPIClient();
            //client.TranslateFromNames
            
        }

        static async Task<string> TranslateText(string text, string sourceLanguage, string targetLanguage)
        {
            if (targetLanguage == null) targetLanguage = "English";
            Languages? targetcode = (Languages?)LanguagesUtil.GetCode(targetLanguage);

            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLanguage}&tl={targetcode.Value}&dt=t&q={Uri.EscapeDataString(text)}";
            
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Извлечь переведенный текст из ответа
                JsonDocument jsonDocument = JsonDocument.Parse(responseBody);
                JsonElement jsonArray = jsonDocument.RootElement[0];

                string translatedText = "";
                foreach (JsonElement jsonElement in jsonArray.EnumerateArray())
                {
                    translatedText += jsonElement[0].GetString();
                }

                return translatedText;
            }
        }

        private async void TranslateButton_Click(object sender, RoutedEventArgs e) 
        {
            string text = textbox1.Text; // Ваш текст для перевода
            string translatedText = await TranslateText(text, "auto", languagesuggestbox.Text);
            textbox2.Text = translatedText;
        }

        private void OnHotKeyHandler(HotKey hotKey) => CSInputs.SendInput.Keyboard.SendString(Clipboard.GetText());

        
        private void ThemeChange(object sender, RoutedEventArgs e)
        {
            if(Theme.GetAppTheme() == ThemeType.Light) Theme.Apply(ThemeType.Dark);
            else Theme.Apply(ThemeType.Light);

        }

        private void SettingsHide(object sender, RoutedEventArgs e) => SettingsDialog.Hide();
        private void SettingsShow(object sender, RoutedEventArgs e) => SettingsDialog.Show();

        private void HideSuggests(object sender, MouseButtonEventArgs e) => languagesuggestbox.IsSuggestionListOpen = false;

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Wpf.Ui.Controls.TextBox textBox = (Wpf.Ui.Controls.TextBox)sender;
            if (textBox.Text == "")
            {
                textbox1.Text = "";
                textbox2.Text = "";
            }
        }
    }
}
