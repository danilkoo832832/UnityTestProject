using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    public GameObject objWithAnimations;
    public void DownButton(){
        if (objWithAnimations.GetComponent<AnimationObj>().anim != null){
            objWithAnimations.GetComponent<AnimationObj>().anim.Points = objWithAnimations.GetComponent<AnimationObj>().arpoly(new Vector3(0,0,0),4,8,6);
        }
        else{
            objWithAnimations.GetComponent<AnimationObj>().anim = new Animations(objWithAnimations.GetComponent<AnimationObj>().arpoly(new Vector3(0,0,0),4,8,6),true,10);
        }
    }
}
