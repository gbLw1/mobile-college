using AppFit.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace AppFit.ViewModels;

[QueryProperty("PegarIdDaNavegacao", "parametro_id")]
public class CadastroAtividadeViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public string PegarIdDaNavegacao
    {
        set
        {
            int id_parametro = int.Parse(Uri.UnescapeDataString(value));

            VerAtividade.Execute(id_parametro);
        }
    }

    private string descricao;
    public string Descricao
    {
        get => descricao;
        set
        {
            descricao = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Descricao)));
        }
    }

    private string observacoes;
    public string Observacoes
    {
        get => observacoes;
        set
        {
            observacoes = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Observacoes)));
        }
    }

    private int id;
    public int Id
    {
        get => id;
        set
        {
            id = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Id)));
        }
    }

    private DateTime data;
    public DateTime Data
    {
        get => data;
        set
        {
            data = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
        }
    }

    private double? peso;
    public double? Peso
    {
        get => peso;
        set
        {
            peso = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Peso)));
        }
    }

    public ICommand VerAtividade => new Command<int>(async (int id) =>
    {
        try
        {
            Atividade model = await App.Database.GetById(id);

            Id = model.Id;
            Data = model.Data;
            Peso = model.Peso;
            Descricao = model.Descricao;
            Observacoes = model.Observacoes;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Ops", ex.Message, "OK");
        }
    });

    public ICommand NovaAtividade => new Command(() =>
    {
        Id = 0;
        Descricao = string.Empty;
        Data = DateTime.Now;
        Peso = null;
        Observacoes = string.Empty;

    });

    public ICommand SalvarAtividade => new Command(async () =>
    {
        try
        {
            Atividade model = new()
            {
                Descricao = Descricao,
                Data = Data,
                Peso = Peso,
                Observacoes = observacoes,
            };

            if (Id == 0)
            {
                await App.Database.Insert(model);
            }
            else
            {
                model.Id = Id;
                await App.Database.Update(model);
            }

            await Application.Current.MainPage.DisplayAlert(
                "Beleza", "Atividade salva", "OK");
            await Shell.Current.GoToAsync("//MinhasAtividades");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Ops", ex.Message, "OK");
        }
    });
}
