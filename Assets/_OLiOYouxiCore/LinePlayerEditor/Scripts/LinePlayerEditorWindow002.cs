using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;
using System.Text;

namespace OLiOYouxiCore.OLinePlayer
{
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using static _ChatboxScriptableObj;
    using Object = UnityEngine.Object;
    using Coroutine = OLiOYouxi.OCoroutine.OCoroutine;
    using OLiOYouxi.OSystem;
    using OLiOYouxi.OCoroutine;
    internal class LinePlayerEditorWindow002 : OEditorWindow
    {
        #region -- Private Data --
        private EditorCustomer chatboxEditorCustomer = null;
        private Editor chatboxEditorDefault = null;
        private Conversation m_ConversationData = null;
        private _ChatboxScriptableObj m_ChatboxData = null;
        #endregion

        #region -- Public ShotC --
        public Conversation conversationData
        {
            get => m_ConversationData;
            internal set => m_ConversationData = value;
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
            if (m_ChatboxData != null) EditorUtility.SetDirty(m_ChatboxData);
            if (m_ConversationData != null) 
            {
                m_ConversationData.Value.ForEach((f) =>
                {
                    EditorUtility.SetDirty(f);
                    f.Value.ForEach((ff) => EditorUtility.SetDirty(ff));
                });
            }
            m_ConversationData = null;
            m_ChatboxData = null;
        }
        private void Update()
        {
            if (Text.HasNoChanged) return;
            chatboxEditorCustomer.text = Text.OutValue();
        }
        #endregion

        #region -- Override APIMethods --
        protected override bool Row1()
        {
            GUI.enabled = false;
            m_ChatboxData = (_ChatboxScriptableObj)EditorGUILayout.ObjectField(
                m_ChatboxData,
                typeof(_ChatboxScriptableObj),
                false,
                Constants.maxWidth
                );
            GUI.enabled = true;
            return true;
        }
        protected override bool Row2() => DisplayCustomer(m_ConversationData, m_ChatboxData);

        #endregion

        #region -- Private APIMethods --
        private bool DisplayCustomer(object conversationData, Object chatboxData)
        {
            if (conversationData == null || chatboxData == null) return false;
            chatboxEditorCustomer = chatboxEditorCustomer
                ?? (chatboxEditorCustomer = EditorCustomer.CreateEditor(
                    new ChatboxEditor002 {
                        ConversationData = (Conversation)conversationData,
                        ChatboxData = (_ChatboxScriptableObj)chatboxData,
                        TextData = Text
                    }) as EditorCustomer
                    );
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                chatboxEditorCustomer.OnInspectorGUI();
                return true;
            }
        }
        #endregion

        #region -- LayoutDrawer --
        [CustomEditor(typeof(ChatboxEditor002), false), CanEditMultipleObjects]
        private class EditorCustomer : OEditor
        {
            #region -- Private Data --
            private _ChatboxScriptableObj m_ChatboxData = null;
            private Conversation m_ConversationData = null;
            private TextController m_TextData = null;
            private SerializedObject m_Object = null;
            private Dictionary<int, ReorderableList> listMapper = null;
            private Dictionary<int, Vector2> listMainSizeMapper = null;
            private Dictionary<int, Vector2> listMinorSizeMapper = null;
            private Dictionary<int, Coroutine> coroutineMapper = null;
            private Dictionary<int, Vector2Int> statusMapper = null;
            private Dictionary<float, WaitForSeconds> waitForSecondsMapper = null;
            private int mainIndex = int.MinValue;
            private bool m_IsChanged = false;
            #endregion

            #region -- VAR --
            int currentIndex = -1;

            #endregion

