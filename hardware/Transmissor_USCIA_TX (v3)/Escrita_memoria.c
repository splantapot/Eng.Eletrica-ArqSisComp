/*
 * Escrita_memoria.c
 *
 *  Created on: 08/05/2026
 *      Author: evert
 *      Para escrever em uma dada posição da memória
 */
#include <msp430g2553.h>

volatile unsigned char indice, valor, break_condicao;
extern volatile unsigned char* vetor_ptr[];//vetor de endereços na memória
volatile unsigned char comando = 0;
unsigned char escreve_endereco(unsigned char dado) {
	static unsigned char estado = 0;//estado inicializado com zero

	if(break_condicao) {//detectou o break gerado pelo C#?
		UCA0STAT &= ~UCBRK;  estado = 1;
		return 1;
	}

	// break 251 2 0
	switch (estado) {//indice, valor, id
	case 1: // Recebe byte de comando
		estado = 2;
		comando = dado;
		break;
	case 2:
		estado = 3;
		indice = dado;
		break;
		// valor = dado;
	case 3:     // Recebe valor de 8 bits a ser escrito na memória
		estado = 0;
		valor = dado;
		switch(comando){//tipo de comando de escrita
		case 251:
			*(vetor_ptr[indice]) = valor;//escreve efetivamente na memória
			break;
			//case 252:
		}
		return 3;
		break;

		default:
			estado = 0;
			break;
	}//switch estado

	return estado;
}//escreve_endereco()



/*case 2: // Recebe byte baixo do endereço
		addr_low = dado;
		endereco16 = ((unsigned int) indice << 8) | addr_low;
		estado = 3;
		break;
		// grava somente o dado de 8 bits no endereço de 16 bits válido
		if((endereco16 >= 0x0200) && (endereco16 <= 0x03FF))
			*((volatile unsigned char *) endereco16) = valor8;
*/





