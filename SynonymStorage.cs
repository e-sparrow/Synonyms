using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Birdhouse.Abstractions.Interfaces;
using Birdhouse.Common.Extensions;

namespace Birdhouse.Extended.Synonyms
{
    public sealed class SynonymStorage
        : IInitializable, IDisposable
    {
        public SynonymStorage(string data)
        {
            _data = data;
        }

        private readonly string _data;
        
        private readonly Dictionary<string, Dictionary<string, List<string>>> _nameSynonyms =
            new Dictionary<string, Dictionary<string, List<string>>>();

        private readonly Dictionary<string, string> _reverseNameLookup = new Dictionary<string, string>();

        public void Initialize()
        {
            using (var reader = new StringReader(_data))
            {
                var isFirstLine = true;
                while (reader.ReadLine() is { } line)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    var parts = line.Split(',');
                    if (parts.Length != 3)
                        continue;

                    var baseName = parts[0].Trim();
                    var temperature = parts[1].Trim();
                    var variant = parts[2].Trim();

                    if (!_nameSynonyms.ContainsKey(baseName))
                    {
                        _nameSynonyms[baseName] = new Dictionary<string, List<string>>();
                    }

                    if (!_nameSynonyms[baseName].ContainsKey(temperature))
                    {
                        _nameSynonyms[baseName][temperature] = new List<string>();
                    }

                    _nameSynonyms[baseName][temperature].Add(variant);
                    _reverseNameLookup[variant] = baseName;
                }
            }
        }

        public void Dispose()
        {
            _nameSynonyms.Clear();
            _reverseNameLookup.Clear();
        }

        public bool IsBaseName(string name)
        {
            var result = _nameSynonyms.Keys.Contains(name);
            return result;
        }

        public string GetBaseName(string name)
        {
            var result = _reverseNameLookup.TryGetValue(name, out var baseName) ? baseName : name;
            return result;
        }

        public bool TryGetBaseName(string name, out string result)
        {
            var isBaseName = IsBaseName(name);
            if (isBaseName)
            {
                result = name;
                return true;
            }
            
            var hasBaseName = _reverseNameLookup.TryGetValue(name, out result);
            return hasBaseName;
        }

        public IEnumerable<string> GetNameVariants(string baseName, string temperature)
        {
            if (_nameSynonyms.TryGetValue(baseName, out var temperatureDict) &&
                temperatureDict.TryGetValue(temperature, out var variants))
            {
                return variants;
            }

            return baseName.AsSingleArray();
        }

        public string GetRandomNameVariant(string baseName, string temperature)
        {
            var result = GetNameVariants(baseName, temperature).GetRandom();
            return result;
        }
    }
}