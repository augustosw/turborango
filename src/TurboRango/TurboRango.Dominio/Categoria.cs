using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TurboRango.Dominio
{
    internal enum Categoria
    {
        [Description("Comum")]
        COMUM,
        [Description("Cozinha Natural")]
        COZINHA_NATURAL,
        [Description("Cozinha Mexicana")]
        COZINHA_MEXICANA,
        [Description("Churrascaria")]
        CHURRASCARIA,
        [Description("Cozinha Japonesa")]
        COZINHA_JAPONESA,
        [Description("FastFood")]
        FASTFOOD,
        [Description("Pizzaria")]
        PIZZARIA
    }
}
