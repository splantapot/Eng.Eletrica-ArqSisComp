/*
 * intimer.c
 *
 *  Created on: 18 de mai de 2026
 *      Author: João Victor Carvalho
 */

#include "intimer.h"

volatile unsigned long timer = 0;

void setup_timer(void) {
    // Timer A configuration
    __bis_SR_register(GIE);
    TACTL = TASSEL_2 + ID_0 + MC_1 + TAIE;
    TACCR0 = MOD_TACCR0;
}

void reset_timer(void) {
    // Timer A reset
    TAR = 0;
    timer = 0;
}

#pragma vector=TIMER0_A1_VECTOR
__interrupt void TIMER_A_ISR(void){
    TACTL &= ~TAIFG;
    timer++;
    /*
    switch (TA1IV) {
        case TA1IV_TAIFG:
            timer++;
            break;
    }
    */
}
