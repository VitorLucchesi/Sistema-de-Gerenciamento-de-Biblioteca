// See https://aka.ms/new-console-template for more information
using System;

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string COD { get; set; }
    public bool Disponivel { get; set; }

    public Livro(string titulo, string autor, string cod)
    {
        Titulo = titulo;
        Autor = autor;
        COD = cod;
        Disponivel = true;
    }

    public void ExibirDetalhes()
    {
        Console.WriteLine($"Título: {Titulo}, Autor: {Autor}, COD: {COD}, Disponível: {(Disponivel ? "Sim" : "Não")}");
    }
}

class ItemBiblioteca
{
    private List<Livro> livros;

    public ItemBiblioteca()
    {
        livros = new List<Livro>();
    }

    public void IncluirLivro(Livro livro)
    {
        livros.Add(livro);
        Console.WriteLine("Livro incluído com sucesso!");
    }

    public void AdicionarLivro(Livro livro)
    {
        livros.Add(livro);
        Console.WriteLine("Livro adicionado com sucesso!");
    }

    public void CadastrarLivro()
    {
        Console.WriteLine("Digite o título do livro:");
        string titulo = Console.ReadLine();

        Console.WriteLine("Digite o autor do livro:");
        string autor = Console.ReadLine();

        Console.WriteLine("Digite o Código do livro:");
        string isbn = Console.ReadLine();

        Livro novoLivro = new Livro(titulo, autor, isbn);

        AdicionarLivro(novoLivro);
    }

    public void ListarLivros()
    {
        if (livros.Count == 0)
        {
            Console.WriteLine("Não temos este livro na biblioteca.");
            return;
        }

        foreach (var livro in livros)
        {
            livro.ExibirDetalhes();
        }
    }

    public void EmprestarLivro(string cod)
    {
        var livro = livros.Find(l => l.COD == cod);

        if (livro != null)
        {
            if (livro.Disponivel)
            {
                livro.Disponivel = false;
                Console.WriteLine($"Livro '{livro.Titulo}' emprestado com sucesso.");
            }
            else
            {
                Console.WriteLine($"O livro '{livro.Titulo}' não está disponível no momento.");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    public void BuscarLivrosPorTitulo(string titulo)
    {
        var livrosEncontrados = livros.Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();

        if (livrosEncontrados.Count > 0)
        {
            foreach (var livro in livrosEncontrados)
            {
                livro.ExibirDetalhes();
            }
        }
        else
        {
            Console.WriteLine($"Nenhum livro encontrado com o título '{titulo}'.");
        }
    }

    public void BuscarLivrosPorAutor(string autor)
    {
        var livrosEncontrados = livros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();

        if (livrosEncontrados.Count > 0)
        {
            foreach (var livro in livrosEncontrados)
            {
                livro.ExibirDetalhes();
            }
        }
        else
        {
            Console.WriteLine($"Nenhum livro encontrado do autor '{autor}'.");
        }
    }
    public void DevolverLivro(string cod)
    {
        var livro = livros.Find(l => l.COD == cod);

        if (livro != null)
        {
            if (!livro.Disponivel)
            {
                livro.Disponivel = true;
                Console.WriteLine($"Livro '{livro.Titulo}' devolvido com sucesso.");
            }
            else
            {
                Console.WriteLine($"O livro '{livro.Titulo}' não estava emprestado.");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }
}

class Sistema
{
    static void Main()
    {
        ItemBiblioteca biblioteca = new ItemBiblioteca();

        biblioteca.IncluirLivro(new Livro("O poder do hábito", " Charles Duhigg", "01"));
        biblioteca.IncluirLivro(new Livro("Psicanálise de boteco", "Alexandre Patricio de Almeida ", "02"));
        biblioteca.IncluirLivro(new Livro("O animal social", "Joshua Aronson", "03"));
        biblioteca.IncluirLivro(new Livro("Pai Rico, pai Pobre", "Kiyosaki Robert T ", "04"));
        biblioteca.IncluirLivro(new Livro("O corpo fala", "Pierre Weil", "05"));

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("Sistema de Gestão de Biblioteca");
            Console.WriteLine("1. Listar Livros");
            Console.WriteLine("2. Emprestar Livro");
            Console.WriteLine("3. Devolver Livro");
            Console.WriteLine("4. Buscar Livro por Título");
            Console.WriteLine("5. Buscar Livro por Autor");
            Console.WriteLine("6. Adicionar Novo Livro");
            Console.WriteLine("7. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    biblioteca.ListarLivros();
                    break;

                case "2":
                    Console.Write("Digite o Código do livro para emprestar: ");
                    string isbnEmprestimo = Console.ReadLine();
                    biblioteca.EmprestarLivro(isbnEmprestimo);
                    break;

                case "3":
                    Console.Write("Digite o Código do livro para devolver: ");
                    string isbnDevolucao = Console.ReadLine();
                    biblioteca.DevolverLivro(isbnDevolucao);
                    break;

                case "4":
                    Console.Write("Digite o título do livro para buscar: ");
                    string tituloBusca = Console.ReadLine();
                    biblioteca.BuscarLivrosPorTitulo(tituloBusca);
                    break;

                case "5":
                    Console.Write("Digite o autor para buscar livros: ");
                    string autorBusca = Console.ReadLine();
                    biblioteca.BuscarLivrosPorAutor(autorBusca);
                    break;

                case "6":
                    biblioteca.CadastrarLivro();
                    break;

                case "7":
                    continuar = false;
                    Console.WriteLine("Saindo do sistema...");
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}