using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;


namespace AICOM.ViewModels
{
    public class ParametrosPaginacao
    {
        public int Current { get; set; }
        public int RowCount { get; set; }
        public string SearchPhrase { get; set; }
        public string CampoOrdenado { get; set; }

        //current - pagina atual apresentada na tabela(grid) ex: 1
        //rowCount - Quantidade de linhas ou livros apresentados na tabela(grid) ex: 10
        //searchPhrase - Formulario input de Pesquisa dos itens.
        //chave - Parametro de pesquisa para ordenação por título ou por outra parametro escolhido pelo usuário ex: "sort[titulo]"
        //campo - Parametro de pesquisa para ordenação por titulo sem o sort ex: titulo"
        // ordenação - Parametro que informa como a ordenação será apreentada na tabela de forma crescente ou decrescente ex: "asc"
        // Livros - Lista de livros
        //total - quantidade de livros cadastrados.
        //campoOrdenacao - classifica a forma de ordenação ex: "Titulo asc"
        //LivrosPaginados - Ordena e Lista os livros que serão paginados na tabela atual, skip pular os primeiros registros e Take - mostrar os proximos. 
        // ex: livrosPaginados = livros.OrderBy(campoOrdenacao).Skip((current - 1) * rowCount).Take(rowCount);

        //request os parametros de paginação da tabela

        public ParametrosPaginacao(NameValueCollection dados)
        {
            //sort[Titulo]||sort[Autor]||sort[AnoEdicao]||sort[Titulo]
            string chave = dados.AllKeys.Where(k => k.StartsWith("sort")).First();
            string ordenacao = dados[chave];
            string campo = chave.Replace("sort[", String.Empty).Replace("]", String.Empty);
            CampoOrdenado = String.Format("{0} {1}", campo, ordenacao);
            Current = int.Parse(dados["current"]);
            RowCount = int.Parse(dados["rowCount"]);
            SearchPhrase = dados["searchPhrase"];
        }
    }
}