using UnityEngine;


public interface ISubject<TContext>
{
	void Subscribe(IObserver<TContext> observer);
	void Unsubscribe(IObserver<TContext> observer);
}
