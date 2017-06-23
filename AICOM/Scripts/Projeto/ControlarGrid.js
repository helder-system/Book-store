
function configurarControles() {

    var traducao = {
        infos: "Exibindo {{ctx.start}} até {{ctx.end}} de {{ctx.total}} registros",
        loading: "Carregando, Isso pode levar alguns segundos...",
        noResults: "Não há dados para exibir.",
        refresh: "Atualizar",
        search: "Pesquisar"

    };
            
           var grid = $("#gridDados").bootgrid(
                {
                    ajax: true,
                    url:  urlListar,
                    labels: traducao,
                    searchSettings: {
                        characters: 4
                    },
                    /* Codigo antigo para listar e paginar
                       Adicionando botões Criar, listar e excluir
                       formatters - recurso do jquery bootgrid.*/

        formatters: {
            "acoes": function (column, row) {
                return "<a href='#' class='btn btn-info' data-acao='Details' data-row-id='" + row.Id + "'>" + "<span class='glyphicon glyphicon-list'></span>" + "</a> " +
                       "<a href='#' class='btn btn-warning' data-acao='Edit' data-row-id='" + row.Id + "'>" + "<span class='glyphicon glyphicon-edit'></span>" + "</a> " +
                       "<a href='#' class='btn btn-danger' data-acao='Delete' data-row-id='"+ row.Id +"'>" + "<span class='glyphicon glyphicon-trash'></span>" + "</a> ";
            }
        }
});

            /*grid.find("a.btn").eache() - Procurar os elementos botão da classe btn  e executar determinada função*/

            grid.on("loaded.rs.jquery.bootgrid", function () {
                grid.find("a.btn").each(function(index, elemento){
                    var  botaoDeAcao = $(elemento);
                    var acao = botaoDeAcao.data("acao");
                    var idEntidade = botaoDeAcao.data("row-id");

                    botaoDeAcao.on("click", function(){
                        abrirModal(acao,idEntidade);
                    });

                });
            });


            /*Identifica o link btn e chama a função abrir modal
$(this).data("action") - Pega parametro do data-action
*/
$("a.btn").click(function () {
    var acao = $(this).data("action");
    abrirModal(acao);
});
}


/* abrir modal
{ctrl} controler*/
function abrirModal(acao, parametro) {
var url = "/{ctrl}/{acao}/{parametro}";
url = url.replace("{ctrl}", controller);
url = url.replace("{acao}", acao);

if (parametro != null) {
url = url.replace("{parametro}", parametro);
}
else {
url = url.replace("{parametro}", "");
}

$("#conteudoModal").load(url, function () {
$("#minhaModal").modal('show');
});
}
