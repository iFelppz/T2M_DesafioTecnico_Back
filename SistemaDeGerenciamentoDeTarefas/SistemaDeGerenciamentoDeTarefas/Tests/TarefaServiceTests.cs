using Moq;
using NUnit.Framework;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Enums;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Repositores;
using SistemaDeGerenciamentoDeTarefas.Service;
using Moq;
using NUnit.Framework;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Service;

namespace SistemaDeGerenciamentoDeTarefas.Tests
{
    public class TarefaServiceTests
    {
    }
}

[TestFixture]
public class TarefaServiceTests
{
    private Mock<ITarefaRepository> _mockTarefaRepository;
    private TarefaService _tarefaService;

    [SetUp]
    public void Setup()
    {
        _mockTarefaRepository = new Mock<ITarefaRepository>();
        _tarefaService = new TarefaService(_mockTarefaRepository.Object);
    }

    [Test]
    public void CriarTarefa_ShouldAddNewTarefa()
    {
        // Arrange
        var tarefaDto = new InserirTarefaDTO
        {
            Titulo = "Nova Tarefa",
            Descricao = "Descrição da nova tarefa",
            UsuarioId = 1,
            Status = StatusTarefa.Pendente
        };
        var tarefaModel = new TarefaModel
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            UsuarioId = tarefaDto.UsuarioId,
            Status = tarefaDto.Status
        };
        _mockTarefaRepository.Setup(repo => repo.Adicionar(It.IsAny<TarefaModel>())).Returns(tarefaModel);

        // Act
        var result = _tarefaService.CriarTarefa(tarefaDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(tarefaDto.Titulo, result.Titulo);
        Assert.AreEqual(tarefaDto.Descricao, result.Descricao);
    }

    [Test]
    public void ListarTudo_ShouldReturnAllTarefas()
    {
        // Arrange
        var tarefas = new List<TarefaModel>
    {
        new TarefaModel { Id = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1" },
        new TarefaModel { Id = 2, Titulo = "Tarefa 2", Descricao = "Descrição 2" }
    };
        _mockTarefaRepository.Setup(repo => repo.GetAll()).Returns(tarefas);

        // Act
        var result = _tarefaService.ListarTudo();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [Test]
    public void AtualizarTarefa_ShouldUpdateExistingTarefa()
    {
        // Arrange
        var tarefaModel = new TarefaModel
        {
            Id = 1,
            Titulo = "Tarefa Existente",
            Descricao = "Descrição Existente",
            Status = StatusTarefa.Pendente
        };
        var tarefaDto = new TarefaDTO(tarefaModel)
        {
            Titulo = "Tarefa Atualizada",
            Descricao = "Descrição Atualizada",
            Status = StatusTarefa.Finalizado
        };
        _mockTarefaRepository.Setup(repo => repo.BuscarPorId(tarefaModel.Id)).Returns(tarefaModel);
        _mockTarefaRepository.Setup(repo => repo.Atualizar(It.IsAny<TarefaModel>()));

        // Act
        _tarefaService.AtualizarTarefa(tarefaModel.Id, tarefaDto.Titulo, tarefaDto.Descricao, tarefaDto.Status, tarefaModel.Prazo);

        // Assert
        _mockTarefaRepository.Verify(repo => repo.Atualizar(It.Is<TarefaModel>(t =>
            t.Id == tarefaModel.Id &&
            t.Titulo == tarefaDto.Titulo &&
            t.Descricao == tarefaDto.Descricao &&
            t.Status == tarefaDto.Status &&
            t.Prazo == tarefaDto.Prazo
        )), Times.Once);
    }

    [Test]
    public void DeletarTarefa_ShouldDeleteExistingTarefa()
    {
        // Arrange
        var tarefaModel = new TarefaModel { Id = 1, Titulo = "Tarefa a ser deletada" };
        _mockTarefaRepository.Setup(repo => repo.BuscarPorId(tarefaModel.Id)).Returns(tarefaModel);
        _mockTarefaRepository.Setup(repo => repo.Deletar(tarefaModel.Id));

        // Act
        _tarefaService.DeletarTarefa(tarefaModel.Id);

        // Assert
        _mockTarefaRepository.Verify(repo => repo.Deletar(tarefaModel.Id), Times.Once);
    }


}
