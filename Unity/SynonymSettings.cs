using Birdhouse.Common.Singleton.Scriptable;
using Birdhouse.Common.Singleton.Scriptable.Attributes;
using UnityEngine;

namespace Birdhouse.Extended.Synonyms.Unity
{
    [ScriptableSingletonPath("Birdhouse/Settings/SynonymSettings")]
    [CreateAssetMenu(menuName = "Birdhouse/Synonym/Settings", fileName = "Synonym Settings")]
    public sealed class SynonymSettings
        : ScriptableSingletonBase<SynonymSettings>
    {
        [field: SerializeField]
        public TextAsset SynonymsSheet
        {
            get;
            set;
        }
    }
}