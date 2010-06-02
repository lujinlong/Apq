<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ifAddinList.aspx.cs" Inherits="dtxc.Admin.ifAddinList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>插件列表</title>
	<link type="text/css" href="../CSS/main.css" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="scAddinList" runat="server">
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

		var btnAddinFresh_Click = function() {
			Apq_Pager_Current();
		};
	</script>

	<div id="divBar" style="width: 100%">
		<input type="button" id="btnAddinFresh" onclick="btnAddinFresh_Click()" value="刷新" />
		<input type="button" id="btnAddinAdd" onclick="btnAddinAdd_Click();" value="添加" />
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
								插件ID
							</td>
							<td>
								插件名
							</td>
						</tr>
					</thead>
					<tbody id="tbdList">
				</HeaderTemplate>
				<ItemTemplate>
					<tr addinid="<%#DataBinder.Eval(Container.DataItem,"AddinID") %>">
						<td>
							<a href="javascript:;" onclick="AddinEdit(<%#DataBinder.Eval(Container.DataItem,"AddinID") %>,0);return false;">
								编辑</a> <a href="javascript:;" onclick="AddinEdit(<%#DataBinder.Eval(Container.DataItem,"AddinID") %>,1);return false;">
									查看</a> <a href="javascript:;" onclick="AddinDelete(<%#DataBinder.Eval(Container.DataItem,"AddinID") %>);return false;">
										删除</a>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"AddinID") %>
						</td>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"AddinName") %>
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

		// 添加
		function btnAddinAdd_Click() {
			ApqJS.using("ApqJS.Simulator");
			var ModalDialog = new ApqJS.Simulator.Dialog(1, "发布插件", "mdAddinEdit.aspx?m=a&t=" + Math.random(), 700, 0, 0);
			ModalDialog.Show();
		}

		// 删除
		function AddinDelete(AddinID) {
			if (window.confirm("删除确认:插件ID " + AddinID))
				dtxc.WS.Admin.WS1.AddinDelete(AddinID, AddinDelete_Success);
		}
		function AddinDelete_Success(stReturn) {
			ApqJS.using("ApqJS.Simulator");
			var MsgBox = new ApqJS.Simulator.Dialog(0, "提示", stReturn.ExMsg, 300, 0, 150, AddinDelete_Success_Confirm);
			MsgBox.Show();
		}
		function AddinDelete_Success_Confirm() {
			location.reload(true);
		}

		// 打开修改页面
		function AddinEdit(AddinID, ReadOnly) {
			var strUrl = "mdAddinEdit.aspx";
			var objQueryString = ApqJS.location.getQueryString(strUrl);
			objQueryString.t = Math.random();
			objQueryString.m = ReadOnly ? "v" : "e";
			objQueryString.AddinID = AddinID;

			ApqJS.using("ApqJS.Simulator");
			var ModalDialog = new ApqJS.Simulator.Dialog(1, ReadOnly ? "查看插件" : "修改插件", strUrl + "?" + ApqJS.location.BuildSearch(objQueryString), 700, 0, 0);
			ModalDialog.Show();
		}

		var ifAddinList_Load = function() {
			if (ApqJS.location.QueryString["l"] == "1") {//单项Lookup
				window.ll = ApqJS.location.QueryString["ll"];
				var tbdList = ApqJS.$("tbdList");
				tbdList.attachEvent("onclick", tbdList_onclick);
				for (var r = 0; r < tbdList.rows.length; r++) {
					var tr = tbdList.rows[r];

					if (tr.addinid == window.ll) {//设置高亮
						tr.style.backgroundColor = "blue";
					}

					//隐藏操作列
					tr.cells[0].style.display = "none";
				}
				ApqJS.$("divBar").style.display = "none";
				var thList = ApqJS.$("thList");
				thList.rows[0].cells[0].style.display = "none";
			}
		};
		window.attachEvent("onload", ifAddinList_Load);

		function FindTrByAddinID(AddinID) {
			var tbdList = ApqJS.$("tbdList");
			for (var r = 0; r < tbdList.rows.length; r++) {
				if (tbdList.rows[r].addinid == AddinID) {
					return tbdList.rows[r];
				}
			}
			return null;
		}

		function FindTrByElt(he) {
			if (he.tagName == "TABLE" || he.tagName == "TBODY") return null;
			if (he.tagName == "TR") return he;
			return FindTrByElt(he.parentNode);
		}

		function tbdList_onclick(e) {
			// 取消旧行
			var trOld = FindTrByAddinID(window.ll);
			if (trOld) trOld.style.backgroundColor = "transparent";

			// 设置新行,并获取AddinID
			var trNew = FindTrByElt(e.srcElement);
			if (trNew) {
				trNew.style.backgroundColor = "blue";
				window.ll = trNew.addinid;

				parent.ApqJS.$(ApqJS.location.QueryString["pe"]).value = window.ll;
				parent.ApqJS.$(ApqJS.location.QueryString["pe"]).Lookup.Hide();
			}
		}
	</script>

	</form>
</body>
</html>
