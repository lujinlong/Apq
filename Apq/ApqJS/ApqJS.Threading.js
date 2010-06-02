ApqJS.namespace("ApqJS.Threading");

if (!ApqJS.Threading.Thread) {
	ApqJS.Threading.Thread = ApqJS.Class("ApqJS.Threading.Thread");

	/// <t=0>启动延时时间</t>
	/// <m=true>是否允许多次排队</m>
	ApqJS.Threading.Thread.prototype.ctor = function(ThreadStart, t, args, o, m) {
		this.t = t || 0;
		this.ThreadStart = ThreadStart;
		this.args = args;
		this.o = o;
		this.m = m == null ? true : m;
		this.ids = []; // 排队号码集合
	};

	ApqJS.Threading.Thread.prototype.Enqueue = function() {
		if (!this.m && this.ids.length) {
			throw new Error(-1, "该线程不允许重复排队");
		}
		var id = ApqJS.setTimeout(this.t, this.ThreadStart, arguments.length ? arguments : this.args, this.o);
		this.ids.push(id);
		return id;
	};

	ApqJS.Threading.Thread.prototype.Dequeue = function(id) {
		id = id || this.ids.concat();

		if (id instanceof Array) {
			if (id.length) {
				for (var i = 0; i < id.length; i++) {
					this.Dequeue(id[i]);
				}
			}
			else {
				this.Dequeue();
			}
		}
		else if (this.ids.Contains(id)) {
			clearTimeout(id);
			ApqJS.Array.Remove(this.ids, id);
		}
	};
}
