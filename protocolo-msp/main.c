#include <msp430.h> 

#define tp BIT3 //transistor bc639 de programação
#define tu BIT4 //transistor bc337 de utilização

/**
 * main.c
 */
int main(void) {

	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	DCOCTL = 60;   BCSCTL1 = 135;           //calibrado para 1 MHz
    P1DIR = 0xFF;                             // All P1.x outputs
    P1OUT = 0;                                // All P1.x reset
    P2DIR = 0xFF;                             // All P2.x outputs
    P2OUT = 0;                                // All P2.x reset
    P1SEL = BIT1 + BIT2;                     // P1.1 = RXD, P1.2=TXD
    P1SEL2 = BIT1 + BIT2;                     // P1.1 = RXD, P1.2=TXD
    P3DIR = 0xFF;                             // All P3.x outputs
    P3OUT = 0;                                // All P3.x reset

    P1REN = 0xFF; P2REN = 0xFF; P3REN = 0xFF;
    P3OUT |= BIT0;  P2OUT &= ~BIT0;

    //configuração dos pinos para chaveamento
    P1DIR &= ~BIT5;          //entrada
    P1REN &= ~(BIT5 + BIT2); //sem resistor
    P1REN |= BIT1;  //<<<<<<<<<habilita resistor de pullup no pino RX do msp<<<<<<<<<<<<<
    P1DIR |= tp + tu;//saída
    P1REN &= ~tp;       //sem resistor
    P1OUT &= ~tp;       //nivel baixo para permitir a aplicaçao Rx/Tx do usuario
    P1REN |= tu;        //resistor on
    P1OUT |= tu;
	
	return 0;
}
