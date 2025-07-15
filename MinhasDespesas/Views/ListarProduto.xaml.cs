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
        try
        {
            lista.Clear();
            List<Produto> temp = await App.Db.GetAll();
            temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Mecanismo de busca de produtos 
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {

            string searchText = e.NewTextValue;

            lista.Clear(); // Limpa a lista atual para evitar duplicação de itens

            List<Produto> temp = await App.Db.Search(searchText);

            temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Método para somar todos os produtos da nossa lista
    private void ToolbarItem_Somar(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        DisplayAlert("Total dos produtos: ", $"{soma:C}", "OK");
    }

    // Método para remover um item no MenuItem
    private async void MenuItem_Clicked_Remove(object sender, EventArgs e)
    {
        try
        {
            // Confirmando que o usuário vai selecionar o MenuItem
            MenuItem selected = sender as MenuItem;
            // Pega os dados associados a um item selecionado na interface do 
            // usuario e trata ele como um Produto
            Produto prod = selected.BindingContext as Produto;
            
            bool confirm = await DisplayAlert(
                "Tem certeza?"
                ,$"Remover {prod.Descricao}?",
                "Sim", "Não");
            
            if (confirm) {

                await App.Db.Delete(prod.Id);
                lista.Remove(prod);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // 
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto prod = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = prod,
            });
        } 
        catch (Exception ex) 
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}