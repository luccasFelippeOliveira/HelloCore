using System.Collections.Generic;

namespace HelloCore.Model
{
    public class NetworkResult
    {
        public List<double> Erros { get; }
        public List<double> Resultados { get; }
        public List<double> ValoresEsperados { get; }

        public NetworkResult()
        {
            Erros = new List<double>();
            Resultados = new List<double>();
            ValoresEsperados = new List<double>();
        }
    }
}