#include <msp430.h>
#define TEMPO_TICK 10000 //resolver essa duplicidade aqui depois
extern unsigned int tick;//biblioteca temporizador
//C:\Users\evert\workspace_v5_5\Transmissor_USCIA_TX
extern volatile unsigned char* pc;
extern unsigned char qtd; extern char* fila_msgs[]; //char* paux=  (char*)0xC44C;

char* msg = "\r\nTESTE\r\n";//DEON //
volatile unsigned char cont3 = 0, var = 0xF0;
volatile unsigned char v1 = 'A',v2 = 'B', v3 = 'C';//, v4 = 'd';//teste de memoria com valores fixos, DEON
volatile unsigned char* vetor_ptr[] = {&v1,&v2, &P3IN, &var};// &v4}; //{&P1IN, &P2IN, &P3IN, &v4};

char e = 0, cont = 0; static char i = 0; //variaveis do depurador
char SendId = 1;
#define clear_e_cont_txie_pc e = 0; cont = 0; IE2 &= ~UCA0TXIE; pc = 0;

#pragma vector=TIMER1_A1_VECTOR
__interrupt void TIMER1_A1_ISR_HOOK(void){//RTI do timer E421
	switch (TA1IV) {
	case TA1IV_TACCR2:  //canal 2, //Vem pra cá a cada intervalo de tempo definido pelo ta1ccr2
		if (e == 0){//estado de decisao, se envia string ou sequencia de dados RAWs
				if (qtd) {//se tem item na fila, pega ele
					pc = fila_msgs[0];//pega o primeiro elemento. Obs: deixa por enquanto esse warning, pois assim funcionou. /*pc = msg; pc = paux; int x;  = 0;*/
					unsigned char ite;//indexador ou iterador
					for (ite = 0; ite < (qtd - 1); ite++)//deslocamento os ponteiros
						fila_msgs[ite] = fila_msgs[ite+1];//desloca os elementos na fila, caminhando a fila...
					qtd--;
				}
				if (pc != 0) {//tenho string vindo da fila?//
					UCA0CTL1 |= UCTXBRK;// É necessário enviar um "dummy byte" para disparar a sequência// UCA0TXBUF = 0x00;
					UCA0TXBUF = 0;//
					IE2 |= UCA0TXIE; e = 1; SendId = 1;//tendo, imprima-a
				}	else e = 2;//não tendo string, imprima a sequencia de raws, começando com "e" em 2
		}//fim do if e==0

		if (e == 2){//
			if (!i) {//dado inicial da sequencia de Raws //if (cont ....)  {      }
				if (++cont >= 4){//versão com contador cont de amostragem
					UCA0CTL1 |= UCTXBRK;// É necessário enviar um "dummy byte" para disparar a sequência// UCA0TXBUF = 0x00;
					UCA0TXBUF = 0;//
					IE2 |= UCA0TXIE; e = 3; SendId = 1; //<<<<<<<<<<<i = 1;
				}//100xtick ms = 40 ms
				else e = 0;//xxx
			}
		}//e == 2

		if(++cont3 >= 100){
		    var ^= 255;
		    cont3 = 0;
		}
		pula://para gerar o ponto no intuito de marcar que o proximo byte é o RAW inicial
		TA1CCR2 += TEMPO_TICK; // timer1 contando até seu máximo de 65535(modo 2)
		tick++;//obrigatório para que a biblioteca do temporizador funcione
		break;
	}// fim switch taiv
}//RTI Timer1 canal2 usado */

#pragma vector=USCIAB0TX_VECTOR
__interrupt void USCI0TX_ISR(void){	//1) versão com break;
	if (!SendId) {//se nao for enviar o ID, i.e, transfira caracteres ou raws.
		switch (e) {
		case 1://desse jeito, em prol da rapidez, não espera o ultimo
			if (*pc == 0){//fim da string, i.e, feito?
				e = 0;  IE2 &= ~UCA0TXIE; pc = 0;
				//clear_e_cont_txie_pc;
			} //pc em zero é o mesmo indicativo de TXie em zero, talvez desnecessário ter que zerar ele
			else { UCA0TXBUF = *pc++; }//nao, i,e, caracteres ainda a enviar
			break;
		case 3:
			UCA0TXBUF = *vetor_ptr[i++];//aponta para o raw e avança o indice i
			if (i > (sizeof(vetor_ptr)>>1) - 1 ) {//enviou todos os raws?
				i = 0;//0 1 2
				clear_e_cont_txie_pc;
				//e = 0;  IE2 &= ~UCA0TXIE; pc = 0
			}//se enviou todos os raws
			break;
		}//fim do switch
	} else { //se é para transferir o ID
		UCA0TXBUF = e; SendId = 0;
	}
}//interrupcao TX

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
