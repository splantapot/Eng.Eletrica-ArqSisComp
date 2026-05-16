/*
 * Botao.h
 *
 *  Created on: 4 de mar de 2024
 *      Author: evert
 */

#ifndef BOTAO_H_
#define BOTAO_H_
#include <Temporizador.h>

typedef unsigned char byte;
struct botao {//definição do meu novo tipo de dado temporizador
	char subida;
	char inverter;
	char f1;
};
// fim da definição da estrutura
typedef struct botao Botao;
void inicializa_botao(byte subida, Botao* b);
byte botao_clicado(Botao* b, byte entrada);//entrada tem que ser 0 ou 1 obrigatoriamente

////============================================BOTAO FILTRADO====================================================================
//nova função: a do botão filtrado
struct botao_filtrado {
	Temporizador t;
	Botao b;
	char osc;
	char ent_ant;
};
typedef struct botao_filtrado Botao_filtrado;
char botao_filtrado_clicado(Botao_filtrado* bf, byte entrada);//retorna se o botao filtrado foi clicado
void inicializa_botao_filtrado(byte subida, Botao_filtrado* bf, byte tempo_oscilacao);

#endif /* BOTAO_H_ */
