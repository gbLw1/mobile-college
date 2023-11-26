namespace BuscaCep.Models;

public class Endereco
{
    public int id_logradouro { get; set; }
    public int id_cidade { get; set; }
    public string tipo { get; set; }
    public string descricao { get; set; }
    public string uf { get; set; }
    public string complemento { get; set; }
    public string descricao_sem_numero { get; set; }
    public string descricao_cidade { get; set; }
    public string descricao_bairro { get; set; }
    public string cep { get; set; }
}
