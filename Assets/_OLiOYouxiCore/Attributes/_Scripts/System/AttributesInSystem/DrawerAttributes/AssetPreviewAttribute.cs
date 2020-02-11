using System;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ����һ����Դ��Ԥ��ͼ
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AssetPreviewAttribute : ADrawerAttribute
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// ����һ����Դ��Ԥ��ͼ��������ĿΪ͸��Ԥ��ͼ��������ĿΪ����Ԥ��ͼ
        /// </summary>
        /// <param name="width">Ĭ��64</param>
        /// <param name="height">Ĭ��64</param>
        public AssetPreviewAttribute(int width = 64, int height = 64)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
