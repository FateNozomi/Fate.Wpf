using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Wpf.MVVM;

namespace Fate.Wpf.Controls.Tester
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private string _labelText;
        private string _labelValueText;
        private string _labelUnitText;
        private string _labelUnitValueText;
        private string _labelUnitUnitText;

        public MainWindowViewModel()
        {
            WireCommands();
        }

        public string LabelText { get => _labelText; set => SetProperty(ref _labelText, value); }

        public string LabelValueText { get => _labelValueText; set => SetProperty(ref _labelValueText, value); }

        public string LabelUnitText { get => _labelUnitText; set => SetProperty(ref _labelUnitText, value); }

        public string LabelUnitValueText { get => _labelUnitValueText; set => SetProperty(ref _labelUnitValueText, value); }

        public string LabelUnitUnitText { get => _labelUnitUnitText; set => SetProperty(ref _labelUnitUnitText, value); }

        public AsyncCommand<object> LabelTextBoxCommand { get; private set; }

        public AsyncCommand<object> LabelUnitTextBoxCommand { get; private set; }

        private void WireCommands()
        {
            LabelTextBoxCommand = AsyncCommand.Create(
                async (token, param) =>
                {
                    LabelText = default(string);
                    LabelValueText = default(string);
                    await Task.Delay(1000, token);
                    LabelText = "Hello";
                    LabelValueText = "World!";
                });

            LabelUnitTextBoxCommand = AsyncCommand.Create(
                async (token, param) =>
                {
                    LabelUnitText = default(string);
                    LabelUnitValueText = default(string);
                    LabelUnitUnitText = default(string);
                    await Task.Delay(1000, token);
                    LabelUnitText = "Hello";
                    LabelUnitValueText = "World!";
                    LabelUnitUnitText = "Alpha";
                });
        }
    }
}
