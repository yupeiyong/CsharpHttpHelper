using System.Text;


namespace UnitTestProject1.Test_Globalcompanions
{

    public class DataEncode
    {
        public static string EncodeData(string strData)
        {
            string str = "";
            for (int i = 0; i < strData.Length; i++)
            {
                char ch;
                if ((((strData[i] >= '0') && (strData[i] <= '9')) || ((strData[i] >= 'a') && (strData[i] <= 'z'))) || (((strData[i] >= 'A') && (strData[i] <= 'Z')) || ((((strData[i] == '=') || (strData[i] == '&')) || ((strData[i] == '_') || (strData[i] == '-'))) || (((strData[i] == '.') || (strData[i] == '*')) || (((strData[i] == ' ') || (strData[i] == '\r')) || (strData[i] == '\n'))))))
                {
                    ch = strData[i];
                    str = str + ch.ToString();
                }
                else
                {
                    ch = strData[i];
                    byte[] bytes = Encoding.Default.GetBytes(ch.ToString());
                    string str2 = string.Format("%{0:X00}", bytes[0]);
                    str = str + str2;
                }
            }
            return str;
        }
    }
}

