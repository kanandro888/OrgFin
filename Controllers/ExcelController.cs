using SAPIExcel.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SAPIExcel.Controllers
{
    [Route("api/excel")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly Metodos _metodos;

        public ExcelController()
        {
            _metodos = new Metodos();
        }

        [HttpPost("salvar")]
        public IActionResult Salvar([FromBody] ExcelRequest request)
        {
            try
            {
                decimal valorDecimal = Convert.ToDecimal(request.Valor);
                // Passa os parâmetros corretos para o método SalvarNoExcel
                _metodos.SalvarNoExcel(request.Data, request.Mes, request.Ano, valorDecimal, request.Descricao, request.Categoria);
                return Ok(new { mensagem = "Dados salvos no Excel com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }

    public class ExcelRequest
    {
        public DateTime Data { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public int Coluna { get; set; }
    }
}
