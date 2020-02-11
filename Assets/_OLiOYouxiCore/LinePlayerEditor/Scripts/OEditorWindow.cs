using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    internal abstract class OEditorWindow : EditorWindow
    {
        #region -- Private Data --
        private int m_IsPrev = 0;
        private TextController m_Text = null;
        #endregion

        #region -- Protected Data --
        protected float indentContainer = 4f;
        protected float indentRowColumn = 2f;
        #endregion

        #region -- Protected ShotC --
        protected bool IsPrev => m_IsPrev == 1;
        protected TextController Text => m_Text ?? (m_Text = new TextController());
        #endregion

        #region -- MONO APIMethods --
        private void OnGUI()
        {
            m_IsPrev = m_IsPrev == 0 ? 1 : 0;
            using (var toContainer = new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(indentContainer);
                using (var _toContainer = new EditorGUILayout.VerticalScope())
                {
                    GUILayout.Space(indentContainer);
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row1())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row2())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row3())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row4())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row5())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                    using (var toRow = new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(indentRowColumn);
                        using (var toColume = new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Space(indentRowColumn);
                            if (!Row6())
                            {
                                Repaint();
                                return;
                            }
                        }
                    }
                }
            }
            Repaint();
        }

        #endregion

        protected virtual bool Row1() => true;
        protected virtual bool Row2() => true;
        protected virtual bool Row3() => true;
        protected virtual bool Row4() => true;
        protected virtual bool Row5() => true;
        protected virtual bool Row6() => true;


        #region -- Text Controller --
        internal class TextController
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

            //#region -- 单例 --
            //static private TextController instance = null;
            public TextController() => this.stringBuilder = new StringBuilder();
            //internal static TextController ForNewOrExistingDisplayText()
            //    => instance == null ? instance = new TextController() : instance;
            //#endregion

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

        #endregion
    }

}

