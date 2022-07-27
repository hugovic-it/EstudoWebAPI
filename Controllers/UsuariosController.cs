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

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> Get(int id)
        {//Retorna o usuario pelo Id
            var usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);
            if (usuario is null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {

            if (usuario is null)
            {
                return BadRequest();
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterUsuario",
                 new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);
            //var produto = _context.Usuarios.Find(id)
            if (usuario is null)
            {
                return NotFound("Produto nao localizado...");
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }

    }
}
