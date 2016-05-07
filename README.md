# ExtendedEntry
Add super powers to your entry with this Custom Renderer for Xamarin Forms (iOS &amp; Android).
Do validations, customize keyboard type and more.

# How to install
First of all you have to install the JSON.Net component (https://components.xamarin.com/view/json.net) for each platform. Then, just copy the CustomEntry Class to your Xamarin Forms Portable Project, next you must copy the CustomEntryRenderer for the iOS and the Android project.

# How to use
Simply use it in your XAML like this:

    <whateveryourxmlnamespaceiscalled:CustomEntry  Placeholder="Default"  
    MaxLength="200"  
    KeyboardType="1"  
    Validations="[{Key: 'Required', Value: true}, {Key: 'MinLength', Value: 7}, {Key: 'Type', Value: 'email'}]" />

# Bindable Properties
>MaxLength (int)
Is used to set the limit of characters of your entry.

>KeyboardType (int)
Is used to set the type of keyboard you want to display when using your entry.
The possible values are:

**0** for numeric.
**1** for default text.
**2** for phone pad.
**3** for DatetimeVariationDate. **Only Android**
**4** for TextVariationPassword. **Only Android**
**5** for DatetimeVariationTime. **Only Android**

>Validations (string)
Is used to add validations to your entry.
The value is represented as a JSON Array of objects with Key and Value properties

**{Key: 'Required', Value: true}** to set as required entry, the Value is bool.
**{Key: 'MinLength', Value: 7}** to set a minimun length required by the entry, the Value is int, and it must be greater than the Bindable Property MaxLength.
**{Key: 'Type', Value: 'email'}** for validate a type for the entered text, for now we only have the Value "email" (which is string, btw), it just evaluate the text against a RegExp, so you can give me a hand to add more Types.

>IsValid (bool)
Is used to get and set whether the entry is valid or not.
