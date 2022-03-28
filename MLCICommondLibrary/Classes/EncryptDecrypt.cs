using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Classes
{
    public interface IEncryptDecrypt
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
