module SelfNodeTests

open NUnit.Framework
open SpecUnit
open SelfNode
open System.Net.Sockets

[<TestFixture>]
type SelfNode__When_creating_a_self_node () =
    [<DefaultValue(false)>]
    val mutable _result : LocalNode
    inherit ContextSpecification
        override x.Because () =
            x._result <- SelfNode.BuildSelfNode "testnode" "testcookie" 1234 
        [<Test>]
        member x.should_have_a_tcpListener_of_type_TcpListener () =
            do x._result.TcpListener.ShouldBeOfType(typeof<TcpListener>)
        [<Test>]
        member x.should_have_a_pid_with_an_id_of_1234 () =
            do x._result.Pid.Id.ShouldEqual(1234)
        [<Test>]
        member x.should_have_a_port_of_1234 () =
            do x._result.Port.ShouldEqual(1234)
        override x.Because_After () =           
            do x._result.TcpListener.Stop()
