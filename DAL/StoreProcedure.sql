
CREATE PROC GetDataSemester
AS
	BEGIN
		SELECT * FROM Semester s ;
	END
	
CREATE PROC GetDataTypeOfPoint
AS 
	Begin
		SELECT * FROM TypeOfPoint top2 ;
	END
	

	
--	EXECUTE GetDataSemester
--	EXEC GetDataTypeOfPoint
	
CREATE PROCEDURE FindCapacity
    @STR NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Capacity c
    WHERE capacityName LIKE '%' + @STR + '%'
END



--EXEC FindCapacity 'k' 


CREATE PROCEDURE FindConduct
    @STR NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Conduct
    WHERE conductName LIKE '%' + @STR + '%'
END
--EXEC FindConduct  'k' 




--
--SELECT * 
--FROM StudentClassSemesterAcademicYear scsay 
--LEFT JOIN Student s ON s.ID = scsay.studentID 


/*
SELECT * FROM TypeOfPoint top2 WHERE top2.pointName = N'Điểm đánh giá thường xuyên'

*/

--SELECT * FROM StudentClassSemesterAcademicYear scsay 
--SELECT * FROM Point p 
--	WHERE p.studentID = 71
--ORDER BY createDate 
--SELECT * FROM TypeOfPoint top2 

--UPDATE StudentClassSemesterAcademicYear
--SET gradeID = 
--    CASE 
--        WHEN classID <= 3 THEN 1
--        WHEN classID <= 6 THEN 2
--        WHEN classID <= 9 THEN 3
--        ELSE gradeID
--END



CREATE  PROC DATA_POINT
  @academicyearName NVARCHAR(255),
  @semesterName NVARCHAR(255),
  @className NVARCHAR(255),
  @subjectName NVARCHAR(255)
  AS
  BEGIN
SELECT 
    s.ID,
    s.name,
    dp1.point AS N'Điểm đánh giá thường xuyên',
    dp2.point AS N'Điểm giữa kỳ',
    dp3.point AS N'Điểm cuối kỳ',  
    ay.academicyearName ,
    s2.semesterName ,
    c.className ,
    s3.subjectName 
FROM  
    Student s 
    LEFT JOIN AcademicYear ay ON ay.academicyearName = @academicyearName
    LEFT JOIN Semester s2 ON s2.semesterName = @semesterName
    LEFT JOIN Class c ON c.className = @className
    LEFT JOIN Subject s3 ON s3.subjectName = @subjectName
    INNER JOIN StudentClassSemesterAcademicYear scsay ON scsay.classID = c.ID  
     AND scsay.studentID = s.ID 
     AND scsay.semesterID = s2.ID 
     AND scsay.academicyearID = ay.ID
    OUTER APPLY (
        -- bảng ảo cho Điểm đánh giá thường xuyên
        SELECT TOP 1 p.point
        FROM Point p
        INNER JOIN TypeOfPoint top2 ON p.typeofpointID = top2.ID
        WHERE top2.pointName = N'Điểm đánh giá thường xuyên'
            AND s.ID = p.studentID 
            AND p.academicyearID = ay.ID 
            AND s2.ID = p.semesterID 
            AND p.subjectID = s3.ID 
        ORDER BY p.updateDate 
    ) AS dp1
    OUTER APPLY (
        -- bảng ảo cho Điểm giữa kỳ
        SELECT TOP 1 p.point
        FROM Point p
        INNER JOIN TypeOfPoint top2 ON p.typeofpointID = top2.ID
        WHERE top2.pointName = N'Điểm giữa kỳ'
            AND s.ID = p.studentID 
            AND p.academicyearID = ay.ID 
            AND s2.ID = p.semesterID 
            AND p.subjectID = s3.ID 
        ORDER BY p.updateDate 
    ) AS dp2
    OUTER APPLY (
        -- bảng ảo cho Điểm cuối kỳ
        SELECT TOP 1 p.point
        FROM Point p
        INNER JOIN TypeOfPoint top2 ON p.typeofpointID = top2.ID
        WHERE top2.pointName = N'Điểm cuối kỳ'
            AND s.ID = p.studentID 
            AND p.academicyearID = ay.ID 
            AND s2.ID = p.semesterID
            AND p.subjectID = s3.ID 
        ORDER BY p.updateDate 
    ) AS dp3  
  
  END
  

   
  --EXEC DATA_POINT N'Năm học 2023 - 2024', N'Học kỳ 1', N'Lớp 10A2', N'Toán'