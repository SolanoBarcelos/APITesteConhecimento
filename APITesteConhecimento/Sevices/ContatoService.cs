using APITesteConhecimento.Interfaces;
using APITesteConhecimento.Repository;
using AutoMapper;
using CoreContato.DTOs;
using CoreContato.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace APITesteConhecimento.Services
{
    public class ContatoService : IContatoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContatoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContatoDTO>> GetAllContatosAsync()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return _mapper.Map<List<ContatoDTO>>(contatos);
        }

        public async Task AddContatoAsync(ContatoDTO contatoDto)
        {
            var contato = _mapper.Map<Contato>(contatoDto);

            ValidateContato(contato, isPartialUpdate: false);

            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<ContatoDTO?> UpdateContatoAsync(int id_contato, ContatoDTO contatoDto)
        {
            var contato = await _context.Contatos.FindAsync(id_contato);

            if (contato == null)
                return null;

            // Atualizações parciais
            if (!string.IsNullOrWhiteSpace(contatoDto.nome_contato))
                contato.nome_contato = contatoDto.nome_contato;

            if (!string.IsNullOrWhiteSpace(contatoDto.telefone_contato))
                contato.telefone_contato = contatoDto.telefone_contato;


            ValidateContato(contato, isPartialUpdate: true);

            _context.Contatos.Update(contato);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContatoDTO>(contato);
        }

        public async Task<bool> DeleteContatoAsync(int id_contato)
        {
            var contato = await _context.Contatos.FindAsync(id_contato);

            if (contato == null)
                return false;

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return true;
        }


        public void ValidateContato(Contato contato, bool isPartialUpdate = false)
        {
            if (!isPartialUpdate || !string.IsNullOrWhiteSpace(contato.nome_contato))
            {
                if (string.IsNullOrWhiteSpace(contato.nome_contato))
                    throw new ArgumentException("Nome do contato é obrigatório.");
            }

            if (!isPartialUpdate || !string.IsNullOrWhiteSpace(contato.telefone_contato))
            {
                if (!IsValidTelefone(contato.telefone_contato))
                    throw new ArgumentException("Telefone deve conter 11 dígitos numéricos.");
            }
        }

        private bool IsValidTelefone(string telefone)
        {
            var regex = new Regex(@"^\d{11}$");
            return regex.IsMatch(telefone);
        }
    }
}
