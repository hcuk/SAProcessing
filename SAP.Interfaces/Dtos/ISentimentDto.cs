using System;
using System.Collections.Generic;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentDto
    {
        int Id { get; set; }
        int SentimentQueueID { get; set; }
        DateTime DateCreated { get; set; }
        decimal? AverageScore { get; set; }
        List<ISentimentSentenceDto> SentimentSentences { get; set; }
    }
}
