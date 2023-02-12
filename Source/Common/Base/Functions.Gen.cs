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

