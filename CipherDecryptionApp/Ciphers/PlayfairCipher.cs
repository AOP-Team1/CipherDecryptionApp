using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public class PlayfairCipher : ICipher
    {
		private char[,] keySquare;
		private const int GridSize = 5;

		public PlayfairCipher(string key)
		{
			keySquare = GenerateKeySquare(key);
		}

		private char[,] GenerateKeySquare(string key)
		{
			var normalizedKey = string.Concat(key.ToUpper().Where(char.IsLetter));
			var distinctLetters = new HashSet<char>();
			var keySquare = new char[GridSize, GridSize];
			int x = 0, y = 0;

			foreach (var c in normalizedKey)
			{
				if (!distinctLetters.Contains(c) && c != 'J')
				{
					keySquare[x, y] = c;
					distinctLetters.Add(c);
					if (++y == GridSize)
					{
						y = 0;
						x++;
					}
				}
			}

			for (char ch = 'A'; ch <= 'Z'; ch++)
			{
				if (ch != 'J' && !distinctLetters.Contains(ch))
				{
					keySquare[x, y] = ch;
					if (++y == GridSize)
					{
						y = 0;
						x++;
					}
				}
			}

			return keySquare;
		}

		public string Encode(string plaintext)
		{
			return ProcessText(plaintext, true);
		}

		public string Decode(string ciphertext)
		{
			return ProcessText(ciphertext, false);
		}

		private string ProcessText(string input, bool encode)
		{
			input = input.ToUpper().Replace("J", "I");
			StringBuilder output = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsLetter(input[i]))
				{
					if (i == input.Length - 1 || !char.IsLetter(input[i + 1]))
					{
						output.Append(ProcessPair(input[i], 'X', encode));
					}
					else
					{
						output.Append(ProcessPair(input[i], input[++i], encode));
					}
				}
			}
			return output.ToString();
		}

		private string ProcessPair(char a, char b, bool encode)
		{
			int row1, col1, row2, col2;
			GetPosition(a, out row1, out col1);
			GetPosition(b, out row2, out col2);

			if (row1 == row2)
			{
				col1 = WrapIndex(col1 + (encode ? 1 : -1));
				col2 = WrapIndex(col2 + (encode ? 1 : -1));
			}
			else if (col1 == col2)
			{
				row1 = WrapIndex(row1 + (encode ? 1 : -1));
				row2 = WrapIndex(row2 + (encode ? 1 : -1));
			}
			else
			{
				int temp = col1;
				col1 = col2;
				col2 = temp;
			}

			return string.Concat(keySquare[row1, col1], keySquare[row2, col2]);
		}

		private void GetPosition(char ch, out int row, out int col)
		{
			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					if (keySquare[i, j] == ch)
					{
						row = i;
						col = j;
						return;
					}
				}
			}
			throw new Exception($"Character {ch} not found in key square.");
		}

		private int WrapIndex(int index)
		{
			return (index + GridSize) % GridSize;
		}
	}
}
