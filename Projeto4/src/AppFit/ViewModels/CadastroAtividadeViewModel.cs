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
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(descricao)));
        }
    }

    private string observacoes;
    public string Observacoes
    {
        get => observacoes;
        set
        {
            observacoes = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(observacoes)));
        }
    }

    private int id;
    public int Id
    {
        get => id;
        set
        {
            id = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(id)));
        }
    }

    private DateTime data;
    public DateTime Data
    {
        get => data;
        set
        {
            data = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(data)));
        }
    }

    private double? peso;
    public double? Peso
    {
        get => peso;
        set
        {
            peso = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(peso)));
        }
    }

    public ICommand VerAtividade
    {
        get => new Command<int>(async (int id) =>
        {
            try
            {
                Atividade model = await App.Database.GetById(id);

                this.Id = model.Id;
                this.Data = model.Data;
                this.Peso = model.Peso;
                this.Descricao = model.Descricao;
                this.Observacoes = model.Observacoes;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ops", ex.Message, "OK");
            }
        });
    }

    public ICommand NovaAtividade
    {
        get => new Command(() =>
        {
            Id = 0;
            Descricao = string.Empty;
            Data = DateTime.Now;
            Peso = null;
            Observacoes = string.Empty;

        });
    }

    public ICommand SalvarAtividade
    {
        get => new Command(async () =>
        {
            try
            {
                Atividade model = new()
                {
                    Descricao = this.Descricao,
                    Data = this.Data,
                    Peso = this.Peso,
                    Observacoes = this.observacoes,
                };

                if (this.Id == 0)
                {
                    await App.Database.Insert(model);
                }
                else
                {
                    model.Id = this.Id;
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
}
