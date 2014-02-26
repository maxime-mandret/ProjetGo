using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ObjetsDeJeu
{
    public interface IObservable<T>
    {
        List<IObserver<T>> Observers { get; set; } 
    }
}
