﻿#region ORIGINS, COPYRIGHTS, AND LICENSE
/*

This C# version of LAPACK is derivied from http://www.netlib.org/clapack/,
and the original copyright and license is as follows:

Copyright (c) 1992-2008 The University of Tennessee.  All rights reserved.
$COPYRIGHT$ Additional copyrights may follow $HEADER$

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer. 
  
- Redistributions in binary form must reproduce the above copyright
  notice, this list of conditions and the following disclaimer listed
  in this license in the documentation and/or other materials
  provided with the distribution.
  
- Neither the name of the copyright holders nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.
  
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT  
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT 
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT  
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

public partial class ManagedLapack
{
    public static unsafe int dtrsm(char side, char uplo, char transa, char diag,
        int m, int n, double alpha, double* a, int lda, double* b, int ldb)
    {
         /* System generated locals */
        int a_dim1, a_offset, b_dim1, b_offset, i__1, i__2, i__3;

        /* Local variables */
        int i__, j, k, info;
        double temp;
        bool lside;
        int nrowa;
        bool upper;
        bool nounit;

        /* Parameter adjustments */
        a_dim1 = lda;
        a_offset = 1 + a_dim1;
        a -= a_offset;
        b_dim1 = ldb;
        b_offset = 1 + b_dim1;
        b -= b_offset;

        /* Function Body */
        lside = lsame(side, 'L');
        if (lside) {
	    nrowa = m;
        } else {
	    nrowa = n;
        }
        nounit = lsame(diag, 'N');
        upper = lsame(uplo, 'U');

        info = 0;
        if (! lside && ! lsame(side, 'R')) {
	    info = 1;
        } else if (! upper && ! lsame(uplo, 'L')) {
	    info = 2;
        } else if (! lsame(transa, 'N') && ! lsame(transa, 
	         'T') && ! lsame(transa, 'C')) {
	    info = 3;
        } else if (! lsame(diag, 'U') && ! lsame(diag, 
	        'N')) {
	    info = 4;
        } else if (m < 0) {
	    info = 5;
        } else if (n < 0) {
	    info = 6;
        } else if (lda < max(1,nrowa)) {
	    info = 9;
        } else if (ldb < max(1,m)) {
	    info = 11;
        }
        if (info != 0) {
	    xerbla("DTRSM ", info);
	    return 0;
        }

    /*     Quick return if possible. */

        if (n == 0) {
	    return 0;
        }

    /*     And when  alpha.eq.zero. */

        if (alpha == 0.0) {
	    i__1 = n;
	    for (j = 1; j <= i__1; ++j) {
	        i__2 = m;
	        for (i__ = 1; i__ <= i__2; ++i__) {
		    b[i__ + j * b_dim1] = 0.0;
    /* L10: */
	        }
    /* L20: */
	    }
	    return 0;
        }

    /*     Start the operations. */

        if (lside) {
	    if (lsame(transa, 'N')) {

    /*           Form  B := alpha*inv( A )*B. */

	        if (upper) {
		    i__1 = n;
		    for (j = 1; j <= i__1; ++j) {
		        if (alpha != 1.0) {
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + j * b_dim1] = alpha * b[i__ + j * b_dim1]
				        ;
    /* L30: */
			    }
		        }
		        for (k = m; k >= 1; --k) {
			    if (b[k + j * b_dim1] != 0.0) {
			        if (nounit) {
				    b[k + j * b_dim1] /= a[k + k * a_dim1];
			        }
			        i__2 = k - 1;
			        for (i__ = 1; i__ <= i__2; ++i__) {
				    b[i__ + j * b_dim1] -= b[k + j * b_dim1] * a[
					    i__ + k * a_dim1];
    /* L40: */
			        }
			    }
    /* L50: */
		        }
    /* L60: */
		    }
	        } else {
		    i__1 = n;
		    for (j = 1; j <= i__1; ++j) {
		        if (alpha != 1.0) {
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + j * b_dim1] = alpha * b[i__ + j * b_dim1]
				        ;
    /* L70: */
			    }
		        }
		        i__2 = m;
		        for (k = 1; k <= i__2; ++k) {
			    if (b[k + j * b_dim1] != 0.0) {
			        if (nounit) {
				    b[k + j * b_dim1] /= a[k + k * a_dim1];
			        }
			        i__3 = m;
			        for (i__ = k + 1; i__ <= i__3; ++i__) {
				    b[i__ + j * b_dim1] -= b[k + j * b_dim1] * a[
					    i__ + k * a_dim1];
    /* L80: */
			        }
			    }
    /* L90: */
		        }
    /* L100: */
		    }
	        }
	    } else {

    /*           Form  B := alpha*inv( A' )*B. */

	        if (upper) {
		    i__1 = n;
		    for (j = 1; j <= i__1; ++j) {
		        i__2 = m;
		        for (i__ = 1; i__ <= i__2; ++i__) {
			    temp = alpha * b[i__ + j * b_dim1];
			    i__3 = i__ - 1;
			    for (k = 1; k <= i__3; ++k) {
			        temp -= a[k + i__ * a_dim1] * b[k + j * b_dim1];
    /* L110: */
			    }
			    if (nounit) {
			        temp /= a[i__ + i__ * a_dim1];
			    }
			    b[i__ + j * b_dim1] = temp;
    /* L120: */
		        }
    /* L130: */
		    }
	        } else {
		    i__1 = n;
		    for (j = 1; j <= i__1; ++j) {
		        for (i__ = m; i__ >= 1; --i__) {
			    temp = alpha * b[i__ + j * b_dim1];
			    i__2 = m;
			    for (k = i__ + 1; k <= i__2; ++k) {
			        temp -= a[k + i__ * a_dim1] * b[k + j * b_dim1];
    /* L140: */
			    }
			    if (nounit) {
			        temp /= a[i__ + i__ * a_dim1];
			    }
			    b[i__ + j * b_dim1] = temp;
    /* L150: */
		        }
    /* L160: */
		    }
	        }
	    }
        } else {
	    if (lsame(transa, 'N')) {

    /*           Form  B := alpha*B*inv( A ). */

	        if (upper) {
		    i__1 = n;
		    for (j = 1; j <= i__1; ++j) {
		        if (alpha != 1.0) {
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + j * b_dim1] = alpha * b[i__ + j * b_dim1]
				        ;
    /* L170: */
			    }
		        }
		        i__2 = j - 1;
		        for (k = 1; k <= i__2; ++k) {
			    if (a[k + j * a_dim1] != 0.0) {
			        i__3 = m;
			        for (i__ = 1; i__ <= i__3; ++i__) {
				    b[i__ + j * b_dim1] -= a[k + j * a_dim1] * b[
					    i__ + k * b_dim1];
    /* L180: */
			        }
			    }
    /* L190: */
		        }
		        if (nounit) {
			    temp = 1.0 / a[j + j * a_dim1];
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + j * b_dim1] = temp * b[i__ + j * b_dim1];
    /* L200: */
			    }
		        }
    /* L210: */
		    }
	        } else {
		    for (j = n; j >= 1; --j) {
		        if (alpha != 1.0) {
			    i__1 = m;
			    for (i__ = 1; i__ <= i__1; ++i__) {
			        b[i__ + j * b_dim1] = alpha * b[i__ + j * b_dim1]
				        ;
    /* L220: */
			    }
		        }
		        i__1 = n;
		        for (k = j + 1; k <= i__1; ++k) {
			    if (a[k + j * a_dim1] != 0.0) {
			        i__2 = m;
			        for (i__ = 1; i__ <= i__2; ++i__) {
				    b[i__ + j * b_dim1] -= a[k + j * a_dim1] * b[
					    i__ + k * b_dim1];
    /* L230: */
			        }
			    }
    /* L240: */
		        }
		        if (nounit) {
			    temp = 1.0 / a[j + j * a_dim1];
			    i__1 = m;
			    for (i__ = 1; i__ <= i__1; ++i__) {
			        b[i__ + j * b_dim1] = temp * b[i__ + j * b_dim1];
    /* L250: */
			    }
		        }
    /* L260: */
		    }
	        }
	    } else {

    /*           Form  B := alpha*B*inv( A' ). */

	        if (upper) {
		    for (k = n; k >= 1; --k) {
		        if (nounit) {
			    temp = 1.0 / a[k + k * a_dim1];
			    i__1 = m;
			    for (i__ = 1; i__ <= i__1; ++i__) {
			        b[i__ + k * b_dim1] = temp * b[i__ + k * b_dim1];
    /* L270: */
			    }
		        }
		        i__1 = k - 1;
		        for (j = 1; j <= i__1; ++j) {
			    if (a[j + k * a_dim1] != 0.0) {
			        temp = a[j + k * a_dim1];
			        i__2 = m;
			        for (i__ = 1; i__ <= i__2; ++i__) {
				    b[i__ + j * b_dim1] -= temp * b[i__ + k * 
					    b_dim1];
    /* L280: */
			        }
			    }
    /* L290: */
		        }
		        if (alpha != 1.0) {
			    i__1 = m;
			    for (i__ = 1; i__ <= i__1; ++i__) {
			        b[i__ + k * b_dim1] = alpha * b[i__ + k * b_dim1]
				        ;
    /* L300: */
			    }
		        }
    /* L310: */
		    }
	        } else {
		    i__1 = n;
		    for (k = 1; k <= i__1; ++k) {
		        if (nounit) {
			    temp = 1.0 / a[k + k * a_dim1];
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + k * b_dim1] = temp * b[i__ + k * b_dim1];
    /* L320: */
			    }
		        }
		        i__2 = n;
		        for (j = k + 1; j <= i__2; ++j) {
			    if (a[j + k * a_dim1] != 0.0) {
			        temp = a[j + k * a_dim1];
			        i__3 = m;
			        for (i__ = 1; i__ <= i__3; ++i__) {
				    b[i__ + j * b_dim1] -= temp * b[i__ + k * 
					    b_dim1];
    /* L330: */
			        }
			    }
    /* L340: */
		        }
		        if (alpha != 1.0) {
			    i__2 = m;
			    for (i__ = 1; i__ <= i__2; ++i__) {
			        b[i__ + k * b_dim1] = alpha * b[i__ + k * b_dim1]
				        ;
    /* L350: */
			    }
		        }
    /* L360: */
		    }
	        }
	    }
        }

        return 0;
    } 
}

