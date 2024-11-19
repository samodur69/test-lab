using Application.Model.PageElements.Login;

namespace Application.Business.PageElements.Login;

public class LoginPasswordInput(LoginPasswordInputControl model) : Input(model)
{
    public bool IsValidData() => model.IsValidData() && !model.IsValidatorError();
}