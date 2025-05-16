using CoreContato.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITesteConhecimento.Interfaces
{
    public interface IContatoRepository
    {
        Task<List<ContatoDTO>> GetAllContatosAsync();
        Task AddContatoAsync(ContatoDTO contatoDTO);
        Task UpdateContatoAsync(ContatoDTO contatoDTO);
        Task DeleteContatoAsync(int id_contato);
    }
}
