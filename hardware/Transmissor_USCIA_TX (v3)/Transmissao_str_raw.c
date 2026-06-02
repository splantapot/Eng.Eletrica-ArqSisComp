/*
 * Transmissao_str_raw.c
teste de envio de dados pela serial para envio de string e dados da memoria para
implementar um depurador ou pelo menos para visualizar na tela do computador os dados
na memória
 */
#include <msp430.h>
#include <Temporizador.h>
#include <stdlib.h>
#include <string.h>
#define TEMPO_TICK 10000 /* 10 mil us = 10 ms*/
unsigned int tick;
void setup_tick(unsigned int tempo_tick);

#define tp BIT3 //transistor bc639 de programação
#define tu BIT4 //transistor bc337 de utilização
#define teste2
#ifdef teste1
const char string1[] = { "Everton\r\n" };//tem que ser \r\n nessa sequencia
#endif
#ifdef teste2
char string1[8]; //const char string1[] = { "Everton\r\n" };//tem que ser \r\n nessa sequencia
#endif
volatile unsigned char* pc = 0;// aponta para ninguem. Com volatile, ele é obrigado a ler da memória toda vez.
unsigned char qtd = 0; char* fila_msgs[3];//fila

char* str = "\r\nOla, bom dia\r\n";
void imprima(char* ptr_C);

/*void func(void);
unsigned int pece = (unsigned int)func;*/
//volatile unsigned int pece;//para saber do valor do PC
//void pc_to_string(char *str); char buff[5];
extern char e;
int main(void){
  //__asm(" MOV R0, &pece");//saber do pc atual => 0xC00A
  WDTCTL = WDTPW + WDTHOLD;                 // Stop WDT
  //pc_to_string(buff);
  DCOCTL = 68;   BCSCTL1 = 135;  			//calibrado para 1 MHz
  P1DIR = 0xFF;                             // All P1.x outputs
  P1OUT = 0;                                // All P1.x reset
  P2DIR = 0xFF;                             // All P2.x outputs
  P2OUT = 0;                                // All P2.x reset
  P1SEL = BIT1 + BIT2;                     // P1.1 = RXD, P1.2=TXD
  P1SEL2 = BIT1 + BIT2;                     // P1.1 = RXD, P1.2=TXD
  P3DIR = 0xFF;                             // All P3.x outputs
  P3OUT = 0;                                // All P3.x reset
  setup_tick(TEMPO_TICK);

  P1REN = 0xFF; P2REN = 0xFF; P3REN = 0xF7;//p3.3 resistor desativado
  P3OUT |= BIT0;  P2OUT &= ~BIT0;
  //configuração dos pinos para chaveamento
  	P1DIR &= ~BIT5;          //entrada
  	P1REN &= ~(BIT5 + BIT2); //sem resistor  <<<??? tp também?
  	P1REN |= BIT1;  //<<<<<<<<<habilita resistor de pullup no pino RX do msp<<<<<<<<<<<<<
  	P1DIR |= tp + tu;//saída
  	P1REN &= ~tp;	    //sem resistor
  	P1OUT &= ~tp;	    //nivel baixo para permitir a aplicaçao Rx/Tx do usuario
  	P1REN |= tu;	    //resistor on
  	P1OUT |= tu | BIT1;////<<<<<<<<<<<

  	/*UCA0BR0 = 104;                            // 1MHz 9600
  UCA0BR1 = 0;                              // 1MHz 9600
  UCA0MCTL = UCBRS0;*/                        // Modulation UCBRSx = 1
  //UART configuração
  	UCA0CTL1 |=  UCSWRST;      // Reseta o módulo USCI_A0
  	UCA0CTL1 |= UCSSEL_2 | UCBRKIE;//SMCLK e <<<<<<<<<habilitada a detecção de break!!!!!!!!!!!!!!

  	UCA0BR0 = 26;//38400 , dá erro repetindo o primeiro caracter a 76800
  	UCA0BR1 = 0;
  	UCA0MCTL = 0;
  	UCA0CTL1 &= ~UCSWRST;                     // **Initialize USCI state machine**
  	IE2 |= UCA0RXIE;                          // Enable USCI_A0 RX interrupt

  	__bis_SR_register(GIE);       // Enter LPM3 w/ int until Byte RXed

  	char cont = 0;
  	Temporizador t; inicializa_temporizador(100, &t); //5000 ms	//P3OUT = 48;
  	//Temporizador* temp = (Temporizador* )malloc(sizeof(Temporizador));
  	//inicializa_temporizador(10,temp);
  	while(1){
  		/*char* ptr_a = "VEJA"; char* ptr_b = ptr_a;
	  while(1){
		  while(*ptr_a) {
		  		  while(!(IFG2 & UCA0TXIFG));
		  		  UCA0TXBUF = *ptr_a++;
		  	  }
		  ptr_a = ptr_b;
	  }*/
  		if (passou_tempo(&t)) { reseta_temporizador(&t); P1OUT ^= BIT0;
  		switch (cont) {
  		case 0:
  		{
  			char* msgUser = "USER\n";
  			while (*msgUser != '\0'){//if (e==0) ... implementar semaforo aqui.
  				while (!(IFG2 & UCA0TXIFG));  // Aguarda buffer livre
  				UCA0TXBUF = *msgUser++;
  			}
  		}
  		//imprima("Fazer Joao\r\n");//imprima("\r\n1)THEY\r\n");imprima("\r\n1)RGRGNRNGORNG\r\n");
  		break;
  		case 1:
  			//imprima("Com esta\r\n");
  		{
  			//char* buffer;//para alocacao dinamica
  			//buffer = (char* )malloc(40);//lembre-se tem que reservar barra 0
  			//if (buffer != NULL){
  			//strcpy(buffer, "Msg do usuario\n");//"1878 bytes programmed Programming completed.Prog/Verify: 0.6 sec - Over all: 1.2 sec\n");
  			//char* ptr_aux = buffer;
  			char* buffer = "Msg do usuario\n";
  			while (*buffer != '\0'){
  				if (e == 0){//semaforo simples, mas nao garantido. Melhorou com ele
  					while (!(IFG2 & UCA0TXIFG));  // Aguarda buffer livre
  					UCA0TXBUF = *buffer++;
  				}//if (e == 0)
  			}//while buffer
  			//free(ptr_aux);
  			//}else {
  			//	imprima("Error Alocacao\n");
  			//}
  		}//bloco {}
  		break;
  		case 2:
  			imprima("Esta versao\r\n");
  			break;
  		case 3:
  			imprima("Aqui que fiz\r\n");
  			/*case 4:
				imprima(buff);
			break;*/
  			/*case 5:
				__bis_SR_register(LPM0);//da no mesmo disso __bis_SR_register(LPM0 or GIE);
				break;*/
  		}//switch
  		cont++;		cont %= 5;
  		/*__asm(" NOP");//para marcar a localização
		__asm(" NOP");*/

  		} //passou tempo
  		//teste do temporizador alocado dinamicamente que funcionou perfeitamente.
  		//if (passou_tempo(temp)){ reseta_temporizador(temp); P1OUT ^= BIT0; }
  	}//while 1
}//main

