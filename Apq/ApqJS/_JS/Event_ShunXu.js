事件顺序

第一步:
	obj.onxxx = function(){}
	event="onxxx" for="obj"
	只运行其中一种,event for 会延迟解析,故一般最后才解析
第二步:
	attachEvent
