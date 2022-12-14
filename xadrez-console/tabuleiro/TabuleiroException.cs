using System;

namespace tabuleiro
{
    class TabuleiroException : Exception //Classe Exception é uma classe do CSharp
    {

        public TabuleiroException(string msg) : base(msg)
        {
        }

    }
}
