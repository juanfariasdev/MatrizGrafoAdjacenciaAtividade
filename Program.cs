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
        static void Main(string[] args)
        {
            Grafo meuGrafo = new Grafo(10);
            meuGrafo.carregarMatrizDeArquivo("matrizSimetrica.txt");
            int opcao=0;
            do
            {
               // opcao = menu();
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
