using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FastUI.FastUILibrary.Core
{
    public static class FastValidation
    {



        // --------------------------------------------------------------
        // VALIDATE KEYPRESS (RESTRICTIONS)
        // --------------------------------------------------------------
        public static bool IsKeyAllowed(FastEnumInputType type, KeyPressEventArgs e, string currentText)
        {
            switch (type)
            {
                case FastEnumInputType.Integer:
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                        return false;
                    break;


                case FastEnumInputType.Decimal:
                    if (!char.IsDigit(e.KeyChar) &&
                        e.KeyChar != ',' &&
                        e.KeyChar != (char)Keys.Back)
                        return false;

                    if (e.KeyChar == ',' && currentText.Contains(","))
                        return false;
                    break;


                case FastEnumInputType.PhoneDZ:
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                        return false;

                    if (char.IsDigit(e.KeyChar) && currentText.Length >= 10)
                        return false;
                    break;
            }

            return true;
        }


        // --------------------------------------------------------------
        // FINAL VALIDATION ON LEAVE
        // --------------------------------------------------------------
        public static bool IsValid(FastEnumInputType type, string text)
        {

            return type switch
            {
                FastEnumInputType.Email => IsValidEmail(text),
                FastEnumInputType.PhoneDZ => IsValidDZPhone(text),
                _ => true
            };
        }


        // --------------------------------------------------------------
        // EMAIL CHECK
        // --------------------------------------------------------------
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        // --------------------------------------------------------------
        // DZ PHONE CHECK
        // --------------------------------------------------------------
        private static bool IsValidDZPhone(string phone)
        {
            if (phone.Length != 10) return false;
            if (phone[0] != '0') return false;

            char second = phone[1];
            if (second != '5' && second != '6' && second != '7')
                return false;

            return true;
        }
    }
}
