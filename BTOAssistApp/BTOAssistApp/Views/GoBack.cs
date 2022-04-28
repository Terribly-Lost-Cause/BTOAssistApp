using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BTOAssistApp.Views
{
    public class GoBack : ContentPage
    { 
        
        /*
        public GoBack()
        {
           Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }*/

        public Action CustomBackButtonAction { get; set; }

        public static readonly BindableProperty EnableBackButtonOverrideProperty =
               BindableProperty.Create(
               nameof(EnableBackButtonOverride),
               typeof(bool),
               typeof(GoBack),
               false);

        /// <summary>
        /// Gets or Sets Custom Back button overriding state
        /// </summary>
        public bool EnableBackButtonOverride
        {
            get
            {
                return (bool)GetValue(EnableBackButtonOverrideProperty);
            }
            set
            {
                SetValue(EnableBackButtonOverrideProperty, value);
            }
        }

    }
}