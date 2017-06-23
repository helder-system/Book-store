   /*btnAcao - submete acões do form-group*/
    var btnAcao = $("input[type='button']");
    var formulario = $("#formCrud")
btnAcao.on("click", submeter);
    /*Se o formulario estiver valido pega a url do formulario e metodo para serializar e depois enviar para o servidor via Ajax para processar o tratamento do retorno (mensagem se o livro foi adiciona, editado ou excluido.) */
    function submeter() {
        if(formulario.valid()){
            var url = formulario.prop("action");
            var metodo = formulario.prop("method");
            var dadosFormulario = formulario.serialize();
            $.ajax({
    url: url,
    type: metodo,
    data: dadosFormulario,
    success: tratarRetorno
            });
         }
    }
//Parametro resultadoServidor - retorna o Json do LivrosController - Create, Edit e Delete - Post. 
//resultado(Boolean) Retorno Json - True se o livro for cadastrado e False se o livro não for cadastrado (Retorno Json). 
//mensagem - Mensagem enviada para o modal, caso o livro seja cadastrado ou não cadastrado.

function tratarRetorno(resultadoServidor) {
    if (resultadoServidor.resultado) {
        toastr['success'](resultadoServidor.mensagem);
        /*Fecha a modal*/
        $("#minhaModal").modal("hide");
         /*Recarrega a grid atualizada.*/
        $("#gridDados").bootgrid("reload");
    }
    else {
        toastr['error'](resultadoServidor.mensagem);
    }
}