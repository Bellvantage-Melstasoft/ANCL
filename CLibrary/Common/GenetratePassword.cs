using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Common
{
   public class GenetratePassword
    {

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(1, false));
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(1000, 9999));
           

            return builder.ToString();
        }

        public string GetUserName()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Sup_");
            builder.Append(RandomString(1, false));
            builder.Append(RandomNumber(10000, 99999));
            return builder.ToString();
        }

        public string GetUserPIN()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomNumber(1000, 9999));
            return builder.ToString();
        }

        public double GetRanDomNumber()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomNumber(1, 5));
            builder.Append(".");
            builder.Append(RandomNumber(10, 99));

            return double.Parse(builder.ToString());
        }

        public string GetActivationCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, false));
            builder.Append(RandomNumber(100000, 999999));
            return builder.ToString();
        }

    }
}
