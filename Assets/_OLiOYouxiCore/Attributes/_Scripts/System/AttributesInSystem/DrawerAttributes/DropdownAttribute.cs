using System;
using System.Collections;
using System.Collections.Generic;

namespace OLiOYouxiCore.OAttributes
{
    /// <summary>
    /// ʵ��һ��������
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DropdownAttribute : ADrawerAttribute
    {
        public string ValuesFieldName { get; private set; }

        /// <summary>
        /// ʵ��һ��������
        /// </summary>
        /// <param name="valuesFieldName">һ���������һ�����ϣ����ǣ���Ԫ������Ҫ����ָ�����ֶ�������ͬ</param>
        public DropdownAttribute(string valuesFieldName)
        {
            this.ValuesFieldName = valuesFieldName;
        }
    }

    public interface IDropdownList : IEnumerable<KeyValuePair<string, object>>
    {
    }

    /// <summary>
    /// ����ר��Ϊ�����������list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DropdownList<T> : IDropdownList
    {
        private List<KeyValuePair<string, object>> values;

        public DropdownList()
        {
            this.values = new List<KeyValuePair<string, object>>();
        }

        /// <summary>
        /// ����json���ԵĸϽţ���ֵ����
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="value"></param>
        public void Add(string displayName, T value)
        {
            this.values.Add(new KeyValuePair<string, object>(displayName, value));
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.values.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //�ο��� https://blog.csdn.net/PalmAdorableTiger/article/details/80707456
        static public explicit operator DropdownList<object>(DropdownList<T> target)
        {
            DropdownList<object> result = new DropdownList<object>();
            foreach (var kvp in target)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }
    }
}
