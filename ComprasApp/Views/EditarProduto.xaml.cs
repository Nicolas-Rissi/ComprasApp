using ComprasApp.Models;

namespace ComprasApp.Views;

public partial class EditarProduto : ContentPage
{
    // Construtor da classe que inicializa os componentes da p�gina.
    public EditarProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface de usu�rio.
    }

    // Evento que � disparado quando o item da barra de ferramentas � clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o produto anexado ao contexto de liga��o da p�gina.
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os dados atualizados.
            Produto p = new Produto()
            {
                Id = produto_anexado.Id, // Mant�m o ID do produto existente para atualiza��o.
                Descricao = txt_descricao.Text, // Obt�m a descri��o do campo de texto.
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o texto do pre�o para double.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para double.
            };

            // Chama o m�todo Update da base de dados para atualizar o produto.
            await App.Database.Update(p);
            // Exibe um alerta de sucesso.
            await DisplayAlert("Sucesso", "Atualizado", "Ok");
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
