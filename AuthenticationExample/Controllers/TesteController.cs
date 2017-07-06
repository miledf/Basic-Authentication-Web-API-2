using System.Web.Http;

namespace AuthenticationExample.Controllers
{
    [Authorize]
    [RoutePrefix("api/teste")]
    public class TesteController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("anonimo")]
        public IHttpActionResult anonimo()
        {
            return Json("Hello World");
        }
        
        [HttpGet]
        [Route("autenticado")]
        public IHttpActionResult autenticado()
        {
            return Json("Hello World autenticado");
        }
    }
}
