#include <msp430.h>
#define tp BIT3 //transistor bc639 de programação
#define tu BIT4 //transistor bc337 de utilização
#define teste2
#ifdef teste1
const char string1[] = { "Everton\r\n" };//tem que ser \r\n nessa sequencia
#endif
#ifdef teste2
char string1[8]; //const char string1[] = { "Everton\r\n" };//tem que ser \r\n nessa sequencia
#endif
volatile unsigned char* pc = 0;// aponta para ninguem
unsigned int i = 0; char j = 0;

char* str = "ola, bom dia";
void imprima(char* ptr_C);
int main(void){
  //string1[7] = '\0';
  WDTCTL = WDTPW + WDTHOLD;                 // Stop WDT
  DCOCTL = 60;   BCSCTL1 = 135; 			//calibrado para 1 MHz
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
  	P1REN &= ~tp;	    //sem resistor
  	P1OUT &= ~tp;	    //nivel baixo para permitir a aplicaçao Rx/Tx do usuario
  	P1REN |= tu;	    //resistor on
  	P1OUT |= tu;

  UCA0CTL1 |= UCSSEL_2;                     // SMCLK
  /*UCA0BR0 = 104;                            // 1MHz 9600
  UCA0BR1 = 0;                              // 1MHz 9600
  UCA0MCTL = UCBRS0;*/                        // Modulation UCBRSx = 1

  UCA0BR0 = 26;//38400 , dá erro repetindo o primeiro caracter a 76800
  UCA0BR1 = 0;
  UCA0MCTL = 0;

  UCA0CTL1 &= ~UCSWRST;                     // **Initialize USCI state machine**
  IE2 |= UCA0RXIE;                          // Enable USCI_A0 RX interrupt

  __bis_SR_register(/*LPM3_bits*/ + GIE);       // Enter LPM3 w/ int until Byte RXed
  char cont = 0;


  while(1){
	  P1OUT ^= BIT0;
	  volatile unsigned int j=65000;
	  while(j--);

	  switch (cont) {
	  case 0:
		  imprima("Bem-vindo\r\n"); //pc = str;
		  break;
	  case 1:
		  imprima("Hoje\r\n");
		  break;
	  case 2:
		  imprima("BOM\r\n");
			break;
		case 3:
			imprima("Perfeito\r\n");
			break;
		//default:
	  }

	  cont++;
	  cont %= 4;
  }
}

void imprima(char* ptr_C){
	pc = ptr_C;
	P3OUT ^= BIT7;
	IE2 |= UCA0TXIE;                        // Enable USCI_A0 TX interrupt
	UCA0TXBUF = *pc++;//string1[i++]; //triga o tx inicial
}

#pragma vector=USCIAB0TX_VECTOR
__interrupt void USCI0TX_ISR(void){
  UCA0TXBUF = *pc++;//string1[i++];                 // TX next character

  if (!(*pc)/*!string1[i]*/)  // == '\0' TX over?  i == sizeof string1
  {  IE2 &= ~UCA0TXIE;       }                // Disable USCI_A0 TX interrupt
}


#pragma vector=USCIAB0RX_VECTOR
__interrupt void USCI0RX_ISR(void){
	if (UCA0RXBUF == 'r'){                     // 'r' received?
		WDTCTL = 0;
	}
}

//P3OUT ^= BIT7;//deon
	/*
	string1[j++] = UCA0RXBUF;
	if (j > sizeof string1 - 1){   //  3  > 2
		i = j = 0;
		IE2 |= UCA0TXIE;                        // Enable USCI_A0 TX interrupt
		UCA0TXBUF = string1[i++];
	}*/

	/*if (UCA0RXBUF == 'u'){                     // 'u' received?
		P3OUT ^= BIT7;
		IE2 |= UCA0TXIE;                        // Enable USCI_A0 TX interrupt
		//UCA0TXBUF = string1[i++];
		UCA0TXBUF = *pc++;//string1[i++];
	}*/

/*
 if (j > sizeof string1 - 1)     3  > 2
 {
   i = 0;
   j = 0;
   IE2 |= UCA0TXIE;                        // Enable USCI_A0 TX interrupt
   UCA0TXBUF = string1[i++];
 }*/
