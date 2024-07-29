using System;
using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.Pools;

namespace App.Scripts.Modules.PopupLogic.General.Providers
{
    public interface IPopupProvider
    {
        Dictionary<Type, IPool<Popup.Popup>> PopupPoolsDictionary { get; }
    }
}