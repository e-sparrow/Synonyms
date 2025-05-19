using System;
using Birdhouse.Extended.Synonyms.Unity;

namespace Birdhouse.Extended.Synonyms
{
    public static class SynonymHelper
    {
        private static readonly Lazy<SynonymStorage> LazyStorage = new Lazy<SynonymStorage>(CreateStorage);

        public static SynonymStorage Storage => LazyStorage.Value;

        public static SynonymStorage CreateStorage()
        {
            var sheet = SynonymSettings.Instance.SynonymsSheet;
            
            var storage = new SynonymStorage(sheet.text);
            storage.Initialize();

            return storage;
        }
    }
}