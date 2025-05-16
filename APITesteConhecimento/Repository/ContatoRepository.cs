using APITesteConhecimento.Interfaces;
using AutoMapper;
using CoreContato.DTOs;
using CoreContato.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace APITesteConhecimento.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AppDbContext _appcontext;
        private readonly IMapper _mapper;

        public ContatoRepository(AppDbContext appcontext, IMapper mapper)
        {
            _appcontext = appcontext;
            _mapper = mapper;
        }

        public async Task<List<ContatoDTO>> GetAllContatosAsync()
        {
            var contatos = await _appcontext.Contatos.ToListAsync();
            return _mapper.Map<List<ContatoDTO>>(contatos);
        }

        public async Task AddContatoAsync(ContatoDTO contatoDTO)
        {
            var contato = _mapper.Map<Contato>(contatoDTO);
            _appcontext.Contatos.Add(contato);
            await _appcontext.SaveChangesAsync();
        }

        public async Task UpdateContatoAsync(ContatoDTO contatoDTO)
        {
            var contatoExistente = await _appcontext.Contatos
                .FirstOrDefaultAsync(c => c.id_contato == contatoDTO.id_contato);

            if (contatoExistente == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            // Atualiza apenas os campos preenchidos
            if (!string.IsNullOrEmpty(contatoDTO.nome_contato))
                contatoExistente.nome_contato = contatoDTO.nome_contato;

            if (!string.IsNullOrEmpty(contatoDTO.telefone_contato))
                contatoExistente.telefone_contato = contatoDTO.telefone_contato;

            await _appcontext.SaveChangesAsync();
        }

        public async Task DeleteContatoAsync(int id_contato)
        {
            var contato = await _appcontext.Contatos.FindAsync(id_contato);
            if (contato != null)
            {
                _appcontext.Contatos.Remove(contato);
                await _appcontext.SaveChangesAsync();
            }
        }
    }
}
