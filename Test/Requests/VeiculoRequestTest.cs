using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    private void AddAuthHeader()
    {
        // Remove header antigo, se existir
        if (Setup.client.DefaultRequestHeaders.Contains("Authorization"))
            Setup.client.DefaultRequestHeaders.Remove("Authorization");

        Setup.client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Setup.Token);
    }

    [TestMethod]
    public async Task Testar_Listar_Todos_Veiculos()
    {
        AddAuthHeader();

        var response = await Setup.client.GetAsync("/veiculos");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculos);
        Assert.IsTrue(veiculos.Count > 0, "Deve retornar pelo menos um veículo");

        Console.WriteLine($"Quantidade de veículos retornados: {veiculos.Count}");
    }

    [TestMethod]
    public async Task Testar_Buscar_Veiculo_Por_Id()
    {
        AddAuthHeader();

        var response = await Setup.client.GetAsync("/veiculos/1");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculo);
        Assert.AreEqual(1, veiculo.Id);
        Console.WriteLine($"Veículo encontrado: {veiculo.Nome} ({veiculo.Marca})");
    }

    [TestMethod]
    public async Task Testar_Incluir_Novo_Veiculo()
    {
        AddAuthHeader();

        var novo = new Veiculo
        {
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2024
        };

        var content = new StringContent(JsonSerializer.Serialize(novo), Encoding.UTF8, "application/json");

        var response = await Setup.client.PostAsync("/veiculos", content);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<Veiculo>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoCriado);
        Assert.AreEqual("Corolla", veiculoCriado.Nome);
        Console.WriteLine($"Veículo criado com ID: {veiculoCriado.Id}");
    }

    [TestMethod]
    public async Task Testar_Atualizar_Veiculo()
    {
        AddAuthHeader();

        var veiculoAtualizado = new Veiculo
        {
            Id = 1,
            Nome = "Gol G6",
            Marca = "VW",
            Ano = 2021
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoAtualizado), Encoding.UTF8, "application/json");

        var response = await Setup.client.PutAsync("/veiculos/1", content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculo);
        Assert.AreEqual("Gol G6", veiculo.Nome);
        Console.WriteLine($"Veículo atualizado: {veiculo.Nome}");
    }

    [TestMethod]
    public async Task Testar_Apagar_Veiculo()
    {
        AddAuthHeader();

        var response = await Setup.client.DeleteAsync("/veiculos/2");

        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        Console.WriteLine("Veículo com ID 2 foi removido com sucesso.");
    }
}
