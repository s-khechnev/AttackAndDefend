using System.Collections.Generic;
using Defender.HUD.Command;
using UnityEngine;
using UnityEngine.UI;

namespace Defender.HUD.Menu
{
    public abstract class GUIMenuBase : IGUIMenu
    {
        protected GameObject Instance;
        protected Dictionary<Button, ICommand> Associations = new();
        protected List<Button> ActiveButtons;

        public abstract void Init();

        public virtual void Show()
        {
            Instance.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            Instance.gameObject.SetActive(false);
        }

        public virtual bool CanShow(GameObject guiItem)
        {
            return true;
        }

        public virtual bool IsShown(GameObject guiItem)
        {
            return true;
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
            Associate(button, command);
            button.onClick.AddListener(() => ButtonClick(button));
        }

        protected virtual void Associate(Button button, ICommand command)
        {
            Associations.Add(button, command);
        }

        protected virtual void RemoveAssociation(Button button)
        {
            if (Associations.ContainsKey(button))
                Associations.Remove(button);
        }
    }
}