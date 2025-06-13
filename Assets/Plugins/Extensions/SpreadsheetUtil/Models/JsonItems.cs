using System;
using System.Collections.Generic;

namespace Plugins.Extensions.SpreadsheetUtil.Models
{
	[Serializable]
	public class JsonItems<T>
	{
		public List<T> List;
	}
}