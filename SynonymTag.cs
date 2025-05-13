using System.Collections.Generic;
using Birdhouse.Tools.Strings.Abstractions;

namespace Birdhouse.Extended.Synonyms
{
    public sealed class SynonymTag
        : ITag
    {
        private const string TemperatureName = "Temperature";
        
        public string Process(string input, IDictionary<string, string> parameters = null)
        {
            var hasTemperature = parameters.TryGetValue(TemperatureName, out var temperature);
            if (!hasTemperature)
            {
                return input;
            }

            var result = SynonymHelper.Storage.GetRandomNameVariant(input, temperature);
            return result;
        }
    }
}