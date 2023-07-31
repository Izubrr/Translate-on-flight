using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Translate_on_fly
{
    public static class LanguagesFull
    {
        private static Dictionary<string, Languages> NameToCode = null;

        private static Dictionary<Languages, string> CodeToName = new Dictionary<Languages, string>
        {
            {
                Languages.af,
                "Afrikaans"
            },
            {
                Languages.sq,
                "Albanian"
            },
            {
                Languages.ar,
                "Arabic"
            },
            {
                Languages.az,
                "Azerbaijani"
            },
            {
                Languages.eu,
                "Basque"
            },
            {
                Languages.be,
                "Belarusian"
            },
            {
                Languages.bn,
                "Bengali"
            },
            {
                Languages.bg,
                "Bulgarian"
            },
            {
                Languages.ca,
                "Catalan"
            },
            {
                Languages.zh_CN,
                "Chinese Simplified"
            },
            {
                Languages.zh_TW,
                "Chinese Traditional"
            },
            {
                Languages.hr,
                "Croatian"
            },
            {
                Languages.cs,
                "Czech"
            },
            {
                Languages.da,
                "Danish"
            },
            {
                Languages.nl,
                "Dutch"
            },
            {
                Languages.en,
                "English"
            },
            {
                Languages.eo,
                "Esperanto"
            },
            {
                Languages.et,
                "Estonian"
            },
            {
                Languages.tl,
                "Filipino"
            },
            {
                Languages.fi,
                "Finnish"
            },
            {
                Languages.fr,
                "French"
            },
            {
                Languages.gl,
                "Galician"
            },
            {
                Languages.ka,
                "Georgian"
            },
            {
                Languages.de,
                "German"
            },
            {
                Languages.el,
                "Greek"
            },
            {
                Languages.gu,
                "Gujarati"
            },
            {
                Languages.ht,
                "Haitian Creole"
            },
            {
                Languages.iw,
                "Hebrew"
            },
            {
                Languages.hi,
                "Hindi"
            },
            {
                Languages.hu,
                "Hungarian"
            },
            {
                Languages.ice,
                "Icelandic"
            },
            {
                Languages.id,
                "Indonesian"
            },
            {
                Languages.ga,
                "Irish"
            },
            {
                Languages.it,
                "Italian"
            },
            {
                Languages.ja,
                "Japanese"
            },
            {
                Languages.kn,
                "Kannada"
            },
            {
                Languages.ko,
                "Korean"
            },
            {
                Languages.la,
                "Latin"
            },
            {
                Languages.lv,
                "Latvian"
            },
            {
                Languages.lt,
                "Lithuanian"
            },
            {
                Languages.mk,
                "Macedonian"
            },
            {
                Languages.ms,
                "Malay"
            },
            {
                Languages.mt,
                "Maltese"
            },
            {
                Languages.no,
                "Norwegian"
            },
            {
                Languages.fa,
                "Persian"
            },
            {
                Languages.pl,
                "Polish"
            },
            {
                Languages.pt,
                "Portuguese"
            },
            {
                Languages.ro,
                "Romanian"
            },
            {
                Languages.ru,
                "Russian"
            },
            {
                Languages.sr,
                "Serbian"
            },
            {
                Languages.sk,
                "Slovak"
            },
            {
                Languages.sl,
                "Slovenian"
            },
            {
                Languages.es,
                "Spanish"
            },
            {
                Languages.sw,
                "Swahili"
            },
            {
                Languages.sv,
                "Swedish"
            },
            {
                Languages.ta,
                "Tamil"
            },
            {
                Languages.te,
                "Telugu"
            },
            {
                Languages.th,
                "Thai"
            },
            {
                Languages.tr,
                "Turkish"
            },
            {
                Languages.uk,
                "Ukrainian"
            },
            {
                Languages.ur,
                "Urdu"
            },
            {
                Languages.vi,
                "Vietnamese"
            },
            {
                Languages.cy,
                "Welsh"
            },
            {
                Languages.yi,
                "Yiddish"
            }
        };
        public static ObservableCollection<string> GetLanguageNames()
        {
            ObservableCollection<string> languages = new ObservableCollection<string>
            {
                "Afrikaans",
                "Albanian",
                "Arabic",
                "Azerbaijani",
                "Basque",
                "Belarusian",
                "Bengali",
                "Bulgarian",
                "Catalan",
                "Chinese Simplified",
                "Chinese Traditional",
                "Croatian",
                "Czech",
                "Danish",
                "Dutch",
                "English",
                "Esperanto",
                "Estonian",
                "Filipino",
                "Finnish",
                "French",
                "Galician",
                "Georgian",
                "German",
                "Greek",
                "Gujarati",
                "Haitian Creole",
                "Hebrew",
                "Hindi",
                "Hungarian",
                "Icelandic",
                "Indonesian",
                "Irish",
                "Italian",
                "Japanese",
                "Kannada",
                "Korean",
                "Latin",
                "Latvian",
                "Lithuanian",
                "Macedonian",
                "Malay",
                "Maltese",
                "Norwegian",
                "Persian",
                "Polish",
                "Portuguese",
                "Romanian",
                "Russian",
                "Serbian",
                "Slovak",
                "Slovenian",
                "Spanish",
                "Swahili",
                "Swedish",
                "Tamil",
                "Telugu",
                "Thai",
                "Turkish",
                "Ukrainian",
                "Urdu",
                "Vietnamese",
                "Welsh",
                "Yiddish"
            };

            return languages;
        }

        public static ObservableCollection<string> GetTransNames()
        {
            ObservableCollection<string> languages = new ObservableCollection<string>
            {
                "Georgian",
                "Arabic"
            };

            return languages;
        }
        public static Dictionary<string, string> GetGeorgianReverseTransliterationMap()
        {
            Dictionary<string, string> georgianReverseTransliterationMap = new Dictionary<string, string>
            {
                {"ა", "a"},
                {"ბ", "b"},
                {"გ", "g"},
                {"დ", "d"},
                {"ე", "e"},
                {"ვ", "v"},
                {"ზ", "z"},
                {"თ", "t"},
                {"ი", "i"},
                {"კ", "k"},
                {"ლ", "l"},
                {"მ", "m"},
                {"ნ", "n"},
                {"ო", "o"},
                {"პ", "p"},
                {"ჟ", "zh"},
                {"რ", "r"},
                {"ს", "s"},
                {"ტ", "t"},
                {"უ", "u"},
                {"ფ", "ph"},
                {"ქ", "q"},
                {"ღ", "gh"},
                {"ყ", "k"},
                {"შ", "sh"},
                {"ჩ", "ch"},
                {"ც", "ts"},
                {"ძ", "dz"},
                {"წ", "ts"},
                {"ჭ", "ch"},
                {"ხ", "kh"},
                {"ჯ", "j"},
                {"ჰ", "h"},
            };

            return georgianReverseTransliterationMap;
        }

        public static Dictionary<string, string> GetArabicReverseTransliterationMap()
        {
            Dictionary<string, string> arabicReverseTransliterationMap = new Dictionary<string, string>
            {
                {"ا", "a"},
                {"ب", "b"},
                {"ت", "t"},
                {"ث", "th"},
                {"ج", "j"},
                {"ح", "H"},
                {"خ", "kh"},
                {"د", "d"},
                {"ذ", "dh"},
                {"ر", "r"},
                {"ز", "z"},
                {"س", "s"},
                {"ش", "sh"},
                {"ص", "S"},
                {"ض", "D"},
                {"ط", "T"},
                {"ظ", "Z"},
                {"ع", "'"},
                {"غ", "gh"},
                {"ف", "f"},
                {"ق", "q"},
                {"ك", "k"},
                {"ل", "l"},
                {"م", "m"},
                {"ن", "n"},
                {"ه", "h"},
                {"و", "w"},
                {"ي", "y"},
            };

            return arabicReverseTransliterationMap;
        }

        public static string GetName(Languages languageCode)
        {
            string value = null;
            CodeToName.TryGetValue(languageCode, out value);
            return value;
        }

        public static string GetId(Languages languageCode)
        {
            string text = (languageCode.ToString() ?? "").Replace("_", "-");
            if (text == "ice")
            {
                text = "is";
            }

            return text;
        }

        public static string GetId(string languageName)
        {
            Languages? code = GetCode(languageName);
            if (!code.HasValue)
            {
                return null;
            }

            return GetId(code.Value);
        }

        public static Languages? GetCode(string languageName)
        {
            InitNameToCode();
            if (!NameToCode.TryGetValue(languageName, out var value))
            {
                return null;
            }

            return value;
        }

        public static Languages? GetCodeFromId(string languageId)
        {
            string text = languageId.Replace("-", "_");
            if (text == "is")
            {
                text = "ice";
            }

            if (!Enum.TryParse<Languages>(text, out var result))
            {
                return null;
            }

            return result;
        }

        private static void InitNameToCode()
        {
            if (NameToCode != null)
            {
                return;
            }

            NameToCode = new Dictionary<string, Languages>();
            foreach (KeyValuePair<Languages, string> item in CodeToName)
            {
                NameToCode[item.Value] = item.Key;
            }
        }

        public static Dictionary<Languages, string> GetLanguagesCodesToNames()
        {
            Dictionary<Languages, string> dictionary = new Dictionary<Languages, string>();
            foreach (KeyValuePair<Languages, string> item in CodeToName)
            {
                dictionary[item.Key] = item.Value;
            }

            return dictionary;
        }

        public static Dictionary<string, Languages> GetLanguagesNamesToCodes()
        {
            Dictionary<string, Languages> dictionary = new Dictionary<string, Languages>();
            InitNameToCode();
            foreach (KeyValuePair<string, Languages> item in NameToCode)
            {
                dictionary[item.Key] = item.Value;
            }

            return dictionary;
        }
    }
}