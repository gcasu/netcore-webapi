using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Linq;

namespace WebApi.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Serializes the model state errors into a human readable object.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string Errors(this ModelStateDictionary modelState) =>
            JsonConvert.SerializeObject(modelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage));
    }
}
