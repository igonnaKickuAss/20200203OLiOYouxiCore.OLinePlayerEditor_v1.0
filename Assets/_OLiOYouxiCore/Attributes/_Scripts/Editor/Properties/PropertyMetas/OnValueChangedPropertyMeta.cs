using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace OLiOYouxiCore.OAttributes.Editor
{
    [PropertyMeta(typeof(OnValueChangedAttribute))]
    public class OnValueChangedPropertyMeta : APropertyMeta
    {
        public override void ApplyPropertyMeta(SerializedProperty property, AMetaAttribute metaAttribute)
        {
            OnValueChangedAttribute onValueChangedAttribute = (OnValueChangedAttribute)metaAttribute;
            UnityEngine.Object target = PropertyUtility.GetTargetObject(property);

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, onValueChangedAttribute.CallbackName);
            if (callbackMethod != null &&
                callbackMethod.ReturnType == typeof(void) &&
                callbackMethod.GetParameters().Length == 0)
            {
                property.serializedObject.ApplyModifiedProperties(); // ���Ǳ���Ӧ���ѱ༭Ԫ���ݣ������ص������Ϳ��Ա�ִ��

                callbackMethod.Invoke(target, null);
            }
            else
            {
                EditorDrawUtility.DrawHelpBox(onValueChangedAttribute.GetType().Name + " ֻ�������޷���ֵ���޲����ĺ���", MessageType.Warning, context: target);
            }
        }
    }
}
