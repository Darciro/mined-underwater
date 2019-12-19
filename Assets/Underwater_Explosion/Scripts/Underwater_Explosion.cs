// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Underwater_Explosion : MonoBehaviour {



	public GameObject underwaterExplosion;

	private bool activeExplosion = false;

	void  Start (){

		underwaterExplosion.SetActive(false);

	}


	void  Update (){

		if (Input.GetButtonDown("Fire1"))
		{

			if (activeExplosion == false)
			{
				
				StartCoroutine("launchUnderwaterExplosion");
			}

		}

	}



	IEnumerator launchUnderwaterExplosion (){

		underwaterExplosion.SetActive(true);


		activeExplosion = true;

		yield return new WaitForSeconds (4);  //  Wait for the shockwave to finish

		activeExplosion = false;

		underwaterExplosion.SetActive(false);

	}
}