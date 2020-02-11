using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ�����ֵ�޶�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MaxValueAttribute : AValidatorAttribute
    {
        public float MaxValue { get; private set; }

        /// <summary>
        /// ʵ��һ�����ֵ�޶�
        /// </summary>
        /// <param name="maxValue">���ֵfloat</param>
        public MaxValueAttribute(float maxValue)
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// ʵ��һ�����ֵ�޶�
        /// </summary>
        /// <param name="maxValue">���ֵint</param>
        public MaxValueAttribute(int maxValue)
        {
            this.MaxValue = maxValue;
        }
    }
}
