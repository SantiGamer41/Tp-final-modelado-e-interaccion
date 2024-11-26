using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInteraction : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLenght;
    public LayerMask layer;
    public GameObject uiGO;
    public Image hint;
    public Text hintText;
    public float hintTime;
    public string defaultHint;
    public InteractableObject InteractableScript;

    // Start is called before the first frame update
    void Start()
    {
        hint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLenght, layer))
        {            
            interactable = hit.collider.GetComponent<InteractableObject>();
            InteractableScript = hit.collider.GetComponent<InteractableObject>();

            if (interactable)
            {
                
                hint.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {

                    
                    string message = "";

                    if (!interactable.activated)
                    {
                        InteractableScript.PlayObjectAnimation();
                        interactable.PlayObjectAnimation();
                        interactable.activated = true;
                        message = "Succesfully open!";
                    }
                    else
                    {
                        message = "It's already open!";
                    }
                    StopAllCoroutines();
                    StartCoroutine(ShowHintDuringTime(message));
                }

            }

            
            
        }
        else
        {
            hint.gameObject.SetActive(false);
        }
        uiGO.SetActive(interactable);

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLenght);
    }

    IEnumerator ShowHintDuringTime(string hintMessage)
    {
        float time = 0;
        hintText.text = hintMessage;
        while (time < hintTime)
        {
            time += Time.deltaTime;
            uiGO.SetActive(true);
            yield return null;
        }
        uiGO.SetActive(false);
        hintText.text = "Press E to Open";
    }

}
