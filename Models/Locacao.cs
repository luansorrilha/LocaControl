namespace LocaControl.Models
{
    public class Locacao
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

        public decimal ValorTotal { get; set; }
    }
}