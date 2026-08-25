namespace LocaControl.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Modelo { get; set; }
        public decimal ValorDiaria { get; set; }
        public bool Disponivel { get; set; }
    }
}
