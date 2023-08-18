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

using AutoMapper;
using SAP.DataModel;
using SAP.Dtos;
using SAP.Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SAP.DataAccess
{
    /// <summary>
    /// data access for queue processing
    /// </summary>
    public static class Queue
    {

        //TODO: refactor with unit of work so context is shared between data access classes
        //private static SentimentEntities _db = new SentimentEntities();
        //for now just wrap with using statments to ensure data is not stale
        //i.e. using (var db = new SentimentEntities()) 
        
        /// <summary>
        /// creates a new sentiment batch and returns it
        /// </summary>
        /// <param name="batchLimit"></param>
        /// <param name="batchSize"></param>
        /// <param name="dateStart"></param>
        /// <returns></returns>
        public static ISentimentBatchDto StartBatch(int batchLimit, int batchSize, DateTime dateStart)//TODO: refactor params to Interface
        {
            using (SentimentEntities sentimentEntities = new SentimentEntities())
            {
                sentiment_batch sentimentBatch = new sentiment_batch()
                {
                    batch_limit = batchLimit,
                    batch_size = batchSize,
                    date_start = dateStart
                };
                sentimentEntities.sentiment_batch.Add(sentimentBatch);
                sentimentEntities.SaveChanges();
                ISentimentBatchDto destination = (ISentimentBatchDto)new SentimentBatchDto();
                return Mapper.Map<sentiment_batch, ISentimentBatchDto>(sentimentBatch, destination);
            }
        }

        /// <summary>
        /// updates the finish date of the batch passed in 
        /// </summary>
        /// <param name="sentimentBatch"></param>
        public static void FinishBatch(ISentimentBatchDto sentimentBatch)
        {
            using (SentimentEntities sentimentEntities = new SentimentEntities())
            {
                sentimentEntities.sentiment_batch.Find(new object[1]
                {
          (object) sentimentBatch.Id
                }).date_finish = sentimentBatch.DateFinish;
                sentimentEntities.SaveChanges();
            }
        }

        public static List<ISentimentQueueDto> GetSentimentQueueForProcessing(
          int batchSize,
          bool retryFailed = false)
        {
            using (SentimentEntities sentimentEntities = new SentimentEntities())
            {
                List<sentiment_queue> sentimentQueueList = new List<sentiment_queue>();
                List<sentiment_queue> list;
                if (!retryFailed)
                    list = sentimentEntities.sentiment_queue.OrderBy<sentiment_queue, int>((Expression<Func<sentiment_queue, int>>)(x => x.id)).Where<sentiment_queue>((Expression<Func<sentiment_queue, bool>>)(x => x.processed == false)).Take<sentiment_queue>(batchSize).ToList<sentiment_queue>();
                else
                    list = sentimentEntities.sentiment_queue.OrderBy<sentiment_queue, int>((Expression<Func<sentiment_queue, int>>)(x => x.id)).Where<sentiment_queue>((Expression<Func<sentiment_queue, bool>>)(x => x.processed == false || x.error == (bool?)true)).Take<sentiment_queue>(batchSize).ToList<sentiment_queue>();
                List<ISentimentQueueDto> destination = new List<ISentimentQueueDto>();
                return Mapper.Map<List<sentiment_queue>, List<ISentimentQueueDto>>(list, destination);
            }
        }

        /// <summary>
        /// saves the processing outcome of a sentiment queue item
        /// </summary>
        /// <param name="sentimentQueue"></param>
        public static void SaveSentimentQueueProcessingOutcome(ISentimentQueueDto sentimentQueue)
        {
            using (SentimentEntities sentimentEntities = new SentimentEntities())
            {
                sentiment_queue sentimentQueue1 = sentimentEntities.sentiment_queue.Find(new object[1]
                {
          (object) sentimentQueue.Id
                });
                sentimentQueue1.batch_id = sentimentQueue.BatchId;
                sentimentQueue1.processed = sentimentQueue.Processed;
                sentimentQueue1.date_processed = sentimentQueue.DateProcessed;
                sentimentQueue1.error = sentimentQueue.Error;
                sentimentQueue1.sentiment_queue_error = (ICollection<sentiment_queue_error>)Mapper.Map<List<ISentimentQueueErrorDto>, List<sentiment_queue_error>>(sentimentQueue.SentimentQueueErrors, new List<sentiment_queue_error>());
                sentimentEntities.SaveChanges();
            }
        }       
    }
}
