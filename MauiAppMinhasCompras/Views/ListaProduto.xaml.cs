namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
			// inserindo mensagem de erro caso dê algum erro ao usuário
		{
			Navigation.PushAsync(new Views.NovoProduto());

		}catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Ok");
		}
    }
}