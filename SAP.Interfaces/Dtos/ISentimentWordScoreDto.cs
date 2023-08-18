using System;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentWordScoreDto
    {
        int Id { get; set; }
        string Word { get; set; }
        int Score { get; set; }
        DateTime DateCreated { get; set; }
        int CreatedBy { get; set; }
        DateTime? DateUpdated { get; set; }
        int? UpdatedBy { get; set; }
        bool Deleted { get; set; }
    }
}
