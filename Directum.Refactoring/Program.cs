using Directum.Refactoring;

try
{
    Console.WriteLine(XmlHelper.FindAttributeValue("test.xml", "type", "testAttr"));
    Console.WriteLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

try
{
    Console.WriteLine(XmlHelper.Func1("test.xml", "type", "testAttr"));
    Console.WriteLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

Console.ReadKey();