using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OLinePlayer
{
    using Core = List<OLiOYouxiCore.OLinePlayer.Letter>;
    using OMathf = OLiOYouxi.OSystem.MathUtility;
    using ReorderableList = OLiOYouxiCore.OAttributes.ReorderableListAttribute;
    //using 

    [CreateAssetMenu(menuName = "OLiOYouxi/Sentence"), Serializable]
    internal class _SentenceScriptableObj : ScriptableObject
    {
        [SerializeField] private int m_Key = 1;
        [SerializeField] private AnimationCurve m_Curve = new AnimationCurve();
        [SerializeField, ReorderableList] private Core m_Value = new Core();

        #region -- Public ShotC --
        public int Key
        {
            get => m_Key;
            internal set => m_Key = value;
        }
        public Core Value
        {
            get => m_Value;
            internal set => m_Value = value;
        }
        public AnimationCurve Curve
        {
            get
            {
                if (m_Value == null) return null;
                m_Curve = m_Curve ?? new AnimationCurve();
                m_Curve.keys = null;
                for (int i = 0, count = m_Value.Count; i < count; i++)
                {
                    float appearVelocity = 10f / m_Value[i].remain;
                    appearVelocity = appearVelocity <= 0 ? .01f : appearVelocity;
                    m_Curve.AddKey(new Keyframe { value = appearVelocity, time = i });
                }
                return m_Curve;
            }
            internal set
            {
                if (m_Value == null) return;
                m_Curve = value;
                for (int i = 0, count = m_Value.Count; i < count; i++)
                {
                    float remain = 10f / m_Curve.Evaluate(i);
                    char val = m_Value[i].value;
                    m_Value[i] = new Letter { remain = remain, value = val };
                }
            }
        }
        #endregion

        #region -- Public APIMethods --

        #endregion



        #region -- Private APIMethods --
        private float GetTimeAmount()
        {
            float time = .0f;
            for (int i = 0, length = m_Value.Count; i < length; i++) time += m_Value[i].remain;
            return time;
        }
        private Vector2Int GetRange()
        {
            Vector2Int range = Vector2Int.zero;
            for (int i = 0, length = m_Value.Count; i < length; i++)
            {
                float currentRemain = m_Value[i].remain;
                if (i != 0)
                {
                    float prevRemain = m_Value[i - 1].remain;
                    if (currentRemain <= prevRemain) continue;
                    range = new Vector2Int(0, Mathf.CeilToInt(currentRemain));
                    continue;
                }
                range = new Vector2Int(0, Mathf.CeilToInt(currentRemain));
            }
            return range;
        }

        #endregion
    }
}
