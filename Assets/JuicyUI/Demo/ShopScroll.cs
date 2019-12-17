/* using UnityEngine;
using System.Collections;

public class ShopScroll : MonoBehaviour 
{
	public GameObject Page;

	private GameObject SecondPage;
	private bool isTransitioning = false;
	
	void Awake () 
	{
		SecondPage = (GameObject)Instantiate(Page);
		SecondPage.transform.parent = Page.transform.parent;
		SecondPage.transform.localScale = Page.transform.localScale;
		SecondPage.gameObject.SetActive(false);
	}

	public void ScrollFromRight()
	{
		Scroll(1);
	}

	public void ScrollFromLeft()
	{
		Scroll(-1);
	}

	private void Scroll(int direction)
	{
		if(isTransitioning)
			return;

		float panelWidth = Page.transform.parent.GetComponent<UIPanel>().width;

		isTransitioning = true;
		SecondPage.gameObject.SetActive(true);
		SecondPage.transform.localPosition = new Vector3(direction * panelWidth, 0, 0);
		TweenPosition.Begin(SecondPage, 0.3f, new Vector3(0,0,0));
		TweenPosition.Begin(Page, 0.3f, new Vector3(-direction * panelWidth,0,0));

		SecondPage.GetComponent<TweenPosition>().SetOnFinished(() => {
			isTransitioning = false;

			var oldPage = Page;
			Page = SecondPage;
			SecondPage = oldPage;

			SecondPage.gameObject.SetActive(false);
		});
	}
}*/
