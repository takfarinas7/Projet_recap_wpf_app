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
            // Récupère les valeurs
            var idea = new Idea
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text
            };

            // Résout le service
            var svc = App.ServiceProvider.GetRequiredService<IIdeaService>();

            try
            {
                // Sauvegarde
                await svc.SubmitIdeaAsync(idea);

                // Confirmation
                MessageBox.Show("Idée enregistrée !",
                                "Succès",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                // → Remise à zéro des champs
                TitleTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}",
                                "Erreur",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
