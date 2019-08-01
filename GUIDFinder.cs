using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Syy.Tools
{
    public class GUIDFinder : EditorWindow
    {
        [MenuItem("Window/GUIDFinder")]
        public static void Open()
        {
            GetWindow<GUIDFinder>("GUIDFinder");
        }

        string _guid;
        UnityEngine.Object _findGUIDTarget;

        void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("■ Find Object by GUID");
            using (new EditorGUILayout.VerticalScope("box"))
            {
                _guid = EditorGUILayout.TextField("Set GUID Here", _guid);
                if (!string.IsNullOrEmpty(_guid))
                {
                    var path = AssetDatabase.GUIDToAssetPath(_guid);
                    var result = AssetDatabase.LoadMainAssetAtPath(path);

                    if (result == null)
                    {
                        EditorGUILayout.LabelField("Result : None");
                    }
                    else
                    {
                        EditorGUILayout.ObjectField("Result : ", result, typeof(UnityEngine.Object), true);
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("■ Find GUID by Object");
            using (new EditorGUILayout.VerticalScope("box"))
            {
                _findGUIDTarget = EditorGUILayout.ObjectField("Set Object Here", _findGUIDTarget, typeof(UnityEngine.Object), false);
                if (_findGUIDTarget != null)
                {
                    var path = AssetDatabase.GetAssetPath(_findGUIDTarget);
                    var guid = AssetDatabase.AssetPathToGUID(path);
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("Result : GUID :" + guid);
                        if (GUILayout.Button("Copy", GUILayout.Width(80)))
                        {
                            EditorGUIUtility.systemCopyBuffer = guid;
                            EditorUtility.DisplayDialog("Copy", guid, "OK");
                        }
                    }
                }
            }
        }
    }
}
