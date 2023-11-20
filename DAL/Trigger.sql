
--Trigger Cho bảng học sinh
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


--Update Student set gender = N'Nữ' where id = 204
--SELECT * from Student s where id = 204
--Update Student set numberPhone = '0392932776' where id = 204
--INSERT INTO Student (gender, name, birthday, numberPhone, email, birthplace, [address], image)
--VALUES 
--    ('Male', 'John Doe', '1990-01-01', '0123456789', 'john.doe@example.com', 'City A', '123 Main St', 'image.jpg'),
--    ('Female', 'Jane Smith', '1995-05-10', '0987654321', 'jane.smith@example.com', 'City B', '456 Elm St', 'image.jpg'),
--    ('Male', 'David Johnson', '1988-09-15', '0111222333', 'david.johnson@example.com', 'City C', '789 Oak St', 'image.jpg');

CREATE TRIGGER INSERT_NEW_PARENT
ON Parent
AFTER INSERT 
AS
BEGIN
    UPDATE Parent 
    SET createDate = DATEADD(HOUR, 7, GETUTCDATE())
    FROM Parent
    INNER JOIN inserted ON Parent.studentID = inserted.studentID AND Parent.gender = inserted.gender;
END;

CREATE TRIGGER UPDATE_PARENT
ON Parent
AFTER UPDATE
AS
BEGIN
    UPDATE Parent
    SET updateDate = DATEADD(HOUR, 7, GETUTCDATE())
    FROM Parent
    INNER JOIN inserted ON Parent.studentID = inserted.studentID AND Parent.gender = inserted.gender;
END;


/*
delete parent
INSERT INTO Parent (studentID, name, birthday, gender, numberPhone, address, image)
VALUES 
    (204, 'John Doe', '1975-08-15', 'Male', '0123456789', '123 Main St', 'image.jpg'),
    (203, 'Jane Smith', '1980-05-20', 'Female', '0987654321', '456 Elm St', 'image.jpg'),
    (202, 'David Johnson', '1978-12-10', 'Male', '0111222333', '789 Oak St', 'image.jpg');

        
Update Parent set gender = N'Nữ' where studentID = 204 and name = 'John Doe'
*/
CREATE TRIGGER INSERT_NEW_STUDENT
ON Student
AFTER INSERT
AS
BEGIN
    UPDATE Student 
    SET createDate = DATEADD(HOUR, 7, GETUTCDATE()) 
    WHERE ID IN (SELECT ID FROM inserted);
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
END;
