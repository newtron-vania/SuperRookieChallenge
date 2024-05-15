public abstract class UI_Popup : UI_Base
{
    public abstract Define.PopupUIGroup PopupID { get; }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}