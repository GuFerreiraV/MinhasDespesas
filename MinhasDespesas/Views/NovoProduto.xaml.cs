using MinhasDespesas.Models;

namespace MinhasDespesas.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto prod = new Produto
			{
				Descricao = txt_descricao.Text,
				QtdeProduto = Convert.ToInt32(txt_quantidade.Text),
				ValorProduto = Convert.ToDouble(txt_preco.Text)
			};
            // inserindo o produto no banco de dados
            await App.Db.Insert(prod);
			await DisplayAlert("Sucesso", "Registro sucedido", "Ok");
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}