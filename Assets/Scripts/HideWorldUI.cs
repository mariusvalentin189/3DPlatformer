using UnityEngine;
using UnityEngine.UI;

public class HideWorldUI : MonoBehaviour
{
    [SerializeField] float hideDistance;
    Transform player;
    bool visible = true;
    Image panelImage;
    GameObject tutorialText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        panelImage = GetComponent<Image>();
        tutorialText = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > hideDistance && visible)
        {
            panelImage.enabled = false;
            tutorialText.SetActive(false);
            visible = false;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= hideDistance && !visible)
        {
            panelImage.enabled = true;
            tutorialText.SetActive(true);
            visible = true;
        }
    }
}
