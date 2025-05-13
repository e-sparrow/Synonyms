using UnityEditor;
using UnityEngine;

namespace Birdhouse.Extended.Synonyms.Unity
{
    [CreateAssetMenu(menuName = "Birdhouse/Synonym/Settings", fileName = "Synonym Settings")]
    public sealed class SynonymSettings
        : ScriptableSingleton<SynonymSettings>
    {
        [field: SerializeField]
        public TextAsset SynonymsSheet
        {
            get;
            set;
        }
    }
}