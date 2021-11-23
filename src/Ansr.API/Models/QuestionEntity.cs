using Microsoft.Azure.Cosmos.Table;
using System;


namespace ansr.API.Models
{
    public class QuestionEntity : TableEntity
    {
        public string Id { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public string Code { get; set; }
    }
}