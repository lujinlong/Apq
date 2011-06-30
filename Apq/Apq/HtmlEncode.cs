using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Apq
{
	/*
	* (c) Craig Dunn - ConceptDevelopment.NET
	* 12-June-04
	*
	* To use:
	*    string encoded = ExtendedHtmlUtility.HtmlEntityEncode ("test string with Unicode chars and & < > é");
	*    string decoded = ExtendedHtmlUtility.HtmlEntityDecode (encoded); // "string with &amp &lt; &gt; &eacute;"
	*
	* More info:
	*    http://users.bigpond.com/conceptdevelopment/localization/htmlencode/
	*/
	/// <summary>
	/// ExtendedHtmlUtility
	/// </summary>
	public class ExtendedHtmlUtility
	{
		/// <summary>
		/// Based on the 'reflected' code (from the Framework System.Web.HttpServerUtility)
		/// listed on this page
		/// UrlEncode vs. HtmlEncode
		/// http://www.aspnetresources.com/blog/encoding_forms.aspx
		///
		/// PDF of unicode characters in the 0-127 (dec) range
		/// http://www.unicode.org/charts/PDF/U0000.pdf
		/// </summary>
		/// <param name="unicodeText">Unicode text</param>
		/// <returns>
		/// &amp; becomes &amp;amp;  (encoded for XML Comments - don't be confused)
		/// 0-9a-zA-Z and some punctuation (ASCII, basically) remain unchanged
		/// </returns>
		public static string HtmlEntityEncode(string unicodeText)
		{
			return HtmlEntityEncode(unicodeText, true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="unicodeText"></param>
		/// <param name="encodeTagsToo">whether to encode &amp; &lt; and &gt; which will
		/// cause the entire string to be 'displayable' as HTML. true is the default value.
		/// Setting this to false will result in a string where the non-ASCII characters
		/// are encoded, but HTML tags remain in-tact for display in a browser.</param>
		/// <returns>
		/// 0-9a-zA-Z and some punctuation (ASCII, basically) remain unchanged,
		/// everything else encoded
		/// </returns>
		public static string HtmlEntityEncode(string unicodeText, bool encodeTagsToo)
		{
			int unicodeVal;
			string encoded = string.Empty;        // StringBuilder would be better - but this is simpler
			foreach (char c in unicodeText)
			{
				unicodeVal = c;
				switch (unicodeVal)
				{
					case '&':
						if (encodeTagsToo) encoded += "&amp;";
						break;
					case '<':
						if (encodeTagsToo) encoded += "&lt;";
						break;
					case '>':
						if (encodeTagsToo) encoded += "&gt;";
						break;
					default:
						if ((c >= ' ') && (c <= 0x007E))
						{ // from 'space' to '~tilde' hex 20-7E (dec 32-127)
							// in 'ascii' range x30 to x7a which is 0-9A-Za-z plus some punctuation
							encoded += c;    // leave as-is
						}
						else
						{ // outside 'ascii' range - encode
							encoded += string.Concat("&#"
								, unicodeVal.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)
								, ";");
						}
						break;
				}
			}
			return encoded;
		} // HtmlEntityEncode

		/// <summary>
		/// Converts Html Entities back to their 'underlying' Unicode characters
		/// </summary>
		/// <param name="encodedText">Text containing Html Entities eg. &amp;#1234; or &amp;eacute;</param>
		/// <returns>
		/// &amp;amp; becomes &amp;  (encoded for XML Comments - don't be confused)
		/// 1-9a-zA-Z and some punctuation (ASCII, basically) remain unchanged
		/// </returns>
		public static string HtmlEntityDecode(string encodedText)
		{
			return entityResolver.Replace(encodedText, new MatchEvaluator(ResolveEntityAngleAmp));
		} // HtmlEntityDecode


		/// <summary>
		/// Converts Html Entities back to their 'underlying' Unicode characters
		/// </summary>
		/// <param name="encodedText">Text containing Html Entities eg. &amp;#1234; or &amp;eacute;</param>
		/// <param name="encodeTagsToo">Include angles and ampersand &lt; &gt; &amp;</param>
		/// <returns>
		/// &amp;amp; becomes &amp;  (encoded for XML Comments - don't be confused)
		/// 1-9a-zA-Z and some punctuation (ASCII, basically) remain unchanged
		/// </returns>
		public static string HtmlEntityDecode(string encodedText, bool encodeTagsToo)
		{
			if (encodeTagsToo)
				return entityResolver.Replace(encodedText, new MatchEvaluator(ResolveEntityAngleAmp));
			else
				return entityResolver.Replace(encodedText, new MatchEvaluator(ResolveEntityNotAngleAmp));
		} // HtmlEntityDecode


		/// <summary>
		/// Static Regular Expression to match Html Entities in encoded text
		/// Looking for named-entity, decimal or hexadecimal formats
		/// http://www.w3.org/TR/REC-html40/charset.html#h-5.3.1
		/// http://www.i18nguy.com/markup/ncrs.html
		/// http://www.vigay.com/inet/acorn/browse-html2.html#entities
		/// </summary>
		private static Regex entityResolver =
			  new Regex(
				@"([&][#](?'decimal'[0-9]+);)|([&][#][(x|X)](?'hex'[0-9a-fA-F]+);)|([&](?'html'\w+);)"
			  );

		/// <summary>
		/// Regex Match processing delegate to replace the Entities with their
		/// underlying Unicode character.
		/// </summary>
		/// <param name="matchToProcess">Regular Expression Match eg. [ &#123; | &#x1F | &eacute; ]</param>
		/// <returns>
		/// &amp;amp; becomes &amp;  (encoded for XML Comments - don't be confused)
		/// and &amp;eacute; becomes é
		/// BUT we dont modify the angles or ampersand
		/// </returns>
		private static string ResolveEntityNotAngleAmp(System.Text.RegularExpressions.Match matchToProcess)
		{
			string x = ""; // default 'char placeholder' if cannot be resolved - shouldn't occur
			if (matchToProcess.Groups["decimal"].Success)
			{
				x = System.Convert.ToChar(System.Convert.ToInt32(matchToProcess.Groups["decimal"].Value)).ToString();
			}
			else if (matchToProcess.Groups["hex"].Success)
			{
				x = System.Convert.ToChar(HexToInt(matchToProcess.Groups["hex"].Value)).ToString();
			}
			else if (matchToProcess.Groups["html"].Success)
			{
				string entity = matchToProcess.Groups["html"].Value;
				switch (entity.ToLower())
				{
					case "lt":
					case "gt":
					case "amp":
						x = "&" + entity + ";";
						break;
					default:
						x = EntityLookup(entity);
						break;
				}
			}
			else x = "X";
			return x;
		} // ResolveEntityNotAngleAmp()

		/// <summary>
		/// Regex Match processing delegate to replace the Entities with their
		/// underlying Unicode character.
		/// </summary>
		/// <param name="matchToProcess">Regular Expression Match eg. [ &#123; | &#x1F | &eacute; ]</param>
		/// <returns>
		/// &amp;amp; becomes &amp;  (encoded for XML Comments - don't be confused)
		/// and &amp;eacute; becomes é
		/// </returns>
		private static string ResolveEntityAngleAmp(System.Text.RegularExpressions.Match matchToProcess)
		{
			string x = ""; // default 'char placeholder' if cannot be resolved - shouldn't occur
			if (matchToProcess.Groups["decimal"].Success)
			{
				x = System.Convert.ToChar(System.Convert.ToInt32(matchToProcess.Groups["decimal"].Value)).ToString();
			}
			else if (matchToProcess.Groups["hex"].Success)
			{
				x = System.Convert.ToChar(HexToInt(matchToProcess.Groups["hex"].Value)).ToString();
			}
			else if (matchToProcess.Groups["html"].Success)
			{
				x = EntityLookup(matchToProcess.Groups["html"].Value);
			}
			else x = "Y";
			return x;
		} // ResolveEntityAngleAmp()



		/// <summary>
		/// For the 'hexadecimal' format &amp;#x233; we need to be able 
		/// to convert hex into decimal...
		/// Thanks for this method to
		/// http://www.c-sharpcorner.com/Code/2002/Sept/HexColors.asp
		/// </summary>
		/// <remarks>
		/// This method converts a hexvalues string as 80FF into a integer.
		/// Note that you may NOT put a '#' at the beginning of string! There
		/// is not much error checking in this method. If the string does not
		/// represent a valid hexadecimal value it returns 0.
		/// </remarks>
		/// <param name="hexstr">eg 00, 1A, ff, etc</param>
		/// <returns>eg. 0, 26, 255 (zero if the inputstring is NOT a valid hexadecimal)</returns>
		public static int HexToInt(string hexstr)
		{
			int counter, hexint;
			char[] hexarr;
			hexint = 0;
			hexstr = hexstr.ToUpper();
			hexarr = hexstr.ToCharArray();
			for (counter = hexarr.Length - 1; counter >= 0; counter--)
			{
				if ((hexarr[counter] >= '0') && (hexarr[counter] <= '9'))
				{
					hexint += (hexarr[counter] - 48) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
				}
				else
				{
					if ((hexarr[counter] >= 'A') && (hexarr[counter] <= 'F'))
					{
						hexint += (hexarr[counter] - 55) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
					}
					else
					{
						hexint = 0;
						break;
					}
				}
			}
			return hexint;
		} //HexToInt



		/// <summary>
		/// Thanks to this site for the Entity Reference
		/// http://www.vigay.com/inet/acorn/browse-html2.html#entities
		/// </summary>
		private static string EntityLookup(string entity)
		{
			string x = "";
			switch (entity)
			{
				case "Aacute": x = System.Convert.ToChar(0x00C1).ToString(); break;
				case "aacute": x = System.Convert.ToChar(0x00E1).ToString(); break;
				case "acirc": x = System.Convert.ToChar(0x00E2).ToString(); break;
				case "Acirc": x = System.Convert.ToChar(0x00C2).ToString(); break;
				case "acute": x = System.Convert.ToChar(0x00B4).ToString(); break;
				case "AElig": x = System.Convert.ToChar(0x00C6).ToString(); break;
				case "aelig": x = System.Convert.ToChar(0x00E6).ToString(); break;
				case "Agrave": x = System.Convert.ToChar(0x00C0).ToString(); break;
				case "agrave": x = System.Convert.ToChar(0x00E0).ToString(); break;
				case "alefsym": x = System.Convert.ToChar(0x2135).ToString(); break;
				case "Alpha": x = System.Convert.ToChar(0x0391).ToString(); break;
				case "alpha": x = System.Convert.ToChar(0x03B1).ToString(); break;
				case "amp": x = System.Convert.ToChar(0x0026).ToString(); break;
				case "and": x = System.Convert.ToChar(0x2227).ToString(); break;
				case "ang": x = System.Convert.ToChar(0x2220).ToString(); break;
				case "aring": x = System.Convert.ToChar(0x00E5).ToString(); break;
				case "Aring": x = System.Convert.ToChar(0x00C5).ToString(); break;
				case "asymp": x = System.Convert.ToChar(0x2248).ToString(); break;
				case "Atilde": x = System.Convert.ToChar(0x00C3).ToString(); break;
				case "atilde": x = System.Convert.ToChar(0x00E3).ToString(); break;
				case "auml": x = System.Convert.ToChar(0x00E4).ToString(); break;
				case "Auml": x = System.Convert.ToChar(0x00C4).ToString(); break;
				case "bdquo": x = System.Convert.ToChar(0x201E).ToString(); break;
				case "Beta": x = System.Convert.ToChar(0x0392).ToString(); break;
				case "beta": x = System.Convert.ToChar(0x03B2).ToString(); break;
				case "brvbar": x = System.Convert.ToChar(0x00A6).ToString(); break;
				case "bull": x = System.Convert.ToChar(0x2022).ToString(); break;
				case "cap": x = System.Convert.ToChar(0x2229).ToString(); break;
				case "Ccedil": x = System.Convert.ToChar(0x00C7).ToString(); break;
				case "ccedil": x = System.Convert.ToChar(0x00E7).ToString(); break;
				case "cedil": x = System.Convert.ToChar(0x00B8).ToString(); break;
				case "cent": x = System.Convert.ToChar(0x00A2).ToString(); break;
				case "chi": x = System.Convert.ToChar(0x03C7).ToString(); break;
				case "Chi": x = System.Convert.ToChar(0x03A7).ToString(); break;
				case "circ": x = System.Convert.ToChar(0x02C6).ToString(); break;
				case "clubs": x = System.Convert.ToChar(0x2663).ToString(); break;
				case "cong": x = System.Convert.ToChar(0x2245).ToString(); break;
				case "copy": x = System.Convert.ToChar(0x00A9).ToString(); break;
				case "crarr": x = System.Convert.ToChar(0x21B5).ToString(); break;
				case "cup": x = System.Convert.ToChar(0x222A).ToString(); break;
				case "curren": x = System.Convert.ToChar(0x00A4).ToString(); break;
				case "dagger": x = System.Convert.ToChar(0x2020).ToString(); break;
				case "Dagger": x = System.Convert.ToChar(0x2021).ToString(); break;
				case "darr": x = System.Convert.ToChar(0x2193).ToString(); break;
				case "dArr": x = System.Convert.ToChar(0x21D3).ToString(); break;
				case "deg": x = System.Convert.ToChar(0x00B0).ToString(); break;
				case "Delta": x = System.Convert.ToChar(0x0394).ToString(); break;
				case "delta": x = System.Convert.ToChar(0x03B4).ToString(); break;
				case "diams": x = System.Convert.ToChar(0x2666).ToString(); break;
				case "divide": x = System.Convert.ToChar(0x00F7).ToString(); break;
				case "eacute": x = System.Convert.ToChar(0x00E9).ToString(); break;
				case "Eacute": x = System.Convert.ToChar(0x00C9).ToString(); break;
				case "Ecirc": x = System.Convert.ToChar(0x00CA).ToString(); break;
				case "ecirc": x = System.Convert.ToChar(0x00EA).ToString(); break;
				case "Egrave": x = System.Convert.ToChar(0x00C8).ToString(); break;
				case "egrave": x = System.Convert.ToChar(0x00E8).ToString(); break;
				case "empty": x = System.Convert.ToChar(0x2205).ToString(); break;
				case "emsp": x = System.Convert.ToChar(0x2003).ToString(); break;
				case "ensp": x = System.Convert.ToChar(0x2002).ToString(); break;
				case "epsilon": x = System.Convert.ToChar(0x03B5).ToString(); break;
				case "Epsilon": x = System.Convert.ToChar(0x0395).ToString(); break;
				case "equiv": x = System.Convert.ToChar(0x2261).ToString(); break;
				case "Eta": x = System.Convert.ToChar(0x0397).ToString(); break;
				case "eta": x = System.Convert.ToChar(0x03B7).ToString(); break;
				case "eth": x = System.Convert.ToChar(0x00F0).ToString(); break;
				case "ETH": x = System.Convert.ToChar(0x00D0).ToString(); break;
				case "Euml": x = System.Convert.ToChar(0x00CB).ToString(); break;
				case "euml": x = System.Convert.ToChar(0x00EB).ToString(); break;
				case "euro": x = System.Convert.ToChar(0x20AC).ToString(); break;
				case "exist": x = System.Convert.ToChar(0x2203).ToString(); break;
				case "fnof": x = System.Convert.ToChar(0x0192).ToString(); break;
				case "forall": x = System.Convert.ToChar(0x2200).ToString(); break;
				case "frac12": x = System.Convert.ToChar(0x00BD).ToString(); break;
				case "frac14": x = System.Convert.ToChar(0x00BC).ToString(); break;
				case "frac34": x = System.Convert.ToChar(0x00BE).ToString(); break;
				case "frasl": x = System.Convert.ToChar(0x2044).ToString(); break;
				case "gamma": x = System.Convert.ToChar(0x03B3).ToString(); break;
				case "Gamma": x = System.Convert.ToChar(0x393).ToString(); break;
				case "ge": x = System.Convert.ToChar(0x2265).ToString(); break;
				case "gt": x = System.Convert.ToChar(0x003E).ToString(); break;
				case "hArr": x = System.Convert.ToChar(0x21D4).ToString(); break;
				case "harr": x = System.Convert.ToChar(0x2194).ToString(); break;
				case "hearts": x = System.Convert.ToChar(0x2665).ToString(); break;
				case "hellip": x = System.Convert.ToChar(0x2026).ToString(); break;
				case "Iacute": x = System.Convert.ToChar(0x00CD).ToString(); break;
				case "iacute": x = System.Convert.ToChar(0x00ED).ToString(); break;
				case "icirc": x = System.Convert.ToChar(0x00EE).ToString(); break;
				case "Icirc": x = System.Convert.ToChar(0x00CE).ToString(); break;
				case "iexcl": x = System.Convert.ToChar(0x00A1).ToString(); break;
				case "Igrave": x = System.Convert.ToChar(0x00CC).ToString(); break;
				case "igrave": x = System.Convert.ToChar(0x00EC).ToString(); break;
				case "image": x = System.Convert.ToChar(0x2111).ToString(); break;
				case "infin": x = System.Convert.ToChar(0x221E).ToString(); break;
				case "int": x = System.Convert.ToChar(0x222B).ToString(); break;
				case "Iota": x = System.Convert.ToChar(0x0399).ToString(); break;
				case "iota": x = System.Convert.ToChar(0x03B9).ToString(); break;
				case "iquest": x = System.Convert.ToChar(0x00BF).ToString(); break;
				case "isin": x = System.Convert.ToChar(0x2208).ToString(); break;
				case "iuml": x = System.Convert.ToChar(0x00EF).ToString(); break;
				case "Iuml": x = System.Convert.ToChar(0x00CF).ToString(); break;
				case "kappa": x = System.Convert.ToChar(0x03BA).ToString(); break;
				case "Kappa": x = System.Convert.ToChar(0x039A).ToString(); break;
				case "Lambda": x = System.Convert.ToChar(0x039B).ToString(); break;
				case "lambda": x = System.Convert.ToChar(0x03BB).ToString(); break;
				case "lang": x = System.Convert.ToChar(0x2329).ToString(); break;
				case "laquo": x = System.Convert.ToChar(0x00AB).ToString(); break;
				case "larr": x = System.Convert.ToChar(0x2190).ToString(); break;
				case "lArr": x = System.Convert.ToChar(0x21D0).ToString(); break;
				case "lceil": x = System.Convert.ToChar(0x2308).ToString(); break;
				case "ldquo": x = System.Convert.ToChar(0x201C).ToString(); break;
				case "le": x = System.Convert.ToChar(0x2264).ToString(); break;
				case "lfloor": x = System.Convert.ToChar(0x230A).ToString(); break;
				case "lowast": x = System.Convert.ToChar(0x2217).ToString(); break;
				case "loz": x = System.Convert.ToChar(0x25CA).ToString(); break;
				case "lrm": x = System.Convert.ToChar(0x200E).ToString(); break;
				case "lsaquo": x = System.Convert.ToChar(0x2039).ToString(); break;
				case "lsquo": x = System.Convert.ToChar(0x2018).ToString(); break;
				case "lt": x = System.Convert.ToChar(0x003C).ToString(); break;
				case "macr": x = System.Convert.ToChar(0x00AF).ToString(); break;
				case "mdash": x = System.Convert.ToChar(0x2014).ToString(); break;
				case "micro": x = System.Convert.ToChar(0x00B5).ToString(); break;
				case "middot": x = System.Convert.ToChar(0x00B7).ToString(); break;
				case "minus": x = System.Convert.ToChar(0x2212).ToString(); break;
				case "Mu": x = System.Convert.ToChar(0x039C).ToString(); break;
				case "mu": x = System.Convert.ToChar(0x03BC).ToString(); break;
				case "nabla": x = System.Convert.ToChar(0x2207).ToString(); break;
				case "nbsp": x = System.Convert.ToChar(0x00A0).ToString(); break;
				case "ndash": x = System.Convert.ToChar(0x2013).ToString(); break;
				case "ne": x = System.Convert.ToChar(0x2260).ToString(); break;
				case "ni": x = System.Convert.ToChar(0x220B).ToString(); break;
				case "not": x = System.Convert.ToChar(0x00AC).ToString(); break;
				case "notin": x = System.Convert.ToChar(0x2209).ToString(); break;
				case "nsub": x = System.Convert.ToChar(0x2284).ToString(); break;
				case "ntilde": x = System.Convert.ToChar(0x00F1).ToString(); break;
				case "Ntilde": x = System.Convert.ToChar(0x00D1).ToString(); break;
				case "Nu": x = System.Convert.ToChar(0x039D).ToString(); break;
				case "nu": x = System.Convert.ToChar(0x03BD).ToString(); break;
				case "oacute": x = System.Convert.ToChar(0x00F3).ToString(); break;
				case "Oacute": x = System.Convert.ToChar(0x00D3).ToString(); break;
				case "Ocirc": x = System.Convert.ToChar(0x00D4).ToString(); break;
				case "ocirc": x = System.Convert.ToChar(0x00F4).ToString(); break;
				case "OElig": x = System.Convert.ToChar(0x0152).ToString(); break;
				case "oelig": x = System.Convert.ToChar(0x0153).ToString(); break;
				case "ograve": x = System.Convert.ToChar(0x00F2).ToString(); break;
				case "Ograve": x = System.Convert.ToChar(0x00D2).ToString(); break;
				case "oline": x = System.Convert.ToChar(0x203E).ToString(); break;
				case "Omega": x = System.Convert.ToChar(0x03A9).ToString(); break;
				case "omega": x = System.Convert.ToChar(0x03C9).ToString(); break;
				case "Omicron": x = System.Convert.ToChar(0x039F).ToString(); break;
				case "omicron": x = System.Convert.ToChar(0x03BF).ToString(); break;
				case "oplus": x = System.Convert.ToChar(0x2295).ToString(); break;
				case "or": x = System.Convert.ToChar(0x2228).ToString(); break;
				case "ordf": x = System.Convert.ToChar(0x00AA).ToString(); break;
				case "ordm": x = System.Convert.ToChar(0x00BA).ToString(); break;
				case "Oslash": x = System.Convert.ToChar(0x00D8).ToString(); break;
				case "oslash": x = System.Convert.ToChar(0x00F8).ToString(); break;
				case "otilde": x = System.Convert.ToChar(0x00F5).ToString(); break;
				case "Otilde": x = System.Convert.ToChar(0x00D5).ToString(); break;
				case "otimes": x = System.Convert.ToChar(0x2297).ToString(); break;
				case "Ouml": x = System.Convert.ToChar(0x00D6).ToString(); break;
				case "ouml": x = System.Convert.ToChar(0x00F6).ToString(); break;
				case "para": x = System.Convert.ToChar(0x00B6).ToString(); break;
				case "part": x = System.Convert.ToChar(0x2202).ToString(); break;
				case "permil": x = System.Convert.ToChar(0x2030).ToString(); break;
				case "perp": x = System.Convert.ToChar(0x22A5).ToString(); break;
				case "Phi": x = System.Convert.ToChar(0x03A6).ToString(); break;
				case "phi": x = System.Convert.ToChar(0x03C6).ToString(); break;
				case "Pi": x = System.Convert.ToChar(0x03A0).ToString(); break;
				case "pi": x = System.Convert.ToChar(0x03C0).ToString(); break;
				case "piv": x = System.Convert.ToChar(0x03D6).ToString(); break;
				case "plusmn": x = System.Convert.ToChar(0x00B1).ToString(); break;
				case "pound": x = System.Convert.ToChar(0x00A3).ToString(); break;
				case "Prime": x = System.Convert.ToChar(0x2033).ToString(); break;
				case "prime": x = System.Convert.ToChar(0x2032).ToString(); break;
				case "prod": x = System.Convert.ToChar(0x220F).ToString(); break;
				case "prop": x = System.Convert.ToChar(0x221D).ToString(); break;
				case "psi": x = System.Convert.ToChar(0x03C8).ToString(); break;
				case "Psi": x = System.Convert.ToChar(0x03A8).ToString(); break;
				case "quot": x = System.Convert.ToChar(0x0022).ToString(); break;
				case "radic": x = System.Convert.ToChar(0x221A).ToString(); break;
				case "rang": x = System.Convert.ToChar(0x232A).ToString(); break;
				case "raquo": x = System.Convert.ToChar(0x00BB).ToString(); break;
				case "rarr": x = System.Convert.ToChar(0x2192).ToString(); break;
				case "rArr": x = System.Convert.ToChar(0x21D2).ToString(); break;
				case "rceil": x = System.Convert.ToChar(0x2309).ToString(); break;
				case "rdquo": x = System.Convert.ToChar(0x201D).ToString(); break;
				case "real": x = System.Convert.ToChar(0x211C).ToString(); break;
				case "reg": x = System.Convert.ToChar(0x00AE).ToString(); break;
				case "rfloor": x = System.Convert.ToChar(0x230B).ToString(); break;
				case "rho": x = System.Convert.ToChar(0x03C1).ToString(); break;
				case "Rho": x = System.Convert.ToChar(0x03A1).ToString(); break;
				case "rlm": x = System.Convert.ToChar(0x200F).ToString(); break;
				case "rsaquo": x = System.Convert.ToChar(0x203A).ToString(); break;
				case "rsquo": x = System.Convert.ToChar(0x2019).ToString(); break;
				case "sbquo": x = System.Convert.ToChar(0x201A).ToString(); break;
				case "Scaron": x = System.Convert.ToChar(0x0160).ToString(); break;
				case "scaron": x = System.Convert.ToChar(0x0161).ToString(); break;
				case "sdot": x = System.Convert.ToChar(0x22C5).ToString(); break;
				case "sect": x = System.Convert.ToChar(0x00A7).ToString(); break;
				case "shy": x = System.Convert.ToChar(0x00AD).ToString(); break;
				case "sigma": x = System.Convert.ToChar(0x03C3).ToString(); break;
				case "Sigma": x = System.Convert.ToChar(0x03A3).ToString(); break;
				case "sigmaf": x = System.Convert.ToChar(0x03C2).ToString(); break;
				case "sim": x = System.Convert.ToChar(0x223C).ToString(); break;
				case "spades": x = System.Convert.ToChar(0x2660).ToString(); break;
				case "sub": x = System.Convert.ToChar(0x2282).ToString(); break;
				case "sube": x = System.Convert.ToChar(0x2286).ToString(); break;
				case "sum": x = System.Convert.ToChar(0x2211).ToString(); break;
				case "sup": x = System.Convert.ToChar(0x2283).ToString(); break;
				case "sup1": x = System.Convert.ToChar(0x00B9).ToString(); break;
				case "sup2": x = System.Convert.ToChar(0x00B2).ToString(); break;
				case "sup3": x = System.Convert.ToChar(0x00B3).ToString(); break;
				case "supe": x = System.Convert.ToChar(0x2287).ToString(); break;
				case "szlig": x = System.Convert.ToChar(0x00DF).ToString(); break;
				case "Tau": x = System.Convert.ToChar(0x03A4).ToString(); break;
				case "tau": x = System.Convert.ToChar(0x03C4).ToString(); break;
				case "there4": x = System.Convert.ToChar(0x2234).ToString(); break;
				case "theta": x = System.Convert.ToChar(0x03B8).ToString(); break;
				case "Theta": x = System.Convert.ToChar(0x0398).ToString(); break;
				case "thetasym": x = System.Convert.ToChar(0x03D1).ToString(); break;
				case "thinsp": x = System.Convert.ToChar(0x2009).ToString(); break;
				case "thorn": x = System.Convert.ToChar(0x00FE).ToString(); break;
				case "THORN": x = System.Convert.ToChar(0x00DE).ToString(); break;
				case "tilde": x = System.Convert.ToChar(0x02DC).ToString(); break;
				case "times": x = System.Convert.ToChar(0x00D7).ToString(); break;
				case "trade": x = System.Convert.ToChar(0x2122).ToString(); break;
				case "Uacute": x = System.Convert.ToChar(0x00DA).ToString(); break;
				case "uacute": x = System.Convert.ToChar(0x00FA).ToString(); break;
				case "uarr": x = System.Convert.ToChar(0x2191).ToString(); break;
				case "uArr": x = System.Convert.ToChar(0x21D1).ToString(); break;
				case "Ucirc": x = System.Convert.ToChar(0x00DB).ToString(); break;
				case "ucirc": x = System.Convert.ToChar(0x00FB).ToString(); break;
				case "Ugrave": x = System.Convert.ToChar(0x00D9).ToString(); break;
				case "ugrave": x = System.Convert.ToChar(0x00F9).ToString(); break;
				case "uml": x = System.Convert.ToChar(0x00A8).ToString(); break;
				case "upsih": x = System.Convert.ToChar(0x03D2).ToString(); break;
				case "Upsilon": x = System.Convert.ToChar(0x03A5).ToString(); break;
				case "upsilon": x = System.Convert.ToChar(0x03C5).ToString(); break;
				case "Uuml": x = System.Convert.ToChar(0x00DC).ToString(); break;
				case "uuml": x = System.Convert.ToChar(0x00FC).ToString(); break;
				case "weierp": x = System.Convert.ToChar(0x2118).ToString(); break;
				case "Xi": x = System.Convert.ToChar(0x039E).ToString(); break;
				case "xi": x = System.Convert.ToChar(0x03BE).ToString(); break;
				case "yacute": x = System.Convert.ToChar(0x00FD).ToString(); break;
				case "Yacute": x = System.Convert.ToChar(0x00DD).ToString(); break;
				case "yen": x = System.Convert.ToChar(0x00A5).ToString(); break;
				case "Yuml": x = System.Convert.ToChar(0x0178).ToString(); break;
				case "yuml": x = System.Convert.ToChar(0x00FF).ToString(); break;
				case "zeta": x = System.Convert.ToChar(0x03B6).ToString(); break;
				case "Zeta": x = System.Convert.ToChar(0x0396).ToString(); break;
				case "zwj": x = System.Convert.ToChar(0x200D).ToString(); break;
				case "zwnj": x = System.Convert.ToChar(0x200C).ToString(); break;
			}
			return x;
		}  // EntityLookup()
	} // class ExtendedHtmlUtility
}
