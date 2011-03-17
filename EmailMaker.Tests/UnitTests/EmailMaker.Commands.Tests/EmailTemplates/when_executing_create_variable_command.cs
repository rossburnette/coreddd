﻿using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplate;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_variable_command
    {
        private EmailTemplate _emailTemplate;
        private int _htmlTemplatePartId;
        private int _htmlStartIndex;
        private int _length;
        private EmailTemplatePartDTO[] _emailTemplatePartDtos;
        private EmailTemplateDTO _emailTemplateDTO;

        [SetUp]
        public void Context()
        {
            _emailTemplate = MockRepository.GenerateMock<EmailTemplate>();
            
            var emailTemplateId = 23;
            var emailTemplateRepository = MockRepository.GenerateStub<IRepository<EmailTemplate>>();
            emailTemplateRepository.Stub(a => a.GetById(emailTemplateId)).Return(_emailTemplate);

            _htmlTemplatePartId = 47;
            _htmlStartIndex = 56;
            _length = 65;
            _emailTemplatePartDtos = new[]
                                         {
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Html, PartId = 45, Html = "html1"},
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Variable, PartId = 46, VariableValue = "value1"},
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Html, PartId = _htmlTemplatePartId, Html = "html2"},
                                         };
            _emailTemplateDTO = new EmailTemplateDTO
                                    {
                                        EmailTemplateId = emailTemplateId,
                                        Parts =
                                            _emailTemplatePartDtos
                                    };
            var command = new CreateVariableCommand
                              {
                                  EmailTemplate = _emailTemplateDTO,
                                  HtmlStartIndex = _htmlStartIndex,
                                  HtmlTemplatePartId = _htmlTemplatePartId,
                                  Length = _length
                              };
            var handler = new CreateVariableCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            _emailTemplate.AssertWasCalled(a => a.Update(_emailTemplateDTO));
        }

        [Test]
        public void create_variable_method_was_called()
        {
            _emailTemplate.AssertWasCalled(a => a.CreateVariable(_htmlTemplatePartId, _htmlStartIndex, _length));
        }
    
    }
}
