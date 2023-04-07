using System.Collections.Generic;
using Defender.HUD.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD.Menus
{
    public abstract class GUIMenuBase : MonoBehaviour, IGUIMenu
    {
        protected GameObject Instance;
        protected Dictionary<Button, ICommand> Associations = new();

        public virtual void Show()
        {
            Instance.SetActive(true);
        }

        public virtual void Hide()
        {
            Instance.SetActive(false);
        }

        public virtual bool CanShow(GameObject guiItem)
        {
            return true;
        }

        public virtual bool IsShown(GameObject guiItem)
        {
            return guiItem.activeSelf;
        }

        public virtual bool IsAvailable(GameObject guiItem)
        {
            return true;
        }

        public virtual bool CanButtonClick(Button button)
        {
            return Associations.ContainsKey(button) && Associations[button].CanExecute(button);
        }

        public void ButtonClick(Button button)
        {
            if (CanButtonClick(button))
                Associations[button].Execute(button);
        }

        public virtual void OnSourceChanged()
        {
            foreach (var item in Associations)
            {
                var buttonScript = item.Key.GetComponent<Button>();
                if (buttonScript != null)
                {
                    buttonScript.enabled = true;
                }
            }
        }

        protected virtual void AssociateButton(Button button, ICommand command)
        {
            AddAssociation(button, command);
            button.onClick.AddListener(() => ButtonClick(button));
        }

        private void AddAssociation(Button button, ICommand command)
        {
            Associations.Add(button, command);
        }

        private void RemoveAssociation(Button button)
        {
            if (Associations.ContainsKey(button))
                Associations.Remove(button);
        }
    }
}