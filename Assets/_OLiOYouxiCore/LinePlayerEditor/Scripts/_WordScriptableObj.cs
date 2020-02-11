using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    using Core = List<_SentenceScriptableObj>;
    using ReorderableList = OAttributes.ReorderableListAttribute;
    using Preview = OAttributes.AssetPreviewAttribute;

    [CreateAssetMenu(menuName = "OLiOYouxi/Word"), Serializable]
    internal class _WordScriptableObj : ScriptableObject
    {
        [SerializeField, Preview] private Texture2D m_Icon = null;
        [SerializeField, ReorderableList] private Core m_Value = new Core();

        #region -- Public ShotC --
        public Texture2D Icon
        {
            get => m_Icon;
            internal set => m_Icon = value;
        }
        public Core Value
        {
            get => m_Value;
        }
        #endregion

        #region -- MONO APIMethods --
        private void OnEnable()
            => m_Icon = m_Icon ?? AssetDatabase.LoadAssetAtPath<Texture2D>(Constants.defaultIcon);

        #endregion
    }
}

