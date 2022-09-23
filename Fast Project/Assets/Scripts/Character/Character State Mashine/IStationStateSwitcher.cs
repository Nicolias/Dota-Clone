namespace Characters.StateMashine
{
    public interface IStationStateSwitcher
    {
        void SwitchState<T>() where T : BaseState;
    }
}