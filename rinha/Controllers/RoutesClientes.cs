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

                var ultimas_transacoes = await context.transacoes
                .Where(t => t.Cliente_id == id)
                .OrderByDescending(t => t.Realizada_em)
                .Take(10)
                .Select(
                    t => new
                    {
                        t.Valor,
                        t.Tipo,
                        t.Descricao,
                        t.Realizada_em,
                    }
                )
                .ToListAsync();

                var cliente = await context.clientes.SingleOrDefaultAsync(t => t.Id == id);

                return Results.Ok(new { saldo = new { total = cliente.Saldo, data_extrato = DateTime.UtcNow, limite = cliente.Limite }, ultimas_transacoes });

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

                var cliente = await context.clientes.FindAsync(id);

                switch (Request.tipo)
                {
                    case "c":
                        cliente.Saldo += Request.valor;

                        context.transacoes.Add(new Transacoes(id, Request.valor, Request.tipo, Request.descricao));

                        await context.SaveChangesAsync();
                        return Results.Ok(new { limite = cliente.Limite, saldo = cliente.Saldo });

                    case "d":

                        cliente.Saldo -= Request.valor;

                        if (cliente.Limite + cliente.Saldo < 0)
                        {
                            return Results.UnprocessableEntity();
                        }
                        else
                        {

                            context.transacoes.Add(new Transacoes(id, Request.valor, Request.tipo, Request.descricao));

                            await context.SaveChangesAsync();
                            return Results.Ok(new { limite = cliente.Limite, saldo = cliente.Saldo });

                        }



                    default:
                        return Results.UnprocessableEntity();
                }
            });

        }
    }
}