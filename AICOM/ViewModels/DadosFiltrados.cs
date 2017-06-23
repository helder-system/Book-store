using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AICOM.ViewModels
{
    //classe para serializar o retorno Json do Listar na classe LivrosControler
    public class DadosFiltrados
    {
        public dynamic rows { get; set; }
        public int current { get; set; }
        public int rowCount { get; set; }
        public int total { get; set; }

        public DadosFiltrados(ParametrosPaginacao parametrosPaginacao)
        {
            rowCount = parametrosPaginacao.RowCount;
            current = parametrosPaginacao.Current;
        }
    }
}