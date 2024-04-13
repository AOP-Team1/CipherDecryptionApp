using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp.ViewModels
{
    public class UserInputViewModel : ViewModelBase
    {
        private string _input = string.Empty;
        private string _key = string.Empty;
        private string _output = string.Empty;
        private string[] _modes = new[] { "Encode", "Decode" };
        private string _selectedMode;

        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public string[] Modes => _modes;

        public UserInputViewModel()
        {
            var isValidObservable = this.WhenAnyValue(
                x => x.Input,
                x => x.Key,
                (input, key) => !string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(key)
            );

            OkCommand = ReactiveCommand.Create(ExecuteCommand, isValidObservable);
        }

        public string Input
        {
            get => _input;
            set => this.RaiseAndSetIfChanged(ref _input, value);
        }

        public string Key
        {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }

        public string Output
        {
            get => _output;
            set => this.RaiseAndSetIfChanged(ref _output, value);
        }

        public string SelectedMode
        {
            get => _selectedMode;
            set => this.RaiseAndSetIfChanged(ref _selectedMode, value);
        }

        private void ExecuteCommand()
        {
            ICipher cipher = new CaesarCipher(int.Parse(Key));
            if (SelectedMode == "Encode")
                Output = cipher.Encode(Input);
            else if (SelectedMode == "Decode")
                Output = cipher.Decode(Input);
        }
    }
}
