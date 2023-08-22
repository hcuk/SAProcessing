using System;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentSentenceDto
    {
        int Id { get; set; }
        int SentimentId { get; set; }
        string Text { get; set; }
        int Score { get; set; }
        DateTime DateCreated { get; set; }
    }
}
