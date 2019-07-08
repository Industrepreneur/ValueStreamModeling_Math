#include "class_lab.cpp"
#include "afx.h"

class class_c_lab
{

public:  

	int count;
	
	class_lab *  c_lab[1];

	int  point;

class_c_lab::class_c_lab(int count1)
{
	int i;

	count = count1;
	//  allow for count1 pointers ...
	  class_lab * c_lab = new class_lab[count1];

	  	
	return;
};


class_c_lab::~class_c_lab(void)
{

	int i;
	class_lab * lab1;
	for (i =0; i< count; i++) { 
		lab1 = c_lab[i];
		//  err ~lab1(); erase contents 
	}
};

	

//    methods ....-----------------------------------------------------------------------------


	class_lab *first_lab_ptr()  { 
		point = 0;
		return c_lab[0];
	};

	class_lab *next_lab_ptr()  { 
		point +=1;
		if (point >=count) return NULL;
		else return c_lab[point];
	};

	class_lab *add_lab(char * name1, double size1)  { 
		class_lab * l1 = new class_lab;
		point +=1;
		c_lab[point] = l1;  

		//err       l1->name = name1;
		// err  or  strcpy(l1->name = name1);
		l1->num  = size1;

		return c_lab[point];
	};

	int find_lab_name( char * lname1) { 
			return (-1);
	}

	class_lab * get_lab_num( int num) {
		return all_lab(num);
	}
};
