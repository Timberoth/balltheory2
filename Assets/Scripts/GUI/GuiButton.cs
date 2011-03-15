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
    public string messagee = "";
    public string message = "";
    public string messageDoubleClick = "";
    public ButtonTextures textures;
 
    protected int state = 0;
    protected GUITexture myGUITexture;
    
    private int clickCount = 1;
    private float lastClickTime = 0.0f;
	private bool mouseOver = false;
	
    static private float doubleClickSensitivity = 0.5f;

    protected virtual void SetButtonTexture(ButtonState state)
    {
        if (textures[state] != null)
        {
            myGUITexture.texture = textures[state];			
        }
    }
    
    public virtual void Reset()
    {
        messagee = "";
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
				
		mouseOver = true;
    }

    public virtual void OnMouseDown()
    {
        state++;
        if (state == 2)
            SetButtonTexture(ButtonState.armed);
				
    }

    public virtual void OnMouseUp()
    {		
		// Don't register the click if it' outside the button.
		if( !mouseOver )
		{
			return;
		}
		
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
                if (messagee != "" && message != "")
                {								
					GameObject gameObject = GameObject.Find(messagee);
					if( gameObject != null )
						gameObject.SendMessage( message, SendMessageOptions.RequireReceiver);                    
                }
            }
            else
            {
                if (messagee != "" && messageDoubleClick != "")
                {
                    GameObject gameObject = GameObject.Find(messagee);
					if( gameObject != null )
						gameObject.SendMessage( messageDoubleClick, SendMessageOptions.RequireReceiver);  
                }
            }
        }
        else
        {
            state --;
            if (state < 0)
                state = 0;
        }
		
		// If we're still over the button, then make sure to stay on the hover texture.
		if( mouseOver )
        	SetButtonTexture(ButtonState.hover);
		else
			SetButtonTexture(ButtonState.normal);
		
        lastClickTime = Time.time;			
    }

    public virtual void OnMouseExit()
    {		
		// Deselect the button on mouse exit.
		state = 0;
		SetButtonTexture(ButtonState.normal);		
		mouseOver = false;
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
                        if (messagee != "" && message != "")
                        {
                            GameObject gameObject = GameObject.Find(messagee);
							if( gameObject != null )
								gameObject.SendMessage( messageClick, SendMessageOptions.RequireReceiver);  
		               	}
                    }
                    else if (touch.tapCount == 2)
                    {
                        if (messagee != "" && messageDoubleClick != "")
                        {
                            GameObject gameObject = GameObject.Find(messagee);
							if( gameObject != null )
								gameObject.SendMessage( messageDoubleClick, SendMessageOptions.RequireReceiver);  
		                        }
		                    }
                }
                break;
            }
        }
    }
#endif
}