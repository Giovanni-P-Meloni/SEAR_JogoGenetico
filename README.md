# SEAR_JogoGenetico
Jogo feito para a disciplina de Sistemas Evolutivos durante o primeiro semestre de 2020


Durante a disciplina, foi desenvolvido um jogo que usa os conceitos de Algoritmos Geneticos ensinados em aula.
O jogo consiste em um canhão que dispara pequenos projéteis, cada vez que um projétil colide com um bloco, aquele bloco é destruido e o score associado à aquele bloco é adicionado ao score total daquela jogada. O projétil é destruido quando sai da área designada a ele ou quando entra em um loop infinito.

Cada jogada, que consiste em 3 projéteis, é considerada um 'indivíduo' de uma população de 'n' jogadas. Após os 'n' individuos terem tido sua vez, o melhor daquela geração é escolhido para passar seus genes para frente. Detalhes mais específicos sobre implemenação encontra-se dentro do próprio código em forma de comentários.
