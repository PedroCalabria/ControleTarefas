using ControleTarefas.Entidade.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ControleTarefas.Repositorio
{
    public class Contexto : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<TarefaUsuario> TarefaUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
