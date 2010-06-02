<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifUserList.aspx.cs" Inherits="dtxc.Admin.ifUserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>会员列表</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scUserList" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/Admin/WS2.asmx" />
		</Services>
		<Scripts>
			<asp:ScriptReference Path="~/Script/Apq.js" />
		</Scripts>
	</asp:ScriptManager>

	<script type="text/javascript" id="sc_Pager">
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

		var btnUserFresh_Click = function() {
			Apq_Pager_Current();
		};
	</script>

	<div id="divBar" style="width: 100%">
		<input type="button" id="btnUserFresh" onclick="btnUserFresh_Click()" value="刷新" />
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
								操作
							</td>
							<td>
								会员ID
							</td>
							<td>
								会员名
							</td>
							<td>
								是否团队
							</td>
							<td>
								性别
							</td>
							<td>
								帐号状态
							</td>
							<td>
								支付宝帐号
							</td>
						</tr>
					</thead>
					<tbody id="tbdList">
				</HeaderTemplate>
				<ItemTemplate>
					<tr userid="<%#DataBinder.Eval(Container.DataItem,"UserID")%>">
						<td>
							<a href="javascript:;" onclick="UserEdit(<%#DataBinder.Eval(Container.DataItem,"UserID")%>,0);return false;">
								编辑</a> <a href="javascript:;" onclick="UserEdit(<%#DataBinder.Eval(Container.DataItem,"UserID") %>,1);return false;">
									查看</a>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem, "UserID")%>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"Name") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"Sex") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"UserType") %>
						</td>
						<td>
							<a href="javascript:;" onclick="UserToggle(<%#DataBinder.Eval(Container.DataItem,"UserID") %>,'<%#DataBinder.Eval(Container.DataItem,"Name") %>')">
								<%#DataBinder.Eval(Container.DataItem,"Status") %></a>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"Alipay") %>
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

		var ifUserList_Load = function() {
		};
		window.attachEvent("onload", ifUserList_Load);

		// 帐号状态切换
		function UserToggle(UserID, Name) {
			if (window.confirm("切换帐号状态确认:会员ID [" + UserID + "] 会员名 [" + Name + "]"))
				dtxc.WS.Admin.WS2.dtxc_User_Toggle(UserID, UserToggle_Success);
		}
		function UserToggle_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, UserToggle_Success_Confirm);
			MsgBox.Show();
		}
		function UserToggle_Success_Confirm() {
			location.reload(true);
		}

		// 打开修改页面
		function UserEdit(UserID, ReadOnly) {
			var strUrl = "mdUserEdit.aspx";
			var objQueryString = ApqJS.location.getQueryString(strUrl);
			objQueryString.t = Math.random();
			objQueryString.m = ReadOnly ? "v" : "e";
			objQueryString.UserID = UserID;

			ApqJS.using("ApqJS.Simulator");
			var ModalDialog = new ApqJS.Simulator.Dialog(1, ReadOnly ? "查看会员" : "修改会员", strUrl + "?" + ApqJS.location.BuildSearch(objQueryString), 700, 0, 0);
			ModalDialog.Show();
		}
	</script>

	</form>
</body>
</html>
