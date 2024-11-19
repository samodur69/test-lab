using Application.Model.PageElements.Login;

namespace Application.Business.PageElements.Login;

public class LoginErrorBanner(LoginErrorBannerControl model) : Input(model)
{
    public bool IsError() => model.IsError();
}