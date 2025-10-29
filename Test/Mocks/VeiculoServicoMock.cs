using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static List<Veiculo> _veiculos = new List<Veiculo>()
    {
        new Veiculo {
            Id = 1,
            Nome = "Gol",
            Marca = "VW",
            Ano = 2020
        },
        new Veiculo {
            Id = 2,
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        }
    };

    private static int _nextId = 3;

    public void Apagar(Veiculo veiculo)
    {
        _veiculos.Remove(veiculo);
    }

    public void Atualizar(Veiculo veiculo)
    {
        var index = _veiculos.FindIndex(v => v.Id == veiculo.Id);

        if (index != -1)
            _veiculos[index] = veiculo;
    }


    public Veiculo? BuscaPorId(int id)
    {
        return _veiculos.FirstOrDefault(v => v.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
        // Atribui um novo ID se não tiver
        if (veiculo.Id == 0)
        {
            veiculo.Id = _nextId++;
        }

        _veiculos.Add(veiculo);
    }

    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
    {
        var query = _veiculos.AsQueryable();

        // Aplica filtros
        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(v => v.Marca.Contains(marca, StringComparison.OrdinalIgnoreCase));
        }

        // Aplica paginação
        if (pagina.HasValue && pagina.Value > 0)
        {
            const int TAMANHO_PAGINA = 10;
            int pular = (pagina.Value - 1) * TAMANHO_PAGINA;
            query = query.Skip(pular).Take(TAMANHO_PAGINA);
        }

        return query.ToList();
    }
}