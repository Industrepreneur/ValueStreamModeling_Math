


/*Microsoft files*/
#include <stdio.h>
#include <string.h>
#include <stdlib.h>


#include "class_model.cpp"

// int gstrcmp( char * a1, char * a2 );

/* ################### */
class_lab *   find_labor(class_model * c1, char * name)
{

		class_lab *lab;
		int i;

		if(strlen(name) == 0) return (c1->all_lab[0]);


        for(i=0;i<c1->lab_count; i++) {
			lab = c1->all_lab[i];

                if (strcmp(name, lab->name) == 0) 
                        
                        return (lab);
              
        }

        return (NULL);
}
/* ################### */
class_eq * find_equip(class_model * c1, char * name)
{

		class_eq *eq;
		int i;

        if(strlen(name) ==0) return (c1->all_eq[0]);
		


        for(i=0;i<c1->eq_count; i++) {
			eq = c1->all_eq[i];

                if (strcmp(name, eq->name) == 0) 
                        
                        return (eq);
              
        }

        return (NULL);
}

/* ################### */
class_part *   find_part(class_model * c1,  char * name)
{

		class_part *part;
		int i;

        if(strlen(name) ==0) return (c1->all_part[0]);
		


        for(i=0;i<c1->part_count; i++) {
			part = c1->all_part[i];

                if (strcmp(name, part->name) == 0) 
                        
                        return (part);
              
        }

        return (NULL);
}
		/* ################### */
class_oper *  find_opas(class_model * c1, char * name, class_part *tpart)
{
       		
		int i;
		int o_count;
		int o_start;

				o_start = tpart->oper_start;
				o_count = tpart->oper_cnt;
				
         for (i= o_start; i< o_start+o_count; i++ )  

        {
                if (strcmp(name, c1->all_oper[i]->name) == 0) 
                      
                    return(c1->all_oper[i]);
               
        }

        return (NULL);
}

/* ################### */
class_route *  find_rt_from(class_model * c1,  char * name, class_part *tpart)
{
        class_route *toprt;
		int i;
		int r_count;
		int r_start;

				r_start = tpart->route_start;
				r_count = tpart->route_cnt;
				
                for (i= r_start; i< r_start+r_count; i++ )  
				{
				   toprt = c1->all_route[i];
                   if (strcmp(name, c1->get_oper_num(toprt->op_from_ptr)->name) == 0)
                        return (toprt);
               
				}
        return (NULL);
}
/* ################### */

/* ****************************************************** */
int gstrcmp( char * a1, char * a2 )
/* ****************************************************** */
{
  if ( ((a1 == NULL) || (strlen(a1)==0) ) &&
       ((a2 == NULL) || (strlen(a2)==0) ) )
       return (0);
  else
    if ( (!((a1 == NULL) || (strlen(a1)==0) )) &&
           ((a2 == NULL) || (strlen(a2)==0) ) )
         return (1);
  else
    if (   ((a1 == NULL) || (strlen(a1)==0) ) &&
         (!((a2 == NULL) || (strlen(a2)==0) )) )
         return (1);
  else return (strcmp(a1, a2));
}

/* ****************************************************** */
int gstrlen( char * a1 )
/* ****************************************************** */
{
  if (a1 == NULL) return(0);
  else return(strlen(a1));
}
