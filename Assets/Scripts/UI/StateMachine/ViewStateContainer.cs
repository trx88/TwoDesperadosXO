using System;
using System.Collections.Generic;
using UI.Views.Abstraction;
using UnityEngine;

namespace UI.StateMachine
{
    public class ViewStateContainer : MonoBehaviour
    {
        [SerializeField] public List<ViewMap> viewMap;
    }
    
    [Serializable]
    public class ViewMap
    {
        public UIView viewType;
        public ViewBase view;
        public ViewBase parent;
    }
}