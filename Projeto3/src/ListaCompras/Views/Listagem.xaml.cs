using ListaCompras.Models;
using System.Collections.ObjectModel;

namespace ListaCompras.Views;

public partial class Listagem : ContentPage
{
    ObservableCollection<Produto> produtos = new();

    public Listagem()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = produtos;
    }

    protected override void OnAppearing()
    {
        if (produtos.Any())
        {
            Task.Run(async () =>
            {
                List<Produto> produtosDb = await App.Database.GetAll();

                foreach (Produto item in produtosDb)
                {
                    produtos.Add(item);
                }
                ref_carregando.IsRefreshing = false;
            });
        }
    }

    public async Task ObterProdutos()
    {
        var produtosDb = await App.Database.GetAll();

        foreach (var produto in produtosDb)
        {
            produtos.Add(produto);
        }

        ref_carregando.IsRefreshing = false;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoProduto());
    }

    private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}