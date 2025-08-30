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
		List<Produto> tmp = await App.Db.GetAll();
		tmp.ForEach(i => lista.Add(i)); // aqui estamos populando a lista com os produtos do banco de dados
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) // evento clicked inserido posteriormente ao programar ListaProduto.xaml
    {
		try
			// inserindo mensagem de erro caso d� algum erro ao usu�rio
		{
			Navigation.PushAsync(new Views.NovoProduto()); // aqui estamos navegando para a p�gina NovoProduto

        }
        catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Ok");
		}
    }

	private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

		tmp.ForEach(i => lista.Add(i));

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total); // aqui estamos somando os pre�os dos produtos da lista

		string msg = $"O total � {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try 
		{ 
			var produtoASerRemovido = (sender as MenuItem)?.BindingContext as Produto;
			
			if (produtoASerRemovido != null)
			{
				await App.Db.Delete(produtoASerRemovido.Id);
            }
        }
		catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}