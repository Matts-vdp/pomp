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
        public void ShouldExecuteTest()
        {
            var offTime = 1;
            var onTime = 1;
            var amount = 1;
            var command = new RepeatedCommand(amount, offTime, onTime);

            var result = command.ShouldExecute(DateTime.Now);
            command.Execute();
            var result2 = command.ShouldExecute(DateTime.Now);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, result2);
        }

        [Test]
        public void ExecuteOnceTest()
        {
            var offTime = 1;
            var onTime = 1;
            var amount = 1;

            var command = new RepeatedCommand(amount, offTime, onTime);

            var result = command.Execute();
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, command.IsDone());

            var result2 = command.Execute();
            Assert.AreEqual(false, result2);
            Assert.AreEqual(true, command.IsDone());
        }

        [Test]
        public void ExecuteMoreTest()
        {
            var offTime = 1;
            var onTime = 1;
            var amount = 2;

            var command = new RepeatedCommand(amount, offTime, onTime);

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

        [TestCase(2, 1, 2, (2 + 1 + 2))]
        [TestCase(2, 1, 1, (2))]
        [TestCase(2, 1, 3, (2 + 1 + 2 + 1 + 2))]
        public void EndTimeCorrectTest(int onTime, int offTime, int amount, int totalSeconds)
        {
            var command = new RepeatedCommand(amount, offTime, onTime);
            var Correct = command.NextTime + TimeSpan.FromSeconds(totalSeconds);

            var result = command.EndTime;

            Assert.AreEqual(Correct, result);
        }
    }
}
