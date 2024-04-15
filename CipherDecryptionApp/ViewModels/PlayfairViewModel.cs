﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp.ViewModels
{
	public class PlayfairViewModel : ViewModelBase
	{
		private string _input = string.Empty;
		private string _key = string.Empty;
		private string _output = string.Empty;

		public ReactiveCommand<Unit, Unit> EncodeCommand { get; }
		public ReactiveCommand<Unit, Unit> DecodeCommand { get; }

		public PlayfairViewModel()
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

		private void ExecuteEncodeCommand()
		{
			ICipher cipher = new PlayfairCipher(Key);
			Output = cipher.Encode(Input);
		}

		private void ExecuteDecodeCommand()
		{
			ICipher cipher = new PlayfairCipher(Key);
			Output = cipher.Decode(Input);
		}
	}
}
