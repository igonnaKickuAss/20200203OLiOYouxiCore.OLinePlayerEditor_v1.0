using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ���������Сֵ��Χ��slider
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinMaxSliderAttribute : ADrawerAttribute
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        /// <summary>
        /// ʵ��һ���������Сֵ��Χ��slider��������Vector2
        /// </summary>
        /// <param name="minValue">��Сֵ</param>
        /// <param name="maxValue">���ֵ</param>
        public MinMaxSliderAttribute(float minValue, float maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
