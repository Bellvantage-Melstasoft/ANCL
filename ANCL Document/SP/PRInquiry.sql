IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRAdvanceSearchForInquiry]') )
DROP PROCEDURE [dbo].[PRAdvanceSearchForInquiry]
GO
-- =============================================
-- Author:		Mohammed Shanaz
-- Create date: 16/6/2019
-- Description:	PR Inquiry Advance Search
-- =============================================
CREATE PROCEDURE [dbo].[PRAdvanceSearchForInquiry] 
(
	@CompanyId AS INT,
	@searchBy  AS INT,
	@categoryId AS INT,
	@subDepartmentId AS INT,
	@searchText varchar(50)
) 
AS
BEGIN

	IF @searchBy=0
	BEGIN
		IF ( @categoryId >1 AND @subDepartmentId = 0 )
			BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID )
            WHERE PM.ITEM_CATEGORY_ID = @categoryId
            ORDER BY PM.PR_ID DESC
			END
		ELSE IF (@categoryId = 0 AND @subDepartmentId > 0)
			BEGIN
			SELECT * FROM dbo.PR_MASTER As PM
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
            ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId != 0 AND @subDepartmentId != 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
            WHERE PM.ITEM_CATEGORY_ID = @categoryId
            ORDER BY PM.PR_ID DESC
		END
	END
	ELSE IF @searchBy=1
	BEGIN
		IF (@categoryId > 1 AND @subDepartmentId = 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP 
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID )
            WHERE PM.ITEM_CATEGORY_ID = @categoryId
            AND PM.PR_ID like '%' + replace(@searchText, '%', '[%]') + '%' 
            OR PM.PR_CODE like '%' + replace(@searchText, '%', '[%]') + '%'
            ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId = 0 AND @subDepartmentId > 0)
		BEGIN
			 SELECT * FROM  dbo.PR_MASTER As PM 
             INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM  dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
             INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM  dbo.SUB_DEPARTMENT) AS SUBDEP  
             ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM  dbo.COMPANY_LOGIN a 
             WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
             WHERE PM.PR_ID like '%' + replace(@searchText, '%', '[%]') + '%' 
             OR PM.PR_CODE like '%' + replace(@searchText, '%', '[%]') + '%' 
             ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId != 0 AND @subDepartmentId != 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a 
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
            WHERE PM.PR_ID like '%' + replace(@searchText, '%', '[%]') + '%' 
            OR PM.PR_CODE like '%' + replace(@searchText, '%', '[%]') + '%' 
            AND  PM.ITEM_CATEGORY_ID =@categoryId
            ORDER BY PM.PR_ID DESC
		END
		ELSE
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) 
            WHERE PM.PR_ID like '%' + replace(@searchText, '%', '[%]') + '%' 
            OR PM.PR_CODE like '%' + replace(@searchText, '%', '[%]') + '%' 
            ORDER BY PM.PR_ID DESC
		END
	END
	ELSE IF @searchBy=2 
	BEGIN
	 IF (@categoryId > 1 AND @subDepartmentId = 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) 
            WHERE PM.ITEM_CATEGORY_ID = @categoryId
            AND  CL.CREATED_BY_NAME Like @searchText 
            ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId = 0 AND @subDepartmentId > 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a 
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId  ) 
            WHERE  CL.CREATED_BY_NAME Like @searchText 
            ORDER BY PM.PR_ID DESC
		END
		 ELSE IF (@categoryId != 0 AND @subDepartmentId != 0)
		 BEGIN
			SELECT * FROM  dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM  dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM  dbo.SUB_DEPARTMENT) AS SUBDEP 
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM  dbo.COMPANY_LOGIN a 
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID =  @subDepartmentId ) 
            WHERE  CL.CREATED_BY_NAME Like @searchText   
            AND  PM.ITEM_CATEGORY_ID =@categoryId  
            ORDER BY PM.PR_ID DESC
		 END
		 ELSE
		 BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) 
            WHERE  CL.CREATED_BY_NAME Like @searchText     
            ORDER BY PM.PR_ID DESC
		 END
	END
	ELSE IF @searchBy=3 
	BEGIN
		IF (@categoryId > 1 AND @subDepartmentId = 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP 
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) 
            WHERE PM.ITEM_CATEGORY_ID = @categoryId
            AND Cast(PM.DATE_OF_REQUEST AS Date)  = @searchText  
            ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId = 0 AND @subDepartmentId > 0)
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM 
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP 
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a 
            WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
            AND Cast(PM.DATE_OF_REQUEST AS Date)  = @searchText  
            ORDER BY PM.PR_ID DESC
		END
		ELSE IF (@categoryId != 0 AND @subDepartmentId != 0)
		BEGIN
			 SELECT * FROM dbo.PR_MASTER As PM 
             INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
             INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP  
             ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a
             WHERE a.USER_ID = CL.USER_ID AND a.SUB_DEPARTMENT_ID = @subDepartmentId ) 
             WHERE Cast(PM.DATE_OF_REQUEST AS Date)  = @searchText 
             AND  PM.ITEM_CATEGORY_ID = @categoryId
             ORDER BY PM.PR_ID DESC
		END
		ELSE
		BEGIN
			SELECT * FROM dbo.PR_MASTER As PM
            INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM dbo.COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID
            INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM dbo.SUB_DEPARTMENT) AS SUBDEP 
            ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM dbo.COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) 
            WHERE Cast(PM.DATE_OF_REQUEST AS Date)  =  @searchText 
            ORDER BY PM.PR_ID DESC
		END
	END
END
