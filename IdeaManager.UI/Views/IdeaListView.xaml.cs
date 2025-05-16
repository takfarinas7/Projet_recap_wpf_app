using System.Windows;
using System.Windows.Controls;
using IdeaManager.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaManager.UI.Views
{
    public partial class IdeaListView : Page
    {
        public IdeaListView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var svc = App.ServiceProvider.GetRequiredService<IIdeaService>();
            var all = await svc.GetAllAsync();
            IdeasItemsControl.ItemsSource = all;
        }
    }
}
