<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifUserTaskList.aspx.cs"
	Inherits="dtxc.User.ifUserTaskList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>任务列表</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scUserTaskList" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/User/WS1.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>

	<script type="text/javascript" id="sc_Pager">
		ApqJS.location.QueryString.Status = ApqJS.location.QueryString.Status || "1,2";
		var Apq_Pager_Goto = function(n) {
			var strUrl = ApqJS.location.removeSearch(location.href);
			ApqJS.location.QueryString.t = Math.random();
			ApqJS.location.QueryString.ps = document.getElementById("txtPager_PageSize").value || 20;
			ApqJS.location.QueryString.p = n || document.getElementById("txtPager_Page").value || 1;

			location = strUrl + "?" + ApqJS.location.BuildSearch(ApqJS.location.QueryString);
		};
		var Apq_Pager_First = function() {
			Apq_Pager_Goto(1);
		};
		var Apq_Pager_Last = function() {
			var PageCount = parseInt(document.getElementById("txtPager_PageCount").innerText);
			Apq_Pager_Goto(PageCount);
		};
		var Apq_Pager_Current = function() {
			var PageNumbuer = parseInt(document.getElementById("txtPager_Page").value);
			if (PageNumbuer >= 0) {
				Apq_Pager_Goto(PageNumbuer);
			}
		};
		var Apq_Pager_Pre = function() {
			var PageNumbuer = parseInt(document.getElementById("txtPager_Page").value);
			if (PageNumbuer > 1) {
				Apq_Pager_Goto(PageNumbuer - 1);
			}
		};
		var Apq_Pager_Next = function() {
			var PageNumbuer = parseInt(document.getElementById("txtPager_Page").value);
			Apq_Pager_Goto(PageNumbuer + 1);
		};

		var btnTaskFresh_Click = function() {
			Apq_Pager_Current();
		};
	</script>

	<div id="divBar" style="width: 100%">
		<input type="button" id="btnTaskFresh" onclick="btnTaskFresh_Click()" value="刷新" />
		<%--<input type="button" id="btnTaskAdd" onclick="btnTaskAdd_Click();" value="添加" />--%>
		<input type="checkbox" id="cbStatus_1" runat="server" onclick="cbStatus_Click('1')" /><label
			for="cbStatus_1">未结算</label>
		<input type="checkbox" id="cbStatus_2" runat="server" onclick="cbStatus_Click('2')" /><label
			for="cbStatus_2">已结算</label>
	</div>
	<div id="divPager1" align="right">
		<a href="javascript:;" onclick="Apq_Pager_First()">首页</a> <a href="javascript:;"
			onclick="Apq_Pager_Pre()">上一页</a> <a href="javascript:;" onclick="Apq_Pager_Next()">
				下一页</a> <a href="javascript:;" onclick="Apq_Pager_Last()">尾页</a>行数:<asp:TextBox ID="txtPager_PageSize"
					runat="server" Width="20"></asp:TextBox>
	</div>
	<div>
		<table width="100%">
			<asp:Repeater ID="rpt" runat="server">
				<HeaderTemplate>
					<thead id="thList">
						<tr>
							<td>
								任务ID
							</td>
							<td>
								任务名
							</td>
							<td>
								任务总价
							</td>
							<td>
								状态
							</td>
						</tr>
					</thead>
					<tbody id="tbdList">
				</HeaderTemplate>
				<ItemTemplate>
					<tr taskid="<%#DataBinder.Eval(Container.DataItem,"TaskID")%>">
						<td>
							<%#DataBinder.Eval(Container.DataItem,"TaskID")%>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"TaskName")%>
						</td>
						<td>
							<%#Convert.ToInt64(DataBinder.Eval(Container.DataItem,"TaskMoney"))%>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"Status")%>
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</tbody>
				</FooterTemplate>
			</asp:Repeater>
		</table>
	</div>
	<div id="divPager2" align="right">
		<a href="javascript:;" onclick="Apq_Pager_First()">首页</a> <a href="javascript:;"
			onclick="Apq_Pager_Pre()">上一页</a> <a href="javascript:;" onclick="Apq_Pager_Next()">
				下一页</a> <a href="javascript:;" onclick="Apq_Pager_Last()">尾页</a>总页数:<span id="txtPager_PageCount"
					runat="server">0</span> 转到:<asp:TextBox ID="txtPager_Page" runat="server" Width="30"></asp:TextBox><a
						href="javascript:;" onclick="Apq_Pager_Goto(parseInt(document.getElementById('txtPager_Page').value))">GO</a>
	</div>

	<script type="text/javascript">
		if (window.attachEvent) {
			window.attachEvent("onload", ApqJS.document.iframeAutoFit);
		}
		else if (window.addEventListener) {
			window.addEventListener('load', ApqJS.document.iframeAutoFit, false);
		}

		var aryStatus = ApqJS.location.QueryString.Status.split(","); // 保存当前页面已选择的Status
		function cbStatus_Click(s) {
			if ($get("cbStatus_" + s).checked) {
				ApqJS.Array.AddUnique(aryStatus, s);
			}
			else {
				ApqJS.Array.Remove(aryStatus, s);
			}
			ApqJS.location.QueryString.Status = aryStatus.join(",");
		}
	</script>

	</form>
</body>
</html>
