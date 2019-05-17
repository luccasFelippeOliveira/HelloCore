namespace HelloCore.Model
{
    public class NeuralNetworkParametersDto
    {
        public double Minimo  { get; set; }
        public double Maximo { get; set; }
        public int QuantidadeAmostras { get; set; }
        public int QuantidadeCamadas { get; set; }
        public int QuantidadeNeuronios { get; set; }
        public double TaxaAprendizagem { get; set; }
        public bool UsarErro { get; set; }
        public double ErroAceitavel { get; set; }
        public int QuantidadeCiclos { get; set; }
    }
}