using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ�����������������������ʾ���ֶ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ValidateInputAttribute : AValidatorAttribute
    {
        public string CallbackName { get; private set; }
        public string Message { get; private set; }

        /// <summary>
        /// ʵ��һ�����������������������ʾ���ֶ�
        /// </summary>
        /// <param name="callbackName">�ص�����ǩ�����ƣ���������ֵ����ָ���ֶ�����һ�µĵ��Σ�</param>
        /// <param name="message">������ʾѶϢ</param>
        public ValidateInputAttribute(string callbackName, string message = null)
        {
            this.CallbackName = callbackName;
            this.Message = message;
        }
    }
}
