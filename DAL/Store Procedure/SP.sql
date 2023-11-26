CREATE  PROC DATA_POINT
  @academicyearName NVARCHAR(255),
  @semesterName NVARCHAR(255),
  @className NVARCHAR(255),
  @subjectName NVARCHAR(255)
  AS
  BEGIN
SELECT 
    s.ID AS N'Mã học sinh',
    s.name AS N'Tên học sinh',
    dp1.point AS N'Điểm đánh giá thường xuyên',
    dp2.point AS N'Điểm giữa kỳ',
    dp3.point AS N'Điểm cuối kỳ' 
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
        -- Thực hiện stored procedure và chuyển kết quả thành bảng ảo cho Điểm đánh giá thường xuyên
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
  
  END;

CREATE  PROC DATA_POINT_STUDENT
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
END;

CREATE PROC DATA_POINT_TYPE
	@NAMEPOINT NVARCHAR(255),
	@STUDENTID INT
AS
BEGIN
	SELECT TOP 1 p.point
        FROM Point p, TypeOfPoint top2
        WHERE p.typeofpointID = top2.ID
        	AND top2.pointName = @NAMEPOINT
        	AND @STUDENTID = p.studentID 
        ORDER BY p.updateDate 
END;

CREATE    PROC FINAL_RESULT
    @academicyearName NVARCHAR(255),
	@studentID INT
	AS
	BEGIN
		DECLARE @academicyearID INT
		DECLARE @typeofpointID INT
		DECLARE @semesterID1 INT
		DECLARE @semesterID2 INT
		DECLARE @coefficient1 INT
		DECLARE @coefficient2 INT
		DECLARE @minpoint DECIMAL(10, 2)
		DECLARE @avgpoint DECIMAL(10, 2)
		DECLARE @capacityName NVARCHAR(255)
		DECLARE @conductName NVARCHAR(255)
		
		SELECT @academicyearID = ay.ID 
		FROM AcademicYear ay 
		WHERE ay.academicyearName = @academicyearName
		
		SELECT @typeofpointID = top2.ID
		FROM TypeOfPoint top2 
		WHERE top2.pointName =  N'Điểm trung bình môn'
		
		SELECT @semesterID1 = s.ID ,@coefficient1 = s.coefficient 
		FROM Semester s 
		WHERE s.semesterName = N'Học kỳ 1'
		
		SELECT @semesterID2 = s.ID ,@coefficient2 = s.coefficient 
		FROM Semester s 
		WHERE s.semesterName = N'Học kỳ 2'
		
		
	
	    SELECT @avgpoint = AVG(resultpoint) , @minpoint = MIN(resultpoint)  FROM 
	    (
		    SELECT (p1.point*@coefficient1 + p2.point*@coefficient2)/(@coefficient1+@coefficient2) AS resultpoint
		    FROM 
		    (
			    SELECT p.studentID , p.subjectID ,p.academicyearID ,p.semesterID ,p.point 
			    FROM Point p 
			    --Đặt biến
			    WHERE p.typeofpointID = @typeofpointID 
			    --Đặt biến
			    AND p.semesterID = @semesterID1 
			    --Truyền vào
			    AND p.academicyearID = @academicyearID
			    --Truyền vào
			    AND p.studentID = @studentID
		    ) AS p1, 
		    (
			    
			    SELECT p.studentID , p.subjectID ,p.academicyearID ,p.semesterID ,p.point 
			    FROM Point p 
			    --Đặt biến
			    WHERE p.typeofpointID = @typeofpointID 
			    --Đặt biến
			    AND p.semesterID = @semesterID2
			    --Truyền vào
			    AND p.academicyearID = @academicyearID
			    --Truyền vào
			    AND p.studentID = @studentID
		    ) AS p2
			WHERE p1.studentID = p2.studentID 
			AND p1.subjectID = p2.subjectID
			AND p1.academicyearID = p2.academicyearID
		) AS res
		
		SELECT TOP 1 @capacityName = c.capacityName 
		FROM Capacity c 
		INNER JOIN Capacity c2 ON c.ID = c2.ID 
			AND c2.paraPoint < @minpoint
		WHERE  c.lowerLimit <= @avgpoint 
		
		SELECT top 1 @conductName = c.conductName 
			FROM StudentConduct sc  
			INNER JOIN Conduct c ON sc.point BETWEEN c.lowerLimit AND c.upperLimit
			WHERE sc.studentID = @studentID
			GROUP BY c.conductName
			--ORDER BY c.ID 
			HAVING AVG(sc.point) BETWEEN MIN(c.lowerLimit) AND MAX(c.upperLimit)
		SELECT N'Cả năm' AS N'Học kỳ', @conductName AS N'Hạnh Kiểm', @capacityName AS N'Học lực',  @avgpoint AS N'Điểm trung bình'
	END;

