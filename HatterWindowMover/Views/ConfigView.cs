using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowMover
{
    public partial class ConfigView : Form
    {
        private readonly ConfigViewModel _viewModel;

        public ConfigView(ConfigViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            //apply bindings
            txtKey.DataBindings.Add("Text", _viewModel, "DefaultKeyString");
            btnSave.DataBindings.Add("Enabled", _viewModel, "CanSave");
            lstAvailableKeyModifiers.DataSource = _viewModel.AvailableModifiers;
            lstCurrentKeyModifiers.DataSource = _viewModel.CurrentModifiers;

            btnSave.Tag = _viewModel.SaveCommand;
        }

        private void BtnSaveOnClick(object sender, EventArgs e)
        {
            var command = ((Control) sender).Tag as ICommand;
            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }

            this.Close();
        }

        private void BtnCancelOnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtKeyOnKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            _viewModel.DefaultKey = e.KeyChar;
        }

        private void LstAvailableKeyModifiersOnDoubleClick(object sender, EventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem;
            if (selected != null)
                _viewModel.AddModifier((KeyModifiers)selected);
        }
        
        private void LstCurrentKeyModifiersOnDoubleClick(object sender, EventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem;
            if (selected != null)
                _viewModel.RemoveModifier((KeyModifiers)selected);

        }
    }
}
