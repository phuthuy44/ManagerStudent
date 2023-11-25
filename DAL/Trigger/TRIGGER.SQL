CREATE TRIGGER INSERT_NEW_PARENT
ON Parent
AFTER INSERT 
AS
BEGIN
    UPDATE Parent 
    SET createDate = DATEADD(HOUR, 7, GETUTCDATE())
    FROM Parent
    INNER JOIN inserted ON Parent.studentID = inserted.studentID;
END;

CREATE TRIGGER INSERT_NEW_STUDENT
ON Student
AFTER INSERT
AS
BEGIN
    UPDATE Student 
    SET createDate = DATEADD(HOUR, 7, GETUTCDATE()) 
    WHERE ID IN (SELECT ID FROM inserted);

    -- Kiểm tra tính hợp lệ của số điện thoại
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE ISNUMERIC(numberPhone) = 0 OR LEN(numberPhone) <> 10
    )
    BEGIN
        RAISERROR('Số điện thoại không hợp lệ', 16, 1);
        ROLLBACK;
    END;
END;

CREATE  TRIGGER SET_FINAL_POINT
ON Point
AFTER INSERT, UPDATE
AS
BEGIN
    IF NOT EXISTS (
        SELECT *
        FROM inserted i
        INNER JOIN Point p ON p.studentID = i.studentID
            AND p.typeofpointID = (
                SELECT TOP 1 top2.ID 
                FROM TypeOfPoint top2 
                WHERE top2.pointName =  N'Điểm trung bình môn'
            )
            AND p.subjectID = i.subjectID
            AND p.semesterID = i.semesterID 
            AND p.academicyearID = i.academicyearID
            AND p.classID = i.classID
    )
    BEGIN 
        INSERT INTO Point (studentID, typeofpointID, subjectID, academicyearID, semesterID, classID, point)
        SELECT 
            i.studentID,
            (
                SELECT TOP 1 top3.ID 
                FROM TypeOfPoint top3 
                WHERE top3.pointName = N'Điểm trung bình môn'
            ),
            i.subjectID, 
            i.academicyearID, 
            i.semesterID, 
            i.classID,
            (
                p.point * (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p.typeofpointID 
                ) +  p2.point * (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p2.typeofpointID 
                ) + p3.point * (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p3.typeofpointID 
                )
            ) / (
                (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p.typeofpointID 
                ) +   (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p2.typeofpointID 
                ) + (
                    SELECT TOP 1 top4.coefficient 
                    FROM TypeOfPoint top4 
                    WHERE top4.ID = p3.typeofpointID 
                )
            )
        FROM inserted i
        LEFT JOIN Point p ON p.studentID = i.studentID
            AND p.typeofpointID = (
                SELECT TOP 1 top2.ID 
                FROM TypeOfPoint top2 
                WHERE top2.pointName = N'Điểm đánh giá thường xuyên'
            )
            AND p.subjectID = i.subjectID
            AND p.semesterID = i.semesterID 
            AND p.academicyearID = i.academicyearID
            AND p.classID = i.classID
        LEFT JOIN Point p2 ON p2.studentID = i.studentID
            AND p2.typeofpointID = (
                SELECT TOP 1 top2.ID 
                FROM TypeOfPoint top2 
                WHERE top2.pointName = N'Điểm giữa kỳ'
            )
            AND p2.subjectID = i.subjectID
            AND p2.semesterID = i.semesterID 
            AND p2.academicyearID = i.academicyearID
            AND p2.classID = i.classID
        LEFT JOIN Point p3 ON p3.studentID = i.studentID
            AND p3.typeofpointID = (
                SELECT TOP 1 top2.ID 
                FROM TypeOfPoint top2 
                WHERE top2.pointName = N'Điểm cuối kỳ'
            )
            AND p3.subjectID = i.subjectID
            AND p3.semesterID = i.semesterID 
            AND p3.academicyearID = i.academicyearID
            AND p3.classID = i.classID;
    END
    ELSE
    --Ngược lại sẽ update
    BEGIN
       	DECLARE @tmp1 DECIMAL
        DECLARE @tmp3 DECIMAL
        DECLARE @tmp2 DECIMAL

        -- Lấy hệ số của các điểm
        SELECT @tmp1 = top2.coefficient 
        FROM TypeOfPoint top2 
        WHERE top2.pointName = N'Điểm đánh giá thường xuyên'

        SELECT @tmp2 = top2.coefficient 
        FROM TypeOfPoint top2 
        WHERE top2.pointName = N'Điểm giữa kỳ'

        SELECT @tmp3 = top2.coefficient 
        FROM TypeOfPoint top2 
        WHERE top2.pointName = N'Điểm cuối kỳ'

        DECLARE @tmp1point DECIMAL
        DECLARE @tmp3point DECIMAL
        DECLARE @tmp2point DECIMAL

        -- Lấy dữ liệu cần thiết từ Point và inserted vào các biến tạm thời
        SELECT @tmp1point = p.point 
        FROM Point p 
        INNER JOIN inserted i ON i.studentID = p.studentID 
        AND p.typeofpointID = (
            SELECT TOP 1 top2.ID 
            FROM TypeOfPoint top2 
            WHERE top2.pointName = N'Điểm đánh giá thường xuyên'
        )
        AND p.subjectID = i.subjectID
        AND p.semesterID = i.semesterID 
        AND p.academicyearID = i.academicyearID
        AND p.classID = i.classID;

        SELECT @tmp2point = p.point 
        FROM Point p 
        INNER JOIN inserted i ON i.studentID = p.studentID 
        AND p.typeofpointID = (
            SELECT TOP 1 top2.ID 
            FROM TypeOfPoint top2 
            WHERE top2.pointName = N'Điểm giữa kỳ'
        )
        AND p.subjectID = i.subjectID
        AND p.semesterID = i.semesterID 
        AND p.academicyearID = i.academicyearID
        AND p.classID = i.classID;

        SELECT @tmp3point = p.point 
        FROM Point p 
        INNER JOIN inserted i ON i.studentID = p.studentID 
        AND p.typeofpointID = (
            SELECT TOP 1 top2.ID 
            FROM TypeOfPoint top2 
            WHERE top2.pointName = N'Điểm cuối kỳ'
        )
        AND p.subjectID = i.subjectID
        AND p.semesterID = i.semesterID 
        AND p.academicyearID = i.academicyearID
        AND p.classID = i.classID;

        -- Cập nhật điểm cuối kỳ trong Point
        UPDATE p
        SET p.point = (
                @tmp1point * @tmp1 + @tmp2point * @tmp2 + @tmp3point * @tmp3
            ) / (
                @tmp1 + @tmp2 + @tmp3
            )
        FROM Point p 
        INNER JOIN inserted i ON i.studentID = p.studentID 
        AND p.typeofpointID = (
            SELECT TOP 1 top2.ID 
            FROM TypeOfPoint top2 
            WHERE top2.pointName = N'Điểm trung bình môn'
        )
        AND p.subjectID = i.subjectID
        AND p.semesterID = i.semesterID 
        AND p.academicyearID = i.academicyearID
        AND p.classID = i.classID;
    END
    END;

CREATE TRIGGER UPDATE_PARENT
ON Parent
AFTER UPDATE
AS
BEGIN
    UPDATE Parent
    SET updateDate = DATEADD(HOUR, 7, GETUTCDATE())
    FROM Parent
    INNER JOIN inserted ON Parent.studentID = inserted.studentID;
END;

CREATE TRIGGER UPDATE_STUDENT
ON Student
AFTER UPDATE
AS
BEGIN
    UPDATE Student
    SET updateDate = DATEADD(HOUR, 7, GETUTCDATE())
    FROM Student
    INNER JOIN inserted ON Student.ID = inserted.ID;

    -- Kiểm tra tính hợp lệ của số điện thoại
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE ISNUMERIC(numberPhone) = 0 OR LEN(numberPhone) <> 10
    )
    BEGIN
        RAISERROR('Số điện thoại không hợp lệ', 16, 1);
        ROLLBACK;
    END;
END;