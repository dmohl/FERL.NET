module PeerNodeTests

open PeerNode
open NodeCommon
open NUnit.Framework
open SpecUnit
open System.Net.Sockets

[<TestFixture>]
type PeerNode__When_creating_a_peer_node () =
    [<DefaultValue(false)>]
    val mutable _result : Node
    inherit ContextSpecification
        override x.Because () =
            x._result <- PeerNode.BuildPeerNode "testpeernode@dmohl-PC"
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
