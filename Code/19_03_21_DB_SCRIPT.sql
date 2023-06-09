USE [SATO_ABAS]
GO
/****** Object:  Table [dbo].[TBL_GroupMaster]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_GroupMaster](
	[GroupName] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_GroupRight]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_GroupRight](
	[GroupName] [varchar](50) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[REG_ID] [numeric](2, 0) NULL,
	[Add] [bit] NULL,
	[Update] [bit] NULL,
	[Delete] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_UserTypeRight] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC,
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_ModuleMaster]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_ModuleMaster](
	[ModuleId] [int] NOT NULL,
	[ModuleName] [varchar](150) NULL,
	[Active] [bit] NULL CONSTRAINT [DF_ModuleMaster_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_ScreenMaster] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_SCANNING_DATA]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_SCANNING_DATA](
	[ItemCode] [nvarchar](50) NOT NULL,
	[EntryDate] [nvarchar](50) NULL,
	[EntryTime] [nvarchar](50) NULL,
	[GroupNo] [nvarchar](50) NULL,
	[DeviceNo] [nvarchar](50) NULL,
	[UpdateNo] [nvarchar](50) NULL,
	[ScannedBy] [nvarchar](50) NULL,
	[ScannedOn] [datetime] NULL,
 CONSTRAINT [PK_TBL_SCANNING_DATA] PRIMARY KEY CLUSTERED 
(
	[ItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_UserMaster]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_UserMaster](
	[US_ID] [numeric](6, 0) IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[UserName] [varchar](150) NULL,
	[Password] [varchar](50) NULL,
	[GroupName] [varchar](50) NULL,
	[EmailId] [nvarchar](100) NULL,
	[EmpCode] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_UserMaster_1] PRIMARY KEY CLUSTERED 
(
	[US_ID] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Version]    Script Date: 19-03-2021 17:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Version](
	[AppName] [nvarchar](50) NULL,
	[App_Version] [nvarchar](50) NULL
) ON [PRIMARY]

GO
INSERT [dbo].[TBL_GroupMaster] ([GroupName], [CreatedOn], [CreatedBy]) VALUES (N'Admin', CAST(N'2020-03-02 09:51:44.613' AS DateTime), N'admin')
INSERT [dbo].[TBL_GroupMaster] ([GroupName], [CreatedOn], [CreatedBy]) VALUES (N'User', CAST(N'2020-03-02 09:52:33.250' AS DateTime), N'admin')
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [REG_ID], [Add], [Update], [Delete], [CreatedOn], [CreatedBy]) VALUES (N'Admin', 101, NULL, 1, 1, 1, CAST(N'2021-03-18 16:37:06.700' AS DateTime), N'1')
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [REG_ID], [Add], [Update], [Delete], [CreatedOn], [CreatedBy]) VALUES (N'Admin', 102, NULL, 1, 1, 1, CAST(N'2021-03-18 16:37:06.700' AS DateTime), N'1')
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [REG_ID], [Add], [Update], [Delete], [CreatedOn], [CreatedBy]) VALUES (N'Admin', 301, NULL, 1, 1, 1, CAST(N'2021-03-18 16:37:06.700' AS DateTime), N'1')
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [REG_ID], [Add], [Update], [Delete], [CreatedOn], [CreatedBy]) VALUES (N'User', 301, NULL, 0, 0, 0, CAST(N'2020-12-24 17:41:37.857' AS DateTime), N'1')
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (101, N'Group Master', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (102, N'User Master', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (301, N'Report', 1)
INSERT [dbo].[TBL_SCANNING_DATA] ([ItemCode], [EntryDate], [EntryTime], [GroupNo], [DeviceNo], [UpdateNo], [ScannedBy], [ScannedOn]) VALUES (N'19012117125908A002', N'20210319', N'1606', N'001', N'082', N'202103191606', N'ADMIN', CAST(N'2021-03-19 16:06:28.390' AS DateTime))
INSERT [dbo].[TBL_SCANNING_DATA] ([ItemCode], [EntryDate], [EntryTime], [GroupNo], [DeviceNo], [UpdateNo], [ScannedBy], [ScannedOn]) VALUES (N'19012117135702Y416', N'20210319', N'1724', N'001', N'082', N'202103191724', N'ADMIN', CAST(N'2021-03-19 17:24:43.257' AS DateTime))
INSERT [dbo].[TBL_SCANNING_DATA] ([ItemCode], [EntryDate], [EntryTime], [GroupNo], [DeviceNo], [UpdateNo], [ScannedBy], [ScannedOn]) VALUES (N'19012117155908A003', N'20210319', N'1606', N'001', N'082', N'202103191606', N'ADMIN', CAST(N'2021-03-19 16:06:10.560' AS DateTime))
INSERT [dbo].[TBL_SCANNING_DATA] ([ItemCode], [EntryDate], [EntryTime], [GroupNo], [DeviceNo], [UpdateNo], [ScannedBy], [ScannedOn]) VALUES (N'19012117161708A004', N'20210319', N'1606', N'001', N'082', N'202103191606', N'ADMIN', CAST(N'2021-03-19 16:06:21.170' AS DateTime))
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] ON 

INSERT [dbo].[TBL_UserMaster] ([US_ID], [UserId], [UserName], [Password], [GroupName], [EmailId], [EmpCode], [Designation], [CreatedOn], [CreatedBy]) VALUES (CAST(1 AS Numeric(6, 0)), N'1', N'1', N'1', N'Admin', NULL, NULL, NULL, CAST(N'2020-03-02 09:53:02.023' AS DateTime), N'admin')
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] OFF
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'PC', N'1.0.0.1')
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'Device', N'1.0.0.0')
/****** Object:  StoredProcedure [dbo].[PRC_BIND_COMBO]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_BIND_COMBO]
@TYPE NVARCHAR(100)
AS
BEGIN
 DECLARE @QUERY NVARCHAR(MAX)=NULL;
   IF @TYPE='SELECT_GROUP'
     BEGIN
        SET @QUERY='SELECT GroupName FROM TBl_GroupMaster Order By GroupName'
		 EXEC(@QUERY)
	 END
   IF @TYPE='BIND_PART'
     BEGIN
	    SET @QUERY='SELECT PART_NO,PART_NO FROM TBL_PART_MASTER'
		EXEC(@QUERY)
	 END
   IF @TYPE='BIND_OPERATOR'
     BEGIN
	    SET @QUERY='SELECT UserName,UserName FROM TBL_UserMaster'
		EXEC(@QUERY)
	 END
   IF @TYPE='BIND_CUSTOMER'
     BEGIN
	    SET @QUERY='SELECT CUST_CODE,CUST_NAME FROM TBL_CUSTOMER_MASTER'
		EXEC(@QUERY)
	 END
	
END






GO
/****** Object:  StoredProcedure [dbo].[PRC_GET_VERSION]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PRC_GET_VERSION]
@APP_NAME NVARCHAR(10),
@APP_VERSION NVARCHAR(100)
AS
BEGIN
   DECLARE @DB_APP_VERSION NVARCHAR(100)=NULL
   IF NOT EXISTS(SELECT App_Version FROM TBL_Version WHERE  AppName=@APP_NAME AND App_Version=@APP_VERSION )
     BEGIN
	    SELECT @DB_APP_VERSION =MAX(App_Version) FROM TBL_Version WHERE  AppName=@APP_NAME 
		
	    SELECT 'New application version('+cast( @DB_APP_VERSION as varchar)+') and your application version is ('+cast(@APP_VERSION as varchar)+'). Kindly update with new version!!!!!!'  ;
	 END
   else
     SELECT 'OK'
END







GO
/****** Object:  StoredProcedure [dbo].[PRC_GroupMaster]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



	CREATE proc [dbo].[PRC_GroupMaster] 
	@Type varchar(20),
	@GroupName varchar(50)=null,
	@ModuleId int=null,
	@Add bit=0,
	@Update bit=0,
	@Delete bit=0,
	@CreatedBy varchar(100)=null
	as
	  DECLARE @Query nvarchar(max)=null;
	  Begin
  		 if(@Type='SELECT')
		  begin
			Select  GroupName,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
				  from TBL_GroupMaster 
					Order By GroupName

			Select ModuleId,ModuleName,0'Add',0'Update',0'Delete' From TBL_ModuleMaster Where Active = 'True' Order By ModuleName
		  end
		else if(@Type='SELECTBYID')
		  begin
				Select ModuleId,[Add],[Update],[Delete] From TBL_GroupRight  Where GroupName = @GroupName 
		  end
	   else if(@Type='GET_USER_RIGHTS')
		  begin
		        set @Query='
				Select ModuleId From TBL_GroupRight'
				if @GroupName !='ADMIN'
				  begin
				    set @Query=@Query+' Where GroupName = '''+@GroupName+''''
				  end
				exec(@Query)
		  end		  
		else if(@Type='INSERT')
		  begin
	 
			Insert Into TBL_GroupMaster (GroupName,CreatedBy,CreatedOn)
			values( @GroupName,@CreatedBy,GETDATE())
	    
			Select 'SUCCESS'
			end
		  else if(@Type='INSERT_GROUP_RIGHT')
		  begin
			INSERT INTO [dbo].[TBL_GroupRight]
			   ([GroupName]
			   ,[ModuleId]
			   ,[Add]
			   ,[Update]
			   ,[Delete]
			   ,[CreatedOn]
			   ,[CreatedBy]) VALUES(@GroupName,@ModuleId,@Add,@Update,@Delete,GETDATE(),@CreatedBy)
			 Select 'SUCCESS'
		  end
		else if(@Type='DELETE_GROUP_RIGHT')
		  begin
			Delete From TBL_GroupRight Where GroupName = @GroupName 
			 Select 'SUCCESS'
		  end
		else if(@Type='DELETE')
		  begin
			  Begin Try
				Begin Tran
					 delete from TBL_GroupMaster
					 WHERE GroupName=@GroupName 

					 Delete From TBL_GroupRight Where GroupName = @GroupName

					 Select 'SUCCESS'
				Commit Tran
			End Try
			Begin Catch
				Rollback Tran
				Select 'DB Error-'+  ERROR_MESSAGE()
			End Catch
		  end
		
	
	  End
















GO
/****** Object:  StoredProcedure [dbo].[PRC_REPORT]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_REPORT]
@TYPE NVARCHAR(100)=NULL,
@Model_No  NVARCHAR(50)=NULL,
@FROM_DATE NVARCHAR(50)=NULL,
@TO_DATE NVARCHAR(50)=NULL,
@FG_Part_No  NVARCHAR(50)=NULL
AS
BEGIN

 DECLARE @QUERY NVARCHAR(MAX)=NULL
   

  IF @TYPE='GET_BARCODE_DATA'
    BEGIN
	   SET @QUERY='
	       SELECT ROW_NUMBER() OVER(ORDER BY ScannedOn asc) AS SNo,ItemCode,EntryDate,EntryTime,GroupNo,DeviceNo,UpdateNo FROM  TBL_SCANNING_DATA WHERE CONVERT(VARCHAR(10),ScannedOn,121) BETWEEN '''+@FROM_DATE+''' AND '''+@TO_DATE+''''
	    
	  EXEC(@QUERY)
	END
    
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_SCANNING]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_SCANNING]
@TYPE NVARCHAR(100)=NULL,
@BARCODE NVARCHAR(200)=NULL,
@GROUP NVARCHAR(3)=NULL,
@DEVICE NVARCHAR(3)=NULL,
@CREATED_BY NVARCHAR(100)=NULL
AS
BEGIN

    DECLARE 
			@DATE NVARCHAR(10)=NULL,
			@TIME NVARCHAR(8)=NULL,
			@CREATED_ON DATETIME=NULL,
			@FOR_BARCODE_DATE NVARCHAR(100)=NULL,
			@FOR_BARCODE_TIME NVARCHAR(100)=NULL

    IF @TYPE='SELECT'
	  BEGIN
		  SELECT top(10) [ItemCode]
			  ,[EntryDate]
			  ,[EntryTime]
			  ,[GroupNo]
			  ,[DeviceNo]
			  ,[UpdateNo]
			  ,[ScannedBy]
			  ,[ScannedOn]
		  FROM [dbo].[TBL_SCANNING_DATA] order by [EntryDate]
			  ,[EntryTime] desc
	  END
	IF @TYPE='SCAN'
	  BEGIN
	       
			IF EXISTS(SELECT 1  FROM TBL_SCANNING_DATA WHERE ItemCode=@BARCODE)
			  BEGIN
			      SELECT 'INVALID~This Barcode Already Scanned!!';
				  return;
			  END
			SELECT @CREATED_ON=GETDATE()
			SELECT @DATE=CONVERT(varchar,@CREATED_ON,103)
		
			SELECT @TIME=CONVERT(varchar(5),@CREATED_ON,8)
		
			SELECT @FOR_BARCODE_DATE=CONVERT(varchar,@CREATED_ON,112)
			SELECT @FOR_BARCODE_TIME =REPLACE(@TIME,':','')
			

			

			INSERT INTO [dbo].[TBL_SCANNING_DATA]
				   ([ItemCode]
				   ,[EntryDate]
				   ,[EntryTime]
				   ,[GroupNo]
				   ,[DeviceNo]
				   ,[UpdateNo]
				   ,[ScannedBy]
				   ,[ScannedOn])
			 VALUES
				   (@BARCODE
				   ,@FOR_BARCODE_DATE
				   ,@FOR_BARCODE_TIME
				   ,@GROUP
				   ,@DEVICE
				   ,@FOR_BARCODE_DATE+@FOR_BARCODE_TIME
				   ,@CREATED_BY
				   ,@CREATED_ON)
			select 'VALID~SAVED'
	  END
	IF @TYPE='TIME'
	  BEGIN
	       	SELECT @CREATED_ON=GETDATE()
			SELECT @DATE=CONVERT(varchar,@CREATED_ON,103)
		
			SELECT @TIME=CONVERT(varchar(5),@CREATED_ON,8)
			
			select 'VALID~'+CAST(@TIME AS NVARCHAR)+''
	  END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_SET_CHILDSEC_RIGHTS]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PRC_SET_CHILDSEC_RIGHTS] 
@GROUP_NAME NVARCHAR(50)=NULL,
@MODULE_NAME NVARCHAR(100)=NULL
AS
BEGIN
  
SELECT distinct [Add] as [Save],[Update],[Delete]  FROM TBL_GROUPRIGHT GR JOIN TBL_MODULEMASTER MD ON GR.MODULEID=MD.MODULEID
WHERE MODULENAME=@MODULE_NAME AND GroupName=@GROUP_NAME
	
END






GO
/****** Object:  StoredProcedure [dbo].[PRC_UserMaster]    Script Date: 19-03-2021 17:27:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[PRC_UserMaster] 
@Type varchar(max),
@UserID nvarchar(50)=null,
@UserName nvarchar(150)=null,
@Password nvarchar(50)=null,
@Group nvarchar(50)=null,
@CreatedBy nvarchar(50)=null,
@NewPassword nvarchar(50)=null,
@EmailId nvarchar(100)=null,
@EmpCode nvarchar(100)=null,
@EmpDesignation nvarchar(100)=null

AS
BEGIN
	Declare @Count int;
	 DECLARE @QUERY NVARCHAR(MAX)=NULL;
	if (@Type = 'SELECT')
		begin
			 SET @QUERY='SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
			 FROM TBL_UserMaster us'
			  SET @QUERY=@QUERY+' ORDER BY USERNAME'
		    EXEC(@QUERY)
		end
	
	else if (@Type = 'SEARCH')
		begin
			SET @QUERY='SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn 
			FROM TBL_UserMaster us '

			SET @QUERY=@QUERY+' WHERE USERID like ''%'+CAST(@UserID AS nvarchar)+'%'' or  UserName like ''%'+CAST(@UserID AS nvarchar)+'%'''
			
		   EXEC(@QUERY)
		end
	else if(@Type='SELECTBYID')
	  begin
				SET @QUERY='SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
				 FROM TBL_UserMaster us join TBL_Region_Master rg on us.REG_ID=rg.RG_ID
				 join TBL_Department_Master dp on us.DP_ID=dp.DP_ID
					WHERE USERID = '''+CAST(@UserID AS nvarchar)+''''
		
		     EXEC(@QUERY)
	  end
	else if(@Type='INSERT')
	  begin
	    IF EXISTS(SELECT 1 FROM TBL_UserMaster WHERE UserId=@UserID)
		 BEGIN
		     Select 'This user id is already exists!!!!' AS RESULT
			 RETURN
		 END
	    INSERT INTO [dbo].[TBL_UserMaster]
           ([UserId]
           ,[UserName]
           ,[Password]
           ,[GroupName]
		   ,[EmailId]
		   ,[EmpCode]
		   ,[Designation]
           ,[CreatedOn]
           ,[CreatedBy])
		 VALUES
           (@UserID
           ,@UserName
           ,@Password
           ,@Group
		   ,@EmailId
		   ,@EmpCode
		   ,@EmpDesignation
           ,GETDATE()
           ,@CreatedBy)

			Select 'Y' AS RESULT
	  end
	 else if(@Type='UPDATE')
	  begin
	     UPDATE [dbo].[TBL_UserMaster]
		   SET [UserName] = @UserName
			  ,[Password] = @Password
			  ,[GroupName] = @Group
			  ,[EmailId]=@EmailId
			  ,[EmpCode]=@EmpCode
			  ,[Designation]=@EmpDesignation
		 WHERE UserId = @UserId 
		 Select 'Y' AS RESULT
	  end
	else if(@Type='DELETE')
	  begin
	      declare @Uid int=null
		  set @Uid=(select US_ID from dbo.[TBL_UserMaster] WHERE UserId=@UserID )
	     delete from dbo.[TBL_UserMaster] WHERE UserId=@UserID 
		 Select 'Y' AS RESULT
	  end
	
	else if (@Type='VALIDATEUSER') 
		    begin
			      Select distinct UserName,GroupName,USERID,US_ID From TBL_UserMaster
				   Where UserId = @UserID And Password = @Password 
		    end
    else if (@Type='ACCESSUSER') 
		    begin
			   if exists(select US_ID from TBL_UserMaster where UserId = @UserID And Password = @Password AND GroupName in ('ADMIN','SUPERVISOR'))
			     begin
				      DECLARE @MSG_RS nvarchar(50)=null
					  Select distinct @MSG_RS=UserName From TBL_UserMaster
					   Where UserId = @UserID And Password = @Password 
					  Select 'Y' AS RESULT,@MSG_RS AS MSG
				 end
			   else
			      Select 'N' AS RESULT,'Only Admin/Supervisor can give the access!!' AS MSG
		    end
	else if (@Type='UPDATEPASSWORD')
		begin
			Declare @UserOldPassword varchar(50)
			Select @UserOldPassword = Password From TBL_UserMaster Where UserId = @UserID
		
			If(@Password = @UserOldPassword)
			Begin
				UPDATE TBL_UserMaster set PASSWORD = @NewPassword where  UserId=@UserID
				Select 'Y' AS RESULT
			End
			Else
			Begin
				Select 'Wrong Old Password' AS RESULT
			End
		end

END




GO
