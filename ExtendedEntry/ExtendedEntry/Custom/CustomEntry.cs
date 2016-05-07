using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExtendedEntry
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty KeyboardTypeProperty =
             BindableProperty.Create(
             "KeyboardType",
             typeof(int),
             typeof(Entry),
             0);

        public int KeyboardType
        {
            set { SetValue(KeyboardTypeProperty, value); }
            get { return (int)GetValue(KeyboardTypeProperty); }
        }

        public static readonly BindableProperty MaxLengthProperty =
             BindableProperty.Create(
             "MaxLength",
             typeof(int),
             typeof(Entry),
             0);

        public int MaxLength
        {
            set { SetValue(MaxLengthProperty, value); }
            get { return (int)GetValue(MaxLengthProperty); }
        }

        public static readonly BindableProperty IsValidProperty =
             BindableProperty.Create(
             "IsValid",
             typeof(bool),
             typeof(Entry),
             true);

        public bool IsValid
        {
            set { SetValue(IsValidProperty, value); }
            get { return (bool)GetValue(IsValidProperty); }
        }

        public static readonly BindableProperty ValidationsProperty =
             BindableProperty.Create(
             "Validations",
             typeof(string),
             typeof(Entry),
             "");

        public string Validations
        {
            set { SetValue(ValidationsProperty, value); }
            get { return (string)GetValue(ValidationsProperty); }
        }

    }
}