            #region -- MONO APIMethods --
            private void OnEnable()
            {
                m_ChatboxData = ((ChatboxEditor002)base.target).ChatboxData;
                m_ConversationData = ((ChatboxEditor002)base.target).ConversationData;
                m_TextData = ((ChatboxEditor002)base.target).TextData;
                m_Object = CreateEditor(m_ChatboxData).serializedObject;
                listMapper = new Dictionary<int, ReorderableList>();
                listMainSizeMapper = new Dictionary<int, Vector2>();
                listMinorSizeMapper = new Dictionary<int, Vector2>();
                coroutineMapper = new Dictionary<int, Coroutine>();
                statusMapper = new Dictionary<int, Vector2Int>();
                waitForSecondsMapper = new Dictionary<float, WaitForSeconds>();
                mainIndex = int.MinValue;
                m_IsChanged = true;
                //handle
                m_ConversationData.Value.RemoveAll((f) => f == null);
                for (int i = 0, count = m_ConversationData.Value.Count; i < count; i++)
                    m_ConversationData.Value[i].Value.RemoveAll((f) => f == null);
            }
            private void OnDisable()
            {
                this.StopAllCoroutine();
                m_ChatboxData = null;
                m_ConversationData = null;
                m_TextData = null;
                m_Object = null;
                listMapper = null;
                listMainSizeMapper = null;
                listMinorSizeMapper = null;
                coroutineMapper = null;
                waitForSecondsMapper = null;
                statusMapper = null;
            }
            #endregion

            #region -- Override APIMethods --
            protected override bool ColumnWithTitleAndContent()
            {
                content.text = "标题：";
                content.tooltip = "";
                content.image = null;
                GUI.skin.label.fontSize = Constants.fontSizeForLabel;
                GUI.skin.label.fontStyle = FontStyle.Bold;
                EditorGUILayout.SelectableLabel(content.text, GUI.skin.label, GUILayout.Width(GUI.skin.label.CalcSize(content).x));
                GUI.skin.label.fontSize = 0;
                GUI.skin.label.fontStyle = FontStyle.Normal;

                content.text = m_ConversationData.Title;
                content.tooltip = string.Empty;
                content.image = null;
                GUI.skin.textField.fontSize = Constants.fontSizeForTextField;
                GUI.skin.textField.fontStyle = FontStyle.Normal;
                if (m_ConversationData.Title.Length > 6)
                {
                    GUI.skin.textField.CalcMinMaxWidth(content, out float minWidth, out float maxWidth);
                    GUILayoutOption heightOption = GUILayout.Height(GUI.skin.textField.CalcHeight(content, 1f));
                    GUILayoutOption minWidthOption = GUILayout.MinWidth(minWidth);
                    GUILayoutOption maxWidthOption = GUILayout.MaxWidth(maxWidth + 8f);
                    m_ConversationData.Title = EditorGUILayout.TextField(
                        m_ConversationData.Title,
                        GUI.skin.textField,
                        heightOption,
                        minWidthOption,
                        maxWidthOption
                        );
                }
                else
                {
                    GUILayoutOption heightOption = GUILayout.Height(GUI.skin.textField.CalcHeight(content, 1f));
                    m_ConversationData.Title = EditorGUILayout.TextField(
                        m_ConversationData.Title,
                        GUI.skin.textField,
                        heightOption,
                        Constants.midWidth
                        );
                }
                GUI.skin.textField.fontSize = 0;
                GUI.skin.textField.fontStyle = FontStyle.Normal;
                return true;
            }
            protected override bool RowWithTitleAndContent()
            {
                content.text = "聊天开始了：";
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

                if (IsChanged())
                {
                    m_IsChanged = false;
                    listMapper[mainIndex] = GetListMain(m_ConversationData.Value);
                    for (int i = 0, count = listMapper[mainIndex].list.Count; i < count; i++)
                        listMapper[i] = GetListMinor(((_WordScriptableObj)listMapper[mainIndex].list[i]).Value);
                }
                listMapper[mainIndex].DoLayoutList();
                return true;
            }
            #endregion
            
