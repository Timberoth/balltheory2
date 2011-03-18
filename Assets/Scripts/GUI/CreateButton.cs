using UnityEngine;
using System.Collections;

public class CreateButton : GuiButton {

	public override void OnMouseUp()
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
					GameObject messageeObject = GameObject.Find(messagee);
					if( messageeObject != null )
					{
						// Parse the message, 1st part operation, 2nd part Gizmo
						if( message.Contains("Create_") )
						{
							string[] parts = message.Split('_');							
							
							Camera mainCamera = UnityEngine.Camera.mainCamera;
							Vector3 worldPosition = mainCamera.ViewportToWorldPoint( gameObject.transform.position );							
							
							CreateButtonData data = new CreateButtonData();
							data.x = worldPosition.x - 2.0f;
							data.y = worldPosition.y + 1.0f;
							data.gizmoName = parts[1];							
							messageeObject.SendMessage( "CreateGizmo", data, SendMessageOptions.RequireReceiver);
						}
						else
						{
							messageeObject.SendMessage( message, SendMessageOptions.RequireReceiver);
						}
					}
                }
            }
            else
            {
                if (messagee != "" && messageDoubleClick != "")
                {
                    GameObject gameObject = GameObject.Find(messagee);
					if( gameObject != null )
					{
						gameObject.SendMessage( messageDoubleClick, SendMessageOptions.RequireReceiver);
					}
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
   

#if (UNITY_IPHONE || UNITY_ANDROID)
    public virtual void Update()
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
							{
								// Parse the message, 1st part operation, 2nd part Gizmo
								if( message.Contains("Create_") )
								{
									string[] parts = message.Split('_');
									string function = "";
									if( parts[0] == "Create" )
									{
										function = "CreateGizmo";
									}
									
									gameObject.SendMessage( function, parts[1], SendMessageOptions.RequireReceiver);
								}
								else
								{
									gameObject.SendMessage( message, SendMessageOptions.RequireReceiver);
								}
							}
		                }
                    }
                    else if (touch.tapCount == 2)
                    {
                        if (messagee != "" && messageDoubleClick != "")
                        {
                            GameObject gameObject = GameObject.Find(messagee);
							if( gameObject != null )
							{
								// Parse the message, 1st part operation, 2nd part Gizmo
								if( messageDoubleClick.Contains("Create_") )
								{
									string[] parts = messageDoubleClick.Split('_');
									string function = "";
									if( parts[0] == "Create" )
									{
										function = "CreateGizmo";
									}
									
									gameObject.SendMessage( function, parts[1], SendMessageOptions.RequireReceiver);
								}
								else
								{
									gameObject.SendMessage( messageDoubleClick, SendMessageOptions.RequireReceiver);
								}
							} 
		               	}
		          	}
                }
                break;
            }
        }
    }
#endif
}
