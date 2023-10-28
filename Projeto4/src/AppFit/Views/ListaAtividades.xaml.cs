using AppFit.ViewModels;

namespace AppFit.Views;

public partial class ListaAtividades : ContentPage
{
    public ListaAtividades()
    {
        InitializeComponent();
        BindingContext = new ListaAtividadesViewModel();
    }

    protected override void OnAppearing()
    {
        var vm = (ListaAtividadesViewModel)BindingContext;
        vm.AtualizarLista.Execute(null);
    }
}