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
using SAP.Map;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Timers;

namespace SAP.WinSvc
{
    internal class SentimentQueueManager
    {
        private static Timer _timer;

        public SentimentQueueManager() => Mappings.Setup();

        public void OnStart(string[] args)
        {
            SentimentQueueManager._timer = new Timer()
            {
                Interval = (double)Convert.ToInt32(ConfigurationManager.AppSettings["timerInterval"])
            };
            SentimentQueueManager._timer.Elapsed += new ElapsedEventHandler(SentimentQueueManager.ProcessBatch);
            SentimentQueueManager._timer.Start();
        }

        public void OnStop()
        {
            SentimentQueueManager._timer.Stop();
            SentimentQueueManager._timer.Dispose();
        }

        private static void ProcessBatch(object source, ElapsedEventArgs e)
        {
            Trace.WriteLine("Processing Batch...");
            SentimentQueueManager._timer.Stop();
            try
            {
                int int32 = Convert.ToInt32(ConfigurationManager.AppSettings["batchLimit"]);
                bool flag = false;
                if (ConfigurationManager.AppSettings["retryFailed"] != null)
                    flag = bool.Parse(ConfigurationManager.AppSettings["retryFailed"]);
                int num = flag ? 1 : 0;
                Helper.ProcessBatch(int32, num != 0);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error processing sentiments: [{0}]", (object)ex.ToString()));
            }
            SentimentQueueManager._timer.Start();
            Trace.WriteLine("Batch Processing Ended.");
        }
    }
}
