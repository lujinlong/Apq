/* 将 ApqJS.js 中定义的 内置对象原型扩展 的属性复制到 真实原型对象中
*
* 2007-03-10	黄宗银
* */

if (!Object.prototype.ToString) {
	ApqJS.Object.Copy(ApqJS.$Object_p, Object.prototype);
	ApqJS.Object.Copy(ApqJS.$Array_p, Array.prototype);
	ApqJS.Object.Copy(ApqJS.$Function_p, Function.prototype);
	ApqJS.Object.Copy(ApqJS.$String_p, String.prototype);

	ApqJS.Object.Copy(ApqJS.$Boolean_p, Boolean.prototype);
	ApqJS.Object.Copy(ApqJS.$Number_p, Number.prototype);

	ApqJS.Object.Copy(ApqJS.$Date_p, Date.prototype);
	ApqJS.Object.Copy(ApqJS.$Error_p, Error.prototype);
	ApqJS.Object.Copy(ApqJS.$Enumerator_p, Enumerator.prototype);
	ApqJS.Object.Copy(ApqJS.$RegExp_p, RegExp.prototype);
}