            #region -- Private APIMethods --
            private bool IsAnswer(int index) => (index & 1) != 0;
            private bool IsChanged() => m_IsChanged;
            private ReorderableList GetListMain(IList dataList)
                => new ReorderableList(
                    dataList,
                    typeof(_WordScriptableObj),
                    true,
                    false,
                    true,
                    true
                    )
                {
                    onReorderCallbackWithDetails = (list, prevIndex, currentIndex) =>
                    {
                        //Debug.Log = "ListMain has changed。。";
                        m_IsChanged = true;
                        var currentValue = listMapper[currentIndex];
                        var prevValue = listMapper[prevIndex];
                        listMapper.Remove(currentIndex);
                        listMapper.Add(currentIndex, prevValue);
                        listMapper.Remove(prevIndex);
                        listMapper.Add(prevIndex, currentValue);
                    },
                    elementHeightCallback = (index) =>
                    {
                        if (!listMainSizeMapper.ContainsKey(index)) return 180f;
                        return listMainSizeMapper[index].y > 180f ? listMainSizeMapper[index].y : 180f;
                    },
                    drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        currentIndex = index;
                        content.text = string.Empty;
                        content.tooltip = string.Empty;
                        content.image = null;
                        GUI.Box(rect, content);
                        if (/*true*/ !IsAnswer(index))
                        {
                            //_WordScriptableObj speaker = m_ConversationData.Value[index];
                            var speaker = (_WordScriptableObj)listMapper[mainIndex].list[index];
                            Rect group1 = new Rect(
                                new Vector2(rect.x + indentRowColumn, rect.y + indentRowColumn),
                                new Vector2(128f, 128f)
                                );
                            GUI.BeginGroup(group1);
                            speaker.Icon = speaker.Icon ?? (Texture2D)EditorGUIUtility.IconContent("textureChecker", "头像").image;
                            speaker.Icon = (Texture2D)EditorGUI.ObjectField(
                                new Rect(Vector2.zero, new Vector2(128f, 128f)),
                                speaker.Icon,
                                typeof(Texture2D),
                                false
                                );
                            GUI.EndGroup();
                            //====================================================================================================================
                            Rect group2 = new Rect(
                                    new Vector2(rect.x + indentRowColumn + group1.width, rect.y + indentRowColumn),
                                    new Vector2(rect.width - indentRowColumn - group1.width, rect.height - indentRowColumn)
                                    );
                            GUI.BeginGroup(group2);
                            listMapper[index].DoList(new Rect(Vector2.zero, group2.size));
                            GUI.EndGroup();
                        }
                        else
                        {
                            //TODO.. Draw Anwser
                            var speaker = (_WordScriptableObj)listMapper[mainIndex].list[index];
                            Rect group1 = new Rect(
                                new Vector2(rect.width - 98f - indentRowColumn, rect.y + indentRowColumn),
                                new Vector2(128f, 128f)
                                );
                            GUI.BeginGroup(group1);
                            speaker.Icon = speaker.Icon ?? (Texture2D)EditorGUIUtility.IconContent("textureChecker", "头像").image;
                            speaker.Icon = (Texture2D)EditorGUI.ObjectField(
                                new Rect(Vector2.zero, new Vector2(128f, 128f)),
                                speaker.Icon,
                                typeof(Texture2D),
                                false
                                );
                            GUI.EndGroup();
                            //====================================================================================================================
                            Rect group2 = new Rect(
                                    new Vector2(rect.x, rect.y + indentRowColumn),
                                    new Vector2(rect.width - indentRowColumn - group1.width, rect.height - indentRowColumn)
                                    );
                            GUI.BeginGroup(group2);
                            listMapper[index].DoList(new Rect(Vector2.zero, group2.size));
                            GUI.EndGroup();
                        }
                        if (IsPrev) return;
                        listMainSizeMapper[index] = new Vector2(0, listMapper[index].GetHeight() + 8f);
                    },
                    onAddCallback = (list) =>
                    {
                        m_IsChanged = true;
                        ScriptableObject @object = CreateInstance<_WordScriptableObj>();
                        string file = "TestWord001";
                        string filePath = "Assets/_OLiOYouxiCore/LinePlayerEditor/Examples/";
                        string fileAvailable = GetAvailableFile(file, filePath);
                        AssetDatabase.CreateAsset(@object, $"{filePath}{fileAvailable}.asset");
                        AssetDatabase.SaveAssets();
                        list.list.Add(@object);
                        Debug.Log = $"成功创建文件：{fileAvailable}";
                    }
                };
            private ReorderableList GetListMinor(IList dataList)
                => new ReorderableList(
                    dataList,
                    typeof(_SentenceScriptableObj),
                    true,
                    false,
                    true,
                    true
                    )
                {
                    #region -- Else Events --
                    //onReorderCallbackWithDetails = (list, prevIndex, currentIndex) =>
                    //{
                    //    //TODO..当集合发生改变的时候一次触发。。（发生在onChangedCallback之前）
                    //    //Debug.Log = "ListMinor has changed。。";

                    //},
                    //onChangedCallback = (list) =>
                    //{
                    //    //TODO..当集合发生改变的时候一次触发。。（发生在onReorderCallbackWithDetails之后）
                    //    //Debug.Log = "ListMinor is changing。。";
                    //},
                    //onSelectCallback = (list) =>
                    //{
                    //    //TODO..当左键按下的时候一次触发。。
                    //    //Debug.Log = "ListMinor is selecting。。";
                    //},
                    //onMouseDragCallback = (list) =>
                    //{
                    //    //TODO..当左键按住拖拽的时候保持触发。。
                    //    //Debug.Log = "ListMinor is mouseDragging。。";
                    //},
                    //onMouseUpCallback = (list) =>
                    //{
                    //    //TODO..当左键松开的时候一次触发。。（发生在onReorderCallbackWithDetails之后）
                    //    //Debug.Log = "ListMinor is mouseUpping。。";
                    //},
                    #endregion
                    elementHeightCallback = (index) =>
                    {
                        int key = KeyForMinorMain(index, currentIndex);
                        if (!listMinorSizeMapper.ContainsKey(key)) return 44f;
                        return listMinorSizeMapper[key].y > 44f ? listMinorSizeMapper[key].y : 44f;
                    },
                    drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        Rect group1 = new Rect(
                            new Vector2(rect.x + indentRowColumn, rect.y + indentRowColumn),
                            new Vector2(rect.width - indentRowColumn, rect.height - indentRowColumn)
                            );
                        GUI.BeginGroup(group1);
                        //var sentence = m_ConversationData.Value[currentIndex].Value[index];
                        var sentence = (_SentenceScriptableObj)listMapper[currentIndex].list[index];
                        sentence.Key = index;
                        int key = KeyForMinorMain(index, currentIndex);
                        if (!statusMapper.ContainsKey(key)) statusMapper[key] = new Vector2Int(0, index);
                        StringBuilder stringBuilder = null;
                        if (statusMapper[key].x == 0)
                        {
                            stringBuilder = new StringBuilder();
                            for (int i = 0, count = sentence.Value.Count; i < count; i++) stringBuilder.Append(sentence.Value[i].value);
                            GUI.enabled = true;
                        }
                        else if(statusMapper[key].x == 1)
                        {
                            stringBuilder = new StringBuilder(text);
                            GUI.enabled = false;
                        }
                        string editValue = stringBuilder.ToString();
                        content.text = editValue;
                        content.tooltip = string.Empty;
                        content.image = null;
                        GUI.skin.textArea.fontSize = Constants.fontSizeForTextArea;
                        GUI.skin.textArea.wordWrap = true;
                        float width = group1.width - (group1.width * (1f / 3f));
                        float height = GUI.skin.textArea.CalcHeight(content, width);
                        editValue = EditorGUI.TextArea(new Rect(Vector2.zero, new Vector2(width, height)), editValue, GUI.skin.textArea);
                        GUI.skin.textArea.fontSize = 0;
                        GUI.skin.textArea.wordWrap = false;
                        if (editValue != stringBuilder.ToString())
                        {
                            var letters = new List<Letter>();
                            for (int i = 0, length = editValue.Length; i < length; i++) letters.Add(new Letter { value = editValue[i], remain = .04f });
                            sentence.Value = letters;
                        }
                        GUI.enabled = true;

                        GUIContent playBtnContent = EditorGUIUtility.IconContent("PlayButton", "播放");
                        Rect playBtnRect = new Rect(new Vector2(width + indentRowColumn, 0), GUI.skin.button.CalcSize(playBtnContent));
                        GUI.SetNextControlName("playBtn");
                        if (GUI.Button(playBtnRect, playBtnContent))
                        {
                            GUI.FocusControl("playBtn");
                            if (coroutineMapper.ContainsKey(key))
                            {
                                this.StopCoroutine(coroutineMapper[key]);
                                m_TextData.ClearValue();
                            }
                            coroutineMapper[key] = this.StartCoroutine(PlaySentence(sentence, key));
                            statusMapper[key] = new Vector2Int(1, index);
                        }

                        GUIContent pauseContent = EditorGUIUtility.IconContent("PauseButton", "暂停");
                        Rect pauseBtnRect = new Rect(new Vector2(width + playBtnRect.width + indentRowColumn, 0), playBtnRect.size);
                        if(GUI.Button(pauseBtnRect, pauseContent)) Debug.LogWarn = "尴尬，介个按钮还没搞出来。。";

                        GUIContent stopContent = EditorGUIUtility.IconContent("CN Box", "停止");
                        Rect stopBtnRect = new Rect(new Vector2(width + playBtnRect.width + pauseBtnRect.width + indentRowColumn, 0), playBtnRect.size);
                        GUI.SetNextControlName("stopBtn");
                        if (GUI.Button(stopBtnRect, stopContent))
                        {
                            GUI.FocusControl("playBtn");
                            if (!coroutineMapper.ContainsKey(key)) return;
                            this.StopCoroutine(coroutineMapper[key]);
                            statusMapper[key] = new Vector2Int(0, index);
                            m_TextData.ClearValue();
                        }

                        GUIContent editContent = EditorGUIUtility.IconContent("Preset.Context", "编辑");
                        Rect editBtnRect = new Rect(
                            new Vector2(width + playBtnRect.width + pauseBtnRect.width + stopBtnRect.width + indentRowColumn, 0),
                            playBtnRect.size
                            );
                        if(GUI.Button(editBtnRect, editContent))
                        {
                            LinePlayerEditorWindowController.SentenceData = sentence;
                            LinePlayerEditorWindowController.ChatboxData = m_ChatboxData;
                            LinePlayerEditorWindowController.Open003();
                        }

                        //GUI.skin.button.fontSize = Constants.fontSizeForButton;
                        //GUI.skin.button.fontStyle = FontStyle.Bold;
                        //content.text = "0";
                        //content.tooltip = string.Empty;
                        //content.image = null;
                        //Rect sequenceBtnRect = new Rect(new Vector2(width + playBtnRect.width * 2 + pauseBtnRect.width + stopBtnRect.width + indentRowColumn, 0), playBtnRect.size);
                        //if (GUI.Button(sequenceBtnRect, content)) Debug.LogWarn = "尴尬，介个按钮还没搞出来。。";
                        //GUI.skin.button.fontSize = 0;
                        //GUI.skin.button.fontStyle = FontStyle.Normal;

                        GUI.EndGroup();

                        if (IsPrev) return;
                        listMinorSizeMapper[KeyForMinorMain(index, currentIndex)] = new Vector2(width, height + 8f);
                    },
                    onAddCallback = (list) =>
                    {
                        m_IsChanged = true;
                        ScriptableObject @object = CreateInstance<_SentenceScriptableObj>();
                        string file = "TestSentence001";
                        string filePath = "Assets/_OLiOYouxiCore/LinePlayerEditor/Examples/";
                        string fileAvailable = GetAvailableFile(file, filePath);
                        AssetDatabase.CreateAsset(@object, $"{filePath}{fileAvailable}.asset");
                        AssetDatabase.SaveAssets();
                        list.list.Add(@object);
                        Debug.Log = $"成功创建文件：{fileAvailable}";
                    }
                };
            private string GetAvailableFile(string file, string filePath)
            {
                if (string.IsNullOrEmpty(file)) return string.Empty;
                if (!RuntimeUtility.FileExist($"{filePath}{file}.asset")) return file;
                string numberString = file.Substring(file.Length - 3, 3);
                int number = int.Parse(numberString) + 1;
                string filePattern = file.Substring(0, file.Length - 3);
                string numberPattern = number > 999 ? 
                    throw new Exception("太多啦！") : number > 99 ? 
                    number.ToString() : number > 9 ? 
                    $"0{number.ToString()}" : $"00{number.ToString()}";
                return GetAvailableFile($"{filePattern}{numberPattern}", filePath);
            }
            private int KeyForMinorMain(int minorIndex, int mainIndex) => mainIndex * 1000 + minorIndex;
            private IEnumerator PlaySentence(Object @object, int key)
            {
                var sentence = (_SentenceScriptableObj)@object;
                for (int i = 0, length = sentence.Value.Count; i < length; i++)
                {
                    Letter letter = sentence.Value[i];
                    WaitForSeconds seconds = waitForSecondsMapper.ContainsKey(letter.remain) ?
                        waitForSecondsMapper[letter.remain] :
                        waitForSecondsMapper[letter.remain] = new WaitForSeconds(letter.remain);
                    yield return seconds;
                    m_TextData.AppendValue(ref letter.value);
                }
                statusMapper[key] = Vector2Int.zero;
                m_TextData.ClearValue();
            }
            #endregion
        }

        #endregion
    }
}

