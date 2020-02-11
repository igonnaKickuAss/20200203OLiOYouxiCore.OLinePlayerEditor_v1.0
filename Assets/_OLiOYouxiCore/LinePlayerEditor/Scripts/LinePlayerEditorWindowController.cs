using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using static _ChatboxScriptableObj;
    internal class LinePlayerEditorWindowController
    {
        #region -- 单例 --
        static private LinePlayerEditorWindowController instance = null;
        private LinePlayerEditorWindowController() { }
        #endregion

        #region -- Private Data --
        private _ChatboxScriptableObj m_ChatboxData = null;
        private _SentenceScriptableObj m_SentenceData = null;
        private Conversation m_ConversationData = null;

        #endregion

        #region -- Static ShotC --
        static public _ChatboxScriptableObj ChatboxData
        {
            get => (instance ?? (instance = new LinePlayerEditorWindowController())).m_ChatboxData;
            internal set => (instance ?? (instance = new LinePlayerEditorWindowController())).m_ChatboxData = value;
        }
        static public _SentenceScriptableObj SentenceData
        {
            get => (instance ?? (instance = new LinePlayerEditorWindowController())).m_SentenceData;
            internal set => (instance ?? (instance = new LinePlayerEditorWindowController())).m_SentenceData = value;
        }
        static public Conversation ConversationData
        {
            get => (instance ?? (instance = new LinePlayerEditorWindowController())).m_ConversationData;
            internal set => (instance ?? (instance = new LinePlayerEditorWindowController())).m_ConversationData = value;
        }
        #endregion

        [MenuItem("OLiOYouxiOToolkits/奥利奥台词编辑器")]
        static public void Open001()
        {
            LinePlayerEditorWindow001 window = EditorWindow.GetWindowWithRect<LinePlayerEditorWindow001>(GetWindowRect(), true, "奥利奥台词编辑器", true);
            window.Show(false);
        }

        [MenuItem("OLiOYouxiOToolkits/奥利奥对话编辑器")]
        static public void Open002()
        {
            if(instance == null || instance.m_ChatboxData ==null || instance.m_ConversationData == null)
            {
                Debug.LogErr = "不要从这里打开，你得通过台词编辑器去编辑指定的对话。。";
                return;
            }
            LinePlayerEditorWindow002 window = (LinePlayerEditorWindow002)EditorWindow.GetWindow(typeof(LinePlayerEditorWindow002), true, "奥利奥对话编辑器");
            window.chatboxData = instance.m_ChatboxData;
            window.conversationData = instance.m_ConversationData;
            window.ShowAuxWindow();
            instance.m_ChatboxData = null;
            instance.m_ConversationData = null;
        }

        [MenuItem("OLiOYouxiOToolkits/奥利奥句子编辑器")]
        static public void Open003()
        {
            if (instance == null || instance.m_SentenceData == null || instance.m_ChatboxData == null)
            {
                Debug.LogErr = "不要从这里打开，你得通过对话编辑器去编辑指定的句子。。";
                return;
            }
            LinePlayerEditorWindow003 window = (LinePlayerEditorWindow003)EditorWindow.GetWindow(typeof(LinePlayerEditorWindow003), true, "奥利奥句子编辑器");
            window.sentenceData = instance.m_SentenceData;
            window.chatboxData = instance.m_ChatboxData;
            window.ShowAuxWindow();
            instance.m_SentenceData = null;
            instance.m_ChatboxData = null;
        }

        [MenuItem("OLiOYouxiOToolkits/测试矩阵")]
        static public void OpenTest001()
        {
            LinePlayerEditorWindowTest001 window = EditorWindow.GetWindowWithRect<LinePlayerEditorWindowTest001>(GetWindowRect(), true, "测试矩阵", true);
            window.Show(false);
        }

        [MenuItem("OLiOYouxiOToolkits/测试协程")]
        static public void OpenTest002()
        {
            LinePlayerEditorWindowTest002 window = EditorWindow.GetWindowWithRect<LinePlayerEditorWindowTest002>(GetWindowRect(), true, "测试协程", true);
            window.Show(false);
        }

        [MenuItem("OLiOYouxiOToolkits/测试曲线")]
        static public void OpenTest003()
        {
            LinePlayerEditorWindowTest003 window = EditorWindow.GetWindowWithRect<LinePlayerEditorWindowTest003>(GetWindowRect(), true, "测试曲线", true);
            window.Show(false);
        }

        /// <summary>
        /// 返回摄像机分辨率尺寸
        /// </summary>
        /// <returns></returns>
        static private Rect GetWindowRect()
        {
            System.Type t = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo sizeOfMainGameView = t.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object res = sizeOfMainGameView.Invoke(null, null);
            UnityEngine.Vector2 size = (Vector2)res;
            return new Rect(0, 0, size.x, size.y);
        }

    }
}

