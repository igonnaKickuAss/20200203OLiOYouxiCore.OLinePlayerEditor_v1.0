using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text;
namespace OLiOYouxiCore.OLinePlayer
{
    using Coroutine = OLiOYouxi.OCoroutine.OCoroutine;
    using OLiOYouxi.OCoroutine;

    internal class LinePlayerEditorWindowTest002 : EditorWindow
    {
        TextController textController = TextController.ForNewOrExistingDisplayText();
        string testText1 = "撒大青蛙擦撒大青蛙擦拭啊是的拭撒大青撒大青蛙擦拭啊是的蛙擦拭撒大青蛙擦拭撒大青蛙擦拭啊是的请问撒大青蛙擦拭啊是的";
        string testValue1 = string.Empty;
        int isPlaying = 0;
        GUIContent content = new GUIContent();
        private void OnDisable()
        {
            textController = null;
            this.StopAllCoroutine();
        }
        private void OnGUI()
        {
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                if (GUILayout.Button("播放测试"))
                {
                    isPlaying += 1;
                    this.StartCoroutine(PlaySentence(testText1));
                }
                GUILayout.Space(44f);
                if (isPlaying != 0) GUI.enabled = false;
                GUI.skin.textArea.fontSize = 32;
                GUI.skin.textArea.wordWrap = true;
                content.text = testText1;
                content.tooltip = string.Empty;
                content.image = null;
                float width = 600f;
                float height = GUI.skin.textArea.CalcHeight(content, width);
                testValue1 = EditorGUILayout.TextArea(testValue1, GUI.skin.textArea, GUILayout.Width(width), GUILayout.Height(height));
                GUI.skin.textArea.fontSize = 0;
                GUI.skin.textArea.wordWrap = false;
                GUI.enabled = true;
            }
            Repaint();
        }

        private void Update()
        {
            if (textController.HasNoChanged) return;
            testValue1 = textController.OutValue();
        }

        private IEnumerator PlaySentence(string data)
        {
            WaitForSeconds seconds = new WaitForSeconds(.02f);
            for (int i = 0, length = data.Length; i < length; i++)
            {
                char letter = data[i];
                yield return seconds;
                textController.AppendValue(ref letter);
            }
        }

        private class TextController
        {
            #region -- Private Data --
            private StringBuilder stringBuilder = null;
            #endregion

            #region -- VAR --
            int before = 0;
            int after = 0;
            #endregion

            #region -- Public ShotC --
            public bool HasNoChanged => before == after;

            #endregion

            #region -- 单例 --
            static private TextController instance = null;
            private TextController() => this.stringBuilder = new StringBuilder();
            internal static TextController ForNewOrExistingDisplayText()
                => instance == null ? instance = new TextController() : instance;
            #endregion

            #region -- Public APIMethods --
            public void AppendValue(ref char value)
            {
                stringBuilder.Append(value);
                after = 1;
            }
            public string OutValue()
            {
                after = 0;
                return stringBuilder.ToString();
            }
            public void ClearValue() => stringBuilder.Clear();
            #endregion
        }
    }
}

