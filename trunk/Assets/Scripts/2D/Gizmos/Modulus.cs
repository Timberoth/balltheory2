using UnityEngine;
using System.Collections;

public class Modulus : Gizmo {
				
	// This is the second ball output used in the modulus gizmo.
	private Vector3 ballSpawnPoint2;
	
	public int divisor = 1;
	private int remainder = 0;
		
	public override void DoMathematicalOperation()
	{		
		ballCounter /= divisor;
		remainder = ballCounter%divisor;
	}
	
	// Divisor can only go from 1-9
	public void incrementDivisor()
	{
		divisor++;
		
		// Roll back to 1
		if( divisor >= 10 )
			divisor = 1;
	}
	
	// Divisor can only go from 1-9
	public void decrementMultiplier()
	{		
		divisor--;
	
		// Roll up to 9
		if( divisor <= 0 )
			divisor = 9;
	}
}
