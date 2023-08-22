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
using System.Diagnostics;
using System.Timers;
using System.Configuration;

namespace SAP.WinSvc
{
    internal class SentimentQueueManager
    {

        private static Timer _timer;

        public SentimentQueueManager() => Mappings.Setup();

        public void OnStart(string[] args)
        {
            _timer = new Timer { Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timerInterval"]) };
            _timer.Elapsed += ProcessBatch;
            _timer.Start();
        }

        public void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private static void ProcessBatch(object source, ElapsedEventArgs e)
        {
            Trace.WriteLine("Processing Batch...");
            _timer.Stop();

            try
            {
                var batchLimit = Convert.ToInt32(ConfigurationManager.AppSettings["batchLimit"]);
                var retryFailed = false;
                if (ConfigurationManager.AppSettings["retryFailed"] != null)
                {
                    retryFailed = bool.Parse(ConfigurationManager.AppSettings["retryFailed"]);
                }
                Helper.ProcessBatch(batchLimit, retryFailed);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error processing sentiments: [{0}]", ex.ToString()));
            }

            _timer.Start();
            Trace.WriteLine("Batch Processing Ended.");
        }
    }
}
