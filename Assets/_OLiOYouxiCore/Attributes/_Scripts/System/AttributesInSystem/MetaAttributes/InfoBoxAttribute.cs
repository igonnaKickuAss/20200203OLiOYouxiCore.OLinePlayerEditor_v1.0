using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ����Ϣ����
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class InfoBoxAttribute : AMetaAttribute
    {
        public string Text { get; private set; }
        public InfoBoxType Type { get; private set; }
        public string VisibleIf { get; private set; }

        /// <summary>
        /// ʵ��һ����Ϣ����
        /// </summary>
        /// <param name="text">��Ϣ���ӵ�ѶϢ</param>
        /// <param name="type">�����Ϣ��ʲô���ࣿ</param>
        /// <param name="visibleIf">ʲô������ʾ����һ������ֵ�ֶλ���һ�����ز���ֵ�ĺ�����</param>
        public InfoBoxAttribute(string text, InfoBoxType type = InfoBoxType.Normal, string visibleIf = null)
        {
            this.Text = text;
            this.Type = type;
            this.VisibleIf = visibleIf;
        }

        /// <summary>
        /// ʵ��һ����Ϣ����
        /// </summary>
        /// <param name="text">��Ϣ���ӵ�ѶϢ</param>
        /// <param name="visibleIf">ʲô������ʾ����һ������ֵ�ֶλ���һ�����ز���ֵ�ĺ�����</param>
        public InfoBoxAttribute(string text, string visibleIf)
            : this(text, InfoBoxType.Normal, visibleIf)
        {
        }
    }

    public enum InfoBoxType
    {
        Normal,
        Warning,
        Error
    }
}
