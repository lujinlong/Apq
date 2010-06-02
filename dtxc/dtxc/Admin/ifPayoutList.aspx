<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifPayoutList.aspx.cs" Inherits="dtxc.Admin.ifPayoutList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>支付申请列表</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scPayoutList" runat="server">
		<Services>
			<asp:ServiceReference Path="~/WS/Admin/WS1.asmx" />
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

		var btnPayoutFresh_Click = function() {
			Apq_Pager_Current();
		};
	</script>

	<div id="divBar" style="width: 100%">
		<input type="button" id="btnPayoutFresh" onclick="btnPayoutFresh_Click()" value="刷新" />
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
								支付申请ID
							</td>
							<td>
								会员名
							</td>
							<td>
								金额
							</td>
							<td>
								支付宝帐号
							</td>
						</tr>
					</thead>
					<tbody id="tbdList">
				</HeaderTemplate>
				<ItemTemplate>
					<tr payouid="<%#DataBinder.Eval(Container.DataItem,"PayoutID") %>">
						<td>
							<a href="javascript:;" onclick="PayoutConfirm(<%#DataBinder.Eval(Container.DataItem,"PayoutID") %>);return false;">
								支付</a>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem, "PayoutID")%>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"UserName") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"Payout") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem, "Alipay")%>
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

		// 删除
		function PayoutConfirm(PayoutID) {
			if (window.confirm("支付确认:支付ID " + PayoutID + "\r\n请务必确定已支付后操作"))
				dtxc.WS.Admin.WS1.PayoutConfirm(PayoutID, PayoutConfirm_Success);
		}
		function PayoutConfirm_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, PayoutConfirm_Success_Confirm);
			MsgBox.Show();
		}
		function PayoutConfirm_Success_Confirm() {
			location.reload(true);
		}
	</script>

	</form>
</body>
</html>
