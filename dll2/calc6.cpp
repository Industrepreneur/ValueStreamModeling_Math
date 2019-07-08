#include <stdlib.h>
#include <math.h>


/* ################################################ */
void   matinv(int *isol, int *idsol, int nr, int nc, float *a,
int mca, int kwa[], float *det)

/* ################################################ */

/* this subroutine finds the inverse and/or solves
         simultaneous equations, or neither, and
         calculates a determinant of a real matrix.*/


{
        long i, j, k,l, mser, mz;
        int ic,iec,ii,ip,ir,iric,ibmp,
        jbmp,jj,jr,kbmp,kk,kser,
        mad,mdiv,nes,net;

        float piv = 0.0;
        float psto = 0.0;

        ir = nr;
        *isol = 1;
        *idsol = 1;
        if(nr <= 0) goto er1;
        if((ir - mca) > 0) goto er1;
        ic = abs(nc);
        if ((ic - ir) <  0) ic = ir;
        ibmp = mca;
        jbmp = 1;
        kbmp = jbmp + ibmp;
        nes = (ir-1) * ibmp + ir - 1;
        net = nes + ic - ir;
        if(nc == 0) goto er1;
        if(nc < 0)
        {
                mdiv = 1;
                iric = ir - ic;
        }
        else mdiv = 0;
        mad = mdiv;
        mser = 0;
        kser = (ir-1) * mca;
        mz = 0;
        *det = 1.;
reit:
        piv = 0.;
        for(i = mser; i <= kser; i+=mca)
        {
                if((fabs(a[i])-piv) > 0.)
                {
                        piv = fabs(a[i]);
                        ip = i;
                }
        }
        if(piv == 0.) goto er2;
        if(nc > 0)
        {
                i = ip / mca * mca;
                j = mser / mca * mca;
                jj = mser/mca;
                ii = jj + (ip -mser)/mca;
                kwa[jj] = ii;
        }
        else
            {
                i = ip;
                j = mser;
        }

        if(((i / mca) - (mser/mca)) < 0) goto er1;
        else
            {
                if(((ip/mca) - (mser/mca)) > 0)
                {
                        for(k = 1; k <= mca; k++)
                        {
                                psto = a[i];
                                a[i] = a[j];
                                a[j] = psto;
                                i = i + 1;
                                j = j + 1;
                        }
                        *det = - *det;
                }
                psto = a[mser];
                *det = *det * psto;
        }
        if (*det == 0.)
        {
                *idsol = 1;
                *isol = 2;
                return;
        }
        psto = 1. / psto;
        a[mser] = 1.;
        i = mdiv;
        iec = (i / mca) * mca + ic -1;
        for(k = i; k <= iec; k++)
        {
                a[i] = a[i] * psto;
                i = i + 1;
        }
        for(k = mz; k <= kser; k+=mca)
        {
                if(mz != mser)
                {
                        i = mad;
                        j = mdiv;
                        psto = a[mz];
                        if(psto == 0.) goto kbr;
                        a[mz] = 0.;
                        iec = (j / mca) * mca + ic - 1;
                        for(kk = j; kk <= iec; kk++)
                        {
                                a[i] = a[i] - a[j] * psto;
                                j = j + 1;
                                i = i + 1;
                        }
                }
kbr:
                mad = mad + mca;
                if(mad > net)
                        mad = mad - (mad / mca) * mca + 1;
                mz = mz + mca;
                if( mz > net)
                        mz = mz - (mz / mca) * mca + 1;
        }
        kser = kser + 1;
        if ((kser-nes) >  0) goto invr;
        mser = mser + kbmp;
        if(nc >= 0)
        {
                mdiv = mdiv + mca;
                if( mdiv > net)
                        mdiv = mdiv - (mdiv / mca) * mca + 1;
                mz = mser - (mser / mca) * mca;
                mad = 0;
                goto reit;
        }
        mdiv = mdiv + kbmp;
        if(iric == 0)
        {
                mz = mser + mca;
                if( mz > net)
                        mz = mz - (mz / mca) * mca + 1;
        }
        else mz = mser - (mser / mca) * mca;
        mad = mz + 1;
        goto reit;
invr:
        if(nc <  0) return;
        jr = ir - 1;
        for(kk = jr; kk >= 0; kk--)
        {
                if((kwa[kk] - kk) < 0) goto er1;
                if((kwa[kk] - kk) > 0)
                {
                        k = kk ;
                        j = kwa[kk];

                        for(i = 0; i <= jr; i++)
                        {
                                l = (i * mca);
                                psto = a[j+l];
                                a[j+l] = a[k+l];
                                a[k+l] = psto;
                        }
                        goto inv2;
                }
inv2:
                ;
        }
        return;
er1:
        *isol = 3;
        return;
er2:
        *det = 0.;
        *isol = 2;
        *idsol = 1;
        return;
er3:
        *isol = 2;
        *idsol = 2;
        return;
}
