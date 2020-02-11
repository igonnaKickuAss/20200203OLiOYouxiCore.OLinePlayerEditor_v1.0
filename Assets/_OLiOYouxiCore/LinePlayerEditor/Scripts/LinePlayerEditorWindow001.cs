using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace OLiOYouxiCore.OLinePlayer
{
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using OLiOYouxi.OSystem;
    using static _ChatboxScriptableObj;
    internal class LinePlayerEditorWindow001 : OEditorWindow
    {
        #region -- Private Data --
        private EditorCustomer chatboxEditorCustomer = null;
        private Editor chatboxEditorDefault = null;
        private _ChatboxScriptableObj m_ChatboxData = null;
        #endregion

        #region -- MONO APIMethods --
        private void OnEnable()
        {
            //EditorApplication.Beep();
        }
        private void OnDisable()
        {
            //EditorApplication.Beep();
            if (m_ChatboxData != null) EditorUtility.SetDirty(m_ChatboxData);
            chatboxEditorDefault = null;
            chatboxEditorCustomer = null;
            m_ChatboxData = null;
        }
        #endregion

        #region -- Override APIMethods --
        protected override bool Row1()
        {
            EditorGUILayout.LabelField(Constants.copyRight);
            return true;
        }
        protected override bool Row2()
        {
            EditorGUILayout.LabelField(Constants.requires);
            return true;
        }
        protected override bool Row3()
        {
            EditorGUILayout.LabelField(Constants.version);
            return true;
        }
        protected override bool Row4()
        {
            m_ChatboxData = (_ChatboxScriptableObj)EditorGUILayout.ObjectField(
                m_ChatboxData,
                typeof(_ChatboxScriptableObj),
                false,
                Constants.maxWidth
                );
            if (m_ChatboxData != null)
            {
                //handle
                m_ChatboxData.Value.RemoveAll((f) => f == null);
                for (int i = 0, count = m_ChatboxData.Value.Count; i < count; i++)
                    m_ChatboxData.Value[i].Value.RemoveAll((f) => f == null);
            }
            return true;
        }
        protected override bool Row5() => DisplayCustomer(m_ChatboxData);
        #endregion

        #region -- Private APIMethods --
        private bool DisplayDefault(Object @object)
        {
            if (@object == null) return false;
            chatboxEditorDefault = chatboxEditorDefault ?? (chatboxEditorDefault = Editor.CreateEditor(@object));
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                GUILayout.Space(4f);
                chatboxEditorDefault.OnInspectorGUI();
                return true;
            }
        }
        private bool DisplayCustomer(Object @object)
        {
            if (@object == null) return false;
            chatboxEditorCustomer = chatboxEditorCustomer ??
                (chatboxEditorCustomer = EditorCustomer.CreateEditor(
                    new ChatboxEditor001 {
                        Obj = (_ChatboxScriptableObj)@object
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
        [CustomEditor(typeof(ChatboxEditor001), false), CanEditMultipleObjects]
        private class EditorCustomer : OEditor
        {
            #region -- Private Data --
            private _ChatboxScriptableObj m_Target = null;
            private SerializedObject m_Object = null;
            private Dictionary<int, Vector2> listMainSizeMapper = null;
            private bool m_IsChanged = false;

            private ReorderableList listMain = null;
            #endregion

            #region -- MONO APIMethods --
            private void OnEnable()
            {
                m_Target = ((ChatboxEditor001)base.target).Obj;
                m_Object = CreateEditor(m_Target).serializedObject;
                listMainSizeMapper = new Dictionary<int, Vector2>();
                m_IsChanged = true;
            }
            private void OnDisable()
            {
                m_Target = null;
                m_Object = null;
                listMain = null;
                listMainSizeMapper = null;
            }
            #endregion

            #region -- Override APIMethods --
            protected override bool ColumnWithTitleAndContent()
            {
                content.text = "场景：";
                content.tooltip = "";
                content.image = null;
                GUI.skin.label.fontSize = Constants.fontSizeForLabel;
                GUI.skin.label.fontStyle = FontStyle.Bold;
                EditorGUILayout.SelectableLabel(content.text, GUI.skin.label, GUILayout.Width(GUI.skin.label.CalcSize(content).x));
                GUI.skin.label.fontSize = 0;
                GUI.skin.label.fontStyle = FontStyle.Normal;

                content.text = m_Target.Title;
                content.tooltip = string.Empty;
                content.image = null;
                GUI.skin.textField.fontSize = Constants.fontSizeForTextField;
                GUI.skin.textField.fontStyle = FontStyle.Normal;
                if (m_Target.Title.Length > 6)
                {
                    GUI.skin.textField.CalcMinMaxWidth(content, out float minWidth, out float maxWidth);
                    GUILayoutOption heightOption = GUILayout.Height(GUI.skin.textField.CalcHeight(content, 1f));
                    GUILayoutOption minWidthOption = GUILayout.MinWidth(minWidth);
                    GUILayoutOption maxWidthOption = GUILayout.MaxWidth(maxWidth + 8f);
                    m_Target.Title = EditorGUILayout.TextField(
                        m_Target.Title,
                        GUI.skin.textField,
                        heightOption,
                        minWidthOption,
                        maxWidthOption
                        );
                }
                else
                {
                    GUILayoutOption heightOption = GUILayout.Height(GUI.skin.textField.CalcHeight(content, 1f));
                    m_Target.Title = EditorGUILayout.TextField(
                        m_Target.Title,
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
                content.text = "下面会有好多说话：";
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
                    listMain = GetListMain(m_Target.Value);
                }
                listMain.DoLayoutList();
                return true;
            }
            #endregion

            #region -- Private APIMethods --
            private bool IsChanged() => m_IsChanged;
            private ReorderableList GetListMain(IList dataList)
                => new ReorderableList(
                    dataList,
                    typeof(Conversation),
                    true,
                    false,
                    true,
                    true
                    )
                {
                    elementHeightCallback = (index) =>
                    {
                        if (!listMainSizeMapper.ContainsKey(index)) return 44f;
                        return listMainSizeMapper[index].y > 44f ? listMainSizeMapper[index].y : 44f;
                    },
                    drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        var conversation = (Conversation)listMain.list[index];

                        Rect group1 = new Rect(
                            new Vector2(rect.x + indentRowColumn, rect.y + indentRowColumn),
                            new Vector2(rect.width - indentRowColumn, rect.height - indentRowColumn)
                            );
                        GUI.BeginGroup(group1);
                        GUI.skin.label.fontSize = Constants.fontSizeForLabel;
                        GUI.skin.label.fontStyle = FontStyle.Bold;
                        content.text = "标题:";
                        content.tooltip = "";
                        content.image = null;
                        float lableWidth1 = group1.width - (group1.width * (9f / 10f));
                        float lableHeight1 = GUI.skin.textArea.CalcHeight(content, lableWidth1);
                        EditorGUI.SelectableLabel(new Rect(Vector2.zero, new Vector2(lableWidth1, lableHeight1 + indentContainer)), content.text, GUI.skin.label);
                        GUI.skin.label.fontSize = 0;
                        GUI.skin.label.fontStyle = FontStyle.Normal;

                        GUI.skin.textArea.fontSize = Constants.fontSizeForTextArea;
                        GUI.skin.textArea.wordWrap = true;
                        content.text = conversation.Title;
                        content.tooltip = "";
                        content.image = null;
                        float width1 = group1.width - (group1.width * (1f / 3f));
                        float height1 = GUI.skin.textArea.CalcHeight(content, width1);
                        conversation.Title = EditorGUI.TextArea(new Rect(new Vector2(lableWidth1, 0), new Vector2(width1, height1)), conversation.Title, GUI.skin.textArea);
                        GUI.skin.textArea.fontSize = 0;
                        GUI.skin.textArea.wordWrap = false;

                        GUI.skin.label.fontSize = Constants.fontSizeForLabel;
                        GUI.skin.label.fontStyle = FontStyle.Bold;
                        content.text = "操作:";
                        content.tooltip = "";
                        content.image = null;
                        float lableWidth2 = group1.width - (group1.width * (9f / 10f));
                        float lableHeight2 = GUI.skin.textArea.CalcHeight(content, lableWidth2);
                        EditorGUI.SelectableLabel(new Rect(new Vector2(0, height1), new Vector2(lableWidth2, lableHeight2 + indentContainer)), content.text, GUI.skin.label);
                        GUI.skin.label.fontSize = 0;
                        GUI.skin.label.fontStyle = FontStyle.Normal;

                        GUI.skin.button.fontSize = Constants.fontSizeForButton;
                        content.text = "▶播放";
                        content.tooltip = "";
                        content.image = null;
                        float width2 = GUI.skin.button.CalcSize(content).x;
                        float height2 = GUI.skin.button.CalcHeight(content, width2);
                        if (GUI.Button(new Rect(new Vector2(lableWidth2, height1), new Vector2(width2, height2)), content, GUI.skin.button)) Debug.LogWarn = "尴尬，介个按钮还没搞出来。。";
                        GUI.skin.button.fontSize = 0;

                        GUI.skin.button.fontSize = Constants.fontSizeForButton;
                        content.text = "✔编辑";
                        content.tooltip = "";
                        content.image = null;
                        float width3 = GUI.skin.button.CalcSize(content).x;
                        float height3 = GUI.skin.textField.CalcHeight(content, width3);
                        if (GUI.Button(new Rect(new Vector2(lableWidth2 + width2 + indentRowColumn, height1), new Vector2(width3, height2)), content, GUI.skin.button))
                        {
                            LinePlayerEditorWindowController.ChatboxData = m_Target;
                            LinePlayerEditorWindowController.ConversationData = conversation;
                            LinePlayerEditorWindowController.Open002();
                        }
                        GUI.skin.button.fontSize = 0;
                        GUI.EndGroup();
                        if (!IsPrev) return;
                        listMainSizeMapper[index] = new Vector2(width1, height1 + lableHeight2 * 2 + 8f);
                    }
                };

            #endregion
        }

        #endregion
    }
}

//public override void OnInspectorGUI()
//{

//    using (var toScroll = new EditorGUILayout.ScrollViewScope(scrollPos))
//    {
//        scrollPos = toScroll.scrollPosition;
//        using (var toContainer = new EditorGUILayout.HorizontalScope(GUI.skin.box))
//        {
//            GUILayout.Space(indentContainer);
//            using (var _toContainer = new EditorGUILayout.VerticalScope())
//            {
//                GUILayout.Space(indentContainer);
//                using (var toRow = new EditorGUILayout.VerticalScope())
//                {
//                    using (var toColumn = new EditorGUILayout.HorizontalScope())
//                    {

//                    }
//                    using (var toColumn = new EditorGUILayout.HorizontalScope())
//                    {
//                        content.text = "交谈:";
//                        content.tooltip = "";
//                        content.image = null;
//                        GUI.skin.label.fontSize = Constants.fontSizeForLabel;
//                        GUI.skin.label.fontStyle = FontStyle.Bold;
//                        EditorGUILayout.SelectableLabel(content.text, GUI.skin.label, GUILayout.Width(GUI.skin.label.CalcSize(content).x));
//                        GUI.skin.label.fontSize = 0;
//                        GUI.skin.label.fontStyle = FontStyle.Normal;

//                        using (var toDown = new EditorGUILayout.VerticalScope())
//                        {
//                            listMain = listMain ?? new ReorderableList(m_Target.Value, typeof(Conversation), true, false, true, true)
//                            {
//                                elementHeightCallback = (index) =>
//                                {
//                                    var conversation = (Conversation)(listMain.list[index]);
//                                    content.text = conversation.Title;
//                                    content.tooltip = "";
//                                    content.image = null;
//                                    GUI.skin.textField.fontSize = Constants.fontSizeForTextField;
//                                    GUI.skin.textField.fontStyle = FontStyle.Normal;
//                                    float height = GUI.skin.textField.CalcHeight(content, 1f) * 2 + 4f;
//                                    GUI.skin.textField.fontSize = 0;
//                                    GUI.skin.textField.fontStyle = FontStyle.Normal;
//                                    return height;
//                                },
//                                drawElementCallback = (rect, index, isActive, isFocused) =>
//                                {
//                                    var conversation = (Conversation)(listMain.list[index]);

//                                    content.text = "标题:";
//                                    content.tooltip = "";
//                                    content.image = null;
//                                    GUI.skin.label.fontSize = Constants.fontSizeForLabel;
//                                    GUI.skin.label.fontStyle = FontStyle.Bold;
//                                    float width0 = GUI.skin.label.CalcSize(content).x;
//                                    float height0 = GUI.skin.label.CalcSize(content).y;
//                                    EditorGUI.SelectableLabel(new Rect(rect.x, rect.y, width0, height0), content.text, GUI.skin.label);
//                                    GUI.skin.label.fontSize = 0;
//                                    GUI.skin.label.fontStyle = FontStyle.Normal;

//                                    content.text = conversation.Title;
//                                    content.tooltip = "谈话标题。。";
//                                    content.image = null;
//                                    GUI.skin.textField.fontSize = Constants.fontSizeForTextField;
//                                    GUI.skin.textField.fontStyle = FontStyle.Normal;
//                                    float height1 = GUI.skin.textField.CalcHeight(content, 1f);
//                                    if (conversation.Title.Length > 6)
//                                    {
//                                        GUI.skin.textField.CalcMinMaxWidth(content, out float minWidth, out float maxWidth);
//                                        conversation.Title = EditorGUI.TextField(new Rect(rect.x + width0, rect.y, maxWidth + 8f, height1), conversation.Title, GUI.skin.textField);
//                                    }
//                                    else conversation.Title = EditorGUI.TextField(
//                                        new Rect(rect.x + width0, rect.y, 150f, height1),
//                                        conversation.Title,
//                                        GUI.skin.textField
//                                        );
//                                    GUI.skin.textField.fontSize = 0;
//                                    GUI.skin.textField.fontStyle = FontStyle.Normal;

//                                    content.text = "操作:";
//                                    content.tooltip = "";
//                                    content.image = null;
//                                    GUI.skin.label.fontSize = Constants.fontSizeForLabel;
//                                    GUI.skin.label.fontStyle = FontStyle.Bold;
//                                    float width00 = GUI.skin.label.CalcSize(content).x;
//                                    float height00 = GUI.skin.label.CalcSize(content).y;
//                                    EditorGUI.SelectableLabel(new Rect(rect.x, rect.y + height1, width00, height00), content.text, GUI.skin.label);
//                                    GUI.skin.label.fontSize = 0;
//                                    GUI.skin.label.fontStyle = FontStyle.Normal;

//                                    GUI.skin.button.fontSize = Constants.fontSizeForButton;
//                                    content.text = "▶播放";
//                                    content.tooltip = "";
//                                    content.image = null;
//                                    float height2 = GUI.skin.textField.CalcHeight(content, 1f);
//                                    float width2 = GUI.skin.button.CalcSize(content).x;
//                                    GUI.Button(new Rect(rect.x + width0, rect.y + height1 + 3f, width2, height2), content, GUI.skin.button);
//                                    content.text = "✔编辑";
//                                    content.tooltip = "";
//                                    content.image = null;
//                                    float height3 = GUI.skin.textField.CalcHeight(content, 1f);
//                                    float width3 = GUI.skin.button.CalcSize(content).x;
//                                    if (GUI.Button(new Rect(rect.x + width0 + width2, rect.y + height1 + 3f, width3, height3), content, GUI.skin.button))
//                                    {
//                                        LinePlayerEditorWindowController.ChatboxData = m_Target;
//                                        LinePlayerEditorWindowController.ConversationData = conversation;
//                                        LinePlayerEditorWindowController.Open002();
//                                    }
//                                    GUI.skin.button.fontSize = 0;
//                                }
//                            };
//                            listMain.DoLayoutList();
//                        }
//                    }
//                }
//            }
//        }
//    }

//}