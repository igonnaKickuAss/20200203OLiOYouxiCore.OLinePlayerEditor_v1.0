using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ����ͨslider
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SliderAttribute : ADrawerAttribute
    {
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }

        /// <summary>
        /// ʵ��һ����ͨsliderFloat
        /// </summary>
        /// <param name="minValue">��Сֵ</param>
        /// <param name="maxValue">���ֵ</param>
        public SliderAttribute(float minValue, float maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// ʵ��һ����ͨsliderInt
        /// </summary>
        /// <param name="minValue">��Сֵ</param>
        /// <param name="maxValue">���ֵ</param>
        public SliderAttribute(int minValue, int maxValue)
        {
            this.MaxValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
