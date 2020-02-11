using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ���������һ��������ť
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : ADrawerAttribute
    {
        public string Text { get; private set; }

        /// <summary>
        /// ���������һ��������ť������Ը���ȡһ�����֣��������ı�ǩ���÷�����ǩ����������û�в����ķ���
        /// </summary>
        /// <param name="text">������֡����Լ����֤�����롣��</param>
        public ButtonAttribute(string text = null)
        {
            this.Text = text;
        }
    }
}
