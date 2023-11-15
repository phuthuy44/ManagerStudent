
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



  CREATE PROCEDURE UpdatePoint
  @studentID INT,
  @academicyearName NVARCHAR(255),
  @semesterName NVARCHAR(255),
  @subjectName NVARCHAR(255),
  @regularPoint FLOAT,
  @midtermPoint FLOAT,
  @finalPoint FLOAT
AS
BEGIN
  UPDATE Point
  SET point = CASE
                WHEN TypeOfPoint.pointName = N'Điểm đánh giá thường xuyên' THEN @regularPoint
                WHEN TypeOfPoint.pointName = N'Điểm giữa kỳ' THEN @midtermPoint
                WHEN TypeOfPoint.pointName = N'Điểm cuối kỳ' THEN @finalPoint
              END
  FROM Point
  INNER JOIN TypeOfPoint ON Point.typeofpointID = TypeOfPoint.ID
  INNER JOIN AcademicYear ON Point.academicyearID = AcademicYear.ID AND AcademicYear.academicyearName = @academicyearName
  INNER JOIN Semester ON Point.semesterID = Semester.ID AND Semester.semesterName = @semesterName
  INNER JOIN Subject ON Point.subjectID = Subject.ID AND Subject.subjectName = @subjectName
  WHERE Point.studentID = @studentID
END


--Sửa 1 trong 3 cột điểm

--SELECT * FROM Point WHERE studentID = 44

--EXEC UpdatePoint 44, N'2023-2024', N'Học kỳ 1', N'Tiếng anh', 4, 5, 6


CREATE PROC InsertPoint
  @studentID INT,
  @academicyearName NVARCHAR(255),
  @semesterName NVARCHAR(255),
  @subjectName NVARCHAR(255),
  @pointName NVARCHAR(255),
  @point FLOAT
AS
BEGIN
    DECLARE @academicYearID INT
    DECLARE @semesterID INT
    DECLARE @subjectID INT
    DECLARE @pointTypeID INT

    -- Lấy ID của AcademicYear
    SELECT @academicYearID = ID
    FROM AcademicYear
    WHERE academicyearName = @academicyearName

    -- Lấy ID của Semester
    SELECT @semesterID = ID
    FROM Semester
    WHERE semesterName = @semesterName

    -- Lấy ID của Subject
    SELECT @subjectID = ID
    FROM Subject
    WHERE subjectName = @subjectName

    -- Lấy ID của TypeOfPoint
    SELECT @pointTypeID = ID
    FROM TypeOfPoint
    WHERE pointName = @pointName

    -- Kiểm tra xem có tồn tại loại điểm tương ứng không
    IF @pointTypeID IS NULL
    BEGIN
        RAISERROR('Invalid point type: %s', 16, 1, @pointName)
        RETURN
    END

    -- Thực hiện việc chèn điểm vào bảng Point
    INSERT INTO Point (studentID, academicyearID, semesterID, subjectID, typeofpointID, point)
    VALUES (@studentID, @academicYearID, @semesterID, @subjectID, @pointTypeID, @point)
END


--EXEC InsertPoint @studentID, @academicyearName, @semesterName, @subjectName, @pointName, @point 

--EXEC InsertPoint 26, N'2023-2024', N'Học kỳ 1', N'Toán', N'Điểm giữa kỳ', 8