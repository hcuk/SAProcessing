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
using Elmah;
using SAP.DataAccess;
using SAP.Interfaces;
using SAP.Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace SAP.Process
{
    /// <summary>
    /// returns data from dataaccess project and performs any pre-processing
    /// </summary>
    public class Engine : IEngine
    {
        public ISentimentBatchDto StartBatch(int batchLimit, int batchSize, DateTime dateStart)
        {
            try
            {
                return Queue.StartBatch(batchLimit, batchSize, dateStart);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("StartBatch exception: {0}", (object)ex.Message));
                ErrorLog.GetDefault((HttpContext)null).Log(new Error(ex));
                throw ex;
            }
        }

        public void FinishBatch(ISentimentBatchDto sentimentBatch)
        {
            try
            {
                Queue.FinishBatch(sentimentBatch);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("FinishBatch exception: {0}", (object)ex.Message));
                ErrorLog.GetDefault((HttpContext)null).Log(new Error(ex));
                throw ex;
            }
        }

        public List<ISentimentQueueDto> GetSentimentQueueForProcessing(int batchSize, bool retryFailed= false)
        {
            try
            {
                return Queue.GetSentimentQueueForProcessing(batchSize, retryFailed);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("GetSentimentQueueForProcessing exception: {0}", (object)ex.Message));
                ErrorLog.GetDefault((HttpContext)null).Log(new Error(ex));
                throw ex;
            }
        }

        public void SaveSentimentQueueProcessingOutcome(ISentimentQueueDto sentimentQueue)
        {
            try
            {
                Queue.SaveSentimentQueueProcessingOutcome(sentimentQueue);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("SaveSentimentQueueProcessingOutcome exception: {0}", (object)ex.Message));
                ErrorLog.GetDefault((HttpContext)null).Log(new Error(ex));
                throw ex;
            }
        }
        
        public void SaveSentiment(ISentimentDto sentiment)
        {
            try
            {
                Sentiment.SaveSentiment(sentiment);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("SaveSentiment exception: {0}", (object)ex.Message));
                ErrorLog.GetDefault((HttpContext)null).Log(new Error(ex));
                throw ex;
            }
        }
    }
}
