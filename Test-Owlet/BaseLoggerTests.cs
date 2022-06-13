using NUnit.Framework;
using Owlet.BaseClasses;
using System.Net.Sockets;
using System.Net;
using System;

namespace OwletTests
{
    public class BaseLoggerTests
    {
        private BaseLogger logger;
        private TcpListener server;

        [SetUp]
        public void Setup()
        {
            // Set the TcpListener on port 13000.
            int port = BaseNetworkWriter.StandardPort;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);
            server.Start();

            logger = BaseLogger.GetInstance();
        }
        [TearDown]
        public void Stop()
        {
            server.Stop();

            BaseLogger.ClearLogger();
        }

        [Test]
        public void CanCreateStorage()
        {
            var inputName = "data";
            var input = "this is data";
            logger.Log(inputName, input);

           
            Assert.IsNotEmpty(logger.NewestFile.FilePath);
        }

        [Test]
        public void CanLogDataToStorage()
        {
            var inputName = "data";
            var input = "this is data";

            logger.Log(inputName, input);
            logger.SaveLog();

            Assert.IsTrue(logger.NewestFile.ReadText().Contains(input));
        }

        [Test]
        public void NoSavedFileMeaningfulError()
        {
            TestDelegate ExceptionCall = () => logger.NewestFile.ReadText();

            Assert.Throws<Owlet.Exceptions.NoSavedLogException>(ExceptionCall);
        }

        [Test]
        public void NoBatchedFileMeaningfulError()
        {
            logger.DeleteAllLogs();
            TestDelegate ExceptionCall = () => logger.NewestFile.ReadText();

            Assert.Throws<Owlet.Exceptions.NoBatchedLogException>(ExceptionCall);
        }
    }
}