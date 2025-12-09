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
        
        static int menu()
        {
            Console.WriteLine("\n=== Menu de Opções ===");
            Console.WriteLine("1. Adicionar Aresta");
            Console.WriteLine("2. Remover Aresta");
            Console.WriteLine("3. Mostrar Matriz");
            Console.WriteLine("4. Verificar Propriedades");
            Console.WriteLine("5. Matriz R infinito");
            Console.WriteLine("6. Matriz Conexividade");
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
            meuGrafo.carregarMatrizDeArquivo("matrizSimetrica.txt");
            int opcao=0;
            do
            {
               opcao = menu();
                switch (opcao)
                {
                    case 1:
                        
                        break;
                    case 2:
                       
                        break;
                    case 3:
                        
                    case 4:
                        
                    case 5:
                        
                    case 6:
                        
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }while (opcao != 0);
        }
    }
}
