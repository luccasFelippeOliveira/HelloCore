using System;
using System.Collections.Generic;
using HelloCore.Model;

namespace HelloCore.NeuralNetwork
{
    public class Tester
    {
        public double min = -3;
        public double max = 3;
        public int qtdAmostras = 300;
        public int qtdCamadas = 2;
        public int qtdNeuronios = 50;
        public double taxaAprendizagem = 0.0033;
        public bool usarErro = false;
        public double erroAceitavel = 0.02;
        public int quantidadeCiclos = 3000;

        // Use this for initialization
        public NetworkResult Start()
        {
            var result = new NetworkResult();
            
            double erroAtual = 0.0f;
            int cicloAtual = 0;
            int[] camadas = new int[this.qtdCamadas + 1];
            camadas[0] = 1;
            for (int i = 1; i < this.qtdCamadas; i++)
            {
                camadas[i] = this.qtdNeuronios;
            }
            camadas[camadas.Length - 1] = 1;
            NeuralNetwork network = new NeuralNetwork(camadas, this.taxaAprendizagem); //intiilize network

            //inicializando amostras
            double x = this.min;
            double[,] entradas = new double[this.qtdAmostras, 1];
            double[,] saidasEsperadas = new double[this.qtdAmostras, 1];
            double[,] saidasEncontradas = new double[this.qtdAmostras, 1];

            for (int i = 0; i < this.qtdAmostras; i++)
            {
                entradas[i, 0] = x;
                saidasEsperadas[i, 0] = this.funcao(x);
                x += 0.01f;
            }


            //treinando a rede
            do
            {
                for (int i = 0; i < this.qtdAmostras; i++)
                {
                    double[] entrada = new double[1];
                    entrada[0] = entradas[i, 0];
                    double[] saida = new double[1];
                    saida = network.FeedForward(entrada);
                    saidasEncontradas[i, 0] = saida[0];
                    double[] saidaEsperada = new double[1];
                    saidaEsperada[0] = saidasEsperadas[i, 0];
                    network.BackProp(saidaEsperada);
                }
                cicloAtual++;
                erroAtual = this.calculaErro(saidasEsperadas, saidasEncontradas);
                result.Erros.Add(erroAtual);
                Console.WriteLine(erroAtual);
            } while (this.usarErro ? (erroAtual >= this.erroAceitavel) : (cicloAtual < this.quantidadeCiclos));
            
            double[,] novasEntradas = new double[(int) ((this.max - this.min) / 0.01) + 1, 1];
            int index = 0;
            for (var i = this.min; i < this.max; i += 0.01)
            {
                try
                {
                    novasEntradas[index, 0] = i;
                    result.Resultados.Add(this.funcao(i));

                    double[] e = new double[1];
                    e[0] = novasEntradas[index, 0];

                    result.ValoresEsperados.Add(Math.Tanh(network.FeedForward(e)[0]));
                    index++;
                }
                catch
                {
                    Console.WriteLine(index);
                    Console.WriteLine(i);
                    Console.WriteLine((this.max - this.min) / 0.01);
                    throw;
                }
                
            }

            return result;
        }

        private double funcao(double x)
        {
            return Math.Cos(x) * Math.Cos(3 * x);
        }

        private double calculaErro(double[,] esperados, double[,] encontrados)
        {
            double somaQuadratica = 0.0f;

            for (int i = 0; i < esperados.GetLength(0); i++)
            {
                for (int j = 0; j < esperados.GetLength(1); j++)
                {
                    somaQuadratica += Math.Pow((esperados[i, j] - encontrados[i, j]), 2);
                }
            }

            return (double)(somaQuadratica / (esperados.GetLength(0) * esperados.GetLength(1)));
        }
    }
}