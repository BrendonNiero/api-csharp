using System.Net.Http.Headers;
using API_CSHARP.Data;
using Microsoft.EntityFrameworkCore;
namespace API_CSHARP.Estudantes;

public static class EstudantesRotas
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        var rotasEstudantes = app.MapGroup("estudantes");

        // Criar
        rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDBContext context) => 
        {
            var jaExiste = await context.Estudantes.AnyAsync(estudante => estudante.Nome == request.Nome);

            if(jaExiste)
                return Results.Conflict("Já existe!");

            var novoEstudante = new Estudante(request.Nome);
            await context.Estudantes.AddAsync(novoEstudante);
            await context.SaveChangesAsync();

            var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);
            return Results.Ok(estudanteRetorno);
        });

        // Listar
        rotasEstudantes.MapGet("", async (AppDBContext context) => {
            var estudantes = await context.Estudantes
            .Where(estudantes => estudantes.Ativo)
            .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
            .ToListAsync();
            return estudantes;
        });

        // Atualizar
        rotasEstudantes.MapPut("{id}", async (Guid id, UpdateEstudanteRequest request, AppDBContext context) => {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id);

            if(estudante == null)
                return Results.NotFound();

            estudante.AtualizarNome(request.Nome);

            await context.SaveChangesAsync();
            return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
        });
    }
}