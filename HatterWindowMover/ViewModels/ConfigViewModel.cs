using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowMover.Annotations;
using WindowMover.Models;
using WindowMover.Services;

namespace WindowMover.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private readonly ISettingsService _settings;

        public ConfigViewModel(ISettingsService settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            _settings = settings;
            var allKeyModifiers = _settings.GetAllModifiers().Where(m => m != KeyModifiers.Nomod).ToList();
            CurrentModifiers = new BindingList<KeyModifiers>(_settings.GetSavedModifiers().ToList());
            AvailableModifiers = new BindingList<KeyModifiers>(allKeyModifiers.Except(CurrentModifiers).ToList());
            if (CurrentModifiers.Count == 0)
            {
                CurrentModifiers.Add(KeyModifiers.Nomod);
            }

            DefaultKey = _settings.GetDefaultKey();
            SaveCommand = new RelayCommand<Action>(Save, onComplete => CanSave);
        }

        private bool _isSaving;
        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                if (_isSaving != value)
                {
                    _isSaving = value;
                    OnPropertyChanged();
                    OnPropertyChanged("CanSave");
                    OnPropertyChanged("CanClose");
                }
            }
        }

        public ICommand SaveCommand { get; private set; }
        public BindingList<KeyModifiers> CurrentModifiers { get; private set; }
        public BindingList<KeyModifiers> AvailableModifiers { get; private set; }

        public void AddModifier(KeyModifiers modifier)
        {
            if (CurrentModifiers.Count == 1 && CurrentModifiers.Contains(KeyModifiers.Nomod))
            {
                CurrentModifiers.Remove(KeyModifiers.Nomod);
            }
            AvailableModifiers.Remove(modifier);
            CurrentModifiers.Add(modifier);
            OnPropertyChanged("CanSave");
        }

        public void RemoveModifier(KeyModifiers modifier)
        {
            AvailableModifiers.Add(modifier);
            CurrentModifiers.Remove(modifier);
            if (CurrentModifiers.Count == 0)
            {
                CurrentModifiers.Add(KeyModifiers.Nomod);
            }
            OnPropertyChanged("CanSave");
        }

        private char? _defaultKey;
        public char? DefaultKey
        {
            get { return _defaultKey; }
            set
            {
                if (_defaultKey != value)
                {
                    _defaultKey = value.HasValue ? Char.ToUpper(value.Value) : default(char?);
                    OnPropertyChanged();
                    OnPropertyChanged("DefaultKeyString");
                    OnPropertyChanged("CanSave");
                }
            }
        }

        public string DefaultKeyString { get { return DefaultKey.HasValue ? DefaultKey.ToString() : ""; } }
        
        public bool CanSave
        {
            get
            {
                return !IsSaving && (CurrentModifiers.Count > 1 ||
                        (CurrentModifiers.Count > 0 && !CurrentModifiers.Contains(KeyModifiers.Nomod)));
            }
        }

        public bool CanClose
        {
            get
            {
                return !IsSaving;
            }
        }

        public void Save(Action onComplete)
        {
            if (CanSave)
            {
                IsSaving = true;

                Task.Run(() =>
                    {
                        if (DefaultKey.HasValue)
                            _settings.SaveDefaultKey(DefaultKey.Value);

                        _settings.SaveModifiers(CurrentModifiers);
                    })
                    .ContinueWith(t =>
                    {
                        IsSaving = false;

                        onComplete();
                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
