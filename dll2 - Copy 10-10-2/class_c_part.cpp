
#include "StdAfx.h"
#include "class_part.h" 

	

class class_c_part
{

public:  


	int count;
	
	class_part *  c_part[1];

	int  point;

class_c_part::class_c_part(int count1)
{
	int i;

	count = count1;
	//  allow for count1 pointers ...

	class_part * c_part = new class_part[count];
	   
	return;
};


class_c_part::~class_c_part(void)
{

	int i;
	class_part * part1;

	for (i =0; i< count; i++) { 
		 part1 = c_part[i];
		//xx  err ~part1(); erase contents 
	}
};

//    methods ....-----------------------------------------------------------------------------

	class_part *first_part()  { 
	     point = 0;
		return c_part[0];
	};

	class_part *next_part()  { 
		point +=1;
		if (point >=count) return NULL;
		else return c_part[point];
	};

	class_part *add_part(char * name1, double size1)  { 
		class_part * l1 = new class_part;

		c_part[point] = l1; 

		  //xx ?  or     l1->name = name1;
		//xx          strcpy(l1->name, name1);

		point +=1;

		return c_part[point-1];
	};
	

};