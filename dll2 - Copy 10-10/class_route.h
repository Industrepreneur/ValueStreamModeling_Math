

#include "class_oper.h" 
#pragma once

class class_route
{
public:
	class_route(void);
	~class_route(void);

		int part_ptr ;  //  part index 
        int op_from_ptr; //class_oper    * from_op;
        int op_to_ptr;   //class_oper    * to_op;
        double cmmpij;

		int me_ptr;  //  point to me to get next me ..

    
};

