using ControleTarefas.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace ControleTarefas.TestesUnitarios
{
    public class TesteUnitarioBase
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();
        protected Contexto _contexto;
        protected Mock<T> RegistrarMock<T>() where T : class
        {
            var mock = new Mock<T>();

            ServiceCollection.AddSingleton(typeof(T), mock.Object);

            return mock;
        }

        protected void Registrar<I, T>() where I : class where T : class, I
          => ServiceCollection.AddSingleton<I, T>();

        protected I ObterServico<I>() where I : class
          => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegistrarObjeto<Tp, T>(Tp type, T objeto) where Tp : Type where T : class
           => ServiceCollection.AddSingleton(type, objeto);

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            ConfigureInMemoryDataBase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            _contexto.Dispose();
        }

        private void ConfigureInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<Contexto>()
                              .UseInMemoryDatabase("InMemoryDatabase")
            .Options;

            _contexto = new Contexto(options);

            if (_contexto.Database.IsInMemory())
                _contexto.Database.EnsureDeleted();
        }
    }
}