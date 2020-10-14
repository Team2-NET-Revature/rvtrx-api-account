using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.Controllers;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AddressControllerTest
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AccountContext> _options;
    private readonly AddressController _controller;
    private readonly ILogger<AddressController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddressControllerTest()
    {
      _connection = new SqliteConnection("Data Source=:memory:");
      _options = new DbContextOptionsBuilder<AccountContext>()
        .UseSqlite(_connection)
        .Options;

      var contextMock = new Mock<AccountContext>(_options);
      var loggerMock = new Mock<ILogger<AddressController>>();
      var repositoryMock = new Mock<Repository<AddressModel>>(new AccountContext(_options));
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<AddressModel>())).Returns(Task.FromResult<AddressModel>(null));
      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<AddressModel>>(null));
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<AddressModel>(null));
      repositoryMock.Setup(m => m.Update(It.IsAny<AddressModel>()));
      unitOfWorkMock.Setup(m => m.Address).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new AddressController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(-1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(-5);
      var resultOne = await _controller.Get(-1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new AddressModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new AddressModel());

      Assert.NotNull(resultPass);
    }
  }
}
