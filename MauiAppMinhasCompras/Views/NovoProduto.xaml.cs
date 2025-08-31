using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e) // aqui trouxe o evento clicked no bot�o salvar
    {
		try
		{
            // aqui vamos inserir o produto no banco de dados
            // est� dentro do try catch para tratar erro caso aconte�a erro

            Produto p = new Produto // instanciando a classe produto
            {
                // aqui inserindo as propriedades dos produtos
                Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),

			};

            // todo m�todo que tem await precisa estar dentro de um m�todo async
            await App.Db.Insert(p); // aqui estamos chamando o m�todo Insert da classe App que � onde est� a conex�o com o banco de dados
            await DisplayAlert("Sucesso!", "Registro inserido", "Ok"); // aqui estamos exibindo uma mensagem de sucesso ao usu�rio
            await Navigation.PopAsync(); // voltando para a lista dos produtos

        } catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}