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
    public class CommandTest
    {
        [Test]
        public void CommandShouldExecuteTest()
        {
            var value = true;
            var command = new BasicCommand(value);
            var result = command.ShouldExecute(DateTime.Now);
            Assert.AreEqual(true, result);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CommandExecuteTest(bool value)
        {
            var command = new BasicCommand(value);
            var result = command.Execute();
            Assert.AreEqual(value, result);
        }

        [Test]
        public void CommandIsDoneTest()
        {
            var value = true;
            var command = new BasicCommand(value);

            var resultBefore = command.IsDone();
            command.Execute();
            var resultAfter = command.IsDone();

            Assert.AreEqual(false, resultBefore);
            Assert.AreEqual(true, resultAfter);
        }
    }
}
