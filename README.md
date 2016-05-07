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

To be continue...
