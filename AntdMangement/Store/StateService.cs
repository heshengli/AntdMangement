namespace AntdMangement.Store
{
    public class StateService
    {
        private string _state;

        public string GetCurrentState() => _state ??= Guid.NewGuid().ToString();

        public void RestoreCurrentState(string state) => _state = state;
    }
}
