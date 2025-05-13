using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Birdhouse.Extended.Synonyms.Unity
{
    public sealed class SynonymSettingsWindow
        : EditorWindow
    {
        private SynonymSettings _settings;
        
        [MenuItem("Birdhouse/Synonym/Settings")]
        public static void ShowWindow()
        {
            GetWindow<SynonymSettingsWindow>("Synonym Settings");
        }

        private void OnEnable()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(SynonymSettings)}");
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                _settings = AssetDatabase.LoadAssetAtPath<SynonymSettings>(path);
            }
        }

        private void OnGUI()
        {
            if (_settings == null)
            {
                EditorGUILayout.HelpBox("Настройки не найдены. Пожалуйста, создайте объект настроек.", MessageType.Warning);
                if (GUILayout.Button("Создать настройки"))
                {
                    _settings = CreateInstance<SynonymSettings>();
                    var path = "Assets/Settings/SynonymSettings.asset";
                    if (!AssetDatabase.IsValidFolder("Assets/Settings"))
                    {
                        AssetDatabase.CreateFolder("Assets", "Settings");
                    }
                    
                    AssetDatabase.CreateAsset(_settings, path);
                    AssetDatabase.SaveAssets();
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = _settings;
                }
                return;
            }

            EditorGUILayout.LabelField("Настройки синонимов имён", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            _settings.SynonymsSheet = (TextAsset) EditorGUILayout.ObjectField("CSV Файл", _settings.SynonymsSheet, typeof(TextAsset), false);

            EditorUtility.SetDirty(_settings);
        }
    }
}