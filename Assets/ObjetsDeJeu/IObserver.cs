using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ObjetsDeJeu
{
    public interface IObserver<T>
    {
        void ObservedNotified<T>(T observedState);
    }
}
