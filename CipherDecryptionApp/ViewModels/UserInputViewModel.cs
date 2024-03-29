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
        public ReactiveCommand<Unit, Unit> OkCommand { get; }

        public UserInputViewModel()
        {
            // This checks if the text input box has anything in it
            // I later use it to determine if the user can send the content to be en/decoded
            var isValidObservable = this.WhenAnyValue(
                x => x.Input,
                x => !string.IsNullOrWhiteSpace(x)
            );

            OkCommand = ReactiveCommand.Create(
                () => Console.WriteLine($"This is where you can get the user input: {Input}"), isValidObservable
            );
        }

        public string Input
        {
            get => _input;
            set => this.RaiseAndSetIfChanged(ref _input, value);
        }
    }
}
