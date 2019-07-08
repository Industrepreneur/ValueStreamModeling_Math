



//xx using namespace class_part

//#pragma once

 
#pragma once 

class class_ibom
{
public:
	
	 class_ibom(void);
	 ~class_ibom(void);

        double  upa;
        double  salv;

		int prnt_ptr; //  class_part   * n_prt;
        int cmpt_ptr; //class_part   * n_cmpt;

		int me_ptr;  //  pointer to me  +1  pointer to next ? 
};

