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


using SAP.DataModel;
using AutoMapper;
using SAP.Interfaces.Dtos;

namespace SAP.DataAccess
{
    /// <summary>
    /// data access for sentiments and adjustments
    /// </summary>
    public static class Sentiment
    {        
        /// <summary>
        /// saves a sentiment and its sentences
        /// </summary>
        /// <param name="sentimentDto"></param>
        public static void SaveSentiment(ISentimentDto sentimentDto)
        {
            using (SentimentEntities sentimentEntities = new SentimentEntities())
            {
                sentiment entity = Mapper.Map<ISentimentDto, sentiment>(sentimentDto, new sentiment());
                sentimentEntities.sentiments.Add(entity);
                sentimentEntities.SaveChanges();
            }
        }   
    }
}
