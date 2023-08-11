using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class FlyViewer : MonoBehaviour
{
  [SerializeField]
  private OVRInput.Controller m_controllerXY;
  [SerializeField]
  private OVRInput.Controller m_controllerZ;

  void Start()
  {
    Debug.Log("PERK starting the script");

    Debug.Log("PERK headset type: " + OVRPlugin.GetSystemHeadsetType());
    if( OVRPlugin.GetSystemHeadsetType() == 
                    OVRPlugin.SystemHeadset.Oculus_Quest_2 ){
      m_controllerXY = OVRInput.Controller.LTouch;
      m_controllerZ = OVRInput.Controller.RTouch;
      Debug.Log("PERK Oculus Quest 2 detected");
    }
    else{
      Debug.Log("PERK **WARNING** unknown system headset type");
      Debug.Log("PERK **WARNING** no controller set!");
    }
	}

  void Update(){
    Vector2 tstick2Dxy = 
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controllerXY);
    Vector2 tstick2Dz = 
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controllerZ);
    GameObject go_ul = GameObject.Find("OVRCameraRig");
    Vector3 currpos = go_ul.transform.position;
    float multiplier = 0.1f;
    if( tstick2Dxy[0] < -0.8 ){
      Vector3 deltapos = new Vector3(-1.0f, 0.0f, 0.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else if( tstick2Dxy[0] > 0.8 ){
      Vector3 deltapos = new Vector3(1.0f, 0.0f, 0.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else if( tstick2Dxy[1] < -0.8 ){
      Vector3 deltapos = new Vector3(0.0f, 1.0f, 0.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else if( tstick2Dxy[1] > 0.8 ){
      Vector3 deltapos = new Vector3(0.0f, -1.0f, 0.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else{
    }

    currpos = go_ul.transform.position;
    if( tstick2Dz[1] < -0.9 ){
      Vector3 deltapos = new Vector3(0.0f, 0.0f, -1.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else if( tstick2Dz[1] > 0.9 ){
      Vector3 deltapos = new Vector3(0.0f, 0.0f, 1.0f);
      Vector3 newpos = currpos + multiplier*deltapos;
      go_ul.transform.position = newpos;
    }
    else{
    }
  }
}
