module PeerNode

open NodeCommon

let BuildPeerNode nodeName =
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName 0
    {NodeName = assignedNodeName; TcpListener = null; Pid = pid; Port = 0; Cookie = null}
