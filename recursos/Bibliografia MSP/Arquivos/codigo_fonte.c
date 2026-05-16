/*
 * ======== Standard MSP430 includes ========
 */
#include <msp430.h>
#include <Temporizador.h>
#include <Utilidades.h>
#include <Botao.h>
/*
 * ======== Grace related includes ========
 */
#include <ti/mcu/msp430/Grace.h>

#define TEMPO_TICK 10000 //us
unsigned int tick; //deixar declarado essa variavel chamada de tick

int main(void)
{
    Grace_init();                   // Activate Grace-generated configuration
    setup_tick(TEMPO_TICK);
    Temporizador t1, t2;//criado o temporizador t1
    Temporizador t[20];
    int i;
    for (i = 0; i < 20; ++i) {
		inicializa_temporizador(10*(i+1),&t[i]);
	}
    Botao br; Botao_filtrado bf;
    inicializa_temporizador(50,&t1);//incializar t1 com 500 ms
    inicializa_temporizador(10,&t2);
    inicializa_botao(1,&br);
    inicializa_botao_filtrado(1,&bf,10);
    while(1){// >>>>> Fill-in user code here <<<<<
		if (passou_tempo(&t1)) { ////aguarda 500 ms para inverter o estado do bit5 da porta2
			P2OUT ^= BIT4;				//inverte o estado do bit 4 da porta 2
			reseta_temporizador(&t1);	//reseta a contagem de tempo de t1
		}	//// fim do if passou tempo do t1

    	if (passou_tempo(&t2)) {//aguarda 100 ms para inverter o estado do bit4 da porta2
			P2OUT ^= BIT5;              //inverte o estado do bit 4 da porta 2
			reseta_temporizador(&t2);	//reseta a contagem de tempor de t2
		}// fim do if passou tempo do t2

    	if ( botao_clicado(&br, (P2IN & BIT1)? 1: 0)  ){//verifica se o botão sem filtro no pino p2.1 foi clicado
    		P2OUT ^= BIT0;
    	}

    	if ( botao_clicado(&bf, (P2IN & BIT2)? 1: 0)  ){//verifica se o botão com filtro no pino p2.2 foi clicado
    		P2OUT ^= BIT0;
    	}
    }//fim do laço infinito
    
    return (0);
}

#pragma vector=TIMER1_A1_VECTOR
__interrupt void TIMER1_A1_ISR_HOOK(void){
	switch (TA1IV) {
	//case 0x02: //canal 1
	//	break;
	case 0x04: 	//canal 2
		//Vem pra cá a cada intervalo de tempo definido pelo ta1ccr2
		TA1CCR2 += TEMPO_TICK; //p/ timer1 contando até 65535(modo 2)
		TA1CCTL2 &= ~CCIFG;
		tick++;//obrigatório para que a biblioteca funcione
		//LPM0_EXIT;//opcional, caso deseje que a main rode
		break;
	}// fim switch taiv
}
