﻿using ControleTarefas.Entidade.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Entidade.Entidades
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public DateTime DataAtualizacao { get; set; }


        public Usuario() { }
    }
}