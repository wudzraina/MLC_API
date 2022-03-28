using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.T_Sql
{
	public struct Query_ViolationDapper
	{

		#region Select Active Brand
		const string Select_Brand = @"
                     SELECT [colBrandIdInt] as BrandId
                          ,[colBrandNameVarchar] as BrandName
                          ,[colBrandCodeVarchar] as BrandCode
                          ,[colCompanyCodeVarchar] as CompanyCode
                      FROM Travelmart.[dbo].[TblBrand]
                      where colIsActiveBit = 1
 
	                SELECT [VesselId] as ShipId
                            ,[VesselName] ShipName
                            ,[VesselCode] as ShipCode
                            ,[Brand] as BrandName
                            ,[VesselNo]
                            ,[BrandId]
                        FROM [dbo].[Vessel]
                    where IsActive = 1

                    select C.CostCenterID
			            ,C.CostCenterCode 
		                ,C.CostCenterName 
                    from dbo.CostCenter C 
                    where C.IsActive  =  1

                    select  [colRankIDInt] as PostionId
                            ,[colCostCenterIDInt] as CostCenterId
                            ,[colRankNameVarchar] as PositionName
                            ,[colRankCodeVarchar] as PositionCode
                    from Travelmart.dbo.TblRank   
                    where colIsActiveBit =  1
  
                    select s.Year  
                        ,m.Monthtinyint  
                        ,m.Month
                    from dbo.Year s
                    cross  join dbo.[Month] m
                    where s.IsActive = 1	and s.Year = year(GETDATE()) and Monthtinyint <= MONTH(getdate())	
                    union  
                    select  
                        s.Year  
                        ,m.Monthtinyint  
                        ,m.Month
                    from dbo.[Year] s
                    cross  join dbo.Month m
                    where s.IsActive = 1	and s.Year <year(GETDATE()) 
                    order by s.Year desc 

                    SELECT [PeriodID]
                          ,[Period] as AuditPeriods
                          ,[StartDays] as StartDay
                          ,[NoOfDays]
                    FROM [dbo].[AuditPeriod]
                    where IsActive = 1

                ";
		public static string Sel_Brand { get { return Select_Brand; } }
		#endregion

		#region Select Active Violation
		const string Select_Violation = @"

                declare @isManger tinyint = 0, @IsUserBrand bit = 0
				select top 1 @isManger = isnull(IsManager,0) from dbo.Users
				where UserName = @pUserID and IsActive = 1
				declare @pFrom  as dateTime, @pTo as datetime
				select @pFrom = [From],@pTo = [To]  from [FromAndToDate](@pDateFrom, @pDateTo)

				if @pDateFrom is null or @pDateTo is null begin
					set  @pFrom =  null
					set  @pTo = null
				end

				SELECT  DISTINCT  
						a.ViolatorsID
						,a.ViolationDate
						,A.EmployeeID
						,a.CostCenterCode
						,a.JobCode
						,a.Status 
						,a.VesselCode 	 
						,IsLock  = case when @isManger = 3 then 0 else 
										case when DATEDIFF(d,cast(@pTo as date), CAST(GETDATE() AS date) ) >= 10 then 1 else 0 end
									end
						,IsManager = @isManger
						,Ship = a.VesselCode
						,RootCauseID =			a.RootCauseID
						,RootCauseComment = 	a.RootCauseComment
						,CorrectiveAction = 	a.CorrectiveActionTaken
						,RootCauseCategoryID =	a.RootCauseCategoryID
						,MngrRootCauseID =	 	a.MngrRootCauseID
						,MngerID = a. MngerID
						,MngerPosition = a.MngerPosition

					into #TempViolators 		
					FROM dbo.View_Violation a with (nolock)
					WHERE a. ViolationDate BETWEEN  @pFrom and @pTo and a.Brand = isnull(@pBrandID,'') 
						AND ((isnull(a.VesselCode  ,'') = isnull(@pVesselID ,'') and isnull(a.VesselCode  ,'')  <> '')  or isnull(@pVesselID,'')= '')
						AND ((isnull(a.CostCenterCode,'') =  isnull(@pCostcenter,'' ) and isnull(@pCostcenter,'' ) <> '') or isnull(@pCostcenter,'') = '')
						AND ((isnull(a.JobCode,'') =  isnull(@pJobeCode,'') and  isnull(@pJobeCode,'') <> '')or isnull(@pJobeCode,'') = '')
 
			 
		    
					SELECT  DISTINCT  
								A.ViolatorsID
								,ViolationDate = CONVERT(varchar(11),a.ViolationDate,101)
								,EmployeeID
								,LastName = isnull(emp.colLastNameVarchar,'')
								,FirstName = Isnull(emp.colFirstNameVarchar,'')
								,MiddleName = ISNULL(emp.colMiddleNameVarchar, '')
								,Ship 
								,CostCenter = a.CostCenterCode
								,JobCode = a.JobCode
								,ViolationTypeID = d.ViolationTypeID
								,violationType = UPPER(d.ViolationType)
								,RootCauseCategory = UPPER(c.RootCause)
								,a.RootCauseID  
								,a.RootCauseComment 
								,a.CorrectiveAction  
								,a.RootCauseCategoryID 
								,a.MngerID  
								,a.MngerPosition  
								,MngerName =  (Mangemp.colLastNameVarchar  +  ' ' + Mangemp.colFirstNameVarchar  +  ' ' + ISNULL(Mangemp.colMiddleNameVarchar,  + CHAR(39) + CHAR(39)   ))
								,A.IsLock  
								,IsManager 
								,a.MngrRootCauseID  
								,MngrRootCause = mc.RootCause
								,MngrRootCauseCode = mc.RootCauseCode
					into #Violators1
					FROM #TempViolators a
						LEFT JOIN RootCause c with (nolock) ON a.RootCauseID = c.RootCauseID
						LEFT JOIN ViolationType d with (nolock) ON a.Status = d.ViolationTypeID
						JOIN Travelmart.dbo.TblSeafarer emp with (nolock) ON a.EmployeeID = emp.colSeafarerIDint	
						left join Travelmart.dbo.TblSeafarer Mangemp with (nolock) ON a.MngerID = Mangemp.colSeafarerIDint	
						LEFT JOIN RootCause mc with (nolock) ON a.MngrRootCauseID = mc.RootCauseID
					ORDER BY LastName--, ViolationDate  
					CREATE INDEX IX_sp ON #Violators1 (ViolatorsID)
 
  
					if (select COUNT(*) from #Violators1) > 0 begin
						select top (@pPagingCount)   
						A.* into #Violators
						from (
							SELECT ROW_NUMBER()over(order by ViolationDate asc, LastName) as RowNumber,*    
							FROM #Violators1
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
					select * from 
					(
						select V.EmployeeID
							,V.ViolationDate
							, V.ViolatorsID
							,B.ViolatorsID as GroupID
						from Violators  V with  (nolock)
							inner join #Violators B on V.EmployeeID = B.EmployeeID
								and V.ViolationDate <  B.ViolationDate
								and datediff(d,V.ViolationDate, B.ViolationDate) = 1
						union  
						select 
							V.EmployeeID
							,V.ViolationDate
							,V.ViolatorsID
							,V.ViolatorsID as GroupID
							from #Violators V
						union   
						select
								V.EmployeeID
							,V.ViolationDate
							,V.ViolatorsID
							,B.ViolatorsID  as GroupID
						from Violators  V with  (nolock)
							inner join #Violators B on V.EmployeeID = B.EmployeeID
								and V.ViolationDate >  B.ViolationDate
								and datediff(d,B.ViolationDate,V.ViolationDate ) = 1
					) as V order by EmployeeID, ViolationDate
		
					;WITH X as (
							SELECT DISTINCT a.ViolatorsId ,
									a.ShiftNum,
									S.EmployeeID,
									S.GroupID,
									S.ViolationDate ,
									Details = STUFF((SELECT ',' + ShiftValue
												FROM ShiftHistory b
												WHERE b.ViolatorsID = a.ViolatorsID
														AND b.ShiftNum = a.ShiftNum
										FOR XML PATH ('')),1,1,'')
							FROM ShiftHistory a with (nolock)
								inner join #TempViolationShift S on a.ViolatorsID = S.ViolatorsID
					)
		 
					SELECT   distinct a.ViolatorsID,
							a.EmployeeID,
							A.GroupID,
							a.ViolationDate ,
							Details = STUFF((SELECT '^' + Details
											FROM X b
											WHERE b.ViolatorsID = a.ViolatorsID
									FOR XML PATH ('')),1,1,'')
					into #ViolationShift  
					FROM X a
					GROUP BY a.EmployeeID,a.GroupID,a.ViolatorsID,a.ViolationDate
					ORDER BY a.EmployeeID,a.GroupID,a.ViolationDate desc,a.ViolatorsID

					select * from #ViolationShift

					SELECT DISTINCT C.RootCauseID,
									C.ViolationTypeID,
									C.RootCauseCode,
									upper(C.RootCause) as RootCause,
									upper(V.ViolationType) as ViolationType
					FROM RootCause C with (nolock)
						inner join ViolationType V with (nolock) on C.ViolationTypeID = V.ViolationTypeID and V.IsActive = 1
					WHERE C.IsActive = 1
					order by C.RootCauseCode


					select top 1 R.Details from #ViolationShift R
					order by len(R.Details) desc

                ";
		public static string Sel_Violation { get { return Select_Violation; } }
		#endregion Select Active Violation

		#region Select All Active Violation
		const string Select_AllViolation = @"
                
					declare @isManger tinyint = 0, @IsUserBrand bit = 0
					select top 1 @isManger = isnull(IsManager,0) from dbo.Users
					where UserName = @pUserID and IsActive = 1
					declare @pFrom  as dateTime, @pTo as datetime
					select @pFrom = [From],@pTo = [To]  from [FromAndToDate](@pDateFrom, @pDateTo)

					if @pDateFrom is null or @pDateTo is null begin
						set  @pFrom =  null
						set  @pTo = null
					end

					SELECT  DISTINCT 
							a.ViolatorsID
							,a.ViolationDate
							,A.EmployeeID
							,a.CostCenterCode
							,a.JobCode
							,a.Status 
							,a.VesselCode 	 
							,IsLock  = case when @isManger = 3 then 0 else 
											case when DATEDIFF(d,cast(@pTo as date), CAST(GETDATE() AS date) ) >= 10 then 1 else 0 end
										end
							,IsManager = @isManger
							,Ship = a.VesselCode
							,RootCauseID =			a.RootCauseID
							,RootCauseComment = 	a.RootCauseComment
							,CorrectiveAction = 	a.CorrectiveActionTaken
							,RootCauseCategoryID =	a.RootCauseCategoryID
							,MngrRootCauseID =	 	a.MngrRootCauseID
							,MngerID = a. MngerID
							,MngerPosition = a.MngerPosition

						into #TempViolators 		
						FROM dbo.View_Violation a with (nolock)
						WHERE a. ViolationDate BETWEEN  @pFrom and @pTo and a.Brand = isnull(@pBrandID,'') 
							AND ((isnull(a.VesselCode  ,'') = isnull(@pVesselID ,'') and isnull(a.VesselCode  ,'')  <> '')  or isnull(@pVesselID,'')= '')
							AND ((isnull(a.CostCenterCode,'') =  isnull(@pCostcenter,'' ) and isnull(@pCostcenter,'' ) <> '') or isnull(@pCostcenter,'') = '')
							AND ((isnull(a.JobCode,'') =  isnull(@pJobeCode,'') and  isnull(@pJobeCode,'') <> '')or isnull(@pJobeCode,'') = '')
  
						SELECT  DISTINCT  
									A.ViolatorsID
									,ViolationDate = CONVERT(varchar(11),a.ViolationDate,101)
									,EmployeeID
									,LastName = isnull(emp.colLastNameVarchar,'')
									,FirstName = Isnull(emp.colFirstNameVarchar,'')
									,MiddleName = ISNULL(emp.colMiddleNameVarchar, '')
									,Ship 
									,CostCenter = a.CostCenterCode
									,JobCode = a.JobCode
									,ViolationTypeID = d.ViolationTypeID
									,violationType = UPPER(d.ViolationType)
									,RootCauseCategory = UPPER(c.RootCause)
									,a.RootCauseID  
									,a.RootCauseComment 
									,a.CorrectiveAction  
									,a.RootCauseCategoryID 
									,a.MngerID  
									,a.MngerPosition  
									,MngerName =  (Mangemp.colLastNameVarchar  +  ' ' + Mangemp.colFirstNameVarchar  +  ' ' + ISNULL(Mangemp.colMiddleNameVarchar,  + CHAR(39) + CHAR(39)   ))
									,A.IsLock  
									,IsManager 
									,a.MngrRootCauseID  
									,MngrRootCause = mc.RootCause
									,MngrRootCauseCode = mc.RootCauseCode
						into #Violators
						FROM #TempViolators a
							LEFT JOIN RootCause c with (nolock) ON a.RootCauseID = c.RootCauseID
							LEFT JOIN ViolationType d with (nolock) ON a.Status = d.ViolationTypeID
							JOIN Travelmart.dbo.TblSeafarer emp with (nolock) ON a.EmployeeID = emp.colSeafarerIDint	
							left join Travelmart.dbo.TblSeafarer Mangemp with (nolock) ON a.MngerID = Mangemp.colSeafarerIDint	
							LEFT JOIN RootCause mc with (nolock) ON a.MngrRootCauseID = mc.RootCauseID
						ORDER BY LastName--, ViolationDate  
 
   
						select * from #Violators
 

						SELECT DISTINCT C.RootCauseID,
										C.ViolationTypeID,
										C.RootCauseCode,
										upper(C.RootCause) as RootCause,
										upper(V.ViolationType) as ViolationType
						FROM RootCause C with (nolock)
							inner join ViolationType V with (nolock) on C.ViolationTypeID = V.ViolationTypeID and V.IsActive = 1
						WHERE C.IsActive = 1
						order by C.RootCauseCode



                ";
		public static string Sel_AllViolation { get { return Select_AllViolation; } }
		#endregion Select All Active Violation

		#region start save comment
		private const string save_Comment = @"

				if IsNull(@pRootCauseCategoryID,0) = 0 begin

						-- Insert statements for procedure here
						INSERT INTO [dbo].[RootCauseComments]
							   ([ViolatorsID]
							   ,[ViolationTypeID]
							   ,[RootCauseID]
							   ,[RootCauseComment]
							   ,[CorrectiveActionTaken]
							   ,[MngerID]
							   ,[MngerPosition]
							   ,[CreatedBy]
							   ,[MngrRootCauseID]
							   ,[DateCreated])
						 VALUES
							   (@pViolatorsID  
							   ,@pViolationTypeID   
							   ,@pRootCauseID   
							   ,@pRootCauseComment 
							   ,@pCorrectiveActionTaken  
							   ,@pMngerID    
							   ,@pMngerPosition 
							   ,@pUserID    
							   ,@pMngrRootCauseID
							   ,GETDATE() )
						set @pRootCauseCategoryID = @@IDENTITY	   
					end
					else begin
						UPDATE [MLC].[dbo].[RootCauseComments]
						   SET [ViolatorsID] = @pViolatorsID
							  ,[ViolationTypeID] = @pViolationTypeID
							  ,[RootCauseID] = @pRootCauseID 
							  ,[RootCauseComment] = @pRootCauseComment 
							  ,[CorrectiveActionTaken] = @pCorrectiveActionTaken 
							  ,[MngerID] = @pMngerID
							  ,[MngerPosition] = @pMngerPosition
							  ,[DateModified] = GETDATE()
							  ,[ModifiedBy] =@pUserID
							  ,[MngrRootCauseID] = @pMngrRootCauseID
						 WHERE RootCauseCategoryID = @pRootCauseCategoryID
					end

					select @pRootCauseCategoryID as RootCauseCategoryID

		";
		public static string SaveComment { get { return save_Comment; } }
		#endregion start save comment

		#region start delete comment
		private const string delete_Comment = @"
					delete from [dbo].[RootCauseComments]
					WHERE RootCauseCategoryID = @pRootCauseCategoryID
					select @@Rowcount as RowAffected
				";
		public static string Del_Comment { get { return delete_Comment; } }
		#endregion end delete comment


		#region Select Active Rest hour 
		const string Select_RestHours = @"

				declare @pViolationDate date, @pEmployeeId bigInt

				select top 1 @pViolationDate = ViolationDate, @pEmployeeId = EmployeeID
				from Violators where ViolatorsID = @pViolatorsID

				CREATE TABLE #TempViolationShift
				(
					EmployeeID bigint,
					ViolationDate date,
					ViolatorsID int,
					GroupID bigint
				)
				create Index #X_ViolatorsID on #TempViolationShift(ViolatorsID)
 
				insert into #TempViolationShift
				select * from 
				(
					select top 1 V.EmployeeID
						,V.ViolationDate
						, V.ViolatorsID
						,@pViolatorsID as GroupID
					from Violators  V with  (nolock)
					where V.EmployeeID = @pEmployeeId and V.ViolationDate <  @pViolationDate
						and datediff(d,V.ViolationDate, @pViolationDate) = 1
					order by V.ViolatorsID desc
					union  
					select V.EmployeeID
						,V.ViolationDate
						,V.ViolatorsID
						,V.ViolatorsID as GroupID
					from Violators V
					where V.ViolatorsID = @pViolatorsID
					union   
					select top 1
						V.EmployeeID
						,V.ViolationDate
						,V.ViolatorsID
						,@pViolatorsID  as GroupID
					from Violators  V with  (nolock)
					where V.EmployeeID = @pEmployeeId and V.ViolationDate >  @pViolationDate
						and datediff(d, @pViolationDate,V.ViolationDate) = 1
					order by V.ViolatorsID desc
				) as V order by EmployeeID, ViolationDate
		
				;WITH X as (
						SELECT DISTINCT a.ViolatorsId ,
								a.ShiftNum,
								S.EmployeeID,
								S.GroupID,
								S.ViolationDate ,
								Details = STUFF((SELECT ',' + ShiftValue
											FROM ShiftHistory b
											WHERE b.ViolatorsID = a.ViolatorsID
													AND b.ShiftNum = a.ShiftNum
									FOR XML PATH ('')),1,1,'')
						FROM ShiftHistory a with (nolock)
							inner join #TempViolationShift S on a.ViolatorsID = S.ViolatorsID
				)
				SELECT   distinct a.ViolatorsID,
						a.EmployeeID,
						A.GroupID,
						a.ViolationDate ,
						Detail = STUFF((SELECT '^' + Details
										FROM X b
										WHERE b.ViolatorsID = a.ViolatorsID
								FOR XML PATH ('')),1,1,'')
				into #ViolationShift  
				FROM X a
				GROUP BY a.EmployeeID,a.GroupID,a.ViolatorsID,a.ViolationDate
				ORDER BY a.EmployeeID,a.GroupID,a.ViolationDate desc,a.ViolatorsID

				select * from #ViolationShift
				order by ViolationDate asc;

				select top 1 R.Detail from #ViolationShift R
				order by len(R.Detail) desc;

				--RECONFIGURE
				--WITH OVERRIDE;

                ";
		public static string Sel_RestHours { get { return Select_RestHours; } }
		#endregion End Active Rest hour 



	}
}