//if (!listMapper.ContainsKey(-10))
//{
//    listMapper.Add(-10, null);
//    ReorderableList listMain = listMapper[-10] ?? (listMapper[-10] = new ReorderableList(m_ConversationData.Value, typeof(_WordScriptableObj), true, false, true, true)
//    {
//        onReorderCallbackWithDetails = (list, prevIndex, currentIndex) =>
//        {
//            var currentValue = listMapper[currentIndex];
//            var prevValue = listMapper[prevIndex];
//            listMapper.Remove(currentIndex);
//            listMapper.Add(currentIndex, prevValue);
//            listMapper.Remove(prevIndex);
//            listMapper.Add(prevIndex, currentValue);
//            var value = currentValue.list;
//            currentValue.list = prevValue.list;
//            prevValue.list = value;
//            //var current = list.list[currentIndex];
//            //var prev = list.list[prevIndex];
//            //list.list.Insert(currentIndex, prev);
//            //list.list.RemoveAt(currentIndex + 1);
//            //list.list.Insert(prevIndex, current);
//            //list.list.RemoveAt(prevIndex + 1);
//        },
//        elementHeightCallback = (index) =>
//        {
//            if (!listMainSizeMapper.ContainsKey(index)) return 180f;
//            return listMainSizeMapper[index].y > 180f ? listMainSizeMapper[index].y : 180f;
//        },
//        drawElementCallback = (rect, index, isActive, isFocused) =>
//        {
//            content.text = string.Empty;
//            content.tooltip = string.Empty;
//            content.image = null;
//            GUI.Box(rect, content);
//            if (true /*!IsAnswer(index)*/)
//            {
//                //_WordScriptableObj speaker = m_ConversationData.Value[index];
//                var speaker = (_WordScriptableObj)listMapper[-10].list[index];
//                Rect group1 = new Rect(new Vector2(rect.x + indentRowColumn, rect.y + indentRowColumn), new Vector2(128f, 128f));
//                GUI.BeginGroup(group1);
//                speaker.Icon = speaker.Icon ?? (Texture2D)EditorGUIUtility.IconContent("textureChecker", "头像").image;
//                speaker.Icon = (Texture2D)EditorGUI.ObjectField(
//                    new Rect(Vector2.zero, new Vector2(128f, 128f)),
//                    speaker.Icon,
//                    typeof(Texture2D),
//                    false
//                    );
//                GUI.EndGroup();

