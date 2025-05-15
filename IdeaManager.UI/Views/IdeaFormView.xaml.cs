using System.Windows;
using System.Windows.Controls;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaManager.UI.Views
{
    public partial class IdeaFormView : Page
    {
        public IdeaFormView()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var idea = new Idea
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text
            };
            var svc = App.ServiceProvider.GetRequiredService<IIdeaService>();

            try
            {
                await svc.SubmitIdeaAsync(idea);
                MessageBox.Show("Idée enregistrée !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Navigue vers la page de liste via la frame publique
                var main = (MainWindow)Application.Current.MainWindow;
                main.MainFrame2.Navigate(new IdeaListView());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
