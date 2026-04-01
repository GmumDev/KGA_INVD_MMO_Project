using UnityEngine;

public interface IObtainer
{

	public void Subscribe(IObtainObserver observer);
	public void Unsubscribe(IObtainObserver observer);
	public void Obtain(ItemIds id, int cnt);
}
