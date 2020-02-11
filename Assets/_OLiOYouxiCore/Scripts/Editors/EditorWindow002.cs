using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxi.Editors
{
    using OLiOYouxi.OCoroutine;
    using System.Text;
    using Coroutine = OLiOYouxi.OCoroutine.OCoroutine;
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using OMathf = OLiOYouxi.OSystem.MathUtility;
    public class EditorWindow002 : EditorWindow
    {
        [MenuItem("OLiOYouxi/EditorWindow002")]
        static void Open()
        {
            EditorWindow002 window = GetWindow(typeof(EditorWindow002), true) as EditorWindow002;
            window.curve = new AnimationCurve();
            window.curveContrary = new AnimationCurve();
            window.content = new GUIContent("曲线");
            window.indent = 4.0f;
            window.@string = new StringBuilder();
        }

        AnimationCurve curve = null;
        AnimationCurve curveContrary = null;
        GUIContent content = null;
        StringBuilder @string = null;
        float indent = float.MinValue;
        private void OnGUI()
        {
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                using (var _toRight = new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(indent);
                    curve = EditorGUILayout.CurveField(content, curve);
                }
                using (var _toRight = new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(indent);
                    curveContrary = EditorGUILayout.CurveField(content, curveContrary);
                }
                using (var _toRight = new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(indent);
                    if (GUILayout.Button("查看值"))
                    {
                        for (int i = 0,length = curve.keys.Length; i < length; i++) @string.Append($"\nvalue：{curve.keys[i].value}");
                        Debug.Log = @string.ToString();
                        @string.Clear();
                    }
                    if (GUILayout.Button("生成新的"))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            float value = Random.Range(-5f, 5f);
                            float v1 = OMathf.Normalized(value, new Vector2Int(-5, 5), new Vector2Int(-1, 1));
                            float v2 = 0f;
                            float time = Random.Range(0f, 10f);
                            Debug.Log = $"\nvalue：{value.ToString()}\nv1：{v1}\nv2：{v2}";
                            curve.AddKey(new Keyframe { value = v1, time = time });
                            curveContrary.AddKey(new Keyframe { value = v2, time = time });
                        }
                    }
                }
            }
        }
    }
}
