using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;//*aula177
        private HashSet<Peca> capturadas;//*aula177

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();//*aula177
            capturadas = new HashSet<Peca>();//*aula177
            colocarPecas();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if  (pecaCapturada != null)// aula177
            {
                capturadas.Add(pecaCapturada);// aula177
           }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        
        public HashSet<Peca> pecasCapturadas(Cor cor)// aula177
        {
            HashSet<Peca> aux = new HashSet<Peca>();// aula177
            foreach (Peca x in capturadas)// aula177
            {
                if (x.cor == cor)// aula177
                {
                    aux.Add(x);// aula177
                }
            }
            return aux;// aula177
        }


        public HashSet<Peca> pecasEmJogo(Cor cor)// aula177
        {
            HashSet<Peca> aux = new HashSet<Peca>();// aula177
            foreach (Peca x in pecas)// aula177
            {
                if (x.cor == cor)// aula177
                {
                    aux.Add(x);// aula177
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));// aula177
            return aux;// aula177
        }
        
        public void colocarNovaPeca(char coluna, int linha, Peca peca)// aula177
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());// aula177
            pecas.Add(peca);// aula177
        }
        

        private void colocarPecas()
        {
            
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));// aula177
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));// aula177
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));// aula177
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));// aula177
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));// aula177
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));// aula177

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));// aula177
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));// aula177
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));// aula177
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));// aula177
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));// aula177
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));// aula177
            

            /*
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
            */
        }
    }
}