using System;
using System.Threading.Tasks;
using HelloCore.Model;
using HelloCore.NeuralNetwork;
using Microsoft.AspNetCore.Mvc;

namespace HelloCore.Controllers
{
    [Route("api/[controller]")]
    public class NeuralNetworkController : ControllerBase
    {
        /// <summary>
        /// Performs a server healthcheck
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Response HealthCheck()
        {
            return new Response()
            {
                Status = "Ok",
                Message = "Service is running correctly"
            };
        }

        /// <summary>
        /// Trains the neural network
        /// </summary>
        /// <param name="parameters">Neural network parameters.</param>
        /// <returns></returns>
        [HttpPost("train")]
        public IActionResult TrainNetwork([FromBody] NeuralNetworkParametersDto parameters)
        {
            Console.WriteLine(parameters);
            var t = new Tester();
            
            t.min = parameters.Minimo;
            t.max = parameters.Maximo;
            t.qtdAmostras = parameters.QuantidadeAmostras;
            t.qtdCamadas = parameters.QuantidadeCamadas;
            t.qtdNeuronios = parameters.QuantidadeNeuronios;
            t.taxaAprendizagem = parameters.TaxaAprendizagem;
            t.usarErro = parameters.UsarErro;
            t.erroAceitavel = parameters.ErroAceitavel;
            t.quantidadeCiclos = parameters.QuantidadeCiclos;
            
            var r = t.Start();
            return new JsonResult(r);
        }
    }
}