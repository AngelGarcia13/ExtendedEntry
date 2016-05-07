using CoreGraphics;
using ExtendedEntry;
using ExtendedEntry.iOS;
using ExtendedEntry.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ExtendedEntry.iOS
{
    class CustomEntryRenderer : EntryRenderer
    {
        bool required = false;
        int minLength = 0;
        string type = "";
        string regExp = "";
        bool isValid = true;
        int length = 0;
        CustomEntry currentElement;
        CGColor initialBorderColor;
        UITextField textField;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                currentElement = (CustomEntry)Element;
                textField = (UITextField)Control;
                initialBorderColor = Control.Layer.BorderColor;
                setKeyboadType();
                setMaxLength();
                setValidations();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomEntry.KeyboardTypeProperty.PropertyName)
            {
                setKeyboadType();
            }
            //else if(e.PropertyName == CustomEntry.MaxLengthProperty.PropertyName)
            //{
            //    setMaxLength();
            //}
        }

        private void setKeyboadType()
        {
            Control.SecureTextEntry = false;
            switch (((CustomEntry)Element).KeyboardType)
            {
                case 0:
                    Control.KeyboardType = UIKit.UIKeyboardType.NumberPad;
                    break;
                case 1:
                    Control.KeyboardType = UIKit.UIKeyboardType.Default;
                    break;
                case 2:
                    Control.KeyboardType = UIKit.UIKeyboardType.PhonePad;
                    break;
                default:
                    Control.KeyboardType = UIKit.UIKeyboardType.Default;
                    break;
            }
        }

        private void setMaxLength()
        {
            if (currentElement.Text == null)
            {
                length = 0;
            }
            else
            {
                length = currentElement.Text.Length;
            }

            if (currentElement.MaxLength > 0 && length > currentElement.MaxLength)
            {
                currentElement.Text = currentElement.Text.Remove(currentElement.Text.Length - 1, 1);
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
            Control.EditingChanged += Control_ValidateTextChanged;

        }

        private void Control_ValidateTextChanged(object sender, EventArgs e)
        {
            isValid = true;

            if (required && currentElement.Text.Trim() == string.Empty)
            {
                isValid = false;
                textField.TextColor = new UIColor(UIColor.Red.CGColor);
                //Control.Error = "Campo requerido";
            }
            else if (currentElement.Text.Trim().Length < minLength)
            {
                isValid = false;
                textField.TextColor = new UIColor(UIColor.Red.CGColor);
                //Control.Error = minLength + " carácteres requeridos";
            }
            else if (!regExp.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
            {
                isValid = Regex.IsMatch(currentElement.Text, regExp);
                if (!isValid)
                {
                    textField.TextColor = new UIColor(UIColor.Red.CGColor);
                    //Control.Error = "Campo inválido";
                }
                else
                {
                    textField.TextColor = new UIColor(UIColor.Black.CGColor);
                }

            }
            else
            {
                textField.TextColor = new UIColor(UIColor.Black.CGColor);
            }
            currentElement.IsValid = isValid;
        }
    }
}
