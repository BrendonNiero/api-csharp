using API_CSHARP.Data;
namespace API_CSHARP.Estudantes;

public static class EstudantesRotas
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        var rotasEstudantes = app.MapGroup("estudantes");

        rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDBContext context) => 
        {
            var novoEstudante = new Estudante(request.Nome);

            await context.Estudantes.AddAsync(novoEstudante);
            await context.SaveChangesAsync();
        });
    }
}