using Microsoft.AspNetCore.Mvc;
using Produtos.Context;
using Produtos.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Produtos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly LojaContext _context;

        public ProdutoController(LojaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            try
            {
               _context.Add(produto);
               _context.SaveChanges();
               return CreatedAtAction(nameof(ObterPorId), new {id = produto.Id}, produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var produto = _context.Produtos.Find(id);

            if(produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var produtos = _context.Produtos.Where(x => x.Nome.Contains(nome));
            return Ok(produtos);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Produto produto)
        {
            var produtoBanco = _context.Produtos.Find(id);

            if(produtoBanco == null)
            {
                return NotFound();
            }

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Quantidade = produto.Quantidade;
            produtoBanco.Preco = produto.Preco;

            _context.Produtos.Update(produtoBanco);
            _context.SaveChanges();

            return Ok(produtoBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var produtoBanco = _context.Produtos.Find(id);

            if(produtoBanco == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produtoBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
