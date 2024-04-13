
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public class CaesarCipher : ICipher
    {
        private readonly int key;

        public CaesarCipher(int key)
        {
            this.key = key;
        }

        public string Encode(string message)
        {
            StringBuilder encodedMessage = new();

            foreach (var c in message)
            {
                if (char.IsAsciiLetter(c))
                {
                    char initialChar = char.IsUpper(c) ? 'A' : 'a';
                    char encodedChar = (char)(((c - initialChar + key) % 26) + initialChar);
                    encodedMessage.Append(encodedChar);
                }
                else
                    encodedMessage.Append(c);
            }

            return encodedMessage.ToString();
        }

        public string Decode(string message)
        {
            StringBuilder decodedMessage = new();

            foreach (var character in message)
            {
                if (char.IsAsciiLetter(character))
                {
                    char initialChar = char.IsUpper(character) ? 'A' : 'a';
                    char decodedChar = (char)(((character - initialChar + key + 26) % 26) + initialChar);
                    decodedMessage.Append(decodedChar);
                }
                else
                    decodedMessage.Append(character);
            }

            return decodedMessage.ToString();

        }
    }
}

