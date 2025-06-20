using System;

namespace GestaoDocumentosPdf.Data

{
    public class Documento
    {        
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string CaminhoArquivo { get; set; } = string.Empty;
            public DateTime DataEnvio { get; set; } = DateTime.Now;
            public int UsuarioId { get; set; }
            public Usuario? Usuario { get; set; }
        
    }
}