//                Rect group2 = new Rect(new Vector2(rect.x + indentRowColumn + group1.width, rect.y + indentRowColumn), new Vector2(rect.width - indentRowColumn - group1.width, rect.height - indentRowColumn));
//                GUI.BeginGroup(group2);
//                if (!listMapper.ContainsKey(index))
//                {
//                    listMapper.Add(index, null);
//                    ReorderableList listMinor = listMapper[index] ?? (listMapper[index] = new ReorderableList(m_ConversationData.Value[index].Value, typeof(_SentenceScriptableObj), true, false, true, true)
//                    {
//                        elementHeightCallback = (_index) =>
//                        {
//                            if (!listMinorSizeMapper.ContainsKey(_index)) return 44f;
//                            return listMinorSizeMapper[_index].y;
//                        },
//                        drawElementCallback = (_rect, _index, _isActive, _isFocused) =>
//                        {
//                            if (_index >= listMapper[index].list.Count)
//                            {
//                                Debug.Log = $"_i:{_index}count:{ listMapper[index].list.Count}";
//                                return;
//                            }
//                            Rect _group1 = new Rect(new Vector2(_rect.x + indentRowColumn, _rect.y + indentRowColumn), new Vector2(_rect.width - indentRowColumn, _rect.height - indentRowColumn));
//                            GUI.BeginGroup(_group1);
//                            var sentence = (_SentenceScriptableObj)listMapper[index].list[_index];
//                            //var sentence = m_ConversationData.Value[index].Value[_index];
//                            var value = new StringBuilder();
//                            for (int i = 0, count = sentence.Value.Count; i < count; i++) value.Append(sentence.Value[i].value);
//                            string editValue = value.ToString();
//                            content.text = editValue;
//                            content.tooltip = string.Empty;
//                            content.image = null;
//                            GUI.skin.textArea.fontSize = Constants.fontSizeForTextArea;
//                            GUI.skin.textArea.wordWrap = true;
//                            float width = _group1.width - (_group1.width * (1f / 3f));
//                            float height = GUI.skin.textArea.CalcHeight(content, width);
//                            if (!IsPrev) listMinorSizeMapper[_index] = new Vector2(width, height + 8f);
//                            editValue = EditorGUI.TextArea(new Rect(Vector2.zero, new Vector2(width, height)), editValue, GUI.skin.textArea);
//                            GUI.skin.textArea.fontSize = 0;
//                            GUI.skin.textArea.wordWrap = false;
//                            if (editValue != value.ToString())
//                            {
//                                var letters = new List<Letter>();
//                                for (int i = 0, length = editValue.Length; i < length; i++) letters.Add(new Letter { value = editValue[i], remain = 0 });
//                                sentence.Value = letters;
//                            }

