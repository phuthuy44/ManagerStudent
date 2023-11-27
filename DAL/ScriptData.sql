--use master;
--DROP database StudentManager;
--CREATE DATABASE StudentManager;

--USE StudentManager;
-- 1
--CREATE DATABASE ManagerStudent;

-- Tạo bảng hạnh kiểm
CREATE TABLE Conduct(
	--ID VARCHAR(8) NOT NULL,
	ID INT IDENTITY(1,1) NOT NULL,
	conductName NVARCHAR(100),
	upperLimit INT,
	lowerLimit INT,
	PRIMARY KEY (ID)
)

-- Tạo bảng học lực 
CREATE TABLE Capacity(

	ID INT IDENTITY(1,1) NOT NULL,
	capacityName NVARCHAR(100),
	upperLimit FLOAT,
	lowerLimit FLOAT,
	paraPoint FLOAT,
	PRIMARY KEY (ID)
)

-- Tạo bảng học kì
CREATE TABLE Semester (
	ID INT IDENTITY(1,1) NOT NULL,
	semesterName NVARCHAR(255),
	coefficient INT, -- Hệ số của học kì	
	startDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (ID)
)

-- Tạo bảng năm học
CREATE TABLE AcademicYear (
	ID INT IDENTITY(1, 1) NOT NULL,
	academicyearName NVARCHAR(255),
	startDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (ID)
)

-- Tạo bảng phân loại điểm
CREATE TABLE TypeOfPoint(
	ID INT IDENTITY(1, 1) NOT NULL,
	pointName NVARCHAR (255),
	coefficient INT, -- Hệ số của loại điểm
	PRIMARY KEY (ID)
)

--DROP TABLE Subject
-- Tạo bảng môn học
CREATE TABLE Subject(
	ID INT IDENTITY(1, 1) NOT NULL,
	subjectName NVARCHAR(255),
	PRIMARY KEY (ID)
)
	
-- Tạo bảng chức vụ
CREATE TABLE Position(
	ID INT IDENTITY(1, 1) NOT NULL,
	positionName NVARCHAR(255),
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (ID)
)

-- Tạo bảng học sinh
CREATE TABLE Student(
	ID INT IDENTITY(1, 1) NOT NULL,
	gender NVARCHAR(50),
	name NVARCHAR(255),
	birthday DATETIME,	
	numberPhone VARCHAR (11),
	email VARCHAR (255),
	birthplace NVARCHAR(255),
	[address] NVARCHAR(255),
	image VARCHAR(255),
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (ID)
)

-- Tạo bảng cha mẹ học sinh
CREATE TABLE Parent(
	studentID INT NOT NULL,
	name NVARCHAR(255),
	birthday DATETIME,
	gender NVARCHAR(50),
	numberPhone VARCHAR(11),
	address NVARCHAR(255),
	image VARCHAR(255),
	createDate DATETIME,
	PRIMARY KEY (studentID, gender)
)

ALTER TABLE Parent 
	ADD CONSTRAINT Parent_studentID_Student_ID 
	FOREIGN KEY (studentID) REFERENCES Student(ID)
	
-- Tạo bảng giáo viên
CREATE TABLE Teacher(
	ID INT IDENTITY(1, 1)  NOT NULL,
	teacherName NVARCHAR(255),
	gender NVARCHAR(50),
	birthday DATETIME,
	birthplace NVARCHAR(255),
	email VARCHAR(255),
	phonenumber INT,
	address NVARCHAR(255),
	image VARCHAR(255),
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (ID)	
)


-- Tạo bảng tài khoản 
CREATE TABLE Account(
	username VARCHAR(255) NOT NULL,
	password VARCHAR(255),
	teacherID INT NOT NULL,
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (username)
)

-- Thêm các khoá chính và khoá ngoại
ALTER TABLE [Account]  
	ADD CONSTRAINT Account_teacherID_Teacher_ID
	FOREIGN KEY (teacherID) REFERENCES Teacher(ID)

--Tạo bảng status
CREATE TABLE Status (
	ID INT IDENTITY(1, 1) NOT NULL,
	statusName NVARCHAR(255),
	isActive BIT --Dùng để phân biệt trạng thái. VD: 0 là trạng thái sai, 1 là trạng thái đúng
	PRIMARY KEY (ID)
)

CREATE TABLE AccountStatus(
	accountID VARCHAR(255) NOT NULL,
	statusID INT NOT NULL,
	PRIMARY KEY (accountID, statusID)
)

ALTER TABLE AccountStatus 
	ADD CONSTRAINT AccountStatus_accountID_Account_username
	FOREIGN KEY (accountID)
	REFERENCES Account(username)

