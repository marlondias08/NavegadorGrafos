using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico01
{
    class Grafo
    {

        private int[,] matriz;
        private int numVertices;
        private int numArestas = 0;
        private int custoMax = 50;

        public Grafo(int numVertices)
        {
            this.numVertices = numVertices + 1;
            matriz = new int[this.numVertices, this.numVertices];

            for (int i = 0; i < matriz.GetLength(0); i++) //Prenche as posições [0,x] e [x,0] com os labels dos vértices,
            {                                             // fazendo com que os vértices comecem na posição 1
                for (int j = 0; j < matriz.GetLength(0); j++)
                {
                    if (i == 0 && i != j)
                        matriz[i, j] = j ;
                    if (j == 0 && j != i)
                        matriz[i, j] = i ;
                }
            }

        }

        private bool Vazio()
        {
            return numArestas == 0; //Se nao tiver arestas, está vazio.
        }

        private bool VerticeValido(int v)
        {
            if (v + 1 > matriz.GetLength(0)) //Se for maior que a matriz, não é válido
            {
                Console.WriteLine("\nERRO! Vértice inválido.\n");
                return false;
            }
            else if (v < 1) //Se for menor o primeiro vértice, não é válido
            {
                Console.WriteLine("\nERRO! Vértice inválido.\n");
                return false;
            }

            return true;
        }

        public bool InsereAresta(int v1, int v2, int peso)
        {
            if (v1 <= 0 || v2 <= 0) //As posições [0,x] e [x,0] são usadas para os labels dos vértices
            {
                Console.WriteLine("\nERRO! Os vértices devem ser maiores que 0.\n");
                return false;
            }
            else
            {
                matriz[v1, v2] = peso; //Insere peso na aresta informada
                matriz[v2, v1] = peso;
                numArestas++; //Atuliza número de aresta                
                Console.WriteLine("\nSUCESSO! Aresta inserida!\n");
                return true;
            }
        }

        private bool ExisteAresta(int v1, int v2)
        {
            if (!VerticeValido(v1) && !VerticeValido(v2))
                return false; 
            else
                return matriz[v1, v2] > 0; //Se não for zero na aresta, ela existe.
        }

        public bool RetiraAresta(int v1, int v2)
        {
            if (!VerticeValido(v1) && !VerticeValido(v2))
                return false;
            else if (matriz[v1, v2] == 0) //Não remove nada se o peso já for 0.
            {
                Console.WriteLine("\nAVISO: Não existe aresta para os vértices {0} e {1} !\n", v1, v2);
                return false;
            }
            else //Se não for 0
            {
                matriz[v1, v2] = 0;  //Remove os pesos nas duas posições da aresta
                matriz[v2, v1] = 0;
                numArestas--;  //Atualiza número de arestas
                Console.WriteLine("\nSUCESSO: Aresta dos vértices {0} e {1} excluída!\n", v1, v2);
                return true;
            }
        }

        public void InsereVertice()
        {   
            numVertices++;
            int[,] aux = new int[numVertices, numVertices]; //Cria matriz auxiliar com uma posição a mais

            if (Vazio()) //Se o matriz estiver vazio
                matriz = aux; //Faz a matriz principal ser igual a matriz auxiliar.
            else         //Se não estiver vazio
            {
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
                        aux[i, j] = matriz[i, j]; //Copia os valores da principal para a auxiliar
                    }
                }
                aux[0, numVertices - 1] = numVertices - 1; //E adicona o novo label do vértice nas extremidades.
                aux[numVertices - 1, 0] = numVertices - 1;
            }
            matriz = aux; //Copia a matriz auxiliar com o novo vértice para a principal.
            Console.WriteLine("\nSUCESSO: Vértice {0} inserido!\n", numVertices - 1); 
        }

        public bool RetiraVertice(int v)
        {
            if (!VerticeValido(v)) //Verifica se o vértice passado é válido
                return false;
            else
            {
                numVertices--;
                int[,] aux = new int[numVertices, numVertices]; //Cria matriz auxiliar com uma posição a menos
                int cont = 0;
                int contAux = 0;

                for (int i = 0; i < aux.GetLength(0); i++)
                {
                    if (i >= v)   //Se i tiver o valor do vértice informado, ignora a linha.
                        contAux = i + 1;
                    else
                        contAux = i;

                    for (int j = 0; j < aux.GetLength(0); j++)
                    {
                        if (j < v)
                        {
                            if (j == 0 || i == 0) //Se for um label, só copia o valor
                                aux[i, j] = matriz[i, j];
                            else
                                aux[i, j] = matriz[contAux, j]; //Enquanto não encontar o vértice informado, copia toda a matriz principal a para matriz auxiliar

                            if (matriz[contAux, j] > 0 && j > 0 && i > 0) //Conta número de arestas, posições 0 são para os labels
                                cont++;
                        }
                        else
                        {
                            if (j == 0 || i == 0) //Se for um label, só copia o valor
                                aux[i, j] = matriz[i, j];
                            else
                                aux[i, j] = matriz[contAux, j + 1];  //Se encontrar, ignora o vértice e continua copiando.   
                             
                            if (matriz[contAux, j + 1] > 0 && j > 0 && i > 0) //Conta número de arestas, posições 0 são para os labels
                                cont++;
                        }
                    }
                }

                numArestas = cont /2;
                matriz = aux;
                Console.WriteLine("\nSUCESSO: Vértice {0} Removido!\n", v);
                return true;
            }    
        }

        public void ListaDeAdjacente(int v)
        {
            if (!VerticeValido(v)) { } //Verifica se o vértice é válido
            else
            {
                string lista = "\nLista de adjacentes do vértice " + v + ": ";
                bool vazia = true;
                for (int j = 1; j < matriz.GetLength(0); j++)
                {
                    if (ExisteAresta(v, j)) //Adiciona o vértice adjacente para a váriável da lista se existir aresta.
                    {
                        lista += j + ", ";
                        vazia = false;
                    }
                }
                if(vazia)
                    Console.WriteLine("\nO vértice {0} não tem adjacentes.\n", v);
                else
                    Console.WriteLine(lista +"\n");
            }
        }

        public void PercorreGrafo(int v1, int v2)
        {
            int peso = 0;
            if (!VerticeValido(v1) || !VerticeValido(v2)) { } //verifica se os dois vértices do caminho são válidos
            else
            {    
                if (v2 > v1) //Se o segundo vértice do caminho for maior
                {
                    for (int j = 1; j <= v2; j++) 
                    {
                        peso += matriz[v1, j];   //Deixa a linha fixa no primeiro vértice e soma os pesos das colunas até chegar no segundo vértice.
                    }
                }
                else if (v1 > v2) //Se o primeiro vértice for maior
                {
                    for (int j = 1; j <= v1; j++)
                    {
                        peso += matriz[v2, j]; //Deixa a linha fixa no segundo vértice e soma os pesos das colunas até chegar no primeiro vértice.
                    }
                }

            }
            Console.WriteLine("\nO peso para percorrer o caminho é : " + peso + ".\n");
        }      

        public void ImprimeGrafo()
        {
            Console.WriteLine("\nmatriz TAD:\n");
            Console.WriteLine("_________________________________________________");
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(0); j++)
                {
                    if (i == 0 && j == 0) //Se for a primeira posição, deixa vazio.
                        Console.Write("\t ");
                    else
                        Console.Write(matriz[i, j] + "\t"); //Se não preenche com 0 onde não existe arestas ou com o peso, se existir aresta.
                }
                if (i < matriz.GetLength(0) - 1)
                    Console.WriteLine("\n");
                else
                    Console.WriteLine();
            }
            Console.WriteLine("_________________________________________________");
            Console.WriteLine();
        }

        ////////////////////////////////////////////////////////////
        //      Os métodos relacionados ao algoritmo guloso       //
        //      e com o Tentativa e Erro foram retirados e        //
        //      adaptados do código encontrado no link abaixo     //    
        //      https://github.com/BarbaraGCOL/CaixeiroViajante   //  
        ////////////////////////////////////////////////////////////
                 
        public void Guloso()
        {
            Console.WriteLine("\n Solução: Algoritmo Guloso");

            int menor;
            int cidade1 = 1, cidade2 = 1;
            List<Caminho> caminho = new List<Caminho>(numVertices);
            Caminho Caminho = new Caminho();

            for (int i = 1; i < caminho.Capacity; i++)
            {
                menor = custoMax;
                Caminho.cidade1 = cidade1;

                if (i == caminho.Capacity - 1)
                {
                    menor = matriz[cidade1, 1];
                    cidade2 = 1;
                }
                else
                {
                    for (int j = 1; j < numVertices; j++)
                    {
                        if (j != cidade1 && caminho.Find(x => x.cidade1 == j || x.cidade2 == j) == null)
                        {
                            if (matriz[cidade1, j] != 0 && matriz[cidade1, j] < menor)
                            {
                                menor = matriz[cidade1, j];
                                cidade2 = j;
                            }
                        }
                    }
                }

                Caminho.cidade2 = cidade2;
                Caminho.distancia = menor;
                caminho.Add(Caminho);
                Caminho = new Caminho();
                cidade1 = cidade2;
            }

            ImprimeCaminho(caminho);
        }        

        public void TentativaErro()
        {
            Console.WriteLine("\nSolução: Algoritmo de tentativa e Erro");

            int controle = -1, melhorCusto = int.MaxValue;
            Caminho[] melhorCaminho = new Caminho[numVertices - 1];
            int[] permutacao = new int[numVertices];

            for (int i = 0; i < melhorCaminho.Length ; i++)
                melhorCaminho[i] = new Caminho();

            permuta(permutacao, melhorCaminho, ref melhorCusto, controle, 1);

            ImprimeCaminho(melhorCaminho.ToList());

        }

        private void permuta(int[] permutacao, Caminho[] caminho, ref int melhorCusto, int controle, int k)
        {
            int i;
            permutacao[k] = ++controle;
            if (controle == (caminho.Length  ))
                melhorCaminho(caminho, ref melhorCusto, permutacao);
            else
                for (i = 1; i <= caminho.Length; i++)
                    if (permutacao[i] == 0)
                        permuta(permutacao, caminho, ref melhorCusto, controle, i);
            controle--;
            permutacao[k] = 0;
        }

        private void melhorCaminho(Caminho[] melhorCaminho, ref int melhorCusto, int[] permutacao)
        {
            int j, k;
            int cid1, cid2;
            int custo;
            int[] proxDoCaminho;


            proxDoCaminho = new int[melhorCaminho.Length +1];

            cid1 = 1;
            cid2 = permutacao[2];
            custo = matriz[cid1, cid2];
            proxDoCaminho[cid1] = cid2;

            for (j = 3; j <= melhorCaminho.Length; j++)
            {
                cid1 = cid2;
                cid2 = permutacao[j];
                custo += matriz[cid1, cid2];
                proxDoCaminho[cid1] = cid2;
            }

            proxDoCaminho[cid2] = 1;
            custo += matriz[cid2, 1];

            if (custo < melhorCusto)
            {
                melhorCusto = custo;
                cid2 = 1;
                for (k = 0; k < melhorCaminho.Length ; k++)
                {
                    cid1 = cid2;
                    cid2 = proxDoCaminho[cid1];
                    melhorCaminho[k].cidade1 = cid1;
                    melhorCaminho[k].cidade2 = cid2;
                    melhorCaminho[k].distancia = matriz[cid1, cid2];
                }
            }
        }

        private int PercorreGrafo(List<Caminho> caminho)
        {
            return caminho.Sum(x => x.distancia);
        }

        private void ImprimeCaminho(List<Caminho> caminho)
        {
            if (caminho.Count > 0)
            {
                Console.Write("\nO menor caminho percorrido foi: \n");
                for (int i = 0; i < caminho.Count; i++)
                {
                    Console.Write($"{caminho[i]}\n");
                }

                Console.WriteLine($"Com a distância de: {PercorreGrafo(caminho)}\n");
            }
        }
    }
}
