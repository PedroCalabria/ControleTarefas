using ControleTarefas.Entidade.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Repositorio.Map
{
    public class TarefaUsuarioMap : IEntityTypeConfiguration<TarefaUsuario>
    {
        public void Configure(EntityTypeBuilder<TarefaUsuario> builder)
        {
            builder.ToTable("tb_tarefausuario");

            builder.HasKey(e => new { e.IdTarefa, e.IdUsuario});

            builder.Property(e => e.IdUsuario)
                .HasColumnName("id_usuario")
                .IsRequired();

            builder.Property(e => e.IdTarefa)
                .HasColumnName("id_tarefa")
                .IsRequired();

            builder.Property(e => e.Concluída)
                .HasColumnName("flg_concluida")
                .IsRequired();

            builder.HasOne(e => e.Usuario)
                .WithMany(e => e.TarefasUsuario)
                .HasForeignKey(e => e.IdUsuario);

            builder.HasOne(e => e.Tarefa)
                .WithMany(e => e.UsuariosTarefa)
                .HasForeignKey(e => e.IdTarefa);
        }
    }
}