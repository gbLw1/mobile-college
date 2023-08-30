namespace IMC;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void CalcularIMC(object sender, EventArgs e)
    {
        try
        {
            double peso = double.Parse(txt_peso.Text);
            double altura = double.Parse(txt_altura.Text);

            //if (!double.TryParse(txt_altura.Text, out double altura)
            //   || !double.TryParse(txt_peso.Text, out double peso))
            //{
            //    DisplayAlert("Erro", "Coloque as informações corretas", "Ok sou debiloide 😒");
            //    return;
            //}

            double imc = (peso / (altura * altura));
            if (double.IsNaN(imc) || double.IsInfinity(imc))
            {
                DisplayAlert("Erro", "Resultado inválido, tente novamente com outros valores.", "OK");
                return;
            }

            txt_res.IsVisible = true;
            btn_limpar.IsVisible = true;
            txt_res.Text = $"Seu imc é: {imc:F2}";
            tbl_referencia.IsVisible = true;
        }
        catch (FormatException)
        {
            DisplayAlert("Erro", "Formato inválido, verifique os campos preenchidos e tente novamente!", "OK");
        }
        catch (ArgumentNullException)
        {
            DisplayAlert("Erro", "Preencha os campos corretamente!", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void CalcularIMC2(object sender, EventArgs e)
    {
        if (double.TryParse(txt_altura.Text, out double altura)
            && double.TryParse(txt_peso.Text, out double peso))
        {
            double imc = (peso / (altura * altura));
            DisplayAlert("Seu IMC é: ", imc.ToString(), "Massa viu pai 😎😎😎🔥");
        }
        else
        {
            DisplayAlert("Erro", "Coloque as informações corretas", "Ok sou debiloide 😒");
        }

    }

    private void CalcularIMC3(object sender, EventArgs e)
    {
        try
        {
            double peso = double.Parse(txt_peso.Text);
            double altura = double.Parse(txt_altura.Text);
            double imc = (peso / (altura * altura));

            DisplayAlert("Seu IMC é: ", imc.ToString(), "Massa viu pai 😎😎😎🔥");

            tbl_referencia.IsVisible = true;
            btn_limpar.IsVisible = true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK 🫡");
        }
    }

    private void Limpar(object sender, EventArgs e)
    {
        txt_res.Text = string.Empty;
        txt_res.IsVisible = false;

        txt_altura.Text = string.Empty;
        txt_peso.Text = string.Empty;

        txt_altura.Focus();

        tbl_referencia.IsVisible = false;
        btn_limpar.IsVisible = false;
    }
}

