module Node

open System.Net
open System.Net.Sockets
open NodeCommon

let BuildPeerNode nodeName =
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName 0
    {NodeName = assignedNodeName; TcpListener = null; Pid = pid; Port = 0; Cookie = null}

let BuildSelfNode nodeName erlangCookie port =
    let tcpListener = new TcpListener(IPAddress, port)
    do tcpListener.Start()
    let assignedPort = match port with
                       | _ when port <> 0 -> port
                       | _ -> (tcpListener.LocalEndpoint :?> IPEndPoint).Port
    let assignedNodeName = BuildNodeName nodeName
    let pid = CreatePid assignedNodeName assignedPort
    {NodeName = assignedNodeName; TcpListener = tcpListener; Pid = pid; Port = assignedPort; Cookie = erlangCookie}
   
let ConnectNodes selfNode peerNode = 
    {SelfNode = selfNode; PeerNode = peerNode; IsConnected = true}