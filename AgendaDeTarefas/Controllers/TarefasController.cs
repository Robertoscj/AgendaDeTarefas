using AgendaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly Contexto _contexto;
        public TarefasController(Contexto contexto) 
        {
            _contexto = contexto;
        }
        private List<DatasViewlModel> PegarDatas()
        {
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = DateTime.Now.AddDays(10);
            int qtdDias = 0;
            DatasViewlModel data;
            List<DatasViewlModel> listaDatas = new List<DatasViewlModel>();

            while (dataAtual < dataLimite)
            {
                data = new DatasViewlModel();
                data.Datas = dataAtual.ToShortDateString();
                data.Identificadores = "collapse" + dataAtual.ToShortDateString().Replace("/", "");
                listaDatas.Add(data);
                qtdDias = qtdDias + 1;
                dataAtual = DateTime.Now.AddDays(qtdDias);
            }

            return listaDatas;
        }


        [HttpGet]
        public IActionResult CriarTarefa(string dataTarefa)
        {
            Tarefa tarefa = new Tarefa
            {
                Data = dataTarefa
            };

            return View(tarefa);
        }



        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Tarefas.Add(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }



        [HttpGet]
        public async Task<IActionResult> AtualizarTarefa(int Id)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(Id);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirTarefa(int Id)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(Id);
            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
            return Json(true);
        }


    }
}
