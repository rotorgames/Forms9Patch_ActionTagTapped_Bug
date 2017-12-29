using System.Collections.Generic;
using System.Diagnostics;
using Forms9Patch;
using Xamarin.Forms;

namespace Forms9PatchHtmlTag
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var itemSource = new List<ExampleModel>();

            for (var i = 0; i < 20; i++)
                itemSource.Add(new ExampleModel
                {
                    Title = "Title " + i,
                    Description = "<font color=\"#FF4A4B\"><a href=\"http://google.com\">Click HERE</a></font>"
                });

            ThisListView.ItemsSource = itemSource;
        }

        private void Label_OnActionTagTapped(object sender, ActionTagEventArgs e)
        {
            Debug.WriteLine("ActionTagTapped works!!!");
        }
    }
}