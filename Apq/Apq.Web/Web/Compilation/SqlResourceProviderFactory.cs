using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Compilation;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;

namespace Apq.Web.Compilation
{
	/// <summary>
	/// 
	/// </summary>
	public class SqlResourceProviderFactory : ResourceProviderFactory
	{
		/// <summary>
		/// 
		/// </summary>
		public SqlResourceProviderFactory()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="classKey"></param>
		/// <returns></returns>
		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new SqlResourceProvider(null, classKey);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="virtualPath"></param>
		/// <returns></returns>
		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			virtualPath = System.IO.Path.GetFileName(virtualPath);
			return new SqlResourceProvider(virtualPath, null);
		}

		private sealed class SqlResourceProvider : IResourceProvider
		{
			private string _virtualPath;
			private string _className;
			private IDictionary _resourceCache;

			private static object CultureNeutralKey = new object();

			public SqlResourceProvider(string virtualPath, string className)
			{
				_virtualPath = virtualPath;
				_className = className;
			}

			private IDictionary GetResourceCache(string cultureName)
			{
				object cultureKey;

				if (cultureName != null)
				{
					cultureKey = cultureName;
				}
				else
				{
					cultureKey = CultureNeutralKey;
				}


				if (_resourceCache == null)
				{
					_resourceCache = new ListDictionary();
				}

				IDictionary resourceDict = _resourceCache[cultureKey] as IDictionary;

				if (resourceDict == null)
				{
					resourceDict = SqlResourceHelper.GetResources(_virtualPath, _className, cultureName, false, null);
					_resourceCache[cultureKey] = resourceDict;
				}

				return resourceDict;
			}

			object IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
			{
				string cultureName = null;
				if (culture != null)
				{
					cultureName = culture.Name;
				}
				else
				{
					cultureName = CultureInfo.CurrentUICulture.Name;
				}

				object value = GetResourceCache(cultureName)[resourceKey];
				if (value == null)
				{
					// resource is missing for current culture
					SqlResourceHelper.AddResource(resourceKey, _virtualPath, _className, cultureName);
					value = GetResourceCache(null)[resourceKey];
				}

				if (value == null)
				{
					// the resource is really missing, no default exists
					SqlResourceHelper.AddResource(resourceKey, _virtualPath, _className, string.Empty);
				}

				return value;
			}

			IResourceReader IResourceProvider.ResourceReader
			{
				get
				{
					return new SqlResourceReader(GetResourceCache(null));

				}
			}

		}

		private sealed class SqlResourceReader : IResourceReader
		{
			private IDictionary _resources;

			public SqlResourceReader(IDictionary resources)
			{
				_resources = resources;
			}

			IDictionaryEnumerator IResourceReader.GetEnumerator()
			{
				return _resources.GetEnumerator();
			}

			void IResourceReader.Close()
			{
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return _resources.GetEnumerator();
			}

			void IDisposable.Dispose()
			{
			}
		}


	}
}
