using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using rinha.Domain.Model;
using rinha.Infrastruture;

namespace rinha.Controllers
{
    public static class RoutesClientes
    {

        public static void MapClientesRoutes(this WebApplication app)
        {
            var rotaClientes = app.MapGroup("clientes");

            rotaClientes.MapGet("/{id}/extrato", async (int id, AppDBContext context) =>
            {

                if (id < 1 || id > 5)
                    return Results.NotFound();


                var value = await context.Extrato
                  .FromSqlInterpolated($"SELECT * FROM proc_extrato({id})")
                  .ToListAsync();


                var objetoDeserializado = JsonSerializer.Deserialize<ExtartoBody>(value[0].proc_extrato);
                return Results.Ok(objetoDeserializado);

            });

            rotaClientes.MapPost("{id}/transacoes", async (int id, AddTransacaoRecords? Request, AppDBContext context) =>
            {


                if (id < 1 || id > 5)
                    return Results.NotFound();

                if (
                    Request == null ||
                    String.IsNullOrEmpty(Request.descricao) ||
                    Request.valor < 0 ||
                    Request.descricao.Length > 10 ||
                    Request.tipo != "c" && Request.tipo != "d"
                )
                {
                    return Results.UnprocessableEntity();
                }

                var extrato = await context.AtualizarSaldos
                .FromSqlInterpolated($"SELECT * FROM maker_transacao({id}, {Request.tipo}, {Request.descricao}, {Request.valor})").ToListAsync();




                if (extrato[0].falha)
                {
                    return Results.UnprocessableEntity();

                }

                return Results.Ok(new { saldo = extrato[0].saldo, limite = extrato[0].limite_cliente, });
            });
        }
    }
}