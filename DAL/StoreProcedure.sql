
CREATE PROC GetDataSemester
AS
	BEGIN
		SELECT ID as N'Mã học kỳ', 
            semesterName as N'Tên học kỳ', 
            coefficient as N'Hệ số' 
		FROM Semester s ;
	END;
GO


    CREATE PROCEDURE FindSemester
    @STR NVARCHAR(100)
AS
    BEGIN
		SELECT ID as N'Mã học kỳ', semesterName as N'Tên học kỳ', coefficient as N'Hệ số' 
		FROM Semester s
    WHERE semesterName LIKE '%' + @STR + '%'
END;

GO
	
CREATE PROC GetDataTypeOfPoint
AS 
	Begin
		SELECT ID as N'Mã loại điểm', 
        pointName as N'Tên loại điểm', 
        coefficient as N'Hệ số'  
		FROM TypeOfPoint;
	END

GO

    CREATE PROCEDURE FindTypeOfPoint
    @STR NVARCHAR(100)
AS
BEGIN
	SELECT ID as N'Mã loại điểm', 
		pointName as N'Tên loại điểm', 
		coefficient as N'Hệ số'  
	FROM TypeOfPoint
    WHERE pointName LIKE '%' + @STR + '%'
END
	
    GO
	
--	EXECUTE GetDataSemester
--	EXEC GetDataTypeOfPoint
	
CREATE PROCEDURE FindCapacity
    @STR NVARCHAR(100)
AS
BEGIN
SELECT ID as N'Mã học lực', 
       capacityName AS N'Tên hạnh kiểm', 
       upperLimit AS N'Điểm cận trên', 
       lowerLimit AS N'Điểm cận dưới', 
       paraPoint AS N'Điểm khống chế'
FROM Capacity
    WHERE capacityName LIKE '%' + @STR + '%'
END

GO

CREATE PROCEDURE DeleteTypeOfPoint
    @pointName NVARCHAR(100)
AS
BEGIN
DELETE FROM Point
WHERE typeofpointID IN (
    SELECT ID
    FROM TypeOfPoint
    WHERE pointName = @pointName
);

DELETE FROM TypeOfPoint
WHERE pointName = @pointName;

END


--EXEC FindCapacity 'k' 

GO 

CREATE PROCEDURE FindConduct
    @STR NVARCHAR(100)
AS
BEGIN
SELECT ID as N'Mã hạnh kiểm', conductName AS 'Tên hạnh kiểm', 
        lowerLimit AS 'Điểm cận dưới', upperLimit AS 'Điểm cận trên' 
    FROM Conduct
    WHERE conductName LIKE '%' + @STR + '%'
END 
--EXEC FindConduct  'k' 

GO

CREATE PROCEDURE FindSubject
    @STR NVARCHAR(100)
AS
BEGIN
SELECT ID as N'Mã môn học', subjectName as 'Tên môn học'
    FROM Subject
    WHERE subjectName LIKE '%' + @STR + '%'
END 


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

GO

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

  GO

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

GO

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



--##########################################################################################################################


SELECT * from AcademicYear ay 
SELECT * FROM Student s 
select * from TypeOfPoint top2 
select * from Semester s2 
select * from Class c 


GO	
	
	
-----------------------------------------------------------------------------------------------------
CREATE OR ALTER  PROC DATA_POINT_STUDENT
    @academicyearName NVARCHAR(255),
    @semesterName NVARCHAR(255),
    @className NVARCHAR(255),
    @studentID INT
AS
BEGIN

    SELECT  s.subjectName AS N'Môn Học',
        p.point AS N'Điểm đánh giá thường xuyên',
        p2.point AS N'Điểm giữa kỳ',
        p3.point AS N'Điểm cuối kỳ',
        p4.point AS N'Điểm trung bình môn'
    FROM Subject s
        INNER JOIN Student s2
            ON s2.ID = @studentID
        LEFT JOIN Point p
            ON p.subjectID = s.ID
               AND p.typeofpointID =
               (
                   SELECT TOP 1
                       top2.ID
                   FROM TypeOfPoint top2
                   WHERE top2.pointName = N'Điểm đánh giá thường xuyên'
               )
               AND p.academicyearID =
               (
                   SELECT TOP 1
                       ay.ID
                   FROM AcademicYear ay
                   WHERE ay.academicyearName = @academicyearName
               )
               AND p.semesterID =
               (
                   SELECT TOP 1 s3.ID FROM Semester s3 WHERE s3.semesterName = @semesterName
               )
               AND p.classID = 
               (
               		SELECT TOP 1 c.ID FROM Class c WHERE c.className = @className
               )
               AND p.studentID = s2.ID
        LEFT JOIN Point p2
            ON p2.subjectID = s.ID
               AND p2.typeofpointID =
               (
                   SELECT TOP 1
                       top2.ID
                   FROM TypeOfPoint top2
                   WHERE top2.pointName = N'Điểm giữa kỳ'
               )
               AND p2.academicyearID =
               (
                   SELECT TOP 1
                       ay.ID
                   FROM AcademicYear ay
                   WHERE ay.academicyearName = @academicyearName
               )
               
               AND p2.classID = 
               (
               		SELECT TOP 1 c.ID FROM Class c WHERE c.className = @className
               )
               AND p2.semesterID =
               (
                   SELECT TOP 1 s3.ID FROM Semester s3 WHERE s3.semesterName = @semesterName
               )
               AND p2.studentID = s2.ID 
        LEFT JOIN Point p3 ON p3.subjectID = s.ID
        AND p3.typeofpointID = (
            SELECT TOP 1 top2.ID
            FROM TypeOfPoint top2
            WHERE top2.pointName = N'Điểm cuối kỳ'
        )
        AND p3.academicyearID = (
            SELECT TOP 1 ay.ID
            FROM AcademicYear ay
            WHERE ay.academicyearName = @academicyearName
        )
        AND p3.semesterID = (
            SELECT TOP 1 s3.ID
            FROM Semester s3
            WHERE s3.semesterName = @semesterName
        )
        AND p3.classID = (
            SELECT TOP 1 c.ID
            FROM Class c
            WHERE c.className = @className
        )
        AND p3.studentID = s2.ID
    LEFT JOIN Point p4 ON p4.subjectID = s.ID
        AND p4.typeofpointID = (
            SELECT TOP 1 top2.ID
            FROM TypeOfPoint top2
            WHERE top2.pointName = N'Điểm trung bình môn'
        )
        AND p4.academicyearID = (
            SELECT TOP 1 ay.ID
            FROM AcademicYear ay
            WHERE ay.academicyearName = @academicyearName
        )
        AND p4.semesterID = (
            SELECT TOP 1 s3.ID
            FROM Semester s3
            WHERE s3.semesterName = @semesterName
        )
        AND p4.classID = (
            SELECT TOP 1 c.ID
            FROM Class c
            WHERE c.className = @className
        )
        AND p4.studentID = s2.ID
END
EXEC DATA_POINT_STUDENT N'Năm học 2023 - 2024', N'Học kỳ 2', N'Lớp 10A1', 1


-----------------------------------------------------------------------------------------------------

INSERT INTO Point  (studentID, typeofpointID, subjectID, academicyearID, semesterID, point)
VALUES (1, 1, 1, 1, 1, 9);