using UnityEditor;

namespace OLiOYouxiCore.OAttributes.Editor
{
    [PropertyDrawer(typeof(SliderAttribute))]
    public class SliderPropertyDrawer : APropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            EditorDrawUtility.DrawHeader(property);

            SliderAttribute sliderAttribute = PropertyUtility.GetAttribute<SliderAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUILayout.IntSlider(property, (int)sliderAttribute.MinValue, (int)sliderAttribute.MaxValue);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUILayout.Slider(property, sliderAttribute.MinValue, sliderAttribute.MaxValue);
            }
            else
            {
                string warning = sliderAttribute.GetType().Name + " ֻ�����������ֶκ͵����ȸ������ֶΡ�����";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: PropertyUtility.GetTargetObject(property));

                EditorDrawUtility.DrawPropertyField(property);
            }
        }
    }
}