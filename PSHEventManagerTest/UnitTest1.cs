using LiteDB;

namespace PSHEventManagerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLitedb()
        {
            String dbPath = "./ldb/TextFile1.txt";
            string str = File.ReadAllText(dbPath);
            Console.WriteLine(str);
        }
    }
}