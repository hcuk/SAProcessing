using System;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentModelLoggingDto
    {
        int Id { get; set; }
        int SentimentModelId { get; set; }
        int SentimentTrainingId { get; set; }
        DateTime DateCreated { get; set; }
    }
}
