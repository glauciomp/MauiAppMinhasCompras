using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
            BindingContext = new Produto();
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            var produto = BindingContext as Produto;

            produto.ValidateAll();

            if (produto.HasErrors)
            {
                await DisplayAlert("Atenção", "Corrija os erros antes de salvar.", "OK");
                return;
            }

            await App.Db.Insert(produto);
            await DisplayAlert("Sucesso!", "Registro inserido", "Ok");
            await Navigation.PopAsync();
        }
    }
}