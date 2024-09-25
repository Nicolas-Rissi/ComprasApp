using System.Collections.ObjectModel;
using ComprasApp.Models;

namespace ComprasApp
{
    public partial class MainPage : ContentPage
    {
        // Coleção observável de produtos, que atualiza a interface automaticamente.
        ObservableCollection<Produto> lista_produtos = new();

        // Construtor da classe MainPage.
        public MainPage()
        {
            InitializeComponent(); // Inicializa os componentes da interface.
            lst_produtos.ItemsSource = lista_produtos; // Define a fonte de itens da lista de produtos.
        }

        // Método que é acionado quando o item da barra de ferramentas para somar produtos é clicado.
        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            // Calcula a soma do total dos produtos na lista.
            double soma = lista_produtos.Sum(i => i.Total);
            string msg = $"O total dos produtos é {soma:C}"; // Formata a soma como moeda.
            DisplayAlert("Resultado", msg, "Fechar"); // Exibe o resultado em um alerta.
        }

        // Método que é acionado quando o item da barra de ferramentas para adicionar um novo produto é clicado.
        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            // Navega para a página de criação de um novo produto.
            await Navigation.PushAsync(new Views.NovoProduto());
        }

        // Método que é chamado quando a página aparece.
        protected async override void OnAppearing()
        {
            // Verifica se a lista de produtos está vazia.
            if (lista_produtos.Count == 0)
            {
                // Busca todos os produtos do banco de dados.
                List<Produto> tmp = await App.Database.GetAll();
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p); // Adiciona os produtos à lista observável.
                }
            }
        }

        // Método que é acionado quando o texto de busca é alterado.
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue; // Obtém o novo valor de texto da busca.
            lista_produtos.Clear(); // Limpa a lista atual de produtos.

            // Busca produtos que correspondem ao texto inserido.
            List<Produto> tmp = await App.Database.Search(q);
            foreach (Produto p in tmp)
            {
                lista_produtos.Add(p); // Adiciona os produtos encontrados à lista.
            }
        }

        // Método que é acionado quando um item da lista de produtos é selecionado.
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto; // Obtém o produto selecionado.

            // Navega para a página de edição de produto, passando o produto como contexto de ligação.
            Navigation.PushAsync(new Views.EditarProduto { BindingContext = p });
        }

        // Método que é acionado quando um item do menu (para remover um produto) é clicado.
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem; // Obtém o item de menu clicado.
                Produto p = selecionado.BindingContext as Produto; // Obtém o produto associado ao item de menu.

                // Exibe um alerta de confirmação antes de remover o produto.
                bool confirm = await DisplayAlert("Tem certeza?", "Remover Produto?", "Sim", "Não");

                if (confirm)
                {
                    // Remove o produto do banco de dados e da lista.
                    await App.Database.Delete(p.Id);
                    await DisplayAlert("Sucesso!", "Produto removido", "OK");
                    lista_produtos.Remove(p); // Remove o produto da lista observável.
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe uma mensagem de erro.
                await DisplayAlert("Ops", ex.Message, "Fechar");
            }
        }
    }


}
