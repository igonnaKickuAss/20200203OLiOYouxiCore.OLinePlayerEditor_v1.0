using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    internal class LinePlayerEditorWindowTest001 : EditorWindow
    {
        string testText1 = string.Empty;
        float height = 0;

        private float rotAngle = 0;       //旋转的角度
        private Vector2 pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);       //旋转时的中心坐标
        private void OnGUI()
        {
            //GUI.skin.textArea.fontSize = 16;
            ////GUI.skin.textArea.stretchHeight = true;
            //GUI.skin.textArea.wordWrap = true;
            //testText1 = EditorGUILayout.TextArea(testText1, GUI.skin.textArea, GUILayout.Height(GetHeight(testText1)), GUILayout.Width(300f));
            //GUI.skin.textArea.fontSize = 0;
            ////GUI.skin.textArea.stretchHeight = false;
            //GUI.skin.textArea.wordWrap = false;
            //float a = GUI.skin.textArea.CalcHeight(new GUIContent(), 100f);

            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 100, 50, 50), "right"))
                rotAngle += 10;

            if (GUI.Button(new Rect(Screen.width / 2 + 25, Screen.height / 2 - 100, 50, 50), "left"))
                rotAngle -= 10;

            Matrix4x4 matrix = GUI.matrix;                 //记录此时的矩阵

            //var col0 = GUI.matrix.GetColumn(0);
            //var col1 = GUI.matrix.GetColumn(1);
            //var col2 = GUI.matrix.GetColumn(2);
            //var col3 = GUI.matrix.GetColumn(3);

            //var row0 = GUI.matrix.GetRow(0);
            //var row1 = GUI.matrix.GetRow(1);
            //var row2 = GUI.matrix.GetRow(2);
            //var row3 = GUI.matrix.GetRow(3);

            //GUI.matrix = new Matrix4x4(
            //    col3,
            //    col2,
            //    col1,
            //    col0
            //    );

            //GUI.matrix = new Matrix4x4(
            //    new Vector4(0, 0, 0, 1),
            //    new Vector4(0, 0, 1, 0),
            //    new Vector4(0, 1, 0, 0),
            //    new Vector4(1, 0, 0, 0)
            //    );
            
            GUIUtility.RotateAroundPivot(rotAngle, pivotPoint); //旋转
            //Vector3 v3 = new Vector3(Screen.width / 2 - 25, Screen.height / 2 - 25);    //"(455.0, 275.0, 0.0)"
            //Vector3 v3 = GUI.matrix.MultiplyVector(new Vector3(Screen.width / 2 - 25, Screen.height / 2 - 25));
            //Vector4 test1v4 = GUI.matrix.inverse.MultiplyPoint3x4(new Vector3(Screen.width / 2 - 25, Screen.height / 2 - 25));
            //Vector4 test2v4 = GUI.matrix.transpose.MultiplyPoint3x4(new Vector3(Screen.width / 2 - 25, Screen.height / 2 - 25));
            //float sin = Mathf.Sin(-180);
            //float cos = Mathf.Cos(-180);

            //Vector4 col0 = new Vector4(1, 0, 0, 0);
            //Vector4 col1 = new Vector4(0, cos, sin, 0);
            //Vector4 col2 = new Vector4(0, -sin, cos, 0);
            //Vector4 col3 = new Vector4(0, 0, 0, 1);

            //Vector4 col0 = new Vector4(cos, 0, -sin, 0);
            //Vector4 col1 = new Vector4(0, 1, 0, 0);
            //Vector4 col2 = new Vector4(sin, 0, cos, 0);
            //Vector4 col3 = new Vector4(0, 0, 0, 1);

            //Vector4 col0 = new Vector4(cos, sin, 0, 0);
            //Vector4 col1 = new Vector4(-sin, cos, 0, 0);
            //Vector4 col2 = new Vector4(0, 0, 1, 0);
            //Vector4 col3 = new Vector4(0, 0, 0, 1);

            //GUI.matrix = new Matrix4x4(
            //    col0,
            //    col1,
            //    col2,
            //    col3
            //    );

            GUI.matrix = Matrix4x4.TRS(
                new Vector3(0, 0, 0),
                Quaternion.Euler(0, 180, 0),
                new Vector3(
                    Screen.width / Screen.width,
                    Screen.height / Screen.height,
                    1.0f)
                );

            Vector3 test3v4 = GUI.matrix.MultiplyPoint3x4(new Vector3(Screen.width / 2 - 25, Screen.height / 2 - 25));

            Debug.Log(GUI.matrix);
            //Debug.Log(GUI.matrix.inverse);
            //Debug.Log(GUI.matrix.transpose);
            //Debug.Log(test1v4);
            //Debug.Log(test2v4);
            Debug.Log(test3v4);
            GUI.Button(new Rect(new Vector2(test3v4.x, test3v4.y), new Vector2(100, 50)), "好久没吃奥利奥");

            GUI.matrix = matrix;

            //GUI.matrix = Matrix4x4.TRS(
            //    new Vector3(0, 0, 0),
            //    Quaternion.identity,
            //    new Vector3(
            //        Screen.width / resolutionWidth,
            //        Screen.height / resolutionHeight,
            //        1.0f)
            //        );
            //Rect boxRect = new Rect(
            //    resolutionWidth / 2.0f - 240f, resolutionHeight / 2.0f - 170,
            //    480, 340);
            //GUI.Box(boxRect, "");
            //GUI.matrix = matrix;
        }
        float resolutionWidth = 800.0f;
        float resolutionHeight = 480.0f;
        
        private float GetHeight(string testText1)
        {
            return GUI.skin.textArea.CalcHeight(new GUIContent(testText1), 300f);
        }
    }

}
