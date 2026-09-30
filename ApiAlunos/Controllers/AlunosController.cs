using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiAlunos;

namespace ApiAlunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        // Lista de alunos usada na aula 
        private static List<Aluno> alunos = new()
{
    new Aluno { Id = 1, Nome = "Ana", Cidade = "Campinas" },
    new Aluno { Id = 2, Nome = "Bruno", Cidade = "Valinhos" },
    new Aluno { Id = 3, Nome = "Carlos", Cidade = "Campinas" },
    new Aluno { Id = 4, Nome = "Daniela", Cidade = "Vinhedo" },
    new Aluno { Id = 5, Nome = "Eduardo", Cidade = "Campinas" },
    new Aluno { Id = 6, Nome = "Fernanda", Cidade = "Valinhos" }
};
        // GET: busca alunos 
        [HttpGet]
        public IActionResult Get(
            string? cidade,
            string? sort,
            int page = 1,
            int pageSize = 3)
        {
            // Começa com todos os alunos 
            var resultado = alunos.AsQueryable();

            // Filtro por cidade 
            if (!string.IsNullOrEmpty(cidade))
            {
                resultado = resultado.Where(a => a.Cidade == cidade);
            }

            // Ordenação pelo nome 
            if (sort == "nome")
            {
                resultado = resultado.OrderBy(a => a.Nome);
            }

            // Total de alunos 
            var total = resultado.Count();

            // Paginação 
            var dados = resultado
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Retorna os dados 
            return Ok(new
            {
                pagina = page,
                tamanhoPagina = pageSize,
                total = total,
                alunos = dados
            });
            return Ok(new
            {
                pagina = page,
                tamanhoPagina = pageSize,
                total = total,
                alunos = dados
            });
        }
        // Insira 
        // GET por ID: busca um aluno específico 
        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
            {
                return Problem(
                    title: "Aluno não encontrado",
                    detail: $"O aluno com ID {id} não existe.",
                    statusCode: 404);
            }

            return Ok(aluno);
        }


    }
} 
 
    // este método procura um aluno pelo id
