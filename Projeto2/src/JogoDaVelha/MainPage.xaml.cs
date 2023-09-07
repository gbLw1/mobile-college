using JogoDaVelha.Enums;

namespace JogoDaVelha;

public partial class MainPage : ContentPage
{
    private Player Player { get; set; } = Player.X;
    private string CurrentPlayer => Player is Player.X ? "✖️" : "⭕";

    public int PlayerXPoints { get; set; } = 0;
    public int PlayerOPoints { get; set; } = 0;

    private readonly Button[,] buttons;

    public MainPage()
    {
        InitializeComponent();

        buttons = new Button[,]
        {
            { btn10, btn11, btn12 },
            { btn20, btn21, btn22 },
            { btn30, btn31, btn32 },
        };
    }

    async void HandleClick(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        btn.IsEnabled = false;

        btn.Text = CurrentPlayer;

        if (VerificarVencedor())
        {
            if (Player is Player.X)
            {
                pontuacaoPlayerX.Text = $"✖️ = {++PlayerXPoints}";
            }
            else
            {
                pontuacaoPlayerO.Text = $"⭕ = {++PlayerOPoints}";
            }

            await DisplayAlert("GG", $"O jogador {CurrentPlayer} venceu!", "Jogar novamente.");

            JogarNovamente();
        }
        else
        {
            if (VerificarEmpate())
            {
                await DisplayAlert("GG", "Deu velha 🥸", "Jogar novamente.");

                JogarNovamente();
            }
        }

        Player = Player is Player.X ? Player.O : Player.X;
    }

    bool VerificarVencedor()
    {
        // verificar horizontal (linhas)
        for (int i = 0; i < 3; ++i)
        {
            if (buttons[i, 0].Text == CurrentPlayer &&
                buttons[i, 1].Text == CurrentPlayer &&
                buttons[i, 2].Text == CurrentPlayer)
            {
                return true;
            }
        }

        // verificar vertical (colunas)
        for (int j = 0; j < 3; ++j)
        {
            if (buttons[0, j].Text == CurrentPlayer &&
                buttons[1, j].Text == CurrentPlayer &&
                buttons[2, j].Text == CurrentPlayer)
            {
                return true;
            }
        }

        // Verificar diagonal principal
        if (buttons[0, 0].Text == CurrentPlayer &&
            buttons[1, 1].Text == CurrentPlayer &&
            buttons[2, 2].Text == CurrentPlayer)
        {
            return true;
        }

        // Verificar diagonal secundária
        if (buttons[0, 2].Text == CurrentPlayer &&
            buttons[1, 1].Text == CurrentPlayer &&
            buttons[2, 0].Text == CurrentPlayer)
        {
            return true;
        }

        return false;
    }

    bool VerificarEmpate()
    {
        foreach (Button btn in buttons)
        {
            if (string.IsNullOrEmpty(btn.Text))
            {
                return false;
            }
        }

        return true;
    }

    void ResetarPontuacao(object sender, EventArgs e)
    {
        PlayerXPoints = 0;
        PlayerOPoints = 0;
        pontuacaoPlayerX.Text = "✖️ = 0";
        pontuacaoPlayerO.Text = "⭕ = 0";
    }

    void JogarNovamente()
    {
        foreach (Button botao in buttons)
        {
            botao.Text = string.Empty;
            botao.IsEnabled = true;
        }
    }
}
