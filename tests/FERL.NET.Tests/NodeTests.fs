module SelfNodeTests

open NUnit.Framework
open SpecUnit
open Node
open NodeCommon
open System.Net.Sockets

[<TestFixture>]
type Node__When_creating_a_self_node () =
    [<DefaultValue(false)>]
    val mutable _result : Node
    inherit ContextSpecification
        override x.Because () =
            x._result <- Node.BuildSelfNode "testnode" "testcookie" 1234 
        [<Test>]
        member x.should_have_a_tcpListener_of_type_TcpListener () =
            do x._result.TcpListener.ShouldBeOfType typeof<TcpListener>
        [<Test>]
        member x.should_have_a_pid_with_an_id_of_1234 () =
            do x._result.Pid.Id.ShouldEqual 1234
        [<Test>]
        member x.should_have_a_port_of_1234 () =
            do x._result.Port.ShouldEqual 1234
        [<Test>]
        member x.should_have_a_node_name_of_testnode_at_dmohl_pc() =
            do x._result.NodeName.ShouldEqual "testnode@dmohl-PC"
        override x.Because_After () =           
            do x._result.TcpListener.Stop()

[<TestFixture>]
type Node__When_creating_a_peer_node () =
    [<DefaultValue(false)>]
    val mutable _result : Node
    inherit ContextSpecification
        override x.Because () =
            x._result <- Node.BuildPeerNode "testpeernode@dmohl-PC"
        [<Test>]
        member x.should_have_a_tcpListener_of_null () =
            do x._result.TcpListener.ShouldBeNull ()
        [<Test>]
        member x.should_have_a_pid_with_an_id_of_0 () =
            do x._result.Pid.Id.ShouldEqual 0
        [<Test>]
        member x.should_have_a_port_of_0() =
            do x._result.Port.ShouldEqual 0
        [<Test>]
        member x.should_have_a_node_name_of_testnode_at_dmohl_pc() =
            do x._result.NodeName.ShouldEqual "testpeernode@dmohl-PC"
