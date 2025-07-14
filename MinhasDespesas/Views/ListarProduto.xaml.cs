using MinhasDespesas.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MinhasDespesas.Views;

public partial class ListarProduto : ContentPage
{

    // Coleção para armazenar produtos, integração maior com a UI do XAML
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); 

	public ListarProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    // Método para carregar os produtos do banco de dados
    protected async override void OnAppearing()
    {
		List<Produto> temp = await App.Db.GetAll();
		temp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

		}catch(Exception ex) 
		{
			DisplayAlert("Ops", ex.Message, "Ok");
		}
    }

	// Mecanismo de busca de produtos 
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string searchText = e.NewTextValue;

		lista.Clear(); // Limpa a lista atual para evitar duplicação de itens

        List<Produto> temp = await App.Db.Search(searchText);

		temp.ForEach(i => lista.Add(i));
    }

	// Método para somar todos os produtos da nossa lista
    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		DisplayAlert("Total dos produtos: ",$"{soma:C}", "OK");
    }

    private async void MenuItem_Clicked_Remove(object sender, EventArgs e)
    {
        // sender é o item do menu que o usuário clicou
        if (sender is not MenuItem mI) return;
        // O 'BindingContext' dele é a forma mais direta de saber a qual
        // item da lista (o objeto 'Produto') esse botão pertence.
        if (mI.BindingContext is not Produto produtoPraDeletar) return;

		try
		{
			int searchId = produtoPraDeletar.Id;
            int result = await App.Db.Delete(searchId);
            await DisplayAlert("Sucesso", "Produto removido", "Ok");
        }
		catch (Exception ex) 
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}