void imprima(char* ptr_C){
	fila_msgs[qtd++] = ptr_C;//adiciona o ponteiro a fila
	if (qtd > 3) {pc = "ERROR_FILA_CHEIA\r\n"; qtd = 3;}//=3 para saturar no fim, no error
}

/*void pc_to_string(char *str) {
	unsigned int val = pece; 	int i;

	for (i = 0; i < 4; i++) {
		unsigned char nibble = (val >> (12 - 4 * i)) & 0x0F;

		if (nibble < 10)
			str[i] = '0' + nibble;
		else
			str[i] = 'A' + (nibble - 10);
	}

	str[4] = '\0';
}*/
//para depurar uma função?
//void func(void);
//unsigned int pc = (unsigned int)func; //pega o endereço do PC da função
//if ( func =< pc < foo)
//void foo();

/* Dentro da RTI
unsigned int *sp;
sp = (unsigned int *)_get_SP_register();
unsigned int pc_salvo = *(sp); // ou offset dependendo do compilador
*/
/*
 * 	//_get_SP_register
  	1. 🔎 Debug e análise de memória

  	Você pode verificar:

  	quanto da pilha está sendo usada
  	se há risco de stack overflow
 * */


/*IE2 |= UCA0TXIE;                        // Enable USCI_A0 TX interruptUCA0TXBUF = *pc++;//string1[i++]; //triga o tx inicial*/


