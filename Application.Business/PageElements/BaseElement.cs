namespace Application.Business.PageElements;

using Application.Model.PageElements;
using Application.Business;
using Common.Utils.ExceptionWrapper;

public class BaseElement(BaseElementControl model) : BusinessControlBase(model)
{
    public virtual string Text {
        get { return model.Text; }
        set 
        { 
            throw new NotImplementedException($"{nameof(BaseElement)} setter for Text is not implemented!");
        }
    }

    public bool WaitValid(int timeout = 0)
    {
        try
        {
            model.WaitValid(timeout);
            return true;
        } catch
        {
            return false;
        }
    }
    public bool WaitInvalid(int timeout = 0)
    {
        try
        {
            model.WaitInvalid(timeout);
            return true;
        } catch
        {
            return false;
        }
    }
}