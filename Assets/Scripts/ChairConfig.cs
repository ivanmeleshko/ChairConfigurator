using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChairConfig : MonoBehaviour
{

    public GameObject Model;
    public GameObject[] ChairBase;
    public Text textValues;
    List<GameObject> parts;
    public enum ScrollType { Color, Base, Mod };
    public static ScrollType scrollType;


    void Start()
    {
        if (UI.mobileSupport)
        {
            Model.transform.position = new Vector3(0, -1.22f, 8);
        }
        parts = new List<GameObject>();
        parts.Add(FindObject(GameObject.Find(Model.name), "T_pillowhight"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T-sidewalls"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_beck"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_pillow"));       
        parts.Add(FindObject(GameObject.Find(Model.name), "T-sidewalls"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_subcontractor"));
        parts.Add(FindObject(GameObject.Find(Model.name), "T_subcontractor2.0"));
        //Invoke("m1", 1.0f);
    }


    public void SetMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }


    public void ChangeMaterial(Material mat)
    {
        foreach (GameObject part in parts)
        {
            if (part != null)
            {
                if (!part.name.Equals("T_subcontractor"))
                    part.GetComponent<MeshRenderer>().material = mat;
                else
                    part.GetComponent<SkinnedMeshRenderer>().material = mat;
            }
        }

        scrollType = ScrollType.Color;
    }


    public void ChangeBase(string basePart)
    {
        foreach (GameObject g in ChairBase)
        {
            g.SetActive(g.name.Equals(basePart));
        }

        scrollType = ScrollType.Base;
    }


    public void EnablePillowhight(bool pillowhight)
    {
        parts[0].SetActive(pillowhight);
        scrollType = ScrollType.Mod;
        textValues.text = pillowhight ? sTotal : s;      
    }


    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }


    public void m1()
    {
        //gameObject.GetComponent<MeshRenderer>().material = newMat1;
        //Renderer r = GetComponent<Renderer>();
        //Material[] mats = r.materials;  // copy of materials array.
        //mats[2] = newMat1; // set new material
        //r.materials = mats; // assign updated array to materials array

        //Invoke("m2", 1.0f);
    }

    void m2()
    {
        //gameObject.GetComponent<MeshRenderer>().material = newMat2;
        //Renderer r = GetComponent<Renderer>();
        //Material[] mats = r.materials;  // copy of materials array.
        //mats[2] = newMat2; // set new material
        //r.materials = mats; // assign updated array to materials array

        //Invoke("m1", 1.0f);
    }


    string sTotal = @"



77

105

22

3,5

990";

    string s = @"



77

85

22

3,5

990";

}
