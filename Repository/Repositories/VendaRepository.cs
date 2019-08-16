using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        public SistemaContext context;

        public VendaRepository()
        {
            context = new SistemaContext();
        }

        public bool Alterar(Venda venda)
        {
            var vendaOriginal = context.Vendas
                .FirstOrDefault(x => x.Id == venda.Id);
            if (venda == null)
                return false;

            vendaOriginal.IdCliente = venda.IdCliente;
            vendaOriginal.Descricao = venda.Descricao;
            int quantidadeAfetada = context.SaveChanges();
            return quantidadeAfetada == 1;
        }

        public bool Apagar(int id)
        {
            var venda = context.Vendas
                .FirstOrDefault(x => x.Id == id);
            if (venda == null)
                return false;
            venda.RegistroAtivo = false;
            int quantidadeAfetada = context.SaveChanges();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Venda venda)
        {
            venda.RegistroAtivo = true;
            context.Vendas.Add(venda);
            context.SaveChanges();
            return venda.Id;
        }

        public Venda ObterPeloId(int id)
        {
            return context.Vendas
                .Include("Cliente")
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Venda> ObterTodos()
        {
            return context
                .Vendas
                .Where(x => x.RegistroAtivo)
                .ToList();
        }
    }
}
