using BuscaCep.Models;
using System.Text.Json;

namespace BuscaCep.Services;

public class DataService
{
    public static async Task<Endereco> GetEnderecoByCep(string cep)
    {
        Endereco endereco;

        using (HttpClient client = new())
        {
            string url = $"https://cep.metoda.com.br/endereco/by-cep?cep={cep}";

            HttpResponseMessage resposta = await client.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                string json = resposta.Content.ReadAsStringAsync().Result;

                endereco = JsonSerializer.Deserialize<Endereco>(json);
            }
            else
                throw new Exception(resposta.RequestMessage.Content.ToString());
        }

        return endereco;
    }
}
