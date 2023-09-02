namespace JogoDaVelha;

public partial class MainPage : ContentPage
{
    Player Player { get; set; } = Player.X;

    public MainPage() => InitializeComponent();

    async void HandleClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        btn.IsEnabled = false;

        if (Player is Player.X)
        {
            btn.Text = "X";
            Player = Player.O;
        }
        else
        {
            btn.Text = "O";
            Player = Player.X;
        }

        if (VerificarVencedor(Player.X))
        {
            await DisplayAlert("GG", "O jogador ❌ venceu!", "Jogar novamente.");
            JogarNovamente();
        }
        else if (VerificarVencedor(Player.O))
        {
            await DisplayAlert("GG", "O jogador ⭕ venceu!", "Jogar novamente.");
            JogarNovamente();
        }
        else
        {
            await VerificarVelha();
        }

    }

    async Task VerificarVelha()
    {
        if (!string.IsNullOrWhiteSpace(btn10.Text) && !string.IsNullOrWhiteSpace(btn11.Text) && !string.IsNullOrWhiteSpace(btn12.Text)
            && !string.IsNullOrWhiteSpace(btn20.Text) && !string.IsNullOrWhiteSpace(btn21.Text) && !string.IsNullOrWhiteSpace(btn22.Text)
            && !string.IsNullOrWhiteSpace(btn30.Text) && !string.IsNullOrWhiteSpace(btn31.Text) && !string.IsNullOrWhiteSpace(btn32.Text))
        {
            await DisplayAlert("GG", "Deu velha 🥸", "Jogar novamente.");
            JogarNovamente();
        };
    }

    bool VerificarVencedor(Player player)
    {
        // Verificar horizontais
        if ((btn10.Text == player.ToString() && btn11.Text == player.ToString() && btn12.Text == player.ToString())
            || (btn20.Text == player.ToString() && btn21.Text == player.ToString() && btn22.Text == player.ToString())
            || (btn30.Text == player.ToString() && btn31.Text == player.ToString() && btn32.Text == player.ToString()))
        {
            return true;
        }

        // verificar verticais
        if ((btn10.Text == player.ToString() && btn20.Text == player.ToString() && btn30.Text == player.ToString())
            || (btn11.Text == player.ToString() && btn21.Text == player.ToString() && btn31.Text == player.ToString())
            || (btn12.Text == player.ToString() && btn22.Text == player.ToString() && btn32.Text == player.ToString()))
        {
            return true;
        }

        // verificar diagonais
        if ((btn10.Text == player.ToString() && btn21.Text == player.ToString() && btn32.Text == player.ToString())
            || (btn12.Text == player.ToString() && btn21.Text == player.ToString() && btn30.Text == player.ToString()))
        {
            return true;
        }

        return false;
    }

    void JogarNovamente()
    {
        btn10.Text = string.Empty;
        btn10.IsEnabled = true;

        btn11.Text = string.Empty;
        btn11.IsEnabled = true;

        btn12.Text = string.Empty;
        btn12.IsEnabled = true;

        btn20.Text = string.Empty;
        btn20.IsEnabled = true;

        btn21.Text = string.Empty;
        btn21.IsEnabled = true;

        btn22.Text = string.Empty;
        btn22.IsEnabled = true;

        btn30.Text = string.Empty;
        btn30.IsEnabled = true;

        btn31.Text = string.Empty;
        btn31.IsEnabled = true;

        btn32.Text = string.Empty;
        btn32.IsEnabled = true;

        Player = Player.X;
    }
}

enum Player
{
    X = 0,
    O = 1,
}