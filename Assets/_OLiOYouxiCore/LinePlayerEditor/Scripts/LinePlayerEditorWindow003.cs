using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using Object = UnityEngine.Object;
    using Coroutine = OLiOYouxi.OCoroutine.OCoroutine;
    using OLiOYouxi.OCoroutine;
    using UnityEditor.Experimental.UIElements;
    using System.Text;

    internal class LinePlayerEditorWindow003 : OEditorWindow
    {
        #region -- Private Data --
        private EditorCustomer sentenceEditorCustomer = null;
        private Editor sentenceEditorDefault = null;
        private _SentenceScriptableObj m_SentenceData = null;
        private _ChatboxScriptableObj m_ChatboxData = null;
        #endregion

        #region -- Public ShotC --
        public _SentenceScriptableObj sentenceData
        {
            get => m_SentenceData;
            internal set => m_SentenceData = value;
        }
        public _ChatboxScriptableObj chatboxData
        {
            get => m_ChatboxData;
            internal set => m_ChatboxData = value;
        }

        #endregion

        #region -- MONO APIMethods --
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
            if (m_SentenceData != null) EditorUtility.SetDirty(m_SentenceData);
            m_SentenceData = null;
        }
        private void Update()
        {
            if (Text.HasNoChanged) return;
            sentenceEditorCustomer.text = Text.OutValue();
        }
        #endregion

        #region -- Override APIMethods --
        protected override bool Row1()
        {
            GUI.enabled = false;
            m_SentenceData = (_SentenceScriptableObj)EditorGUILayout.ObjectField(
                m_SentenceData,
                typeof(_SentenceScriptableObj),
                false,
                Constants.maxWidth
                );
            GUI.enabled = true;
            return true;
        }
        protected override bool Row2() => DisplayCustomer(m_SentenceData);

        #endregion

        #region -- Private APIMethods --
        private bool DisplayCustomer(object sentenceData)
        {
            if (sentenceData == null) return false;
            sentenceEditorCustomer = sentenceEditorCustomer
                ?? (sentenceEditorCustomer = EditorCustomer.CreateEditor(
                    new ChatboxEditor003
                    {
                        SentenceData = (_SentenceScriptableObj)sentenceData,
                        ChatboxData = (_ChatboxScriptableObj)chatboxData,
                        TextData = Text
                    }) as EditorCustomer
                    );
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                sentenceEditorCustomer.OnInspectorGUI();
                return true;
            }
        }
        #endregion

        #region -- LayoutDrawer --
        [CustomEditor(typeof(ChatboxEditor003), false), CanEditMultipleObjects]
        private class EditorCustomer : OEditor
        {
            #region -- Private Data --
            private _SentenceScriptableObj m_SentenceData = null;
            private _ChatboxScriptableObj m_ChatboxData = null;
            private TextController m_TextData = null;
            private SerializedObject m_Object = null;
            private Dictionary<float, WaitForSeconds> waitForSecondsMapper = null;
            private Coroutine coroutine = null;
            private AnimationCurve curve = null;
            private string strSentence = string.Empty;
            private string prevSentence = string.Empty;
            #endregion

            #region -- MONO APIMethods --
            private void OnEnable()
            {
                m_SentenceData = ((ChatboxEditor003)base.target).SentenceData;
                m_ChatboxData = ((ChatboxEditor003)base.target).ChatboxData;
                m_TextData = ((ChatboxEditor003)base.target).TextData;
                m_Object = CreateEditor(m_ChatboxData).serializedObject;
                waitForSecondsMapper = new Dictionary<float, WaitForSeconds>();
                curve = m_SentenceData.Curve;
                StringBuilder @string = new StringBuilder();
                m_SentenceData.Value.ForEach(f => @string.Append(f.value));
                strSentence = @string.ToString();
            }
            private void OnDisable()
            {
                this.StopAllCoroutine();
                m_ChatboxData = null;
                m_TextData = null;
                m_Object = null;
                waitForSecondsMapper = null;
                coroutine = null;
                curve = null;
            }
            #endregion

            #region -- Override APIMethods --
            protected override bool ColumnWithTitleAndContent() => base.ColumnWithTitleAndContent();
            protected override bool RowWithTitleAndContent()
            {
                content.text = "讲话的速度：";
                content.tooltip = "";
                content.image = null;
                GUI.skin.label.fontSize = Constants.fontSizeForLabel;
                GUI.skin.label.fontStyle = FontStyle.Bold;
                Vector2 labelSize = GUI.skin.label.CalcSize(content);
                EditorGUILayout.SelectableLabel(
                    content.text,
                    GUI.skin.label,
                    GUILayout.Width(labelSize.x),
                    GUILayout.Height(labelSize.y)
                    );
                GUI.skin.label.fontSize = 0;
                GUI.skin.label.fontStyle = FontStyle.Normal;
                curve = EditorGUILayout.CurveField(curve, Color.green, new Rect(new Vector2(0, .01f), new Vector2(100, 400)), GUILayout.Height(300f));
                m_SentenceData.Curve = curve;

                using (var toRight = new EditorGUILayout.HorizontalScope())
                {
                    GUIContent playBtnContent = EditorGUIUtility.IconContent("PlayButton", "播放");
                    GUI.SetNextControlName("playBtn");
                    Vector2 btnSize = GUI.skin.button.CalcSize(playBtnContent);
                    if (GUILayout.Button(playBtnContent, GUILayout.Width(btnSize.x)))
                    {
                        GUI.FocusControl("playBtn");
                        this.StopAllCoroutine();
                        m_TextData.ClearValue();
                        coroutine = this.StartCoroutine(PlaySentence());
                    }
                    GUIContent stopContent = EditorGUIUtility.IconContent("CN Box", "停止");
                    GUI.SetNextControlName("stopBtn");
                    if (GUILayout.Button(stopContent, GUILayout.Width(btnSize.x), GUILayout.Height(btnSize.y)))
                    {
                        GUI.FocusControl("stopBtn");
                        this.StopAllCoroutine();
                        m_TextData.ClearValue();
                    }
                }

                GUI.enabled = false;
                GUI.skin.textArea.fontSize = Constants.fontSizeForTextArea;
                GUI.skin.textArea.wordWrap = true;
                GUI.skin.textArea.stretchHeight = true;
                if (coroutine == null) prevSentence = strSentence;
                else prevSentence = text;
                prevSentence = EditorGUILayout.TextArea(prevSentence, GUI.skin.textArea);
                GUI.skin.textArea.fontSize = 0;
                GUI.skin.textArea.wordWrap = false;
                GUI.skin.textArea.stretchHeight = false;
                GUI.enabled = true;
                
                return true;
            }
            #endregion

            #region -- Private APIMethods --
            private IEnumerator PlaySentence()
            {
                for (int i = 0, count = m_SentenceData.Value.Count; i < count; i++)
                {
                    //Debug.Log = $"第{text}次";
                    Letter letter = m_SentenceData.Value[i];
                    WaitForSeconds seconds = waitForSecondsMapper.ContainsKey(letter.remain) ?
                        waitForSecondsMapper[letter.remain] :
                        waitForSecondsMapper[letter.remain] = new WaitForSeconds(letter.remain);
                    yield return seconds;
                    m_TextData.AppendValue(ref letter.value);
                }
                m_TextData.ClearValue();
                coroutine = null;
            }

            #endregion
        }
        #endregion
    }
}