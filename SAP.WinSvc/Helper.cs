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

using SAP.Interfaces;
using SAP.Interfaces.Dtos;
using SAP.Process;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace SAP.WinSvc
{
    public static class Helper
    {
        /// <summary>
        /// processes a batch of specified size
        /// </summary>
        /// <param name="batchLimit"></param>
        /// <param name="retryFailed"></param>
    public static void ProcessBatch(int batchLimit, bool retryFailed = false)
    {
      Factory.Current.RegisterInterfaces();
      IEngine engine = Factory.Current.Engine();
      Trace.WriteLine("Calling eng.GetSentimentQueueForProcessing...");
      List<ISentimentQueueDto> queueForProcessing = engine.GetSentimentQueueForProcessing(batchLimit, retryFailed);
      int batchSize = queueForProcessing.Count<ISentimentQueueDto>();
      if (batchSize == 0)
        return;
      Trace.WriteLine("Calling eng.StartBatch...");
      ISentimentBatchDto sentimentBatch = engine.StartBatch(batchLimit, batchSize, DateTime.Now);
      IAnalyser analyser = Factory.Current.Analyse();
      string appSetting = ConfigurationManager.AppSettings["sentimentModelPath"];
      Trace.WriteLine("Calling analyser.Init...");
      analyser.Init(appSetting);
      Trace.WriteLine("Processing queue...");
      foreach (ISentimentQueueDto sentimentQueueDto in queueForProcessing)
      {
        try
        {
          sentimentQueueDto.BatchId = new int?(sentimentBatch.Id);
          sentimentQueueDto.Processed = true;
          sentimentQueueDto.DateProcessed = new DateTime?(DateTime.Now);
          sentimentQueueDto.Error = new bool?();
          ISentimentDto sentiment = analyser.GetSentiment(sentimentQueueDto);
          engine.SaveSentiment(sentiment);
        }
        catch (Exception ex)
        {
          sentimentQueueDto.Error = new bool?(true);
          ISentimentQueueErrorDto sentimentQueueErrorDto = (ISentimentQueueErrorDto) new SentimentQueueErrorDto()
          {
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            DateCreated = DateTime.Now
          };
          sentimentQueueDto.SentimentQueueErrors.Add(sentimentQueueErrorDto);
        }
        finally
        {
          engine.SaveSentimentQueueProcessingOutcome(sentimentQueueDto);
        }
      }
      sentimentBatch.DateFinish = new DateTime?(DateTime.Now);
      engine.FinishBatch(sentimentBatch);
    }
  }
}
