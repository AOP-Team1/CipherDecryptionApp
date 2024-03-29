using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherDecryptionApp
{
    public interface ICipher
    {
        public string Encode(string message);
        public string Decode(string cipher);
    }
}
