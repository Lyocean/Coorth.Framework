namespace Coorth;
#region Action1
public delegate void ActionR1<TP1>(ref TP1 p1);
public delegate TResult FuncR1<TP1, out TResult>(ref TP1 p1);

public delegate void ActionI1<TP1>(in TP1 p1);
public delegate TResult FuncI1<TP1, out TResult>(in TP1 p1);

#endregion

#region Action2
public delegate void ActionR2<TP1, TP2>(ref TP1 p1, ref TP2 p2);
public delegate TResult FuncR2<TP1, TP2, out TResult>(ref TP1 p1, ref TP2 p2);

public delegate void ActionI1R1<TP1, TP2>(in TP1 p1, ref TP2 p2);
public delegate TResult FuncI1R1<TP1, TP2, out TResult>(in TP1 p1, ref TP2 p2);

public delegate void ActionI2<TP1, TP2>(in TP1 p1, in TP2 p2);
public delegate TResult FuncI2<TP1, TP2, out TResult>(in TP1 p1, in TP2 p2);

#endregion

#region Action3
public delegate void ActionR3<TP1, TP2, TP3>(ref TP1 p1, ref TP2 p2, ref TP3 p3);
public delegate TResult FuncR3<TP1, TP2, TP3, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3);

public delegate void ActionI1R2<TP1, TP2, TP3>(in TP1 p1, ref TP2 p2, ref TP3 p3);
public delegate TResult FuncI1R2<TP1, TP2, TP3, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3);

public delegate void ActionI2R1<TP1, TP2, TP3>(in TP1 p1, in TP2 p2, ref TP3 p3);
public delegate TResult FuncI2R1<TP1, TP2, TP3, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3);

public delegate void ActionI3<TP1, TP2, TP3>(in TP1 p1, in TP2 p2, in TP3 p3);
public delegate TResult FuncI3<TP1, TP2, TP3, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3);

#endregion

#region Action4
public delegate void ActionR4<TP1, TP2, TP3, TP4>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);
public delegate TResult FuncR4<TP1, TP2, TP3, TP4, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

public delegate void ActionI1R3<TP1, TP2, TP3, TP4>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);
public delegate TResult FuncI1R3<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

public delegate void ActionI2R2<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4);
public delegate TResult FuncI2R2<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4);

public delegate void ActionI3R1<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4);
public delegate TResult FuncI3R1<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4);

public delegate void ActionI4<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4);
public delegate TResult FuncI4<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4);

#endregion

#region Action5
public delegate void ActionR5<TP1, TP2, TP3, TP4, TP5>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);
public delegate TResult FuncR5<TP1, TP2, TP3, TP4, TP5, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

public delegate void ActionI1R4<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);
public delegate TResult FuncI1R4<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

public delegate void ActionI2R3<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);
public delegate TResult FuncI2R3<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

public delegate void ActionI3R2<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5);
public delegate TResult FuncI3R2<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5);

public delegate void ActionI4R1<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5);
public delegate TResult FuncI4R1<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5);

public delegate void ActionI5<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5);
public delegate TResult FuncI5<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5);

#endregion

#region Action6
public delegate void ActionR6<TP1, TP2, TP3, TP4, TP5, TP6>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);
public delegate TResult FuncR6<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

public delegate void ActionI1R5<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);
public delegate TResult FuncI1R5<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

public delegate void ActionI2R4<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);
public delegate TResult FuncI2R4<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

public delegate void ActionI3R3<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);
public delegate TResult FuncI3R3<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

public delegate void ActionI4R2<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6);
public delegate TResult FuncI4R2<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6);

public delegate void ActionI5R1<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6);
public delegate TResult FuncI5R1<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6);

public delegate void ActionI6<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6);
public delegate TResult FuncI6<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6);

#endregion

#region Action7
public delegate void ActionR7<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncR7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI1R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncI1R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI2R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncI2R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI3R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncI3R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI4R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncI4R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI5R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7);
public delegate TResult FuncI5R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7);

public delegate void ActionI6R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7);
public delegate TResult FuncI6R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7);

public delegate void ActionI7<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7);
public delegate TResult FuncI7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7);

#endregion

#region Action8
public delegate void ActionR8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncR8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI1R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI1R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI2R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI2R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI3R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI3R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI4R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI4R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI5R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI5R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI6R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8);
public delegate TResult FuncI6R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8);

public delegate void ActionI7R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8);
public delegate TResult FuncI7R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8);

public delegate void ActionI8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8);
public delegate TResult FuncI8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8);

#endregion

#region Action9
public delegate void ActionR9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncR9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI1R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI1R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI2R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI2R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI3R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI3R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI4R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI4R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI5R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI5R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI6R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI6R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI7R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9);
public delegate TResult FuncI7R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9);

public delegate void ActionI8R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9);
public delegate TResult FuncI8R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9);

public delegate void ActionI9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9);
public delegate TResult FuncI9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9);

#endregion

#region Action10
public delegate void ActionR10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncR10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI1R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI1R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI2R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI2R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI3R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI3R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI4R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI4R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI5R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI5R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI6R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI6R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI7R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI7R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI8R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10);
public delegate TResult FuncI8R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10);

public delegate void ActionI9R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10);
public delegate TResult FuncI9R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10);

public delegate void ActionI10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10);
public delegate TResult FuncI10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10);

#endregion

#region Action11
public delegate void ActionR11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncR11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI1R10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI1R10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI2R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI2R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI3R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI3R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI4R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI4R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI5R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI5R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI6R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI6R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI7R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI7R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI8R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI8R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI9R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10, ref TP11 p11);
public delegate TResult FuncI9R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10, ref TP11 p11);

public delegate void ActionI10R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, ref TP11 p11);
public delegate TResult FuncI10R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, ref TP11 p11);

public delegate void ActionI11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11);
public delegate TResult FuncI11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11);

#endregion

#region Action12
public delegate void ActionR12<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncR12<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI1R11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI1R11<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI2R10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI2R10<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI3R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI3R9<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI4R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI4R8<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI5R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI5R7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI6R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI6R6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI7R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI7R5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, ref TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI8R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI8R4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, ref TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI9R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI9R3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, ref TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI10R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, ref TP11 p11, ref TP12 p12);
public delegate TResult FuncI10R2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, ref TP11 p11, ref TP12 p12);

public delegate void ActionI11R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11, ref TP12 p12);
public delegate TResult FuncI11R1<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11, ref TP12 p12);

public delegate void ActionI12<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11, in TP12 p12);
public delegate TResult FuncI12<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7, in TP8 p8, in TP9 p9, in TP10 p10, in TP11 p11, in TP12 p12);

#endregion

