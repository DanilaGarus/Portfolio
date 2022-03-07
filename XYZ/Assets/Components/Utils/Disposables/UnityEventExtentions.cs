﻿using System;
using UnityEditor;
using UnityEngine.Events;

namespace Components.Utils.Disposables
{
    public static class UnityEventExtentions
    {
        public static IDisposable Subscribe(this UnityEvent unityEvent, UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable((() => unityEvent.RemoveListener(call)));
        }
        
        public static IDisposable Subscribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable((() => unityEvent.RemoveListener(call)));
        }
    }
}