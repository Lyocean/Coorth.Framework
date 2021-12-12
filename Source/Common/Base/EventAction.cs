
namespace Coorth {
    public delegate void EventAction<TP1>(in TP1 p1);

    public delegate TResult EventFunc<TP1, out TResult>(in TP1 p1);

    public delegate void EventActionR<TP1>(ref TP1 p1);

    public delegate TResult EventFuncR<TP1, out TResult>(ref TP1 p1);

    public delegate void EventAction<TP1, TP2>(in TP1 p1, in TP2 p2);

    public delegate TResult EventFunc<TP1, TP2, out TResult>(in TP1 p1, in TP2 p2);

    public delegate void EventActionR<TP1, TP2>(in TP1 p1, ref TP2 p2);

    public delegate TResult EventFuncR<TP1, TP2, out TResult>(in TP1 p1, ref TP2 p2);

    public delegate void EventActionR2<TP1, TP2>(ref TP1 p1, ref TP2 p2);

    public delegate TResult EventFuncR2<TP1, TP2, out TResult>(ref TP1 p1, ref TP2 p2);

    public delegate void EventAction<TP1, TP2, TP3>(in TP1 p1, in TP2 p2, in TP3 p3);

    public delegate TResult EventFunc<TP1, TP2, TP3, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3);

    public delegate void EventActionR<TP1, TP2, TP3>(in TP1 p1, in TP2 p2, ref TP3 p3);

    public delegate TResult EventFuncR<TP1, TP2, TP3, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3);

    public delegate void EventActionR2<TP1, TP2, TP3>(in TP1 p1, ref TP2 p2, ref TP3 p3);

    public delegate TResult EventFuncR2<TP1, TP2, TP3, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3);

    public delegate void EventActionR3<TP1, TP2, TP3>(ref TP1 p1, ref TP2 p2, ref TP3 p3);

    public delegate TResult EventFuncR3<TP1, TP2, TP3, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3);

    public delegate void EventAction<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4);

    public delegate TResult EventFunc<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4);

    public delegate void EventActionR<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4);

    public delegate TResult EventFuncR<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4);

    public delegate void EventActionR2<TP1, TP2, TP3, TP4>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate TResult EventFuncR2<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate void EventActionR3<TP1, TP2, TP3, TP4>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate TResult EventFuncR3<TP1, TP2, TP3, TP4, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate void EventActionR4<TP1, TP2, TP3, TP4>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate TResult EventFuncR4<TP1, TP2, TP3, TP4, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4);

    public delegate void EventAction<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5);

    public delegate TResult EventFunc<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5);

    public delegate void EventActionR<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5);

    public delegate TResult EventFuncR<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5);

    public delegate void EventActionR2<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate TResult EventFuncR2<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate void EventActionR3<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate TResult EventFuncR3<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate void EventActionR4<TP1, TP2, TP3, TP4, TP5>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate TResult EventFuncR4<TP1, TP2, TP3, TP4, TP5, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate void EventActionR5<TP1, TP2, TP3, TP4, TP5>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate TResult EventFuncR5<TP1, TP2, TP3, TP4, TP5, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5);

    public delegate void EventAction<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6);

    public delegate TResult EventFunc<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6);

    public delegate void EventActionR<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6);

    public delegate void EventActionR2<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR2<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate void EventActionR3<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR3<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate void EventActionR4<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR4<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate void EventActionR5<TP1, TP2, TP3, TP4, TP5, TP6>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR5<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate void EventActionR6<TP1, TP2, TP3, TP4, TP5, TP6>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate TResult EventFuncR6<TP1, TP2, TP3, TP4, TP5, TP6, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6);

    public delegate void EventAction<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7);

    public delegate TResult EventFunc<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, in TP7 p7);

    public delegate void EventActionR<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, in TP6 p6, ref TP7 p7);

    public delegate void EventActionR2<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR2<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, in TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate void EventActionR3<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR3<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, in TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate void EventActionR4<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR4<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, in TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate void EventActionR5<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR5<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, in TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate void EventActionR6<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR6<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(in TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate void EventActionR7<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

    public delegate TResult EventFuncR7<TP1, TP2, TP3, TP4, TP5, TP6, TP7, out TResult>(ref TP1 p1, ref TP2 p2, ref TP3 p3, ref TP4 p4, ref TP5 p5, ref TP6 p6, ref TP7 p7);

}