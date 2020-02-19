using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APi.Models;
using APi.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APi.Controllers
{
    public class FundoCapitalController : Controller
    {

        //Ideal é criar uma camada de Serviços.
        //Nesta APi não utilizarei esta camada.

        private readonly IFundoCapitalRepository _fundoCapitalRepository;

        public FundoCapitalController(IFundoCapitalRepository fundoCapitalRepository)
        {
            _fundoCapitalRepository = fundoCapitalRepository;
        }

        [HttpGet("v1/fundocapital")]
        public IActionResult ListarFundos()
        {
            try
            {
               var lista =  _fundoCapitalRepository.ListarFundos();

                return Json(lista);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [HttpPost("v1/fundocapital")]
        public IActionResult AdicionarFundo([FromBody]FundoCapital fundo)
        {
            try
            {
                _fundoCapitalRepository.Adicionar(fundo);

                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpPut("v1/fundocapital/{id}")]
        public IActionResult AlterarFundo(Guid id, [FromBody]FundoCapital fundo)
        {
           
               var fundoAnterior =  _fundoCapitalRepository.ObterPorId(id);

                if (fundoAnterior == null)
                {
                    return NotFound();
                }

                fundoAnterior.Nome = fundo.Nome;
                fundoAnterior.ValorAtual = fundo.ValorAtual;
                fundoAnterior.ValorNecessario = fundo.ValorNecessario;
                fundoAnterior.DataResgate = fundo.DataResgate;

                _fundoCapitalRepository.Alterar(fundoAnterior);

                return Ok();
        }

        [HttpGet("v1/fundocapital/{id}")]
        public IActionResult ObterFundo(Guid id)
        {
            var fundoAnterior = _fundoCapitalRepository.ObterPorId(id);

            if (fundoAnterior == null)
            {
                return NotFound();
            }

            return Ok(fundoAnterior);

        }

        [HttpDelete("v1/fundocapital/{id}")]
        public IActionResult RemoverFundo(Guid id)
        {
            var fundoAnterior = _fundoCapitalRepository.ObterPorId(id);

            if (fundoAnterior == null)
            {
                return NotFound();
            }

            _fundoCapitalRepository.RemoverFundo(fundoAnterior);

            return Ok();
        }
    }
}