using JogoPedraPapelTesoura.Enum;
using JogoPedraPapelTesoura.Model;
using Microsoft.AspNetCore.Mvc;

namespace JogoPedraPapelTesoura.Controllers
{
    [Route("api/jogo")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        [HttpPost]
        [Route("JogarPartida")]
        public IActionResult JogarPartida(OpcaoEnum opcaoJogador)
        {
            var resultado = RealizarPartida(opcaoJogador);

            if (resultado.resultadoPartida == "0")
            {
                return BadRequest("Ocorreu um erro ao realizar partida");
            }

            return Ok(resultado);
        }

        private ResultadoModel RealizarPartida(OpcaoEnum opcaoJogador)
        {
            var opcaoAdversario = GerarOpcaoAdversario();
            var resultado = VerificarGanhador(opcaoJogador, opcaoAdversario);
            return resultado;

        }

        private ResultadoModel VerificarGanhador(OpcaoEnum opcaoJogador, OpcaoEnum opcaoAdversario)
        {
            string resultadoPartida = "0";

            switch (opcaoJogador)
            {
                case OpcaoEnum.Pedra:
                    if (opcaoAdversario == OpcaoEnum.Pedra)
                    {
                        resultadoPartida = "Empate";
                    }
                    if (opcaoAdversario == OpcaoEnum.Papel)
                    {
                        resultadoPartida = "Perdeu";
                    }
                    if (opcaoAdversario == OpcaoEnum.Tesoura)
                    {
                        resultadoPartida = "Ganhou";
                    }
                    break;
                case OpcaoEnum.Papel:
                    if (opcaoAdversario == OpcaoEnum.Pedra)
                    {
                        resultadoPartida = "Ganhou";
                    }
                    if (opcaoAdversario == OpcaoEnum.Papel)
                    {
                        resultadoPartida = "Empate";
                    }
                    if (opcaoAdversario == OpcaoEnum.Tesoura)
                    {
                        resultadoPartida = "Perdeu";
                    }
                    break;
                case OpcaoEnum.Tesoura:
                    if (opcaoAdversario == OpcaoEnum.Pedra)
                    {
                        resultadoPartida = "Perdeu";
                    }
                    if (opcaoAdversario == OpcaoEnum.Papel)
                    {
                        resultadoPartida = "Ganhou";
                    }
                    if (opcaoAdversario == OpcaoEnum.Tesoura)
                    {
                        resultadoPartida = "Empatou";
                    }
                    break;
                default:
                    break;
            }

            ResultadoModel resultado = new ResultadoModel()
            {
                opcaoAdversario = opcaoAdversario.ToString(),
                resultadoPartida = resultadoPartida
            };

            return resultado;

        }

        private OpcaoEnum GerarOpcaoAdversario()
        {
            // Criar uma instância da classe Random
            Random random = new Random();
            // Gerar um número aleatório inteiro entre 0 e 2
            int numeroAleatorio = random.Next(0, 3);

            var resultado = DicionarioOpcaoEnum(numeroAleatorio);

            return resultado;
        }

        private OpcaoEnum DicionarioOpcaoEnum(int numero)
        {
            Dictionary<int, OpcaoEnum> dicionario = new Dictionary<int, OpcaoEnum>();
            dicionario.Add(0, OpcaoEnum.Pedra);
            dicionario.Add(1, OpcaoEnum.Papel);
            dicionario.Add(2, OpcaoEnum.Tesoura);

            return dicionario[numero];
        }
    }
}
