using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MinimalApi.Dominio.Servicos;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServiceTest
{
    private DbContexto CriarContextoDeTest()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        // passa o IConfiguration diretamente
        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TesntandoSalvarAdministrador()
    {
        var contexto = CriarContextoDeTest();
        contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo = new Veiculo();
        veiculo.Id = 1;
        veiculo.Nome = "Gol";
        veiculo.Marca = "VW";
        veiculo.Ano = 2020;

        var veiculoServico = new VeiculoServico(contexto);

        //Act
        veiculoServico.Incluir(veiculo);
        var veiculoEncontrado = veiculoServico.BuscaPorId(veiculo.Id);

        //Assert
        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }

}