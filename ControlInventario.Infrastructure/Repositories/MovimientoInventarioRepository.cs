using ControlInventario.Core.Entities;
using ControlInventario.Core.Interfaces;
using ControlInventario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Infrastructure.Repositories
{
    public class MovimientoInventarioRepository :
        IMovimientoInventarioRepository
    {
        private readonly InventarioContext _inventarioContext;

        public MovimientoInventarioRepository(
            InventarioContext inventarioContext)
        {
            _inventarioContext = inventarioContext;
        }

        public async Task<IEnumerable<MovimientoInventario>>
            GetAllMovimientosAsync()
        {
            return await _inventarioContext.MovimientosInventario
                .AsNoTracking()
                .OrderByDescending(m => m.FechaRegistro)
                .ThenByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<MovimientoInventario?>
            GetMovimientoByIdAsync(int id)
        {
            return await _inventarioContext.MovimientosInventario
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MovimientoInventario>>
            GetMovimientosByProductoAsync(int productoId)
        {
            return await _inventarioContext.MovimientosInventario
                .AsNoTracking()
                .Where(m => m.ProductoId == productoId)
                .OrderByDescending(m => m.FechaRegistro)
                .ThenByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task InsertMovimiento(
            MovimientoInventario movimiento)
        {
            _inventarioContext.MovimientosInventario.Add(movimiento);

            await _inventarioContext.SaveChangesAsync();
        }

        public async Task<ResultadoRegistroMovimiento>
            RegistrarMovimientoAsync(
                MovimientoInventario movimiento)
        {
            await using var transaccion =
                await _inventarioContext.Database
                    .BeginTransactionAsync(
                        System.Data.IsolationLevel.ReadCommitted);

            try
            {
                // Buscar producto
                var producto =
                    await _inventarioContext.Productos
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            p => p.Id == movimiento.ProductoId);

                if (producto == null)
                {
                    await transaccion.RollbackAsync();

                    return ResultadoRegistroMovimiento.Rechazado(
                        MotivoRechazoMovimiento.ProductoNoEncontrado);
                }

                // Verificar producto activo
                if (!producto.Activo)
                {
                    await transaccion.RollbackAsync();

                    return ResultadoRegistroMovimiento.Rechazado(
                        MotivoRechazoMovimiento.ProductoInactivo,
                        producto.StockActual);
                }

                // Verificar usuario
                var usuarioExiste =
                    await _inventarioContext.Usuarios
                        .AsNoTracking()
                        .AnyAsync(
                            u => u.Id == movimiento.UsuarioId);

                if (!usuarioExiste)
                {
                    await transaccion.RollbackAsync();

                    return ResultadoRegistroMovimiento.Rechazado(
                        MotivoRechazoMovimiento.UsuarioNoEncontrado);
                }

                var esEntrada =
                    string.Equals(
                        movimiento.TipoMovimiento,
                        "ENTRADA",
                        StringComparison.OrdinalIgnoreCase);

                int filasAfectadas;

                if (esEntrada)
                {
                    // CU-03:
                    // aumentar stock
                    filasAfectadas =
                        await _inventarioContext.Database
                            .ExecuteSqlInterpolatedAsync(
                                $@"
                                UPDATE productos
                                SET stock_actual =
                                    stock_actual + {movimiento.Cantidad}
                                WHERE id =
                                    {movimiento.ProductoId}
                                AND activo = 1
                                ");
                }
                else
                {
                    // Salida:
                    // disminuir stock solo si existe suficiente
                    filasAfectadas =
                        await _inventarioContext.Database
                            .ExecuteSqlInterpolatedAsync(
                                $@"
                                UPDATE productos
                                SET stock_actual =
                                    stock_actual - {movimiento.Cantidad}
                                WHERE id =
                                    {movimiento.ProductoId}
                                AND activo = 1
                                AND stock_actual >=
                                    {movimiento.Cantidad}
                                ");
                }

                if (filasAfectadas == 0)
                {
                    await transaccion.RollbackAsync();

                    var productoActual =
                        await _inventarioContext.Productos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(
                                p => p.Id ==
                                     movimiento.ProductoId);

                    if (productoActual == null)
                    {
                        return ResultadoRegistroMovimiento.Rechazado(
                            MotivoRechazoMovimiento.ProductoNoEncontrado);
                    }

                    if (!productoActual.Activo)
                    {
                        return ResultadoRegistroMovimiento.Rechazado(
                            MotivoRechazoMovimiento.ProductoInactivo,
                            productoActual.StockActual);
                    }

                    return ResultadoRegistroMovimiento.Rechazado(
                        MotivoRechazoMovimiento.StockInsuficiente,
                        productoActual.StockActual);
                }

                // Registrar movimiento
                _inventarioContext.MovimientosInventario
                    .Add(movimiento);

                await _inventarioContext.SaveChangesAsync();

                // Obtener stock actualizado
                var stockResultante =
                    await _inventarioContext.Productos
                        .AsNoTracking()
                        .Where(
                            p => p.Id ==
                                 movimiento.ProductoId)
                        .Select(
                            p => p.StockActual)
                        .FirstAsync();

                await transaccion.CommitAsync();

                return ResultadoRegistroMovimiento.Exito(
                    stockResultante);
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}