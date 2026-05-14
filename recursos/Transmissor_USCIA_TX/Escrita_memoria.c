/*
 * Escrita_memoria.c
 *
 *  Created on: 08/05/2026
 *      Author: evert
 *      Para escrever em uma dada posição da memória
 */
#include <msp430g2553.h>

volatile unsigned char indice, break_condicao;
extern volatile unsigned char* vetor_ptr[];//vetor de endereços na memória

unsigned char escreve_endereco(unsigned char dado) {
	static unsigned char estado = 0;//estado inicializado com zero
	if(break_condicao) {//detectou o break gerado pelo C#?
		UCA0STAT &= ~UCBRK;  estado = 1;
		return 1;
	}

	switch (estado) {
	case 0: // Espera o byte de sincronismo (0x00)
		break;
	case 1: // Recebe byte do indice do vetor
		indice = dado;
		estado = 2;
		break;
	case 2:     // Recebe valor de 8 bits a ser escrito na memória
		estado = 0;
		*(vetor_ptr[indice]) = dado;//escreve efetivamente na memória
		return 2;
		//break;
	default:
		estado = 0;
		break;
	}     //switch

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





