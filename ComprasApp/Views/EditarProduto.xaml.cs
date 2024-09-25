using ComprasApp.Models;

namespace ComprasApp.Views;

public partial class EditarProduto : ContentPage
{
    // Construtor da classe que inicializa os componentes da página.
    public EditarProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface de usuário.
    }

    // Evento que é disparado quando o item da barra de ferramentas é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o produto anexado ao contexto de ligação da página.
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os dados atualizados.
            Produto p = new Produto()
            {
                Id = produto_anexado.Id, // Mantém o ID do produto existente para atualização.
                Descricao = txt_descricao.Text, // Obtém a descrição do campo de texto.
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o texto do preço para double.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para double.
            };

            // Chama o método Update da base de dados para atualizar o produto.
            await App.Database.Update(p);
            // Exibe um alerta de sucesso.
            await DisplayAlert("Sucesso", "Atualizado", "Ok");
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
