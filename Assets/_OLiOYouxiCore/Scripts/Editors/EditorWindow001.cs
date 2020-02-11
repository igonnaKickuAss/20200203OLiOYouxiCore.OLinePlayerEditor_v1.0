using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxi.Editors
{
    using OLiOYouxi.OCoroutine;
    using Debug = OLiOYouxi.OSystem.DebugFather;
    using Coroutine = OLiOYouxi.OCoroutine.OCoroutine;
    public class EditorWindow001 : EditorWindow
    {
        [MenuItem("OLiOYouxi/EditorWindow001")]
        static private void Open() => GetWindow(typeof(EditorWindow001), true);

        private void OnGUI()
        {
            using (var toDown = new EditorGUILayout.VerticalScope())
            {
                using (var _toRight = new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("新增方法OnUpdate到update")) EditorApplication.update += OnUpdate;
                    if (GUILayout.Button("移除在update里的方法OnUpdate")) EditorApplication.update -= OnUpdate;
                }
                using (var _toRight = new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("开始协程"))
                    {
                        if (coroutines == null) coroutines = new List<Coroutine>();
                        Coroutine coroutine = this.StartCoroutine(DelayFunc());
                        if (coroutine != null) coroutines.Add(coroutine);
                    }
                    if (GUILayout.Button("关闭所有协程"))
                    {
                        if (coroutines == null) coroutines = new List<Coroutine>();
                        this.StopAllCoroutine();
                    }
                    if (GUILayout.Button("关闭最后一个开启的协程"))
                    {
                        if (coroutines == null) return;
                        this.StopCoroutine(coroutines[coroutines.Count - 1]);
                    }
                }
            }
        }

        List<Coroutine> coroutines = null;
        private IEnumerator DelayFunc()
        {
            WaitForSeconds seconds = new WaitForSeconds(1f);
            for (int i = 0, length = 10; i <= length; i++)
            {
                if (i == 10) yield break;
                Debug.Log = $"开始第{i + 1}次迭代";
                yield return seconds;
                Debug.Log = $"第{i + 1}次迭代已经结束";
            }
        }


        IEnumerator current = null;
        TestCoroutine coroutine = null;
        private void OnUpdate()
        {
            if (coroutine == null) coroutine = new TestCoroutine();
            if (current == null) current = coroutine.GetEnumerator();
            if (current.MoveNext()) Debug.Log = "介似第{current.Current}次迭代！";
        }
    }

    internal class TestCoroutine : IEnumerable
    {
        /// <summary>
        /// Routine
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0, length = 10; i < length; i++)
            {
                if (i == 9) yield break;
                yield return i + 1;
            }
        }
    }
}

