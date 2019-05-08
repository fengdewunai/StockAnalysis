delimiter $$
use `stockanalysis`$$

drop procedure if exists Temp$$
create procedure Temp()
begin
	create table if not exists Stock
	(
	   StockCode                 varchar(20) not null comment '股票代码',
	   StockName                 varchar(100) comment '股票名称',
	   primary key (StockCode)
	) comment '股票基本信息';
    
    create table if not exists EveryDayData
	(
	   StockCode            varchar(20) comment '股票代码',
	   CurrentDate          varchar(20) comment '日期',
	   OpenPrice            double comment '开盘价格',
	   HighPrice            double comment '最高价',
	   ClosePrice           double comment '收盘价',
	   LowPrice             double comment '最低价',
	   Volume               double comment '成交量',
	   Price_Change         double comment '价格变动',
	   P_Change             double comment '涨跌幅',
	   Ma5                  double comment '5日均价',
	   Ma10                 double comment '10日均价',
	   Ma20                 double comment '20日均价',
	   V_Ma5                double comment '5日均量',
	   V_Ma10               double comment '10日均量',
	   V_Ma20               double comment '20日均量',
	   Turnover             double comment '换手率',
       index `EveryDayData_StockCode` (StockCode)
	) comment '每日数据';

end$$
call Temp()$$
drop procedure if exists Temp$$

drop procedure if exists StockAnalysis_Stock_Save$$
create procedure StockAnalysis_Stock_Save(
	v_StockCode varchar(20),
    v_StockName varchar(100)
)
begin
	if not exists(select 1 from Stock where StockCode=v_StockCode)
    then
		insert into Stock(StockCode,StockName)
        values(v_StockCode,v_StockName);
	else
		update Stock set StockName = v_StockName where StockCode=v_StockCode;
    end if;
end$$

drop procedure if exists StockAnalysis_Stock_Read$$
create procedure StockAnalysis_Stock_Read()
begin
	select * from Stock;
end$$

drop procedure if exists StockAnalysis_EveryDayData_Save$$
create procedure StockAnalysis_EveryDayData_Save(
	v_StockCode            varchar(20),
	v_CurrentDate          varchar(20),
	v_OpenPrice            double,
	v_HighPrice            double,
	v_ClosePrice           double,
	v_LowPrice             double,
	v_Volume               double,
	v_Price_Change         double,
	v_P_Change             double,
	v_Ma5                  double,
	v_Ma10                 double,
	v_Ma20                 double,
	v_V_Ma5                double,
	v_V_Ma10               double,
	v_V_Ma20               double,
	v_Turnover             double
)
begin
	if not exists(select 1 from EveryDayData where StockCode=v_StockCode and CurrentDate = v_CurrentDate)
    then
		insert into EveryDayData(StockCode, CurrentDate, OpenPrice, HighPrice, ClosePrice, LowPrice, Volume, Price_Change, P_Change, Ma5, Ma10, Ma20, V_Ma5, V_Ma10, V_Ma20, Turnover)
        values(v_StockCode, v_CurrentDate, v_OpenPrice, v_HighPrice, v_ClosePrice, v_LowPrice, v_Volume, v_Price_Change, v_P_Change, v_Ma5, v_Ma10, v_Ma20, v_V_Ma5, v_V_Ma10, v_V_Ma20, v_Turnover);
	
    end if;
end$$


drop procedure if exists StockAnalysis_EveryDayData_ReadByCode$$
create procedure StockAnalysis_EveryDayData_ReadByCode(
	v_StockCode	varchar(20)
)
begin
	select StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover from everydaydata where StockCode = v_StockCode order by CurrentDate asc;
end$$


drop procedure if exists StockAnalysis_EveryDayData_ReadAll$$
create procedure StockAnalysis_EveryDayData_ReadAll()
begin
	select StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover from everydaydata;
end$$

drop procedure if exists StockAnalysis_EveryDayData_ReadByDate$$
create procedure StockAnalysis_EveryDayData_ReadByDate(
	v_StartDate	varchar(20),
    v_EndDate	varchar(20)
)
begin
	select StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover from everydaydata where CurrentDate >= v_StartDate and CurrentDate <= v_EndDate and ClosePrice < 20 order by CurrentDate asc;
end$$