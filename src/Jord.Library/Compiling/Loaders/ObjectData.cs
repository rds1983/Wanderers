﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Jord.Compiling.Loaders
{
	public class ObjectData
	{
		public string Source;
		public JObject Data;
		public Dictionary<string, string> Properties;
	}
}
