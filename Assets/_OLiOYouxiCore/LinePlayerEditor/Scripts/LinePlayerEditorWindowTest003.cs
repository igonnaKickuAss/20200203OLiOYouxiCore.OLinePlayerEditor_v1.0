using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
namespace OLiOYouxiCore.OLinePlayer
{
    internal class LinePlayerEditorWindowTest003 : EditorWindow
    {
        TestCurve testCurve = null;
        SerializedProperty m_Curve = null;
        AnimationCurve curve = null;
        private void OnEnable()
        {
            testCurve = new TestCurve();
            m_Curve = Editor.CreateEditor(testCurve).serializedObject.FindProperty("m_Curve");
            curve = new AnimationCurve(new Keyframe[2] { new Keyframe(2f, 12), new Keyframe(4f, 1) });
        }
        private void OnGUI()
        {
            curve = EditorGUI.CurveField(new Rect(new Vector2(100f, 50f), new Vector2(500f, 200f)), curve);
            //EditorGUIUtility.DrawCurveSwatch(new Rect(new Vector2(100f, 50f), new Vector2(500f, 200f)), curve, m_Curve, Color.green, Color.gray);
            //EditorGUI.CurveField(
            //    new Rect(new Vector2(100f, 50f),
            //    new Vector2(500f, 200f)), m_Curve,
            //    Color.green,
            //    new Rect(new Vector2(-10f, 10f), new Vector2(-10f, 10f))
            //    );

            Repaint();
        }

        private class TestCurve : Editor
        {
            private AnimationCurve m_Curve = new AnimationCurve();
            public AnimationCurve Curve
            {
                get => m_Curve;
                internal set => m_Curve = value;
            }
        }
    }

    
}

