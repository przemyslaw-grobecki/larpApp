//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("RealmTry.Views.AddChatPopupPage.xaml", "Views/AddChatPopupPage.xaml", typeof(global::RealmTry.Views.AddChatPopupPage))]

namespace RealmTry.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\AddChatPopupPage.xaml")]
    public partial class AddChatPopupPage : global::Xamarin.CommunityToolkit.UI.Views.Popup {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.IndicatorView NewChatParticipantsIndicator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView Friends;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AddChatPopupPage));
            NewChatParticipantsIndicator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.IndicatorView>(this, "NewChatParticipantsIndicator");
            Friends = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "Friends");
        }
    }
}
