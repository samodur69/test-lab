namespace Application.Business;

using Application.Model;
using Common.Utils.Waiter;

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
    protected static void WaitForDomProperty(ModelControlBase elem, string prop, string value)
    {
        if(!Waiter.WaitUntil(() => elem.GetDomProperty(prop) == value))
            throw new Exception($"Dom property's value '{prop}' did not match the argument '{value}'!");
    }
    protected static void WaitForAttribute(ModelControlBase elem, string attrib, string value)
    {
        if(!Waiter.WaitUntil(() => elem.GetAttribute(attrib) == value))
            throw new Exception($"Attribute's value '{attrib}' did not match the argument '{value}'!");
    }
    protected static void WaitUntilEnabled(ModelControlBase elem)
    {
        if(!Waiter.WaitUntil(() => elem.IsEnabled))
            throw new Exception($"Element is not enabled!");
    }
    protected static void WaitUntilVisible(ModelControlBase elem)
    {
        if(!Waiter.WaitUntil(() => elem.IsVisible))
            throw new Exception($"Element is not visible!");
    }
    protected static void WaitUntilSelected(ModelControlBase elem)
    {
        if(!Waiter.WaitUntil(() => elem.IsSelected))
            throw new Exception($"Element is not selected!");
    }
}
