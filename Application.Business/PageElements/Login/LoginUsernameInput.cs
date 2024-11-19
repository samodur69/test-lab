using Application.Model.PageElements.Login;

namespace Application.Business.PageElements.Login;

public class LoginUsernameInput(LoginUsernameInputControl model) : Input(model)
{
    public bool IsValidData() => model.IsValidData() && !model.IsValidatorError();
}