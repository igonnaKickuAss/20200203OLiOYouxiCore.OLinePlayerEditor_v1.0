using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    using Core = List<_ChatboxScriptableObj.Conversation>;
    using ReorderableList = OAttributes.ReorderableListAttribute;
    using Object = UnityEngine.Object;
    using static _ChatboxScriptableObj;
    using static OEditorWindow;

    [CreateAssetMenu(menuName = "OLiOYouxi/Chatbox"), Serializable]
    internal class _ChatboxScriptableObj : ScriptableObject
    {
        [SerializeField] private string m_Title = string.Empty;
        [SerializeField, ReorderableList] private Core m_Value = new Core();

        #region -- Public ShotC --
        public string Title
        {
            get => m_Title;
            set => m_Title = value;
        }
        public Core Value
        {
            get => m_Value;
        }
        #endregion

        [Serializable]
        internal class Conversation
        {
            [SerializeField] private string m_Title = string.Empty;
            [SerializeField] private List<_WordScriptableObj> m_Value = new List<_WordScriptableObj>();

            #region -- Public ShotC --
            public string Title
            {
                get => m_Title;
                set => m_Title = value;
            }
            public List<_WordScriptableObj> Value => m_Value;
            #endregion
        }
    }
    [CanEditMultipleObjects]
    internal class ChatboxEditor001 : Editor
    {
        private _ChatboxScriptableObj m_Obj = null;
        public _ChatboxScriptableObj Obj
        {
            get => m_Obj;
            internal set => m_Obj = value;
        }
    }
    [CanEditMultipleObjects]
    internal class ChatboxEditor002 : Editor
    {
        private _ChatboxScriptableObj m_ChatboxData = null;
        private Conversation m_ConversationData = null;
        private TextController m_TextData = null;
        public Conversation ConversationData
        {
            get => m_ConversationData;
            internal set => m_ConversationData = value;
        }
        public _ChatboxScriptableObj ChatboxData
        {
            get => m_ChatboxData;
            internal set => m_ChatboxData = value;
        }
        public TextController TextData
        {
            get => m_TextData;
            internal set => m_TextData = value;
        }
    }
    [CanEditMultipleObjects]
    internal class ChatboxEditor003 : Editor
    {
        private _ChatboxScriptableObj m_ChatboxData = null;
        private _SentenceScriptableObj m_SentenceData = null;
        private TextController m_TextData = null;
        public _SentenceScriptableObj SentenceData
        {
            get => m_SentenceData;
            internal set => m_SentenceData = value;
        }
        public _ChatboxScriptableObj ChatboxData
        {
            get => m_ChatboxData;
            internal set => m_ChatboxData = value;
        }
        public TextController TextData
        {
            get => m_TextData;
            internal set => m_TextData = value;
        }
    }
}
