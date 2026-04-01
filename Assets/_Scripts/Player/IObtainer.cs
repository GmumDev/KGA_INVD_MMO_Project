using UnityEngine;

public interface IObtainer
{

	public void Subscribe(IObtainObserver observer);
	public void Unsubscribe(IObtainObserver observer);
	public void Obtain(ItemData item, int cnt = 1);
}
