using UnityEngine;
using UnityEngine.UI;

public class SetImageActive : MonoBehaviour
{
    public void ToggleImage()
    {
        Image img = GetComponent<Image>();

        img.enabled = !img.isActiveAndEnabled;
    }
}
