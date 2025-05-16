using APITesteConhecimento.Repository;
using APITesteConhecimento.Services;
using AutoMapper;
using CoreContato.DTOs;
using CoreContato.Mappings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestsAPITesteConhecimento.IntegrationTests
{
    public class ContatoIntegrationTests
    {
        private readonly AppDbContext _context;
        private readonly ContatoService _service;

        public ContatoIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=db_bteste_conhecimento;User Id=admin;Password=1234;")
                .Options;

            _context = new AppDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ContatoProfile>();
            });

            _service = new ContatoService(_context, mapperConfig.CreateMapper());
        }

        [Fact]
        public async Task GetAllContacts()
        {
            var contatos = await _service.GetAllContatosAsync();

            Assert.NotNull(contatos);
            Assert.NotEmpty(contatos);
        }
    }
}
