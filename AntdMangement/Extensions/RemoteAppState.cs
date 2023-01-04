using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace AntdMangement.Extensions
{
    public class RemoteAppState : RemoteAuthenticationState
    {
        public string State { get; set; }
    }
}
