using System;
using System.Collections.Generic;
using System.Linq;
using WindowMover.Models;

namespace WindowMover.Services
{
    public interface ISettingsService
    {
        IEnumerable<KeyModifiers> GetSavedModifiers();
        IEnumerable<KeyModifiers> GetAllModifiers();
        void SaveModifiers(IEnumerable<KeyModifiers> modifiers);
        char GetDefaultKey();
        void SaveDefaultKey(char key);
    }

    public class SettingsService : ISettingsService
    {
        public IEnumerable<KeyModifiers> GetSavedModifiers()
        {
            var modifiers = Properties.Settings.Default.Modifiers;
            var saved = new List<KeyModifiers>();
            if (!string.IsNullOrWhiteSpace(modifiers))
            {
                foreach (var s in modifiers.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    KeyModifiers mod;
                    if (Enum.TryParse(s, out mod)) saved.Add(mod);
                }
            }
            return saved;
        }

        public IEnumerable<KeyModifiers> GetAllModifiers()
        {
            return Enum.GetNames(typeof (KeyModifiers))
                .Select(s => Enum.Parse(typeof (KeyModifiers), s, true))
                .Cast<KeyModifiers>()
                .ToList();
        }

        public void SaveModifiers(IEnumerable<KeyModifiers> modifiers)
        {
            Properties.Settings.Default.Modifiers = string.Join(",", modifiers);
            Properties.Settings.Default.Save();
        }

        public char GetDefaultKey()
        {
            return Properties.Settings.Default.DefaultKey;
        }

        public void SaveDefaultKey(char key)
        {
            Properties.Settings.Default.DefaultKey = key;
            Properties.Settings.Default.Save();
        }
    }
}
