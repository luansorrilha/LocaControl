using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocaControl.Models;
using LocaControl.Data;

public class LocacoesController : Controller
{
    private readonly AppDbContext _context;

    public LocacoesController(AppDbContext context)
    {
        _context = context;
    }

    // LISTAR LOCAÇÕES
    public async Task<IActionResult> Index()
    {
        var locacoes = _context.Locacoes
            .Include(l => l.Cliente)
            .Include(l => l.Equipamento);

        return View(await locacoes.ToListAsync());
    }

    // DETALHES
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var locacao = await _context.Locacoes
            .Include(l => l.Cliente)
            .Include(l => l.Equipamento)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (locacao == null)
        {
            return NotFound();
        }

        return View(locacao);
    }
    // ABRIR TELA DE EDIÇÃO
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var locacao = await _context.Locacoes.FindAsync(id);

        if (locacao == null)
        {
            return NotFound();
        }

        ViewBag.ClienteId = new SelectList(
            await _context.Clientes.ToListAsync(),
            "Id",
            "Nome",
            locacao.ClienteId
        );

        ViewBag.EquipamentoId = new SelectList(
            await _context.Equipamentos.ToListAsync(),
            "Id",
            "Nome",
            locacao.EquipamentoId
        );

        return View(locacao);
    }

    // SALVAR NOVA LOCAÇÃO
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,ClienteId,EquipamentoId,DataInicio,DataFinal,ValorTotal")]
        Locacao locacao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(locacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["ClienteId"] =
            new SelectList(
                _context.Clientes,
                "Id",
                "Nome",
                locacao.ClienteId);

        ViewData["EquipamentoId"] =
            new SelectList(
                _context.Equipamentos,
                "Id",
                "Nome",
                locacao.EquipamentoId);

        return View(locacao);
    }

 
    // SALVAR EDIÇÃO
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
     int? id,
        [Bind("Id,ClienteId,EquipamentoId,DataInicio,DataFinal,ValorTotal")]
        Locacao locacao)
    {
        if (id != locacao.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(locacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(locacao.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["ClienteId"] =
            new SelectList(
                _context.Clientes,
                "Id",
                "Nome",
                locacao.ClienteId);

        ViewData["EquipamentoId"] =
            new SelectList(
                _context.Equipamentos,
                "Id",
                "Nome",
                locacao.EquipamentoId);

        return View(locacao);
    }

    // ABRIR TELA DE EXCLUSÃO
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var locacao = await _context.Locacoes
            .Include(l => l.Cliente)
            .Include(l => l.Equipamento)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (locacao == null)
        {
            return NotFound();
        }

        return View(locacao);
    }

    // CONFIRMAR EXCLUSÃO
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var locacao = await _context.Locacoes.FindAsync(id);

        if (locacao != null)
        {
            _context.Locacoes.Remove(locacao);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool LocacaoExists(int? id)
    {
        return _context.Locacoes.Any(e => e.Id == id);
    }
}