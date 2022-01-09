using Microsoft.AspNetCore.Http;
using MovieApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Extensions
{
    public static class HttpContextExtensions
    {
        public static DataTableParameter GetDataTableParameter(this IHttpContextAccessor controllerBase)
        {
            var dataTableParameter = new DataTableParameter();
            dataTableParameter.Filter = controllerBase.HttpContext.Request.Query["filter"];
            //dataTableParameter.Orderby = controllerBase.HttpContext.Request.Query["orderby"];
            int size = 0, index = 0;
            int.TryParse(controllerBase.HttpContext.Request.Query["pageSize"], out size);
            dataTableParameter.PageSize = size == 0 ? 10 : size;
            int.TryParse(controllerBase.HttpContext.Request.Query["pageIndex"], out index);
            dataTableParameter.PageIndex = index == 0 ? 10 : index;
            return dataTableParameter;
        }
    }
}
