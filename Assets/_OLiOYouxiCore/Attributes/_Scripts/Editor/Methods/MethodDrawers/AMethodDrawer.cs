﻿using System.Reflection;

namespace OLiOYouxiCore.OAttributes.Editor
{
    /// <summary>
    /// 关于Method绘制的抽象类
    /// </summary>
    public abstract class AMethodDrawer
	{
        public abstract void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo);
	}
}