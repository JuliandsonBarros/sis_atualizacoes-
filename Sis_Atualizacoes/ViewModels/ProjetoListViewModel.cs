using Sis_Atualizacoes.Models;

namespace Sis_Atualizacoes.ViewModels
{
    public class ProjetoListViewModel
    {
        public Projetos Projeto { get; set; }

        public IEnumerable<PacotesAtualizacoes> Pacote { get; set; }
       
        public ProjetoListViewModel()
        {
            Projeto = new Projetos();
            Pacote = new List<PacotesAtualizacoes>();
        }
    }

   
}
