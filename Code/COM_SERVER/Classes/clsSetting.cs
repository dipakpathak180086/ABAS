using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Classes
{
    public class clsSetting
    {
        public static string sCon = string.Empty;

        public static string sCon1 = string.Empty;

        public static string sPlant = string.Empty;

        public static string sServerVersion = string.Empty;

        public static string sDeviceVersion = string.Empty;

        public static int sPort;
        public static string gABColorPlant = string.Empty;
        public static string gEmultionFile = string.Empty;
        

        /*
        * Encode the passed string by adding '5' in the ascii value of each character
        */
        public string EncodeString(string strBaseText)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(strBaseText);
            StringBuilder result = new StringBuilder();
            foreach (byte b in asciiBytes)
            {
                result.Append(Convert.ToChar(b + 5));
            }
            return result.ToString();
        }

        /*
        * Decode the passed string by subtracting '5' in the ascii value of each character
        */
        public string DecodeString(string strBaseText)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(strBaseText);
            StringBuilder result = new StringBuilder();
            foreach (byte b in asciiBytes)
            {
                result.Append(Convert.ToChar(b - 5));
            }
            return result.ToString();
        }

        /*
         * Only allow to key alphabet on selected control
         * c - key character passed value from control
         * space - to allow space or not with alpha
         */
        public bool Alpha(char c, bool space)
        {
            if (space == true && c.ToString() == " ")
            {
                return true;
            }
            else if (char.IsLetter(c) == true)
            {
                return true;
            }
            return false;
        }

        /*
         * Only allow to key Symbol on selected control
         * c - key character passed value from control
         */
        public bool Symbol(char c)
        {
            if (char.IsDigit(c) == false && char.IsLetter(c) == false)
            {
                return true;
            }
            return false;
        }

        /*
         * Only allow to key numbers on selected control
         * c - key character passed value from control
         * decimalPoint - to allow decimal or not with number
         */
        public bool Num(char c, bool decimalPoint)
        {
            if (decimalPoint == true && c.ToString() == ".")
            {
                return true;
            }
            else if (char.IsDigit(c) == true)
            {
                return true;
            }
            return false;
        }

        public void ProperAlpha(TextBox txt)
        {
            String[] splWords = txt.Text.Split(' ');
            String finalWord = null;
            for (int i = 0; i < splWords.Length; i++)
            {
                string strWord = splWords[i].ToString();
                if (i > 0) finalWord = finalWord + " ";
                for (int j = 0; j < strWord.Length; j++)
                {
                    if (j == 0)
                    {
                        finalWord = finalWord + strWord[j].ToString().ToUpper();
                    }
                    else
                    { finalWord = finalWord + strWord[j].ToString(); }
                }
            }

            txt.Text = finalWord;
        }

        /*
         * Only allow to key alphabet on selected control
         * c - key character passed value from control
         * space - to allow space or not with alpha
         */
        public bool AlphaNumeric(char c, bool space)
        {
            if (space == true && c.ToString() == " ")
            {
                return true;
            }
            else if (char.IsLetterOrDigit(c) == true)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidEmail(string strIn)
        {
            // code source link with pattern description - http://msdn.microsoft.com/en-us/library/01escwtf.aspx
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public static string EncryptPassword(string lPass, string Ltype)
        {
            string encName = string.Empty;
            string LCompname = string.Empty;
            int i;
            int j;
            int LLen;
            char c1;
            char c2;

            LLen = lPass.Length;
            LCompname = "BCILBCILBCILBCILBCILBCIL";
            for (i = 0; i < LLen; i++)
            {
                c1 = Convert.ToChar(lPass.Substring(i, 1));
                c2 = Convert.ToChar(LCompname.Substring(i, 1));

                if (Ltype == "E")
                {
                    var e1 = Encoding.GetEncoding(1252);
                    j = (int)c1 + (int)c2 + i;
                    var s = e1.GetString(new byte[] { Convert.ToByte(j) });

                    encName = encName + s;
                }
                else
                {
                    var e1 = Encoding.GetEncoding(1252);
                    var r = e1.GetBytes(new char[] { Convert.ToChar(c1) });
                    j = (int)r[0] - (int)c2 - i;

                    encName = encName + (char)j;
                }
            }
            return encName;
        }

    }
}
