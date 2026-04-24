using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum Type
{
    None,
    Normal,
    Reverse
}

public class Button : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    UnityEngine.UI.Button button => GetComponent<UnityEngine.UI.Button>();
    [SerializeField] Type type = Type.Normal;
    [SerializeField] Sprite[] sprite;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        float xVal = type switch
        {
            Type.None => 0,
            Type.Normal => 40,
            Type.Reverse => -40,
            _ => 0
        };

        button.transform.position -= new Vector3(xVal,0,0);
        button.image.sprite = sprite[1];
        var child = button.transform.GetChild(0);
        if (child != null)
            child.GetComponent<Image>().sprite = sprite[1];
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        float xVal = type switch
        {
            Type.None => 0,
            Type.Normal => 40,
            Type.Reverse => -40,
            _ => 0
        };

        button.transform.position += new Vector3(xVal,0,0);
        button.image.sprite = sprite[0];
        button.transform.GetChild(0).GetComponent<Image>().sprite = sprite[0];
    }
}