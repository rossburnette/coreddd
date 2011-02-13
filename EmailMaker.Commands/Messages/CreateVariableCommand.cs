﻿using EmailMaker.Web.DTO.EmailTemplate;

namespace EmailMaker.Commands.Messages
{
    public class CreateVariableCommand
    {
        public int HtmlTemplatePartId { get; set; }
        public int HtmlStartIndex { get; set; }
        public int Length { get; set; }
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}
