using NUnit.Framework;
using Owlet.BaseClasses;

namespace OwletTests
{
    public class BaseLoggerTests
    {
        private BaseLogger logger;

        [SetUp]
        public void Setup()
        {
            BaseLogger.ClearLogger();
            logger = BaseLogger.Instance;
        }

        [Test]
        public void CanLogDataToStorage()
        {
            var input = "this is data";
            logger.Log(input);
            Assert.AreEqual(input, logger.Batched[0]);
        }
    }
}