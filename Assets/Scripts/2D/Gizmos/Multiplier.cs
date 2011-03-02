using UnityEngine;
using System.Collections;

public class Multiplier : Gizmo {
				
	public int multiplier = 1;	
	
	public override void DoMathematicalOperation()
	{		
		ballCounter *= multiplier;
	}
	
	// Multiplier can only go from 1-9
	public void incrementMultiplier()
	{
		multiplier++;
		
		// Roll back to 1
		if( multiplier >= 10 )
			multiplier = 1;
	}
	
	// Multiplier can only go from 1-9
	public void decrementMultiplier()
	{		
		multiplier--;
	
		// Roll up to 9
		if( multiplier <= 0 )
			multiplier = 9;
	}
}
