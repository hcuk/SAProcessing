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
using AutoMapper.Mappers;
using SAP.DataModel;
using SAP.Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SAP.Map
{
    /// <summary>
    /// automapping for dtos to database objects and vice versa
    /// </summary>
    public static class Mappings
    {
        /// <summary>
        /// setups up map configurations for AutoMapper
        /// </summary>
        public static void Setup()
        {
      ListSourceMapper listSourceMapper = new ListSourceMapper();
      Mapper.CreateMap<sentiment_batch, ISentimentBatchDto>()
		.ForMember((Expression<Func<ISentimentBatchDto, object>>) (dest => (object) dest.Id), (Action<IMemberConfigurationExpression<sentiment_batch>>) (src => src.MapFrom<int>((Expression<Func<sentiment_batch, int>>) (p => p.id))))
		.ForMember((Expression<Func<ISentimentBatchDto, object>>) (dest => (object) dest.BatchLimit), (Action<IMemberConfigurationExpression<sentiment_batch>>) (src => src.MapFrom<int>((Expression<Func<sentiment_batch, int>>) (p => p.batch_limit))))
		.ForMember((Expression<Func<ISentimentBatchDto, object>>) (dest => (object) dest.BatchSize), (Action<IMemberConfigurationExpression<sentiment_batch>>) (src => src.MapFrom<int>((Expression<Func<sentiment_batch, int>>) (p => p.batch_size))))
		.ForMember((Expression<Func<ISentimentBatchDto, object>>) (dest => (object) dest.DateStart), (Action<IMemberConfigurationExpression<sentiment_batch>>) (src => src.MapFrom<DateTime>((Expression<Func<sentiment_batch, DateTime>>) (p => p.date_start))))
		.ForMember((Expression<Func<ISentimentBatchDto, object>>) (dest => (object) dest.DateFinish), (Action<IMemberConfigurationExpression<sentiment_batch>>) (src => src.MapFrom<DateTime?>((Expression<Func<sentiment_batch, DateTime?>>) (p => p.date_finish))));
      Mapper.CreateMap<sentiment_queue, ISentimentQueueDto>()
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.Id), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<int>((Expression<Func<sentiment_queue, int>>) (p => p.id))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => dest.TextForAnalysis), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<string>((Expression<Func<sentiment_queue, string>>) (p => p.text_for_analysis))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.DateCreated), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<DateTime?>((Expression<Func<sentiment_queue, DateTime?>>) (p => p.date_created))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.BatchId), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<int?>((Expression<Func<sentiment_queue, int?>>) (p => p.batch_id))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.Processed), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<bool>((Expression<Func<sentiment_queue, bool>>) (p => p.processed))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.DateProcessed), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<DateTime?>((Expression<Func<sentiment_queue, DateTime?>>) (p => p.date_processed))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => (object) dest.Error), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<bool?>((Expression<Func<sentiment_queue, bool?>>) (p => p.error))))
		.ForMember((Expression<Func<ISentimentQueueDto, object>>) (dest => dest.SentimentQueueErrors), (Action<IMemberConfigurationExpression<sentiment_queue>>) (src => src.MapFrom<ICollection<sentiment_queue_error>>((Expression<Func<sentiment_queue, ICollection<sentiment_queue_error>>>) (p => p.sentiment_queue_error))));
      Mapper.CreateMap<sentiment_queue_error, ISentimentQueueErrorDto>()
		.ForMember((Expression<Func<ISentimentQueueErrorDto, object>>) (dest => (object) dest.Id), (Action<IMemberConfigurationExpression<sentiment_queue_error>>) (src => src.MapFrom<int>((Expression<Func<sentiment_queue_error, int>>) (p => p.id))))
		.ForMember((Expression<Func<ISentimentQueueErrorDto, object>>) (dest => (object) dest.SentimentQueueId), (Action<IMemberConfigurationExpression<sentiment_queue_error>>) (src => src.MapFrom<int>((Expression<Func<sentiment_queue_error, int>>) (p => p.sentiment_queue_id))))
		.ForMember((Expression<Func<ISentimentQueueErrorDto, object>>) (dest => dest.Message), (Action<IMemberConfigurationExpression<sentiment_queue_error>>) (src => src.MapFrom<string>((Expression<Func<sentiment_queue_error, string>>) (p => p.message))))
		.ForMember((Expression<Func<ISentimentQueueErrorDto, object>>) (dest => dest.StackTrace), (Action<IMemberConfigurationExpression<sentiment_queue_error>>) (src => src.MapFrom<string>((Expression<Func<sentiment_queue_error, string>>) (p => p.stacktrace))))
		.ForMember((Expression<Func<ISentimentQueueErrorDto, object>>) (dest => (object) dest.DateCreated), (Action<IMemberConfigurationExpression<sentiment_queue_error>>) (src => src.MapFrom<DateTime>((Expression<Func<sentiment_queue_error, DateTime>>) (p => p.date_created))));
      Mapper.CreateMap<sentiment, ISentimentDto>()
		.ForMember((Expression<Func<ISentimentDto, object>>) (dest => (object) dest.Id), (Action<IMemberConfigurationExpression<sentiment>>) (src => src.MapFrom<int>((Expression<Func<sentiment, int>>) (p => p.id))))
		.ForMember((Expression<Func<ISentimentDto, object>>) (dest => (object) dest.DateCreated), (Action<IMemberConfigurationExpression<sentiment>>) (src => src.MapFrom<DateTime>((Expression<Func<sentiment, DateTime>>) (p => p.date_created))))
		.ForMember((Expression<Func<ISentimentDto, object>>) (dest => (object) dest.AverageScore), (Action<IMemberConfigurationExpression<sentiment>>) (src => src.MapFrom<Decimal?>((Expression<Func<sentiment, Decimal?>>) (p => p.average_score))))
		.ForMember((Expression<Func<ISentimentDto, object>>) (dest => (object) dest.SentimentQueueID), (Action<IMemberConfigurationExpression<sentiment>>) (src => src.MapFrom<int>((Expression<Func<sentiment, int>>) (p => p.sentiment_queue_id))))
		.ForMember((Expression<Func<ISentimentDto, object>>) (dest => dest.SentimentSentences), (Action<IMemberConfigurationExpression<sentiment>>) (src => src.MapFrom<ICollection<sentiment_sentences>>((Expression<Func<sentiment, ICollection<sentiment_sentences>>>) (p => p.sentiment_sentences))));
      Mapper.CreateMap<sentiment_sentences, ISentimentSentenceDto>()
		.ForMember((Expression<Func<ISentimentSentenceDto, object>>) (dest => (object) dest.Id), (Action<IMemberConfigurationExpression<sentiment_sentences>>) (src => src.MapFrom<int>((Expression<Func<sentiment_sentences, int>>) (p => p.id))))
		.ForMember((Expression<Func<ISentimentSentenceDto, object>>) (dest => (object) dest.SentimentId), (Action<IMemberConfigurationExpression<sentiment_sentences>>) (src => src.MapFrom<int>((Expression<Func<sentiment_sentences, int>>) (p => p.sentiment_id))))
		.ForMember((Expression<Func<ISentimentSentenceDto, object>>) (dest => dest.Text), (Action<IMemberConfigurationExpression<sentiment_sentences>>) (src => src.MapFrom<string>((Expression<Func<sentiment_sentences, string>>) (p => p.text))))
		.ForMember((Expression<Func<ISentimentSentenceDto, object>>) (dest => (object) dest.Score), (Action<IMemberConfigurationExpression<sentiment_sentences>>) (src => src.MapFrom<int>((Expression<Func<sentiment_sentences, int>>) (p => p.score))))
		.ForMember((Expression<Func<ISentimentSentenceDto, object>>) (dest => (object) dest.DateCreated), (Action<IMemberConfigurationExpression<sentiment_sentences>>) (src => src.MapFrom<DateTime>((Expression<Func<sentiment_sentences, DateTime>>) (p => p.date_created))));
      Mapper.CreateMap<ISentimentDto, sentiment>()
		.ForMember((Expression<Func<sentiment, object>>) (dest => (object) dest.id), (Action<IMemberConfigurationExpression<ISentimentDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentDto, int>>) (p => p.Id))))
		.ForMember((Expression<Func<sentiment, object>>) (dest => (object) dest.date_created), (Action<IMemberConfigurationExpression<ISentimentDto>>) (src => src.MapFrom<DateTime>((Expression<Func<ISentimentDto, DateTime>>) (p => p.DateCreated))))
		.ForMember((Expression<Func<sentiment, object>>) (dest => (object) dest.average_score), (Action<IMemberConfigurationExpression<ISentimentDto>>) (src => src.MapFrom<Decimal?>((Expression<Func<ISentimentDto, Decimal?>>) (p => p.AverageScore))))
		.ForMember((Expression<Func<sentiment, object>>) (dest => (object) dest.sentiment_queue_id), (Action<IMemberConfigurationExpression<ISentimentDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentDto, int>>) (p => p.SentimentQueueID))))
		.ForMember((Expression<Func<sentiment, object>>) (dest => dest.sentiment_sentences), (Action<IMemberConfigurationExpression<ISentimentDto>>) (src => src.MapFrom<List<ISentimentSentenceDto>>((Expression<Func<ISentimentDto, List<ISentimentSentenceDto>>>) (p => p.SentimentSentences))));
      Mapper.CreateMap<ISentimentSentenceDto, sentiment_sentences>()
		.ForMember((Expression<Func<sentiment_sentences, object>>) (dest => (object) dest.id), (Action<IMemberConfigurationExpression<ISentimentSentenceDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentSentenceDto, int>>) (p => p.Id))))
		.ForMember((Expression<Func<sentiment_sentences, object>>) (dest => (object) dest.sentiment_id), (Action<IMemberConfigurationExpression<ISentimentSentenceDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentSentenceDto, int>>) (p => p.SentimentId))))
		.ForMember((Expression<Func<sentiment_sentences, object>>) (dest => dest.text), (Action<IMemberConfigurationExpression<ISentimentSentenceDto>>) (src => src.MapFrom<string>((Expression<Func<ISentimentSentenceDto, string>>) (p => p.Text))))
		.ForMember((Expression<Func<sentiment_sentences, object>>) (dest => (object) dest.score), (Action<IMemberConfigurationExpression<ISentimentSentenceDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentSentenceDto, int>>) (p => p.Score))))
		.ForMember((Expression<Func<sentiment_sentences, object>>) (dest => (object) dest.date_created), (Action<IMemberConfigurationExpression<ISentimentSentenceDto>>) (src => src.MapFrom<DateTime>((Expression<Func<ISentimentSentenceDto, DateTime>>) (p => p.DateCreated))));
      Mapper.CreateMap<ISentimentQueueErrorDto, sentiment_queue_error>()
		.ForMember((Expression<Func<sentiment_queue_error, object>>) (dest => (object) dest.id), (Action<IMemberConfigurationExpression<ISentimentQueueErrorDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentQueueErrorDto, int>>) (p => p.Id))))
		.ForMember((Expression<Func<sentiment_queue_error, object>>) (dest => (object) dest.sentiment_queue_id), (Action<IMemberConfigurationExpression<ISentimentQueueErrorDto>>) (src => src.MapFrom<int>((Expression<Func<ISentimentQueueErrorDto, int>>) (p => p.SentimentQueueId))))
		.ForMember((Expression<Func<sentiment_queue_error, object>>) (dest => dest.message), (Action<IMemberConfigurationExpression<ISentimentQueueErrorDto>>) (src => src.MapFrom<string>((Expression<Func<ISentimentQueueErrorDto, string>>) (p => p.Message))))
		.ForMember((Expression<Func<sentiment_queue_error, object>>) (dest => dest.stacktrace), (Action<IMemberConfigurationExpression<ISentimentQueueErrorDto>>) (src => src.MapFrom<string>((Expression<Func<ISentimentQueueErrorDto, string>>) (p => p.StackTrace))))
		.ForMember((Expression<Func<sentiment_queue_error, object>>) (dest => (object) dest.date_created), (Action<IMemberConfigurationExpression<ISentimentQueueErrorDto>>) (src => src.MapFrom<DateTime>((Expression<Func<ISentimentQueueErrorDto, DateTime>>) (p => p.DateCreated))));

        }
    }
}
