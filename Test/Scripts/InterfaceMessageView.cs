using UnityEngine;
using UnityEngine.UI;

namespace RequireInterface.Test
{
    [AddComponentMenu("Test/RequireInterface/ComponentMessage")]
    public class InterfaceMessageView : MonoBehaviour, IMessageView
    {
        [SerializeField] private Text _text;

        public void SetText (string text)
            => _text.text = text;
    }
}