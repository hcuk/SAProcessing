using System;

namespace SAP.Interfaces.Dtos
{
  public interface ISentimentQueueErrorDto
  {
    int Id { get; set; }

    int SentimentQueueId { get; set; }

    string Message { get; set; }

    string StackTrace { get; set; }

    DateTime DateCreated { get; set; }
  }
}
