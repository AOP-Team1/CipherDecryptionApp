using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public class AtbashCipher : ICipher
    {
        private Dictionary<char, char> _keys = new Dictionary<char, char>
        {
            { ' ', ' ' },
            { 'a', 'z' },
            { 'b', 'y' },
            { 'c', 'x' },
            { 'd', 'w' },
            { 'e', 'v' },
            { 'f', 'u' },
            { 'g', 't' },
            { 'h', 's' },
            { 'i', 'r' },
            { 'j', 'q' },
            { 'k', 'p' },
            { 'l', 'o' },
            { 'm', 'n' },
            { 'n', 'm' },
            { 'o', 'l' },
            { 'p', 'k' },
            { 'q', 'j' },
            { 'r', 'i' },
            { 's', 'h' },
            { 't', 'g' },
            { 'u', 'f' },
            { 'v', 'e' },
            { 'w', 'd' },
            { 'x', 'c' },
            { 'y', 'b' },
            { 'z', 'a' },
        };
        public string Encode(string message)
        {
            // break message into component characters  
            string cipher = "";

            foreach (char item in message)
            {
                char value;
                if (!_keys.ContainsKey(item))
                {
                    value = item;
                }
                else
                {
                    value = _keys[item];
                }
                
                cipher = string.Concat(cipher, value);
            }

            Console.WriteLine(cipher);
                        
            return cipher; 
        }

        public string Decode(string cipher)
        {
            string message = "";

            foreach (char item in cipher)
            {
                char value;
                if (!_keys.ContainsValue(item))
                {
                    value = item;
                }
                else
                {
                    value = _keys[item];
                }
                
                message = string.Concat(message, value);
            }

            Console.WriteLine(message);
                        
            return message; 
        }
    }
}
