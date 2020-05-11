﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TroublesOfJord.Core;
using Newtonsoft.Json.Linq;
using Module = TroublesOfJord.Core.Module;
using TroublesOfJord.Core.Items;
using TroublesOfJord.Compiling.Loaders;
using TroublesOfJord.Generation;

namespace TroublesOfJord.Compiling
{
	public class Compiler
	{
		private readonly Module _module = new Module();
		private readonly Dictionary<Type, BaseLoader> _loaders = new Dictionary<Type, BaseLoader>();

		public Compiler()
		{
			_loaders[typeof(TileSet)] = new TileSetLoader();
			_loaders[typeof(Map)] = new MapLoader();
			_loaders[typeof(MapTemplate)] = new MapTemplateLoader();
			_loaders[typeof(TileInfo)] = new Loader<TileInfo>("TileInfos");
			_loaders[typeof(CreatureInfo)] = new CreatureLoader();
			_loaders[typeof(BaseItemInfo)] = new ItemLoader();
			_loaders[typeof(Class)] = new Loader<Class>("Classes");
			_loaders[typeof(BaseGenerator)] = new GeneratorLoader();
		}

		private void FirstRun(IEnumerable<string> sources)
		{
			if (CompilerParams.Verbose)
			{
				TJ.LogInfo("{0} source files found", sources.Count());
			}

			// First run - parse json and build object maps
			foreach (var s in sources)
			{
				if (CompilerParams.Verbose)
				{
					TJ.LogInfo("Processing {0}", s);
				}

				JObject json;

				try
				{
					json = JObject.Parse(File.ReadAllText(s));
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("JSON parsing error. Source '{0}'. Error '{1}'",
						s, ex.ToString()));
				}

				foreach (var pair in json)
				{
					var key = pair.Key;
					if (key == CompilerUtils.TileSetName)
					{
						// Special case
						if (json.Count > 1)
						{
							throw new Exception(string.Format("Tileset file can have only one tileset entry. Source: '{0}'", s));
						}

						var obj = (JObject)pair.Value;
						JToken idToken;
						if (!obj.TryGetValue(CompilerUtils.IdName, out idToken))
						{
							throw new Exception(string.Format("Tileset object lacks id. Source: '{0}'", s));
						}

						var id = idToken.ToString();

						_loaders[typeof(TileSet)].SafelyAddObject(id, s, (JObject)pair.Value);

						continue;
					}
					else if (key == CompilerUtils.MapName)
					{
						// Another special case
						if (json.Count > 1)
						{
							throw new Exception(string.Format("Map file can have only one map entry. Source: '{0}'", s));
						}

						var obj = (JObject)pair.Value;
						JToken idToken;
						if (!obj.TryGetValue(CompilerUtils.IdName, out idToken))
						{
							throw new Exception(string.Format("Map object lacks id. Source: '{0}'", s));
						}

						var id = idToken.ToString();

						_loaders[typeof(Map)].SafelyAddObject(id, s, (JObject)pair.Value);

						continue;
					}

					BaseLoader loader = null;
					foreach (var pair2 in _loaders)
					{
						if (pair2.Value.JsonArrayName == key)
						{
							loader = pair2.Value;
							break;
						}
					}

					if (loader == null)
					{
						throw new Exception(string.Format("Unknown object type '{0}', source: '{1}", key, s));
					}

					foreach (var pair2 in (JObject)pair.Value)
					{
						loader.SafelyAddObject(pair2.Key, s, (JObject)pair2.Value);
					}
				}
			}
		}

		private void FillData<T>(Dictionary<string, T> output) where T : BaseObject
		{
			((Loader<T>)_loaders[typeof(T)]).FillData(_module, output);
		}

		public void FindSources(string path, bool isTop, List<string> result)
		{
			var files = Directory.EnumerateFiles(path, "*.json", SearchOption.TopDirectoryOnly);
			result.AddRange(files);

			var subFolders = Directory.EnumerateDirectories(path);

			foreach (var sf in subFolders)
			{
				if (isTop && sf == "modules")
				{
					continue;
				}

				FindSources(sf, false, result);
			}
		}

		public Module Process(string path)
		{
			// First run - parse json and build object maps
			var sources = new List<string>();
			FindSources(path, true, sources);

			FirstRun(sources);

			// Second run - build module

			// Tile Sets
			FillData(_module.TileSets);

			// Tile Infos
			FillData(_module.TileInfos);

			// Item Infos
			FillData(_module.ItemInfos);

			// Creature Infos
			FillData(_module.CreatureInfos);

			// Classes
			FillData(_module.Classes);

			// Generators
			FillData(_module.Generators);

			// Maps
			FillData(_module.Maps);

			// Map templates
			FillData(_module.MapTemplates);

			return _module;
		}
	}
}