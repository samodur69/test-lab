namespace Application.Test;

public static class LoginTestCases
{
    public static IEnumerable<TestCaseData> WrongUsernameAndEmailWrongPassword(){
        yield return new TestCaseData("SfA5aS1YFEJKnTHNuusChXsSNJhA", "@323sss13");
        yield return new TestCaseData("wronk_email@email.com", "$$2391470214aa");
        yield return new TestCaseData("somethingsomething@bad.com", "$$2391470214aa");
        yield return new TestCaseData("90123745sChXsS9132phPHWE3h6", "$$2391470214aa");
    }

    public static IEnumerable<TestCaseData> ValidEmailUsername(){
        yield return new TestCaseData("SfA5aS1YFEJKnTHNuusChXsSNJhA");
        yield return new TestCaseData("wronk_email@email.com");
    }

    public static IEnumerable<TestCaseData> ValidPassword(){
        yield return new TestCaseData("2139128@$!$90");
        yield return new TestCaseData("2194y21ihS*YQ389i6o36");
        yield return new TestCaseData("2391470214aa");
        yield return new TestCaseData("1902354upoJS(Y40po2n789)");
    }
}