ALTER TABLE AccountStatus 
	ADD CONSTRAINT AccountStatus_statusID_Status_ID
	FOREIGN KEY (statusID)
	REFERENCES Status(ID)
-- Tạo bảng khối lớp
CREATE TABLE Grade(
	ID INT IDENTITY(1, 1) NOT NULL,
	gradeName NVARCHAR(255),
	PRIMARY KEY (ID)	
)

CREATE TABLE SubjectOfTeacher(
	teacherID INT NOT NULL,
	subjectID INT NOT NULL,
	PRIMARY KEY (teacherID, subjectID)
)

ALTER TABLE SubjectOfTeacher 
	ADD CONSTRAINT SubjectOfTeacher_teacherID_Teacher_ID 
	FOREIGN KEY (teacherID) 
	REFERENCES Teacher(ID)
ALTER TABLE SubjectOfTeacher 
	ADD CONSTRAINT SubjectOfTeacher_subjectID_Subject_ID 
	FOREIGN KEY (subjectID) 
	REFERENCES Subject(ID)

-- Tạo bảng lớp học
CREATE TABLE Class(
	ID INT IDENTITY(1, 1) NOT NULL,
	className NVARCHAR(255),
	maxStudent INT,
	quantityStudent INT,
	PRIMARY KEY (ID)
)
--drop table assignment
--	Tạo bảng phân công
CREATE TABLE Assignment(
	teacherID INT NOT NULL,
	classID INT,
	semesterID INT,
	positionID INT,
	academicyearID INT,
	subjectID INT,
	PRIMARY KEY (teacherID, classID, semesterID, 
	positionID, academicyearID, subjectID)
)

-- Thêm các khoá chính và khoá ngoại
ALTER TABLE [Assignment]  
	ADD CONSTRAINT Assignment_teacherID_Teacher_ID
	FOREIGN KEY (teacherID) REFERENCES Teacher(ID)
	
ALTER TABLE [Assignment]  
	ADD CONSTRAINT Assignment_classID_Class_ID
	FOREIGN KEY (classID) REFERENCES Class(ID)
	
-- Thêm các khoá chính và khoá ngoại
ALTER TABLE Assignment 
	ADD CONSTRAINT Assignment_semesterID_Semester_ID
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)
	
ALTER TABLE Assignment 
	ADD CONSTRAINT Assignment_positionID_Position_ID
	FOREIGN KEY (positionID) REFERENCES Position(ID)

ALTER TABLE Assignment 
	ADD CONSTRAINT Assignment_academicyearID_AcademicYear_ID
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)
	
ALTER TABLE Assignment 
	ADD CONSTRAINT Assignment_subjectID_Subject_ID
	FOREIGN KEY (subjectID) REFERENCES Subject(ID)
	

-- Tạo bảng điểm
CREATE TABLE Point(
	studentID INT NOT NULL,
	typeofpointID INT NOT NULL,
	subjectID INT, -- Giá trị này NULL nếu loại điểm là điểm hạnh kiểm
	academicyearID INT NOT NULL,
	semesterID INT NOT NULL,
	classID INT,
	point DECIMAL(10,2),
	createDate DATETIME,
	updateDate DATETIME,
	PRIMARY KEY (studentID, typeofpointID, 
		subjectID, academicyearID, semesterID, classID)
)	

-- Thêm các khoá chính và khoá ngoại
ALTER TABLE Point 
	ADD CONSTRAINT Point_semesterID_Semester_ID 
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)

ALTER TABLE Point 
	ADD CONSTRAINT Point_classID_Class_ID 
	FOREIGN KEY (classID) REFERENCES Class(ID)
	
ALTER TABLE Point 
	ADD CONSTRAINT Point_academicyearID_AcademicYear_ID 
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)
	
ALTER TABLE Point 
	ADD CONSTRAINT Point_typeofpointID_TypeOfPonit_ID 
	FOREIGN KEY (typeofpointID) REFERENCES TypeOfPoint(ID)	

ALTER TABLE Point 
	ADD CONSTRAINT Point_subjectID_Subject_ID 
	FOREIGN KEY (subjectID) REFERENCES Subject(ID)


ALTER TABLE Point 
	ADD CONSTRAINT Point_studentID_Student_ID
	FOREIGN KEY (studentID) REFERENCES Student(ID)

-- Tạo bảng tổng kết cuối mỗi học kỳ

-------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Summary(
	studentID INT NOT NULL,
	studentconductID INT NOT NULL,
	studentcapacityID INT NOT NULL,
	academicyearID INT NOT NULL,
	semesterID INT NOT NULL,
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (studentID, studentconductID, studentcapacityID, 
		academicyearID, semesterID) 
)

