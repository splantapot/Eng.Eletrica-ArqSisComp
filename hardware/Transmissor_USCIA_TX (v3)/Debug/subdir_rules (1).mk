################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Each subdirectory must supply rules for building sources it contributes
Escrita_memoria.obj: ../Escrita_memoria.c $(GEN_OPTS) $(GEN_HDRS)
	@echo 'Building file: $<'
	@echo 'Invoking: MSP430 Compiler'
	"C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/bin/cl430" -vmsp --abi=eabi --include_path="C:/Users/evert/workspace_v5_5/Projeto4" --include_path="C:/ti/ccsv5/ccsv5/ccs_base/msp430/include" --include_path="C:/Users/evert/workspace_v5_5/Transmissor_USCIA_TX" --include_path="C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/include" -g --define=__MSP430G2553__ --diag_warning=225 --display_error_number --diag_wrap=off --printf_support=minimal --preproc_with_compile --preproc_dependency="Escrita_memoria.pp" $(GEN_OPTS__FLAG) "$<"
	@echo 'Finished building: $<'
	@echo ' '

RTIs.obj: ../RTIs.c $(GEN_OPTS) $(GEN_HDRS)
	@echo 'Building file: $<'
	@echo 'Invoking: MSP430 Compiler'
	"C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/bin/cl430" -vmsp --abi=eabi --include_path="C:/Users/evert/workspace_v5_5/Projeto4" --include_path="C:/ti/ccsv5/ccsv5/ccs_base/msp430/include" --include_path="C:/Users/evert/workspace_v5_5/Transmissor_USCIA_TX" --include_path="C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/include" -g --define=__MSP430G2553__ --diag_warning=225 --display_error_number --diag_wrap=off --printf_support=minimal --preproc_with_compile --preproc_dependency="RTIs.pp" $(GEN_OPTS__FLAG) "$<"
	@echo 'Finished building: $<'
	@echo ' '

Transmissao_str_raw.obj: ../Transmissao_str_raw.c $(GEN_OPTS) $(GEN_HDRS)
	@echo 'Building file: $<'
	@echo 'Invoking: MSP430 Compiler'
	"C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/bin/cl430" -vmsp --abi=eabi --include_path="C:/Users/evert/workspace_v5_5/Projeto4" --include_path="C:/ti/ccsv5/ccsv5/ccs_base/msp430/include" --include_path="C:/Users/evert/workspace_v5_5/Transmissor_USCIA_TX" --include_path="C:/ti/ccsv5/ccsv5/tools/compiler/msp430_4.2.1/include" -g --define=__MSP430G2553__ --diag_warning=225 --display_error_number --diag_wrap=off --printf_support=minimal --preproc_with_compile --preproc_dependency="Transmissao_str_raw.pp" $(GEN_OPTS__FLAG) "$<"
	@echo 'Finished building: $<'
	@echo ' '


