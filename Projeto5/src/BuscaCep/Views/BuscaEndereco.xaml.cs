using BuscaCep.Models;
using BuscaCep.Services;

namespace BuscaCep.Views;

public partial class BuscaEndereco : ContentPage
{
    public BuscaEndereco()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            act_carregando.IsVisible = true;

            Endereco dados = await DataService.GetEnderecoByCep(txt_cep.Text);

            BindingContext = dados;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            act_carregando.IsVisible = false;
        }
    }
}