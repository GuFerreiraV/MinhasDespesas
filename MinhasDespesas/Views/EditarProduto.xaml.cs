using MinhasDespesas.Models;
using System.Threading.Tasks;

namespace MinhasDespesas.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Save(object sender, EventArgs e)
    {
        try
        {
            Produto selected_prod = BindingContext as Produto;

            Produto prod = new Produto
            {
                Id = selected_prod.Id, // Mantém o ID do produto selecionado
                Descricao = txt_descricao.Text,
                QtdeProduto = Convert.ToInt32(txt_qtde.Text),
                ValorProduto = Convert.ToDouble(txt_preco.Text)
            };
            await App.Db.Update(prod);
            await DisplayAlert("Sucesso", "Produto atualizado com sucesso", "Ok");
            await Navigation.PopAsync(); // Regressa a tela anterior
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}