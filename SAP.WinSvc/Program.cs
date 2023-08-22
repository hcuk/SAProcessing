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
using System.Diagnostics;
using System.IO;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace SAP.WinSvc
{
    internal static class Program
    {
        private const string ApplicationName = "Sentiment Analysis Processor";
        private static void Main(string[] args)
        {
            Init();
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<SentimentQueueManager>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new SentimentQueueManager());
                    serviceConfigurator.WhenStarted(sentimentQueueManager => sentimentQueueManager.OnStart(args));
                    serviceConfigurator.WhenStopped(sentimentQueueManager => sentimentQueueManager.OnStop());
                });
                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.SetDescription(ApplicationName);
                hostConfigurator.SetDisplayName(ApplicationName);
                hostConfigurator.SetServiceName(ApplicationName);
            });
        }

        private static void Init()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "logs");
            if (!Directory.Exists(path))
            { 
                Directory.CreateDirectory(path); 
            }
            Trace.WriteLine("Starting Sentiment Service");
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private static void CurrentDomain_UnhandledException(
          object sender,
          UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine(e.ExceptionObject);
        }
    }
}
