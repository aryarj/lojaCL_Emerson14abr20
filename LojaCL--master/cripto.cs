using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCL
{
    public class cripto
    {
        //método para codificação pode ser internal, public or private . . .
        public string Base64Encode(string textoencode) 
        {
            var textoencodebytes = System.Text.Encoding.UTF8.GetBytes(textoencode);
            return System.Convert.ToBase64String(textoencodebytes);
        }
        //método para decodificação pode ser internal, public or private . . .
        public string Base64Decode(string textodecode) 
        {
            var textodecodebytes = System.Convert.FromBase64String(textodecode);
            return System.Text.Encoding.UTF8.GetString(textodecodebytes);
        }
    }
}
