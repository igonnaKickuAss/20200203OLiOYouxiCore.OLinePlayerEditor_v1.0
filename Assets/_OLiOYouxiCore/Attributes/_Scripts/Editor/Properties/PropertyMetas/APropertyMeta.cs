using UnityEditor;

namespace OLiOYouxiCore.OAttributes.Editor
{
    public abstract class APropertyMeta
	{
        public abstract void ApplyPropertyMeta(SerializedProperty property, AMetaAttribute metaAttribute);
	}
}