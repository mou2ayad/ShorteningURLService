/****** Object:  Table [dbo].[KGSRounds]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KGSRounds](
	[RoundId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FromKey] [varchar](6) NOT NULL,
	[ToKey] [varchar](6) NOT NULL,
	[LastCounter] [varchar](50) NOT NULL,
	[RoundDate] [datetime] NOT NULL,
 CONSTRAINT [PK_KGSRounds] PRIMARY KEY CLUSTERED 
(
	[RoundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urls]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urls](
	[KeyId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ShortKey] [varchar](6) NOT NULL,
	[FullUrl] [nvarchar](225) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastReadingDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Url] PRIMARY KEY CLUSTERED 
(
	[KeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CreateNewUrl]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateNewUrl]
	@Shortkey varchar(6), @FullUrl nvarchar(225)
AS
BEGIN
	
	SET NOCOUNT ON;    
	declare @KeyId numeric(18,0);
	declare @ExShortKey varchar(6);
	declare @CreationDate Datetime;
	declare @LastReadingDate Datetime;
	declare @nowdate datetime= GETDATE();
	select @KeyId=KeyId,@ExShortKey=ShortKey,@CreationDate=[CreationDate],@LastReadingDate=LastReadingDate from [dbo].[Urls] with (nolock)
		where [FullUrl]=@FullUrl

	if(@KeyId is null)
	begin
		insert into [dbo].[Urls]([ShortKey],[FullUrl],[CreationDate],[LastReadingDate])
			values(@Shortkey,@FullUrl,@nowdate,@nowdate);		
		select @KeyId=@@IDENTITY,@ExShortKey=@Shortkey,@LastReadingDate=@nowdate,@CreationDate=@nowdate;
	end
	else
	begin 	
		update [dbo].[Urls] set [LastReadingDate]=@nowdate where KeyId=@KeyId;
		set @LastReadingDate=@nowdate;	
	end
	select @KeyId KeyId, @ExShortKey Shortkey,@FullUrl FullUrl,@CreationDate CreationDate,@LastReadingDate LastReadingDate
END
GO
/****** Object:  StoredProcedure [dbo].[GetLatestKGSRound]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetLatestKGSRound] 
as
begin

select * from [dbo].[KGSRounds] with (nolock) where RoundId=(select max(roundId) from [dbo].[KGSRounds] with (nolock))

end
GO
/****** Object:  StoredProcedure [dbo].[GetUrlByKey]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUrlByKey]
	@Shortkey varchar(6)
AS
BEGIN
	
	SET NOCOUNT ON;  
	declare @nowdate datetime= GETDATE();
	declare @FullUrl nvarchar(225);
	declare @CreationDate Datetime;
	declare @LastReadingDate Datetime;
	declare @KeyId numeric(18,0);

	select @KeyId=KeyId, @FullUrl=FullUrl,@CreationDate=[CreationDate],@LastReadingDate=[LastReadingDate] from [dbo].[Urls] with (nolock)
		where [ShortKey]=@Shortkey

	if(@KeyId is not null)
	begin
	update [dbo].[Urls] set [LastReadingDate]=@nowdate where KeyId=@KeyId;	
	select @KeyId KeyId, @Shortkey Shortkey,@FullUrl FullUrl,@CreationDate CreationDate,@nowdate LastReadingDate
	end
	else
	begin
	select null KeyId,null Shortkey,null FullUrl,null CreationDate,null LastReadingDate where 1=0
	end
	

END
GO
/****** Object:  StoredProcedure [dbo].[InsertNewRound]    Script Date: 12/23/2020 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertNewRound] @FromKey varchar(6),@ToKey varchar(6),@LastCounter varchar(50),@RoundDate datetime
as
begin

SET NOCOUNT ON;    

INSERT INTO [dbo].[KGSRounds]
           ([FromKey]
           ,[ToKey]
           ,[LastCounter]
           ,[RoundDate])
     VALUES
           (@FromKey, 
            @ToKey, 
            @LastCounter,
            @RoundDate)

select * from [dbo].[KGSRounds] with (nolock) where RoundId=@@IDENTITY



end
GO
