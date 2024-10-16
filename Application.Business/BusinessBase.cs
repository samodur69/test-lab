namespace Application.Business;

using Application.Model;

public class BusinessBase(ModelBase rootModel)
{
    protected readonly ModelBase BaseModel = rootModel;
    public string Url
    {
        get => BaseModel.Url;
        set => BaseModel.Url = value;
    }
    public List<System.Net.Cookie> GetCookies => [.. BaseModel.Cookies];
    public void Open() => BaseModel.OpenUrl();
    public static void Refresh() => ModelBase.Refresh();
    public static void TakeScreenshot(string name = "") => ModelBase.TakeScreenshot(name);
    public static void CloseBrowser() => ModelBase.CloseBrowser();
}
