﻿using System.Reflection;

namespace OLiOYouxiCore.OAttributes.Editor
{
    /// <summary>
    /// 关于Field绘制的抽象类
    /// </summary>
	public abstract class AFieldDrawer
	{
        public abstract void DrawField(UnityEngine.Object target, FieldInfo fieldInfo);
	}
}