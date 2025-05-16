using CoreContato.DTOs;

namespace APITesteConhecimento.Interfaces
{
    public interface IContatoService
    {
        Task<List<ContatoDTO>> GetAllContatosAsync();

        Task AddContatoAsync(ContatoDTO contatoDto);

        Task<ContatoDTO?> UpdateContatoAsync(int id_contato, ContatoDTO contatoDto);

        Task<bool> DeleteContatoAsync(int id_contato);
    }
}
