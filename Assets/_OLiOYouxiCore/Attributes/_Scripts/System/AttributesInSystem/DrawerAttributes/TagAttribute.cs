using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ����¶������������ʽ��ѡ��Ľ�GameObject.Tag��ֵ���ֶΣ�Ӧ���㷽���ˡ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TagAttribute : ADrawerAttribute
    {
    }
}
