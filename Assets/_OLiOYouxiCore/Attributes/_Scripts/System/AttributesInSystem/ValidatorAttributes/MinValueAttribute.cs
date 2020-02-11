using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ����Сֵ�޶�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinValueAttribute : AValidatorAttribute
    {
        public float MinValue { get; private set; }

        /// <summary>
        /// ʵ��һ����Сֵ�޶�
        /// </summary>
        /// <param name="minValue">��Сֵfloat</param>
        public MinValueAttribute(float minValue)
        {
            this.MinValue = minValue;
        }

        /// <summary>
        /// ʵ��һ����Сֵ�޶�
        /// </summary>
        /// <param name="minValue">��Сֵint</param>
        public MinValueAttribute(int minValue)
        {
            this.MinValue = minValue;
        }
    }
}
