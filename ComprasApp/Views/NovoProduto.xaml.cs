using ComprasApp.Models;

namespace ComprasApp.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da classe que inicializa os componentes da p�gina.
    public NovoProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface de usu�rio.
    }

    // Evento que � disparado quando o item da barra de ferramentas � clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados fornecidos pelo usu�rio.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Obt�m a descri��o do campo de texto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para double.
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o texto do pre�o para double.
            };

            // Chama o m�todo Insert da base de dados para adicionar o novo produto.
            await App.Database.Insert(p);
            // Exibe um alerta de sucesso ao usu�rio.
            await DisplayAlert("Sucesso!", "Produto inserido", "Ok");
            // Navega de volta para a p�gina principal.
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe um alerta com a mensagem de erro.
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
    }
}
