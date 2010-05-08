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

[<TestFixture>]
type Node__When_connecting_two_nodes () =
    [<DefaultValue(false)>]
    val mutable _result : Connection
    inherit ContextSpecification
        override x.Because () =
            let selfNode = Node.BuildSelfNode "testnode" "testcookie" 1234 
            let peerNode = Node.BuildPeerNode "testpeernode@dmohl-PC"
            x._result <- Node.ConnectNodes selfNode peerNode
        [<Test>]
        member x.should_have_a_port_on_the_self_node_of_1234 () =
            do x._result.SelfNode.Port.ShouldEqual 1234
        [<Test>]
        member x.should_have_a_node_name_of_testpeernode_at_dmohl_PC_on_the_peer_node() =
            do x._result.PeerNode.NodeName.ShouldEqual "testpeernode@dmohl-PC"
        [<Test>]
        member x.should_have_an_IsConnected_flag_of_true() =
            do x._result.IsConnected.ShouldBeTrue ()
        override x.Because_After () =           
            do x._result.SelfNode.TcpListener.Stop()
