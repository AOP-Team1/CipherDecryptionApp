﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp.ViewModels
{
	public class DoubleTranspositionViewModel : ViewModelBase
	{
		private string _input = string.Empty;
		private string _key1 = string.Empty;
		private string _key2 = string.Empty;
		private string _output = string.Empty;

		public ReactiveCommand<Unit, Unit> EncodeCommand { get; }
		public ReactiveCommand<Unit, Unit> DecodeCommand { get; }

		public DoubleTranspositionViewModel()
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

		public string Key1
		{
			get => _key1;
			set => this.RaiseAndSetIfChanged(ref _key1, value);
		}

		public string Key2
		{
			get => _key2;
			set => this.RaiseAndSetIfChanged(ref _key2, value);
		}

		public string Output
		{
			get => _output;
			set => this.RaiseAndSetIfChanged(ref _output, value);
		}

		private void ExecuteEncodeCommand()
		{
			ICipher cipher = new DoubleTranspositionCipher(Key1, Key2);
			Output = cipher.Encode(Input);
		}

		private void ExecuteDecodeCommand()
		{
			ICipher cipher = new DoubleTranspositionCipher(Key1, Key2);
			Output = cipher.Decode(Input);
		}
	}
}
