<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteLog.aspx.cs" Inherits="dtxc.VoteLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>投票记录</title>
	<link type="text/css" href="CSS/main.css" />
	<link type="text/css" href="CSS/style.css" />
	<style type="text/css">
		.style1
		{
			background: #99CCFF;
		}
		.style2
		{
			background: #FFFFCC;
		}
	</style>
</head>
<body background="Img/bg.gif">
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scVoteLog" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>

	<script type="text/javascript" id="sc_Pager">
		var btnFresh_Click = function() {
			location.reload(true);
		};
		var btnQuery_Click = function() {
			var strUrl = ApqJS.location.removeSearch(location.href);
			ApqJS.location.QueryString.t = Math.random();
			ApqJS.location.QueryString.TaskName = document.getElementById("txtTaskName").value;
			ApqJS.location.QueryString.UserNameBegin = document.getElementById("txtUserNameBegin").value;

			location = strUrl + "?" + ApqJS.location.BuildSearch(ApqJS.location.QueryString);
		};
	</script>

	<div>
		<div id="divTaskStatus">
		</div>
		<div id="divBar" style="width: 100%; text-align: center">
			<input type="button" id="btnFresh" onclick="btnFresh_Click()" value="刷新" />
			任务名:<input type="text" id="txtTaskName" />
			工号(前缀):<input type="text" id="txtUserNameBegin" />
			<input type="button" id="btnQuery" onclick="btnQuery_Click()" value="刷新" />
		</div>
		<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
			<asp:Repeater ID="rpt" runat="server">
				<HeaderTemplate>
					<thead id="thList">
						<tr>
							<td>
								工号
							</td>
							<td>
								投票名称
							</td>
							<td>
								票数
							</td>
							<td>
								最后更新
							</td>
						</tr>
					</thead>
					<tbody id="tbdList">
				</HeaderTemplate>
				<ItemTemplate>
					<tr bgcolor="#FFFFCC" onmouseover="this.className='style1'" onmouseout="this.className='style2'">
						<td>
							<%#DataBinder.Eval(Container.DataItem,"UserName") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"TaskName") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"VoteCount") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"LastVoteTime") %>
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</tbody>
				</FooterTemplate>
			</asp:Repeater>
		</table>
	</div>

	<script src="VoteLog.js" type="text/javascript"></script>

	</form>
</body>
</html>