CREATE PROCEDURE FindCapacity
    @STR NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Capacity c
    WHERE capacityName LIKE '%' + @STR + '%'
END;

CREATE PROCEDURE FindConduct
    @STR NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Conduct
    WHERE conductName LIKE '%' + @STR + '%'
END;

CREATE PROC GetDataSemester
AS
	BEGIN
		SELECT * FROM Semester s ;
	END;

CREATE PROC GetDataTypeOfPoint
AS 
	Begin
		SELECT * FROM TypeOfPoint top2 ;
	END;

CREATE PROC SUMMARY_POINT_STUDENT
	@academicyearName NVARCHAR(255),
    @semesterName NVARCHAR(255),
    @className NVARCHAR(255),
    @studentID INT
AS
BEGIN
	SELECT  s.semesterName AS N'Học Kỳ',sc2.conductName AS N'Hạnh Kiểm' , sc.capacityName AS N'Học lực',  sc.point AS N'Điểm trung bình'
	FROM StudentManager.dbo.StudentCapacity sc  , StudentManager.dbo.StudentConduct sc2 ,
		StudentManager.dbo.Semester s 
	WHERE sc.studentID = @studentID
	AND sc.academicyearID = (
		SELECT TOP 1 ay.ID 
		FROM AcademicYear ay
		WHERE ay.academicyearName = @academicyearName
	)
	AND sc2.studentID = sc.studentID 
	AND sc2.academicyearID = sc.academicyearID 
	AND sc2.semesterID = sc.semesterID 
	AND s.ID = sc.semesterID 
	ORDER BY s.ID 
END;

CREATE   PROCEDURE UPDATE_INSERT_POINT
    @studentID INT,
    @academicyearName NVARCHAR(255),
    @semesterName NVARCHAR(255),
    @subjectName NVARCHAR(255),
    @className NVARCHAR(255),
    @pointName NVARCHAR(255),
    @point DECIMAL
AS
BEGIN
    DECLARE @academicYearID INT
    DECLARE @semesterID INT
    DECLARE @subjectID INT
    DECLARE @classID INT
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

    
    SELECT @classID = ID
    FROM Class c 
    WHERE c.className  = @className
    
    -- Lấy ID của TypeOfPoint
    SELECT @pointTypeID = ID
    FROM TypeOfPoint
    WHERE pointName =  @pointName

    -- Kiểm tra xem record đã tồn tại chưa
    IF NOT EXISTS (
        SELECT 1 
        FROM Point p
        WHERE p.studentID = @studentID
            AND p.typeofpointID = @pointTypeID
            AND p.academicyearID = @academicYearID
            AND p.semesterID = @semesterID
            AND p.subjectID = @subjectID
            AND p.classID = @classID
    )
    BEGIN
        -- Chưa tồn tại, thực hiện INSERT
        INSERT INTO Point (studentID, academicyearID, semesterID, subjectID, typeofpointID, classID ,point)
        VALUES (@studentID, @academicYearID, @semesterID, @subjectID, @pointTypeID, @classID, @point)
    END
    ELSE
    BEGIN
        -- Đã tồn tại, thực hiện UPDATE
        UPDATE Point
        SET point = @point
        WHERE studentID = @studentID
            AND typeofpointID = @pointTypeID
            AND academicyearID = @academicYearID
            AND semesterID = @semesterID
            AND subjectID = @subjectID
            AND classID = @classID
    END
END;