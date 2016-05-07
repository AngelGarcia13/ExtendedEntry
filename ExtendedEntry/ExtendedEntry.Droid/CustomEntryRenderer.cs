using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExtendedEntry;
using Xamarin.Forms;
using ExtendedEntry.Droid;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Text;
using ExtendedEntry.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ExtendedEntry.Droid
{
    class CustomEntryRenderer : EntryRenderer
    {
        bool required = false;
        int minLength = 0;
        string type = "";
        string regExp = "";
        bool isValid = true;
        CustomEntry currentElement;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                currentElement = (CustomEntry)Element;

                setKeyboadType();
                setMaxLength();
                setValidations();
                //((CustomEntry)e.NewElement).OnValidate += (sender, ev) => Validate();
            }
        }

        //private void DoStuff()
        //{
        //    Toast.MakeText(this.Context, "click", ToastLength.Long).Show();
        //    //throw new NotImplementedException();
        //}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomEntry.KeyboardTypeProperty.PropertyName)
            {
                setKeyboadType();
            }
            else if (e.PropertyName == CustomEntry.MaxLengthProperty.PropertyName)
            {
                setMaxLength();
            }
        }

        private void setKeyboadType()
        {
            switch (currentElement.KeyboardType)
            {
                case 0:
                    Control.SetRawInputType(Android.Text.InputTypes.ClassNumber);
                    break;
                case 1:
                    Control.SetRawInputType(Android.Text.InputTypes.ClassText);
                    break;
                case 2:
                    Control.SetRawInputType(Android.Text.InputTypes.ClassPhone);
                    break;
                case 3:
                    Control.SetRawInputType(Android.Text.InputTypes.DatetimeVariationDate);
                    break;

                case 4:
                    Control.SetRawInputType(Android.Text.InputTypes.TextVariationPassword);
                    break;

                case 5:
                    Control.SetRawInputType(Android.Text.InputTypes.DatetimeVariationTime);
                    break;
                default:
                    Control.SetRawInputType(Android.Text.InputTypes.ClassText);
                    break;
            }
        }

        private void setMaxLength()
        {
            if (currentElement.MaxLength > 0)
            {
                Control.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(currentElement.MaxLength) });
            }

        }

        private void setValidations()
        {
            var validationString = currentElement.Validations;
            var validationRules = JsonConvert.DeserializeObject<List<ValidationModel>>(validationString);
            if (validationRules != null)
            {
                foreach (var item in validationRules)
                {
                    switch (item.Key)
                    {
                        case "Required":
                            required = (bool)item.Value;
                            break;
                        case "MinLength":
                            minLength = int.Parse(item.Value.ToString());
                            break;
                        case "Type":
                            type = (string)item.Value;
                            if (type.Equals("Email", StringComparison.InvariantCultureIgnoreCase))
                            {
                                regExp = @"^([A-Z|a-z|0-9](\.|_){0,1})+[A-Z|a-z|0-9]\@([A-Z|a-z|0-9])+((\.){0,1}[A-Z|a-z|0-9]){2}\.[a-z]{2,3}$";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Control.TextChanged += Control_ValidateTextChanged;
            //Console.WriteLine(validationRules);
        }

        private void Control_ValidateTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            isValid = true;
            //errorIcon.SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.Multiply);
            if (required && currentElement.Text.Trim() == string.Empty)
            {
                isValid = false;
                Control.Error = "Campo requerido";
            }
            else if (currentElement.Text.Trim().Length < minLength)
            {
                isValid = false;
                Control.Error = minLength + " carácteres requeridos";
            }
            else if (!regExp.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
            {
                isValid = Regex.IsMatch(currentElement.Text, regExp);
                if (!isValid)
                {
                    Control.Error = "Campo inválido";
                }

            }
            currentElement.IsValid = isValid;
            
        }
    }
}