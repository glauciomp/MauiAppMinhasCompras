using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        // Coleção observável para atualizar a UI automaticamente
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = CarregarProdutos();
        }

        private async Task CarregarProdutos()
        {
            try
            {
                lista.Clear();
                List<Produto> tmp = await App.Db.GetAll();
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            await PerformSearch(e.NewTextValue);
        }

        private async void txt_search_cat_TextChanged(object sender, TextChangedEventArgs e)
        {
            await PerformCategorySearch(e.NewTextValue);
        }

        private async Task PerformSearch(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    await CarregarProdutos();
                }
                else
                {
                    lst_produtos.IsRefreshing = true;
                    lista.Clear();
                    List<Produto> tmp = await App.Db.Search(query);
                    tmp.ForEach(i => lista.Add(i));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private async Task PerformCategorySearch(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    await CarregarProdutos();
                }
                else
                {
                    lst_produtos.IsRefreshing = true;
                    lista.Clear();
                    List<Produto> tmp = await App.Db.SearchByCategory(query);
                    tmp.ForEach(i => lista.Add(i));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                double soma = lista.Sum(i => i.Total);
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
                    bool resposta = await DisplayAlert("Confirmação!",
                                                       $"Deseja realmente remover o produto '{produtoASerRemovido.Descricao}'?",
                                                       "Sim", "Não");

                    if (resposta)
                    {
                        await App.Db.Delete(produtoASerRemovido.Id);
                        lista.Remove(produtoASerRemovido);
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
                if (p != null)
                {
                    Navigation.PushAsync(new EditarProduto(p));
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {
                await CarregarProdutos();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private async void somartudo(object sender, EventArgs e)
        {
            try
            {
                if (!lista.Any())
                {
                    await DisplayAlert("Aviso", "Nenhum produto na lista.", "Ok");
                    return;
                }

                var somaCategorias = lista
                    .GroupBy(p => p.Categoria)
                    .Select(g => new { Categoria = g.Key, Total = g.Sum(p => p.Total) })
                    .ToList();

                string msg = string.Join("\n", somaCategorias.Select(c => $"{c.Categoria}: {c.Total:C}"));

                await DisplayAlert("Total por Categoria", msg, "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
    }
}