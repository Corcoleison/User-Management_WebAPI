using FluentAssertions;
using Moq;
using UserManagement.Application.Business.Service;
using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Enums;
using UserManagement.Domain.RepositoryInterfaces;
using Xunit.Sdk;
using static UserManagement.Application.Business.Constants.Constants;

namespace UserManagement.Application.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> mockAccessCodeRepository = new Mock<IUserRepository>();
        private readonly UserService service;

        public UserServiceTests()
        {
           service = new UserService(mockAccessCodeRepository.Object);
        }

        #region Create tests

        [Fact]
        public async Task CreateUser_OkModel_ReturnsCreatedUserAsync()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    }
                }
            };

            mockAccessCodeRepository.Setup(x => x.CreateUser(It.IsAny<User>()))
                                    .ReturnsAsync(expectedUser)
                                    .Verifiable();

            //Act
            var result = await service.CreateUser(expectedUser);

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUser);
        }

        [Theory]
        [InlineData(PaymentType.AmericanExpress)]
        [InlineData(PaymentType.Visa)]
        public void CreateUser_ModelHasDuplicatedPayment_ThrowsException(PaymentType paymentType)
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=paymentType,
                        Default=true,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        PaymentType=paymentType,
                        Default=false,
                        UserId=1,
                    },
                }
            };

            //Act
            Action act = () => service.CreateUser(expectedUser);

            //Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(ErrorMessages.DuplicationPaymentError);
        }

        [Fact]
        public void CreateUser_ModelHasMoreThanFivePayment_ThrowsException()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        PaymentType=PaymentType.Visa,
                        Default=false,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 3,
                        PaymentType=PaymentType.MasterCard,
                        Default=false,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 4,
                        PaymentType=PaymentType.Paypal,
                        Default=false,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 5,
                        PaymentType=PaymentType.BankAccount,
                        Default=false,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 6,
                        PaymentType=PaymentType.PaySafeCard,
                        Default=false,
                        UserId=1,
                    },
                }
            };

            //Act
            Action act = () => service.CreateUser(expectedUser);

            //Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(ErrorMessages.NumberOfPaymentaError);
        }

        [Fact]
        public void CreateUser_ModelHasMoreThanOneDefaultPayment_ThrowsException()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.Paypal,
                        Default=true,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        PaymentType=PaymentType.PaySafeCard,
                        Default=true,
                        UserId=1,
                    },
                }
            };

            //Act
            Action act = () => service.CreateUser(expectedUser);

            //Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(ErrorMessages.DefaultPaymentError);
        }

        [Fact]
        public void CreateUser_ModelHasIncorrectEnumType_ThrowsException()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=(PaymentType)45,
                        Default=true,
                        UserId=1,
                    },
                }
            };

            //Act
            Action act = () => service.CreateUser(expectedUser);

            //Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(ErrorMessages.EnumPaymentTypeError);
        }
        #endregion

        #region Delete Tests

        [Fact]
        public async Task DeleteUser_OkModel_ReturnsDeletedUser()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    }
                }
            };

            mockAccessCodeRepository.Setup(x => x.DeleteUser(It.IsAny<int>()))
                                    .ReturnsAsync(expectedUser)
                                    .Verifiable();

            //Act
            var result = await service.DeleteUser(expectedUser.Id);

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task DeleteUser_UserNotFoundAndRepositoryReturnsNull_ReturnsNull()
        {
            //Arrange
            User? expectedUser = null;

            mockAccessCodeRepository.Setup(x => x.DeleteUser(It.IsAny<int>()))
                                    .ReturnsAsync(expectedUser)
                                    .Verifiable();

            //Act
            var result = await service.DeleteUser(1);

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUser);
        }

        #endregion

        #region Get Tests

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
               {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    }
                }
            };

            var expectedUserList = new List<User>()
            {
                expectedUser
            };

            mockAccessCodeRepository.Setup(x => x.GetAllUsers())
                                    .ReturnsAsync(expectedUserList)
                                    .Verifiable();

            //Act
            var result = await service.GetAllUsers();

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUserList);
        }

        [Fact]
        public async Task GetPaginatedUsers_ReturnsPaginatedUsers()
        {
            //Arrange
            var expectedFirstUser = new User()
            {
                Email = "email",
                Name = "First",
                Id = 1,
                PaymentMethods = new[]
               {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        PaymentType=PaymentType.Paypal,
                        Default=false,
                        UserId=1,
                    }
                }
            };

            var expectedSecondUser = new User()
            {
                Email = "email",
                Name = "Second",
                Id = 1,
                PaymentMethods = new[]
               {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    },
                    new PaymentMethod
                    {
                        Id = 2,
                        PaymentType=PaymentType.Paypal,
                        Default=false,
                        UserId=1,
                    }
                }
            };

            var expectedUserList = new User[]
            {
                expectedFirstUser,
                expectedSecondUser
            };

            var expectedPaginatedUserList = new User[]
            {
                expectedFirstUser
            };

            mockAccessCodeRepository.Setup(x => x.GetAllUsers())
                                    .ReturnsAsync(expectedUserList)
                                    .Verifiable();

            //Act
            var result = await service.GetPaginatedUsers(It.IsAny<int>(),1);

            //Assert
            mockAccessCodeRepository.Verify();
            result.Item1.Should().BeEquivalentTo(expectedPaginatedUserList);
            result.Item2.Should().Be(expectedUserList.Length);
        }

        [Fact]
        public async Task GetUser_ReturnUser()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
               {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    }
                }
            };

            mockAccessCodeRepository.Setup(x => x.GetUser(It.IsAny<int>()))
                                    .ReturnsAsync(expectedUser)
                                    .Verifiable();

            //Act
            var result = await service.GetUser(It.IsAny<int>());

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUser);
        }

        #endregion
        #region Update tests

        [Fact]
        public async Task UpdateUser_OkModel_ReturnsUpdatedUser()
        {
            //Arrange
            var expectedUser = new User()
            {
                Email = "email",
                Name = "name",
                Id = 1,
                PaymentMethods = new[]
               {
                    new PaymentMethod
                    {
                        Id = 1,
                        PaymentType=PaymentType.AmericanExpress,
                        Default=true,
                        UserId=1,
                    }
                }
            };

            mockAccessCodeRepository.Setup(x => x.UpdateUser(It.IsAny<User>()))
                                    .ReturnsAsync(expectedUser)
                                    .Verifiable();

            //Act
            var result = await service.UpdateUser(expectedUser);

            //Assert
            mockAccessCodeRepository.Verify();
            result.Should().BeEquivalentTo(expectedUser);
        }

        #endregion
    }
}