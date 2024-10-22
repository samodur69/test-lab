namespace Application.Business.PageElements.Login;
using Application.Model.PageElements.Login;

public class LoginErrorBanner(LoginErrorBannerControl model) : Input(model)
{
    public bool IsError() => model.IsError();
}