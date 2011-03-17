using UnityEngine;
using System.Collections;

public class TrashButton : GuiButton {
	
	 public override void OnMouseEnter()
    {
        state++;
        if (state == 1)
            SetButtonTexture(ButtonState.hover);
				
		mouseOver = true;
		
		// Send a message to the GameManager to Delete the currently dragged Gizmo.
		GameObject gameObject = GameObject.Find(messagee);
		if( gameObject != null )
		{						
			gameObject.SendMessage( message, SendMessageOptions.RequireReceiver);						
		}
    }	
}
