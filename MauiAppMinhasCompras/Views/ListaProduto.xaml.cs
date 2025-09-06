using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista; // aqui estamos vinculando a lista ao ListView
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i)); // aqui estamos populando a lista com os produtos do banco de dados

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }


    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) // evento clicked inserido posteriormente ao programar ListaProduto.xaml
    {
        try
        // inserindo mensagem de erro caso dê algum erro ao usuário
        {
            Navigation.PushAsync(new Views.NovoProduto()); // aqui estamos navegando para a página NovoProduto

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {

            double soma = lista.Sum(i => i.Total); // aqui estamos somando os preços dos produtos da lista

            string msg = $"O total é {soma:C}";

            DisplayAlert("Total dos Produtos", msg, "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var produtoASerRemovido = (sender as MenuItem)?.BindingContext as Produto;

            if (produtoASerRemovido != null)
            {
                // Adiciona o pop-up de confirmação
                bool resposta = await DisplayAlert("Confirmação!",
                                                   $"Deseja realmente remover o produto '{produtoASerRemovido.Descricao}'?",
                                                   "Sim",
                                                   "Não");

                // Se o usuário clicar em "Sim"
                if (resposta)
                {
                    // 1. Remove o produto do banco de dados
                    await App.Db.Delete(produtoASerRemovido.Id);

                    // 2. Remove o produto da ObservableCollection para atualizar a UI
                    lista.Remove(produtoASerRemovido);

                    // Opcional: exibe uma mensagem de sucesso
                    await DisplayAlert("Sucesso", "Produto removido com sucesso!", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync
             (new Views.EditarProduto
                 {
                     BindingContext = p,
                 }
             );
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}