using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    [SerializeField]private GameObject textBox;
    public GameObject TextBox{
        get {return textBox;}
    }
    [SerializeField]public bool View{get;private set;}

    // Start is called before the first frame update
    void Start()
    {
        View = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTextBox(){
		if(TextBox.activeSelf){
			TextBox.SetActive(false);
		}
		else if(!TextBox.activeSelf){
			TextBox.SetActive(true);
		}
	}
    public void ViewCGs(){
        if(View)View = false;
        else View = true;
    }
}
