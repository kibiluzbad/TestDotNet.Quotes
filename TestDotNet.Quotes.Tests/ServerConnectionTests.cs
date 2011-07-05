using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestDotNet.Quotes.Tests
{
    [TestFixture]
    public class ServerConnectionTests
    {
        [Test]
        public void Can_Connect_To_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            var serverConnected = connection.Connect("127.0.0.1", 34567);

            Assert.That(serverConnected, Is.True);
        }

        [Test]
        public void Can_Disconnect_Form_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);
            var serverDisconnected = connection.Disconnect();

            Assert.That(serverDisconnected, Is.True);
        }

        [Test]
        public void Can_Login_To_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);

            var loggedIn = connection.Login("trademaker01", "t01m975kt");

            connection.Disconnect();

            Assert.That(loggedIn, Is.True);
        }

        [Test]
        public void Can_Logout_From_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);

            connection.Login("trademaker01", "t01m975kt");
            var loggedOut = connection.Logout();

            connection.Disconnect();

            Assert.That(loggedOut, Is.True);
        }

        [Test]
        public void Can_GetQuote_Form_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);

            connection.Login("trademaker01", "t01m975kt");

            var quote = new ClientLibrary.Quote();
            var success = connection.GetQuote("PETR4", out quote);
            
            connection.Logout();

            connection.Disconnect();

            Assert.That(success, Is.True);
            Assert.That(quote, Is.Not.Null);
            Assert.That(quote.Object, Is.EqualTo("PETR4"));
        }

        [Test]
        public void Can_StartQuoteStreaming_Form_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);

            connection.Login("trademaker01", "t01m975kt");

            var success = connection.StartQuoteStreaming("PETR4");

            connection.Logout();

            connection.Disconnect();

            Assert.That(success, Is.True);
        }

        [Test]
        public void Can_StopQuoteStreaming_Form_Server()
        {
            // Don't forget to run packages\ClientAPI\Server.exe
            var connection = new ClientAPI.ClientConnection();
            connection.Connect("127.0.0.1", 34567);

            connection.Login("trademaker01", "t01m975kt");

            connection.StartQuoteStreaming("PETR4");
            var success = connection.StopQuoteStreaming("PETR4");

            connection.Logout();

            connection.Disconnect();

            Assert.That(success, Is.True);
        }
    }
}
