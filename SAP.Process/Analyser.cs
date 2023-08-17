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

using edu.stanford.nlp.ling;
using edu.stanford.nlp.neural.rnn;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.sentiment;
using edu.stanford.nlp.trees;
using Elmah;
using java.lang;
using java.util;
using SAP.Dtos;
using SAP.Interfaces;
using SAP.Interfaces.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace SAP.Process
{
    /// <summary>
    /// sentiment analyser 
    /// </summary>
    public class Analyser : IAnalyser
    {
        private StanfordCoreNLP Pipeline { get; set; }
        private Properties Properties { get; set; }

        /// <summary>
        /// init stanford CoreNLP properties
        /// </summary>
        /// <param name="sentimentModelPath"></param>// Path to the folder with models extracted from `stanford-corenlp-3.5.1-models.jar`
    public void Init(string sentimentModelPath)
    {
      try
      {
        Trace.WriteLine("Initialising analyser...");
        this.Properties = new Properties();
        this.Properties.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref, sentiment, parse");
        this.Properties.setProperty("sutime.binders", "0");
        Trace.WriteLine("Setting properties for Analyser...");
        Directory.SetCurrentDirectory(sentimentModelPath);
        this.Pipeline = new StanfordCoreNLP(this.Properties);
        Trace.WriteLine("Initialising analyser complete.");
      }
      catch (System.Exception ex)
      {
        Trace.WriteLine(string.Format("Error initialising analyser: {0}", (object) ex.Message));
        ErrorLog.GetDefault((HttpContext) null).Log(new Elmah.Error(ex));
        throw;
      }
    }

        /// <summary>
        /// returns an overall sentiment score with the component sentences and individual scores
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
    public ISentimentDto GetSentiment(ISentimentQueueDto sentimentQueueItem)
    {
      try
      {
        ISentimentDto sentiment = (ISentimentDto) new SentimentDto()
        {
          SentimentSentences = new List<ISentimentSentenceDto>(),
          DateCreated = System.DateTime.Now,
          SentimentQueueID = sentimentQueueItem.Id
        };
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        if (!string.IsNullOrEmpty(sentimentQueueItem.TextForAnalysis))
        {
          if (this.Pipeline.process(sentimentQueueItem.TextForAnalysis.ToLower()).get((Class) typeof (CoreAnnotations.SentencesAnnotation)) is java.util.ArrayList arrayList)
          {
            foreach (Annotation annotation in (IEnumerable) arrayList)
            {
              num1 += 1M;
              int predictedClass = RNNCoreAnnotations.getPredictedClass((Tree) annotation.get((Class) typeof (SentimentCoreAnnotations.AnnotatedTree)));
              num2 += (Decimal) predictedClass;
              sentiment.SentimentSentences.Add((ISentimentSentenceDto) new SentimentSentenceDto()
              {
                Text = annotation.ToString(),
                Score = predictedClass,
                DateCreated = System.DateTime.Now
              });
            }
          }
          sentiment.AverageScore = new Decimal?(num2 / num1);
        }
        return sentiment;
      }
      catch (System.Exception ex)
      {
        Trace.WriteLine(string.Format("Error analysing sentiment for sentimentId: {0} - error: {1}", (object) sentimentQueueItem.Id, (object) ex.Message));
        ErrorLog.GetDefault((HttpContext) null).Log(new Elmah.Error(ex));
        throw ex;
      }
    }
  }
}
