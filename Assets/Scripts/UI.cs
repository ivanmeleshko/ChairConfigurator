using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public Canvas canvas, canvasMobile;
    public GameObject[] scrolls;
    public Text[] textScrolls;
    public Image[] outlinesColor, outlinesBase, outlinesMod;
    const string colorMagentaHex = "#E6194D";
    const string colorMagHalfTransp = "#E6194D96";
    const string colorTransparent = "#00000000";
    public static bool mobileSupport;


    private void Awake()
    {
        mobileSupport = !(SystemInfo.operatingSystem.Contains("Windows") || SystemInfo.operatingSystem.Contains("Mac"));
        canvasMobile.gameObject.SetActive(mobileSupport);
        canvas.gameObject.SetActive(!mobileSupport);

        if (mobileSupport)
        {
            Camera.main.transform.position = new Vector3(0, 0.29f, 0);
            Camera.main.transform.eulerAngles = new Vector3(4f, -0.172f, 0.022f);
            Rotator._maxZoom = 6.7f;
            Rotator._minZoom = 12;
        }
    }


    public void SetActiveScroll(string scroll)
    {
        foreach (GameObject g in scrolls)
        {
            g.SetActive(g.name.Equals(scroll));
        }
    }


    public void ChangeTextColor(string textScroll)
    {
        foreach (Text t in textScrolls)
        {
            if (t.name.Equals(textScroll))
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(colorMagentaHex, out color))
                {
                    t.color = color;
                }
            }
            else
            {
                t.color = Color.black;
            }
        }
    }


    public void SetBorder(Image imgOutline)
    {
        if (ChairConfig.scrollType == ChairConfig.ScrollType.Color)
        {
            foreach (Image img in outlinesColor)
            {
                if (img.name.Equals(imgOutline.name))
                {
                    Color color;
                    if (img.sprite != null)
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagHalfTransp, out color))
                        {
                            img.color = color;
                        }
                    }
                    else
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagentaHex, out color))
                        {
                            img.color = color;
                        }
                    }
                }
                else
                {
                    Color color;
                    if (ColorUtility.TryParseHtmlString(colorTransparent, out color))
                    {
                        img.color = color;
                    }
                }
            }
        }

        if (ChairConfig.scrollType == ChairConfig.ScrollType.Base)
        {
            foreach (Image img in outlinesBase)
            {
                if (img.name.Equals(imgOutline.name))
                {
                    Color color;
                    if (img.sprite != null)
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagHalfTransp, out color))
                        {
                            img.color = color;
                        }
                    }
                    else
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagentaHex, out color))
                        {
                            img.color = color;
                        }
                    }
                }
                else
                {
                    Color color;
                    if (ColorUtility.TryParseHtmlString(colorTransparent, out color))
                    {
                        img.color = color;
                    }
                }
            }
        }

        if (ChairConfig.scrollType == ChairConfig.ScrollType.Mod)
        {
            foreach (Image img in outlinesMod)
            {
                if (img.name.Equals(imgOutline.name))
                {
                    Color color;
                    if (img.sprite != null)
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagHalfTransp, out color))
                        {
                            img.color = color;
                        }
                    }
                    else
                    {
                        if (ColorUtility.TryParseHtmlString(colorMagentaHex, out color))
                        {
                            img.color = color;
                        }
                    }
                }
                else
                {
                    Color color;
                    if (ColorUtility.TryParseHtmlString(colorTransparent, out color))
                    {
                        img.color = color;
                    }
                }
            }
        }
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
