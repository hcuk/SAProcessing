using System;
using System.Collections.Generic;

namespace SAP.Interfaces.Dtos
{
  public interface ISentimentQueueDto
  {
    int Id { get; set; }

    string TextForAnalysis { get; set; }

    DateTime DateCreated { get; set; }

    int? BatchId { get; set; }

    bool Processed { get; set; }

    DateTime? DateProcessed { get; set; }

    bool? Error { get; set; }

    List<ISentimentQueueErrorDto> SentimentQueueErrors { get; set; }
  }
}
