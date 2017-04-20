using UnityEngine;
using UnityEngine.UI;

namespace TennisGame.Menu
{
    public class MenuComponent: MonoBehaviour
    {
        [SerializeField]
        private Text topPlayerTitle;
        [SerializeField]
        private Text bottomPlayerTitle;
        private string topTitle;
        private string bottomTitle;

        public void SetTopTitle(string value)
        {
            topPlayerTitle.text = string.Format("{0}: {1}", topTitle, value);
        }

        public void SetBottomTitle(string value)
        {
            bottomPlayerTitle.text = string.Format("{0}: {1}", bottomTitle, value);
        }

        private void Awake()
        {
            topTitle = topPlayerTitle.text;
            bottomTitle = bottomPlayerTitle.text;
        }
    }
}
