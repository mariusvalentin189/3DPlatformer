using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] float speed;
    [SerializeField] float selectedSizeIncrease;
    [SerializeField] Button button;
    private Vector3 newSize;
    private Vector3 currentSize;
    private AudioManager sound;
    // Start is called before the first frame update
    void Start()
    {
        newSize = transform.localScale;
        currentSize = transform.localScale;
        sound = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(button.interactable)
            transform.localScale = Vector3.Lerp(currentSize, newSize, speed);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        newSize *= selectedSizeIncrease;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        newSize = currentSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        newSize = currentSize;
        sound.PlayButtonClickSound();
    }
}
