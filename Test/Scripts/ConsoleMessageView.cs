using UnityEngine;

namespace RequireInterface.Test
{
    [CreateAssetMenu(fileName = "ConsoleMessageView", menuName = "Test/RequireInterface/ConsoleMessageView")]
    public class ConsoleMessageView : ScriptableObject, IMessageView
    {
        public void SetText (string text)
            => Debug.Log(text);
    }
}