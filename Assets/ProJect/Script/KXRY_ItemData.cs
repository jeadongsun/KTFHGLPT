using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KXRY_ItemData : MonoBehaviour
{
  public Text Name_Text;

  public Text JiNeng_Text;

  public Text DiQu_Text;

  public Text Time_Text;

  public void StartDrag()
  { 
    //Debug.Log("11111111");
    //GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>().StopScrollRectContrl();
  }

  public void StopDrag()
  {
      //Debug.Log("222222222");
   //GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>().StartScrollRectContrl();
  }
}