//                            GUIContent playBtnContent = EditorGUIUtility.IconContent("PlayButton", "播放");
//                            Rect playBtnRect = new Rect(new Vector2(width + indentRowColumn, 0), GUI.skin.button.CalcSize(playBtnContent));
//                            GUI.Button(playBtnRect, playBtnContent);

//                            GUIContent pauseContent = EditorGUIUtility.IconContent("PauseButton", "暂停");
//                            Rect pauseBtnRect = new Rect(new Vector2(width + playBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                            GUI.Button(pauseBtnRect, pauseContent);

//                            GUIContent stopContent = EditorGUIUtility.IconContent("CN Box", "停止");
//                            Rect stopBtnRect = new Rect(new Vector2(width + playBtnRect.width + pauseBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                            GUI.Button(stopBtnRect, stopContent);

//                            GUIContent editContent = EditorGUIUtility.IconContent("Preset.Context", "编辑");
//                            Rect editBtnRect = new Rect(new Vector2(width + playBtnRect.width + pauseBtnRect.width + stopBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                            GUI.Button(editBtnRect, editContent);

//                            GUI.EndGroup();
//                        },
//                        onAddCallback = (list) =>
//                        {
//                            ScriptableObject @object = CreateInstance<_SentenceScriptableObj>();
//                            string file = "TestSentence001";
//                            string filePath = "Assets/_OLiOYouxiCore/LinePlayerEditor/Examples/";
//                            string fileAvailable = GetAvailableFile(file, filePath);
//                            AssetDatabase.CreateAsset(@object, $"{filePath}{fileAvailable}.asset");
//                            AssetDatabase.SaveAssets();
//                            list.list.Add(@object);
//                            Debug.Log = $"成功创建文件：{fileAvailable}";
//                        }
//                    });
//                }
//                if (!IsPrev) listMainSizeMapper[index] = new Vector2(0, listMapper[index].GetHeight() + 8f);
//                listMapper[index].DoList(new Rect(Vector2.zero, group2.size));
//                GUI.EndGroup();
//            }
//            else
//            {
//                //_WordScriptableObj speaker = m_ConversationData.Value[index];
//                //Rect group1 = new Rect(new Vector2(rect.width - 128f -indentRowColumn, rect.height - indentRowColumn), new Vector2(128f, 128f));
//                //GUI.BeginGroup(group1);
//                //speaker.Icon = speaker.Icon ?? (Texture2D)EditorGUIUtility.IconContent("textureChecker", "头像").image;
//                //speaker.Icon = (Texture2D)EditorGUI.ObjectField(
//                //    new Rect(Vector2.zero, new Vector2(128f, 128f)),
//                //    speaker.Icon,
//                //    typeof(Texture2D),
//                //    false
//                //    );
//                //GUI.EndGroup();

