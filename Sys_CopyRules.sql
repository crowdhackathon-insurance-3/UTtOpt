USE [filler]
GO

/****** Object:  StoredProcedure [dbo].[Sys_CopyRules]    Script Date: 2/9/2019 7:18:40 PM ******/
DROP PROCEDURE [dbo].[Sys_CopyRules]
GO

/****** Object:  StoredProcedure [dbo].[Sys_CopyRules]    Script Date: 2/9/2019 7:18:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sys_CopyRules]
 @ReportName as VARCHAR(1000),
  @ReportRow as VARCHAR(20), 
  @ReportColumn as VARCHAR(20), 
  @ReportNameCopy as VARCHAR(50), 
  @ReportRowCopy as VARCHAR(50),
   @ReportColumnCopy as VARCHAR(50)
AS
BEGIN TRANSACTION


delete from reports where report= @ReportName
AND r_row=@ReportRow 
AND r_column=@ReportColumn;
IF @@ERROR <> 0
 BEGIN
	-- Rollback the transaction
	ROLLBACK

	-- Raise an error and return
	RAISERROR ('Error in deleting reports.', 16, 1)
	RETURN
 END


  INSERT INTO reports (report,r_row,r_column,mf_column,operand,value,source_file,joined_column,joined_to_file)
  select @ReportName,@ReportRow,@ReportColumn ,
 mf_column,operand,value,source_file,joined_column,joined_to_file
  from reports 
  where report=@ReportNameCopy 
   AND r_row=@ReportRowCopy 
    AND r_column=@ReportRowCopy;
IF @@ERROR <> 0
 BEGIN
	-- Rollback the transaction
	ROLLBACK

	-- Raise an error and return
	RAISERROR ('Error in inserting  reports.', 16, 1)
	RETURN
 END

  COMMIT;


GO


