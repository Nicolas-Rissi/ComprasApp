using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ComprasApp.Models
{
    // Define a classe Produto
    public class Produto
    {
        // Criam os atributos da classe
        string? _descricao;
        double _quantidade;
        double _preco;

        // Define a propriedade de chave primária e auto increment
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        // Propriedade Descricao que representa uma descrição, podendo ser nula.
        public string? Descricao
        {
            get => _descricao; // Retorna o valor atual de _descricao.
            set
            {
                // Se o valor for nulo, lança uma exceção.
                if (value == null)
                {
                    throw new Exception("Descrição inválida");
                }

                // Se não for nulo, atribui o valor a _descricao.
                _descricao = value;
            }
        }

        // Propriedade Quantidade que representa a quantidade de um item.
        public double Quantidade
        {
            get => _quantidade; // Retorna o valor atual de _quantidade.
            set
            {
                // Tenta converter o valor recebido para double. Se falhar, _quantidade é definido como 0.
                if (!double.TryParse(value.ToString(), out _quantidade))
                {
                    _quantidade = 0;
                }

                // Verifica se a quantidade é menor ou igual a 0 e lança uma exceção se for.
                if (value <= 0)
                {
                    throw new Exception("Quantidade inválida");
                }

                // Atribui o valor válido a _quantidade.
                _quantidade = value;
            }
        }

        // Propriedade Preco que representa o preço de um item.
        public double Preco
        {
            get => _preco; // Retorna o valor atual de _preco.
            set
            {
                // Tenta converter o valor recebido para double. Se falhar, _preco é definido como 0.
                if (!double.TryParse(value.ToString(), out _preco))
                {
                    _preco = 0;
                }

                // Verifica se o preço é menor ou igual a 0 e lança uma exceção se for.
                if (value <= 0)
                {
                    throw new Exception("Preço inválido");
                }

                // Atribui o valor válido a _preco.
                _preco = value;
            }
        }

        // Propriedade Total que calcula o total com base no preço e na quantidade.
        public double Total
        {
            get => Preco * Quantidade; // Retorna o resultado da multiplicação entre Preco e Quantidade.
        }
    }
}
