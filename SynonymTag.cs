using System.Collections.Generic;
using Birdhouse.Tools.Strings.Abstractions;
using UnityEngine;

namespace Birdhouse.Extended.Synonyms
{
    public sealed class SynonymTag
        : ITag
    {
        private const string TemperatureName = "temperature";
        
        public string Process(string input, IDictionary<string, string> parameters = null)
        {
            var hasTemperature = parameters.TryGetValue(TemperatureName, out var temperature);
            if (!hasTemperature)
            {
                return input;
            }

            var name = input;
            if (!SynonymHelper.Storage.IsBaseName(name))
            {
                var hasName = SynonymHelper.Storage.TryGetBaseName(input, out var baseName);
                if (!hasName)
                {
                    Debug.LogWarning($"No base name for {name} found");
                }
                else
                {
                    name = baseName;
                }
            }

            var result = SynonymHelper.Storage.GetRandomNameVariant(name, temperature);
            return result;
        }
    }
}