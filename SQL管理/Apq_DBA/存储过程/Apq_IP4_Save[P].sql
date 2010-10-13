IF( OBJECT_ID('dbo.Apq_IP4_Save', 'P') IS NULL )
	EXEC sp_executesql N'CREATE PROCEDURE dbo.Apq_IP4_Save AS BEGIN RETURN END';
GO
/* =============================================
-- 作者: 黄宗银
-- 日期: 2007-11-02
-- 描述: 保存查询到的一段 IP4 的各项值
	同时处理重叠的IP段
-- 示例:
EXEC dbo.Apq_IP4_Save '61.133.8.125', '61.133.8.255', '山东省诸城市 网通', 1, 1, 0, getdate();
-- =============================================
*/
ALTER PROC dbo.Apq_IP4_Save
	@Begin		nvarchar(256),
	@End		nvarchar(256),
	@Addr		nvarchar(3000),
	@ProvinceID	bigint,
	@CityID		bigint,
	@IsMatched	smallint,
	@_Time		datetime
AS
SET NOCOUNT ON;

DECLARE	@binBegin binary(4), @binEnd binary(4), 
		@binBegin1 binary(4), @binEnd1 binary(4), @binBegin2 binary(4), @binEnd2 binary(4), 
		@Addr1 nvarchar(3000), @Addr2 nvarchar(3000), @ProvinceID1 bigint, @ProvinceID2 bigint, @CityID1 bigint, @CityID2 bigint, 
		@IsMatched1 smallint, @IsMatched2 smallint, @_Time1 datetime, @_Time2 datetime;
SELECT	 @binBegin = dbo.Apq_ConvertIP4_VarBinary(@Begin)
		,@binEnd = dbo.Apq_ConvertIP4_VarBinary(@End)
		;

-- 直接删掉内部段
DELETE	Apq_IP4 WHERE [Begin] >= @Begin AND [End] <= @End;

-- 记录起始值所在原段的 行值
SELECT	 @binBegin1 = [Begin]
		,@binEnd1 = [End]
		,@Addr1 = Addr
		,@ProvinceID1 = ProvinceID
		,@CityID1 = CityID
		,@IsMatched1 = IsMatched
		,@_Time1 = _Time
FROM	Apq_IP4
WHERE	@binBegin BETWEEN [Begin] AND [End];
IF(@@ROWCOUNT > 0)
BEGIN
	DELETE	Apq_IP4 WHERE @binBegin BETWEEN [Begin] AND [End];

	-- 默认相同值
	SELECT	 @binBegin2 = @binBegin1
			,@binEnd2 = @binEnd1
			,@Addr2 = @Addr1
			,@ProvinceID2 = @ProvinceID1
			,@CityID2 = @CityID1
			,@IsMatched2 = @IsMatched1
			,@_Time2 = @_Time1
			;
END

-- 记录结束值所在原段的 行值
SELECT	 @binBegin2 = [Begin]
		,@binEnd2 = [End]
		,@Addr2 = Addr
		,@ProvinceID2 = ProvinceID
		,@CityID2 = CityID
		,@IsMatched2 = IsMatched
		,@_Time2 = _Time
FROM	Apq_IP4
WHERE	@binEnd BETWEEN [Begin] AND [End];
IF(@@ROWCOUNT > 0)
BEGIN
	DELETE	Apq_IP4 WHERE @binEnd BETWEEN [Begin] AND [End];
END

-- 补起始值之前段
IF(@binBegin > @binBegin1)
BEGIN
	INSERT	Apq_IP4([Begin], [End], Addr, ProvinceID, CityID, IsMatched, _Time)
	VALUES(@binBegin1, @binBegin-1, @Addr1, @ProvinceID1, @CityID1, @IsMatched1, @_Time1);
END
-- 补结束值之后段
IF(@binEnd < @binEnd2)
BEGIN
	INSERT	Apq_IP4([Begin], [End], Addr, ProvinceID, CityID, IsMatched, _Time)
	VALUES(@binEnd+1, @binEnd2, @Addr2, @ProvinceID2, @CityID2, @IsMatched2, @_Time2);
END
-- 插入参数值
INSERT	Apq_IP4([Begin], [End], Addr, ProvinceID, CityID, IsMatched, _Time)
VALUES(@binBegin, @binEnd, @Addr, @ProvinceID, @CityID, @IsMatched, @_Time);
GO
