namespace MatrizGrafo
{
    class Grafo
    {
        // Implementação da classe Grafo usando matriz de adjacência
        private int[,] matrizAdjacencia;
        private int numeroDeVertices;
        public Grafo(int vertices)
        {
            numeroDeVertices = vertices;
            matrizAdjacencia = new int[vertices, vertices];
        }
        public void adicionarAresta(int origem, int destino)
        {
            matrizAdjacencia[origem, destino] = 1;
        }
        public void removerAresta(int origem, int destino)
        {
            matrizAdjacencia[origem, destino] = 0;
        }

        public bool carregarMatrizDeArquivo(string caminhoArquivo)
        {

            string[] linhas = File.ReadAllLines(caminhoArquivo);

            for (int i = 0; i < numeroDeVertices; i++)
            {
                string linha = linhas[i].Trim();
                string[] valores = linha.Split(',');

                for (int j = 0; j < numeroDeVertices; j++)

                    matrizAdjacencia[i, j] = int.Parse(valores[j]);
            }

            return true;
        }




        public void mostrarMatriz()
        {
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    System.Console.Write(matrizAdjacencia[i, j] + " ");
                }
                System.Console.WriteLine();
            }
        }

        public bool eReflexiva()
        {
            //implementar
            return false;
        }

        public bool eSimetrica()
        {
            //implementar
            return false;
        }

        public int[,] obterCaminho2()
        {
            int[,] caminho2 = new int[numeroDeVertices, numeroDeVertices];
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    for (int k = 0; k < numeroDeVertices; k++)
                    {
                        caminho2[i, j] += matrizAdjacencia[i, k] * matrizAdjacencia[k, j];
                    }
                }
            }
            return caminho2;
        }

        public bool verificarTransitividade()
        {
            int[,] caminho2 = obterCaminho2();
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    if (caminho2[i, j] == 1 && matrizAdjacencia[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int[,] obterRInfinito()
        {
            int[,] rInfinito = new int[numeroDeVertices, numeroDeVertices];
            // Inicializa R infinito com a matriz de adjacência
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    rInfinito[i, j] = matrizAdjacencia[i, j];
                }
            }

            // Aplica o algoritmo de Warshall para calcular R infinito
            for (int k = 0; k < numeroDeVertices; k++)
            {
                for (int i = 0; i < numeroDeVertices; i++)
                {
                    for (int j = 0; j < numeroDeVertices; j++)
                    {
                        rInfinito[i, j] = rInfinito[i, j] | (rInfinito[i, k] & rInfinito[k, j]);
                    }
                }
            }
            return rInfinito;
        }

        public int[,] obterMatrizConexividade()
        {
            int[,] conexividade = new int[numeroDeVertices, numeroDeVertices];
            int[,] rInfinito = obterRInfinito();

            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    conexividade[i, j] = rInfinito[i, j] | rInfinito[j, i];
                }
            }
            return conexividade;
        }



    }
}