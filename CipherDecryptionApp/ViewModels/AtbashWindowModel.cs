using System.Reactive;
using ReactiveUI;

namespace CipherDecryptionApp.ViewModels

{
    public class AtbashWindowModel : ViewModelBase
    {
        private string _input = string.Empty;
        private string _output = string.Empty;

        public ReactiveCommand<Unit, Unit> EncodeCommand { get; }
        public ReactiveCommand<Unit, Unit> DecodeCommand { get; }

        public AtbashWindowModel()
        {
            var isValidObservable = this.WhenAnyValue(
                x => x.Input,
                (input) => !string.IsNullOrWhiteSpace(input)
            );

            EncodeCommand = ReactiveCommand.Create(ExecuteEncodeCommand, isValidObservable);
            DecodeCommand = ReactiveCommand.Create(ExecuteDecodeCommand, isValidObservable);
        }

        public string Input
        {
            get => _input;
            set => this.RaiseAndSetIfChanged(ref _input, value);
        }


        public string Output
        {
            get => _output;
            set => this.RaiseAndSetIfChanged(ref _output, value);
        }

        private void ExecuteEncodeCommand()
        {
            ICipher cipher = new AtbashCipher();
            Output = cipher.Encode(Input);
        }

        private void ExecuteDecodeCommand()
        {
            ICipher cipher = new AtbashCipher();
            Output = cipher.Decode(Input);
        }
    }
}