﻿using UnityEditor;

namespace OLiOYouxiCore.OAttributes.Editor
{
    /// <summary>
    /// 关于数据更改的抽象类
    /// </summary>
    public abstract class APropertyValidator
	{
        public abstract void ValidateProperty(SerializedProperty property);
	}
}