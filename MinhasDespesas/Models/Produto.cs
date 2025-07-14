using SQLite;

namespace MinhasDespesas.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int QtdeProduto { get; set; }
        public double ValorProduto { get; set; }
        public double Total{
            get { return ValorProduto * QtdeProduto; }
        }
    }
}
