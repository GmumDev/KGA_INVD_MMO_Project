using UnityEngine;

public interface IObserver<TContext>
{
    void Update(TContext ctx);
}
