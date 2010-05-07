module NodeCommonTests

open NUnit.Framework
open SpecUnit
open NodeCommon
open System.Net.Sockets

[<TestFixture>]
type NodeCommon__When_building_a_node_name_with_no_provided_at_sign () =
    [<DefaultValue(false)>]
    val mutable _result : string
    inherit ContextSpecification
        override x.Because () =
            x._result <- BuildNodeName "testnode"
        [<Test>]
        member x.should_have_a_result_of_nodename_plus_at_sign_plus_computer_name () =
            do x._result.ShouldEqual "testnode@dmohl-PC"

[<TestFixture>]
type NodeCommon__When_building_a_node_name_with_at_sign () =
    [<DefaultValue(false)>]
    val mutable _result : string
    inherit ContextSpecification
        override x.Because () =
            x._result <- BuildNodeName "testnode2@dmohl"
        [<Test>]
        member x.should_have_a_result_equal_to_the_provided_build_node_name () =
            do x._result.ShouldEqual "testnode2@dmohl"
