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
using CSInputs.Enums;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

namespace Translate_on_fly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : UiWindow
    {
        public ObservableCollection<string> LanguageItemsSource { get; set; }
        public ObservableCollection<string> KeysItemsSource { get; set; }
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
            if(text.Length > 1)
            {
                string translatedText = await TranslateText(text, "auto", languagesuggestbox.Text);
                textbox2.Text = translatedText;
            }  
        }

        private async void OnHotKeyHandler(HotKey hotKey)
        {
            //Thread.Sleep(1000);

            CSInputs.SendInput.Keyboard.Send(KeyboardKeys.Ctrl, KeyFlags.Down);
            CSInputs.SendInput.Keyboard.Send(KeyboardKeys.C);
            CSInputs.SendInput.Keyboard.Send(KeyboardKeys.Ctrl, KeyFlags.Up);
            string text = Clipboard.GetText(); // Ваш текст для перевода
            string translatedText = await TranslateText(text, "auto", languagesuggestbox.Text);
            Clipboard.SetText(translatedText);
            CSInputs.SendInput.Keyboard.SendString(Clipboard.GetText());
        }

        
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

        private void SettingsApply(object sender, RoutedEventArgs e)
        {
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
            if(ComboBoxKey.SelectedIndex != Settings.Default.HotkeyKey)
            {
                Settings.Default.HotkeyKey = ComboBoxKey.SelectedIndex;
                Settings.Default.HotkeyModifier = ComboBoxModifier.SelectedIndex;
                Settings.Default.Save();
                _hotKey = new HotKey(letterToKeyDictionary.Values.ElementAt(ComboBoxKey.SelectedIndex), keyModifiersArray[ComboBoxModifier.SelectedIndex], OnHotKeyHandler);
            }
            SettingsDialog.Hide();
        }
        private void HideFocus(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, null);
            Keyboard.ClearFocus();
        }
    }
}
