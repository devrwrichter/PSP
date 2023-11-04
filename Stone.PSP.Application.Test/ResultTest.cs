using FluentAssertions;
using FluentValidation.Results;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace Stone.PSP.Application.Test
{
    public class ResultTest
    {
        [Fact]
        public void Result_WhenHasErrors_ReturnErrors()
        {
            //Arrange
            ValidationResult validationResult = new();
            validationResult.Errors = new List<ValidationFailure> {
                new ValidationFailure { ErrorMessage = "Error 1" },
                new ValidationFailure { ErrorMessage = "Error 2" } };

            //Action
            IResult<ClientViewModel> result = new Result<ClientViewModel>(validationResult);

            //Assert
            result.Model.Should().BeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
        }

        [Fact]
        public void Result_WhenNotErrors_ReturnModel()
        {
            //Arrange
            var obj = new ClientViewModel();

            //Action
            IResult<ClientViewModel> result = new Result<ClientViewModel>(obj);

            //Assert
            result.Model.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }
    }
}