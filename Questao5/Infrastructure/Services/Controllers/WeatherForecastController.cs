//using MediatR;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.Core;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Models;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using ValidationException = FluentValidation.ValidationException;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/controller")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost(Name = "MovimentarConta")]
        //[ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> MovimentarConta([Required] string idRequisicao, [Required] int idConta, [Required] double valor, [Required] char tipoMovimento)
        //{

        //    ContaCorrenteMovimentoModel ccMovimento = new ContaCorrenteMovimentoModel()
        //    {
        //        Id = idRequisicao,
        //        IdContaCorrente = idConta,
        //        Valor = valor,
        //        TipoMovimento = tipoMovimento.ToString()
        //    };

        //    var retorno = await InsertUpdateAssetGenerico<CreateContaCorrenteMovimentoCommand, ContaCorrenteMovimentoModel>(ccMovimento);

        //    return Ok(retorno);

        //    return Ok();

        //}

        [HttpPost("MovimentarConta")]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MovimentarConta([FromBody] CreateContaCorrenteMovimentoRequest createContaCorrenteMovimentoRequest)
        { 
            return await InsertUpdateAssetGenerico<ContaCorrenteMovimento,CreateContaCorrenteMovimentoRequest> (createContaCorrenteMovimentoRequest); 

        }

        [HttpGet("ConsultarSaldo")]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConsultarSaldo([Required] string idContaCorrente)
        {
            GetSaldoContaCorrenteQueryRequest getSaldoContaCorrenteQueryRequest = new()
            {
                IdContaCorrente = idContaCorrente
            };

            return await GetAsset<GetSaldoContaCorrenteQueryResponse, GetSaldoContaCorrenteQueryRequest>(getSaldoContaCorrenteQueryRequest);

        }

        private async Task<IActionResult> GetAsset<T, BaseEntity>(dynamic model)
        {
            try
            {
               
                var result = await Mediator.Send(model);

                if (result != null)
                {
                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Erro");
            }
            catch (ValidationException vEx)
            { 
                return BadRequest(GetError(vEx));
            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private async Task<IActionResult> InsertUpdateAssetGenerico<T, BaseEntity>(dynamic model)
        {
           // return Ok();
            try
            { 
                var result = await Mediator.Send(model);
                if (result != null)
                {
                    return Ok((result));
                }
                else
                {
                    var error = result.Descricao;
                    return StatusCode(StatusCodes.Status400BadRequest, error?.ErrorMessage);
                }
            }
            catch (ValidationException vEx)
            {
                return BadRequest(GetError(vEx));
                //return StatusCode(StatusCodes.Status400BadRequest, failure.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private Error GetError(ValidationException ex)
        {
            Error error = new Error();
            foreach (var item in ex.Errors)
            {
                error.Descricao = item.ErrorMessage;
                error.Tipo = item.PropertyName;
            }

            return error;
        }
    }
}