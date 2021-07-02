using Entities.Enums;
using UnityEngine;

namespace Systems.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public ScreenEnum ScreenType { get; protected set; }
    }
}