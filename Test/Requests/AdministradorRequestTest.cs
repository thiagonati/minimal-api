using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class AdministradorRequestTest
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

    [TestMethod]
    public async Task Testar_Login_Administrador_Com_Sucesso()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "O login deve retornar 200 OK");

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado, "O objeto AdministradorLogado não deve ser nulo");
        Assert.IsFalse(string.IsNullOrEmpty(admLogado?.Email), "O e-mail não deve ser vazio");
        Assert.IsFalse(string.IsNullOrEmpty(admLogado?.Perfil), "O perfil não deve ser vazio");
        Assert.IsFalse(string.IsNullOrEmpty(admLogado?.Token), "O token não deve ser vazio");

        Console.WriteLine($"Token retornado: {admLogado?.Token}");

        Setup.Token = admLogado!.Token;
    }
}
