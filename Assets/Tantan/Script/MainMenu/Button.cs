using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    UnityEngine.UI.Button button => GetComponent<UnityEngine.UI.Button>();
    [SerializeField] Sprite[] sprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.transform.position -= new Vector3(40,0,0);
        button.image.sprite = sprite[1];
        button.transform.GetChild(0).GetComponent<Image>().sprite = sprite[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.position += new Vector3(40,0,0);
        button.image.sprite = sprite[0];
        button.transform.GetChild(0).GetComponent<Image>().sprite = sprite[0];
    }
}
