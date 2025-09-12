using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto(Produto produto)
        {
            InitializeComponent();
            BindingContext = produto;
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            var produto = BindingContext as Produto;

            // For�a a valida��o de todos os campos
            produto.ValidateAll();

            if (produto.HasErrors)
            {
                await DisplayAlert("Aten��o", "Corrija os erros antes de salvar.", "OK");
                return;
            }

            await App.Db.Update(produto);
            await DisplayAlert("Sucesso!", "Registro atualizado", "Ok");
            await Navigation.PopAsync();
        }
    }
}