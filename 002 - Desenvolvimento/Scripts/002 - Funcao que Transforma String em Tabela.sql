CREATE FUNCTION [dbo].[fnSplit](
    @sInputList VARCHAR(8000) -- Lista os itens que serao separados
    ,@sDelimiter VARCHAR(8000) = ',' -- O caractere separador
) RETURNS @List TABLE (item VARCHAR(8000))
BEGIN
    DECLARE @sItem VARCHAR(8000)
    WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
    BEGIN
        SELECT @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),@sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
        IF LEN(@sItem) > 0
            INSERT INTO @List SELECT @sItem
        END
            IF LEN(@sInputList) > 0
                INSERT INTO @List SELECT @sInputList -- Insere o ultimo item na lista
    RETURN
END