CREATE TABLE StudentConduct(
	studentconductID INT IDENTITY(1, 1) NOT NULL,
	studentID INT NOT NULL,
	academicyearID INT NOT NULL,
	semesterID INT NOT NULL,
	conductName NVARCHAR(255),
	point DECIMAL(10, 2),
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (studentconductID, studentID, 
		academicyearID, semesterID) 
)

ALTER TABLE StudentConduct
	ADD CONSTRAINT StudentConduct_studentID_Student_ID
	FOREIGN KEY (studentID)
	REFERENCES Student(ID) 

ALTER TABLE StudentConduct
	ADD CONSTRAINT StudentConduct_academicyearID_AcademicYear_ID
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)


ALTER TABLE StudentConduct
	ADD CONSTRAINT StudentConduct_semesterID_Semester_ID
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)

CREATE TABLE StudentCapacity(
	studentcapacityID INT IDENTITY(1, 1) NOT NULL,
	studentID INT NOT NULL,
	academicyearID INT NOT NULL,
	semesterID INT NOT NULL,
	capacityName NVARCHAR(255),
	point DECIMAL(10, 2),
	createDate DATETIME,
	updateDate DATETIME,
	finishDate DATETIME,
	PRIMARY KEY (studentcapacityID, studentID, 
		academicyearID, semesterID) 
)	

ALTER TABLE StudentCapacity
	ADD CONSTRAINT StudentCapacity_studentID_Student_ID
	FOREIGN KEY (studentID)
	REFERENCES Student(ID) 

ALTER TABLE StudentCapacity
	ADD CONSTRAINT studentcapacity_academicyearID_AcademicYear_ID
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)


ALTER TABLE StudentCapacity
	ADD CONSTRAINT studentcapacity_semesterID_Semester_ID
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)



ALTER TABLE Summary  
	ADD CONSTRAINT Summary_studentID_Student_ID
	FOREIGN KEY (studentID) REFERENCES Student(ID)

ALTER TABLE Summary 
	ADD CONSTRAINT Summary_semesterID_Semester_ID
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)

ALTER TABLE Summary 
	ADD CONSTRAINT Summary_academicyearID_AcademicYear_ID
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)

ALTER TABLE Summary
	ADD CONSTRAINT Summary_studentconductID_studentID_academicyearID_semesterID_StudentConduct_ID
	FOREIGN KEY (studentconductID, studentID, academicyearID, semesterID)
	REFERENCES StudentConduct(studentconductID, studentID, academicyearID, semesterID)


ALTER TABLE Summary
	ADD CONSTRAINT Summary_studentcapacityID_studentID_academicyearID_semesterID_studentcapacity_ID
	FOREIGN KEY (studentcapacityID, studentID, academicyearID, semesterID)
	REFERENCES StudentCapacity(studentcapacityID, studentID, academicyearID, semesterID)

CREATE TABLE StudentClassSemesterAcademicYear(
	studentID INT NOT NULL,
	classID INT NOT NULL,
	semesterID INT NOT NULL,
	academicyearID INT NOT NULL,
	gradeID INT NOT NULL,
	PRIMARY KEY (studentID, classID, semesterID, academicyearID, gradeID)

)

ALTER TABLE StudentClassSemesterAcademicYear
	ADD CONSTRAINT StudentClassSemesterAcademicYear_studentID_Student_ID
	FOREIGN KEY (studentID) REFERENCES Student(ID)

ALTER TABLE StudentClassSemesterAcademicYear
	ADD CONSTRAINT StudentClassSemesterAcademicYear_classID_Class_ID
	FOREIGN KEY (classID) REFERENCES Class(ID)
	
ALTER TABLE StudentClassSemesterAcademicYear
	ADD CONSTRAINT StudentClassSemesterAcademicYear_semesterID_Semester_ID
	FOREIGN KEY (semesterID) REFERENCES Semester(ID)

ALTER TABLE StudentClassSemesterAcademicYear
	ADD CONSTRAINT StudentClassSemesterAcademicYear_academicyearID_AcademicYear_ID
	FOREIGN KEY (academicyearID) REFERENCES AcademicYear(ID)
	
ALTER TABLE StudentClassSemesterAcademicYear
	ADD CONSTRAINT StudentClassSemesterAcademicYear_gradeID_Grade_ID
	FOREIGN KEY (gradeID) REFERENCES Grade(ID)

CREATE TABLE ClassGrade(
	classID INT,
	gradeID INT
	PRIMARY KEY (classID, gradeID)
)

ALTER TABLE ClassGrade 
	ADD CONSTRAINT ClassGrade_classID_Class_ID
	FOREIGN KEY (classID) REFERENCES Class(ID)

ALTER TABLE ClassGrade 
	ADD CONSTRAINT ClassGrade_gradeID_Grade_ID
	FOREIGN KEY (gradeID) REFERENCES Grade(ID)
