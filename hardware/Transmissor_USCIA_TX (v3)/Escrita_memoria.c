/*
 * Escrita_memoria.c
 *
 *  Created on: 08/05/2026
 *      Author: evert
 *      Para escrever em uma dada posição da memória
 */
#include <msp430g2553.h>

volatile unsigned char indice, indice_alto, valor, break_condicao;
volatile unsigned int end16;
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
        switch(comando) {//tipo de comando de escrita
        // 0x0019;
        case 190:   //BITSET
            // cmd, indice (baixo), indice(alto), valor
            estado = 4;
            indice_alto = dado;
            break;
        case 191:   //BITCLR
            // cmd, indice (baixo), indice(alto), valor
            estado = 5;
            indice_alto = dado;
            break;
        case 192:   //BITINV
            // cmd, indice (baixo), indice(alto), valor
            estado = 6;
            indice_alto = dado;
            break;

            // Escreve efetivamente na memória, por índice
        case 251:
            *(vetor_ptr[indice]) = valor;
            break;
        }
        return 3;
        break;

        default:
            estado = 0;
            break;

    case 4:
        // BITSET
        estado = 0;
        end16 = (((unsigned int) (indice_alto << 8)) + ((unsigned int) indice));
        *(unsigned char*)end16 |= dado;
        return 3;
        break;
    case 5:
        // BITCLR
        estado = 0;
        end16 = (((unsigned int) (indice_alto << 8)) + ((unsigned int) indice));
        *(unsigned char*)end16 &= ~dado;
        return 3;
        break;
    case 6:
        // BITCLR
        estado = 0;
        end16 = (((unsigned int) (indice_alto << 8)) + ((unsigned int) indice));
        *(unsigned char*)end16 ^= dado;
        return 3;
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





