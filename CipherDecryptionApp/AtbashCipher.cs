using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public class AtbashCipher : ICipher
    {
        public string Encode(string message)
        {
            return "Encoded";
        }

        public string Decode(string cipher)
        {
            return "decoded";
        }
    }
}
