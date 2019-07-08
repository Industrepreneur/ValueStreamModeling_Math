#include "class_oper.h"

#pragma once
class class_oplst
{
public:

#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 400

	     // int index;  gwwd using me_ptr 
        char name[NAME_SIZE];
        double optot;
        double rttot;
        class_oper * oper;
		int me_ptr;
        //  next item in list p_OPLST   * n_oplst;


	class_oplst(void);
	~class_oplst(void);
};

