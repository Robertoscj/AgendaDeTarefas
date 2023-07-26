using AgendaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeTarefas.ViewComponents
{
    public class ListaDeTarefasViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;
        public ListaDeTarefasViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(string data)
        {
            return View(await _contexto.Tarefas
                .OrderBy(t => t.Horario).Where(t => t.Data == data).ToListAsync());
        }
    }
}
