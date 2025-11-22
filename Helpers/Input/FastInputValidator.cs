using System;
using System.Net.Mail;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FastUI.Helpers.Input
{
    public static class FastInputValidator
    {
        // --------------------------------------------------------------
        // GET PLACEHOLDER BASED ON INPUT TYPE
        // --------------------------------------------------------------
        public static string GetPlaceholder(FastInputType type)
        {
            return type switch
            {
                FastInputType.Email => "example@mail.com",
                FastInputType.PhoneDZ => "0XXXXXXXXX",
                FastInputType.Integer => "0",
                FastInputType.Decimal => "0,00",
                _ => "Text"
            };
        }


        // --------------------------------------------------------------
        // VALIDATE KEYPRESS (RESTRICTIONS)
        // --------------------------------------------------------------
        public static bool IsKeyAllowed(FastInputType type, KeyPressEventArgs e, string currentText)
        {
            switch (type)
            {
                case FastInputType.Integer:
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                        return false;
                    break;


                case FastInputType.Decimal:
                    if (!char.IsDigit(e.KeyChar) &&
                        e.KeyChar != ',' &&
                        e.KeyChar != (char)Keys.Back)
                        return false;

                    if (e.KeyChar == ',' && currentText.Contains(","))
                        return false;
                    break;


                case FastInputType.PhoneDZ:
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
        public static bool IsValid(FastInputType type, string text)
        {

            return type switch
            {
                FastInputType.Email => IsValidEmail(text),
                FastInputType.PhoneDZ => IsValidDZPhone(text),
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
