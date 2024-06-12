using System.Collections.Generic;
using System.Windows.Controls;
using Test1_Base;

namespace ExtensionMethods
{
    internal static class ControlValidationExtension
    {
        public static Validator Rules(this TextBox control) => new Validator(control, control.Text);
        public static Validator Rules(this PasswordBox control) => new Validator(control, control.Password);

    }

    internal static class ListContainsExtension
    {
        public static bool Contains(this List<Playlist> lists, MyPlaylist value)
        {
            foreach(var list in lists)
            {
                if (list.Name == value.Name && list.UserId == value.UserId)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Validator
    { 
        private readonly Control control;
        private readonly string content;

        private bool success = true;

        public Validator(Control control, string content)
        {
            this.control = control;
            this.content = content;
        }

        public Validator MinCharacters(int count)
        {
            if(content.Length < count) 
            {
                success = false;
            }

            return this;
        }

        public bool Validate()
        {
            if (!success)
            {
                control.ToolTip = "Это поле введено некорректно";
                control.Background = System.Windows.Media.Brushes.Red;
                return success;
            }
            else
            {
                control.Background = System.Windows.Media.Brushes.Transparent;
                return success;
            }
        }

        public Validator EqualTo(TextBox textBox)
        {
            if (textBox.Text != content)
            {
                success = false;
            }

            return this;
        }

        public Validator EqualTo(PasswordBox passwordBox)
        {
            if(passwordBox.Password != content)
            {
                success = false;
            }

            return this;
        }
    }

}
