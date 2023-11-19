using AppFit.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AppFit.ViewModels;

public class ListaAtividadesViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;
    public string ParametroBusca { get; set; }

    private bool estaAtualizando;
    public bool EstaAtualizando
    {
        get => estaAtualizando;
        set
        {
            estaAtualizando = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EstaAtualizando)));
        }
    }

    private ObservableCollection<Atividade> listaAtividades = new();
    public ObservableCollection<Atividade> ListaAtividades
    {
        get => listaAtividades;
        set => listaAtividades = value;
    }

    public ICommand AtualizarLista => new Command(async () =>
    {
        try
        {
            if (EstaAtualizando)
                return;

            EstaAtualizando = true;
            List<Atividade> tmp = await App.Database.GetAllRows();
            ListaAtividades.Clear();
            tmp.ForEach(i => ListaAtividades.Add(i));

        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ops",
                ex.Message, "OK");
        }
        finally
        {
            EstaAtualizando = false;
        }
    });

    public ICommand Buscar => new Command(async () =>
    {
        try
        {
            if (EstaAtualizando) return;

            EstaAtualizando = true;
            List<Atividade> tmp = await App.Database.Search(ParametroBusca);
            ListaAtividades.Clear();
            tmp.ForEach(i => ListaAtividades.Add(i));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            EstaAtualizando = false;
        }
    });

    public ICommand AbrirDetalhesAtividade => new Command<int>(async (int id) =>
    {
        await Shell.Current.GoToAsync($"//CadastroAtividade?parametro_id={id}");
    });

    public ICommand Remover => new Command<int>(async (int id) =>
    {
        try
        {
            bool conf = await Application.Current.MainPage.DisplayAlert("Tem certeza?", "Excluir", "Sim", "Não");
            if (conf)
            {
                await App.Database.Delete(id);
                AtualizarLista.Execute(null);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            EstaAtualizando = false;
        }
    });
}
