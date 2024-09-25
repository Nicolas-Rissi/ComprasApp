using ComprasApp.Models;

namespace ComprasApp.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da classe que inicializa os componentes da página.
    public NovoProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface de usuário.
    }

    // Evento que é disparado quando o item da barra de ferramentas é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados fornecidos pelo usuário.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Obtém a descrição do campo de texto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para double.
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o texto do preço para double.
            };

            // Chama o método Insert da base de dados para adicionar o novo produto.
            await App.Database.Insert(p);
            // Exibe um alerta de sucesso ao usuário.
            await DisplayAlert("Sucesso!", "Produto inserido", "Ok");
            // Navega de volta para a página principal.
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe um alerta com a mensagem de erro.
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
    }
}
