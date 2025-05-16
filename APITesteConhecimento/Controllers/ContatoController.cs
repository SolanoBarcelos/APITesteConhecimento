using APITesteConhecimento.Interfaces;
using CoreContato.DTOs;
using CoreContato.Logs.APITesteConhecimento.Logging;
using CoreContato.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITesteConhehecimento.Controllers
{
    [ApiController]
    [Route("api/v1/contatos")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly Log<ContatoController> _log;

        public ContatoController(IContatoService contatoService, ILogger<ContatoController> logger)
        {
            _contatoService = contatoService;
            _log = new Log<ContatoController>(logger);
        }

        /// <summary>
        /// Rota UP.
        /// </summary>
        /// <returns>Rota UP.</returns>
        [HttpGet("up")]
        public IActionResult Up()
        {
            return Ok("API is running");
        }

        /// <summary>
        /// Rota para listar todos contatos - Get todos contatos.
        /// </summary>
        /// <returns>Lista todos contatos.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _log.Info("Buscando todos os Contatos.");
                var contatos = await _contatoService.GetAllContatosAsync();
                _log.Info("Retornando {0} contatos.", contatos.Count);
                return Ok(contatos);
            }
            catch (Exception ex)
            {
                _log.Error("Erro ao buscar contatos.", ex);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        /// <summary>
        /// Rota para criar um contato.
        /// </summary>
        /// <returns>Create Contato.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ContatoDTO contatoDto)
        {
            if (contatoDto == null)
            {
                _log.Info("Contato nulo.");
                return BadRequest("Contato nulo.");
            }

            try
            {
                await _contatoService.AddContatoAsync(contatoDto);
                _log.Info("Contato criado: {@Contato}", contatoDto);
                return Ok(new
                {
                    message = $"Contato criado: {@contatoDto}",
                    contato = contatoDto
                });
            }
            catch (Exception ex)
            {
                _log.Error("Erro ao criar o contato.", ex);
                return StatusCode(500, $"Erro ao criar o contato: {ex.Message}");
            }
        }

        /// <summary>
        /// Rota para atualizar um contato por id.
        /// </summary>
        /// <returns>Update Contato.</returns>
        [HttpPatch("update/{id_contato}")]
        public async Task<IActionResult> Update(int id_contato, [FromBody] ContatoDTO contatoDto)
        {
            try
            {
                _log.Info("Atualizando contato com ID: {0}.", id_contato);

                var contatoAtualizado = await _contatoService.UpdateContatoAsync(id_contato, contatoDto);

                if (contatoAtualizado == null)
                {
                    _log.Info("Contato com ID {0} não encontrado para atualização.", id_contato);
                    return NotFound(new { message = "Contato não encontrado." });
                }

                _log.Info("Contato atualizado: {@Contato}", contatoAtualizado);
                return Ok(new
                {
                    message = "Contato atualizado.",
                    contato = contatoAtualizado
                });
            }
            catch (Exception ex)
            {
                _log.Error("Erro ao atualizar o contato com ID {0}.", ex, id_contato);
                return StatusCode(500, "Erro ao atualizar o contato.");
            }
        }

        /// <summary>
        /// Rota para deletar um contato por id.
        /// </summary>
        /// <returns>Delete Contato.</returns>
        [HttpDelete("delete/{id_contato}")]
        public async Task<IActionResult> Delete(int id_contato)
        {
            try
            {
                _log.Info("Deletanto contato {0}.", id_contato);

                var sucesso = await _contatoService.DeleteContatoAsync(id_contato);

                if (!sucesso)
                {
                    _log.Info("Contato com ID {0} não encontrado.", id_contato);
                    return NotFound(new { message = "Contato não encontrado." });
                }

                _log.Info("Contato com ID {0} excluído com sucesso.", id_contato);
                return Ok(new { message = "Contato excluído com sucesso." });
            }
            catch (Exception ex)
            {
                _log.Error("Erro ao excluir o contato com ID {0}.", ex, id_contato);
                return StatusCode(500, "Erro ao excluir o contato.");
            }
        }
    }
}
