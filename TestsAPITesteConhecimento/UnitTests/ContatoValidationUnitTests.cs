using APITesteConhecimento.Repository;
using APITesteConhecimento.Services;
using AutoMapper;
using CoreContato.Mappings;
using CoreContato.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsAPITesteConhecimento.UnitTests
{
    public class ContatoValidationUnitTests
    {
        private readonly ContatoService _service;

        public ContatoValidationUnitTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ValidacaoTest")
                .Options;

            var context = new AppDbContext(options);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ContatoProfile>();
            });
            var mapper = config.CreateMapper();

            _service = new ContatoService(context, mapper);
        }

        [Fact]
        public void ValidateContato_ContatoValido_NaoLancaExcecao()
        {
            var contato = new Contato("Solano", "31991485674");

            _service.ValidateContato(contato); 
        }

        [Fact]
        public void ValidateContato_TelefoneInvalido_LancaExcecao()
        {
            var contato = new Contato("Solano2", "123");

            Assert.Throws<ArgumentException>(() => _service.ValidateContato(contato));
        }

        [Fact]
        public void ValidateContato_NomeNulo_LancaExcecao()
        {
            var contato = new Contato(null, "31999999999");

            Assert.Throws<ArgumentException>(() => _service.ValidateContato(contato));
        }

        [Fact]
        public void ValidateContato_UpdateParcial_ComCamposNulos_NaoLancaExcecao()
        {
            var contato = new Contato(null, null);

            _service.ValidateContato(contato, isPartialUpdate: true);
        }
    }
}
