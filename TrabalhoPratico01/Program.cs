using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrabalhoPratico01
{
    class Program
    {
        static Grafo grafo = new Grafo(0);
        static void Main(string[] args)
        {            
            bool controle = false;
            int vertice, vertice2, peso, vertices = 0;
            
            

            Console.WriteLine("                      Trabalho prático de Algoritmos em Grafos");
            Console.WriteLine("                    Desenvolvido por Marlon Dias e Matheus Cunha\n\n\n");
            while (!controle)
            {
                string entrada;
                Console.WriteLine("Informe o número inicial de vértices do grafo ou tecle ENTER para carregar dados do arquivo de entrada.");
                entrada = Console.ReadLine();
                if (entrada  == "")
                {
                    CarregaGrafo();
                    controle = true;
                }
                else if (int.TryParse(entrada, out vertices) && vertices > 0)
                {
                    grafo = new Grafo(vertices);
                    controle = true;
                }
                else
                    Console.WriteLine("ERRO! Número de vértices inválido!\n");
            }

        Menu:
            Console.WriteLine("Escolha uma opção:\n1 - Inserir Aresta\n2 - Retirar aresta\n3 - Inserir vértice\n4 - Retirar vértice\n5 - Imprimir grafo\n6 - Mostrar lista de adjacência de um vértice\n7 - Peso de um caminho no grafo\n----Problema do Caixeiro Viajante----\n8 - Guloso\n9 - Tentativa e Erro\n-------------------------------------\n0 - Sair ");

            char opcao = Console.ReadKey(true).KeyChar;
            while (Console.KeyAvailable) opcao = Console.ReadKey(true).KeyChar;
            {
                controle = false;
                switch (opcao)
                {
                    case '1':
                        while (!controle)
                        {
                            Console.WriteLine("\nInforme os vértices e o peso da aresta\n");
                            Console.WriteLine("Primeiro vértice, segundo vértice e o peso:\n");
                            if (int.TryParse(Console.ReadLine(), out vertice) && int.TryParse(Console.ReadLine(), out vertice2) && int.TryParse(Console.ReadLine(), out peso))
                            {
                                if (vertice > vertices || vertice2 > vertices)
                                    Console.WriteLine("ERRO! Os vértices devem estar entre 1 e {0}.", vertices);
                                else
                                {
                                    grafo.InsereAresta(vertice, vertice2, peso);
                                    controle = true;
                                }
                            }                            
                            else
                                Console.WriteLine("ERRO! Os valores devem ser números inteiros.\n");

                        }
                        goto Menu;
                    case '2':
                        while (!controle)
                        {
                            Console.WriteLine("\nInforme os vértices da aresta\n");
                            Console.WriteLine("Primeiro vértice, segundo vértice:\n");
                            if (int.TryParse(Console.ReadLine(), out vertice) && int.TryParse(Console.ReadLine(), out vertice2))
                            {
                                if (vertice > vertices || vertice2 > vertices)
                                    Console.WriteLine("ERRO! Os vértices devem estar entre 1 e {0}.", vertices);
                                else
                                {
                                    if (grafo.RetiraAresta(vertice, vertice2))
                                        controle = true;
                                }
                            }
                            else
                                Console.WriteLine("ERRO! Os valores devem ser números inteiros.\n");
                        }
                        goto Menu;
                    case '3':                        
                            grafo.InsereVertice(); 
                        goto Menu;
                    case '4':
                        while (!controle)
                        {
                            Console.WriteLine("\nInforme o vértice:\n");
                            if (int.TryParse(Console.ReadLine(), out vertice))
                            {
                                if (grafo.RetiraVertice(vertice))
                                    controle = true;
                            }
                            else
                                Console.WriteLine("ERRO! Os valores devem ser números inteiros.\n");
                        }
                        goto Menu;
                    case '5':
                        grafo.ImprimeGrafo();
                        goto Menu;
                    case '6':
                        while (!controle)
                        {
                            Console.WriteLine("\nInforme o vértice:\n");
                            if (int.TryParse(Console.ReadLine(), out vertice))
                            {
                                grafo.ListaDeAdjacente(vertice);
                                controle = true;
                            }
                            else
                                Console.WriteLine("ERRO! Os valores devem ser números inteiros.\n");
                        }
                        goto Menu;
                    case '7':
                        while (!controle)
                        {
                            Console.WriteLine("\nInforme os vértice inicial e final do caminho desejado:\n");
                            if (int.TryParse(Console.ReadLine(), out vertice) && int.TryParse(Console.ReadLine(), out vertice2))
                            {
                                grafo.PercorreGrafo(vertice, vertice2);
                                controle = true;
                            }
                            else
                                Console.WriteLine("ERRO! Os valores devem ser números inteiros.\n");
                        }
                        goto Menu;

                    case '8':
                        grafo.Guloso();
                        goto Menu;

                    case '9':
                        grafo.TentativaErro();
                        goto Menu;

                    case '0':
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nPor favor, escolha uma opção do menu.\n");
                        goto Menu;
                }
            }
        }

        private static void CarregaGrafo()
        {
            StreamReader leitor = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "cidades.txt"));
            string[] distancias;
            int cont = 1;

            distancias = leitor.ReadLine().Split('\t', ' ');

            grafo = new Grafo(distancias.Length);

            for (int i = 0; i < distancias.Length; i++)
            {
                for (int j = 0; j < distancias.Length; j++)
                {
                    grafo.InsereAresta(i +1, j+1, int.Parse(distancias[j]));
                }

                if (cont < distancias.Length)
                {
                    distancias = leitor.ReadLine().Split('\t', ' ');
                    cont++;
                }
            }

            leitor.Close();
        }
    }
}
