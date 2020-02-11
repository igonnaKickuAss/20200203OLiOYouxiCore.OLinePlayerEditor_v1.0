using System.Reflection;

namespace OLiOYouxiCore.OAttributes.Editor
{
    /// <summary>
    /// 关于NativeProperty绘制的抽象类
    /// </summary>
    public abstract class ANativePropertyDrawer
	{
        public abstract void DrawNativeProperty(UnityEngine.Object target, PropertyInfo propertyInfo); 
	}
}