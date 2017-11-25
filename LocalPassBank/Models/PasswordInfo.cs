using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPassBank.Models
{
    public class PasswordInfo
    {
        private String keyWord;
        public String KeyWord
        {
            get => (keyWord);
            set => keyWord = value;
        }

        private String description;
        public String Description
        {
            get => (description);
            set => description = value;
        }

        public PasswordInfo()
        {

        }

        public PasswordInfo(Byte[] keyWord, Byte[] description, Byte[] key)
        {
            KeyWord = Security.Decryption(keyWord, key);
            Description = Security.Decryption(description, key);
        }
    }
}
