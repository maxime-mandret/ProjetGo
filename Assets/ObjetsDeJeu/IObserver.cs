﻿using Assets.GameLogic;

namespace Assets.ObjetsDeJeu
{
    public interface IObserver<T>
    {
        void ObservedNotified(RemoteMovesStalker remoteMovesStalker);
    }
}