//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
#region LGPL License
/*    
    This file is part of ILNumerics.Net Core Module.

    ILNumerics.Net Core Module is free software: you can redistribute it 
    and/or modify it under the terms of the GNU Lesser General Public 
    License as published by the Free Software Foundation, either version 3
    of the License, or (at your option) any later version.

    ILNumerics.Net Core Module is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with ILNumerics.Net Core Module.  
    If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using ILNumerics.Exceptions; 
using ILNumerics.Storage; 
using ILNumerics.Misc; 
/*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="after">
            inCls1
        </source>
        <destination><![CDATA[ILArray<complex>]]></destination>
        <destination><![CDATA[ILArray<fcomplex>]]></destination>
    </type>
    <type>
        <source locate="after">
            inArr1
        </source>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after">
            outCls
        </source>
        <destination><![CDATA[ILArray<complex>]]></destination>
        <destination><![CDATA[ILArray<fcomplex>]]></destination>
    </type>
    <type>
        <source locate="after">
            outArr
        </source>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="nextline">
            HCCompute00
        </source>
        <destination><![CDATA[(*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0 * (*(tmpIn + *leadDimIdx++)).imag;]]></destination>
        <destination><![CDATA[(*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0f * (*(tmpIn + *leadDimIdx++)).imag;]]></destination>
    </type>
    <type>
        <source locate="nextline">
            HCCompute01
        </source>
        <destination><![CDATA[(*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0 * (*(tmpIn + *leadDimIdx++)).imag;]]></destination>
        <destination><![CDATA[(*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0f * (*(tmpIn + *leadDimIdx++)).imag;]]></destination>
    </type>
    <type>
        <source locate="nextline">
            HCCompute02
        </source>
        <destination><![CDATA[(*tmpOut).real = (*tmpIn).real; (*tmpOut++).imag = (*tmpIn++).imag * -1.0;]]></destination>
        <destination><![CDATA[(*tmpOut).real = (*tmpIn).real; (*tmpOut++).imag = (*tmpIn++).imag * -1.0f;]]></destination>
    </type>
    <type>
        <source locate="after">
            HCfuncname
        </source>
        <destination>conj</destination>
        <destination>conj</destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Complex conjugate of array A</destination>
        <destination>Complex conjugate of array A</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination>Complex conjugate of array A</destination>
        <destination>Complex conjugate of array A</destination>
    </type>
 </hycalper>
 */

namespace ILNumerics.BuiltInFunctions {
    public partial class ILMath {

        #region convenience functions (real arguments)
        /// <summary>
        /// complex conjugate of A
        /// </summary>
        /// <param name="A">input array</param>
        /// <returns>the array itself</returns>
        /// <remarks>This overload is provided for convenience only. It eases the implementation of complex functions, where complex conjugate transposes are needed.</remarks>
        public static ILArray<double> conj (ILArray<double> A) { return A; }
        /// <summary>
        /// complex conjugate of A
        /// </summary>
        /// <param name="A">input array</param>
        /// <returns>the array itself</returns>
        /// <remarks>This overload is provided for convenience only. It eases the implementation of complex functions, where complex conjugate transposes are needed.</remarks>
        public static ILArray<float> conj (ILArray<float> A) { return A; }
        #endregion convenience functions (real arguments)

        #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\builtin\sin.cs

