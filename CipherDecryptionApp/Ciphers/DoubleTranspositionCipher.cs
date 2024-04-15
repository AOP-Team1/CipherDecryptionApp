using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public class DoubleTranspositionCipher : ICipher
    {
		private string key1;
		private string key2;

        public DoubleTranspositionCipher(string key1, string key2)
        {
            this.key1 = key1;
			this.key2 = key2;
        }

        public string Encode(string plaintext)
		{
			string firstPass = Transpose(plaintext, key1);
			return Transpose(firstPass, key2);
		}

		public string Decode(string ciphertext)
		{
			string firstPass = Untranspose(ciphertext, key2);
			return Untranspose(firstPass, key1);
		}

		private string Transpose(string text, string key)
		{
			int[] keySequence = GetKeySequence(key);
			int columns = keySequence.Length;
			int rows = (int)Math.Ceiling(text.Length / (double)columns);
			char[,] grid = new char[rows, columns];
			StringBuilder result = new StringBuilder();

			// Fill the grid
			int charPos = 0;
			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					if (charPos < text.Length)
					{
						grid[row, col] = text[charPos++];
					}
					else
					{
						grid[row, col] = 'X'; // Padding character
					}
				}
			}

			// Read columns by the order of the key
			for (int i = 0; i < columns; i++)
			{
				int col = keySequence[i];
				for (int row = 0; row < rows; row++)
				{
					result.Append(grid[row, col]);
				}
			}

			return result.ToString();
		}

		private string Untranspose(string text, string key)
		{
			int[] keySequence = GetKeySequence(key);
			int columns = keySequence.Length;
			int rows = (int)Math.Ceiling(text.Length / (double)columns);
			char[,] grid = new char[rows, columns];
			StringBuilder result = new StringBuilder();

			// Fill the grid by columns based on key sequence
			int charPos = 0;
			for (int i = 0; i < columns; i++)
			{
				int col = keySequence[i];
				for (int row = 0; row < rows; row++)
				{
					if (charPos < text.Length)
					{
						grid[row, col] = text[charPos++];
					}
				}
			}

			// Read rows
			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					result.Append(grid[row, col]);
				}
			}

			return result.ToString();
		}

		private int[] GetKeySequence(string key)
		{
			char[] keyChars = key.ToCharArray();
			Array.Sort(keyChars);
			int[] sequence = new int[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				sequence[i] = key.IndexOf(keyChars[i]);
				key = key.Remove(sequence[i], 1).Insert(sequence[i], "\0");
			}
			return sequence;
		}
	}
}
