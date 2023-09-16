using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography.Xml;

namespace Translate_on_fly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : UiWindow
    {
        public ObservableCollection<string> LanguageItemsSource { get; set; }
        public ObservableCollection<string> KeysItemsSource { get; set; }
        public ObservableCollection<string> TransItemsSource { get; set; }
        public HotKey _hotKey;

        public MainWindow()
        {
            InitializeComponent();

            ComboBoxModifier.SelectedIndex = Settings.Default.HotkeyModifier;
            ComboBoxKey.SelectedIndex = Settings.Default.HotkeyKey;

            ObservableCollection<string> keys = new ObservableCollection<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                keys.Add(c.ToString());
            }
            KeysItemsSource = keys;
            LanguageItemsSource = LanguagesFull.GetLanguageNames();
            TransItemsSource = LanguagesFull.GetTransNames();
            DataContext = this;

            Dictionary<string, Key> letterToKeyDictionary = new Dictionary<string, Key>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string letter = c.ToString();
                Key key = (Key)Enum.Parse(typeof(Key), letter);
                letterToKeyDictionary.Add(letter, key);
            }
            KeyModifier[] keyModifiersArray = new KeyModifier[]
            {
                KeyModifier.Alt,
                KeyModifier.Ctrl,
                KeyModifier.Shift,
                KeyModifier.Win
            };
            _hotKey = new HotKey(letterToKeyDictionary.Values.ElementAt(Settings.Default.HotkeyKey), keyModifiersArray[Settings.Default.HotkeyModifier], OnHotKeyHandler);
        }

        static async Task<string> TranslateText(string text, string sourceLanguage, string targetLanguage)
        {
            var languegesFull = LanguagesFull.GetLanguageNames();
            if (languegesFull.Contains(targetLanguage) == false) targetLanguage = "English";
            Languages? targetcode = (Languages?)LanguagesFull.GetCode(targetLanguage);

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
            if (text.Length > 1)
            {
                string translatedText = await TranslateText(text, "auto", languagesuggestbox.Text);
                textbox2.Text = translatedText;
            }
        }

        private async void OnHotKeyHandler(HotKey hotKey)
        {
            Clipboard.Clear();

            await Task.Run(KeySimulation.CtrlCAsync);

            string selectedText = Clipboard.GetText();

            if (selectedText.Length > 0)
            {
                string translatedText = await TranslateText(selectedText, "auto", languagesuggestbox.Text);
                Clipboard.SetText(translatedText);

                KeySimulation.CtrlV();
            }
        }

        private void ThemeChange(object sender, RoutedEventArgs e)
        {
            if (Theme.GetAppTheme() == ThemeType.Light) Theme.Apply(ThemeType.Dark);
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

        private void SettingsApply(object sender, RoutedEventArgs e)
        {
            if (ComboBoxKey.SelectedIndex != Settings.Default.HotkeyKey)
            {
                if (_hotKey != null)
                {
                    _hotKey.Unregister();
                    _hotKey = null;
                }
                Settings.Default.HotkeyKey = ComboBoxKey.SelectedIndex;
                Settings.Default.HotkeyModifier = ComboBoxModifier.SelectedIndex;
                Settings.Default.Save();
                _hotKey = new HotKey(Arrays.KeyDictionary.Values.ElementAt(ComboBoxKey.SelectedIndex), Arrays.keyModifiers[ComboBoxModifier.SelectedIndex], OnHotKeyHandler);
            }
            SettingsDialog.Hide();
        }
        private void HideFocus(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, null);
            Keyboard.ClearFocus();
        }
        static string ReverseTransliterate(string input, Dictionary<string, string> transliterationMap)
        {
            foreach (var kvp in transliterationMap)
            {
                input = input.Replace(kvp.Value, kvp.Key);
            }
            return input;
        }

        private void TranslationGridShow(object sender, RoutedEventArgs e)
        {
            TranslationGrid.Visibility = Visibility.Visible;
            TranscriptionGrid.Visibility = Visibility.Collapsed;
        }
        private void TranscriptionGridShow(object sender, RoutedEventArgs e)
        {
            TranslationGrid.Visibility = Visibility.Collapsed;
            TranscriptionGrid.Visibility = Visibility.Visible;
        }

        private async void TranscriptButton_Click(object sender, RoutedEventArgs e)
        {
            string word = ReverseTransliteration(languagesuggestbox_Trans.Text);
            string translatedText = await TranslateText(word, "auto", languagesuggestbox2_Trans.Text);
            textbox2_Trans.Text = translatedText;
        }
        private string ReverseTransliteration(string language)
        {
            string text = textbox1_Trans.Text.ToLower(); // Ваш текст для перевода
            if (text.Length > 1)
            {
                string latinWord = textbox1_Trans.Text;
                Dictionary<string, string> ReverseTransliterationMap;
                switch (language)
                {
                    case "Georgian":
                        ReverseTransliterationMap = LanguagesFull.GetGeorgianReverseTransliterationMap();
                        break;
                    case "Arabic":
                        ReverseTransliterationMap = LanguagesFull.GetArabicReverseTransliterationMap();
                        break;
                    default:
                        return "";
                }
                string Word = ReverseTransliterate(latinWord, ReverseTransliterationMap);
                return Word;
            }
            else return "";
        }
    }
}
