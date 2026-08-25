
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocaControl.Models;
using LocaControl.Data;

public class EquipamentosController : Controller
{
    private readonly AppDbContext _context;

    public EquipamentosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: EQUIPAMENTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Equipamentos.ToListAsync());
    }

    // GET: EQUIPAMENTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var equipamento = await _context.Equipamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (equipamento == null)
        {
            return NotFound();
        }

        return View(equipamento);
    }

    // GET: EQUIPAMENTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EQUIPAMENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Categoria,Modelo,ValorDiaria,Disponivel")] Equipamento equipamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(equipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(equipamento);
    }

    // GET: EQUIPAMENTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento == null)
        {
            return NotFound();
        }
        return View(equipamento);
    }

    // POST: EQUIPAMENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Categoria,Modelo,ValorDiaria,Disponivel")] Equipamento equipamento)
    {
        if (id != equipamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(equipamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoExists(equipamento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(equipamento);
    }

    // GET: EQUIPAMENTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var equipamento = await _context.Equipamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (equipamento == null)
        {
            return NotFound();
        }

        return View(equipamento);
    }

    // POST: EQUIPAMENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento != null)
        {
            _context.Equipamentos.Remove(equipamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EquipamentoExists(int? id)
    {
        return _context.Equipamentos.Any(e => e.Id == id);
    }
}
