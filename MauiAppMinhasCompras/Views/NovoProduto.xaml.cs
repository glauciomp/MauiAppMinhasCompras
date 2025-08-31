using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e) // aqui trouxe o evento clicked no botão salvar
    {
		try
		{
            // aqui vamos inserir o produto no banco de dados
            // está dentro do try catch para tratar erro caso aconteça erro

            Produto p = new Produto // instanciando a classe produto
            {
                // aqui inserindo as propriedades dos produtos
                Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),

			};

            // todo método que tem await precisa estar dentro de um método async
            await App.Db.Insert(p); // aqui estamos chamando o método Insert da classe App que é onde está a conexão com o banco de dados
            await DisplayAlert("Sucesso!", "Registro inserido", "Ok"); // aqui estamos exibindo uma mensagem de sucesso ao usuário
            await Navigation.PopAsync(); // voltando para a lista dos produtos

        } catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}