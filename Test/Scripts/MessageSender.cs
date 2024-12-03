using UnityEngine;

namespace RequireInterface.Test
{
    public class MessageSender : MonoBehaviour
    {
        [SerializeField] private string _text = "Hello!";
        [Header("View")]
        [SerializeField, RequireInterface(typeof(IMessageView))] 
        private Component _component;
        [SerializeField, RequireInterface(typeof(IMessageView))] 
        private ScriptableObject _scriptableObject;
        [SerializeField, RequireInterface(typeof(IMessageView))] 
        private Object _object;

        [ContextMenu("View Message")]
        public void ViewMessage ()
        {
            if (_component != null)
                (_component as IMessageView).SetText(_text);
            if (_scriptableObject != null)
                (_scriptableObject as IMessageView).SetText(_text);
            if (_object != null)
                (_object as IMessageView).SetText(_text);
        }
    }
}