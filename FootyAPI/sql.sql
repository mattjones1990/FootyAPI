DROP TABLE Match
CREATE TABLE [dbo].[Match](
	[MatchID] [int] IDENTITY(1,1) NOT NULL,
	[League] NVARCHAR(50) NOT NULL,
	[HomeTeam] [nvarchar](50) NOT NULL,
	[AwayTeam] [nvarchar](50) NOT NULL,
	CommenceTime DATETIME2 NOT NULL,
	LastUpdated DATETIME2 not null,
	[PinnacleHome] [Decimal](5,2)  NULL,
	[PinnacleDraw] [Decimal](5,2)  NULL,
	[PinnacleAway] [Decimal](5,2)  NULL,
	[VigFreeHome] [Decimal](5,2)  NULL,
	[VigFreeDraw] [Decimal](5,2) NULL,
	[VigFreeAway] [Decimal](5,2) NULL,
	[BetfairHome] [Decimal](5,2)  NULL,
	[BetfairDraw] [Decimal](5,2)  NULL,
	[BetfairAway] [Decimal](5,2)  NULL,
	[ValueHome] [Decimal](5,2)  NULL,
	[ValueDraw] [Decimal](5,2)  NULL,
	[ValueAway] [Decimal](5,2)  NULL,

	Completed BIT NOT NULL
	)