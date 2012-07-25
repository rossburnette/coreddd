﻿using System.Collections.Generic;
using System.Linq;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_email_template : base_simple_persistence_test
    {
        private EmailTemplate _emailTemplate;
        private IEnumerable<EmailTemplateDto> _result;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = new EmailTemplate("html", "name", user.Id);
            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(_emailTemplate, anotherEmailTemplate);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailTemplateQuery();
            _result = query.Execute<EmailTemplateDto>(new GetEmailTemplateQueryMessage {EmailTemplateId = _emailTemplate.Id});
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _result.Count().ShouldBe(1);
            var retrievedEmailTemplateDto = _result.First();
            retrievedEmailTemplateDto.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            retrievedEmailTemplateDto.Name.ShouldBe(_emailTemplate.Name);
        }
    }
}