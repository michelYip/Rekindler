public interface IState
{
    #region Exposed
    #endregion

    #region Main Methods

    public void DoInit();

    public void DoUpdate();

    public void DoExit();

    #endregion

    #region Privates
    #endregion
}
