using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    static class PasswordGenerator
    {
        public static string Generate()
        {
            string generatedPass = string.Empty;
            Random rand = new Random();
            int symbType = 0;
            for (int i = 0; i != 8; i++)
            {
                symbType = rand.Next(0, 3);
                if (symbType == 0)
                    generatedPass += Convert.ToChar(rand.Next(65, 90));
                else
                    if (symbType == 1)
                        generatedPass += Convert.ToChar(rand.Next(97, 122));
                    else
                        if (symbType == 2)
                            generatedPass += Convert.ToChar(rand.Next(48, 57));
            }
            return generatedPass;
        }
    }
}