//                //Rect group2 = new Rect(new Vector2(rect.width - indentRowColumn - group1.width, rect.height - indentRowColumn), new Vector2(rect.width - indentRowColumn - group1.width, rect.height - indentRowColumn));
//                //GUI.BeginGroup(group2);
//                //if (!listMapper.ContainsKey(index))
//                //{
//                //    listMapper.Add(index, null);
//                //    ReorderableList listMinor = listMapper[index] ?? (listMapper[index] = new ReorderableList(m_ConversationData.Value[index].Value, typeof(_WordScriptableObj), true, false, true, true)
//                //    {
//                //        elementHeightCallback = (_index) =>
//                //        {
//                //            if (!listInsiderSizeMapper.ContainsKey(_index)) return 44f;
//                //            return listInsiderSizeMapper[_index].y;
//                //        },
//                //        drawElementCallback = (_rect, _index, _isActive, _isFocused) =>
//                //        {
//                //            Rect _group1 = new Rect(new Vector2(_rect.x + indentRowColumn, _rect.y + indentRowColumn), new Vector2(_rect.width - indentRowColumn, _rect.height - indentRowColumn));
//                //            GUI.BeginGroup(_group1);

//                //            var sentence = m_ConversationData.Value[index].Value[_index];
//                //            var value = new StringBuilder();
//                //            for (int i = 0, count = sentence.Value.Count; i < count; i++) value.Append(sentence.Value[i].value);
//                //            string editValue = value.ToString();
//                //            content.text = editValue;
//                //            content.tooltip = string.Empty;
//                //            content.image = null;
//                //            GUI.skin.textArea.fontSize = Constants.fontSizeForTextArea;
//                //            GUI.skin.textArea.wordWrap = true;
//                //            float width = _group1.width - (_group1.width * (1f / 3f));
//                //            float height = GUI.skin.textArea.CalcHeight(content, width);
//                //            if (!IsPrev) listInsiderSizeMapper[_index] = new Vector2(width, height + 8f);
//                //            editValue = EditorGUI.TextArea(new Rect(Vector2.zero, new Vector2(width, height)), editValue, GUI.skin.textArea);
//                //            GUI.skin.textArea.fontSize = 0;
//                //            GUI.skin.textArea.wordWrap = false;
//                //            if (editValue != value.ToString())
//                //            {
//                //                var letters = new List<Letter>();
//                //                for (int i = 0, length = editValue.Length; i < length; i++) letters.Add(new Letter { value = editValue[i], remain = 0 });
//                //                sentence.Value = letters;
//                //            }

