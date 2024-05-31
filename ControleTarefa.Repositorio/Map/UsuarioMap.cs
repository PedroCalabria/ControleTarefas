using ControleTarefas.Entidade.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleTarefas.Repositorio.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tb_usuario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id_usuario")
                .IsRequired();

            builder.Property(e => e.Nome)
                .HasColumnName("nom_usuario")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Login)
                .HasColumnName("lgn_usuario")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("dsc_email")
                .IsRequired();

            builder.Property(e => e.Perfil)
                .HasColumnName("id_tpperfil")
                .IsRequired();

            builder.Property(e => e.DataAtualizacao)
                .HasColumnName("dat_atualizacao")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}