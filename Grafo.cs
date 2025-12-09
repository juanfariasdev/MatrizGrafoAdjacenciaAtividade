using SkiaSharp;

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

        public void verificarAntissimetricaDetalhado()
        {
            Console.WriteLine("\n=== Verificação Antissimétrica Detalhada ===");
            Console.WriteLine("Propriedade: Se R[i,j]=1 e R[j,i]=1, então i=j");
            Console.WriteLine("Ou seja: não pode haver arestas bidirecionais entre vértices diferentes\n");
            
            bool eAntissimetrica = true;
            List<string> violacoes = new List<string>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = i + 1; j < numeroDeVertices; j++)
                {
                    if (matrizAdjacencia[i, j] == 1 && matrizAdjacencia[j, i] == 1)
                    {
                        eAntissimetrica = false;
                        violacoes.Add($"Arestas bidirecionais: {i}↔{j}");
                    }
                }
            }
            
            if (eAntissimetrica)
            {
                Console.WriteLine("✓ O grafo É ANTISSIMÉTRICO");
                Console.WriteLine("  Não há arestas bidirecionais entre vértices diferentes");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É ANTISSIMÉTRICO");
                Console.WriteLine($"  Encontradas {violacoes.Count} violação(ões):");
                foreach (string v in violacoes)
                {
                    Console.WriteLine($"    - {v}");
                }
            }
        }

        public void verificarIrreflexivaDetalhado()
        {
            Console.WriteLine("\n=== Verificação Irreflexiva Detalhada ===");
            Console.WriteLine("Propriedade: R[i,i] = 0 para todo vértice i");
            Console.WriteLine("Ou seja: nenhum vértice pode ter laço\n");
            
            bool eIrreflexiva = true;
            List<int> verticesComLaco = new List<int>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                if (matrizAdjacencia[i, i] == 1)
                {
                    eIrreflexiva = false;
                    verticesComLaco.Add(i);
                }
            }
            
            if (eIrreflexiva)
            {
                Console.WriteLine("✓ O grafo É IRREFLEXIVO");
                Console.WriteLine("  Nenhum vértice possui laço");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É IRREFLEXIVO");
                Console.WriteLine($"  {verticesComLaco.Count} vértice(s) com laço:");
                foreach (int v in verticesComLaco)
                {
                    Console.WriteLine($"    - Vértice {v}: R[{v},{v}] = 1");
                }
            }
        }

        public void verificarFortementeConexo()
        {
            Console.WriteLine("\n=== Verificação Fortemente Conexo ===");
            Console.WriteLine("Propriedade: Existe caminho entre qualquer par de vértices");
            Console.WriteLine("Verifica se R∞ tem todos elementos = 1\n");
            
            int[,] rInf = obterRInfinito();
            bool eFortementeConexo = true;
            int paresDesconectados = 0;
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = 0; j < numeroDeVertices; j++)
                {
                    if (rInf[i, j] == 0)
                    {
                        eFortementeConexo = false;
                        paresDesconectados++;
                    }
                }
            }
            
            if (eFortementeConexo)
            {
                Console.WriteLine("✓ O grafo É FORTEMENTE CONEXO");
                Console.WriteLine("  Existe caminho entre qualquer par de vértices");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É FORTEMENTE CONEXO");
                Console.WriteLine($"  {paresDesconectados} par(es) de vértices sem caminho");
            }
        }

        public void verificarAciclico()
        {
            Console.WriteLine("\n=== Verificação Acíclico ===");
            Console.WriteLine("Propriedade: Não possui ciclos");
            Console.WriteLine("Verifica se não há caminho de um vértice para ele mesmo\n");
            
            int[,] rInf = obterRInfinito();
            bool eAciclico = true;
            List<int> verticesEmCiclo = new List<int>();
            
            for (int i = 0; i < numeroDeVertices; i++)
            {
                if (rInf[i, i] == 1)
                {
                    eAciclico = false;
                    verticesEmCiclo.Add(i);
                }
            }
            
            if (eAciclico)
            {
                Console.WriteLine("✓ O grafo É ACÍCLICO (DAG)");
                Console.WriteLine("  Não há ciclos no grafo");
            }
            else
            {
                Console.WriteLine("✗ O grafo NÃO É ACÍCLICO");
                Console.WriteLine($"  {verticesEmCiclo.Count} vértice(s) em ciclo:");
                foreach (int v in verticesEmCiclo)
                {
                    Console.WriteLine($"    - Vértice {v} faz parte de um ciclo");
                }
            }
        }

        public void verificarRelacaoEquivalencia()
        {
            Console.WriteLine("\n=== Verificação Relação de Equivalência ===");
            Console.WriteLine("Propriedade: Reflexiva + Simétrica + Transitiva\n");
            
            bool reflexiva = eReflexiva();
            bool simetrica = eSimetrica();
            bool transitiva = verificarTransitividade();
            
            Console.WriteLine($"  Reflexiva: {(reflexiva ? "✓ SIM" : "✗ NÃO")}");
            Console.WriteLine($"  Simétrica: {(simetrica ? "✓ SIM" : "✗ NÃO")}");
            Console.WriteLine($"  Transitiva: {(transitiva ? "✓ SIM" : "✗ NÃO")}");
            
            if (reflexiva && simetrica && transitiva)
            {
                Console.WriteLine("\n✓ É UMA RELAÇÃO DE EQUIVALÊNCIA");
                Console.WriteLine("  Particiona os vértices em classes de equivalência");
            }
            else
            {
                Console.WriteLine("\n✗ NÃO É UMA RELAÇÃO DE EQUIVALÊNCIA");
            }
        }

        public void verificarOrdemParcial()
        {
            Console.WriteLine("\n=== Verificação Ordem Parcial ===");
            Console.WriteLine("Propriedade: Reflexiva + Antissimétrica + Transitiva\n");
            
            bool reflexiva = eReflexiva();
            bool transitiva = verificarTransitividade();
            
            // Verifica antissimétrica
            bool antissimetrica = true;
            for (int i = 0; i < numeroDeVertices; i++)
            {
                for (int j = i + 1; j < numeroDeVertices; j++)
                {
                    if (matrizAdjacencia[i, j] == 1 && matrizAdjacencia[j, i] == 1)
                    {
                        antissimetrica = false;
                        break;
                    }
                }
                if (!antissimetrica) break;
            }
            
            Console.WriteLine($"  Reflexiva: {(reflexiva ? "✓ SIM" : "✗ NÃO")}");
            Console.WriteLine($"  Antissimétrica: {(antissimetrica ? "✓ SIM" : "✗ NÃO")}");
            Console.WriteLine($"  Transitiva: {(transitiva ? "✓ SIM" : "✗ NÃO")}");
            
            if (reflexiva && antissimetrica && transitiva)
            {
                Console.WriteLine("\n✓ É UMA RELAÇÃO DE ORDEM PARCIAL");
                Console.WriteLine("  Define uma hierarquia entre os vértices");
            }
            else
            {
                Console.WriteLine("\n✗ NÃO É UMA RELAÇÃO DE ORDEM PARCIAL");
            }
        }

        public void verificarTodasPropriedades()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ANÁLISE COMPLETA DE PROPRIEDADES DO GRAFO");
            Console.WriteLine(new string('=', 60));
            
            verificarReflexivaDetalhado();
            verificarIrreflexivaDetalhado();
            verificarSimetricaDetalhado();
            verificarAntissimetricaDetalhado();
            verificarTransitividadeDetalhado();
            verificarFortementeConexo();
            verificarAciclico();
            verificarRelacaoEquivalencia();
            verificarOrdemParcial();
            
            Console.WriteLine("\n" + new string('=', 60));
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
                            arestas.Add($"  {i + 1} ⟲ (laço)");
                        }
                        else
                        {
                            arestas.Add($"  {i + 1} → {j + 1}");
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
                        destinos.Add(j + 1); // Adiciona j+1 para numeração 1-based
                    }
                }
                
                if (destinos.Count > 0 || matriz[i, i] == 1)
                {
                    string linha = $"│ [{i + 1}]";
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

        public void gerarImagemGrafo(int[,] matriz, string nomeArquivo, string titulo)
        {
            int largura = 800;
            int altura = 600;
            int raioVertice = 25;
            
            using (var surface = SKSurface.Create(new SKImageInfo(largura, altura)))
            {
                var canvas = surface.Canvas;
                canvas.Clear(SKColors.White);
                
                // Calcula posições dos vértices em círculo
                SKPoint[] posicoes = new SKPoint[numeroDeVertices];
                int centroX = largura / 2;
                int centroY = altura / 2;
                int raioCirculo = Math.Min(largura, altura) / 2 - 80;
                
                for (int i = 0; i < numeroDeVertices; i++)
                {
                    double angulo = 2 * Math.PI * i / numeroDeVertices - Math.PI / 2;
                    posicoes[i] = new SKPoint(
                        centroX + (float)(raioCirculo * Math.Cos(angulo)),
                        centroY + (float)(raioCirculo * Math.Sin(angulo))
                    );
                }
                
                // Desenha arestas
                using (var paintAresta = new SKPaint())
                {
                    paintAresta.Color = SKColors.Black;
                    paintAresta.StrokeWidth = 2;
                    paintAresta.Style = SKPaintStyle.Stroke;
                    paintAresta.IsAntialias = true;
                    
                    for (int i = 0; i < numeroDeVertices; i++)
                    {
                        for (int j = 0; j < numeroDeVertices; j++)
                        {
                            if (matriz[i, j] == 1)
                            {
                                if (i == j)
                                {
                                    // Desenha laço
                                    float loopSize = 20;
                                    SKRect loopRect = new SKRect(
                                        posicoes[i].X - loopSize / 2,
                                        posicoes[i].Y - raioVertice - loopSize,
                                        posicoes[i].X + loopSize / 2,
                                        posicoes[i].Y - raioVertice
                                    );
                                    canvas.DrawOval(loopRect, paintAresta);
                                }
                                else
                                {
                                    // Calcula ponto inicial e final ajustados
                                    float dx = posicoes[j].X - posicoes[i].X;
                                    float dy = posicoes[j].Y - posicoes[i].Y;
                                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                                    
                                    float x1 = posicoes[i].X + dx / dist * raioVertice;
                                    float y1 = posicoes[i].Y + dy / dist * raioVertice;
                                    float x2 = posicoes[j].X - dx / dist * raioVertice;
                                    float y2 = posicoes[j].Y - dy / dist * raioVertice;
                                    
                                    canvas.DrawLine(x1, y1, x2, y2, paintAresta);
                                    
                                    // Desenha seta
                                    float arrowSize = 10;
                                    float angle = (float)Math.Atan2(dy, dx);
                                    
                                    using (var path = new SKPath())
                                    {
                                        path.MoveTo(x2, y2);
                                        path.LineTo(
                                            x2 - arrowSize * (float)Math.Cos(angle - Math.PI / 6),
                                            y2 - arrowSize * (float)Math.Sin(angle - Math.PI / 6)
                                        );
                                        path.MoveTo(x2, y2);
                                        path.LineTo(
                                            x2 - arrowSize * (float)Math.Cos(angle + Math.PI / 6),
                                            y2 - arrowSize * (float)Math.Sin(angle + Math.PI / 6)
                                        );
                                        canvas.DrawPath(path, paintAresta);
                                    }
                                }
                            }
                        }
                    }
                }
                
                // Desenha vértices
                using (var paintVertice = new SKPaint())
                using (var paintBorda = new SKPaint())
                using (var paintTexto = new SKPaint())
                {
                    paintVertice.Color = SKColors.LightBlue;
                    paintVertice.Style = SKPaintStyle.Fill;
                    paintVertice.IsAntialias = true;
                    
                    paintBorda.Color = SKColors.Black;
                    paintBorda.StrokeWidth = 2;
                    paintBorda.Style = SKPaintStyle.Stroke;
                    paintBorda.IsAntialias = true;
                    
                    paintTexto.Color = SKColors.Black;
                    paintTexto.TextSize = 16;
                    paintTexto.IsAntialias = true;
                    paintTexto.TextAlign = SKTextAlign.Center;
                    paintTexto.Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold);
                    
                    for (int i = 0; i < numeroDeVertices; i++)
                    {
                        canvas.DrawCircle(posicoes[i].X, posicoes[i].Y, raioVertice, paintVertice);
                        canvas.DrawCircle(posicoes[i].X, posicoes[i].Y, raioVertice, paintBorda);
                        
                        string texto = (i + 1).ToString();
                        SKRect bounds = new SKRect();
                        paintTexto.MeasureText(texto, ref bounds);
                        canvas.DrawText(texto, posicoes[i].X, posicoes[i].Y - bounds.MidY, paintTexto);
                    }
                }
                
                // Desenha título
                using (var paintTitulo = new SKPaint())
                {
                    paintTitulo.Color = SKColors.Black;
                    paintTitulo.TextSize = 20;
                    paintTitulo.IsAntialias = true;
                    paintTitulo.TextAlign = SKTextAlign.Center;
                    paintTitulo.Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold);
                    
                    canvas.DrawText(titulo, largura / 2, 30, paintTitulo);
                }
                
                // Salva imagem
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite(nomeArquivo))
                {
                    data.SaveTo(stream);
                }
                
                Console.WriteLine($"\n✓ Imagem salva: {nomeArquivo}");
            }
        }

        public void gerarImagemGrafoOriginal(string nomeArquivo)
        {
            gerarImagemGrafo(matrizAdjacencia, nomeArquivo, "Grafo Original");
        }

        public void gerarImagemGrafoR2(string nomeArquivo)
        {
            int[,] r2 = obterCaminho2();
            gerarImagemGrafo(r2, nomeArquivo, "Grafo R² - Caminhos de Comprimento 2");
        }

        public void gerarImagemGrafoRInfinito(string nomeArquivo)
        {
            int[,] rInf = obterRInfinito();
            gerarImagemGrafo(rInf, nomeArquivo, "Grafo R∞ - Fecho Transitivo");
        }


    }
}