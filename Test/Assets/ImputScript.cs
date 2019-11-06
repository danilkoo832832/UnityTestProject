using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ImputScript : MonoBehaviour
{

    string text;
    public GameObject inputField;
    public GameObject textScreen;
    public Animations anim;
    public GameObject objWithAnimations;
    // Start is called before the first frame update
    public void Start(){
        textScreen.GetComponent<Text>().text = "Введите число";
    }
    public void downButton(){
        StreamReader reader = new StreamReader("Assets/Points.json");
        string json = reader.ReadToEnd();
        reader.Close();
        anim = JsonUtility.FromJson<Animations>(json);

        text = inputField.GetComponent<Text>().text;
        int number = System.Convert.ToInt32(text);
        if(number>anim.Points.Length){
            textScreen.GetComponent<Text>().text = "Вы ввели слишком большое число.";
        }
        else
        {
            Vector3[] Points = (Vector3[])anim.Points.Clone();
            anim.Points = new Vector3[number];
            for (int i = 0; i<number;i++){
                anim.Points[i] = Points[i];
            }
            objWithAnimations.GetComponent<AnimationObj>().anim = anim;
        }
    }
}
