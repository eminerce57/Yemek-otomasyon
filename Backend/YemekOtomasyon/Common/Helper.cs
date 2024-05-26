using System.Globalization;
using System.Text;
using System.Security.Cryptography;
using CrudProject.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace CrudProject.Common
{
    public class Helper
    {
        public struct apiReturn<T>
        {
            public bool status { get; set; }

            public string message { get; set; }

            public T data { get; set; }
        }

        public static string GenerateSlug(string name)
        {
            // Verilen metni küçük harfe dönüştürün
            string slug = name.ToLower();

            // Özel karakterleri, boşlukları ve alt satırları temizleyin
            slug = Regex.Replace(slug, @"[^\w]+", ""); // Harf ve rakamları tutar

            return slug;
        }


        // Türkçe karakterleri temizleme
        public static string CleanTurkishChars(string input)
        {
            string[] oldChars = { "ç", "ğ", "ı", "ö", "ş", "ü", "Ç", "Ğ", "İ", "Ö", "Ş", "Ü" };
            string[] newChars = { "c", "g", "i", "o", "s", "u", "C", "G", "I", "O", "S", "U" };
            for (int i = 0; i < oldChars.Length; i++)
            {
                input = input.Replace(oldChars[i], newChars[i]);
            }
            return input;
        }

        // SQL Injection kontrolü (bu, parametrik sorguları doğru kullanırsanız gereksiz olabilir)
        public static bool IsPossibleSqlInjection(string input)
        {
            string[] blackList = { "select", "insert", "update", "delete", "drop", "--", ";--", "/*", "*/", "@@", "char", "nchar", "varchar", "nvarchar", "alter", "create", "cursor", "declare", "execute", "exec", "kill", "fetch", "open", "close", "sys", "sysobjects", "syscolumns" };
            foreach (string item in blackList)
            {
                if (input.ToLower().Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        // Tarih formatını dönüştürme
        public static string ConvertDateToDbFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }


        public static string FormatTime(string timeString)
        {
            if (DateTime.TryParseExact(timeString, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
            {
                return parsedTime.ToString("HH:mm:ss");
            }
            else if (DateTime.TryParseExact(timeString, "dd-MMM-yy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                return parsedDateTime.ToString("HH:mm:ss");
            }
            else
            {
                return "Invalid time format";
            }
        }

        //Email Geçerli mi
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Rastgele Şifre Oluşturma
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new char[length];
            var rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                res[i] = validChars[rnd.Next(validChars.Length)];
            }
            return new string(res);
        }

        //MD5 Şifre Oluştur
        public static string ComputeMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        //Dosya Uzantısı Alma
        public static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        //Değer Sadece SAyı mı?
        public static bool IsNumeric(object expression)
        {
            double retNum;
            bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //Object to String
        public static string getString(object val)
        {
            string ret = "";

            ret = Convert.ToString(val);

            if (String.IsNullOrEmpty(ret))
                ret = "";
            else
                ret = ret.Trim();

            return ret;
        }

        //md5 tick hash
        public static string encode_MD5(string text)
        {
            if (text.Trim() == "")
            {
                return "";
            }

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(text));
            byte[] hash = mD5CryptoServiceProvider.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString().ToUpper().Trim();
        }
        public static string RemoveDiacritics(string text)
        {
            text = text.Replace("Ä\u009f", "ğ").Replace("Ä±", "ı");
            return text;
        }

    }
}