//                //            GUIContent playBtnContent = EditorGUIUtility.IconContent("PlayButton", "播放");
//                //            Rect playBtnRect = new Rect(new Vector2(width + indentRowColumn, 0), GUI.skin.button.CalcSize(playBtnContent));
//                //            GUI.Button(playBtnRect, playBtnContent);

//                //            GUIContent pauseContent = EditorGUIUtility.IconContent("PauseButton", "暂停");
//                //            Rect pauseBtnRect = new Rect(new Vector2(width + playBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                //            GUI.Button(pauseBtnRect, pauseContent);

//                //            GUIContent stopContent = EditorGUIUtility.IconContent("CN Box", "停止");
//                //            Rect stopBtnRect = new Rect(new Vector2(width + playBtnRect.width + pauseBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                //            GUI.Button(stopBtnRect, stopContent);

//                //            GUIContent editContent = EditorGUIUtility.IconContent("Preset.Context", "编辑");
//                //            Rect editBtnRect = new Rect(new Vector2(width + playBtnRect.width + pauseBtnRect.width + stopBtnRect.width + indentRowColumn, 0), playBtnRect.size);
//                //            GUI.Button(editBtnRect, editContent);

//                //            GUI.EndGroup();
//                //        },
//                //        onAddCallback = (list) =>
//                //        {
//                //            ScriptableObject @object = CreateInstance<_SentenceScriptableObj>();
//                //            string file = "TestSentence001";
//                //            string filePath = "Assets/_OLiOYouxiCore/LinePlayerEditor/Examples/";
//                //            string fileAvailable = GetAvailableFile(file, filePath);
//                //            AssetDatabase.CreateAsset(@object, $"{filePath}{fileAvailable}.asset");
//                //            AssetDatabase.SaveAssets();
//                //            list.list.Add(@object);
//                //            Debug.Log = $"成功创建文件：{fileAvailable}";
//                //        }
//                //    });
//                //}
//                //if (!IsPrev) listOuterSizeMapper[index] = new Vector2(0, listMapper[index].GetHeight() + 8f);
//                //listMapper[index].DoList(new Rect(Vector2.zero, group2.size));
//                //GUI.EndGroup();
//            }
//        },
//        onAddCallback = (list) =>
//        {
//            ScriptableObject @object = CreateInstance<_WordScriptableObj>();
//            string file = "TestWord001";
//            string filePath = "Assets/_OLiOYouxiCore/LinePlayerEditor/Examples/";
//            string fileAvailable = GetAvailableFile(file, filePath);
//            AssetDatabase.CreateAsset(@object, $"{filePath}{fileAvailable}.asset");
//            AssetDatabase.SaveAssets();
//            list.list.Add(@object);
//            Debug.Log = $"成功创建文件：{fileAvailable}";
//        }
//    });
//}
//listMapper[-10].DoLayoutList();
