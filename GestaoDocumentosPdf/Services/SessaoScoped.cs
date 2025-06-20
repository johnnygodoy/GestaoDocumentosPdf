using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GestaoDocumentosPdf.Services
{
    public class SessaoScoped
    {
        private readonly ProtectedSessionStorage _storage;

        public SessaoScoped(ProtectedSessionStorage storage) => _storage = storage;

        public string? UsuarioNome { get; private set; }
        public int? UsuarioId { get; private set; }

        public event Action? OnChange;

        public async Task DefinirUsuarioAsync(int id, string nome)
        {
            UsuarioId = id;
            UsuarioNome = nome;
            await _storage.SetAsync("UsuarioId", id);
            await _storage.SetAsync("UsuarioNome", nome);
            NotifyStateChanged();
        }

        public async Task CarregarSessaoAsync()
        {
            var id = await _storage.GetAsync<int?>("UsuarioId");
            var nome = await _storage.GetAsync<string>("UsuarioNome");
            UsuarioId = id.Success ? id.Value : null;
            UsuarioNome = nome.Success ? nome.Value : null;
            NotifyStateChanged();
        }

        public async Task LimparAsync()
        {
            UsuarioId = null;
            UsuarioNome = null;
            await _storage.DeleteAsync("UsuarioId");
            await _storage.DeleteAsync("UsuarioNome");
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}