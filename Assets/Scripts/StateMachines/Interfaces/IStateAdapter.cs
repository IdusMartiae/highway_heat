namespace StateMachines.Interfaces
{
    public interface IStateAdapter
    {
        void OnStateEnter();
        void OnStateExit();
    }
}