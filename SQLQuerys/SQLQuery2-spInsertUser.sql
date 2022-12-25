-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE spInsertUser
	-- Add the parameters for the stored procedure here
	@firstName nvarchar (20),
    @lastName nvarchar (20),
    @email nvarchar (30), 
    @password nvarchar (10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Users_2022] ([firstName], [lastName], [email], [password]) VALUES (@firstName, @lastName, @email, @password)
END
GO


select * from [Users_B]