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
            // Verifica se todos os elementos da diagonal principal são 1
            for (int i = 0; i < numeroDeVertices; i++)
            {
                if (matrizAdjacencia[i, i] != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public bool eSimetrica()
        {
            // Verifica se matriz[i,j] == matriz[j,i] para todos i,j
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    if (matrizAdjacencia[i, j] != matrizAdjacencia[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void verificarReflexivaDetalhado()
        {
            Console.WriteLine("\n=== Verificação Reflexiva Detalhada ===");
            Console.WriteLine("Propriedade: Todos os elementos da diagonal principal devem ser 1");
            Console.WriteLine("Ou seja: R[i,i] = 1 para todo vértice i\n");
            
            bool eReflexiva = true;
            List<int> verticesFalham = new List<int>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                if (matrizAdjacencia[i, i] != 1)
                {
                    eReflexiva = false;
                    verticesFalham.Add(i);
                }
            }
            
            if (eReflexiva)
            {
                Console.WriteLine("✓ O grafo É REFLEXIVO");
                Console.WriteLine("  Todos os vértices possuem laço (aresta para si mesmo)");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É REFLEXIVO");
                Console.WriteLine($"  Faltam laços em {verticesFalham.Count} vértice(s):");
                foreach (int v in verticesFalham)
                {
                    Console.WriteLine($"    - Vértice {v}: R[{v},{v}] = {matrizAdjacencia[v, v]} (deveria ser 1)");
                }
            }
        }

        public void verificarSimetricaDetalhado()
        {
            Console.WriteLine("\n=== Verificação Simétrica Detalhada ===");
            Console.WriteLine("Propriedade: R[i,j] = R[j,i] para todos os pares (i,j)");
            Console.WriteLine("Ou seja: se há aresta de i para j, deve haver de j para i\n");
            
            bool eSimetrica = true;
            List<string> paresAssimetricos = new List<string>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = i + 1; j < numeroDeVertices; j++) // Evita verificar duas vezes
                {
                    if (matrizAdjacencia[i, j] != matrizAdjacencia[j, i])
                    {
                        eSimetrica = false;
                        paresAssimetricos.Add($"R[{i},{j}]={matrizAdjacencia[i, j]} ≠ R[{j},{i}]={matrizAdjacencia[j, i]}");
                    }
                }
            }
            
            if (eSimetrica)
            {
                Console.WriteLine("✓ O grafo É SIMÉTRICO");
                Console.WriteLine("  Todas as arestas são bidirecionais");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É SIMÉTRICO");
                Console.WriteLine($"  Encontradas {paresAssimetricos.Count} assimetria(s):");
                foreach (string par in paresAssimetricos)
                {
                    Console.WriteLine($"    - {par}");
                }
            }
        }

        public void verificarTransitividadeDetalhado()
        {
            Console.WriteLine("\n=== Verificação Transitiva Detalhada ===");
            Console.WriteLine("Propriedade: Se R[i,k]=1 e R[k,j]=1, então R[i,j]=1");
            Console.WriteLine("Ou seja: se há caminho i→k→j, deve haver aresta direta i→j\n");
            
            bool eTransitiva = true;
            List<string> falhasTransitividade = new List<string>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    for (int k = 0; k < numeroDeVertices; k++)
                    {
                        if (matrizAdjacencia[i, k] == 1 && matrizAdjacencia[k, j] == 1 && matrizAdjacencia[i, j] == 0)
                        {
                            eTransitiva = false;
                            string falha = $"Caminho {i}→{k}→{j} existe, mas falta aresta direta {i}→{j}";
                            if (!falhasTransitividade.Contains(falha))
                            {
                                falhasTransitividade.Add(falha);
                            }
                        }
                    }
                }
            }
            
            if (eTransitiva)
            {
                Console.WriteLine("✓ O grafo É TRANSITIVO");
                Console.WriteLine("  Todos os caminhos indiretos possuem aresta direta correspondente");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É TRANSITIVO");
                Console.WriteLine($"  Encontradas {falhasTransitividade.Count} violação(ões):");
                int count = 0;
                foreach (string falha in falhasTransitividade)
                {
                    Console.WriteLine($"    - {falha}");
                    count++;
                    if (count >= 10) // Limita a 10 exemplos
                    {
                        Console.WriteLine($"    ... e mais {falhasTransitividade.Count - 10} violação(ões)");
                        break;
                    }
                }
            }
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

        public void desenharGrafo(int[,] matriz, string titulo)
        {
            Console.WriteLine($"\n{titulo}");
            Console.WriteLine(new string('=', titulo.Length));
            Console.WriteLine();
            
            // Lista todas as arestas
            List<string> arestas = new List<string>();
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    if (matriz[i, j] == 1)
                    {
                        if (i == j)
                        {
                            arestas.Add($"  {i} ⟲ (laço)");
                        }
                        else
                        {
                            arestas.Add($"  {i} → {j}");
                        }
                    }
                }
            }
            
            if (arestas.Count == 0)
            {
                Console.WriteLine("Grafo vazio (sem arestas)");
                return;
            }
            
            Console.WriteLine($"Vértices: {numeroDeVertices}");
            Console.WriteLine($"Arestas: {arestas.Count}");
            Console.WriteLine("\nLista de Arestas:");
            
            // Mostra em colunas para melhor visualização
            int coluna = 0;
            foreach (string aresta in arestas)
            {
                Console.Write(aresta.PadRight(15));
                coluna++;
                if (coluna % 4 == 0)
                {
                    Console.WriteLine();
                }
            }
            if (coluna % 4 != 0)
            {
                Console.WriteLine();
            }
            
            // Desenho visual simplificado
            Console.WriteLine("\nRepresentação Visual:");
            Console.WriteLine("┌─────────────────────────────────────────┐");
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                List<int> destinos = new List<int>();
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    if (matriz[i, j] == 1 && i != j)
                    {
                        destinos.Add(j);
                    }
                }
                
                if (destinos.Count > 0 || matriz[i, i] == 1)
                {
                    string linha = $"│ [{i}]";
                    if (matriz[i, i] == 1)
                    {
                        linha += " ⟲";
                    }
                    if (destinos.Count > 0)
                    {
                        linha += " → " + string.Join(", ", destinos);
                    }
                    Console.WriteLine(linha.PadRight(43) + "│");
                }
            }
            
            Console.WriteLine("└─────────────────────────────────────────┘");
        }

        public void desenharGrafoR2()
        {
            int[,] r2 = obterCaminho2();
            desenharGrafo(r2, "GRAFO R² - Caminhos de Comprimento 2");
        }

        public void desenharGrafoRInfinito()
        {
            int[,] rInf = obterRInfinito();
            desenharGrafo(rInf, "GRAFO R∞ - Fecho Transitivo (Todos os Caminhos)");
        }

        public void desenharGrafoOriginal()
        {
            desenharGrafo(matrizAdjacencia, "GRAFO ORIGINAL - Matriz de Adjacência");
        }


    }
}