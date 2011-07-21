using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pdbp.CryptKey
{
	/// <summary>
	/// RSA加密Key(Base64)
	/// </summary>
	public class RSA
	{
		/*
<RSAKeyValue>
	<Modulus>3hh/H+2IZ3wQE7r7g1l/7ODqeNxeSpt3fMKFsVKeU9Sjwt39eGrKt5BC85M2nv9aeuL+q0oRuX3RO46mfphB/kdLfGRRQN9FJjo61VKPAJnXOaiJBnBymvt3xFHteCKJMXQkVUaih+5AiGc1oD88Ki4s8u8QaEuyDAF1T0JW4Sk=</Modulus>
	<Exponent>AQAB</Exponent>
	<P>+/hQUaB1fAjmT/u/lBZqAV7DIa50pBxirvrPAs7HoRi8PrHcZYawtst2ABuLdrCDXKTpHdxDqalY8Ld7XfbVKQ==</P>
	<Q>4aXc9ujsFAbVBfzU5/6WV57VY/zk0/2L2Tbs+X42lv0hwUOigK/0Z9DuYWi8Ny2stupHZiXk/WgJaQXDLHUsAQ==</Q>
	<DP>NIyhE6UHW4rvnZa/ab8S9J4yy/96TA+vdRbRvaAqeiqSd/DYKkRg7n6YaYVVHLfLbbLm+1dItUWgSvO5QuuLoQ==</DP>
	<DQ>fIIK9Or9KOfEL3Oc/w4JYvuvf2aR1S94NTkLdXdhI9s0/vEU/7EXSRmOD429HS1EugF3uDN8sR7w4lRdp3BYAQ==</DQ>
	<InverseQ>6LxP6ZWA4dn3tQyO6MP6CXe9FwGhPLwNZOqDNaa0V7rTTKiqVu1nkl1D4Ge2Sk9HD5D+FCpesEcklpOvzjAF5Q==</InverseQ>
	<D>PHNSdrBChlhAT/5+4tuDdEgrycXBbHEwbqbLmN4x9gNUp3+gtBvtHVWIw594KIK8b+JFCv5YILLcNme3bZGntDpXT8bx+hgnI/cJCXjXMCZ2leNVjvTxvc6Ka05TJZvo0/g1n2gTNWLq/FsIUGEQwThpZDebYzs996+M7BADQAE=</D>
</RSAKeyValue>
*/
		#region 公钥
		/// <summary>
		/// Modulus
		/// </summary>
		public static string Modulus
		{
			get { return "3hh/H+2IZ3wQE7r7g1l/7ODqeNxeSpt3fMKFsVKeU9Sjwt39eGrKt5BC85M2nv9aeuL+q0oRuX3RO46mfphB/kdLfGRRQN9FJjo61VKPAJnXOaiJBnBymvt3xFHteCKJMXQkVUaih+5AiGc1oD88Ki4s8u8QaEuyDAF1T0JW4Sk="; }
		}

		/// <summary>
		/// Exponent
		/// </summary>
		public static string Exponent
		{
			get { return "AQAB"; }
		}
		#endregion

		/// <summary>
		/// P
		/// </summary>
		public static string P
		{
			get { return "+/hQUaB1fAjmT/u/lBZqAV7DIa50pBxirvrPAs7HoRi8PrHcZYawtst2ABuLdrCDXKTpHdxDqalY8Ld7XfbVKQ=="; }
		}

		/// <summary>
		/// Q
		/// </summary>
		public static string Q
		{
			get { return "4aXc9ujsFAbVBfzU5/6WV57VY/zk0/2L2Tbs+X42lv0hwUOigK/0Z9DuYWi8Ny2stupHZiXk/WgJaQXDLHUsAQ=="; }
		}

		/// <summary>
		/// DP
		/// </summary>
		public static string DP
		{
			get { return "NIyhE6UHW4rvnZa/ab8S9J4yy/96TA+vdRbRvaAqeiqSd/DYKkRg7n6YaYVVHLfLbbLm+1dItUWgSvO5QuuLoQ=="; }
		}

		/// <summary>
		/// DQ
		/// </summary>
		public static string DQ
		{
			get { return "fIIK9Or9KOfEL3Oc/w4JYvuvf2aR1S94NTkLdXdhI9s0/vEU/7EXSRmOD429HS1EugF3uDN8sR7w4lRdp3BYAQ=="; }
		}

		/// <summary>
		/// InverseQ
		/// </summary>
		public static string InverseQ
		{
			get { return "6LxP6ZWA4dn3tQyO6MP6CXe9FwGhPLwNZOqDNaa0V7rTTKiqVu1nkl1D4Ge2Sk9HD5D+FCpesEcklpOvzjAF5Q=="; }
		}

		/// <summary>
		/// D
		/// </summary>
		public static string D
		{
			get { return "PHNSdrBChlhAT/5+4tuDdEgrycXBbHEwbqbLmN4x9gNUp3+gtBvtHVWIw594KIK8b+JFCv5YILLcNme3bZGntDpXT8bx+hgnI/cJCXjXMCZ2leNVjvTxvc6Ka05TJZvo0/g1n2gTNWLq/FsIUGEQwThpZDebYzs996+M7BADQAE="; }
		}

		/// <summary>
		/// XmlString
		/// </summary>
		public static string XmlString
		{
			get
			{
				return string.Format(@"<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
					Modulus, Exponent, P, Q, DP, DQ, InverseQ, D);
			}
		}
	}
}
