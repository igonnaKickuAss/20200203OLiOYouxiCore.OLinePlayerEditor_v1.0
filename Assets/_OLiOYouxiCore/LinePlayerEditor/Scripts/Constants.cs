using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    public struct Constants
    {
        //const
        public const string version = "Version 1.0";
        public const string copyRight = "Copyright 2020 好久没吃奥利奥. All rights reserved.";
        public const string requires = "Requires Unity 2016.4.3f1 or higher.";
        public const string defaultIcon = "Assets/_OLiOYouxiCore/LinePlayerEditor/Icons/olio.jpg";
        public const int fontSizeForLabel = 16;
        public const int fontSizeForTextField = 24;
        public const int fontSizeForTextArea = 22;

        public const int fontSizeForButton = 12;

        //static
        static public GUILayoutOption tinyWidth = GUILayout.Width(40f);
        static public GUILayoutOption minWidth = GUILayout.Width(60f);
        static public GUILayoutOption midWidth = GUILayout.Width(150f);
        static public GUILayoutOption maxWidth = GUILayout.Width(300f);
        static public GUILayoutOption minHeight = GUILayout.Height(EditorGUIUtility.singleLineHeight);
        static public GUILayoutOption midHeight = GUILayout.Height(EditorGUIUtility.singleLineHeight * 2);
        static public GUILayoutOption maxHeight = GUILayout.Height(EditorGUIUtility.singleLineHeight * 10);

        static public GUILayoutOption flexibleWidth = GUILayout.ExpandWidth(true);
        static public GUILayoutOption flexibleHeight = GUILayout.ExpandHeight(true);

        static public GUILayoutOption stickerWidth = GUILayout.Width(126);
        static public GUILayoutOption stickerHeight = GUILayout.Height(126);

        static public GUILayoutOption stickerWidthS = GUILayout.Width(64);
        static public GUILayoutOption stickerHeightS = GUILayout.Height(64);
    }
}

