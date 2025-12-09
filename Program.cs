using System;
namespace MatrizGrafo
{
    class Program
    {
        static void imprimirMatriz(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0);
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    System.Console.Write(matriz[i, j] + " ");
                }
                System.Console.WriteLine();
            }
        }
        
        static string selecionarGrafo()
        {
            string pastaGrafos = "GrafosAlunos";
            
            if (!Directory.Exists(pastaGrafos))
            {
                Console.WriteLine($"Pasta {pastaGrafos} não encontrada!");
                return null;
            }
            
            string[] arquivos = Directory.GetFiles(pastaGrafos, "*.txt");
            
            if (arquivos.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo de grafo encontrado!");
                return null;
            }
            
            Console.WriteLine("\n=== Grafos Disponíveis ===");
            for (int i = 0; i < arquivos.Length; i++)
            {
                string nomeArquivo = Path.GetFileNameWithoutExtension(arquivos[i]);
                Console.WriteLine($"{i + 1}. {nomeArquivo}");
            }
            
            Console.Write($"\nEscolha um grafo (1-{arquivos.Length}) ou 0 para cancelar: ");
            int escolha;
            while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 0 || escolha > arquivos.Length)
            {
                Console.Write($"Entrada inválida. Digite um número entre 0 e {arquivos.Length}: ");
            }
            
            if (escolha == 0)
                return null;
                
            return arquivos[escolha - 1];
        }
        
        static int menu(string arquivoAtual)
        {
            Console.WriteLine("\n=== Menu de Opções ===");
            Console.WriteLine($"Grafo atual: {Path.GetFileNameWithoutExtension(arquivoAtual)}");
            Console.WriteLine("\n1. Adicionar Aresta");
            Console.WriteLine("2. Remover Aresta");
            Console.WriteLine("3. Mostrar Matriz");
            Console.WriteLine("4. Verificar Propriedades");
            Console.WriteLine("5. Matriz R infinito");
            Console.WriteLine("6. Matriz Conexividade");
            Console.WriteLine("7. Selecionar Grafo");
            Console.WriteLine("8. Mostrar R² (Caminhos de Comprimento 2)");
            Console.WriteLine("9. Desenhar Grafo Original");
            Console.WriteLine("10. Desenhar Grafo R²");
            Console.WriteLine("11. Desenhar Grafo R∞");
            Console.WriteLine("0. Sair");
            Console.Write("\nEscolha uma opção: ");
            
            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Write("Entrada inválida. Digite um número: ");
            }
            return opcao;
        }
        
        static void Main(string[] args)
        {
            Grafo meuGrafo = new Grafo(10);
            string arquivoAtual = "matrizSimetrica.txt";
            meuGrafo.carregarMatrizDeArquivo(arquivoAtual);
            int opcao=0;
            do
            {
               opcao = menu(arquivoAtual);
                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o vértice de origem: ");
                        int origem = int.Parse(Console.ReadLine());
                        Console.Write("Digite o vértice de destino: ");
                        int destino = int.Parse(Console.ReadLine());
                        meuGrafo.adicionarAresta(origem, destino);
                        Console.WriteLine($"Aresta adicionada: {origem} -> {destino}");
                        break;
                    case 2:
                        Console.Write("Digite o vértice de origem: ");
                        origem = int.Parse(Console.ReadLine());
                        Console.Write("Digite o vértice de destino: ");
                        destino = int.Parse(Console.ReadLine());
                        meuGrafo.removerAresta(origem, destino);
                        Console.WriteLine($"Aresta removida: {origem} -> {destino}");
                        break;
                    case 3:
                        Console.WriteLine("\nMatriz de Adjacência:");
                        meuGrafo.mostrarMatriz();
                        break;
                    case 4:
                        meuGrafo.verificarReflexivaDetalhado();
                        meuGrafo.verificarSimetricaDetalhado();
                        meuGrafo.verificarTransitividadeDetalhado();
                        break;
                    case 5:
                        Console.WriteLine("\nMatriz R∞ (Fecho Transitivo):");
                        int[,] rInfinito = meuGrafo.obterRInfinito();
                        imprimirMatriz(rInfinito);
                        break;
                    case 6:
                        Console.WriteLine("\nMatriz de Conexividade:");
                        int[,] conexividade = meuGrafo.obterMatrizConexividade();
                        imprimirMatriz(conexividade);
                        break;
                    case 7:
                        string novoArquivo = selecionarGrafo();
                        if (novoArquivo != null)
                        {
                            arquivoAtual = novoArquivo;
                            meuGrafo = new Grafo(10);
                            meuGrafo.carregarMatrizDeArquivo(arquivoAtual);
                            Console.WriteLine($"\nGrafo '{Path.GetFileNameWithoutExtension(arquivoAtual)}' carregado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("\nSeleção cancelada.");
                        }
                        break;
                    case 8:
                        Console.WriteLine("\nMatriz R² (Caminhos de Comprimento 2):");
                        int[,] r2 = meuGrafo.obterCaminho2();
                        imprimirMatriz(r2);
                        break;
                    case 9:
                        meuGrafo.desenharGrafoOriginal();
                        break;
                    case 10:
                        meuGrafo.desenharGrafoR2();
                        break;
                    case 11:
                        meuGrafo.desenharGrafoRInfinito();
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }while (opcao != 0);
        }
    }
}
