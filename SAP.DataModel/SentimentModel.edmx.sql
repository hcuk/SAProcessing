
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/27/2017 14:07:02
-- Generated from EDMX file: C:\GIT Repos\envoy-messenger\SentimentAnalysis - Open Source\ESA.DataModel\SentimentModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SentimentAnalysisDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_sentiment_queue_batch_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sentiment_queue] DROP CONSTRAINT [FK_sentiment_queue_batch_id];
GO
IF OBJECT_ID(N'[dbo].[FK_sentiment_queue_error_queue_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sentiment_queue_error] DROP CONSTRAINT [FK_sentiment_queue_error_queue_id];
GO
IF OBJECT_ID(N'[dbo].[FK_sentiment_sentences_sentiment_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sentiment_sentences] DROP CONSTRAINT [FK_sentiment_sentences_sentiment_id];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sentiment_batch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sentiment_batch];
GO
IF OBJECT_ID(N'[dbo].[sentiment_queue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sentiment_queue];
GO
IF OBJECT_ID(N'[dbo].[sentiment_queue_error]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sentiment_queue_error];
GO
IF OBJECT_ID(N'[dbo].[sentiment_sentences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sentiment_sentences];
GO
IF OBJECT_ID(N'[dbo].[sentiments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sentiments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sentiment_batch'
CREATE TABLE [dbo].[sentiment_batch] (
    [id] int IDENTITY(1,1) NOT NULL,
    [batch_limit] int  NOT NULL,
    [batch_size] int  NOT NULL,
    [date_start] datetime  NOT NULL,
    [date_finish] datetime  NULL
);
GO

-- Creating table 'sentiment_queue'
CREATE TABLE [dbo].[sentiment_queue] (
    [id] int IDENTITY(1,1) NOT NULL,
    [text_for_analysis] nvarchar(612)  NOT NULL,
    [date_created] datetime  NULL,
    [batch_id] int  NULL,
    [processed] bit  NOT NULL,
    [date_processed] datetime  NULL,
    [error] bit  NULL
);
GO

-- Creating table 'sentiment_queue_error'
CREATE TABLE [dbo].[sentiment_queue_error] (
    [id] int IDENTITY(1,1) NOT NULL,
    [sentiment_queue_id] int  NOT NULL,
    [message] nvarchar(500)  NOT NULL,
    [stacktrace] nvarchar(4000)  NULL,
    [date_created] datetime  NOT NULL
);
GO

-- Creating table 'sentiment_sentences'
CREATE TABLE [dbo].[sentiment_sentences] (
    [id] int IDENTITY(1,1) NOT NULL,
    [sentiment_id] int  NOT NULL,
    [text] nvarchar(612)  NULL,
    [score] int  NOT NULL,
    [date_created] datetime  NOT NULL
);
GO

-- Creating table 'sentiments'
CREATE TABLE [dbo].[sentiments] (
    [id] int IDENTITY(1,1) NOT NULL,
    [date_created] datetime  NOT NULL,
    [average_score] decimal(18,2)  NULL,
    [sentiment_queue_id] int NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'sentiment_batch'
ALTER TABLE [dbo].[sentiment_batch]
ADD CONSTRAINT [PK_sentiment_batch]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'sentiment_queue'
ALTER TABLE [dbo].[sentiment_queue]
ADD CONSTRAINT [PK_sentiment_queue]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'sentiment_queue_error'
ALTER TABLE [dbo].[sentiment_queue_error]
ADD CONSTRAINT [PK_sentiment_queue_error]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'sentiment_sentences'
ALTER TABLE [dbo].[sentiment_sentences]
ADD CONSTRAINT [PK_sentiment_sentences]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'sentiments'
ALTER TABLE [dbo].[sentiments]
ADD CONSTRAINT [PK_sentiments]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [batch_id] in table 'sentiment_queue'
ALTER TABLE [dbo].[sentiment_queue]
ADD CONSTRAINT [FK_sentiment_queue_batch_id]
    FOREIGN KEY ([batch_id])
    REFERENCES [dbo].[sentiment_batch]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_sentiment_queue_batch_id'
CREATE INDEX [IX_FK_sentiment_queue_batch_id]
ON [dbo].[sentiment_queue]
    ([batch_id]);
GO

-- Creating foreign key on [sentiment_queue_id] in table 'sentiment_queue_error'
ALTER TABLE [dbo].[sentiment_queue_error]
ADD CONSTRAINT [FK_sentiment_queue_error_queue_id]
    FOREIGN KEY ([sentiment_queue_id])
    REFERENCES [dbo].[sentiment_queue]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_sentiment_queue_error_queue_id'
CREATE INDEX [IX_FK_sentiment_queue_error_queue_id]
ON [dbo].[sentiment_queue_error]
    ([sentiment_queue_id]);
GO

-- Creating foreign key on [sentiment_id] in table 'sentiment_sentences'
ALTER TABLE [dbo].[sentiment_sentences]
ADD CONSTRAINT [FK_sentiment_sentences_sentiment_id]
    FOREIGN KEY ([sentiment_id])
    REFERENCES [dbo].[sentiments]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_sentiment_sentences_sentiment_id'
CREATE INDEX [IX_FK_sentiment_sentences_sentiment_id]
ON [dbo].[sentiment_sentences]
    ([sentiment_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------