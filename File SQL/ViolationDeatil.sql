
declare @pViolatorsID bigint = 1425103

declare @pViolationDate date, @pEmployeeId bigInt
				select top 1 @pViolationDate = ViolationDate, @pEmployeeId = EmployeeID
				from Violators where ViolatorsID = @pViolatorsID

					if Object_Id('tempdb..#TempViolationShift') is not null begin
						drop table #TempViolationShift

					end
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
		

				if Object_Id('tempdb..#ViolationShift') is not null begin
						drop table #ViolationShift

				end
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
				order by ViolationDate asc

				select top 1 R.Detail from #ViolationShift R
				order by len(R.Detail) desc

--RECONFIGURE
--WITH OVERRIDE
