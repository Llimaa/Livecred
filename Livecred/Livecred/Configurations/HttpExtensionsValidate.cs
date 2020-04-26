using Microsoft.AspNetCore.Http;
namespace Livecred.Configurations
{
    public static class HttpExtensionsValidate
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                return false;
            else if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}
