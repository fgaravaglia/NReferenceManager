namespace NReferenceManager.Cli.Tests;

public class ReferenceManagerTests
{
    ReferenceManager _Manager;

    [SetUp]
    public void Setup()
    {
        this._Manager = new ReferenceManager();
    }

    [Test]
    public void ParseArguments_DoesNotThrowEx_IfOptionIsNotManaged()
    {
        //***************** GIVEN
        List<string> args = ["-Not-Managed=blabla"];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.False);
        Assert.Pass();
    }

    [Test]
    public void ParseArguments_DoesNotThrowEx_IfSettingsAreNotValid()
    {
        //***************** GIVEN
        List<string> args = [];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.False);
        Assert.Pass();
    }

    [Test]
    public void Run_ReturnsMinusTwo_IfIsNotInitialized()
    {
        //***************** GIVEN
        List<string> args = [];
        this._Manager.ParseArguments(args);
        Assert.That(_Manager.IsInitialized, Is.False);

        //***************** WHEN
        var result = this._Manager.Run();

        //***************** ASSERT
        Assert.That(result, Is.EqualTo(-2));
        Assert.Pass();
    }

    [Test]
    public void ParseArguments_InitializesManager_IfSettingsAreValid()
    {
        //***************** GIVEN
        List<string> args = ["-r='C:\\Temp\\Project'"];
        this._Manager.ParseArguments(args);
        Assert.That(_Manager.IsInitialized, Is.True);

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        var result = this._Manager.Run();

        //***************** ASSERT
        Assert.That(result, Is.EqualTo(0));
        Assert.Pass();
    }

    [Test]
    public void Run_ReturnsZero_IfJsonFileHasNotBeenFound()
    {
        //***************** GIVEN
        List<string> args = ["-r='C:\\Temp\\Project'"];

        //***************** WHEN
        this._Manager.ParseArguments(args);

        //***************** ASSERT
        Assert.That(_Manager.IsInitialized, Is.True);
        Assert.Pass();
    }
}