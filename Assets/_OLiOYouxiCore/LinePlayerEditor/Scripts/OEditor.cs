using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    public class OEditor : Editor
    {
        #region -- Private Data --
        private int m_IsPrev = 0;
        private string m_text = string.Empty;
        #endregion

        #region -- Protected Data --
        protected Vector2 scrollPos = Vector2.zero;
        protected GUIContent content = new GUIContent();
        protected float indentContainer = 4f;
        protected float indentRowColumn = 2f;
        #endregion

        #region -- Protected ShotC --
        protected bool IsPrev => m_IsPrev == 1;
        #endregion

        #region -- Public ShotC --
        public string text
        {
            get => m_text;
            internal set => m_text = value;
        }
        #endregion

        #region -- Override APIMethods --
        public override void OnInspectorGUI()
        {
            m_IsPrev = m_IsPrev == 0 ? 1 : 0;
            using (var toScroll = new EditorGUILayout.ScrollViewScope(scrollPos))
            {
                scrollPos = toScroll.scrollPosition;
                using (var toContainer = new EditorGUILayout.HorizontalScope(GUI.skin.box))
                {
                    GUILayout.Space(indentContainer);
                    using (var _toContainer = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentContainer);
                        using (var toRow = new EditorGUILayout.VerticalScope())
                        {
                            using (var toColumn = new EditorGUILayout.HorizontalScope())
                            {
                                //TODO..标题。。。内容（水平）
                                if (!ColumnWithTitleAndContent()) return;
                            }
                            using (var toColumn = new EditorGUILayout.HorizontalScope())
                            {
                                using (var toDown = new EditorGUILayout.VerticalScope())
                                {
                                    //TODO..标题。。。内容（垂直）
                                    if (!RowWithTitleAndContent()) return;
                                }
                            }
                        }
                    }
                }
            }
            Repaint();
        }
        #endregion

        #region -- Protected APIMethods --
        protected virtual bool ColumnWithTitleAndContent()
        {
            return true;
        }
        protected virtual bool RowWithTitleAndContent()
        {
            return true;
        }

        #endregion
    }
}

