using NUnit.Framework;
using PompServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PompServer.Test
{
    [TestFixture]
    public class RepeatedCommandTest
    {
        [Test]
        public void RepeatedCommandShouldExecuteTest()
        {
            var id = 1;
            var offTime = TimeSpan.FromMinutes(1);
            var onTime = TimeSpan.FromMinutes(1);
            var amount = 1;
            var command = new RepeatedCommand(id, offTime, onTime, amount);

            var result = command.ShouldExecute(DateTime.Now);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void RepeatedCommandExecuteOnceTest()
        {
            var id = 1;
            var offTime = TimeSpan.FromMinutes(1);
            var onTime = TimeSpan.FromMinutes(1);
            var amount = 1;

            var command = new RepeatedCommand(id, offTime, onTime, amount);

            var result = command.Execute();
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, command.IsDone());

            var result2 = command.Execute();
            Assert.AreEqual(false, result2);
            Assert.AreEqual(true, command.IsDone());
        }

        [Test]
        public void RepeatedCommandExecuteMoreTest()
        {
            var id = 1;
            var offTime = TimeSpan.FromMinutes(1);
            var onTime = TimeSpan.FromMinutes(1);
            var amount = 2;

            var command = new RepeatedCommand(id, offTime, onTime, amount);

            var result = command.Execute();
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, command.IsDone());

            var result2 = command.Execute();
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, command.IsDone());

            var result3 = command.Execute();
            Assert.AreEqual(true, result3);
            Assert.AreEqual(false, command.IsDone());

            var result4 = command.Execute();
            Assert.AreEqual(false, result4);
            Assert.AreEqual(true, command.IsDone());
        }
    }
}
