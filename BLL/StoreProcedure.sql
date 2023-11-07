
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
	

	
	EXECUTE GetDataSemester
	EXEC GetDataTypeOfPoint
	
CREATE PROCEDURE FindCapacity
    @STR NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Capacity c
    WHERE capacityName LIKE '%' + @STR + '%'
END



EXEC FindCapacity 'k' s