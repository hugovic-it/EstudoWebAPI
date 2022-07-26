using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDogs.Data;
using NDogs.Models;

namespace NDogs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get() //Retorna todos os usuarios 
        {
            var usuarios = _context.Usuarios.ToList();
            if (usuarios is null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }



    }
}
