using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

using Object = UnityEngine.Object;

namespace RequireInterface
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceAttributeDrawer : PropertyDrawer
    {
        private Type RequireType => (attribute as RequireInterfaceAttribute).Type;


        public override VisualElement CreatePropertyGUI (SerializedProperty property)
        {
            if (RequireType.IsInterface == false)
                return new Label("Use interface type for attribute.");
            if (property.propertyType != SerializedPropertyType.ObjectReference ||
                property.type.Contains(typeof(GameObject).Name))
                return new Label("Use RequiredInterface for Object, ScriptableObject or Component field.");

            var name = ObjectNames.NicifyVariableName(property.name);
            var field = new ObjectField(name);
            field.bindingPath = property.propertyPath;
            field.RegisterCallback<AttachToPanelEvent>(OnAttach);
            return field;
        }


        private void OnAttach (AttachToPanelEvent e)
        {
            var field = e.target as ObjectField;
            field.UnregisterCallback<AttachToPanelEvent>(OnAttach);
            field.RegisterCallback<DetachFromPanelEvent>(OnDetach);
            field.RegisterValueChangedCallback(OnValueChange);
        }

        private void OnDetach (DetachFromPanelEvent e)
        {
            var field = e.target as ObjectField;
            field.UnregisterCallback<DetachFromPanelEvent>(OnDetach);
            field.UnregisterValueChangedCallback(OnValueChange);
        }

        private void OnValueChange (ChangeEvent<Object> e)
        {
            if (e.newValue == null || e.newValue == e.previousValue)
                return;

            var requireType = RequireType;
            var field = e.target as ObjectField;
            //LogInvalidGameObject(e.newValue, requireType);

            if (e.newValue is GameObject go)
            {
                var newValue = go.GetComponent(requireType);
                field.value = newValue;
                if (newValue == null)
                    Debug.LogError(string.Format("\"{0}\" not contain component implemented {1} interface.",
                            go.name, requireType));
            }
            // If drop GameObject to Component field,
            // its Transform will also be automatically assigned
            else if (e.newValue is Transform t)
            {
                var newValue = t.GetComponent(requireType);
                field.SetValueWithoutNotify(newValue);
            }
            else
            {
                var isImplement = requireType.IsAssignableFrom(e.newValue.GetType());
                if (isImplement == false)
                {
                    Debug.LogError(string.Format("\"{0}\" not implement {1} interface.",
                        e.newValue.name, requireType));
                    field.value = null;
                }
            }
        }

        private void LogInvalidGameObject (Object o, Type requireType)
        {
            if (o is GameObject go && go.GetComponent(requireType) == null)
                Debug.LogError(string.Format("\"{0}\" not contain component implemented {1} interface.",
                        go.name, requireType));
        }
    }
}