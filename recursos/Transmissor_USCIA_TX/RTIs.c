#include <msp430.h>
#define TEMPO_TICK 10000 //resolver essa duplicidade aqui depois
extern unsigned int tick;//biblioteca temporizador
//C:\Users\evert\workspace_v5_5\Transmissor_USCIA_TX
extern volatile unsigned char* pc;
extern unsigned char qtd; extern char* fila_msgs[]; //char* paux=  (char*)0xC44C;

char* msg = "\r\nTESTE\r\n";//DEON //char SendId = 1;
volatile unsigned char v1 = 'a',v2 = 'b',v3 = 'c', v4 = 'd';//teste de memoria com valores fixos, DEON
volatile unsigned char* vetor_ptr[] = {&v1,&v2, &P3OUT, &P3IN}; //{&P1IN, &P2IN, &P3IN, &v4};

char e = 0, cont = 0, cont2 = 0; static char i = 0; //variaveis do depurador
#pragma vector=TIMER1_A1_VECTOR
__interrupt void TIMER1_A1_ISR_HOOK(void){//RTI do timer E421
	switch (TA1IV) {
	case TA1IV_TACCR2:  //canal 2, //Vem pra cá a cada intervalo de tempo definido pelo ta1ccr2
		if (e == 0){
				if (qtd) {//tem item na fila?
					pc = fila_msgs[0];//pega o primeiro elemento. Obs: deixa por enquanto esse warning, pois assim funcionou. /*pc = msg; pc = paux; int x;  = 0;*/
					unsigned char ite;//indexador ou iterador
					for (ite = 0; ite < (qtd - 1); ite++)//deslocamento os ponteiros
						fila_msgs[ite] = fila_msgs[ite+1];//desloca os elementos na fila, caminhando a fila...
					qtd--;
				}
				if (pc != 0) {//tenho string vindo da fila?//
					UCA0TXBUF = *pc++; IE2 |= UCA0TXIE; e = 1;//tendo, imprima-a
				}	else e = 2;//não tendo string, imprima a sequencia de raws
		}//fim do if e==0

		if (e == 2){//evite else if aqui e switch. Tem que ser assim mesmo, para não haver aguardar o proximo tick
			if (!i) { if (++cont <= 1) goto pula; }//100 ms
			pc = vetor_ptr[i++]; if (i > (sizeof(vetor_ptr)>>1) - 1 ) i = 0;//0 1 2
			UCA0TXBUF = *pc++;//UCA0CTL1 |= UCTXBRK;// É necessário enviar um "dummy byte" para disparar a sequência// UCA0TXBUF = 0x00;
			IE2 |= UCA0TXIE;  //não tenho nem mais string ou dado
			e = 3;
		}

		pula://para gerar o ponto no intuito de marcar que o proximo byte é o RAW inicial
		TA1CCR2 += TEMPO_TICK; // timer1 contando até seu máximo de 65535(modo 2)
		tick++;//obrigatório para que a biblioteca do temporizador funcione
		break;
	}// fim switch taiv
}//RTI Timer1 canal2 usado */

#pragma vector=USCIAB0TX_VECTOR
__interrupt void USCI0TX_ISR(void){	//1) versão com break;
	if (e == 1) { //um switch melhoraria aqui
		if (*pc == 0){ e = 2; cont = 0; IE2 &= ~UCA0TXIE; pc = 0; } //pc em zero é o mesmo indicativo de TXie em zero, talvez desnecessário ter que zerar ele
		else { UCA0TXBUF = *pc++; }
	} else if (e == 3){e =    cont = 0; IE2 &= ~UCA0TXIE; pc = 0; } //talvez desnecessário ter que zerar ele
}

extern void imprima(char* ptr_C);
extern unsigned char escreve_endereco(unsigned char dado); extern volatile unsigned char break_condicao;
#pragma vector=USCIAB0RX_VECTOR
__interrupt void USCI0RX_ISR(void){
	//aplicaçao de escrita na memoria
	break_condicao = UCA0STAT & UCBRK;
	unsigned char dado;	dado = UCA0RXBUF;//leitura do dado recebido na UART RX
	unsigned char retorno = escreve_endereco(dado);//Se estritamente necessário, escreve no endereço

	//comandos r, s, p: Reset, String TESTE, Pausa a CPU do MSP
	if (!retorno){//se não estiver ocupada a recepção com o recebimento dos bytes de escrita na memória, receba os comandos do INFERIOR_DIREITO
		if (dado == 'r')// 'r' reseta por software a gerar o reset PUC por error de senha do WDT
			WDTCTL = 0;

		if (dado == 's'){// 's' imprime a string TESTE para ver se imprime em qualquer tempo aleatorio desejado, ok
			imprima(msg);//imprime a mensagem de TESTE
			_bic_SR_register_on_exit(LPM0_bits);//despausa a main     LPM0_EXIT     //_bis_SR_register_on_exit(LPM0 + GIE);
		}

		if (dado == 'p'){// mudando o valor da porta para ver, ok
			P3OUT = P3IN + (1 << 5);//__bis_SR_register_on_exit(LPM0);
			__bis_SR_register_on_exit(LPM0_bits );//pausa a main ou     sem | GIE da o mesmo efeito. Faz sentido, pois só seta o CPUOFF
		}
	}

}//RTI RX

/*
 //fila_msgs[qtd++] = msg;//fila_msgs
 //if (qtd > 3) {pc = "ERROR_FILA_CHEIA\r\n"; qtd = 3;}
 * */
