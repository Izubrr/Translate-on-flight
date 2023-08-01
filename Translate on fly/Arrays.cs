using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Translate_on_fly
{
    internal class Arrays
    {
        public static Dictionary<string, Key> KeyDictionary = new Dictionary<string, Key>
        {
            { "A", Key.A },
            { "B", Key.B },
            { "C", Key.C },
            { "D", Key.D },
            { "E", Key.E },
            { "F", Key.F },
            { "G", Key.G },
            { "H", Key.H },
            { "I", Key.I },
            { "J", Key.J },
            { "K", Key.K },
            { "L", Key.L },
            { "M", Key.M },
            { "N", Key.N },
            { "O", Key.O },
            { "P", Key.P },
            { "Q", Key.Q },
            { "R", Key.R },
            { "S", Key.S },
            { "T", Key.T },
            { "U", Key.U },
            { "V", Key.V },
            { "W", Key.W },
            { "X", Key.X },
            { "Y", Key.Y },
            { "Z", Key.Z }
        };
        public static KeyModifier[] keyModifiers = new KeyModifier[]
        {
            KeyModifier.Alt,
            KeyModifier.Ctrl,
            KeyModifier.Shift,
            KeyModifier.Win
        };
    }
}
