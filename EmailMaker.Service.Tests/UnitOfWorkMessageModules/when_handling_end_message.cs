﻿using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    [TestFixture]
    public class when_handling_end_message : UnitOfWorkMessageModuleWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            Module.HandleEndMessage();
        }

        [Test]
        public void unit_of_work_commits()
        {
            A.CallTo(() => UnitOfWork.Commit()).MustHaveHappened();
        }
    }
}