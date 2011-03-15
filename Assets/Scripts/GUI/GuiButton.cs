/*
 * This class was written by Jonathan Czeck (aarku) and was found on
 * http://www.unifycommunity.com/wiki/index.php?title=Button
 */

using UnityEngine;
using System.Collections;

public enum ButtonState
{
    normal,
    hover,
    armed
}

[System.Serializable] // Required so it shows up in the inspector 
public class ButtonTextures
{
    public Texture normal=null;
    public Texture hover=null;
    public Texture armed=null;
    public ButtonTextures() {}

    public Texture this [ButtonState state]
    {
        get
        {
            switch(state)
            {
                case ButtonState.normal:
                    return normal;
                case ButtonState.hover:
                    return hover;
                case ButtonState.armed:
                    return armed;
                default:
                    return null;
            }
        }
    }
}


[RequireComponent(typeof(GUITexture))]
[AddComponentMenu ("GUI/Button")]    
public class GuiButton : MonoBehaviour
{
    public GameObject messagee;
    public string message = "";
    public string messageDoubleClick = "";
    public ButtonTextures textures;
 
    protected int state = 0;
    protected GUITexture myGUITexture;
    
    private int clickCount = 1;
    private float lastClickTime = 0.0f;
    static private float doubleClickSensitivity = 0.5f;

    protected virtual void SetButtonTexture(ButtonState state)
    {
        if (textures[state] != null)
        {
            myGUITexture.texture = textures[state];
			print("Setting button texture");
        }
    }
    
    public virtual void Reset()
    {
        messagee = gameObject;
        message = "";
        messageDoubleClick = "";
    }
    
    public bool HitTest(Vector2 pos)
    {
        return myGUITexture.HitTest(new Vector3(pos.x, pos.y, 0));
    }
    
    public virtual void Start()
    {
        myGUITexture = GetComponent(typeof(GUITexture)) as GUITexture; 
        SetButtonTexture(ButtonState.normal);
    }

    public virtual void OnMouseEnter()
    {
        state++;
        if (state == 1)
            SetButtonTexture(ButtonState.hover);
		
		print("OnMouseEnter");
    }

    public virtual void OnMouseDown()
    {
        state++;
        if (state == 2)
            SetButtonTexture(ButtonState.armed);
		
		print("OnMouseDown");
    }

    public virtual void OnMouseUp()
    {
		print("OnMouseUp");
        if (Time.time - lastClickTime <= doubleClickSensitivity)
        {
            ++clickCount;
        }
        else
        {
            clickCount = 1;
        }
        
        if (state == 2)
        {
            state--;
            if (clickCount == 1)
            {
                if (messagee != null && message != "")
                {
                    messagee.SendMessage(message, this);
                }
            }
            else
            {
                if (messagee != null && messageDoubleClick != "")
                {
                    messagee.SendMessage(messageDoubleClick, this);
                }
            }
        }
        else
        {
            state --;
            if (state < 0)
                state = 0;
        }
        SetButtonTexture(ButtonState.normal);
        lastClickTime = Time.time;
    }

    public virtual void OnMouseExit()
    {
        if (state > 0)
            state--;
        if (state == 0)
            SetButtonTexture(ButtonState.normal);
		
		print("OnMouseExit");
    }

#if (UNITY_IPHONE || UNITY_ANDROID)
    void Update()
    {
        int count = Input.touchCount;
        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (HitTest(touch.position))
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    SetButtonTexture(ButtonState.normal);
                }
                else
                {
                    SetButtonTexture(ButtonState.armed);
                }
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.tapCount == 1)
                    {
                        if (messagee != null && message != "")
                        {
                            messagee.SendMessage(message, this);
                        }
                    }
                    else if (touch.tapCount == 2)
                    {
                        if (messagee != null && messageDoubleClick != "")
                        {
                            messagee.SendMessage(messageDoubleClick, this);
                        }
                    }
                }
                break;
            }
        }
    }
#endif
}