using SQLite;

namespace MinhasDespesas.Models
{
    public class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrEmpty(value)) 
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value; 
            }
        }
        public int QtdeProduto { get; set; }
        public double ValorProduto { get; set; }
        public double Total
        {
            get
            {
                return QtdeProduto * ValorProduto;
            }
        }
    }
}
