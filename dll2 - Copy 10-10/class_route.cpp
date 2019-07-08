#include "StdAfx.h"
//#include "class_route.h"
#include "class_oper.h"



class class_route { 
public:

		~class_route(void);
		class_route(void);


        int op_from_ptr; //class_oper    * from_op;
        int op_to_ptr;   //class_oper    * to_op;
        double cmmpij;

	
};

		class_route::~class_route(void) {;};
		class_route::class_route(void) {;};