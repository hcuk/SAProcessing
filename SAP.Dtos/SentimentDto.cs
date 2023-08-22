/*
    Copyright 2016 Healthcare Communications UK Ltd
 
    This file is part of HCSentimentAnalysisProcessor.

    HCSentimentAnalysisProcessor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HCSentimentAnalysisProcessor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HCSentimentAnalysisProcessor.  If not, see <http://www.gnu.org/licenses/>.

 */

 using System;
using System.Collections.Generic;
using SAP.Interfaces.Dtos;

namespace SAP.Dtos
{
    public class SentimentDto : ISentimentDto
    {
        public int Id { get; set; }        
        public DateTime DateCreated { get; set; }
        public decimal? AverageScore { get; set; } //TODO: made this nullable for now, the DataModel needs to be updated to set AverageScore NOT NULL
        public List<ISentimentSentenceDto> SentimentSentences { get; set; }
        public int SentimentQueueID { get; set; }
    }
}
