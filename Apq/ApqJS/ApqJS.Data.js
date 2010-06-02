ApqJS.namespace( "ApqJS.Data" );

if( !ApqJS.Data.DataSet )
{
	ApqJS.Data.DataSet = ApqJS.Class( "ApqJS.Data.DataSet" );
	ApqJS.Data.DataSet.prototype.ctor = function( DataSetName ){
		this.DataSetName = DataSetName || "NewDataSet";
		this.Tables = [];
		this.Relations = [];
		
		this.EnforceConstraints = true;
		this.HasErrors = false;
		this.IsInitialized = true;
		
		this.ExtendedProperties = {};
		
		this.Events = {
			"Initialized": ApqJS.Event(),
			"MergeFailed": ApqJS.Event()
		};
	};
	
	ApqJS.Data.DataSet.prototype.AddTable = function( tbl ){
		return ApqJS.Array.AddUnique( this.Tables, tbl );
	};
	
	ApqJS.Data.DataSet.prototype.HasChanges = function(){
		for( var i = 0; i < this.Tables.length; i++ )
		{
			if( this.Tables[i].GetChanges() != null )
			{
				return true;
			}
		}
		return false;
	};
	
	ApqJS.Data.DataSet.prototype.GetChanges = function(){
		var ds = new ApqJS.Data.DataSet();
		for( var i = 0; i < this.Tables.length; i++ )
		{
			ds.AddTable( this.Tables[i].GetChanges() );
		}
		return ds;
	};
	
	ApqJS.Data.DataSet.prototype.AcceptChanges = function(){
		for( var i = 0; i < this.Tables.length; i++ )
		{
			this.Tables[i].AcceptChanges();
		}
	};
	
	ApqJS.Data.DataSet.prototype.RejectChanges = function(){
		for( var i = 0; i < this.Tables.length; i++ )
		{
			this.Tables[i].RejectChanges();
		}
	};
	
	ApqJS.Data.DataSet.prototype.BeginInit = function(){
		this.IsInitialized = false;
	};
	
	ApqJS.Data.DataSet.prototype.EndInit = function(){
		
		this.IsInitialized = true;
	};
	
	ApqJS.Data.DataSet.prototype.Clear = function(){
		for( var i = 0; i < this.Tables.length; i++ )
		{
			this.Tables[i].Clear();
		}
	};
	
	ApqJS.Data.DataSet.prototype.Reset = function(){
		
	};
	
	ApqJS.Data.DataSet.prototype.Merge = function(){
		
	};
	
	ApqJS.Data.DataRelation = ApqJS.Class( "ApqJS.Data.DataRelation" );
	ApqJS.Data.DataRelation.prototype.ctor = function( RelationName, parentColumns, ChildColumns, createConstraints ){
		this.RelationName = RelationName;
		this.DataSet = null;
		
		this.ParentTable = ParentColumns.length ? ParentColumns[0].Table : null;
		this.ChildTable = ChildColumns.length ? ChildColumns[0].Table : null;
		this.ParentColumns = ParentColumns;
		this.ChildColumns = ChildColumns;
		this.ParentKeyConstraint = null;
		this.ChildKeyConstraint = null;
		
		this.ExtendedProperties = {};
	};
	
	ApqJS.Data.DataTable = ApqJS.Class( "ApqJS.Data.DataTable" );
	ApqJS.Data.DataTable.prototype.ctor = function( TableName ){
		this.TableName = TableName;
		this.Columns = [];
		this.Rows = [];
		this.DataSet = null;
		
		this.PrimaryKey = [];
		this.ChildRelations = [];
		this.ParentRelations = [];
		this.Constraints = [];
		
		this.HasErrors = false;
		this.IsInitialized = true;
		
		this.ExtendedProperties = {};
		
		this.Events = {
			"Initialized": ApqJS.Event(),
			"ColumnChanging": ApqJS.Event(),
			"ColumnChanged": ApqJS.Event(),
			"RowChanging": ApqJS.Event(),
			"RowChanged": ApqJS.Event(),
			"RowDeleting": ApqJS.Event(),
			"TableClearing": ApqJS.Event(),
			"TableCleared": ApqJS.Event(),
			"TableNewRow": ApqJS.Event()
		};
	};
	
	ApqJS.Data.DataTable.prototype.AcceptChanges = function(){
		for( var i = 0; i < this.Rows.length; i++ )
		{
			this.Rows[i].AcceptChanges();
		}
	};
	
	ApqJS.Data.DataTable.prototype.RejectChanges = function(){
		for( var i = 0; i < this.Rows.length; i++ )
		{
			this.Rows[i].RejectChanges();
		}
	};
	
	ApqJS.Data.DataTable.prototype.BeginInit = function(){
		this.IsInitialized = false;
	};
	
	ApqJS.Data.DataTable.prototype.EndInit = function(){
		
		this.IsInitialized = true;
	};
	
	ApqJS.Data.DataTable.prototype.BeginLoadData = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.LoadDataRow = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.EndLoadData = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.Clear = function(){
		ApqJS.Array.Clear( this.Rows );
	};
	
	ApqJS.Data.DataTable.prototype.GetChanges = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.GetErrors = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.NewRow = function(){
		var dr = new ApqJS.Data.DataRow();
		// 填充默认值
		dr.Items[this.Columns.length] = null;
		dr.Table = this;
		dr.RowState = ApqJS.Data.DataRowState.Detached;
		return dr;
	};
	
	ApqJS.Data.DataTable.prototype.ImportRow = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.Merge = function(){
		
	};
	
	ApqJS.Data.DataTable.prototype.Reset = function(){
		
	};
	
	/// 
	ApqJS.Data.DataTable.prototype.Select = function(){
		
	};
	
	ApqJS.Data.DataColumn = ApqJS.Class( "ApqJS.Data.DataColumn" );
	ApqJS.Data.DataColumn.prototype.ctor = function( ColumnName, DataType ){
		this.ColumnName = ColumnName;
		this.DataType = DataType;
		this.Caption = this.ColumnName;
		this.Table = null;
		
		this.Ordinal = -1;
		
		this.AllowDBNull = true;
		this.AutoIncrement = false;
		this.AutoIncrementSeed = 1;
		this.AutoIncrementStep = 1;
		this.DefaultValue = null;
		this.MaxLength = -1;
		this.ReadOnly = false;
		this.Unique = false;
		
		this.ExtendedProperties = {};
	};
	
	ApqJS.Data.DataColumn.prototype.Item = function( dr, RowVersion ){
		for( var i = 0; i < this.Table.Columns.length; i++ )
		{
			return dr.Item( i, RowVersion );
		}
	};
	
	ApqJS.Data.DataColumn.prototype.SetOrdinal = function( index ){
		this.Ordinal = index;
	};
	
	ApqJS.Data.Rule = {
		"None": 0,
		/// 删除或更新相关的行。这是默认选项。
		"Cascade": 1
		/// 将相关的行中的值设置为 DBNull。
		"SetNull": 2,
		/// 将相关的行中的值设置为 System.Data.DataColumn.DefaultValue 属性中包含的值。
		"SetDefault": 3
	};
	
	ApqJS.Data.AcceptRejectRule = {
		"None": 0,
		/// 在关系中级联更改
		"Cascade": 1
	};
	
	ApqJS.Data.UniqueConstraint = ApqJS.Class( "ApqJS.Data.UniqueConstraint" );
	ApqJS.Data.UniqueConstraint.prototype.ctor = function( Columns, ConstraintName, IsPrimaryKey ){
		this.Columns = Columns;
		this.ConstraintName = ConstraintName;
		this.IsPrimaryKey = IsPrimaryKey;
		this.Table = Columns.length ? Columns[0].Table : null;
		this.DataSet = null;
		
		this.ExtendedProperties = {};
	};
	
	ApqJS.Data.ForeignKeyConstraint = ApqJS.Class( "ApqJS.Data.ForeignKeyConstraint" );
	ApqJS.Data.ForeignKeyConstraint.prototype.ctor = function( ConstraintName, RelatedColumns, Columns ){
		this.ConstraintName = ConstraintName;
		this.RelatedColumns = RelatedColumns;
		this.Columns = Columns;
		this.RelatedTable = RelatedColumns.length ? RelatedColumns[0].Table : null;
		this.Table = Columns.length ? Columns[0].Table : null;
		this.DataSet = null;
		
		this.AcceptRejectRule = ApqJS.Data.AcceptRejectRule.None;
		this.DeleteRule = ApqJS.Data.Rule.Cascade;
		this.UpdateRule = ApqJS.Data.Rule.Cascade;
		
		this.ExtendedProperties = {};
	};
	
	ApqJS.Data.DataRowState = {
		"None": 0,
		/// 该行已被创建，在添加到集合中之前；或从集合中移除之后。
		"Detached": 1,
		/// 该行自上次调用 AcceptChanges() 以来尚未更改。
		"Unchanged": 2,
		/// 该行已添加到集合中，AcceptChanges() 尚未调用。
		"Added": 4,
		/// 该行已通过 Delete() 方法被删除。
		"Deleted": 8,
		/// 该行已被修改，AcceptChanges() 尚未调用。
		"Modified": 16
	};
	
	ApqJS.Data.DataRowVersion = {
		"None": 0,
		/// 该行中包含其原始值。
		"Original": 0x100,
		/// 该行中包含当前值。
		"Current": 0x200,
		/// 该行中包含建议值。
		"Proposed": 0x400,
		/// DataRowState 的默认版本。对于 Added、Modified 或 Current 的 DataRowState 值，默认版本是 Current。对于 Detached 的 System.Data.DataRowState 值，该版本是 Proposed。
		"Default": 0x600
	};
	
	ApqJS.Data.DataRow = ApqJS.Class( "ApqJS.Data.DataRow" );
	ApqJS.Data.DataRow.prototype.ctor = function(){
		this.Current = null;
		this.Original = null;
		this.Proposed = [];
		this.Table = null;
		this.RowState = ApqJS.Data.DataRowState.None;
		this.HasErrors = false;
		this.RowError = "";
		this.ColumnErrors = [];
	};
	
	/// 检查该行是否属于某个表
	ApqJS.Data.DataRow.prototype.CheckTable = function(){
		if( !this.Table )
		{
			throw new Error( -1, "该行不含任何列." );
		}
	};
	
	ApqJS.Data.DataRow.prototype.HasVersion = function( Version ){
		switch( Version )
		{
			case ApqJS.Data.DataRowVersion.Original:
				return this.Original != null;
			case ApqJS.Data.DataRowVersion.Current:
				return this.Current != null;
			case ApqJS.Data.DataRowVersion.Proposed:
				return this.Proposed != null;
			default:
			case ApqJS.Data.DataRowVersion.Defalut:
				return this.HasVersion( ApqJS.Data.DataRowVersion.Current ) || this.HasVersion( ApqJS.Data.DataRowVersion.Proposed );
		}
	};
	
	/// 索引(位置)
	ApqJS.Data.DataRow.prototype.Item = function( ColIndex, RowVersion ){
		this.CheckTable();
		
		RowVersion = RowVersion || ApqJS.Data.DataRowVersion.Default;
		
		if( this.HasVersion( RowVersion ) )
		{
			if( (RowVersion & ApqJS.Data.DataRowVersion.Original) > 0 )
			{
				return this.Original[ColIndex];
			}
			if( (RowVersion & ApqJS.Data.DataRowVersion.Current) > 0 )
			{
				return this.Current[ColIndex];
			}
			if( (RowVersion & ApqJS.Data.DataRowVersion.Proposed) > 0 )
			{
				return this.Proposed[ColIndex];
			}
		}
	};
	
	/// 索引(名称)
	ApqJS.Data.DataRow.prototype.Item2 = function( ColName, RowVersion ){
		this.CheckTable();
		
		for( var i = 0; i < this.Table.Columns.length; i++ )
		{
			if( this.Table.Columns[i].ColumnName == ColName )
			{
				return this.Item( i, RowVersion );
			}
		}
		
		throw new Error( -1, "不存在指定列." );
	};
	
	ApqJS.Data.DataRow.prototype.AcceptChanges = function(){
		this.EndEdit();
		
		if( (this.RowState & ApqJS.Data.DataRowState.Detached) > 0 )
		{
			return;
		}
		
		this.Original = this.Current;
		this.Current = null;
		
		if( (this.RowState & ApqJS.Data.DataRowState.Deleted) > 0 )
		{
			if( this.Table )
			{
				ApqJS.Array.Remove( this.Table.Rows, this );
			}
			this.RowState = ApqJS.Data.DataRowState.Detached;
			return;
		}
		
		this.RowState = ApqJS.Data.DataRowState.Unchanged;
	};
	
	ApqJS.Data.DataRow.prototype.RejectChanges = function(){
		this.CancelEdit();
		
		if( (this.RowState & ApqJS.Data.DataRowState.Detached) > 0 )
		{
			return;
		}
		
		this.Current = this.Original;
		
		if( (this.RowState & ApqJS.Data.DataRowState.Added) > 0 )
		{
			if( this.Table )
			{
				ApqJS.Array.Remove( this.Table.Rows, this );
			}
			this.RowState = ApqJS.Data.DataRowState.Detached;
			return;
		}
		
		this.RowState = ApqJS.Data.DataRowState.Unchanged;
	};
	
	ApqJS.Data.DataRow.prototype.BeginEdit = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.EndEdit = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.CancelEdit = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.SetModified = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.SetAdded = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.SetColumnError = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.GetColumnError = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.GetColumnsInError = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.Delete = function(){
		this.RowState = ApqJS.Data.DataRowState.Deleted;
	};
	
	ApqJS.Data.DataRow.prototype.Equals = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.GetChildRows = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.GetParentRow = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.SetParentRow = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.GetParentRows = function(){
		
	};
	
	ApqJS.Data.DataRow.prototype.IsNull = function(){
		
	};
}
