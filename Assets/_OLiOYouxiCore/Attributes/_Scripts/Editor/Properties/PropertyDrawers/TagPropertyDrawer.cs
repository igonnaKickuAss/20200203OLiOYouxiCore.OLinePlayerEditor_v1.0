using UnityEditor;
using System.Collections.Generic;

namespace OLiOYouxiCore.OAttributes.Editor
{
    [PropertyDrawer(typeof(TagAttribute))]
    public class TagPropertyDrawer : APropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                // ���ɱ�ǩ�б���Զ����ǩ
                List<string> tagList = new List<string>();
                tagList.Add("(None)");
                tagList.Add("Untagged");
                tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

                string propertyString = property.stringValue;
                int index = 0;
                // ����Ƿ��������Ŀƥ�����Ŀ������ȡ����
                // ��������0������
                for (int i = 1; i < tagList.Count; i++)
                {
                    if (tagList[i] == propertyString)
                    {
                        index = i;
                        break;
                    }
                }

                // �õ�ǰѡ�����������Ƶ�����
                index = EditorGUILayout.Popup(property.displayName, index, tagList.ToArray());

                // ������ѡ���ݵ������Ե�ʵ���ַ���ֵ
                if (index > 0)
                {
                    property.stringValue = tagList[index];
                }
                else
                {
                    property.stringValue = string.Empty;
                }
            }
            else
            {
                EditorGUILayout.HelpBox(property.type + " ������ǩ����֧�֣���\n" +
                "�����string����", MessageType.Warning);
            }
        }
    }
}