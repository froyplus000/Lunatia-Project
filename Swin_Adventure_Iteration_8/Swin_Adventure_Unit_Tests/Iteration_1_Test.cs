using Swin_Adventure_Iteration_8;
namespace TestIdentifiableObject;

public class Iteration_1_Test
{

    private IdentifiableObject obj;

    [SetUp]
    public void Setup()
    {
        obj = new IdentifiableObject(new string[] { "001", "Pattarapol", "Tantechasa" });
    }

    [Test]
    public void TestAreYou()
    {
        bool isPattarapol = obj.AreYou("pattarapol");
        bool isTantechasa = obj.AreYou("tantechasa");
        Assert.That(isPattarapol, Is.True);
        Assert.That(isTantechasa, Is.True);
    }

    [Test]
    public void TestNotAreYou()
    {
        bool isJames = obj.AreYou("james");
        bool isBond = obj.AreYou("bond");
        Assert.That(isJames, Is.False);
        Assert.That(isBond, Is.False);
    }

    [Test]
    public void TestCaseSensitive()
    {
        bool isPattarapol = obj.AreYou("PATTARAPOL");
        bool isTantechasa = obj.AreYou("TanTeChAsA");
        Assert.That(isPattarapol, Is.True);
        Assert.That(isTantechasa, Is.True);
    }

    [Test]
    public void TestFirstID()
    {
        Assert.That("001", Is.EqualTo(obj.FirstId));
    }

    [Test]
    public void TestFirstIDWithNoIDs()
    {
        IdentifiableObject noIds = new IdentifiableObject(new string[] {  });
        string expectedResult = "";
        Assert.That(noIds.FirstId, Is.EqualTo(expectedResult));
    }
    [Test]
    public void TestAddID()
    {
        obj.AddIdentifier("student"); // add "student"

        // Check FirstID value stay the same ("001")
        string expectedResult = "001";
        Assert.That(obj.FirstId, Is.EqualTo(expectedResult));

        // Check AreYou with other id
        bool isStudent = obj.AreYou("student");
        bool isPattarapol = obj.AreYou("pattarapol");
        bool isTantechasa = obj.AreYou("tantechasa");
        Assert.That(isStudent, Is.True);
        Assert.That(isPattarapol, Is.True);
        Assert.That(isTantechasa, Is.True);

    }

    [Test]
    public void TestPrivilegeEscalation()
    {
        obj.PrivilegeEscalation("3220");
        Assert.That("25", Is.EqualTo(obj.FirstId));
    }
}
