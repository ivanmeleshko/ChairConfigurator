using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpener : MonoBehaviour
{

    public GameObject panel, raisingPanel, panelMaterial, panelBase;
    public Image menuImage;


    public void OpenPanel()
    {
        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
        RaisePanel();
    }


    public void RaisePanel()
    {
        if (raisingPanel != null)
        {
            Animator animator = raisingPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool open = animator.GetBool("open");
                animator.SetBool("open", !open);
                menuImage.transform.localScale *= -1;
            }
        }
    }


    public void ShowMatPanel()
    {
        if (panelMaterial != null)
        {
            Animator animator = panelMaterial.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                //panelMaterial.SetActive(!isOpen);
                animator.SetBool("open", !isOpen);
            }
            if (animator.GetBool("open"))
            {
                if (panelBase != null)
                {
                    Animator a = panelBase.GetComponent<Animator>();
                    if (a != null)
                    {
                        //bool isOpen = a.GetBool("open");
                        a.SetBool("open", false);
                    }
                }
            }
        }
    }


    public void ShowBasePanel()
    {
        if (panelBase != null)
        {
            Animator animator = panelBase.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                //panelBase.SetActive(!isOpen);
                animator.SetBool("open", !isOpen);
                
            }
            if (animator.GetBool("open"))
            {
                if (panelMaterial != null)
                {
                    Animator a = panelMaterial.GetComponent<Animator>();
                    if (a != null)
                    {
                        //bool isOpen = a.GetBool("open");
                        a.SetBool("open", false);
                    }
                }
            }
        }
    }
}
