namespace Apq.XSD
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public partial class Explorer
	{
		/// <summary>
		/// 初始化类型查找表
		/// </summary>
		public void Init_luType()
		{
			this.luType.Clear();
			this.luType.AddluTypeRow(0, "逻辑磁盘");
			this.luType.AddluTypeRow(1, "文件夹");
			this.luType.AddluTypeRow(2, "文件");
		}
	}
}
