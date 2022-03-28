using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.T_Sql
{
    public struct Query_Violation
    {

        #region Select Active Brand
        const string Select_Brand = @"
                    SELECT [colBrandIdInt]
                          ,[colBrandCodeVarchar]
                          ,[colBrandNameVarchar]
                          ,[colCreatedByVarchar]
                          ,[colDateCreatedDatetime]
                          ,[colModifiedByVarchar]
                          ,[colDateModifiedDatetime]
                          ,[colIsActiveBit]
                          ,[colCompanyCodeVarchar]
                      FROM [dbo].[TblBrand]
                      where colIsActiveBit = 1
                ";

        public static string Sel_Brand { get { return Select_Brand; } }


        #endregion

        #region Select Active Ship
        const string Select_Ship = @"
                  

                    SELECT [colVesselIdInt]
                          ,[colVesselNameVarchar]
                          ,[colVesselCodeVarchar]
                          ,[colVesselLongCodeVarchar]
                          ,[colVesselClassVarchar]
                          ,[colVesselRegistryVarchar]
                          ,[colBrandVarchar]
                          ,[colVesselNoInt]
                          ,[colBrandIdInt]
                          ,[colCreatedByVarchar]
                          ,[colModifiedByVarchar]
                          ,[colDateCreatedDatetime]
                          ,[colDateModifiedDatetime]
                          ,[colIsActiveBit]
                          ,[colEmailAddVarchar]
                      FROM [dbo].[TblVessel]
                    where colIsActiveBit = 1
                ";

        public static string Sel_Ship { get { return Select_Ship; } }
        #endregion

        #region Select Active CostCenter
        const string Select_CostCenter = @"
	              select   C.colCostCenterIDInt
		            ,C.colCostCenterCodeVarchar
		            ,C.colCostCenterNameVarchar
	              from dbo.TblCostCenter C 
	              where C.colIsActiveBit =  1
                ";

        public static string Sel_CostCenter { get { return Select_CostCenter; } }
        #endregion

        #region Select Active Month Year
        const string Select_Postion = @"
	              select  [colRankIDInt]
                      ,[colCostCenterIDInt]
                      ,[colRankNameVarchar]
                      ,[colRankCodeVarchar]
	              from dbo.TblRank   
	              where colIsActiveBit =  1
                ";

        public static string Sel_Position { get { return Select_Postion; } }
        #endregion

        #region Select Active Month Year
        const string Select_MonthYear = @"
	                             select s.colYear  
			                            ,m.colMonthtinyint  
			                            ,m.colMonth
	                             from dbo.TblYear s
		                            cross  join dbo.TblMonth m
	                             where s.colIsActive = 1	and s.colYear = year(GETDATE()) and colMonthtinyint <= MONTH(getdate())	
	                             union all
	                             select  
			                            s.colYear  
			                            ,m.colMonthtinyint  
			                            ,m.colMonth
	                             from dbo.TblYear s
		                            cross  join dbo.TblMonth m
	                             where s.colIsActive = 1	and s.colYear <year(GETDATE()) 
                                 order by s.colYear desc 
                            ";

        public static string Sel_MonthYear { get { return Select_MonthYear; } }

        #endregion

        #region Select Active Audit Peiod
        const string Select_AuditPeriod = @"
	                 	SELECT [colPeriodID]
		                      ,[colPeriod]
		                      ,[colStartDays]
		                      ,[colNoOfDays]
	                    FROM [dbo].[TblAuditPeriod]
                        where colIsActive = 1
	  
                    ";

        public static string Sel_AuditPeriod { get { return Select_AuditPeriod; } }
        #endregion


        #region Get Violattion

        const string select_Violation = @"
          
            declare @isManger tinyint = 0, @IsUserBrand bit = 0
		    select top 1 @isManger = isnull(IsManager,0) from dbo.TblUsers
		    where UserName = @pUserID and IsActive = 1
		    declare @pFrom  as dateTime, @pTo as datetime
		    select @pFrom = [From],@pTo = [To]  from [FromAndToDate](@pDateFrom, @pDateTo)

		    if @pDateFrom is null or @pDateTo is null begin
			    set  @pFrom =  null
			    set  @pTo = null
		    end

		    CREATE TABLE #Violators
		    (
			    RowID   int,
			    ViolatorsID int,
			    ViolationDate date,
			    EmployeeID bigint,
			    EmpName varchar(100),
			    Ship varchar(5),
			    CostCenter varchar(6),
			    JobCode varchar(15),
			    violationTypeID Int,
			    violationType varchar(100),
			    RootCauseCategory varchar(100),
			    RootCauseID Int,
			    RootCauseComment nvarchar(max),
			    CorrectiveAction nvarchar(max),
			    ViolationIndicator varchar(3),
			    RootCauseCategoryID Int,
			    MngerID bigint,
			    MngerPosition varchar(20),
			    MngerName varchar(200),
			    IsLock bit default(0) ,
			    IsManager tinyint default(0), 
			    MngrRootCauseID Int,
			    MngrRootCause   varchar(200),
			    MngrRootCauseCode   varchar(20) 
		    )
		    CREATE INDEX IX_sp ON #Violators (ViolatorsID)

		 
		    CREATE TABLE #tblViolators
		    (
			    ViolatorsID int,
			    ViolationDate date,
			    EmployeeID bigint,
			    EmpName varchar(100),
			    Ship varchar(5),
			    CostCenter varchar(6),
			    JobCode varchar(15),
			    violationTypeID Int,
			    violationType varchar(100),
			    RootCauseCategory varchar(100),
			    RootCauseID Int,
			    RootCauseComment nvarchar(max),
			    CorrectiveAction nvarchar(max),
			    ViolationIndicator varchar(3),
			    RootCauseCategoryID Int,
			    MngerID bigint,
			    MngerPosition varchar(20),
			    MngerName varchar(200),
			    IsLock bit default(0) ,
			    IsManager tinyint default(0), 
			    MngrRootCauseID Int,
			    MngrRootCause   varchar(200),
			    MngrRootCauseCode   varchar(20) 
		    )
		    CREATE INDEX IX_sp ON #tblViolators (ViolatorsID)
 
 
 
		   SELECT  DISTINCT  
		
					 a.colViolatorsID
					,a.colViolationDate
					,A.colEmployeeID
					,a.colCostCenterCode
					,a.colJobCode
					,a.colStatus 
					--,a.colViolationLength
					,a.colVesselCode 	 
					
			 			
					,IsLock  = case when @isManger = 3 then 0 else 
									case when DATEDIFF(d,cast(@pTo as date), CAST(GETDATE() AS date) ) >= 10 then 1 else 0 end
							   end
					,IsManager = @isManger
					,Ship = a.colVesselCode
					
					,RootCauseID =			a.colRootCauseID
					,RootCauseComment = 	a.colRootCauseComment
					,CorrectiveAction = 	a.colCorrectiveActionTaken
					,RootCauseCategoryID =	a.colRootCauseCategoryID
					,MngrRootCauseID =		a.colMngrRootCauseID
					
					 
					,MngerID = a.colMngerID
					,MngerPosition = a.colMngerPosition
				into #TempViolators 		
				FROM dbo.View_Violation a
				WHERE a.colViolationDate BETWEEN  @pFrom and @pTo and a.colBrandVarchar = isnull(@pBrandID,'') 
					AND ((isnull(a.colVesselCode  ,'') = isnull(@pVesselID ,'') and isnull(a.colVesselCode  ,'')  <> '')  or isnull(@pVesselID,'')= '')
					AND ((isnull(a.colCostCenterCode,'') =  isnull(@pCostcenter,'' ) and isnull(@pCostcenter,'' ) <> '') or isnull(@pCostcenter,'') = '')
					AND ((isnull(a.colJobCode,'') =  isnull(@pJobeCode,'') and  isnull(@pJobeCode,'') <> '')or isnull(@pJobeCode,'') = '')
		 		
		    INSERT INTO #tblViolators
		    SELECT  DISTINCT  
					    ID  = a.colViolatorsID
					    ,ViolationDate = CONVERT(varchar(11),a.colViolationDate,101)
					    ,EmpID = colEmployeeID
					    ,EmpName = (isnull(emp.colLastNameVarchar,'')  +  ', ' + Isnull(emp.colFirstNameVarchar,'')  +  ' ' + ISNULL(emp.colMiddleNameVarchar,  + CHAR(39) + CHAR(39)   ))
					    ,Ship 
					    ,CostCenter = a.colCostCenterCode
					    ,JobCode = a.colJobCode
					    ,ViolationTypeID = d.colViolationTypeID
					    ,violationType = UPPER(d.colViolationType)
					    ,RootCauseCategory = UPPER(c.colRootCause)
					  --  ,ViolationLength = SUBSTRING( convert(varchar, a.colViolationLength,108),1,5)
					    ,a.RootCauseID  
					    ,a.RootCauseComment 
					    ,a.CorrectiveAction  
					    ,ViolationIndicator = d.colViolationIndicator
					    ,a.RootCauseCategoryID 
					    ,a.MngerID  
					    ,a.MngerPosition  
					    ,MngerName =  (Mangemp.colLastNameVarchar  +  ' ' + Mangemp.colFirstNameVarchar  +  ' ' + ISNULL(Mangemp.colMiddleNameVarchar,  + CHAR(39) + CHAR(39)   ))
					    ,A.IsLock  
					    ,IsManager 
					    ,a.MngrRootCauseID  
					    ,MngrRootCause = mc.colRootCause
					    ,MngrRootCauseCode = mc.colRootCauseCode
			    FROM #TempViolators a
				    LEFT JOIN tblRootCause c ON a.RootCauseID = c.colRootCauseID
				    LEFT JOIN tblViolationType d ON a.colStatus = d.colViolationTypeID
				    JOIN Travelmart.dbo.TblSeafarer emp ON a.colEmployeeID = emp.colSeafarerIDint	
				    left join Travelmart.dbo.TblSeafarer Mangemp ON a.MngerID = Mangemp.colSeafarerIDint	
				    LEFT JOIN tblRootCause mc ON a.MngrRootCauseID = mc.colRootCauseID
		 
			    ORDER BY EmpName--, ViolationDate  
 
			    if (select COUNT(*) from #tblViolators) > 0 begin
				    insert into #Violators
				    select top (@pPagingCount)   
				    A.* 
				    from (
					    SELECT ROW_NUMBER()over(order by ViolationDate asc, EmpName) as RowNumber,*    
					    FROM #tblViolators
				    ) as A where A.RowNumber >= ((@pStartRow - 1) * @pPagingCount + 1)    
			    end

			    select * from #Violators

		    CREATE TABLE #TempViolationShift
		    (
			    EmployeeID bigint,
			    ViolationDate date,
			    ViolatorsID int,
			    GroupID bigint
		     )
		    create Index #X_ViolatorsID on #TempViolationShift(ViolatorsID)
 
		    insert into #TempViolationShift
		    select * from (
					select V.colEmployeeID
						,V.colViolationDate
						, V.colViolatorsID
						,B.ViolatorsID as GroupID
					from tblViolators  V
						inner join #Violators B on V.colEmployeeID = B.EmployeeID
							and V.colViolationDate <  B.ViolationDate
							and datediff(d,V.colViolationDate, B.ViolationDate) = 1

					union all
					select 
						V.EmployeeID
						,V.ViolationDate
						,V.ViolatorsID
						,V.ViolatorsID as GroupID
						from #Violators V
					union all
					select
						V.colEmployeeID
						,V.colViolationDate
						,V.colViolatorsID
						,B.ViolatorsID  as GroupID
					from tblViolators  V
						inner join #Violators B on V.colEmployeeID = B.EmployeeID
							and V.colViolationDate >  B.ViolationDate
							and datediff(d,B.ViolationDate,V.colViolationDate ) = 1
		    ) as V order by colEmployeeID, colViolationDate
		
		 
		    ;WITH X as (
					    SELECT DISTINCT a.colViolatorsId ,
							    a.colShiftNum,
							    S.EmployeeID,
							    S.GroupID,
							    S.ViolationDate ,
						        Details = STUFF((SELECT ',' + colShiftValue
										    FROM tblShiftHistory b
										    WHERE b.colViolatorsID = a.colViolatorsID
											      AND b.colShiftNum = a.colShiftNum
								    FOR XML PATH ('')),1,1,'')
					    FROM tblShiftHistory a
						    inner join #TempViolationShift S on a.colViolatorsID = S.ViolatorsID
			    )
			 
			    SELECT   distinct a.colViolatorsID,
				      a.EmployeeID,
				      A.GroupID,
				      a.ViolationDate ,
				      Details = STUFF((SELECT '^' + Details
									    FROM X b
									    WHERE b.colViolatorsID = a.colViolatorsID
							    FOR XML PATH ('')),1,1,'')
			    into #ViolationShift  
			    FROM X a
			    GROUP BY a.EmployeeID,a.GroupID,a.colViolatorsID,a.ViolationDate
			    ORDER BY a.EmployeeID,a.GroupID,a.ViolationDate desc,a.colViolatorsID
 
			    select * from #ViolationShift

			    SELECT DISTINCT C.colRootCauseID,
							    C.colViolationTypeID,
							    C.colRootCauseCode,
							    upper(C.colRootCause) as colRootCause
							    ,upper(V.colViolationType) as ViolationType
			    FROM tblRootCause C
					inner join tblViolationType V on C.colViolationTypeID = V.colViolationTypeID and V.IsActive = 1
			    WHERE colIsActive = 1
			    ORDER BY colRootCause 
 
			    declare @Count int = @pPagingCount  , @RowCounter int = 1
			    select @Count = count(*) from #tblViolators
			    declare @TableCount as table
			    (
				    [COUNTS] int ,
				    [StartRow] tinyint,
				    [PagingCount] tinyInt
			    )

			    while @Count > 0 begin
				    set @Count = @count - @pPagingCount
				    insert into @TableCount
				    select @RowCounter, @pStartRow, @pPagingCount
				    set @RowCounter = @RowCounter + 1
			    end
			
			    select * from @TableCount select top 1 R.Details from #ViolationShift R
			    order by len(R.Details) desc
            
                ";

        public static string Sel_Violation { get { return select_Violation; } }


        #endregion



    }
}