        #endregion HYCALPER LOOPEND UNARY_OPERATOR_TEMPLATE@Functions\builtin\sin.cs
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>Complex conjugate of array A</summary>
        /// <param name="A">input array</param>
        /// <returns>Complex conjugate of array A</returns>
        /// <remarks><para>If the input array is empty, an empty array will be returned.</para>
        /// <para>The array returned will be a dense array.</para></remarks>
        public static  ILArray<fcomplex>  conj ( ILArray<fcomplex> A) {
            if (A.IsEmpty)
                return  ILArray<fcomplex> .empty(A.Dimensions); 
            ILDimension inDim = A.Dimensions;
            fcomplex [] retDblArr;
            // build ILDimension    
            int newLength = inDim.NumberOfElements;
            //retDblArr = new  fcomplex [newLength];
            retDblArr = ILMemoryPool.Pool.New< fcomplex > (newLength); 
            int leadDim = 0;
            int leadDimLen = inDim [0];
            if (A.IsReference) {
                #region Reference storage
                // walk along the longest dimension (for performance reasons)
                for (int i = 1; i < inDim.NumberOfDimensions; i++) {
                    if (leadDimLen < inDim [i]) {
                        leadDimLen = inDim [i];
                        leadDim = i;
                    }
                }
                ILIndexOffset idxOffset = A.m_indexOffset;
                int incOut = inDim.SequentialIndexDistance ( leadDim );
                System.Diagnostics.Debug.Assert(!A.IsVector,"Reference arrays of vector size should not exist!"); 
                if (A.IsMatrix) {
                    #region Matrix
                    ////////////////////////////   MATRIX   ////////////////////
                    int secDim = ( leadDim + 1 ) % 2;
                    unsafe {
                        fixed (int* leadDimStart = idxOffset [leadDim],
                                secDimStart = idxOffset [secDim]) {
                            fixed ( fcomplex * pOutArr = retDblArr)
                            fixed ( fcomplex * pInArr = A.m_data) {
                                fcomplex * tmpOut = pOutArr;
                                fcomplex * tmpIn = pInArr;
                                fcomplex * tmpOutEnd = pOutArr + inDim.NumberOfElements - 1;
                                int* secDimEnd = secDimStart + idxOffset [secDim].Length;
                                int* secDimIdx = secDimStart;
                                int* leadDimIdx = leadDimStart;
                                int* leadDimEnd = leadDimStart + leadDimLen;
                                while (secDimIdx < secDimEnd) {
                                    if (tmpOut > tmpOutEnd)
                                        tmpOut = pOutArr + ( tmpOut - tmpOutEnd);
                                    tmpIn = pInArr + *secDimIdx++;
                                    leadDimIdx = leadDimStart;
                                    while (leadDimIdx < leadDimEnd) {   // HC00
                                        (*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0f * (*(tmpIn + *leadDimIdx++)).imag;
                                        tmpOut += incOut;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                } else {
                    #region arbitrary size
                    unsafe {
                        int [] curPosition = new int [A.Dimensions.NumberOfDimensions];
                        fixed (int* leadDimStart = idxOffset [leadDim]) {
                            fixed ( fcomplex * pOutArr = retDblArr)
                            fixed ( fcomplex * pInArr = A.m_data) {
                                fcomplex * tmpOut = pOutArr;
                                fcomplex * tmpOutEnd = tmpOut + retDblArr.Length - 1;
                                fcomplex * tmpIn = pInArr + A.getBaseIndex ( 0, 0 );
                                tmpIn -= idxOffset [leadDim, 0];     // if the first index of leaddim is not 0, it will be added later anyway. so we subtract it here
                                int* leadDimIdx = leadDimStart;
                                int* leadDimEnd = leadDimStart + leadDimLen;
                                int dimLen = curPosition.Length;
                                int d, curD, count = retDblArr.Length / leadDimLen;
                                // start at first element
                                while (count --> 0) {
                                    leadDimIdx = leadDimStart;
                                    while (leadDimIdx < leadDimEnd) {  //HC01
                                        (*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0f * (*(tmpIn + *leadDimIdx++)).imag;
                                        tmpOut += incOut;
                                    }
                                    if (tmpOut > tmpOutEnd)
                                        tmpOut -= retDblArr.Length - 1;

                                    // increment higher dimensions 
                                    d = 1;
                                    while (d < dimLen) {
                                        curD = ( d + leadDim ) % dimLen;
                                        tmpIn -= idxOffset [curD, curPosition [curD]];
                                        curPosition [curD]++;
                                        if (curPosition [curD] < idxOffset [curD].Length) {
                                            tmpIn += idxOffset [curD, curPosition [curD]];
                                            break;
                                        }
                                        curPosition [curD] = 0;
                                        tmpIn += idxOffset [curD, 0];
                                        d++;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            } else {
                // physical -> pointer arithmetic
                #region physical storage
                unsafe {
                    fixed ( fcomplex * pOutArr = retDblArr)
                    fixed ( fcomplex * pInArr = A.m_data) {
                        fcomplex * lastElement = pOutArr + retDblArr.Length;
                        fcomplex * tmpOut = pOutArr;
                        fcomplex * tmpIn = pInArr;
                        while (tmpOut < lastElement) { // HC02
                            (*tmpOut).real = (*tmpIn).real; (*tmpOut++).imag = (*tmpIn++).imag * -1.0f;
                        }
                    }
                }
                #endregion
            }
            return new  ILArray<fcomplex> ( retDblArr, inDim );
        }
        /// <summary>Complex conjugate of array A</summary>
        /// <param name="A">input array</param>
        /// <returns>Complex conjugate of array A</returns>
        /// <remarks><para>If the input array is empty, an empty array will be returned.</para>
        /// <para>The array returned will be a dense array.</para></remarks>
        public static  ILArray<complex>  conj ( ILArray<complex> A) {
            if (A.IsEmpty)
                return  ILArray<complex> .empty(A.Dimensions); 
            ILDimension inDim = A.Dimensions;
            complex [] retDblArr;
            // build ILDimension    
            int newLength = inDim.NumberOfElements;
            //retDblArr = new  complex [newLength];
            retDblArr = ILMemoryPool.Pool.New< complex > (newLength); 
            int leadDim = 0;
            int leadDimLen = inDim [0];
            if (A.IsReference) {
                #region Reference storage
                // walk along the longest dimension (for performance reasons)
                for (int i = 1; i < inDim.NumberOfDimensions; i++) {
                    if (leadDimLen < inDim [i]) {
                        leadDimLen = inDim [i];
                        leadDim = i;
                    }
                }
                ILIndexOffset idxOffset = A.m_indexOffset;
                int incOut = inDim.SequentialIndexDistance ( leadDim );
                System.Diagnostics.Debug.Assert(!A.IsVector,"Reference arrays of vector size should not exist!"); 
                if (A.IsMatrix) {
                    #region Matrix
                    ////////////////////////////   MATRIX   ////////////////////
                    int secDim = ( leadDim + 1 ) % 2;
                    unsafe {
                        fixed (int* leadDimStart = idxOffset [leadDim],
                                secDimStart = idxOffset [secDim]) {
                            fixed ( complex * pOutArr = retDblArr)
                            fixed ( complex * pInArr = A.m_data) {
                                complex * tmpOut = pOutArr;
                                complex * tmpIn = pInArr;
                                complex * tmpOutEnd = pOutArr + inDim.NumberOfElements - 1;
                                int* secDimEnd = secDimStart + idxOffset [secDim].Length;
                                int* secDimIdx = secDimStart;
                                int* leadDimIdx = leadDimStart;
                                int* leadDimEnd = leadDimStart + leadDimLen;
                                while (secDimIdx < secDimEnd) {
                                    if (tmpOut > tmpOutEnd)
                                        tmpOut = pOutArr + ( tmpOut - tmpOutEnd);
                                    tmpIn = pInArr + *secDimIdx++;
                                    leadDimIdx = leadDimStart;
                                    while (leadDimIdx < leadDimEnd) {   // HC00
                                        (*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0 * (*(tmpIn + *leadDimIdx++)).imag;
                                        tmpOut += incOut;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                } else {
                    #region arbitrary size
                    unsafe {
                        int [] curPosition = new int [A.Dimensions.NumberOfDimensions];
                        fixed (int* leadDimStart = idxOffset [leadDim]) {
                            fixed ( complex * pOutArr = retDblArr)
                            fixed ( complex * pInArr = A.m_data) {
                                complex * tmpOut = pOutArr;
                                complex * tmpOutEnd = tmpOut + retDblArr.Length - 1;
                                complex * tmpIn = pInArr + A.getBaseIndex ( 0, 0 );
                                tmpIn -= idxOffset [leadDim, 0];     // if the first index of leaddim is not 0, it will be added later anyway. so we subtract it here
                                int* leadDimIdx = leadDimStart;
                                int* leadDimEnd = leadDimStart + leadDimLen;
                                int dimLen = curPosition.Length;
                                int d, curD, count = retDblArr.Length / leadDimLen;
                                // start at first element
                                while (count --> 0) {
                                    leadDimIdx = leadDimStart;
                                    while (leadDimIdx < leadDimEnd) {  //HC01
                                        (*tmpOut).real = (*(tmpIn + *leadDimIdx)).real; (*tmpOut).imag = -1.0 * (*(tmpIn + *leadDimIdx++)).imag;
                                        tmpOut += incOut;
                                    }
                                    if (tmpOut > tmpOutEnd)
                                        tmpOut -= retDblArr.Length - 1;

                                    // increment higher dimensions 
                                    d = 1;
                                    while (d < dimLen) {
                                        curD = ( d + leadDim ) % dimLen;
                                        tmpIn -= idxOffset [curD, curPosition [curD]];
                                        curPosition [curD]++;
                                        if (curPosition [curD] < idxOffset [curD].Length) {
                                            tmpIn += idxOffset [curD, curPosition [curD]];
                                            break;
                                        }
                                        curPosition [curD] = 0;
                                        tmpIn += idxOffset [curD, 0];
                                        d++;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            } else {
                // physical -> pointer arithmetic
                #region physical storage
                unsafe {
                    fixed ( complex * pOutArr = retDblArr)
                    fixed ( complex * pInArr = A.m_data) {
                        complex * lastElement = pOutArr + retDblArr.Length;
                        complex * tmpOut = pOutArr;
                        complex * tmpIn = pInArr;
                        while (tmpOut < lastElement) { // HC02
                            (*tmpOut).real = (*tmpIn).real; (*tmpOut++).imag = (*tmpIn++).imag * -1.0;
                        }
                    }
                }
                #endregion
            }
            return new  ILArray<complex> ( retDblArr, inDim );
        }

#endregion HYCALPER AUTO GENERATED CODE
    }
}
