#include <msp430.h>
#include "intimer.h"

#define TP BIT3         //Transistor BC639: Programming
#define TU BIT4         //Transistor BC337: Utility

/* Main */
int main(void) {

	WDTCTL = WDTPW | WDTHOLD;               // Stop Watchdog Timer
	DCOCTL = 60;   BCSCTL1 = 135;           // Calibrated for 1 MHz

    P1DIR = 0xFF; P1OUT = 0; P1REN = 0xFF;  // P1 Reset + Resistor Enable
    P2DIR = 0xFF; P2OUT = 0; P2REN = 0xFF;  // P2 Reset + Resistor Enable
    P3DIR = 0xFF; P3OUT = 0; P3REN = 0xFF;  // P3 Reset + Resistor Enable

    // Setup for Transistors
    P1DIR &= ~BIT5;                         // Input
    P1REN &= ~(BIT5 + BIT2);                // Without Resistor
    P1REN |= BIT1;                          // <<< Enable PullUp Resistor in MSP RX <<<
    P1DIR |= TP + TU;                       // Output
    P1REN &= ~TP;                           // Without Resistor
    P1OUT &= ~TP;                           // Low for enable User RX TX
    P1REN |= TU;                            // Turn on Resistor
    P1OUT |= TU;                            // Turn on pin
	
    // Serial configuration
    //0b00000110
    P1SEL = BIT1 + BIT2;                    // P1.1 = RXD, P1.2=TXD
    P1SEL2 = BIT1 + BIT2;                   // P1.1 = RXD, P1.2=TXD

    setup_timer();                          // Setup the timer

    // My code
    unsigned char count = 0;
    while (1) {
        if (timer >= 1000) {
            timer = 0;
            count++;
        }
        count = count >= 8? 0 : count;
        P3OUT = count<<5;
    }
}
