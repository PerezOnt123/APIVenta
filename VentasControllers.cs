// Controlador VentasController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class VentasController : ControllerBase
{
    private readonly VentasContext _context;

    public VentasController(VentasContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Venta>> ObtenerVentas()
    {
        return _context.Ventas.ToList();
    }

    [HttpGet("{folio}")]
    public ActionResult<Venta> ObtenerVenta(string folio)
    {
        var venta = _context.Ventas.Find(folio);
        if (venta == null) return NotFound();
        return venta;
    }

    [HttpPost]
    public ActionResult<Venta> CrearVenta(Venta venta)
    {
        _context.Ventas.Add(venta);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObtenerVenta), new { folio = venta.Folio }, venta);
    }

    [HttpPut("{folio}")]
    public IActionResult ActualizarVenta(string folio, Venta ventaActualizada)
    {
        var venta = _context.Ventas.Find(folio);
        if (venta == null) return NotFound();
        
        venta.Fecha = ventaActualizada.Fecha;
        venta.Cantidad = ventaActualizada.Cantidad;
        venta.Total = ventaActualizada.Total;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{folio}")]
    public IActionResult EliminarVenta(string folio)
    {
        var venta = _context.Ventas.Find(folio);
        if (venta == null) return NotFound();
        
        _context.Ventas.Remove(venta);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpGet("ordenadasPorTotal")]
    public ActionResult<IEnumerable<Venta>> ObtenerVentasOrdenadas()
    {
        return _context.Ventas.OrderByDescending(v => v.Total).ToList();
    }

    [HttpGet("mayoresA1000")]
    public ActionResult<IEnumerable<Venta>> ObtenerVentasMayoresA1000()
    {
        return _context.Ventas.Where(v => v.Total > 1000).ToList();
    }

    [HttpGet("porAnio/{anio}")]
    public ActionResult<IEnumerable<Venta>> ObtenerVentasPorAnio(int anio)
    {
        return _context.Ventas.Where(v => v.Fecha.Year == anio).ToList();
    }

    [HttpGet("mayorImporte")]
    public ActionResult<Venta> ObtenerVentaMayorImporte()
    {
        var venta = _context.Ventas.OrderByDescending(v => v.Total).FirstOrDefault();
        if (venta == null) return NotFound();
        return venta;
    }
}