using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AICOM.ViewModels;

namespace AICOM.Infra
{
    public class ParametrosPaginacaoModelBinder:IModelBinder    
    {
        //ModelBinder Customizado - responsavel por fazer o tratamento das request da classe ParametrosPaginacao.
        //Precisa ser adicionada no global customizar o ModelBinder e informar o tratamento de request. 

       public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            
            ParametrosPaginacao parametrosPaginacao = new ParametrosPaginacao(request.Form);
            return parametrosPaginacao;

        }
    }
}
